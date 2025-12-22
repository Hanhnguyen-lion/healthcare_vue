<script setup>
    import FooterComponent from '../footer/FooterComponent.vue';
    import { enviroment } from '@/enviroments/enviroment';
    import { deleteItem, getItems } from '@/services/baseServices';
import { useAuthStore } from '@/store/auth.module';
import { isSupperAdmin } from '../helper/helper';

</script>

<template>
    <div class="container">
        <h2>Doctor List</h2>
        <div class="form-group mb-3">
            <RouterLink to="/Doctor/Add" class="btn btn-outline-primary" >Add Doctor</RouterLink>
        </div>
        <div class="tableFixHead">
            <table class="table table-striped">
                <thead class="table-header">
                <tr>
                    <th>#</th>
                    <th>First Name</th>
                    <th>Last Name</th>
                    <th>Phone</th>
                    <th>Email</th>
                    <th>Gender</th>
                    <th>Hospital</th>
                    <th>Action</th>
                </tr>
                </thead>
                <tbody>
                    <tr v-for="(item, index) in items" :key="item.id">
                    <td>{{ index + 1}}</td>
                    <td>{{item.first_name}}</td>
                    <td>{{item.last_name}}</td>
                    <td>{{item.phone}}</td>
                    <td>{{item.email}}</td>
                    <td>{{item.gender}}</td>
                    <td>{{item.hospital_name}}</td>
                    <td>
                        <RouterLink class="btn btn-outline-primary" 
                        :to="'/Doctor/Edit/' + item.id"
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
            auth: useAuthStore(),
            items: [],
            url: `${enviroment.apiUrl}/Doctors`
        }
    },
    methods: {
        remove(id){
            this.$confirm(
            {
                title: 'Delete Doctor',
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
        .then(response =>{
            var doctors = response.data;
            if (doctors){
                for(var i = 0; i< doctors.length; i++){
                    var item = doctors[i];
                    var newItem = {
                        id: (enviroment.mongo_db) ? item.id_guid : item.id,
                        hospital_id: (enviroment.mongo_db) ? item.hospital_id_guid:item.hospital_id,
                        hospital_name: (enviroment.mongo_db) ? item.hospital_name: "",
                        first_name: item.first_name,
                        last_name: item.last_name,
                        phone: item.phone,
                        email: item.email,
                        gender: item.gender
                    };
                    this.items.push(newItem);
                }
            }
            
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
