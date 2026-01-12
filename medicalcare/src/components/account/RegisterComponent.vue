<template>
    <div class="container">
        <div class="row justify-content-center">
            <div class="col-md-6 col-lg-12">
                <div class="card">
                    <h3 class="card-header">{{ $t('account.register.title') }}</h3>
                    <div class="card-body">
                        <form name="form" @submit.prevent="onSubmit()">
                        <div class="row mb-3">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">{{ $t('commonText.firstName') }} <span class="text-danger">*</span></label>
                                    <input type="text" name="first_name" v-model="accountItem.first_name" :placeholder="$t('commonText.enterFirstName')" class="form-control"/>
                                    <div v-if="error.first_name_error" class="invalid-feedback">
                                        <div>{{error.first_name_error}}</div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">{{ $t('commonText.lastName') }} <span class="text-danger">*</span></label>
                                    <input type="text" name="last_name" v-model="accountItem.last_name" :placeholder="$t('commonText.enterLastName')" class="form-control"/>
                                        <div v-if="error.last_name_error" class="invalid-feedback">
                                            <div>{{error.last_name_error}}</div>
                                        </div>
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">{{ $t('account.register.password') }} <span class="text-danger">*</span></label>
                                    <input type="password" name="password" v-model="accountItem.password" :placeholder="$t('account.register.passwordPlaceholder')" class="form-control"/>
                                        <div v-if="error.password_error" class="invalid-feedback">
                                            <div>{{error.password_error}}</div>
                                        </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">{{ $t('account.register.confirmPassword') }} <span class="text-danger">*</span></label>
                                    <input type="password" name="confirmPassword" v-model="confirmPassword" :placeholder="$t('account.register.confirmPasswordPlaceholder')" class="form-control"/>
                                        <div v-if="error.confirmPassword_error" class="invalid-feedback">
                                            <div>{{error.confirmPassword_error}}</div>
                                        </div>
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">{{ $t('commonText.phone') }}</label>
                                    <input  type="text" class="form-control" 
                                        name="phone" v-model="accountItem.phone" :placeholder="$t('commonText.enterPhone')">
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">{{ $t('commonText.email') }}<span class="text-danger">*</span></label>
                                    <input type="text" name="email" v-model="accountItem.email" :placeholder="$t('commonText.enterEmail')" class="form-control"/>
                                        <div v-if="error.email_error" class="invalid-feedback">
                                            <div>{{error.email_error}}</div>
                                        </div>
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">{{ $t('commonText.gender') }}</label>
                                    <div class="form-control btn-group btn-group-toggle" data-toggle="buttons">
                                        <label class="btn">
                                            <input type="radio" name="gender" v-model="accountItem.gender" id="Female" value="Female" checked>Female
                                        </label>
                                        <label class="btn">
                                            <input type="radio" name="gender" v-model="accountItem.gender" id="Male" value="Male">Male
                                        </label>
                                        <label class="btn">
                                            <input type="radio" name="gender" v-model="accountItem.gender" id="Other" value="Other">Other
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">{{ $t('account.register.dob') }}</label>
                                    <input  type="date" class="form-control" 
                                        name="dob" v-model="accountItem.dob"
                                        placeholder="Enter date of birth">
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label class="fw-bold">{{ $t('account.register.address') }}</label>
                                    <input  type="text" class="form-control" 
                                        name="address" v-model="accountItem.address" placeholder="Enter address">
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">{{ $t('account.register.role') }} <span class="text-danger">*</span></label>
                                    <select name="role" v-model="accountItem.role" class="form-select" @change="onChangeHandler($event)" placeholder="Enter acount type">
                                        <option value=""></option>
                                        <option value="Super Admin">Super Admin</option>
                                        <option value="Admin">Admin</option>
                                        <option value="Doctor">Doctor</option>
                                    </select>
                                    <div v-if="error.role_error" class="invalid-feedback">
                                        <div>{{error.role_error}}</div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">{{ $t('account.register.accountType') }} <span class="text-danger">*</span></label>
                                    <select name="account_type" v-model="accountItem.account_type" class="form-select">
                                        <option value=""></option>
                                        <option value="Free">Free</option>
                                        <option value="Member">Member</option>
                                    </select>
                                    <div v-if="error.account_type_error" class="invalid-feedback">
                                        <div>{{error.account_type_error}}</div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label class="fw-bold">{{ $t('commonText.hospital') }}</label>
                                    <select :disabled="accountItem.role == SuperAdmin" name="hospital_id" v-model="accountItem.hospital_id" class="form-select">
                                        <option v-for="item in hospitalItems" :key="item.id" :value="item.id">
                                            {{ item.name }}
                                        </option>
                                    </select>
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col form-group mb-3 d-grid gap-2 d-md-flex">
                                <button :disabled="loading" class="btn btn-outline-primary">
                                    <span v-if="loading" class="spinner-border spinner-border-sm mr-1"></span>
                                    {{ $t('buttons.save') }}
                                </button>
                                <RouterLink class="btn btn-outline-secondary"
                                    to="/Account">{{ $t('buttons.cancel') }}</RouterLink>
                            </div>
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
<RouterView></RouterView>
</template>
<script>
import { enviroment } from '@/enviroments/enviroment';
import { formatDateToString, formatDateYYYYMMDD, validEmail } from '../helper/helper';
import { getItemById, getItems, post, updateItem } from '@/services/baseServices';

    export default{
        data:()=>{
            return{
                SuperAdmin : "Super Admin",
                edit_id: null,
                loading: false,
                apiUrl: `${enviroment.apiUrl}/Accounts/Register`,
                confirmPassword : "",
                hospitalItems:[],
                accountItem:{
                    id: null,
                    id_guid: null,
                    first_name : null,
                    last_name : null,
                    email: null,
                    password : null,
                    account_type: null,
                    role: null,
                    gender: "Female",
                    address: null,
                    phone: null,
                    dob: formatDateToString(new Date(), "YYYY-MM-DD"),
                    hospital_id: null,
                    hospital_id_guid: null
                },
                editItem:{
                    first_name : null,
                    last_name : null,
                    email: null,
                    password : null,
                    account_type: null,
                    role: null,
                    gender: null,
                    address: null,
                    phone: null,
                    dob: null,
                    hospital_id: null,
                    hospital_id_guid: null
                },
                error:{
                    first_name_error : "",
                    last_name_error : "",
                    email_error: "",
                    password_error: "",
                    confirmPassword_error: "",
                    account_type_error:"",
                    role_error: "",
                    message_error:""
                }
            }
        },
        methods:{
            onChangeHandler(e){
                if (e.target.value == this.SuperAdmin){
                    this.accountItem.account_type = "Member";
                }
                else{
                    if (!this.edit_id)
                        this.accountItem.account_type = "";
                }
            },
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
            validRole(){
                if (!this.accountItem.role)
                    this.error.role_error = "Role is required";
                else{
                    this.error.role_error = "";
                    if (this.accountItem.role != this.SuperAdmin){
                        if (!this.accountItem.hospital_id)
                            this.error.role_error = "Hospital is required";
                    }
                }
            },
            async getHospitalItems(){
                return await getItems(`${enviroment.apiUrl}/Hospitals`);
            },
            async getItem(id){
                return await getItemById(`${enviroment.apiUrl}/Accounts/${id}`);
            },
            async onSubmit(){
                this.validLastName();
                this.validFirstName();
                this.checkdEmail();
                this.checkPassword();
                this.checkConfirmPassword();
                this.validRole();
                this.validAccountType();
                if (!this.error.first_name_error &&
                    !this.error.last_name_error &&
                    !this.error.role_error &&
                    !this.error.account_type_error &&
                    !this.error.email_error &&
                    !this.error.password_error &&
                    !this.error.confirmPassword_error
                ){
                    this.loading = true;
                    var updated;
                    if (this.accountItem.role == this.SuperAdmin)
                        this.accountItem.hospital_id = null;
                    this.editItem.account_type = this.accountItem.account_type;
                    this.editItem.email = this.accountItem.email;
                    this.editItem.password = this.accountItem.password;
                    this.editItem.dob = this.accountItem.dob;
                    this.editItem.phone = this.accountItem.phone;
                    this.editItem.address = this.accountItem.address;
                    this.editItem.gender = this.accountItem.gender;
                    this.editItem.first_name = this.accountItem.first_name;
                    this.editItem.last_name = this.accountItem.last_name;
                    this.editItem.role = this.accountItem.role;
                    this.editItem.hospital_id = null;
                    this.editItem.hospital_id_guid = this.accountItem.hospital_id;
                    if (!this.edit_id){
                        updated = await post(this.apiUrl, this.editItem);
                    }
                    else{
                        var url = `${enviroment.apiUrl}/Accounts/Edit/${this.edit_id}`;
                        updated = await updateItem(url, this.editItem);
                    }
                    if (updated.valid)
                        this.$router.push("/Account");
                    else{
                        this.loading = false;
                        this.error.message_error = updated.message
                        console.log("Error:", updated.message);
                    }
                }
            }
        },
        async mounted(){
            var id = this.$route.params["id"];
            if (id){
                this.edit_id = id;
                var data = await this.getItem(id);
                this.accountItem = data.data;
                var dob = this.accountItem.dob;
                this.confirmPassword = this.accountItem.password;
                this.accountItem.dob = dob ? formatDateYYYYMMDD(dob) : null;
            }
            var items = await this.getHospitalItems();
            this.hospitalItems = items.data;
        }
    }
</script>
