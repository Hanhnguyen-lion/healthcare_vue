<script setup>
    import FooterComponent from '../footer/FooterComponent.vue';
    import { enviroment } from '@/enviroments/enviroment';
    import { formatDateToString } from '../helper/helper';
    import { deleteItem, getItems } from '@/services/baseServices';
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
                    <tr v-for="(item, index) in items" :key="item.id">
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
            items: [],
            url: `${enviroment.apiUrl}/Billings`
        }
    },
    mounted() {
        var data = getItems(this.url)
        data.then(data =>{
            this.items = data;
        });
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
                        var deleted = deleteItem(this.url, id);
                        deleted.then(deleted=>{
                            if (deleted){
                                const index = this.items.findIndex(p => p.id === id);
                                this.items.splice(index, 1)
                            }
                        })
                    }
                }
            })
        }
    }
}

</script>
