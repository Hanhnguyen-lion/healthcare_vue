<script setup>
import { getItemById, getItems, post, updateItem } from '@/services/baseServices';
import FooterComponent from '../footer/FooterComponent.vue';
import { enviroment } from '@/enviroments/enviroment';
import { isSupperAdmin, validEmail } from '../helper/helper';
import { useAuthStore } from '@/store/auth.module';

</script>

<template>
    <div class="container">
        <div class="row justify-content-center">
            <div class="col-md-6 col-md-10">
                <div class="card">
                    <div class="card-header">
                        <h3>{{ title }}</h3>
                        <form name="form" @submit.prevent="save()">
                            <div class="row mb-3">
                                <div class="form-group">
                                    <label class="fw-bold">{{$t('commonText.firstName')}} <span class="text-danger">*</span></label>
                                    <input type="text" class="form-control" name="first_name" v-model="item.first_name"
                                        :placeholder="$t('commonText.enterFirstName')" />
                                    <div v-if="first_name_error" class="invalid-feedback">
                                        <div>{{ first_name_error }}</div>
                                    </div>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="form-group">
                                    <label class="fw-bold">{{$t('commonText.lastName')}} <span class="text-danger">*</span></label>
                                    <input type="text" class="form-control" name="last_name" v-model="item.last_name"
                                        :placeholder="$t('commonText.enterLastName')" />
                                    <div v-if="last_name_error" class="invalid-feedback">
                                        <div>{{ last_name_error }}</div>
                                    </div>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="form-group">
                                    <label class="fw-bold">{{$t('doctor.addDoctor.phone')}}</label>
                                    <input type="text" class="form-control" name="phone" v-model="item.phone"
                                        :placeholder="$t('doctor.addDoctor.enterPhoneNumber')" />
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="form-group">
                                    <label class="fw-bold">{{$t('commonText.email')}}</label>
                                    <input type="text" class="form-control" name="email" v-model="item.email"
                                        :placeholder="$t('commonText.enterEmail')" />
                                    <div v-if="email_error" class="invalid-feedback">
                                        <div>{{ email_error }}</div>
                                    </div>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="form-group">
                                <label class="fw-bold">{{$t('commonText.gender')}}</label>
                                <div class="form-control btn-group btn-group-toggle" data-toggle="buttons">
                                    <label class="btn">
                                        <input type="radio" name="gender" v-model="item.gender" id="Female" value="Female" checked>Female
                                    </label>
                                    <label class="btn">
                                        <input type="radio" name="gender" v-model="item.gender" id="Male" value="Male">Male
                                    </label>
                                    <label class="btn">
                                        <input type="radio" name="gender" v-model="item.gender" id="Other" value="Other">Other
                                    </label>
                                </div>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="form-group">
                                <label class="fw-bold">{{$t('doctor.addDoctor.quanlification')}}</label>
                                    <input type="text" class="form-control" name="quanlification" v-model="item.quanlification"
                                        :placeholder="$t('doctor.addDoctor.enterQuanlification')" />
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="form-group">
                                    <label class="fw-bold">{{$t('commonText.hospital')}}</label>
                                    <select class="form-select" name="hospital_id"
                                     v-model="item.hospital_id">
                                        <option v-for="item in hospitalItems" :key="item.id" :value="item.id">
                                            {{item.name}}
                                        </option>
                                    </select>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="form-group">
                                    <label class="fw-bold">{{$t('doctor.addDoctor.jobSpecification')}}</label>
                                    <textarea class="form-control" name="job_specification"
                                     v-model="item.job_specification" :placeholder="$t('doctor.addDoctor.enterJobSpecification')"/>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="col form-group mb-3 d-grid gap-2 d-md-flex">
                                    <button class="btn btn-outline-primary">
                                        <span v-if="loading" class="spinner-border spinner-border-sm mr-1"></span>
                                        {{$t('buttons.save')}}
                                    </button>
                                    <RouterLink class="btn btn-outline-secondary"
                                        to="/Doctor">{{$t('buttons.cancel')}}</RouterLink>
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
                edit_id: null,
                loading: false,
                title: this.$t("doctor.addDoctor.addDoctor"),
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
                    gender: "Female",
                    quanlification:"",
                    job_specification:"",
                    hospital_id: null,
                    id: null,
                    id_guid: null,
                    hospital_id_guid: null
                },
                apiUrl: `${enviroment.apiUrl}/Doctors`
            }
        },
        methods:{
            validFirstName(){
                if (!this.item.first_name)
                    this.first_name_error = this.$t("messages.firstNameRequired");
                else
                    this.first_name_error = "";
            },
            validLastName(){
                if (!this.item.last_name)
                    this.last_name_error = this.$t("messages.lastNameRequired");
                else
                    this.last_name_error = "";
            },
            checkEmail(){
                if (this.item.email && !validEmail(this.item.email))
                    this.email_error = this.$t("messages.emailInvalid");
                else
                    this.email_error = "";
            },
            async getItem(id){
                return await getItemById(`${this.apiUrl}/${id}`);
            },
            async getHospitalItems(){
                return await getItems(`${enviroment.apiUrl}/Hospitals`);
            },
            async save(){
                this.validFirstName();
                this.validLastName();
                this.checkEmail();
                if (!this.first_name_error &&
                    !this.last_name_error &&
                    !this.email_error
                ){
                    this.loading = true;
                    var updated;
                    if (enviroment.mongo_db){
                        this.item.hospital_id_guid = this.item.hospital_id;
                        this.item.hospital_id = null;
                    }
                    if (!this.edit_id)
                        updated = await post(`${this.apiUrl}/Add`, this.item);
                    else
                        updated = await updateItem(`${this.apiUrl}/Edit/${this.edit_id}`, this.item);
                    if (updated.valid){
                        this.$router.push("/Doctor");
                    }
                    else{
                        this.loading = false;
                        this.message_error = updated.message;
                    }
                }
            }
        },
        async mounted(){
            var id = this.$route.params["id"];
            if (id){
                this.edit_id = id;
                this.title = this.$t("doctor.addDoctor.editDoctor");
                var data = await this.getItem(id);
                this.item = data.data;

                this.item.gender = (this.item.gender) ? this.item.gender : "Female";
            }

            var categories = await this.getHospitalItems();
            this.hospitalItems = categories.data;
            if (!isSupperAdmin(this.auth.accountLogin)){
                var hospital_id = this.auth.accountLogin.hospital_id;
                this.hospitalItems = this.hospitalItems.filter(li => li.id == hospital_id);
            }
        }
    }
</script>