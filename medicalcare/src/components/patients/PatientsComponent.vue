<script setup>
    import FooterComponent from '../footer/FooterComponent.vue';
    import { enviroment } from '@/enviroments/enviroment';
    import { formatDateToString } from '../helper/helper';
    import { deleteItem, getItems } from '@/services/baseServices';
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
                    <td>
                        <button class="btn btn-outline-primary" 
                            style="margin-left: 10px;" 
                            @click="edit(item.id)" type="button">Edit</button>
                        <button class="btn btn-outline-info" 
                            style="margin-left: 10px;" 
                            @click="view(item.id)" type="button">View</button>
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
            if (response.valid)
                this.patients = response.data;
        });
    }
}

</script>
