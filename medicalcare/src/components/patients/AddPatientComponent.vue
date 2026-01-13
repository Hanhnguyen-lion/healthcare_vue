<template>
<div class="container">
    <div class="row justify-content-center">
        <div class="col-md-6 col-md-12">
            <div class="card">
                <div class="card-header">
                    <h3>{{this.constant_item.title}}</h3>
                    <form name="form" @submit.prevent="handleAdd">
                        <div class="row mb-3">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">{{$t('patient.patients.code')}} <span class="text-danger">*</span></label>
                                    <input type="text" class="form-control"
                                        name="code" id="code" v-model="item.code" 
                                         :placeholder="$t('patient.addPatient.enterCode')">
                                        <div v-if="item_error.codeError" class="invalid-feedback">
                                            <div>{{item_error.codeError}}</div>
                                        </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">{{$t('patient.patients.dob')}}</label>
                                    <input  type="date" class="form-control" 
                                        name="date_of_birth" v-model="item.date_of_birth"
                                        :placeholder="$t('patient.addPatient.enterDOB')">
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">{{$t('commonText.firstName')}} <span class="text-danger">*</span></label>
                                    <input  type="text" class="form-control"
                                        name="first_name" v-model="item.first_name" 
                                        :placeholder="$t('commonText.enterFirstName')">
                                    <div v-if="item_error.firstNameError" class="invalid-feedback">
                                        <div>{{item_error.firstNameError}}</div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">{{$t('commonText.lastName')}} <span class="text-danger">*</span></label>
                                    <input  type="text" class="form-control"
                                        name="last_name" v-model="item.last_name" 
                                        :placeholder="$t('commonText.enterLastName')">
                                    <div v-if="item_error.lastNameError" class="invalid-feedback">
                                        <div>{{item_error.lastNameError}}</div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">{{$t('commonText.email')}}</label>
                                    <input  type="text" class="form-control" 
                                        name="email" v-model="item.email"
                                        :placeholder="$t('commonText.enterEmail')">
                                    <div v-if="item_error.emailError" class="invalid-feedback">
                                        <div>{{item_error.emailError}}</div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
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
                                        <input type="radio" name="gender" v-model="item.gender" id="Order" value="Order">Order
                                    </label>
                                </div>
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">{{$t('patient.addPatient.phone')}}</label>
                                    <input  type="text" class="form-control" 
                                        name="phone_number" v-model="item.phone_number" 
                                        :placeholder="$t('patient.addPatient.enterPhoneNumber')">
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">{{$t('patient.addPatient.job')}}</label>
                                    <input  type="text" class="form-control" 
                                        name="job" v-model="item.job" :placeholder="$t('patient.addPatient.enterJob')">
                                </div>
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
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label class="fw-bold">{{$t('patient.addPatient.homeAddress')}}</label>
                                    <input  type="text" class="form-control" 
                                        name="home_address" v-model="item.home_address" :placeholder="$t('patient.addPatient.enterHomeAddress')">
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label class="fw-bold">{{$t('patient.addPatient.officeAddress')}}</label>
                                    <input  type="text" class="form-control" 
                                        name="office_address" v-model="item.office_address" :placeholder="$t('patient.addPatient.enterOfficeAddress')">
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">{{$t('patient.addPatient.emergencyContactName')}}</label>
                                    <input  type="text" class="form-control" 
                                        name="emergency_contact_name" v-model="item.emergency_contact_name" :placeholder="$t('patient.addPatient.enterEmergencyContactName')">
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">{{$t('patient.addPatient.emergencyContactPhone')}}</label>
                                    <input  type="text" class="form-control" 
                                        name="emergency_contact_phone" v-model="item.emergency_contact_phone" :placeholder="$t('patient.addPatient.enterEmergencyContactPhone')">
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">{{$t('patient.addPatient.insuranceNumber')}}</label>
                                    <input  type="text" class="form-control" 
                                        name="insurance_policy_number" v-model="item.insurance_policy_number" :placeholder="$t('patient.addPatient.enterInsuranceNumber')">
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">{{$t('patient.addPatient.insuranceExpire')}}</label>
                                    <input type="date" class="form-control" name="insurance_expire" 
                                        v-model="item.insurance_expire"   
                                         :placeholder="$t('patient.addPatient.enterInsuranceExpire')">
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">{{$t('patient.addPatient.insuranceProvider')}}</label>
                                    <input  type="text" class="form-control" name="insurance_provider" 
                                       v-model="item.insurance_provider" :placeholder="$t('patient.addPatient.enterInsuranceProvider')">
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">{{$t('patient.addPatient.insuranceType')}}</label>
                                    <input  type="text" class="form-control" 
                                        name="insurance_type" v-model="item.insurance_type" :placeholder="$t('patient.addPatient.enterInsuranceType')">
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label class="fw-bold">{{$t('patient.addPatient.insuranceInfo')}}</label>
                                    <textarea  rows="3" class="form-control" 
                                        name="insurance_info" v-model="item.insurance_info" :placeholder="$t('patient.addPatient.enterInsuranceInfo')"></textarea>
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label class="fw-bold">{{$t('patient.addPatient.medicalHistory')}}</label>
                                    <textarea rows="3" class="form-control" 
                                        name="medical_history" v-model="item.medical_history" :placeholder="$t('patient.addPatient.enterMedicalHistory')"></textarea>
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col form-group mb-3 d-grid gap-2 d-md-flex">
                                <button type="submit" class="btn btn-outline-primary">
                                    <span v-if="constant_item.loading" class="spinner-border spinner-border-sm mr-1"></span>
                                    {{$t('buttons.save')}}
                                </button>

                                <router-link to="/Patient" class="btn btn-outline-secondary">{{$t('buttons.cancel')}}</router-link>
                            </div>
                        </div>
                        <div v-if="item_error.error" class="row mb-3 invalid-feedback fw-bold">
                            <div>{{item_error.error}}</div>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</div>
</template>

<script>

    import { enviroment } from '@/enviroments/enviroment';
    import { post, getItemById, updateItem, getItems } from '@/services/baseServices';
    import { formatDateToString, formatDateYYYYMMDD, isSupperAdmin } from '../helper/helper';
    import { useAuthStore } from '@/store/auth.module';

export default{

    data(){
        return {
            auth: useAuthStore(),
            hospitalItems:[],
            constant_item:{
                title : this.$t("patient.addPatient.addPatient"),
                loading : false,
                id: null,
                url: `${enviroment.apiUrl}/Patients`
            },
            item:{
                code: "",
                first_name: "",
                last_name: "",
                date_of_birth: formatDateToString(new Date(), "YYYY-MM-DD"),
                email: "",
                gender: "Female",
                phone_number: "",
                job: "",
                home_address: "",
                office_address: "",
                emergency_contact_name: "",
                emergency_contact_phone: "",
                insurance_policy_number: "",
                insurance_expire: null,
                insurance_provider: "",
                insurance_type: "",
                insurance_info: "",
                medical_history: "",
                hospital_id: null,
                id: null,
                hospital_id_guid: null,
                id_guid: null
            },
            item_error: {
                codeError : "",
                firstNameError: "",
                lastNameError: "",
                emailError: "",
                error:""
            }
        };
    },
    methods:{
        validFirstName(){
            if (!this.item.first_name)
                this.item_error.firstNameError = this.$t("messages.firstNameRequired");
            else{
                this.item_error.firstNameError = "";
            }
        },
        validCode(){
            if (!this.item.code)
                this.item_error.codeError = this.$t("messages.codeRequired");
            else{
                this.item_error.codeError = "";
            }
        },
        validLastName(){
            if (!this.item.last_name)
                this.item_error.lastNameError = this.$t("messages.lastNameRequired");
            else{
                this.item_error.lastNameError = "";
            }
        },
        validEmail(){
            const regex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
            if (this.item.email){
                var isValidEmail = regex.test(this.item.email);
                if (!isValidEmail){
                    this.item_error.emailError = this.$t("messages.emailInvalid");
                }
                else{
                    this.item_error.emailError = "";
                }
            }
            else{
                this.item_error.emailError = "";
            }
        },
        async getItem(id){
            return await getItemById(`${this.constant_item.url}/${id}`);
        },
        async getHospitalItems(){
            return await getItems(`${enviroment.apiUrl}/Hospitals`);
        },
        async handleAdd(){
            this.validCode();
            this.validFirstName();
            this.validLastName();
            this.validEmail();

            if (!this.item_error.codeError &&
                !this.item_error.firstNameError &&
                !this.item_error.lastNameError &&
                !this.item_error.emailError
            ){
                this.loading = true;
                var updated;
                if (enviroment.mongo_db){
                    this.item.hospital_id_guid = this.item.hospital_id;
                    this.item.hospital_id = null;
                }
                var url = `${this.constant_item.url}`;
                this.item.date_of_birth = (this.item.date_of_birth) ? this.item.date_of_birth : null;

                this.item.insurance_expire = (this.item.insurance_expire) ? this.item.insurance_expire : null;
                if (this.constant_item.id){
                    url = `${url}/Edit/${this.constant_item.id}`;
                    updated = await updateItem(url, this.item);
                }
                else{
                    url = `${url}/Add`;
                    updated = await post(url, this.item);
                }
                if (updated.valid){
                    this.$router.push("/Patient");
                }
                else{
                    this.loading = false;
                    this.item_error.error = updated.message;
                }
            }
        }
    },
    async mounted(){
        var id = this.$route.params.id;
        if (id) {
            this.constant_item.title = this.$t("patient.addPatient.editPatient");
            this.constant_item.id = id;
            var data = await this.getItem(id);
            this.item = data.data;
            var date_of_birth = this.item.date_of_birth;
            var insurance_expire = this.item.insurance_expire;
            this.item.date_of_birth = (date_of_birth) ? formatDateYYYYMMDD(date_of_birth):null;
            this.item.insurance_expire = (insurance_expire) ? formatDateYYYYMMDD(insurance_expire):null;
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
