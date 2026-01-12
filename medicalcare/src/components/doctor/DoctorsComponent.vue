<script setup>
    import FooterComponent from '../footer/FooterComponent.vue';
    import { enviroment } from '@/enviroments/enviroment';
    import { getItems } from '@/services/baseServices';
import { useAuthStore } from '@/store/auth.module';
import { isSupperAdmin } from '../helper/helper';
import AddButton from '../AddButton.vue';
import EditDeleteButtons from '../EditDeleteButtons.vue';

</script>

<template>
    <div class="container">
        <h2>{{$t('doctor.doctors.title')}}</h2>
        <div class="form-group mb-3">
            <AddButton :title="$t('buttons.add')" router-link-to="/Doctor/Add"></AddButton>
        </div>
        <div class="tableFixHead">
            <table class="table table-striped">
                <thead class="table-header">
                <tr>
                    <th>#</th>
                    <th>{{$t('commonText.firstName')}}</th>
                    <th>{{$t('commonText.lastName')}}</th>
                    <th>{{$t('commonText.phone')}}</th>
                    <th>{{$t('commonText.email')}}</th>
                    <th>{{$t('commonText.gender')}}</th>
                    <th>{{$t('commonText.hospital')}}</th>
                    <th></th>
                </tr>
                </thead>
                <tbody>
                    <tr v-for="(item, index) in items" :key="item.id">
                        <td>{{ index + 1}}</td>
                        <td>{{item.first_name}}</td>
                        <td>{{item.last_name}}</td>
                        <td>{{item.phone}}</td>
                        <td>{{item.email}}</td>
                        <td>{{item.gender}}</td>
                        <td>{{item.hospital_name}}</td>
                        <td>
                            <EditDeleteButtons 
                                :id="item.id" 
                                :apiUrlDelete="url"
                                :items="items"
                                titleDialog="Delete Doctor"
                                routerLinkTo="/Doctor/Edit/"
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
            url: `${enviroment.apiUrl}/Doctors`
        }
    },
    methods: {
        handleItemRemoval(index){
            this.items.splice(index, 1)
        }
    },
    async mounted() {
        var data = await getItems(this.url)
        this.items = data.data;
            
        if (!isSupperAdmin(this.auth.accountLogin)){
            var hospital_id = this.auth.accountLogin.hospital_id;
            this.items = this.items.filter(li => li.hospital_id == hospital_id);
        }
    }
}

</script>
