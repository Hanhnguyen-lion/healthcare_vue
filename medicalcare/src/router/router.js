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
            component: HomeComponent,
            meta: { requiredAuth: false }
        },
        {
            path: "/Account/Login",
            component: LoginComponent,
            meta: { requiredAuth: false }
        },
        {
            path: "/Account/Forgotpassword",
            component: ForgotPasswordComponent,
            meta: { requiredAuth: false }
        },
        {
            path: "/Account/Register",
            component: RegisterComponent,
            meta: { requiredAuth: false }
        },
        {
            path: "/Patient",
            component: PatientsComponent,
            meta: { requiredAuth: true }
        },
        {
            path: "/Patient/Add",
            component: AddPatientComponent,
            meta: { requiredAuth: true }
        },
        {
            path: "/Patient/Edit/:id",
            component: AddPatientComponent,
            meta: { requiredAuth: true }
        },
        {
            path: "/Patient/View/:id",
            component: AddPatientComponent,
            meta: { requiredAuth: true }
        },
        {
            path: "/Billing",
            component: BillingComponent,
            meta:{
                requiresAuth: true
            }
        },
        {
            path: "/Billing/Add",
            component: AddBillingComponent,
            meta: { requiredAuth: true }
        },
        {
            path: "/Billing/Edit/:id",
            component: AddBillingComponent,
            meta: { requiredAuth: true }
        },
        {
            path: "/Billing/View/:id",
            component: BillingDetailComponent,
            meta: { requiredAuth: true }
        },
        {
            path: "/MedicalCare/Print",
            component: ViewMedicalCareComponent,
            meta: { requiredAuth: true }
        },
        {
            path: "/Test",
            component: TestComponent,
            meta: { requiredAuth: false }
        },
        // router.beforeEach((to, from) => {
        //     if (to.meta.requiresAuth){
        //         return {name: 'Login'}
        //     }
        // })
    ]
})

export default router
