<template>
<div class="image">
    <div class="container-fluid">
        <div class="d-flex align-items-center justify-content-center vh-100">
            <div class="col-md-6 col-lg-4">
                <div class="card">
                    <h3 class="card-header">Login</h3>
                    <div class="card-body">
                        <form v-on:submit.prevent="handleLogin">
                            <div class="form-group mb-3">
                                <label>Email <span class="text-danger">*</span></label>
                                <input type="text" required v-model="email" class="form-control" placeholder="Enter your email" />
                            </div>
                            <div class="form-group mb-3">
                                <label>Password <span class="text-danger">*</span></label>
                                <input type="password" required v-model="password" class="form-control" placeholder="Enter password" />
                            </div>
                            <div class="form-group mb-3">
                                <div class="form-group col d-md-flex">
                                    <RouterLink to="/Account/Forgotpassword" class="btn btn-link fw-bold text-black">Forgot Password?</RouterLink>
                                </div>
                            </div>
                            <div class="form-group mb-3">
                                <div class="form-group col d-grid gap-2 d-md-flex">
                                    <button type="submit" class="btn btn-outline-primary">
                                        Login
                                    </button>
                                </div>
                            </div>
                            <div class="form-group mb-3">
                                <div class="invalid-feedback">
                                    <div v-if="inValid">{{messageError}}</div>
                                </div>
                            </div>
                            <div class="form-group mb-3">
                                <RouterLink to="/Account/Register" class="btn btn-outline-secondary">Don't have an account? Register</RouterLink>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<router-view></router-view>
</template>
<script>
import accountService from '@/services/accountService';

export default{
    name: "LoginComponent",
    data:()=>{
        return{
            email: "",
            password: "",
            inValid: false,
            messageError: ""
        }
    },
    methods:{
        handleLogin(){
            var accountItem = accountService.login(this.email, this.password);
            accountItem.then((accountItem)=>{
                if (accountItem && accountItem.item){
                    console.log("accountItem: ",accountItem.item);
                    this.$router.push("/");
                }
                else{
                    this.inValid = true;
                    this.messageError = accountItem.message; 
                }
            }).catch((error)=>{
                console.log("login error: ", error);
                this.inValid = true;
                this.messageError = "Email or password is incorrect.";
            });
        }
    }
}
</script>
