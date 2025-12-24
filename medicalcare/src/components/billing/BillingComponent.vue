<script setup>
    import FooterComponent from '../footer/FooterComponent.vue';
    import { enviroment } from '@/enviroments/enviroment';
    import { formatDateToString, isSupperAdmin } from '../helper/helper';
    import { deleteItem, getItems } from '@/services/baseServices';
    import { useAuthStore } from '@/store/auth.module';
    var numberal = require("numeral");

</script>

<template>
    <div class="container">
        <h2>Billing List</h2>
        <div class="form-group mb-3">
            <RouterLink to="/Billing/Add" class="btn btn-outline-primary" >Add Billing</RouterLink>
        </div>
        <div class="tableFixHead">
            <table class="table table-striped">
                <thead class="table-header">
                <tr>
                    <th>#</th>
                    <th>Billing Date</th>
                    <th>Patient Name</th>
                    <th>Total</th>
                    <th>Action</th>
                </tr>
                </thead>
                <tbody>
                    <tr v-for="(item, index) in items" :key="item.billing_id">
                    <td>{{ index + 1}}</td>
                    <td>{{formatDateToString(item.billing_date, 'DD/MM/YYYY')}}</td>
                    <td>{{item.patient_name}}</td>
                    <td>{{numberal(item.total).format("0,0.00")}}</td>
                    <td>
                        <RouterLink class="btn btn-outline-primary" 
                        :to="'/Billing/Edit/' + item.billing_id"
                        style="margin-left: 10px;" 
                        >Edit</RouterLink>
                        <RouterLink class="btn btn-outline-info" 
                        style="margin-left: 10px;" 
                        :to="'/Billing/View/' + item.billing_id"
                        >View</RouterLink>
                        <button class="btn btn-outline-danger" 
                            style="margin-left: 10px;" 
                            @click="remove(item.billing_id)" type="button">Delete</button>
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
            url: `${enviroment.apiUrl}/Billings`
        }
    },
    methods: {
        remove(id){
            this.$confirm(
            {
                title: 'Delete Billing',
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
                                const index = this.items.findIndex(p => p.billing_id === id);
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
        .then(response =>{
            this.items = response.data;

            if (!isSupperAdmin(this.auth.accountLogin)){
                if (enviroment.mongo_db){
                    var hospital_id_guid = this.auth.accountLogin.hospital_id_guid || "";
                    this.items = this.items.filter(li => li.hospital_id == hospital_id_guid);
                }
                else{
                    var hospital_id = this.auth.accountLogin.hospital_id || 0;
                    this.items = this.items.filter(li => li.hospital_id == hospital_id);
                }
            }
        });
    }
}

</script>
