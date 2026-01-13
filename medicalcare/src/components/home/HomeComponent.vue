<script setup>
import FooterComponent from '../footer/FooterComponent.vue';

</script>
<template>
<div class="carousel slide carousel-fade" data-ride="carousel">
    <div class="carousel-inner">
        <div class="carousel-item active">
            <img src="../../assets/images/1.jpg" class="d-block w-100" alt="...">
            <div class="carousel-caption d-none d-md-block">
                <button class="btn btn-secondary btn-lng"><h2>{{$t("home.title_1")}}</h2></button>
                <button class="btn btn-success">{{$t("home.title_2")}}</button>
            </div>
        </div>
    </div>
</div>

<div class="container my-4">
    <div class="row mb-2">
        <div class="col-md-6">
            <RouterLink to="/Patient" class="nav-link text-black row no-gutters border rounded overflow-hidden flex-md-row mb-4 shadow-sm h-md-250 position-relative">
                <div class="col p-4 d-flex flex-column position-static">
                    
                    <h3 class="mb-0">{{$t("home.title_3")}}</h3>
                    
                    <p class="card-text mb-auto">{{$t("home.title_4")}}</p>
                    
                </div>
            </RouterLink>
        </div>
        <div class="col-md-6">
            <RouterLink to="/Doctor" class="nav-link text-black row no-gutters border rounded overflow-hidden flex-md-row mb-4 shadow-sm h-md-250 position-relative">
                <div class="col p-4 d-flex flex-column position-static">
                    <h3 class="mb-0">{{$t("home.title_5")}}</h3>
                
                    <p class="card-text mb-auto">{{$t("home.title_6")}}</p>
                
                </div>
            </RouterLink>
        </div>
    </div>
    <div class="row mb-2">
        <div class="col-md-6">
            <RouterLink to="/Hospital" class="nav-link text-black row no-gutters border rounded overflow-hidden flex-md-row mb-4 shadow-sm h-md-250 position-relative">
                <div class="col p-4 d-flex flex-column position-static">
                    <h3 class="mb-0">{{$t("home.title_7")}}</h3>
                    <p class="card-text mb-auto">{{$t("home.title_8")}}</p>
                </div>
            </RouterLink>
        </div>
        <div class="col-md-6">
            <RouterLink to="/Medicine" class="nav-link text-black row no-gutters border rounded overflow-hidden flex-md-row mb-4 shadow-sm h-md-250 position-relative">
                <div class="col p-4 d-flex flex-column position-static">
                    <h3 class="mb-0">{{$t("home.title_9")}}</h3>
                    <p class="card-text mb-auto">{{$t("home.title_10")}}</p>
                </div>
            </RouterLink>
        </div>
    </div>
</div>
<div class="container my-4">
    <div class="row mb-2">
        <div class="col-md-6">
            <RouterLink to="/Department" class="nav-link text-black row no-gutters border rounded overflow-hidden flex-md-row mb-4 shadow-sm h-md-250 position-relative">
                <div class="col p-4 d-flex flex-column position-static">
                    
                    <h3 class="mb-0">{{$t("home.title_11")}}</h3>
                    
                    <p class="card-text mb-auto">{{$t("home.title_12")}}</p>
                    
                </div>
            </RouterLink>
        </div>
        <div class="col-md-6">
            <RouterLink to="/Medicine/Category" class="nav-link text-black row no-gutters border rounded overflow-hidden flex-md-row mb-4 shadow-sm h-md-250 position-relative">
                <div class="col p-4 d-flex flex-column position-static">
                    <h3 class="mb-0">{{$t("home.title_13")}}</h3>
                    <p class="card-text mb-auto">{{$t("home.title_14")}}</p>
                </div>
            </RouterLink>
        </div>
    </div>
    <div class="row mb-2">
        <div class="col-md-6">
            <RouterLink to="/Billing" class="nav-link text-black row no-gutters border rounded overflow-hidden flex-md-row mb-4 shadow-sm h-md-250 position-relative">
                <div class="col p-4 d-flex flex-column position-static">
                    <h3 class="mb-0">{{$t("home.title_15")}}</h3>
                    <p class="card-text mb-auto">{{$t("home.title_16")}}</p>
                </div>
            </RouterLink>
        </div>
        <div class="col-md-6">
            <RouterLink to="/MedicalCare/Print" class="nav-link text-black row no-gutters border rounded overflow-hidden flex-md-row mb-4 shadow-sm h-md-250 position-relative">
                <div class="col p-4 d-flex flex-column position-static">
                    <h3 class="mb-0">{{$t("home.title_17")}}</h3>
                    <p class="card-text mb-auto">{{$t("home.title_18")}}</p>
                </div>
            </RouterLink>
        </div>
    </div>
    <div class="row mb-2">
        <div class="col-md-6">
            <RouterLink to="/Appointment" class="nav-link text-black row no-gutters border rounded overflow-hidden flex-md-row mb-4 shadow-sm h-md-250 position-relative">
                <div class="col p-4 d-flex flex-column position-static">
                    <h3 class="mb-0">{{$t("home.title_19")}}</h3>
                    <p class="card-text mb-auto">{{$t("home.title_20")}}</p>
                </div>
            </RouterLink>
        </div>
        <div class="col-md-6">
            <RouterLink to="/Account" class="nav-link text-black row no-gutters border rounded overflow-hidden flex-md-row mb-4 shadow-sm h-md-250 position-relative">
                <div class="col p-4 d-flex flex-column position-static">
                    <h3 class="mb-0">{{$t("home.title_21")}}</h3>
                    <p class="card-text mb-auto">{{$t("home.title_22")}}</p>
                </div>
            </RouterLink>
        </div>
    </div>
</div>
<FooterComponent></FooterComponent>
</template>
<style scoped>
.carousel-caption 
{
    color: rgb(255, 255, 255);
    text-shadow: -1px 1px 0 #000,
                1px 1px 0 #000,
                1px -1px 0 #000,
                -1px -1px 0 #000;
}
</style>
