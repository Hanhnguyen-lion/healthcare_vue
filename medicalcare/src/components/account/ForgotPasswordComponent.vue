<template>
<div class="container">
    <div class="row justify-content-center">
        <div class="col-md-6 col-md-10">
            <div class="card">
                <h3 class="card-header">{{ $t('account.login.forgotPassword') }}</h3>
                <div class="card-body">
                    <form name="form" @submit.prevent="onSubmit()">
                        <div class="form-group mb-3">
                            <label class="fw-bold">{{ $t('commonText.email') }} <span class="text-danger">*</span></label>
                            <input type="text" name="email" v-model="accountItem.email" class="form-control" :placeholder="$t('commonText.enterEmail')" />
                            <div v-if="email_error" class="invalid-feedback">
                                <div>{{email_error}}</div>
                            </div>
                        </div>
                        <div v-if="resetPassword" class="form-group mb-3">
                            <label class="fw-bold">{{ $t('patient.patients.code') }} <span class="text-danger">*</span></label>
                            <input type="text" name="code" v-model="accountItem.reset_password_code" class="form-control"/>
                            <div v-if="reset_password_code_error" class="invalid-feedback">
                                <div>{{reset_password_code_error}}</div>
                            </div>
                        </div>
                        <div v-if="resetPassword" class="form-group mb-3">
                            <label class="fw-bold">{{ $t('account.register.password') }} <span class="text-danger">*</span></label>
                            <input type="password" name="password" v-model="accountItem.password" class="form-control"/>
                            <div v-if="password_error" class="invalid-feedback">
                                <div>{{password_error}}</div>
                            </div>
                        </div>
                        <div v-if="resetPassword" class="form-group mb-3">
                            <label class="fw-bold">{{ $t('account.register.confirmPassword') }} <span class="text-danger">*</span></label>
                            <input type="password" name="confirmPassword" v-model="confirmPassword" class="form-control"/>
                            <div v-if="confirmPassword_error" class="invalid-feedback">
                                <div>{{confirmPassword_error}}</div>
                            </div>
                        </div>
                        <div class="form-group mb-3">
                            <div class="form-group col d-grid gap-2 d-md-flex">
                                <button :disabled="loading" class="btn btn-outline-primary">
                                    <span v-if="loading" class="spinner-border spinner-border-sm mr-1"></span>
                                    {{ $t('buttons.send') }}
                                </button>
                                <RouterLink class="btn btn-outline-secondary"
                                    to="/Account/Login">{{ $t('buttons.cancel') }}</RouterLink>
                            </div>
                        </div>
                        <div v-if="message_error" class="form-group mb-3 invalid-feedback">
                            {{ message_error }}
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
import { updateItem} from '@/services/baseServices';
import { validEmail } from '../helper/helper';

    export default{
        data:()=>{
            return{
                message_error: "",
                email_error: "",
                confirmPassword_error: "",
                password_error: "",
                reset_password_code_error: "",
                accountItem:{
                    email:"",
                    password: "",
                    reset_password_code: ""
                },
                loading: false,
                resetPassword: false,
                confirmPassword: "",
                apiUrl: `${enviroment.apiUrl}/Accounts/ForgotPassword`
            };
        },
        methods:{
            checkdEmail(){
                if (!this.accountItem.email)
                    this.email_error = this.$t("messages.emailRequired");
                else{
                    if (!validEmail(this.accountItem.email))
                        this.email_error = this.$t("messages.emailInvalid");
                    else
                        this.email_error = "";
                }
            },
            checkCode(){
                if (!this.accountItem.reset_password_code)
                    this.reset_password_code_error = this.$t("messages.codeRequired");
                else
                    this.reset_password_code_error = "";
            },
            checkPassword(){
                if (!this.accountItem.password)
                    this.password_error = this.$t("messages.passwordRequired");
                else
                    this.password_error = "";
            },
            checkConfirmPassword(){
                if (!this.confirmPassword)
                    this.confirmPassword_error = this.$t("messages.confirmpasswordRequired");
                else{
                    if (this.accountItem.password != this.confirmPassword)
                        this.confirmPassword_error = this.$t("messages.passwordMatch");
                    else
                        this.confirmPassword_error = "";
                }
            },
            async onSubmit(){
                this.checkdEmail();
                if (this.resetPassword){
                    this.checkCode();
                    this.checkPassword();
                    this.checkConfirmPassword();
                }
                if (!this.email_error &&
                    !this.password_error &&
                    !this.confirmPassword_error &&
                    !this.reset_password_code_error
                ){
                    this.loading = true;
                    var data;
                    if (this.resetPassword){
                        data = await updateItem(`${enviroment.apiUrl}/Accounts/ResetPassword`, this.accountItem);
                    }
                    else{
                        data = await updateItem(this.apiUrl, this.accountItem);
                    }
                    if (data.valid){
                        this.loading = false;
                        this.message_error = "";
                        if (!this.resetPassword)
                            this.resetPassword = true;
                        else{
                            this.$router.push("/Account/Login");
                        }
                    }
                    else{
                        this.loading = false;
                        var error = this.$t("messages.emailNotExists");
                        if (this.resetPassword){
                            error = "Code is invalid";
                        }
                        this.resetPassword = false;
                        this.message_error = error;
                    }
                }
            }
        }
    }
</script>
