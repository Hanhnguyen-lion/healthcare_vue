<script setup>
import { getItemById, getItems, post, updateItem } from '@/services/baseServices';
import FooterComponent from '../footer/FooterComponent.vue';
import { enviroment } from '@/enviroments/enviroment';
import { validEmail } from '../helper/helper';

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
                                    <label class="fw-bold">First Name <span class="text-danger">*</span></label>
                                    <input :readonly="readonly" type="text" class="form-control" name="first_name" v-model="item.first_name"
                                        placeholder="Enter first name" />
                                    <div v-if="first_name_error" class="invalid-feedback">
                                        <div>{{ first_name_error }}</div>
                                    </div>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="form-group">
                                    <label class="fw-bold">Last Name <span class="text-danger">*</span></label>
                                    <input :readonly="readonly" type="text" class="form-control" name="last_name" v-model="item.last_name"
                                        placeholder="Enter last name" />
                                    <div v-if="last_name_error" class="invalid-feedback">
                                        <div>{{ last_name_error }}</div>
                                    </div>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="form-group">
                                    <label class="fw-bold">Phone Number</label>
                                    <input :readonly="readonly" type="text" class="form-control" name="phone" v-model="item.phone"
                                        placeholder="Enter phone" />
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="form-group">
                                    <label class="fw-bold">Email</label>
                                    <input :readonly="readonly" type="text" class="form-control" name="email" v-model="item.email"
                                        placeholder="Enter email" />
                                    <div v-if="email_error" class="invalid-feedback">
                                        <div>{{ email_error }}</div>
                                    </div>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="form-group">
                                <label class="fw-bold">Gender</label>
                                <div class="btn-group btn-group-toggle" data-toggle="buttons">
                                    <label class="btn">
                                        <input :disabled="creadonly" type="radio" name="gender" v-model="item.gender" id="Female" value="Female" checked>Female
                                    </label>
                                    <label class="btn">
                                        <input :disabled="readonly" type="radio" name="gender" v-model="item.gender" id="Male" value="Male">Male
                                    </label>
                                    <label class="btn">
                                        <input :disabled="readonly" type="radio" name="gender" v-model="item.gender" id="Order" value="Order">Order
                                    </label>
                                </div>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="form-group">
                                <label class="fw-bold">Quanlification</label>
                                    <input :readonly="readonly" type="text" class="form-control" name="quanlification" v-model="item.quanlification"
                                        placeholder="Enter quanlification" />
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
                                    <label class="fw-bold">Department</label>
                                    <select :disabled="readonly" class="form-select" name="department_id"
                                     v-model="item.department_id">
                                        <option v-for="item in departmentItems" :key="item.id" :value="item.id">
                                            {{item.name}}
                                        </option>
                                    </select>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="form-group">
                                    <label class="fw-bold">Job Specification</label>
                                    <textarea :readonly="readonly" class="form-control" name="job_specification"
                                     v-model="item.job_specification" placeholder="Enter job specification"/>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="col form-group mb-3 d-grid gap-2 d-md-flex">
                                    <button :disabled="readonly" class="btn btn-outline-primary">
                                        <span v-if="loading" class="spinner-border spinner-border-sm mr-1"></span>
                                        Save
                                    </button>
                                    <RouterLink class="btn btn-outline-secondary"
                                        to="/Doctor">Cancel</RouterLink>
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
                loading: false,
                readonly: false,
                title: "Add Doctor",
                first_name_error:"",
                last_name_error:"",
                email_error:"",
                message_error:"",
                hospitalItems:[],
                departmentItems:[],
                item:{
                    first_name:"",
                    last_name: "",
                    phone: "",
                    email:"",
                    gender: "",
                    quanlification:"",
                    job_specification:"",
                    hospital_id: null,
                    department_id: null,
                    id: 0,
                },
                apiUrl: `${enviroment.apiUrl}/Doctors`
            }
        },
        methods:{
            validFirstName(){
                if (!this.item.first_name)
                    this.first_name_error = "First Name is required";
                else
                    this.first_name_error = "";
            },
            validLastName(){
                if (!this.item.last_name)
                    this.last_name_error = "Last Name is required";
                else
                    this.last_name_error = "";
            },
            checkEmail(){
                if (this.item.email && !validEmail(this.item.email))
                    this.email_error = "email invalid";
                else
                    this.email_error = "";
            },
            async getItem(){
                return await getItemById(`${this.apiUrl}/${this.item.id}`);
            },
            async getHospitalItems(){
                return await getItems(`${enviroment.apiUrl}/Hospitals`);
            },
            async getDepartmentItems(){
                return await getItems(`${enviroment.apiUrl}/Departments`);
            },
            async save(){
                this.validFirstName();
                this.validLastName();
                this.checkEmail();
                if (!this.first_name_error &&
                    !this.last_name_error &&
                    !this.email_error
                ){
                    if (this.item.id == 0){
                        
                        await post(`${this.apiUrl}/Add`, this.item).then(response=>{
                            if (response.valid){
                                this.$router.push("/Doctor");
                            }
                            else
                                this.message_error = response.message;
                        });
                    }
                    else{
                        console.log(this.item);
                        await updateItem(`${this.apiUrl}/Edit/${this.item.id}`, this.item).then(response=>{
                            if (response.valid){
                                this.$router.push("/Doctor");
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
                this.title = "Edit Doctor";
                if (currentPath.indexOf("View") != -1){
                    this.title = "View Doctor";
                    this.readonly = true;
                }
                var data = await this.getItem();
                this.item = data.data;
            }

            var categories = await this.getDepartmentItems();
            this.departmentItems = categories.data;
            categories = await this.getHospitalItems();
            this.hospitalItems = categories.data;
        }
    }
</script>