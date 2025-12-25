<script setup>
    import FooterComponent from '../footer/FooterComponent.vue';
    import { enviroment } from '@/enviroments/enviroment';
    import { deleteItem, getItems } from '@/services/baseServices';

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
                        <td>
                            <RouterLink class="btn btn-outline-primary" 
                            :to="'/Hospital/Edit/' + item.id"
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
            items: [],
            url: `${enviroment.apiUrl}/Hospitals`
        }
    },
    methods: {
        remove(id){
            this.loading = true;
            this.$confirm(
            {
                title: 'Delete Hospital',
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
                            var index = this.items.findIndex(p => p.id === id);
                            this.items.splice(index, 1)
                        }
                    }
                }
            });
        }
    },
    async mounted() {
        var data = await getItems(this.url);
        if(data.valid){
            this.items = data.data;
        }
    }
}

</script>
