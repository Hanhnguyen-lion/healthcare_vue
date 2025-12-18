import { createRouter, createWebHistory } from "vue-router";

import HomeComponent from "@/components/home/HomeComponent.vue";
import LoginComponent from "@/components/account/LoginComponent.vue";
import PatientsComponent from "@/components/patients/PatientsComponent.vue";
import AddPatientComponent from "@/components/patients/AddPatientComponent.vue";
import TestComponent from "@/components/TestComponent.vue";
import BillingComponent from "@/components/billing/BillingComponent.vue";
import AddBillingComponent from "@/components/billing/AddBillingComponent.vue";
import BillingDetailComponent from "@/components/billing/BillingDetailComponent.vue";
import ViewMedicalCareComponent from "@/components/MedicalCare/ViewMedicalCareComponent.vue";
import RegisterComponent from "@/components/account/RegisterComponent.vue";
import ForgotPasswordComponent from "@/components/account/ForgotPasswordComponent.vue";
import AddMedicineCategoryComponent from "@/components/medicine/AddMedicineCategoryComponent.vue";
import AddTreatmentCategoryComponent from "@/components/treatement/AddTreatmentCategoryComponent.vue";
import TreatmentCategoriesComponent from "@/components/treatement/TreatmentCategoriesComponent.vue";
import MedicinesComponent from "@/components/medicine/MedicinesComponent.vue";
import MedicineCategoriesComponent from "@/components/medicine/MedicineCategoriesComponent.vue";
import AddMedicineComponent from "@/components/medicine/AddMedicineComponent.vue";
import HospitalsComponent from "@/components/hospital/HospitalsComponent.vue";
import AddHospitalComponent from "@/components/hospital/AddHospitalComponent.vue";
import DepartmentsComponent from "@/components/department/DepartmentsComponent.vue";
import AddDepartmentComponent from "@/components/department/AddDepartmentComponent.vue";
import DoctorsComponent from "@/components/doctor/DoctorsComponent.vue";
import AddDoctorComponent from "@/components/doctor/AddDoctorComponent.vue";
import AppointmentsComponent from "@/components/appointment/AppointmentsComponent.vue";
import AddAppointmentComponent from "@/components/appointment/AddAppointmentComponent.vue";


const router = createRouter({
    history: createWebHistory(),
    routes:[
        {
            path: "/",
            component: HomeComponent
        },
        {
            path: "/Account/Login",
            component: LoginComponent
        },
        {
            path: "/Account/Forgotpassword",
            component: ForgotPasswordComponent
        },
        {
            path: "/Account/Register",
            component: RegisterComponent
        },
        {
            path: "/Treatement/Category",
            component: TreatmentCategoriesComponent
        },
        {
            path: "/Treatement/Category/Edit/:id",
            component: AddTreatmentCategoryComponent
        },
        {
            path: "/Treatement/Category/Add",
            component: AddTreatmentCategoryComponent
        },
        {
            path: "/Medicine/Category",
            component: MedicineCategoriesComponent
        },
        {
            path: "/Medicine/Category/Edit/:id",
            component: AddMedicineCategoryComponent
        },
        {
            path: "/Medicine/Category/Add",
            component: AddMedicineCategoryComponent
        },
        {
            path: "/Patient",
            component: PatientsComponent
        },
        {
            path: "/Patient/Add",
            component: AddPatientComponent
        },
        {
            path: "/Patient/Edit/:id",
            component: AddPatientComponent
        },
        {
            path: "/Patient/View/:id",
            component: AddPatientComponent
        },
        {
            path: "/Medicine",
            component: MedicinesComponent
        },
        {
            path: "/Medicine/Add",
            component: AddMedicineComponent
        },
        {
            path: "/Medicine/Edit/:id",
            component: AddMedicineComponent
        },
        {
            path: "/Medicine/View/:id",
            component: AddMedicineComponent
        },
        {
            path: "/Hospital",
            component: HospitalsComponent
        },
        {
            path: "/Hospital/Add",
            component: AddHospitalComponent
        },
        {
            path: "/Hospital/Edit/:id",
            component: AddHospitalComponent
        },
        {
            path: "/Hospital/View/:id",
            component: AddHospitalComponent
        },
        {
            path: "/Department",
            component: DepartmentsComponent
        },
        {
            path: "/Department/Add",
            component: AddDepartmentComponent
        },
        {
            path: "/Department/Edit/:id",
            component: AddDepartmentComponent
        },
        {
            path: "/Department/View/:id",
            component: AddDepartmentComponent
        },
        {
            path: "/Doctor",
            component: DoctorsComponent
        },
        {
            path: "/Doctor/Add",
            component: AddDoctorComponent
        },
        {
            path: "/Doctor/Edit/:id",
            component: AddDoctorComponent
        },
        {
            path: "/Doctor/View/:id",
            component: AddDoctorComponent
        },
        {
            path: "/Appointment",
            component: AppointmentsComponent
        },
        {
            path: "/Appointment/Add",
            component: AddAppointmentComponent
        },
        {
            path: "/Appointment/Edit/:id",
            component: AddAppointmentComponent
        },
        {
            path: "/Appointment/View/:id",
            component: AddAppointmentComponent
        },
        {
            path: "/Billing",
            component: BillingComponent
        },
        {
            path: "/Billing/Add",
            component: AddBillingComponent
        },
        {
            path: "/Billing/Edit/:id",
            component: AddBillingComponent
        },
        {
            path: "/Billing/View/:id",
            component: BillingDetailComponent
        },
        {
            path: "/MedicalCare/Print",
            component: ViewMedicalCareComponent
        },
        {
            path: "/Test",
            component: TestComponent
        },
        // router.beforeEach((to, from) => {
        //     if (to.meta.requiresAuth){
        //         return {name: 'Login'}
        //     }
        // })
    ]
})

export default router
