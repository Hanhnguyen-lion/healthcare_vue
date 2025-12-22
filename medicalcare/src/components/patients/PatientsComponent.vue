<script setup>
    import FooterComponent from '../footer/FooterComponent.vue';
    import { enviroment } from '@/enviroments/enviroment';
    import { formatDateToString, isSupperAdmin } from '../helper/helper';
    import { deleteItem, getItems } from '@/services/baseServices';
import { useAuthStore } from '@/store/auth.module';
</script>

<template>
    <div class="container">
        <h2>Patient List</h2>
        <div class="form-group mb-3">
            <button class="btn btn-outline-primary" 
            type="button" @click="add()">Add Patient</button>
        </div>
        <div class="tableFixHead">
            <table class="table table-striped">
                <thead class="table-header">
                <tr>
                    <th>Code</th>
                    <th>First Name</th>
                    <th>Last Name</th>
                    <th>Date Of Birth</th>
                    <th>Gender</th>
                    <th>Hospital</th>
                    <th>Action</th>
                </tr>
                </thead>
                <tbody>
                    <tr v-for="item in patients" :key="item.id">
                        <td>{{item.code}}</td>
                        <td>{{item.first_name}}</td>
                        <td>{{item.last_name}}</td>
                        <td><div v-if="item.date_of_birth">{{formatDateToString(item.date_of_birth, 'DD/MM/YYYY')}}</div></td>
                        <td>{{item.gender}}</td>
                        <td>{{item.hospital_name}}</td>
                        <td>
                            <button class="btn btn-outline-primary" 
                                style="margin-left: 10px;" 
                                @click="edit(item.id)" type="button">Edit</button>
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
            patients: [],
            url: `${enviroment.apiUrl}/Patients`
        }
    },
    methods: {
        remove(id){
            this.$confirm(
            {
                title: 'Delete Patient',
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
                                const index = this.patients.findIndex(p => p.id === id);
                                this.patients.splice(index, 1)
                            }
                        })
                    }
                }
            })
        },
        add(){
            this.$router.push("/Patient/Add");
        },
        edit(id){
            this.$router.push(`/Patient/Edit/${id}`);
        },
        view(id){
            this.$router.push(`/Patient/View/${id}`);
        }
    },
    mounted() {
        getItems(this.url)
        .then(response =>{
            if (response.valid){
                var items = response.data;
                for(var i = 0; i< items.length; i++){
                    var item = items[i];
                    var newItem = {
                        id: (enviroment.mongo_db) ? item.id_guid : item.id,
                        hospital_id: (enviroment.mongo_db) ? item.hospital_id_guid:item.hospital_id,
                        hospital_name: (enviroment.mongo_db) ? item.hospital_name: "",
                        code: item.code,
                        first_name: item.first_name,
                        last_name: item.last_name,
                        date_of_birth: item.date_of_birth,
                        gender: item.gender
                    };
                    this.patients.push(newItem);
                }

                if (!isSupperAdmin(this.auth.accountLogin)){
                    if (enviroment.mongo_db){
                        var hospital_id_guid = this.auth.accountLogin.hospital_id_guid || "";
                        this.patients = this.patients.filter(li => li.hospital_id == hospital_id_guid);
                    }
                    else{
                        var hospital_id = this.auth.accountLogin.hospital_id || 0;
                        this.patients = this.patients.filter(li => li.hospital_id == hospital_id);
                    }
                }
            }
        });
    }
}

</script>
