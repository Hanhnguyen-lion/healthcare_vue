<script setup>
    import FooterComponent from '../footer/FooterComponent.vue';
    import { enviroment } from '@/enviroments/enviroment';
    import { getItems } from '@/services/baseServices';
import { useAuthStore } from '@/store/auth.module';
import AddButton from '../AddButton.vue';
import EditDeleteButtons from '../EditDeleteButtons.vue';
import { formatDateToString } from '../helper/helper';

</script>

<template>
    <div class="container">
        <h2>{{ $t('account.accounts.title') }}</h2>
        <div class="form-group mb-3">
            <AddButton :title="$t('buttons.add')" router-link-to="/Account/Register"></AddButton>
        </div>
        <div class="tableFixHead">
            <table class="table table-striped">
                <thead class="table-header">
                <tr>
                    <th>#</th>
                    <th>{{ $t('commonText.firstName') }}</th>
                    <th>{{ $t('commonText.lastName') }}</th>
                    <th>{{ $t('commonText.phone') }}</th>
                    <th>{{ $t('commonText.email') }}</th>
                    <th>{{ $t('commonText.gender') }}</th>
                    <th>{{ $t('account.accounts.dob') }}</th>
                    <th>{{ $t('account.accounts.role') }}</th>
                    <th>{{ $t('account.accounts.accountType') }}</th>
                    <th>{{ $t('account.accounts.createDate') }}</th>
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
                        <td>{{formatDateToString(item.dob, 'DD/MM/YYYY')}}</td>
                        <td>{{item.role}}</td>
                        <td>{{ item.account_type }}</td>
                        <td>{{formatDateToString(item.create_date, 'DD/MM/YYYY')}}</td>
                        <td>
                            <EditDeleteButtons 
                                :id="item.id" 
                                :apiUrlDelete="url"
                                :items="items"
                                :titleDialog="$t('messages.deleteAccount')"
                                routerLinkTo="/Account/Register/Edit/"
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
            url: `${enviroment.apiUrl}/Accounts`
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
    }
}

</script>
