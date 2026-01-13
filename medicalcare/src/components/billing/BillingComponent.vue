<script setup>
    import FooterComponent from '../footer/FooterComponent.vue';
    import { enviroment } from '@/enviroments/enviroment';
    import { formatDateToString, isSupperAdmin } from '../helper/helper';
    import { getItems } from '@/services/baseServices';
    import { useAuthStore } from '@/store/auth.module';
import AddButton from '../AddButton.vue';
import EditDeleteButtons from '../EditDeleteButtons.vue';
    var numberal = require("numeral");

</script>

<template>
    <div class="container">
        <h2>{{$t('billing.billingList.title')}}</h2>
        <div class="form-group mb-3">
            <AddButton router-link-to="/Billing/Add" :title="$t('buttons.add')"></AddButton>
        </div>
        <div class="tableFixHead">
            <table class="table table-striped">
                <thead class="table-header">
                <tr>
                    <th>#</th>
                    <th>{{$t('billing.billingList.billingDate')}}</th>
                    <th>{{$t('commonText.patient')}}</th>
                    <th>{{$t('billing.billingList.total')}}</th>
                    <th></th>
                </tr>
                </thead>
                <tbody>
                    <tr v-for="(item, index) in items" :key="item.billing_id">
                        <td>{{ index + 1}}</td>
                        <td>{{formatDateToString(item.billing_date, 'DD/MM/YYYY')}}</td>
                        <td>{{item.patient_name}}</td>
                        <td>{{numberal(item.total).format("0,0.00")}}</td>
                        <td>
                            <RouterLink class="btn btn-outline-info" 
                            style="margin-left: 10px;" 
                            :to="'/Billing/View/' + item.billing_id"
                            >{{$t('buttons.view')}}</RouterLink>
                            <EditDeleteButtons 
                                :id="item.billing_id" 
                                :apiUrlDelete="url"
                                :items="items"
                                :titleDialog="this.$t('messages.deleteBilling')"
                                routerLinkTo="/Billing/Edit/"
                                @removeItem="handleItemRemoval">
                            </EditDeleteButtons>
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
            auth: useAuthStore(),
            items: [],
            url: `${enviroment.apiUrl}/Billings`
        }
    },
    methods: {
        handleItemRemoval(index){
            this.items.splice(index, 1)
        }
    },
    async mounted() {
        var data = await getItems(this.url);
        this.items = data.data;

        if (!isSupperAdmin(this.auth.accountLogin)){
            var hospital_id = this.auth.accountLogin.hospital_id;
            this.items = this.items.filter(li => li.hospital_id == hospital_id);
        }
    }
}

</script>
