<template>
<div class="container">
    <div class="row justify-content-center">
        <div class="col-md-6 col-md-6">
            <div class="card">
                <div class="card-header">
                    <h3>{{this.constant_item.title}}</h3>
                    <form name="form" @submit.prevent="handleAdd">
                        <div class="row mb-3">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">Code <span class="text-danger">*</span></label>
                                    <input type="text" class="form-control"
                                        name="code" id="code" v-model="item.code" 
                                        placeholder="Enter code">
                                        <div v-if="item_error.codeError" class="invalid-feedback">
                                            <div>{{item_error.codeError}}</div>
                                        </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">Date of birth</label>
                                    <input  type="date" class="form-control" 
                                        name="date_of_birth" v-model="item.date_of_birth"
                                        placeholder="Enter date of birth">
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">First Name <span class="text-danger">*</span></label>
                                    <input  type="text" class="form-control"
                                        name="first_name" v-model="item.first_name" 
                                        placeholder="Enter first name">
                                    <div v-if="item_error.firstNameError" class="invalid-feedback">
                                        <div>{{item_error.firstNameError}}</div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">Last Name <span class="text-danger">*</span></label>
                                    <input  type="text" class="form-control"
                                        name="last_name" v-model="item.last_name" 
                                        placeholder="Enter last name">
                                    <div v-if="item_error.lastNameError" class="invalid-feedback">
                                        <div>{{item_error.lastNameError}}</div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">Email</label>
                                    <input  type="text" class="form-control" 
                                        name="email" v-model="item.email"
                                        placeholder="Enter email">
                                    <div v-if="item_error.emailError" class="invalid-feedback">
                                        <div>{{item_error.emailError}}</div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                <label class="fw-bold">Gender</label>
                                <div class="btn-group btn-group-toggle" data-toggle="buttons">
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
                                    <label class="fw-bold">Phone Number</label>
                                    <input  type="text" class="form-control" 
                                        name="phone_number" v-model="item.phone_number" 
                                        placeholder="Enter phone number">
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">Job</label>
                                    <input  type="text" class="form-control" 
                                        name="job" v-model="item.job" placeholder="Enter job">
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="form-group">
                                <label class="fw-bold">Hospital</label>
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
                                    <label class="fw-bold">Home Address</label>
                                    <input  type="text" class="form-control" 
                                        name="home_address" v-model="item.home_address" placeholder="Enter home address">
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label class="fw-bold">Office Address</label>
                                    <input  type="text" class="form-control" 
                                        name="office_address" v-model="item.office_address" placeholder="Enter office address">
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">Emergency Contact Name</label>
                                    <input  type="text" class="form-control" 
                                        name="emergency_contact_name" v-model="item.emergency_contact_name" placeholder="Enter emergency contact name">
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">Emergency Contact Phone</label>
                                    <input  type="text" class="form-control" 
                                        name="emergency_contact_phone" v-model="item.emergency_contact_phone" placeholder="Enter emergency contact phone">
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">Insurance Number</label>
                                    <input  type="text" class="form-control" 
                                        name="insurance_policy_number" v-model="item.insurance_policy_number" placeholder="Enter insurance number">
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">Insurance Expire</label>
                                    <input type="date" class="form-control" name="insurance_expire" 
                                        v-model="item.insurance_expire"   
                                         placeholder="Enter insurance expire">
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">Insurance Provider</label>
                                    <input  type="text" class="form-control" name="insurance_provider" 
                                       v-model="item.insurance_provider" placeholder="Enter Insurance Provider">
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">Insurance Type</label>
                                    <input  type="text" class="form-control" 
                                        name="insurance_type" v-model="item.insurance_type" placeholder="Enter Insurance Type">
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label class="fw-bold">Insurance Info</label>
                                    <textarea  rows="3" class="form-control" 
                                        name="insurance_info" v-model="item.insurance_info" placeholder="Enter insurance info"></textarea>
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label class="fw-bold">Medical History</label>
                                    <textarea rows="3" class="form-control" 
                                        name="medical_history" v-model="item.medical_history" placeholder="Enter medical history"></textarea>
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col form-group mb-3 d-grid gap-2 d-md-flex">
                                <button type="submit" class="btn btn-outline-primary">
                                    <span v-if="constant_item.loading" class="spinner-border spinner-border-sm mr-1"></span>
                                    Save
                                </button>

                                <router-link to="/Patient" class="btn btn-outline-secondary">Cancel</router-link>
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
                title : "Add Patient",
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
                this.item_error.firstNameError = "First name is required";
            else{
                this.item_error.firstNameError = "";
            }
        },
        validCode(){
            if (!this.item.code)
                this.item_error.codeError = "Code is required";
            else{
                this.item_error.codeError = "";
            }
        },
        validLastName(){
            if (!this.item.last_name)
                this.item_error.lastNameError = "Last name is required";
            else{
                this.item_error.lastNameError = "";
            }
        },
        validEmail(){
            const regex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
            if (this.item.email){
                var isValidEmail = regex.test(this.item.email);
                if (!isValidEmail){
                    this.item_error.emailError = "Email must be a valid email address";
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
                if (enviroment.mongo_db){
                    this.item.hospital_id_guid = this.item.hospital_id;
                    this.item.hospital_id = null;
                }
                var url = `${this.constant_item.url}`;
                this.item.date_of_birth = (this.item.date_of_birth) ? this.item.date_of_birth : null;

                this.item.insurance_expire = (this.item.insurance_expire) ? this.item.insurance_expire : null;
                if (this.constant_item.id){
                    url = `${url}/Edit/${this.constant_item.id}`;
                    if (enviroment.mongo_db){
                        this.item.id_guid = this.constant_item.id;
                    }
                    else{
                        this.item.id = this.constant_item.id;
                    }
                    await updateItem(url, this.item)
                    .then((response)=>{
                        if (response.valid){
                            this.$router.push("/Patient");
                        }
                        else{
                            this.item_error.error = response.message;
                        }
                    });
                }
                else{
                    url = `${url}/Add`;
                    await post(url, this.item).then(response=>{
                        if (response.valid){
                            this.$router.push("/Patient");
                        }
                        else{
                            this.item_error.error = response.message;
                        }
                    });
                }
            }
        }
    },
    async mounted(){
        var id = this.$route.params.id;
        if (id) {
            this.constant_item.title = "Edit Patient";
            this.constant_item.id = id;
            var data = await this.getItem(id);
            this.item = data.data;
            var date_of_birth = this.item.date_of_birth;
            var insurance_expire = this.item.insurance_expire;
            this.item.date_of_birth = (date_of_birth) ? formatDateYYYYMMDD(date_of_birth):null;
            this.item.insurance_expire = (insurance_expire) ? formatDateYYYYMMDD(insurance_expire):null;
            this.item.hospital_id = (enviroment.mongo_db) ? this.item.hospital_id_guid : this.item.hospital_id;
        }
        var categories = await this.getHospitalItems();
        var hospitals = categories.data;
        if (!isSupperAdmin(this.auth.accountLogin)){
            if (enviroment.mongo_db){
                var hospital_id_guid = this.auth.accountLogin.hospital_id_guid || "";
                hospitals = hospitals.filter(li => li.id_guid == hospital_id_guid);
            }
            else{
                var hospital_id = this.auth.accountLogin.hospital_id || 0;
                hospitals = hospitals.filter(li => li.id == hospital_id);
            }
        }
        for(var i = 0; i < hospitals.length; i++){
            this.hospitalItems.push(
                {
                    id: (enviroment.mongo_db) ? hospitals[i].hospital_id_guid : hospitals[i].id,
                    name: hospitals[i].name
                }
            );
        }
    }
}    
</script>
