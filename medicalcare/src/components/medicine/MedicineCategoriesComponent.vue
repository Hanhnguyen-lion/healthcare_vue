<script setup>
import { deleteItem, getItems } from '@/services/baseServices';
import FooterComponent from '../footer/FooterComponent.vue';
import { enviroment } from '@/enviroments/enviroment';

</script>

<template>
    <div class="container">
        <h2>Medicine Category List</h2>
        <div class="form-group mb-3">
            <RouterLink class="btn btn-outline-primary" to="/Medicine/Category/Add">Add Medicine Category</RouterLink>
        </div>
        <div class="tableFixHead">
            <table class="table table-striped">
                <thead class="table-header">
                <tr>
                    <th>Name EN</th>
                    <th>Name VN</th>
                    <th>Name JP</th>
                    <th>Action</th>
                </tr>
                </thead>
                <tbody>
                <tr v-for="item in data" :key="item.id">
                <td>{{item.name_en}}</td>
                <td>{{item.name_vn}}</td>
                <td>{{item.name_jp}}</td>
                <td v-if="enviroment.mongo_db">
                    <RouterLink class="btn btn-outline-primary" style="margin-left: 10px;" :to="'/Medicine/Category/Edit/'+item.id_guid">Edit</RouterLink>
                    <button class="btn btn-outline-danger" style="margin-left: 10px;" @click="remove(item.id_guid)" type="button">Delete</button>
                </td>
                <td v-else>
                    <RouterLink class="btn btn-outline-primary" style="margin-left: 10px;" :to="'/Medicine/Category/Edit/'+item.id">Edit</RouterLink>
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
        data:()=>{
            return {
                data: [],
                apiUrl:`${enviroment.apiUrl}/MedicinesCategory`
            };
        },
        methods:{
            remove(id){
                this.$confirm(
                {
                    title: 'Delete Medicine Category',
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
                                    if (this.data){
                                        const index = this.data.findIndex(p => p.id === id);
                                        this.data.splice(index, 1)
                                    }
                                }
                            })
                        }
                    }
                });
            },
        },
        mounted(){
            getItems(this.apiUrl)
            .then(response =>{
                if (response.valid){
                    this.data = response.data;
                }
            });
        }
    }
</script>