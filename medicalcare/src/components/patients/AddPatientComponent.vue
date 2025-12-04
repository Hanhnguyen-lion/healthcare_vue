<template>
<div class="container">
    <div class="row justify-content-center">
        <div class="col-md-6 col-md-6">
            <div class="card">
                <div class="card-header">
                    <h3>{{title}}</h3>
                    <form name="form">
                        <div class="row mb-3">
                            <div class="col-md-12">
                                <div class="invalid-feedback">
                                    <div v-for="error in errors" :key="error">
                                        {{ error }}
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-6">
                                <div class="form-group">
                                <label class="fw-bold">Code <span class="text-danger">*</span></label>
                                <input :readonly="readonly" type="text" class="form-control"
                                    name="code" id="code" v-model="code" placeholder="Enter code">
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">First Name <span class="text-danger">*</span></label>
                                    <input :readonly="readonly" type="text" class="form-control"
                                        name="first_name" v-model="first_name" placeholder="Enter first name">
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">Last Name <span class="text-danger">*</span></label>
                                    <input :readonly="readonly" type="text" class="form-control"
                                        name="last_name" v-model="last_name" 
                                        placeholder="Enter last name">
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">Date of birth</label>
                                    <input :readonly="readonly" type="date" class="form-control" 
                                        v-model="date_of_birth" name="date_of_birth" 
                                        placeholder="Enter date of birth">
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">Email</label>
                                    <input :readonly="readonly" type="text" class="form-control" 
                                        name="email" v-model="email" 
                                        placeholder="Enter email">
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                <label class="fw-bold">Gender</label>
                                <div class="btn-group btn-group-toggle" data-toggle="buttons">
                                    <label class="btn">
                                        <input type="radio" name="gender" v-model="gender" id="Female" value="Female" checked>Female
                                    </label>
                                    <label class="btn">
                                        <input type="radio" name="gender" v-model="gender" id="Male" value="Male">Male
                                    </label>
                                    <label class="btn">
                                        <input type="radio" name="gender" v-model="gender" id="Order" value="Order">Order
                                    </label>
                                </div>
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">Phone Number</label>
                                    <input :readonly="readonly" type="text" class="form-control" 
                                        name="phone_number" v-model="phone_number" 
                                        placeholder="Enter phone number">
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">Job</label>
                                    <input :readonly="readonly" type="text" class="form-control" 
                                        name="job" v-model="job" placeholder="Enter job">
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label class="fw-bold">Home Address</label>
                                    <input :readonly="readonly" type="text" class="form-control" 
                                        name="home_address" v-model="home_address" placeholder="Enter home address">
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label class="fw-bold">Office Address</label>
                                    <input :readonly="readonly" type="text" class="form-control" 
                                        name="office_address" v-model="office_address" placeholder="Enter office address">
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">Emergency Contact Name</label>
                                    <input :readonly="readonly" type="text" class="form-control" 
                                        name="emergency_contact_name" v-model="emergency_contact_name" placeholder="Enter emergency contact name">
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">Emergency Contact Phone</label>
                                    <input :readonly="readonly" type="text" class="form-control" 
                                        name="emergency_contact_phone" v-model="emergency_contact_phone" placeholder="Enter emergency contact phone">
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">Insurance Number</label>
                                    <input :readonly="readonly" type="text" class="form-control" 
                                        name="insurance_policy_number" v-model="insurance_policy_number" placeholder="Enter insurance number">
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">Insurance Expire</label>
                                    <input :readonly="readonly" type="date" class="form-control" name="insurance_expire" 
                                       v-model="insurance_expire" placeholder="Enter insurance expire">
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">Insurance Provider</label>
                                    <input :readonly="readonly" type="text" class="form-control" name="insurance_provider" 
                                       v-model="insurance_provider" placeholder="Enter Insurance Provider">
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">Insurance Type</label>
                                    <input :readonly="readonly" type="text" class="form-control" 
                                        name="insurance_type" v-model="insurance_type" placeholder="Enter Insurance Type">
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label class="fw-bold">Insurance Info</label>
                                    <textarea :readonly="readonly" rows="3" class="form-control" 
                                        name="insurance_info" v-model="insurance_info" placeholder="Enter insurance info"></textarea>
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label class="fw-bold">Medical History</label>
                                    <textarea :readonly="readonly" rows="3" class="form-control" 
                                        name="medical_history" v-model="medical_history" placeholder="Enter medical history"></textarea>
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col form-group mb-3 d-grid gap-2 d-md-flex">
                                <button :disabled="readonly" type="button" @click="add()" class="btn btn-outline-primary">
                                    <span v-if="loading" class="spinner-border spinner-border-sm mr-1"></span>
                                    Save
                                </button>

                                <button class="btn btn-outline-secondary" type="button" @click="onCancel()">Cancel</button>
                            </div>
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
import { addItem, getItemById, updateItem } from '@/services/baseServices';
import { formatDateToString } from '../helper/helper';

    export default{
        data: ()=>{
            return{
                title:"Add New Patient",
                readonly: false,
                isView: false,
                id: 0,
                url: `${enviroment.apiUrl}/Patients`,
                errors: [],
                loading: false,
                code: "",
                first_name: "",
                last_name: "",
                date_of_birth: new Date(),
                email: "",
                gender: "",
                phone_number: "",
                job: "",
                home_address: "",
                office_address: "",
                emergency_contact_name: "",
                emergency_contact_phone: "",
                insurance_policy_number: "",
                insurance_expire: new Date(),
                insurance_provider: "",
                insurance_type: "",
                insurance_info: "",
                medical_history: ""
            };
        },
        mounted(){
            this.id = +this.$route.params.id;
            var currentPath = this.$route.path;
            if (this.id > 0) {
                this.title = "Edit Patient";
                if (currentPath.indexOf("View") != -1){
                    this.title = "View Patient";
                    this.readonly = true;
                }
                getItemById(this.url, this.id)
                .then(item =>{
                    this.code = item.code;
                    this.first_name = item.first_name;
                    this.last_name = item.last_name;
                    this.email = item.email;
                    this.gender = item.gender;
                    this.date_of_birth = formatDateToString(item.date_of_birth, "YYYY-MM-DD");
                    this.insurance_expire = formatDateToString(item.insurance_expire, "YYYY-MM-DD");
                    this.insurance_info = item.insurance_info;
                    this.insurance_policy_number = item.insurance_policy_number;
                    this.insurance_provider = item.insurance_provider;
                    this.insurance_type = item.insurance_type;
                    this.medical_history = item.medical_history;
                    this.job = item.job;
                    this.phone_number = item.phone_number;
                    this.home_address = item.home_address;
                    this.office_address = item.office_address;
                    this.emergency_contact_name = item.emergency_contact_name;
                    this.emergency_contact_phone = item.emergency_contact_phone;
                });
            }
        },
        methods:{
            add(){
                this.errors = [];

                if (!this.code){
                    this.errors.push("Code is required");
                }
                if (!this.first_name){
                    this.errors.push("First name is required");
                }
                if (!this.last_name){
                    this.errors.push("Last name is required");
                }
                if (!this.errors.length){
                    var patientItem = {
                        "code": this.code,
                        "first_name": this.first_name,
                        "last_name": this.last_name,
                        "date_of_birth": this.date_of_birth,
                        "email": this.email,
                        "gender": this.gender,
                        "phone_number": this.phone_number,
                        "job": this.job,
                        "home_address": this.home_address,
                        "office_address": this.office_address,
                        "emergency_contact_name": this.emergency_contact_name,
                        "emergency_contact_phone": this.emergency_contact_phone,
                        "insurance_policy_number": this.insurance_policy_number,
                        "insurance_expire": this.insurance_expire,
                        "insurance_provider": this.insurance_provider,
                        "insurance_type": this.insurance_provider,
                        "insurance_info": this.insurance_provider,
                        "medical_history": this.medical_history
                    };
                    if (this.id > 0){
                        updateItem(this.url, patientItem, this.id);
                    }
                    else{
                        addItem(this.url, patientItem);
                    }
                    this.$router.push("/Patient");
                }
            },
            onCancel(){
                this.$router.push("/Patient");
            }
        }
    }
</script>
