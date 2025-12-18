<script setup>
import { getItemById, post, updateItem } from '@/services/baseServices';
import FooterComponent from '../footer/FooterComponent.vue';
import { enviroment } from '@/enviroments/enviroment';
import { validEmail } from '../helper/helper';

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
                                    <label class="fw-bold">Phone</label>
                                    <input type="text" class="form-control" name="phone" v-model="item.phone"
                                        placeholder="Enter phone" />
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="form-group">
                                    <label class="fw-bold">Country</label>
                                    <input type="text" class="form-control" name="country" v-model="item.country"
                                        placeholder="Enter country" />
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="form-group">
                                    <label class="fw-bold">Address</label>
                                    <input type="text" class="form-control" name="address" v-model="item.address"
                                        placeholder="Enter address" />
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="form-group">
                                    <label class="fw-bold">Email</label>
                                    <input type="text" class="form-control" name="email" v-model="item.email"
                                        placeholder="Enter email" />
                                    <div v-if="email_error" class="invalid-feedback">
                                        <div>{{ email_error }}</div>
                                    </div>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="col form-group mb-3 d-grid gap-2 d-md-flex">
                                    <button class="btn btn-outline-primary">
                                        <span v-if="loading" class="spinner-border spinner-border-sm mr-1"></span>
                                        Save
                                    </button>
                                    <RouterLink class="btn btn-outline-secondary"
                                        to="/Hospital">Cancel</RouterLink>
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
                title: "Add Hospital",
                name_error:"",
                email_error:"",
                message_error:"",
                item:{
                    name:"",
                    email: "",
                    phone: "",
                    country: "",
                    address: "",
                    description: "",
                    id: 0,
                },
                apiUrl: `${enviroment.apiUrl}/Hospitals`
            }
        },
        methods:{
            validName(){
                if (!this.item.name)
                    this.name_error = "Name is required";
                else
                    this.name_error = "";
            },
            validEmail(){
                if (this.item.email && !validEmail(this.item.email))
                    this.email_error = "Email invalid";
                else
                    this.email_error = "";
            },
            async getItem(){
                var url = (enviroment.mongo_db) ? `${this.apiUrl}/${this.item.hospital_id_guid}` : `${this.apiUrl}/${this.item.id}`;
                return await getItemById(url);
            },
            async save(){
                this.validName();
                this.validEmail();
                if (!this.name_en_error && 
                    !this.email_error){
                    if (this.item.id == 0){
                        
                        await post(`${this.apiUrl}/Add`, this.item).then(response=>{
                            if (response.valid){
                                this.$router.push("/Hospital");
                            }
                            else
                                this.message_error = response.message;
                        });
                    }
                    else{
                        var url = (enviroment.mongo_db) ? `${this.apiUrl}/Edit/${this.item.hospital_id_guid}` : `${this.apiUrl}/Edit/${this.item.id}`;
                        await updateItem(url, this.item).then(response=>{
                            if (response.valid){
                                this.$router.push("/Hospital");
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
                if (enviroment.mongo_db){
                    this.item.hospital_id_guid = id;
                }
                else{
                    this.item.id = id;
                }
                this.title = "Edit Hospital";
                var data = await this.getItem();
                this.item = data.data;
            }
        }
    }
</script>