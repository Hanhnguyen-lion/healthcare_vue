<script setup>
    import FooterComponent from '../footer/FooterComponent.vue';
    import { enviroment } from '@/enviroments/enviroment';
    import { getItems } from '@/services/baseServices';
import { formatDateToString, isSupperAdmin } from '../helper/helper';
import { useAuthStore } from '@/store/auth.module';
import AddButton from '../AddButton.vue';
import EditDeleteButtons from '../EditDeleteButtons.vue';

</script>

<template>
    <div class="container">
        <h2>{{$t("appointment.appointments.title")}}</h2>
        <div class="form-group mb-3">
            <AddButton router-link-to="/Appointment/Add" :title="$t('buttons.add')"></AddButton>
        </div>
        <div class="tableFixHead">
            <table class="table table-striped">
                <thead class="table-header">
                <tr>
                    <th>#</th>
                    <th>{{$t("commonText.patient")}}</th>
                    <th>{{$t("commonText.doctor")}}</th>
                    <th>{{$t("appointment.appointments.appointmentDate")}}</th>
                    <th>{{$t("appointment.appointments.time")}}</th>
                    <th>{{$t("appointment.appointments.reason")}}</th>
                    <th></th>
                </tr>
                </thead>
                <tbody>
                    <tr v-for="(item, index) in items" :key="item.id">
                        <td>{{ index + 1}}</td>
                        <td>{{item.patient_name}}</td>
                        <td>{{item.doctor_name}}</td>
                        <td>{{formatDateToString(item.appointment_date, "DD/MM/YYYY")}}</td>
                        <td>{{item.times}}</td>
                        <td>{{item.reason_to_visit}}</td>
                        <td>
                            <EditDeleteButtons 
                                :id="item.id" 
                                :apiUrlDelete="url"
                                :items="items"
                                :titleDialog="this.$t('messages.deleteAppointment')"
                                routerLinkTo="/Appointment/Edit/"
                                @removeItem="handleItemRemoval">
                            </EditDeleteButtons>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
    
    <FooterComponent></FooterComponent>
    <RouterView></RouterView>
</template>
<script>

export default{
    data() {
        return {
            auth: useAuthStore(),
            items: [],
            url: `${enviroment.apiUrl}/Appointments`
        }
    },
    methods: {
        handleItemRemoval(index){
            this.items.splice(index, 1)
        }
    },
    async mounted() {
        var appoitments = await getItems(this.url);
        
        if (appoitments.valid){
            this.items = appoitments.data;
        }

        if (!isSupperAdmin(this.auth.accountLogin)){
            var hospital_id = this.auth.accountLogin.hospital_id;
            this.items = this.items.filter(li => li.hospital_id == hospital_id);
        }
    }
}

</script>
