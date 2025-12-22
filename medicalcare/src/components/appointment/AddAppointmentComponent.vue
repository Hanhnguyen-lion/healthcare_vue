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
                                    <input type="date" class="form-control" name="appointment_date" v-model="item.appointment_date"
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
                                    <select class="form-select" name="patient_id"
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
                                    <label class="fw-bold">Reason To Visit</label>
                                    <textarea class="form-control" name="reason_to_visit"
                                     v-model="item.reason_to_visit" placeholder="Enter reason to visit"/>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="col form-group mb-3 d-grid gap-2 d-md-flex">
                                    <button class="btn btn-outline-primary">
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
                title: "Add Appointment",
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
                    if (enviroment.mongo_db){
                        this.item.doctor_id_guid = this.item.doctor_id;
                        this.item.doctor_id = null;
                        this.item.patient_id_guid = this.item.patient_id;
                        this.item.patient_id = null;
                    }
                    if (!this.edit_id){
                        await post(`${this.apiUrl}/Add`, this.item).then(response=>{
                            if (response.valid){
                                this.$router.push("/Appointment");
                            }
                            else
                                this.message_error = response.message;
                        });
                    }
                    else{
                        if (enviroment.mongo_db){
                            this.item.id_guid = this.edit_id;
                        }
                        await updateItem(`${this.apiUrl}/Edit/${this.edit_id}`, this.item).then(response=>{
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
            this.edit_id = this.$route.params["id"];
            if (this.edit_id){
                this.title = "Edit Appointment";
                var data = await this.getItem(this.edit_id);
                var appointment_date = formatDateYYYYMMDD(data.data.appointment_date);
                this.item = data.data;
                this.item.appointment_date = formatDateYYYYMMDD(appointment_date);
                this.item.doctor_id = (enviroment.mongo_db) ? this.item.doctor_id_guid : this.item.doctor_id;
                this.item.patient_id = (enviroment.mongo_db) ? this.item.patient_id_guid : this.item.patient_id;
            }

            var categories = await this.getPatientItems();
            var patients = categories.data;
            categories = await this.getDoctorItems();
            var doctors = categories.data;

            if (!isSupperAdmin(this.auth.accountLogin)){
                if (enviroment.mongo_db){
                    var hospital_id_guid = this.auth.accountLogin.hospital_id_guid || "";
                    doctors = doctors.filter(li => li.hospital_id_guid == hospital_id_guid);
                    patients = patients.filter(li => li.hospital_id_guid == hospital_id_guid);
                }
                else{
                    var hospital_id = this.auth.accountLogin.hospital_id || 0;
                    doctors = doctors.filter(li => li.hospital_id == hospital_id);
                    patients = patients.filter(li => li.hospital_id_guid == hospital_id_guid);
                }
            }
            for(var i = 0; i < doctors.length; i++){
                this.doctorItems.push(
                    {
                        id: (enviroment.mongo_db) ? doctors[i].id_guid : doctors[i].id,
                        first_name: doctors[i].first_name,
                        last_name: doctors[i].last_name
                    }
                );
            }
            for(var j = 0; j < patients.length; j++){
                this.patientsItems.push(
                    {
                        id: (enviroment.mongo_db) ? patients[j].id_guid : patients[j].id,
                        first_name: patients[j].first_name,
                        last_name: patients[j].last_name
                    }
                );
            }

            this.setHours();
            this.setMinutes();
        }
    }
</script>