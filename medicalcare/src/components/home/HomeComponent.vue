<script setup>
import FooterComponent from '../footer/FooterComponent.vue';

</script>
<template>
<div class="carousel slide carousel-fade" data-ride="carousel">
    <div class="carousel-inner">
        <div class="carousel-item active">
            <img src="../../assets/images/1.jpg" class="d-block w-100" alt="...">
            <div class="carousel-caption d-none d-md-block">
                <button class="btn btn-secondary btn-lng"><h2>Welcome to Hospital Management System</h2></button>
                <button class="btn btn-success">Supports hospitals in managing medical records, the hospitalized process of patients, etc.</button>
            </div>
        </div>
    </div>
</div>

<div class="container my-4">
    <div class="row mb-2">
        <div class="col-md-6">
            <div class="row no-gutters border rounded overflow-hidden flex-md-row mb-4 shadow-sm h-md-250 position-relative">
                <div class="col p-4 d-flex flex-column position-static">
                    
                    <h3 class="mb-0">Patient Management</h3>
                    
                    <p class="card-text mb-auto">Patient Management module Hospital Management System is designed to manage everything related to Patient activities.</p>
                    
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="row no-gutters border rounded overflow-hidden flex-md-row mb-4 shadow-sm h-md-250 position-relative">
                <div class="col p-4 d-flex flex-column position-static">
                    <h3 class="mb-0">Doctor Management</h3>
                
                    <p class="card-text mb-auto">Doctor Management module Hospital Management System is designed to manage everything related to Doctor activities.</p>
                
                </div>
            </div>
        </div>
    </div>
    <div class="row mb-2">
        <div class="col-md-6">
            <div class="row no-gutters border rounded overflow-hidden flex-md-row mb-4 shadow-sm h-md-250 position-relative">
                <div class="col p-4 d-flex flex-column position-static">
                    <h3 class="mb-0">Hospital Management</h3>
                    <p class="card-text mb-auto">Hospital Management module Hospital Management System is designed to manage everything related to Hospital activities.</p>
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="row no-gutters border rounded overflow-hidden flex-md-row mb-4 shadow-sm h-md-250 position-relative">
                <div class="col p-4 d-flex flex-column position-static">
                    <h3 class="mb-0">Medicine Management</h3>
                    <p class="card-text mb-auto">Medicine Management module Hospital Management System is designed to manage everything related to Medicine activities.</p>
                </div>
            </div>
        </div>
    </div>
</div>
<div class="container my-4">
    <div class="row mb-2">
        <div class="col-md-6">
            <div class="row no-gutters border rounded overflow-hidden flex-md-row mb-4 shadow-sm h-md-250 position-relative">
                <div class="col p-4 d-flex flex-column position-static">
                    
                    <h3 class="mb-0">Department Management</h3>
                    
                    <p class="card-text mb-auto">Department Management module Hospital Management System is designed to manage everything related to Department activities.</p>
                    
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="row no-gutters border rounded overflow-hidden flex-md-row mb-4 shadow-sm h-md-250 position-relative">
                <div class="col p-4 d-flex flex-column position-static">
                    <h3 class="mb-0">Categories Management</h3>
                
                    <p class="card-text mb-auto">Categories Management module Hospital Management System is designed to manage categories: medicine, treatment, etc...</p>
                
                </div>
            </div>
        </div>
    </div>
    <div class="row mb-2">
        <div class="col-md-6">
            <div class="row no-gutters border rounded overflow-hidden flex-md-row mb-4 shadow-sm h-md-250 position-relative">
                <div class="col p-4 d-flex flex-column position-static">
                    <h3 class="mb-0">Billing Management</h3>
                    <p class="card-text mb-auto">Billing of all Patient consultancy and services with details of Patient Information, Consultation, Prescriptions, Treatment, etc.</p>
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="row no-gutters border rounded overflow-hidden flex-md-row mb-4 shadow-sm h-md-250 position-relative">
                <div class="col p-4 d-flex flex-column position-static">
                    <h3 class="mb-0">Medical Care Management</h3>
                    <p class="card-text mb-auto">Medical Care of all Patient consultancy and services with details of Patient Information, Consultation, Prescriptions, Treatment, etc.</p>
                </div>
            </div>
        </div>
    </div>
    <div class="row mb-2">
        <div class="col-md-6">
            <div class="row no-gutters border rounded overflow-hidden flex-md-row mb-4 shadow-sm h-md-250 position-relative">
                <div class="col p-4 d-flex flex-column position-static">
                    <h3 class="mb-0">Appointment Management</h3>
                    <p class="card-text mb-auto">Appointment Management module Hospital Management System is designed to manage the effective appointments of patients for the doctors</p>
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="row no-gutters border rounded overflow-hidden flex-md-row mb-4 shadow-sm h-md-250 position-relative">
                <div class="col p-4 d-flex flex-column position-static">
                    <h3 class="mb-0">Accounts Management</h3>
                    <p class="card-text mb-auto">Accounts Management module of eHospital - Hospital Management System manages. Manages all hospitals, patients, doctors, billings, etc.</p>
                </div>
            </div>
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
