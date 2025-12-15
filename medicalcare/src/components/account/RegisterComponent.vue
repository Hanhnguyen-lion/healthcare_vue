<template>
    <div class="image">
        <div class="container-fluid">
            <div class="row justify-content-center">
                <div class="col-md-6 col-lg-5">
                    <div class="card">
                        <h3 class="card-header">Register</h3>
                        <div class="card-body">
                            <form name="form" @submit.prevent="onSubmit()">
                                <div class="form-row">
                                    <div class="form-group mb-3">
                                        <label>First Name <span class="text-danger">*</span></label>
                                        <input type="text" name="first_name" v-model="accountItem.first_name" placeholder="Enter first name" class="form-control"/>
                                        <div v-if="error.first_name_error" class="invalid-feedback">
                                            <div>{{error.first_name_error}}</div>
                                        </div>
                                    </div>
                                    <div class="form-group mb-3">
                                        <label>Last Name <span class="text-danger">*</span></label>
                                        <input type="text" name="last_name" v-model="accountItem.last_name" placeholder="Enter last name" class="form-control"/>
                                            <div v-if="error.last_name_error" class="invalid-feedback">
                                                <div>{{error.last_name_error}}</div>
                                            </div>
                                    </div>
                                </div>
                                <div class="form-group mb-3">
                                    <label>Email <span class="text-danger">*</span></label>
                                    <input type="text" name="email" v-model="accountItem.email" placeholder="Enter email" class="form-control"/>
                                        <div v-if="error.email_error" class="invalid-feedback">
                                            <div>{{error.email_error}}</div>
                                        </div>
                                </div>
                                <div class="form-row">
                                    <div class="form-group mb-3">
                                        <label>Password <span class="text-danger">*</span></label>
                                        <input type="password" name="password" v-model="accountItem.password" placeholder="Enter password" class="form-control"/>
                                            <div v-if="error.password_error" class="invalid-feedback">
                                                <div>{{error.password_error}}</div>
                                            </div>
                                    </div>
                                    <div class="form-group mb-3">
                                        <label>Confirm Password <span class="text-danger">*</span></label>
                                        <input type="password" name="confirmPassword" v-model="confirmPassword" placeholder="Enter confirm password" class="form-control"/>
                                            <div v-if="error.confirmPassword_error" class="invalid-feedback">
                                                <div>{{error.confirmPassword_error}}</div>
                                            </div>
                                    </div>
                                </div>
                                <div class="form-group mb-3">
                                    <label>Account Type <span class="text-danger">*</span></label>
                                    <select name="account_type" v-model="accountItem.account_type" class="form-select" placeholder="Enter acount type">
                                        <option value=""></option>
                                        <option value="Admin">Admin</option>
                                        <option value="Doctor">Doctor</option>
                                    </select>
                                    <div v-if="error.account_type_error" class="invalid-feedback">
                                        <div>{{error.account_type_error}}</div>
                                    </div>
                                </div>
                                <div class="form-group mb-3">
                                    <label>Hospital <span class="text-danger">*</span></label>
                                    <select name="hospital_id" v-model="accountItem.hospital_id" class="form-select">
                                        <option v-for="item in hospitalItems" :key="item.id" :value="item.id">
                                            {{ item.name }}
                                        </option>
                                    </select>
                                    <div v-if="error.hospital_error" class="invalid-feedback">
                                        <div>{{error.hospital_error}}</div>
                                    </div>
                                </div>
                                <div class="form-group mb-3">
                                    <label>Date of birth <span class="text-danger">*</span></label>
                                    <input type="date" name="dob" v-model="accountItem.dob" placeholder="Enter date of borth" class="form-control"/>
                                        <div v-if="error.dob_error" class="invalid-feedback">
                                            <div>{{error.dob_error}}</div>
                                        </div>
                                </div>
                                <div class="form-group mb-3 d-grid gap-2 d-md-flex">
                                    <button :disabled="loading" class="btn btn-outline-primary">
                                        <span v-if="loading" class="spinner-border spinner-border-sm mr-1"></span>
                                        Register
                                    </button>
                                </div>
                                <div class="form-group mb-3">
                                    <RouterLink to="/Account/Login" class="btn btn-outline-secondary">Already have an account? Login</RouterLink>
                                </div>
                                <div v-if="error.message_error" class="form-group mb-3">
                                    {{ error.message_error }}
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </div>    
</div>
<RouterView></RouterView>
</template>
<script>
import { enviroment } from '@/enviroments/enviroment';
import { formatDateYYYYMMDD, validEmail } from '../helper/helper';
import { getItems, post } from '@/services/baseServices';

    export default{
        data:()=>{
            return{
                loading: false,
                apiUrl: `${enviroment.apiUrl}/Accounts/Register`,
                confirmPassword : "",
                hospitalItems:[],
                accountItem:{
                    first_name : "",
                    last_name : "",
                    email: "",
                    password : "",
                    account_type: "",
                    hospital_id: 0,
                    dob: formatDateYYYYMMDD(new Date())
                },
                error:{
                    first_name_error : "",
                    last_name_error : "",
                    email_error: "",
                    password_error: "",
                    confirmPassword_error: "",
                    account_type_error:"",
                    dob_error:"",
                    message_error:"",
                    hospital_error:""
                }
            }
        },
        methods:{
            validLastName(){
                if (!this.accountItem.last_name)
                    this.error.last_name_error = "Last name is required";
                else
                    this.error.last_name_error = "";
            },
            validFirstName(){
                if (!this.accountItem.first_name)
                    this.error.first_name_error = "First name is required";
                else
                    this.error.first_name_error = "";
            },
            checkdEmail(){
                if (!this.accountItem.email)
                    this.error.email_error = "Email is required";
                else{
                    if (!validEmail(this.accountItem.email))
                        this.error.email_error = "Email is invalid";
                    else
                        this.error.email_error = "";
                }
            },
            checkPassword(){
                if (!this.accountItem.password)
                    this.error.password_error = "Password is required";
                else
                    this.error.password_error = "";
            },
            checkConfirmPassword(){
                if (!this.confirmPassword)
                    this.error.confirmPassword_error = "Confirm Password is required";
                else{
                    if (this.accountItem.password != this.confirmPassword)
                        this.error.confirmPassword_error = "Passwords must match";
                    else
                        this.error.confirmPassword_error = "";
                }
            },
            validAccountType(){
                if (!this.accountItem.account_type)
                    this.error.account_type_error = "Account Type is required";
                else
                    this.error.account_type_error = "";
            },
            validDob(){
                if (!this.accountItem.dob)
                    this.error.dob_error = "Date of birth is required";
                else
                    this.error.dob_error = "";
            },
            validHospital(){
                if (!this.accountItem.hospital_id)
                    this.error.hospital_error = "Hospital is required";
                else
                    this.error.hospital_error = "";
            },
            async getHospitalItems(){
                return await getItems(`${enviroment.apiUrl}/Hospitals`);
            },
            async onSubmit(){
                this.validLastName();
                this.validFirstName();
                this.checkdEmail();
                this.checkPassword();
                this.checkConfirmPassword();
                this.validAccountType();
                this.validDob();
                this.validHospital();
                if (!this.error.first_name_error &&
                    !this.error.last_name_error &&
                    !this.error.account_type_error &&
                    !this.error.dob_error &&
                    !this.error.email_error &&
                    !this.error.password_error &&
                    !this.error.confirmPassword_error &&
                    !this.error.hospital_error
                ){
                    this.loading = true;
                    var register = await post(this.apiUrl, this.accountItem);
                    if (register.valid){
                        this.$router.push("/Account/Login");
                    }
                    else{
                        this.loading = false;
                        this.error.message_error = register.message;
                        console.log("Error:", register.message);
                    }
                }
            }
        },
        async mounted(){
            var items = await this.getHospitalItems();
            this.hospitalItems = items.data;
        }
    }
</script>
