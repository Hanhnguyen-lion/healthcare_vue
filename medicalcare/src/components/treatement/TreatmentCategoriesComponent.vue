<script setup>
import {getItems } from '@/services/baseServices';
import FooterComponent from '../footer/FooterComponent.vue';
import { enviroment } from '@/enviroments/enviroment';
import AddButton from '../AddButton.vue';
import EditDeleteButtons from '../EditDeleteButtons.vue';
 var numberal = require("numeral");

</script>

<template>
    <div class="container">
        <h2>Treatment Category List</h2>
        <div class="form-group mb-3">
            <AddButton title="Add Treatment Category" router-link-to="/Treatement/Category/Add"></AddButton>
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
                    <td>
                        <EditDeleteButtons 
                            :id="item.id" 
                            :apiUrlDelete="apiUrl"
                            :items="categories"
                            titleDialog="Delete Treatement Category"
                            routerLinkTo="/Treatement/Category/Edit/"
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
        data(){
            return {
                categories:[],
                apiUrl: `${enviroment.apiUrl}/TreatmentsCategory`
            };
        },
        methods:{
            handleItemRemoval(index){
                this.categories.splice(index, 1);
            }
        },
        async mounted() {
            var data = await getItems(this.apiUrl);
            if (data.valid)
                this.categories = data.data;
        }
    }
</script>