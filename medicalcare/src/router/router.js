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
