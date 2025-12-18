<script setup>
    import FooterComponent from '../footer/FooterComponent.vue';
    import { enviroment } from '@/enviroments/enviroment';
    import { deleteItem, getItems } from '@/services/baseServices';
import { useAuthStore } from '@/store/auth.module';
import { isSupperAdmin } from '../helper/helper';

</script>

<template>
    <div class="container">
        <h2>Hospital List</h2>
        <div class="form-group mb-3">
            <RouterLink to="/Hospital/Add" class="btn btn-outline-primary" >Add Hospital</RouterLink>
        </div>
        <div class="tableFixHead">
            <table class="table table-striped">
                <thead class="table-header">
                <tr>
                    <th>#</th>
                    <th>Name</th>
                    <th>Phone</th>
                    <th>Email</th>
                    <th>Country</th>
                    <th>Action</th>
                </tr>
                </thead>
                <tbody>
                    <tr v-for="(item, index) in items" :key="item.id">
                        <td>{{ index + 1}}</td>
                        <td>{{item.name}}</td>
                        <td>{{item.phone}}</td>
                        <td>{{item.email}}</td>
                        <td>{{item.country}}</td>
                        <td v-if="enviroment.mongo_db">
                            <RouterLink class="btn btn-outline-primary" 
                            :to="'/Hospital/Edit/' + item.hospital_id_guid"
                            style="margin-left: 10px;" 
                            >Edit</RouterLink>
                            <button class="btn btn-outline-danger" 
                                style="margin-left: 10px;" 
                                @click="remove(item.hospital_id_guid)" type="button">Delete</button>
                        </td>
                        <td v-else>
                            <RouterLink class="btn btn-outline-primary" 
                            :to="'/Hospital/Edit/' + item.id"
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
            auth:useAuthStore(),
            items: [],
            url: `${enviroment.apiUrl}/Hospitals`
        }
    },
    methods: {
        remove(id){
            this.$confirm(
            {
                title: 'Delete Hospital',
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
                                var index = 0;
                                if (enviroment.mongo_db)
                                    index = this.items.findIndex(p => p.id === id);
                                else
                                    index = this.items.findIndex(p => p.hospital_id_guid === id);
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
            if (!isSupperAdmin(this.auth.accountLogin)){
                var hospital_id = this.auth.accountLogin.hospital_id || 0;
                this.items = this.items.filter(li=>li.id == hospital_id);
            }
        });
    }
}

</script>
