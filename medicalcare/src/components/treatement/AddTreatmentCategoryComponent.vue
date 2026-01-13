<script setup>
import { getItemById, post, updateItem } from '@/services/baseServices';
import FooterComponent from '../footer/FooterComponent.vue';
import { enviroment } from '@/enviroments/enviroment';

</script>

<template>
    <div class="container">
        <div class="row justify-content-center">
            <div class="col-md-6 col-md-10">
                <div class="card">
                    <div class="card-header">
                        <h3>{{ title }}</h3>
                        <form name="form" @submit.prevent="save()">
                            <div class="row mb-3">
                                <div class="form-group">
                                    <label class="fw-bold">{{$t("commonText.name")}} EN <span class="text-danger">*</span></label>
                                    <input type="text" class="form-control" name="name_en" v-model="item.name_en"
                                        :placeholder="$t('category.treatment.enterNameEN')" />
                                    <div v-if="name_en_error" class="invalid-feedback">
                                        <div>{{ name_en_error }}</div>
                                    </div>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="form-group">
                                    <label class="fw-bold">{{$t("commonText.name")}} VN</label>
                                    <input type="text" class="form-control" name="name_vn" v-model="item.name_vn"
                                        :placeholder="$t('category.treatment.enterNameVN')" />
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="form-group">
                                    <label class="fw-bold">{{$t("commonText.name")}} JP</label>
                                    <input type="text" class="form-control" name="name_jp" v-model="item.name_jp"
                                        :placeholder="$t('category.treatment.enterNameJP')" />
                                </div>
                            </div>
                        <div class="row mb-3">
                                <div class="form-group">
                                    <label class="fw-bold">{{$t("category.treatment.price")}}</label>
                                    <input type="number" class="form-control" :min="0" name="price" v-model="item.price" :placeholder="$t('category.treatment.enterPrice')"/>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="form-group">
                                    <label class="fw-bold">{{$t("commonText.description")}}</label>
                                    <textarea rows="3" class="form-control" name="description" v-model="item.description"
                                        :placeholder="$t('commonText.enterDescription')" />
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="col form-group mb-3 d-grid gap-2 d-md-flex">
                                    <button :disabled="loading" class="btn btn-outline-primary">
                                        <span v-if="loading" class="spinner-border spinner-border-sm mr-1"></span>
                                        {{$t("buttons.save")}}
                                    </button>
                                    <RouterLink class="btn btn-outline-secondary"
                                        to="/Treatement/Category">{{$t("buttons.cancel")}}</RouterLink>
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
                edit_id: null,
                title: this.$t("category.treatment.addTreatmentCategory"),
                name_en_error:"",
                message_error:"",
                item:{
                    description:"",
                    name_jp: "",
                    name_vn: "",
                    name_en: "",
                    price: 0,
                    id: null,
                    id_guid: null
                },
                apiUrl: `${enviroment.apiUrl}/TreatmentsCategory`
            }
        },
        methods:{
            validName(){
                if (!this.item.name_en)
                    this.name_en_error = this.$t("messages.namelTreatementCategoryRequired");
                else
                    this.name_en_error = "";
            },
            async getItem(){
                return await getItemById(`${this.apiUrl}/${this.edit_id}`);
            },
            async save(){
                this.validName();
                if (!this.name_en_error){
                    this.loading = true;
                    var updated;
                    if (!this.edit_id)
                        updated = await post(`${this.apiUrl}/Add`, this.item);
                    else
                        updated = await updateItem(`${this.apiUrl}/Edit/${this.edit_id}`, this.item);
                    if (updated.valid)
                        this.$router.push("/Treatement/Category");
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
                this.title = this.$t("category.treatment.editTreatmentCategory");
                var data = await this.getItem();
                this.item = data.data;
            }
        }
    }
</script>