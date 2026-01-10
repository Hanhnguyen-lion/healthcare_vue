<template>
    <RouterLink class="btn btn-outline-primary" style="margin-left: 10px;" :to="routerLinkTo+id">{{ $t('buttons.edit') }}</RouterLink>
    <button class="btn btn-outline-danger" style="margin-left: 10px;" @click="remove(id)" type="button">
    <span v-if="loading" class="spinner-border spinner-border-sm mr-1"></span>
    {{ $t('buttons.delete') }}</button>
</template>
<script>
import { deleteItem } from '@/services/baseServices';
    export default{
        props: ['routerLinkTo', 'titleDialog', 'id', 'apiUrlDelete', 'items'],
        emits :{
          removeItem: null
        },
        data(){
            return {
                loading: false
            }
        },
        methods:{
            remove(id){
                this.loading = true;
                this.$confirm(
                {
                    title: this.titleDialog,
                    message: 'Are you sure to want delete this item?',
                    button: {
                        no: 'No',
                        yes: 'Yes'
                    },
                    callback: async confirm => {
                        if (confirm) {
                            var deleted = await deleteItem(`${this.apiUrlDelete}/Delete/${id}`);
                            if (deleted.valid){
                                this.loading = false;
                                var index = this.items.findIndex(p => p.id === id);

                                this.$emit('removeItem', index)
                            }
                        }
                        else{
                            this.loading = false;
                        }
                    }
                })
            }
        }
    }
</script>