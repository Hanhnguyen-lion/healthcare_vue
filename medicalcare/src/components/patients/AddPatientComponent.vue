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
                                    <input :readonly="constant_item.readonly" type="text" class="form-control"
                                        name="code" id="code" v-model="item.code" 
                                        placeholder="Enter code">
                                        <div v-if="item_error.codeError" class="invalid-feedback">
                                            <div>{{item_error.codeError}}</div>
                                        </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">First Name <span class="text-danger">*</span></label>
                                    <input :readonly="constant_item.readonly" type="text" class="form-control"
                                        name="first_name" v-model="item.first_name" 
                                        placeholder="Enter first name">
                                    <div v-if="item_error.firstNameError" class="invalid-feedback">
                                        <div>{{item_error.firstNameError}}</div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">Last Name <span class="text-danger">*</span></label>
                                    <input :readonly="constant_item.readonly" type="text" class="form-control"
                                        name="last_name" v-model="item.last_name" 
                                        placeholder="Enter last name">
                                    <div v-if="item_error.lastNameError" class="invalid-feedback">
                                        <div>{{item_error.lastNameError}}</div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">Date of birth</label>
                                    <input :readonly="constant_item.readonly" type="date" class="form-control" 
                                        v-model="item.date_of_birth" name="date_of_birth" 
                                        placeholder="Enter date of birth">
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">Email</label>
                                    <input :readonly="constant_item.readonly" type="text" class="form-control" 
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
                                        <input :disabled="constant_item.readonly" type="radio" name="gender" v-model="item.gender" id="Female" value="Female" checked>Female
                                    </label>
                                    <label class="btn">
                                        <input :disabled="constant_item.readonly" type="radio" name="gender" v-model="item.gender" id="Male" value="Male">Male
                                    </label>
                                    <label class="btn">
                                        <input :disabled="constant_item.readonly" type="radio" name="gender" v-model="item.gender" id="Order" value="Order">Order
                                    </label>
                                </div>
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">Phone Number</label>
                                    <input :readonly="constant_item.readonly" type="text" class="form-control" 
                                        name="phone_number" v-model="item.phone_number" 
                                        placeholder="Enter phone number">
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">Job</label>
                                    <input :readonly="constant_item.readonly" type="text" class="form-control" 
                                        name="job" v-model="item.job" placeholder="Enter job">
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label class="fw-bold">Home Address</label>
                                    <input :readonly="constant_item.readonly" type="text" class="form-control" 
                                        name="home_address" v-model="item.home_address" placeholder="Enter home address">
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label class="fw-bold">Office Address</label>
                                    <input :readonly="constant_item.readonly" type="text" class="form-control" 
                                        name="office_address" v-model="item.office_address" placeholder="Enter office address">
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">Emergency Contact Name</label>
                                    <input :readonly="constant_item.readonly" type="text" class="form-control" 
                                        name="emergency_contact_name" v-model="item.emergency_contact_name" placeholder="Enter emergency contact name">
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">Emergency Contact Phone</label>
                                    <input :readonly="constant_item.readonly" type="text" class="form-control" 
                                        name="emergency_contact_phone" v-model="item.emergency_contact_phone" placeholder="Enter emergency contact phone">
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">Insurance Number</label>
                                    <input :readonly="constant_item.readonly" type="text" class="form-control" 
                                        name="insurance_policy_number" v-model="item.insurance_policy_number" placeholder="Enter insurance number">
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">Insurance Expire</label>
                                    <input type="date" class="form-control" name="insurance_expire" 
                                        v-model="item.insurance_expire"   
                                        :readonly="constant_item.readonly" placeholder="Enter insurance expire">
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">Insurance Provider</label>
                                    <input :readonly="constant_item.readonly" type="text" class="form-control" name="insurance_provider" 
                                       v-model="item.insurance_provider" placeholder="Enter Insurance Provider">
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">Insurance Type</label>
                                    <input :readonly="constant_item.readonly" type="text" class="form-control" 
                                        name="insurance_type" v-model="item.insurance_type" placeholder="Enter Insurance Type">
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label class="fw-bold">Insurance Info</label>
                                    <textarea :readonly="constant_item.readonly" rows="3" class="form-control" 
                                        name="insurance_info" v-model="item.insurance_info" placeholder="Enter insurance info"></textarea>
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label class="fw-bold">Medical History</label>
                                    <textarea :readonly="constant_item.readonly" rows="3" class="form-control" 
                                        name="medical_history" v-model="item.medical_history" placeholder="Enter medical history"></textarea>
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col form-group mb-3 d-grid gap-2 d-md-flex">
                                <button type="submit" :disabled="constant_item.readonly" class="btn btn-outline-primary">
                                    <span v-if="constant_item.loading" class="spinner-border spinner-border-sm mr-1"></span>
                                    Save
                                </button>

                                <router-link to="/Patient" class="btn btn-outline-secondary">Cancel</router-link>
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

    data(){
        return {
            constant_item:{
                title : "Add Patient",
                loading : false,
                readonly : false,
                isView: false,
                 id: 0,
                 url: `${enviroment.apiUrl}/Patients`
            },
            item:{
                code: "",
                first_name: "",
                last_name: "",
                date_of_birth: "",
                email: "",
                gender: "",
                phone_number: "",
                job: "",
                home_address: "",
                office_address: "",
                emergency_contact_name: "",
                emergency_contact_phone: "",
                insurance_policy_number: "",
                insurance_expire: "",
                insurance_provider: "",
                insurance_type: "",
                insurance_info: "",
                medical_history: ""
            },
            item_error: {
                codeError : "",
                firstNameError: "",
                lastNameError: "",
                emailError: ""
            }
        };
    },
    mounted(){
        this.constant_item.id = +this.$route.params.id;
        var currentPath = this.$route.path;
        if (this.constant_item.id > 0) {
            this.constant_item.title = "Edit Patient";
            if (currentPath.indexOf("View") != -1){
                this.constant_item.title = "View Patient";
                this.constant_item.readonly = true;
            }
            getItemById(this.constant_item.url, this.constant_item.id)
            .then(item =>{

                this.item = {
                    code: item.code,
                    first_name: item.first_name,
                    last_name: item.last_name,
                    email: item.email,
                    gender: item.gender,
                    date_of_birth: formatDateToString(item.date_of_birth, "YYYY-MM-DD"),
                    insurance_expire: formatDateToString(item.insurance_expire, "YYYY-MM-DD"),
                    insurance_info: item.insurance_info,
                    insurance_policy_number: item.insurance_policy_number,
                    insurance_provider: item.insurance_provider,
                    insurance_type: item.insurance_type,
                    medical_history: item.medical_history,
                    job: item.job,
                    phone_number: item.phone_number,
                    home_address: item.home_address,
                    office_address: item.office_address,
                    emergency_contact_name: item.emergency_contact_name,
                    emergency_contact_phone: item.emergency_contact_phone
                }
            });
        }
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
        handleAdd(){
            this.validCode();
            this.validFirstName();
            this.validLastName();
            this.validEmail();

            if (!this.item_error.codeError &&
                !this.item_error.firstNameError &&
                !this.item_error.lastNameError &&
                !this.item_error.emailError
            ){
                if (this.constant_item.id > 0){
                    this.item.id = this.constant_item.id;
                    updateItem(this.constant_item.url, this.item, this.constant_item.id);
                }
                else{
                    addItem(this.constant_item.url, this.item);
                }
                this.$router.push("/Patient");
            }
        }
    }
}    
</script>
