<script setup>
import { getItemById, getItems, post, updateItem } from '@/services/baseServices';
import FooterComponent from '../footer/FooterComponent.vue';
import { enviroment } from '@/enviroments/enviroment';
import { useAuthStore } from '@/store/auth.module';
import { isSupperAdmin } from '../helper/helper';

</script>

<template>
    <div class="container">
        <div class="row justify-content-center">
            <div class="col-md-6 col-md-4">
                <div class="card">
                    <div class="card-header">
                        <h3>{{ title }}</h3>
                        <form name="form" @submit.prevent="save()">
                            <div class="row mb-3">
                                <div class="form-group">
                                    <label class="fw-bold">Name <span class="text-danger">*</span></label>
                                    <input :readonly="readonly" type="text" class="form-control" name="name" v-model="item.name"
                                        placeholder="Enter name" />
                                    <div v-if="name_error" class="invalid-feedback">
                                        <div>{{ name_error }}</div>
                                    </div>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="form-group">
                                    <label class="fw-bold">Phone</label>
                                    <input :readonly="readonly" type="text" class="form-control" name="phone" v-model="item.phone"
                                        placeholder="Enter phone" />
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="form-group">
                                    <label class="fw-bold">Hospital</label>
                                    <select :disabled="readonly" class="form-select" name="hospital_id"
                                     v-model="item.hospital_id">
                                        <option v-for="item in hospitalItems" :key="item.id" :value="item.id">
                                            {{item.name}}
                                        </option>
                                    </select>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="form-group">
                                    <label class="fw-bold">Doctor</label>
                                    <select :disabled="readonly" class="form-select" name="doctor_id"
                                     v-model="item.doctor_id">
                                        <option v-for="item in doctorItems" :key="item.id" :value="item.id">
                                            {{item.last_name}} {{item.first_name}}
                                        </option>
                                    </select>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="col form-group mb-3 d-grid gap-2 d-md-flex">
                                    <button :disabled="readonly" class="btn btn-outline-primary">
                                        <span v-if="loading" class="spinner-border spinner-border-sm mr-1"></span>
                                        Save
                                    </button>
                                    <RouterLink class="btn btn-outline-secondary"
                                        to="/Department">Cancel</RouterLink>
                                </div>
                                <div class="row mb-3">
                                    <div v-if="message_error" class="form-group">
                                       {{ message_error }}
                                    </div>
                                </div>
                        </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <RouterView></RouterView>
    <FooterComponent></FooterComponent>
</template>
<script>
    export default{
        data(){
            return{
                auth: useAuthStore(),
                loading: false,
                readonly: false,
                title: "Add Department",
                name_error:"",
                message_error:"",
                hospitalItems:[],
                doctorItems:[],
                item:{
                    name:"",
                    phone: "",
                    hospital_id: null,
                    doctor_id: null,
                    id: 0,
                },
                apiUrl: `${enviroment.apiUrl}/Departments`
            }
        },
        methods:{
            validName(){
                if (!this.item.name)
                    this.name_error = "Name is required";
                else
                    this.name_error = "";
            },
            async getItem(){
                return await getItemById(`${this.apiUrl}/${this.item.id}`);
            },
            async getHospitalItems(){
                return await getItems(`${enviroment.apiUrl}/Hospitals`);
            },
            async getDoctorItems(){
                return await getItems(`${enviroment.apiUrl}/Doctors`);
            },
            async save(){
                this.validName();
                if (!this.name_en_error){
                    if (this.item.id == 0){
                        
                        await post(`${this.apiUrl}/Add`, this.item).then(response=>{
                            if (response.valid){
                                this.$router.push("/Department");
                            }
                            else
                                this.message_error = response.message;
                        });
                    }
                    else{
                        await updateItem(`${this.apiUrl}/Edit/${this.item.id}`, this.item).then(response=>{
                            if (response.valid){
                                this.$router.push("/Department");
                            }
                            else
                                this.message_error = response.message;
                        });
                    }
                }
            }
        },
        async mounted(){
            this.item.id = this.$route.params["id"] || 0;
            var currentPath = this.$route.path;
            if (this.item.id > 0){
                this.title = "Edit Department";
                if (currentPath.indexOf("View") != -1){
                    this.title = "View Department";
                    this.readonly = true;
                }
                var data = await this.getItem();
                this.item = data.data;
            }
            var categories = await this.getDoctorItems();
            this.doctorItems = categories.data;
            categories = await this.getHospitalItems();
            this.hospitalItems = categories.data;
            if (!isSupperAdmin(this.auth.accountLogin)){
                var hospital_id = this.auth.accountLogin.hospital_id || 0;
                this.hospitalItems = this.hospitalItems.filter(li => li.id == hospital_id);
                this.doctorItems = this.doctorItems.filter(li => li.hospital_id == hospital_id);
            }
        }
    }
</script>