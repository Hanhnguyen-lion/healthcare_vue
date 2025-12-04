import { createRouter, createWebHistory } from "vue-router";

import HomeComponent from "@/components/home/HomeComponent.vue";
import LoginComponent from "@/components/account/LoginComponent.vue";
import PatientsComponent from "@/components/patients/PatientsComponent.vue";
import AddPatientComponent from "@/components/patients/AddPatientComponent.vue";


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
        }
    ]
})

export default router
