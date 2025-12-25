<script setup>
    import FooterComponent from '../footer/FooterComponent.vue';
    import { enviroment } from '@/enviroments/enviroment';
    import { deleteItem, getItems } from '@/services/baseServices';
import { formatDateToString, isSupperAdmin } from '../helper/helper';
import { useAuthStore } from '@/store/auth.module';

</script>

<template>
    <div class="container">
        <h2>Appointment List</h2>
        <div class="form-group mb-3">
            <RouterLink to="/Appointment/Add" class="btn btn-outline-primary" >Add Appointment</RouterLink>
        </div>
        <div class="tableFixHead">
            <table class="table table-striped">
                <thead class="table-header">
                <tr>
                    <th>#</th>
                    <th>Patient</th>
                    <th>Doctor</th>
                    <th>Appointment Date</th>
                    <th>Time</th>
                    <th>Reason</th>
                    <th>Action</th>
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
                            <RouterLink class="btn btn-outline-primary" 
                            :to="'/Appointment/Edit/' + item.id"
                            style="margin-left: 10px;" 
                            >Edit</RouterLink>
                            <button class="btn btn-outline-danger" 
                                style="margin-left: 10px;" 
                                @click="remove(item.id)" type="button">Delete
                                <span v-if="loading" class="spinner-border spinner-border-sm mr-1"></span>
                            </button>
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
            loading: false,
            auth: useAuthStore(),
            items: [],
            url: `${enviroment.apiUrl}/Appointments`
        }
    },
    methods: {
        remove(id){
            this.loading = true;
            this.$confirm(
            {
                title: 'Delete Appointment',
                message: 'Are you sure to want delete this item?',
                button: {
                    no: 'No',
                    yes: 'Yes'
                },
                callback: async confirm => {
                    if (confirm) {
                        var apiUrl = `${this.url}/Delete/${id}`;
                        var deleted = await deleteItem(apiUrl)
                        if (deleted.valid){
                            this.loading = false;
                            const index = this.items.findIndex(p => p.id === id);
                            this.items.splice(index, 1)
                        }
                    }
                }
            });
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
