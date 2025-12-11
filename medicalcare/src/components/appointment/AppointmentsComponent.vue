<script setup>
    import FooterComponent from '../footer/FooterComponent.vue';
    import { enviroment } from '@/enviroments/enviroment';
    import { deleteItem, getItems } from '@/services/baseServices';
import { formatDateToString } from '../helper/helper';

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
                    <td>{{item.reason_to_visit}}</td>
                    <td>
                        <RouterLink class="btn btn-outline-primary" 
                        :to="'/Appointment/Edit/' + item.id"
                        style="margin-left: 10px;" 
                        >Edit</RouterLink>
                        <button class="btn btn-outline-danger" 
                            style="margin-left: 10px;" 
                            @click="remove(item.id)" type="button">Delete</button>
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
            items: [],
            url: `${enviroment.apiUrl}/Appointments`
        }
    },
    methods: {
        remove(id){
            this.$confirm(
            {
                title: 'Delete Appointment',
                message: 'Are you sure to want delete this item?',
                button: {
                    no: 'No',
                    yes: 'Yes'
                },
                callback: confirm => {
                    if (confirm) {
                        var apiUrl = `${this.url}/Delete/${id}`;
                        deleteItem(apiUrl)
                        .then(response=>{
                            if (response.valid){
                                const index = this.items.findIndex(p => p.id === id);
                                this.items.splice(index, 1)
                            }
                        })
                    }
                }
            });
        }
    },
    mounted() {
        getItems(this.url)
        .then(data =>{
            this.items = data.data;
        });
    }
}

</script>
