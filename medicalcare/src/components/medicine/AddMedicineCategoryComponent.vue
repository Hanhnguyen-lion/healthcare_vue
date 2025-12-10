<script setup>
import { getItemById, post, updateItem } from '@/services/baseServices';
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
                                    <label class="fw-bold">Name EN <span class="text-danger">*</span></label>
                                    <input type="text" class="form-control" name="name_en" v-model="item.name_en"
                                        placeholder="Enter name EN" />
                                    <div v-if="name_en_error" class="invalid-feedback">
                                        <div>{{ name_en_error }}</div>
                                    </div>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="form-group">
                                    <label class="fw-bold">Name VN</label>
                                    <input type="text" class="form-control" name="name_vn" v-model="item.name_vn"
                                        placeholder="Enter name VN" />
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="form-group">
                                    <label class="fw-bold">Name JP</label>
                                    <input type="text" class="form-control" name="name_jp" v-model="item.name_jp"
                                        placeholder="Enter name JP" />
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="form-group">
                                    <label class="fw-bold">Description</label>
                                    <textarea rows="3" class="form-control" name="description" v-model="item.description"
                                        placeholder="Enter description" />
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="col form-group mb-3 d-grid gap-2 d-md-flex">
                                    <button :disabled="loading" class="btn btn-outline-primary">
                                        <span v-if="loading" class="spinner-border spinner-border-sm mr-1"></span>
                                        Save
                                    </button>
                                    <RouterLink class="btn btn-outline-secondary"
                                        to="/Medicine/Category">Cancel</RouterLink>
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
                title: "Add Medicine Category",
                name_en_error:"",
                message_error:"",
                item:{
                    description:"",
                    name_jp: "",
                    name_vn: "",
                    name_en: "",
                    id: 0,
                },
                apiUrl: `${enviroment.apiUrl}/Medicines/Category`
            }
        },
        methods:{
            validName(){
                if (!this.item.name_en)
                    this.name_en_error = "Name is required";
                else
                    this.name_en_error = "";
            },
            async getItem(){
                return await getItemById(`${this.apiUrl}/${this.item.id}`);
            },
            async save(){
                this.validName();
                if (!this.name_en_error){
                    if (this.item.id == 0){
                        
                        await post(`${this.apiUrl}/Add`, this.item).then(response=>{
                            if (response.valid){
                                this.$router.push("/Medicine/Category");
                            }
                            else
                                this.message_error = response.message;
                        });
                    }
                    else{
                        await updateItem(`${this.apiUrl}/Edit/${this.item.id}`, this.item).then(response=>{
                            if (response.valid){
                                this.$router.push("/Medicine/Category");
                            }
                            else
                                this.message_error = response.message;
                        });
                    }
                }
            }
        },
        async mounted(){
            this.item.id = this.$route.params["id"] || 0;
            if (this.item.id > 0){
                this.title = "Edit Medicine Category";
                var data = await this.getItem();
                this.item = data.data;
            }
        }
    }
</script>