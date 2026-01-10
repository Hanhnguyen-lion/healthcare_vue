<script setup>
    import FooterComponent from '../footer/FooterComponent.vue';
    import { enviroment } from '@/enviroments/enviroment';
    import { formatDateToString, isSupperAdmin } from '../helper/helper';
    import { getItems } from '@/services/baseServices';
import { useAuthStore } from '@/store/auth.module';
import AddButton from '../AddButton.vue';
import EditDeleteButtons from '../EditDeleteButtons.vue';
</script>

<template>
    <div class="container">
        <h2>Patient List</h2>
        <div class="form-group mb-3">
            <AddButton :title="$t('buttons.add')" router-link-to="/Patient/Add"></AddButton>
        </div>
        <div class="tableFixHead">
            <table class="table table-striped">
                <thead class="table-header">
                <tr>
                    <th>Code</th>
                    <th>First Name</th>
                    <th>Last Name</th>
                    <th>Date Of Birth</th>
                    <th>Gender</th>
                    <th>Hospital</th>
                    <th>Action</th>
                </tr>
                </thead>
                <tbody>
                    <tr v-for="item in patients" :key="item.id">
                        <td>{{item.code}}</td>
                        <td>{{item.first_name}}</td>
                        <td>{{item.last_name}}</td>
                        <td><div v-if="item.date_of_birth">{{formatDateToString(item.date_of_birth, 'DD/MM/YYYY')}}</div></td>
                        <td>{{item.gender}}</td>
                        <td>{{item.hospital_name}}</td>
                        <td>
                            <EditDeleteButtons 
                                :id="item.id" 
                                :apiUrlDelete="url"
                                :items="patients"
                                titleDialog="Delete Patient"
                                routerLinkTo="/Patient/Edit/"
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
            patients: [],
            url: `${enviroment.apiUrl}/Patients`
        }
    },
    methods: {
        handleItemRemoval(index){
            this.patients.splice(index, 1)
        }
    },
    async mounted() {
        var items = await getItems(this.url);
        if (items.valid){
            this.patients = items.data;

            if (!isSupperAdmin(this.auth.accountLogin)){
                var hospital_id = this.auth.accountLogin.hospital_id;
                this.patients = this.patients.filter(li => li.hospital_id == hospital_id);
            }
        }
    }
}
</script>
