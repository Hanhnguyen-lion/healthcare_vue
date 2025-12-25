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
            url: `${enviroment.apiUrl}/Doctors`
        }
    },
    methods: {
        remove(id){
            this.loading = true;
            this.$confirm(
            {
                title: 'Delete Doctor',
                message: 'Are you sure to want delete this item?',
                button: {
                    no: 'No',
                    yes: 'Yes'
                },
                callback: async confirm => {
                    if (confirm) {
                        var apiUrl = `${this.url}/Delete/${id}`;
                        var deleted = await deleteItem(apiUrl);
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
        var data = await getItems(this.url)
        this.items = data.data;
            
        if (!isSupperAdmin(this.auth.accountLogin)){
            var hospital_id = this.auth.accountLogin.hospital_id;
            this.items = this.items.filter(li => li.hospital_id == hospital_id);
        }
    }
}

</script>
