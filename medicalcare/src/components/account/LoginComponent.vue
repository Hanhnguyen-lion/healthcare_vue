<template>
    <div class="image">
        <div class="container-fluid">
            <div class="d-flex align-items-center justify-content-center vh-100">
                <div class="col-md-6 col-lg-4">
                    <div class="card">
                        <h3 class="card-header">Login</h3>
                        <div class="card-body">
                            <form name="form" @submit.prevent="handleLogin">
                                <div class="form-group mb-3">
                                    <label>Email <span class="text-danger">*</span></label>
                                    <input type="text" required name="email" v-model="email" class="form-control"
                                        placeholder="Enter your email" />
                                </div>
                                <div class="form-group mb-3">
                                    <label>Password <span class="text-danger">*</span></label>
                                    <input type="password" required name="password" v-model="password"
                                        class="form-control" placeholder="Enter password" />
                                </div>
                                <div class="form-group mb-3">
                                    <div class="form-group col d-md-flex">
                                        <RouterLink to="/Account/Forgotpassword"
                                            class="btn btn-link fw-bold text-black">Forgot Password?</RouterLink>
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
                                        <div v-if="inValid">{{ messageError }}</div>
                                    </div>
                                </div>
                                <div class="form-group mb-3">
                                    <RouterLink to="/Account/Register" class="btn btn-outline-secondary">Don't have an
                                        account? Register</RouterLink>
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
import { useAuthStore } from '@/store/auth.module';

export default {
    name: "LoginComponent",
    data: () => {
        return {
            email: "",
            password: "",
            inValid: false,
            messageError: ""
        }
    },
    methods: {
        async handleLogin() {
            const useAuth = useAuthStore();
            var data = await useAuth.login(this.email, this.password);
            if (!data.valid){
                this.inValid = true;
                this.messageError = data.message;
                this.email = "";
                this.password = "";
            }
            else{
                this.$router.push('/');
            }
        }
    }
}
</script>
