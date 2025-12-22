<script setup>
    import FooterComponent from '../footer/FooterComponent.vue';
    import { enviroment } from '@/enviroments/enviroment';
    import { deleteItem, getItems } from '@/services/baseServices';
    import numeral from 'numeral';

</script>

<template>
    <div class="container">
        <h2>Medicine List</h2>
        <div class="form-group mb-3">
            <RouterLink to="/Medicine/Add" class="btn btn-outline-primary" >Add Medicine</RouterLink>
        </div>
        <div class="tableFixHead">
            <table class="table table-striped">
                <thead class="table-header">
                <tr>
                    <th>#</th>
                    <th>Name</th>
                    <th>Type</th>
                    <th>Price</th>
                    <th>Action</th>
                </tr>
                </thead>
                <tbody>
                    <tr v-for="(item, index) in items" :key="item.id">
                        <td>{{ index + 1}}</td>
                        <td>{{item.name}}</td>
                        <td>{{item.medicine_type}}</td>
                        <td>{{numeral(item.price).format("0,0.00")}}</td>
                        <td v-if="enviroment.mongo_db">
                            <RouterLink class="btn btn-outline-primary" 
                            :to="'/Medicine/Edit/' + item.id_guid"
                            style="margin-left: 10px;" 
                            >Edit</RouterLink>
                            <button class="btn btn-outline-danger" 
                                style="margin-left: 10px;" 
                                @click="remove(item.id_guid)" type="button">Delete</button>
                        </td>
                        <td v-else>
                            <RouterLink class="btn btn-outline-primary" 
                            :to="'/Medicine/Edit/' + item.id"
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
            url: `${enviroment.apiUrl}/Medicines`
        }
    },
    methods: {
        remove(id){
            this.$confirm(
            {
                title: 'Delete Medicine',
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
                                const index = (enviroment.mongo_db) ? 
                                this.items.findIndex(p => p.id_guid === id) : this.items.findIndex(p => p.id === id);
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
