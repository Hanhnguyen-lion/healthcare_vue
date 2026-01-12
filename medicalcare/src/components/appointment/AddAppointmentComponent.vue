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
            <div class="col-md-6 col-md-10">
                <div class="card">
                    <div class="card-header">
                        <h3>{{ title }}</h3>
                        <form name="form" @submit.prevent="save()">
                            <div class="row mb-3">
                                <div class="form-group">
                                    <label class="fw-bold">{{$t("appointment.appointments.appointmentDate")}} <span class="text-danger">*</span></label>
                                    <input type="date" class="form-control" name="appointment_date" v-model="item.appointment_date"
                                        :placeholder="$t('appointment.addAppointment.enterAppointmentDate')" />
                                    <div v-if="appointment_date_error" class="invalid-feedback">
                                        <div>{{ appointment_date_error }}</div>
                                    </div>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="form-group">
                                    <label class="fw-bold">{{$t("appointment.appointments.time")}}</label>
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
                                    <label class="fw-bold">{{$t("commonText.patient")}} <span class="text-danger">*</span></label>
                                    <select class="form-select" name="patient_id"
                                     v-model="item.patient_id">
                                        <option v-for="item in patientItems" :key="item.id" :value="item.id">
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
                                    <label class="fw-bold">{{$t("commonText.doctor")}}</label>
                                    <select class="form-select" name="doctor_id"
                                     v-model="item.doctor_id">
                                        <option v-for="item in doctorItems" :key="item.id" :value="item.id">
                                            {{item.last_name}} {{item.first_name}}
                                        </option>
                                    </select>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="form-group">
                                    <label class="fw-bold">{{$t("appointment.addAppointment.reasonToVisit")}}</label>
                                    <textarea class="form-control" name="reason_to_visit"
                                     v-model="item.reason_to_visit" :placeholder="$t('appointment.addAppointment.enterReasonToVisit')"/>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="col form-group mb-3 d-grid gap-2 d-md-flex">
                                    <button class="btn btn-outline-primary">
                                        <span v-if="loading" class="spinner-border spinner-border-sm mr-1"></span>
                                        {{$t("buttons.save")}}
                                    </button>
                                    <RouterLink class="btn btn-outline-secondary"
                                        to="/Appointment">{{$t("buttons.cancel")}}</RouterLink>
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
                title: this.$t("appointment.addAppointment.addAppointment"),
                edit_id: null,
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
                    patient_id_guid: null,
                    doctor_id_guid: null,
                    reason_to_visit:"",
                    id: null,
                    id_guid: null,
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
            async getItem(id){
                return await getItemById(`${this.apiUrl}/${id}`);
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
                    this.loading = true;
                    var updated;
                    if (enviroment.mongo_db){
                        this.item.doctor_id_guid = this.item.doctor_id;
                        this.item.doctor_id = null;
                        this.item.patient_id_guid = this.item.patient_id;
                        this.item.patient_id = null;
                    }
                    if (!this.edit_id)
                        updated = await post(`${this.apiUrl}/Add`, this.item);
                    else
                        updated = await updateItem(`${this.apiUrl}/Edit/${this.edit_id}`, this.item);
                    if (updated.valid)
                        this.$router.push("/Appointment");
                    else{
                        this.loading = false;
                        this.message_error = updated.message;
                    }
                }
            }
        },
        async mounted(){
            this.edit_id = this.$route.params["id"];
            if (this.edit_id){
                this.title = this.$t("appointment.addAppointment.editAppointment");
                var data = await this.getItem(this.edit_id);
                var appointment_date = formatDateYYYYMMDD(data.data.appointment_date);
                this.item = data.data;
                this.item.appointment_date = formatDateYYYYMMDD(appointment_date);
                this.item.hour = (!this.item.hour || this.item.hour == 0) ? null: this.item.hour;
                this.item.minute = (!this.item.hour || this.item.hour == 0) ? null: this.item.minute;
            }

            var categories = await this.getPatientItems();
            this.patientItems = categories.data;
            categories = await this.getDoctorItems();
            this.doctorItems = categories.data;

            if (!isSupperAdmin(this.auth.accountLogin)){
                var hospital_id = this.auth.accountLogin.hospital_id;
                this.doctorItems = this.doctorItems.filter(li => li.hospital_id == hospital_id);
                this.patientItems = this.patientItems.filter(li => li.hospital_id == hospital_id);
            }

            this.setHours();
            this.setMinutes();
        }
    }
</script>