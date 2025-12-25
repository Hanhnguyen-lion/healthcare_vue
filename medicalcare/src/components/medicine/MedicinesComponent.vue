<script setup>
    import FooterComponent from '../footer/FooterComponent.vue';
    import { enviroment } from '@/enviroments/enviroment';
    import { deleteItem, getItems } from '@/services/baseServices';
    import numeral from 'numeral';

    import {utils, writeFile} from 'xlsx-js-style';

</script>

<template>
    <div class="container">
        <h2>Medicine List</h2>
        <div class="form-group mb-3">
            <RouterLink to="/Medicine/Add" class="btn btn-outline-primary" >Add Medicine</RouterLink>
            <button class="btn btn-outline-primary" 
                style="margin-left: 10px;" 
                @click="exportToExcel()" type="button">Export to Excel</button>
        </div>
        <div class="tableFixHead">
            <table class="table table-striped">
                <thead class="table-header">
                <tr>
                    <th>#</th>
                    <th>Name</th>
                    <th>Type</th>
                    <th>Price</th>
                    <th>Action</th>
                </tr>
                </thead>
                <tbody>
                    <tr v-for="(item, index) in items" :key="item.id">
                        <td>{{ index + 1}}</td>
                        <td>{{item.name}}</td>
                        <td>{{item.medicine_type}}</td>
                        <td>{{numeral(item.price).format("0,0.00")}}</td>
                        <td >
                            <RouterLink class="btn btn-outline-primary" 
                            :to="'/Medicine/Edit/' + item.id"
                            style="margin-left: 10px;" 
                            >Edit</RouterLink>
                            <button class="btn btn-outline-danger" 
                                style="margin-left: 10px;" 
                                @click="remove(item.id)" type="button">
                                <span v-if="loading" class="spinner-border spinner-border-sm mr-1"></span>
                                Delete
                            </button>
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
            loading: false,
            items: [],
            url: `${enviroment.apiUrl}/Medicines`
        }
    },
    methods: {
        exportToExcel(){
            var xlsx_data = [];
            for (let index = 0; index < this.items.length; index++) {
                const element = this.items[index];
                xlsx_data.push(
                    {
                        id: element.id,
                        Type: element.medicine_type,
                        Name: element.name,
                        Price: element.price
                    }
                );
            }    

            const headerStyle = {
                font: { bold: true }
            };

            const ws = utils.json_to_sheet(xlsx_data);
            
            // bold header
            const range = utils.decode_range(ws['!ref']);
            for (let col = range.s.c; col <= range.e.c; col++) {
                const cellAddress = utils.encode_cell({ r: range.s.r, c: col });
                ws[cellAddress].s = headerStyle;
            }
            //format number    
            for (let index = range.s.r + 1; index <= range.e.r; index++) {
                const cellAddress = utils.encode_cell({
                    r: index, c:3
                });
                const cell = ws[cellAddress];
                if (cell && cell.t === 'n'){
                    cell.s = {
                        numFmt: "#,##0.00"};
                }
            }

            const wb = utils.book_new()
            utils.book_append_sheet(wb, ws, "medicines");

            writeFile(wb, "medicine.xlsx");
        },
        remove(id){
            this.loading = true;
            this.$confirm(
            {
                title: 'Delete Medicine',
                message: 'Are you sure to want delete this item?',
                button: {
                    no: 'No',
                    yes: 'Yes'
                },
                callback: async confirm => {
                    if (confirm) {
                        var apiUrl = `${this.url}/Delete/${id}`;
                        var response = await deleteItem(apiUrl);
                        if (response.valid){
                            this.loading = false;
                            const index = this.items.findIndex(p => p.id === id);
                            this.items.splice(index, 1)
                        }
                    }
                }
            });
        }
    },
    async mounted() {
        
        var data = await getItems(this.url);
        if (data.valid)
            this.items = data.data;
    }
}

</script>
