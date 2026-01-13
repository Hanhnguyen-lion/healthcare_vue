<script setup>
import { getItemById, post, updateItem } from '@/services/baseServices';
import FooterComponent from '../footer/FooterComponent.vue';
import { enviroment } from '@/enviroments/enviroment';
import { validEmail } from '../helper/helper';

</script>

<template>
    <div class="container">
        <div class="row justify-content-center">
            <div class="col-md-6 col-md-12">
                <div class="card">
                    <div class="card-header">
                        <h3>{{ title }}</h3>
                        <form name="form" @submit.prevent="save()">
                            <div class="row mb-3">
                                <div class="form-group">
                                    <label class="fw-bold">{{$t('commonText.name')}} <span class="text-danger">*</span></label>
                                    <input type="text" class="form-control" name="name" v-model="item.name"
                                        :placeholder="$t('commonText.enterName')" />
                                    <div v-if="name_error" class="invalid-feedback">
                                        <div>{{ name_error }}</div>
                                    </div>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="form-group">
                                    <label class="fw-bold">{{$t('commonText.phone')}}</label>
                                    <input type="text" class="form-control" name="phone" v-model="item.phone"
                                        :placeholder="$t('commonText.enterPhone')" />
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="form-group">
                                    <label class="fw-bold">{{$t('hospital.hospitals.country')}}</label>
                                    <input type="text" class="form-control" name="country" v-model="item.country"
                                        :placeholder="$t('hospital.addHospital.enterCountry')" />
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="form-group">
                                    <label class="fw-bold">{{$t('hospital.addHospital.address')}}</label>
                                    <input type="text" class="form-control" name="address" v-model="item.address"
                                        :placeholder="$t('hospital.addHospital.enterAddress')" />
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="form-group">
                                    <label class="fw-bold">{{$t('commonText.email')}}</label>
                                    <input type="text" class="form-control" name="email" v-model="item.email"
                                        :placeholder="$t('commonText.email')" />
                                    <div v-if="email_error" class="invalid-feedback">
                                        <div>{{ email_error }}</div>
                                    </div>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="col form-group mb-3 d-grid gap-2 d-md-flex">
                                    <button class="btn btn-outline-primary">
                                        <span v-if="loading" class="spinner-border spinner-border-sm mr-1"></span>
                                        {{$t('buttons.save')}}
                                    </button>
                                    <RouterLink class="btn btn-outline-secondary"
                                        to="/Hospital">{{$t('buttons.cancel')}}</RouterLink>
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
                title: this.$t('hospital.addHospital.addHospital'),
                name_error:"",
                email_error:"",
                message_error:"",
                edit_id:null,
                item:{
                    name:"",
                    email: "",
                    phone: "",
                    country: "",
                    address: "",
                    description: "",
                    id: null,
                    id_guid: null
                },
                apiUrl: `${enviroment.apiUrl}/Hospitals`
            }
        },
        methods:{
            validName(){
                if (!this.item.name)
                    this.name_error = this.$t('messages.namelHospitalRequired');
                else
                    this.name_error = "";
            },
            validEmail(){
                if (this.item.email && !validEmail(this.item.email))
                    this.email_error = this.$t('messages.emailInvalid');
                else
                    this.email_error = "";
            },
            async getItem(){
                return await getItemById(`${this.apiUrl}/${this.edit_id}`);
            },
            async save(){
                this.validName();
                this.validEmail();
                if (!this.name_error && 
                    !this.email_error){
                    this.loading = true;
                    var updated;
                    if (!this.edit_id)
                        updated = await post(`${this.apiUrl}/Add`, this.item);
                    else
                        updated = await updateItem(`${this.apiUrl}/Edit/${this.edit_id}`, this.item);
                    
                    if (updated.valid)
                        this.$router.push("/Hospital");
                    else{
                        this.loading = false;
                        this.message_error = updated.message;
                    }
                }
            }
        },
        async mounted(){
            var id = this.$route.params["id"];
            if (id){
                this.edit_id = id;
                this.title = this.$t('hospital.addHospital.editHospital');
                var data = await this.getItem();
                this.item = data.data;
            }
        }
    }
</script>