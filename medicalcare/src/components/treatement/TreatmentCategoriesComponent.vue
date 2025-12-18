<script setup>
import { deleteItem, getItems } from '@/services/baseServices';
import FooterComponent from '../footer/FooterComponent.vue';
import { enviroment } from '@/enviroments/enviroment';
 var numberal = require("numeral");

</script>

<template>
    <div class="container">
        <h2>Treatment Category List</h2>
        <div class="form-group mb-3">
            <RouterLink class="btn btn-outline-primary" to="/Treatement/Category/Add">Add Treatment Category</RouterLink>
        </div>
        <div class="tableFixHead">
            <table class="table table-striped">
                <thead class="table-header">
                <tr>
                    <th>Name EN</th>
                    <th>Name VN</th>
                    <th>Name JP</th>
                    <th>Price</th>
                    <th>Action</th>
                </tr>
                </thead>
                <tbody>
                <tr v-for="item in categories" :key="item.id">
                    <td>{{item.name_en}}</td>
                    <td>{{item.name_vn}}</td>
                    <td>{{item.name_jp}}</td>
                    <td>{{numberal(item.price).format("0,0.00")}}</td>
                    <td v-if="enviroment.mongo_db">
                        <RouterLink class="btn btn-outline-primary" style="margin-left: 10px;" :to="'/Treatement/Category/Edit/'+item.id_guid">Edit</RouterLink>
                        <button class="btn btn-outline-danger" style="margin-left: 10px;" @click="remove(item.id_guid)" type="button">Delete</button>
                    </td>
                    <td v-else>
                        <RouterLink class="btn btn-outline-primary" style="margin-left: 10px;" :to="'/Treatement/Category/Edit/'+item.id">Edit</RouterLink>
                        <button class="btn btn-outline-danger" style="margin-left: 10px;" @click="remove(item.id)" type="button">Delete</button>
                    </td>
                </tr>
                </tbody>
            </table>
        </div>
    </div>
    <RouterView></RouterView>
    <FooterComponent></FooterComponent>
</template>
<script>
    export default{
        data(){
            return {
                categories:[],
                apiUrl: `${enviroment.apiUrl}/Treatments/Category`
            };
        },
        methods:{
            remove(id){
                this.$confirm(
                {
                    title: 'Delete Treatement Category',
                    message: 'Are you sure to want delete this item?',
                    button: {
                        no: 'No',
                        yes: 'Yes'
                    },
                    callback: confirm => {
                        if (confirm) {
                            deleteItem(`${this.apiUrl}/Delete/${id}`)
                            .then(response=>{
                                if (response.valid){
                                    const index = this.categories.findIndex(p => p.id === id);
                                    this.categories.splice(index, 1)
                                }
                            })
                        }
                    }
                })
            }
        },
        mounted() {
            getItems(this.apiUrl)
            .then(response =>{
                if (response.valid)
                    this.categories = response.data;
            });
        }
    }
</script>