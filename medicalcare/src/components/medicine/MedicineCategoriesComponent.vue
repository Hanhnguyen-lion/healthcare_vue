<script setup>
import { getItems } from '@/services/baseServices';
import FooterComponent from '../footer/FooterComponent.vue';
import { enviroment } from '@/enviroments/enviroment';
import AddButton from '../AddButton.vue';
import EditDeleteButtons from '../EditDeleteButtons.vue';

</script>

<template>
    <div class="container">
        <h2>Medicine Category List</h2>
        <div class="form-group mb-3">
            <AddButton title="Add Medicine Category" router-link-to="/Medicine/Category/Add"></AddButton>
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
                    <td>
                        <EditDeleteButtons 
                            :id="item.id" 
                            :apiUrlDelete="apiUrl"
                            :items="data"
                            titleDialog="Delete Medicine Category"
                            routerLinkTo="/Medicine/Category/Edit/"
                            @removeItem="handleItemRemoval">
                        </EditDeleteButtons>
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
            handleItemRemoval(index){
                this.data.splice(index, 1)
            },
        },
        async mounted(){
            var response = await getItems(this.apiUrl);
            if (response.valid){
                this.data = response.data;
            }
        }
    }
</script>