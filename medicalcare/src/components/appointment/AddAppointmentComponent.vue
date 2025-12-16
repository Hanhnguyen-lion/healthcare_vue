<script setup>
import { getItemById, getItems, post, updateItem } from '@/services/baseServices';
import FooterComponent from '../footer/FooterComponent.vue';
import { enviroment } from '@/enviroments/enviroment';
import { formatDateYYYYMMDD, isSupperAdmin, pad} from '../helper/helper';
import { useAuthStore } from '@/store/auth.module';
</script>

<template>
    <div class="container">
        <div class="row justify-content-center">
            <div class="col-md-6 col-md-4">
                <div class="card">
                    <div class="card-header">
                        <h3>{{ title }}</h3>
                        <form name="form" @submit.prevent="save()">
                            <div class="row mb-3">
                                <div class="form-group">
                                    <label class="fw-bold">Appointment Date <span class="text-danger">*</span></label>
                                    <input :readonly="readonly" type="date" class="form-control" name="appointment_date" v-model="item.appointment_date"
                                        placeholder="Enter first name" />
                                    <div v-if="appointment_date_error" class="invalid-feedback">
                                        <div>{{ appointment_date_error }}</div>
                                    </div>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="form-group">
                                    <label class="fw-bold">Time</label>
                                    <div class="input-group mb-2">
                                        <select class="form-select" name="hour" v-model="item.hour">
                                            <option v-for="hour in hours" :key="hour" :value="hour">
                                                {{hour}}
                                            </option>
                                        </select>
                                        <select class="form-select" name="minute" v-model="item.minute">
                                            <option v-for="minute in minutes" :key="minute" :value="minute">
                                                {{pad(minute)}}
                                            </option>
                                        </select>
                                    </div>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="form-group">
                                    <label class="fw-bold">Patient <span class="text-danger">*</span></label>
                                    <select :disabled="readonly" class="form-select" name="patient_id"
                                     v-model="item.patient_id">
                                        <option v-for="item in patientsItems" :key="item.id" :value="item.id">
                                            {{item.last_name}} {{item.first_name}}
                                        </option>
                                    </select>
                                    <div v-if="patient_error" class="invalid-feedback">
                                        <div>{{ patient_error }}</div>
                                    </div>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="form-group">
                                    <label class="fw-bold">Doctor</label>
                                    <select :disabled="readonly" class="form-select" name="doctor_id"
                                     v-model="item.doctor_id">
                                        <option v-for="item in doctorItems" :key="item.id" :value="item.id">
                                            {{item.last_name}} {{item.first_name}}
                                        </option>
                                    </select>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="form-group">
                                    <label class="fw-bold">Reason To Visit</label>
                                    <textarea :readonly="readonly" class="form-control" name="reason_to_visit"
                                     v-model="item.reason_to_visit" placeholder="Enter reason to visit"/>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="col form-group mb-3 d-grid gap-2 d-md-flex">
                                    <button :disabled="readonly" class="btn btn-outline-primary">
                                        <span v-if="loading" class="spinner-border spinner-border-sm mr-1"></span>
                                        Save
                                    </button>
                                    <RouterLink class="btn btn-outline-secondary"
                                        to="/Appointment">Cancel</RouterLink>
                                </div>
                                <div class="row mb-3">
                                    <div v-if="message_error" class="form-group">
                                       {{ message_error }}
                                    </div>
                                </div>
                        </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <RouterView></RouterView>
    <FooterComponent></FooterComponent>
</template>
<script>
    export default{
        data(){
            return{
                auth: useAuthStore(),
                loading: false,
                readonly: false,
                title: "Add Appointment",
                patient_error:"",
                appointment_date_error:"",
                message_error:"",
                doctorItems:[],
                patientsItems:[],
                hours:[],
                minutes:[],
                item:{
                    appointment_date:formatDateYYYYMMDD(new Date()),
                    patient_id: null,
                    doctor_id: null,
                    reason_to_visit:"",
                    id: 0,
                    hour: null,
                    minute: null
                },
                apiUrl: `${enviroment.apiUrl}/Appointments`
            }
        },
        methods:{
            setHours(){
                for (let index = 1; index <= 24; index++){
                    this.hours.push(index);
                }
            },
            setMinutes(){
                for (let index = 0; index <= 60; index++){
                    this.minutes.push(index);
                }
            },
            validPatient(){
                if (!this.item.patient_id)
                    this.patient_error = "Patient is required";
                else
                    this.patient_error = "";
            },
            validAppointmentDate(){
                if (!this.item.appointment_date)
                    this.appointment_date_error = "Appointment date is required";
                else
                    this.appointment_date_error = "";
            },
            async getItem(){
                return await getItemById(`${this.apiUrl}/${this.item.id}`);
            },
            async getPatientItems(){
                return await getItems(`${enviroment.apiUrl}/Patients`);
            },
            async getDoctorItems(){
                return await getItems(`${enviroment.apiUrl}/Doctors`);
            },
            async save(){
                this.validAppointmentDate();
                this.validPatient();
                if (!this.patient_error &&
                    !this.appointment_date_error
                ){
                    if (this.item.id == 0){
                        
                        await post(`${this.apiUrl}/Add`, this.item).then(response=>{
                            if (response.valid){
                                this.$router.push("/Appointment");
                            }
                            else
                                this.message_error = response.message;
                        });
                    }
                    else{
                        await updateItem(`${this.apiUrl}/Edit/${this.item.id}`, this.item).then(response=>{
                            if (response.valid){
                                this.$router.push("/Appointment");
                            }
                            else
                                this.message_error = response.message;
                        });
                    }
                }
            }
        },
        async mounted(){
            this.item.id = this.$route.params["id"] || 0;
            var currentPath = this.$route.path;
            if (this.item.id > 0){
                this.title = "Edit Appointment";
                if (currentPath.indexOf("View") != -1){
                    this.title = "View Appointment";
                    this.readonly = true;
                }
                var data = await this.getItem();
                var appointment_date = formatDateYYYYMMDD(data.data.appointment_date);
                this.item = data.data;
                this.item.appointment_date = formatDateYYYYMMDD(appointment_date);
            }

            var categories = await this.getPatientItems();
            this.patientsItems = categories.data;
            categories = await this.getDoctorItems();
            this.doctorItems = categories.data;
            if (!isSupperAdmin(this.auth.accountLogin)){
                var hospital_id = this.auth.accountLogin.hospital_id;
                this.patientsItems = this.patientsItems.filter(li=>li.hospital_id == hospital_id);
                this.doctorItems = this.doctorItems.filter(li=>li.hospital_id == hospital_id);
            }

            this.setHours();
            this.setMinutes();
        }
    }
</script>