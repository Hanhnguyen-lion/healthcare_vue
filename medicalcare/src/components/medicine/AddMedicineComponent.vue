<script setup>
import { getItemById, getItems, post, updateItem } from '@/services/baseServices';
import FooterComponent from '../footer/FooterComponent.vue';
import { enviroment } from '@/enviroments/enviroment';

</script>

<template>
    <div class="container">
        <div class="row justify-content-center">
            <div class="col-md-6 col-md-4">
                <div class="card">
                    <div class="card-header">
                        <h3>{{ title }}</h3>
                        <form name="form" @submit.prevent="save()">
                            <div class="row mb-3">
                                <div class="form-group">
                                    <label class="fw-bold">Name <span class="text-danger">*</span></label>
                                    <input type="text" class="form-control" name="name" v-model="item.name"
                                        placeholder="Enter name" />
                                    <div v-if="name_error" class="invalid-feedback">
                                        <div>{{ name_error }}</div>
                                    </div>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="form-group">
                                    <label class="fw-bold">Type</label>
                                    <select class="form-select" name="category_id"
                                     v-model="item.category_id">
                                        <option v-for="item in medicineTypeItems" :key="item.id" :value="item.id">
                                            {{item.name_en}}
                                        </option>
                                    </select>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="form-group">
                                    <label class="fw-bold">Price</label>
                                    <input type="number" :min="0" class="form-control" name="price" v-model="item.price"
                                        placeholder="Enter price" />
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="col form-group mb-3 d-grid gap-2 d-md-flex">
                                    <button class="btn btn-outline-primary">
                                        <span v-if="loading" class="spinner-border spinner-border-sm mr-1"></span>
                                        Save
                                    </button>
                                    <RouterLink class="btn btn-outline-secondary"
                                        to="/Medicine">Cancel</RouterLink>
                                </div>
                                <div class="row mb-3">
                                    <div v-if="message_error" class="form-group">
                                       {{ message_error }}
                                    </div>
                                </div>
                        </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <RouterView></RouterView>
    <FooterComponent></FooterComponent>
</template>
<script>
    export default{
        data(){
            return{
                loading: false,
                title: "Add Medicine",
                name_error:"",
                message_error:"",
                medicineTypeItems:[],
                item:{
                    name:"",
                    category_id: null,
                    price: 0,
                    id: null,
                    id_guid: "",
                    category_id_guid: ""
                },
                apiUrl: `${enviroment.apiUrl}/Medicines`
            }
        },
        methods:{
            validName(){
                if (!this.item.name)
                    this.name_error = "Name is required";
                else
                    this.name_error = "";
            },
            async getItem(id){
                return await getItemById(`${this.apiUrl}/${id}`);
            },
            async getMedicineTypeItems(){
                return await getItems(`${enviroment.apiUrl}/MedicinesCategory`);
            },
            async save(){
                this.validName();
                if (!this.name_en_error){
                    if (enviroment.mongo_db){
                        this.item.category_id_guid = this.item.category_id;
                        this.item.category_id = null;
                    }
                    if (!this.item.id){
                        
                        await post(`${this.apiUrl}/Add`, this.item).then(response=>{
                            if (response.valid){
                                this.$router.push("/Medicine");
                            }
                            else
                                this.message_error = response.message;
                        });
                    }
                    else{
                        await updateItem(`${this.apiUrl}/Edit/${this.item.id}`, this.item).then(response=>{
                            if (response.valid){
                                this.$router.push("/Medicine");
                            }
                            else
                                this.message_error = response.message;
                        });
                    }
                }
            }
        },
        async mounted(){
            var id = this.$route.params["id"];
            if (id){
                this.title = "Edit Medicine";
                var data = await this.getItem(id);
                this.item = data.data;
                this.item.id = id;
                this.item.category_id = (enviroment.mongo_db) ? this.item.category_id_guid : this.item.category_id;
            }

            var categories = await this.getMedicineTypeItems();
            for(var i = 0; i < categories.data.length; i++){
                this.medicineTypeItems.push({
                    id: categories.data[i].id_guid,
                    name_en: categories.data[i].name_en
                });
            }
        }
    }
</script>