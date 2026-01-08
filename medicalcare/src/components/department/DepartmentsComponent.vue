<script setup>
    import FooterComponent from '../footer/FooterComponent.vue';
    import { enviroment } from '@/enviroments/enviroment';
    import { getItems, post } from '@/services/baseServices';
    import { useAuthStore } from '@/store/auth.module';
    import { isSupperAdmin } from '../helper/helper';
    import {utils, writeFile} from 'xlsx-js-style';
    import * as XLSX from 'xlsx';
    import AddButton from '../AddButton.vue';
    import EditDeleteButtons from '../EditDeleteButtons.vue';

</script>

<template>
    <div class="container">
        <h2>Department List</h2>
        <div class="form-group mb-3">
            <AddButton router-link-to="/Department/Add" title="Add Department"></AddButton>
            <button class="btn btn-outline-primary" 
                style="margin-left: 10px;" 
                @click="exportToExcel" type="button">Export Department</button>
            <input type="file" ref="fileInput" id="fileInput" class="form-control" style="display: none;" @change="handleFileUpload" accept=".xlsx, .xls" />
            <button class="btn btn-outline-primary" style="margin-left: 10px;"  
                @click="onImport" type="button">Import Department</button>
        </div>
        <div class="form-group mb-3">
            <div v-if="message_imported" class="form-group fw-bold">{{ message_imported }}</div>
        </div>
        <div class="tableFixHead">
            <table class="table table-striped">
                <thead class="table-header">
                <tr>
                    <th>#</th>
                    <th>Name</th>
                    <th>Phone</th>
                    <th>Hospital</th>
                    <th>Action</th>
                </tr>
                </thead>
                <tbody>
                    <tr v-for="(item, index) in items" :key="item.id">
                        <td>{{ index + 1}}</td>
                        <td>{{item.name}}</td>
                        <td>{{item.phone}}</td>
                        <td>{{item.hospital_name}}</td>
                        <td>
                            <EditDeleteButtons 
                                :id="item.id" 
                                :apiUrlDelete="url"
                                :items="items"
                                titleDialog="Delete Department"
                                routerLinkTo="/Department/Edit/"
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
            message_imported: "",
            items: [],
            jsonData: [],
            url: `${enviroment.apiUrl}/Departments`
        }
    },
    methods: {
        onImport(){
            this.jsonData = [];
            this.message_imported = "";
            this.$refs.fileInput.click();
        },
        async handleFileUpload(e){
            const file = e.target.files[0];
            if (!file)
                return;
            const reader = new FileReader();

            reader.onload = (e)=>{
                const wb = XLSX.read(e.target.result, {type: "binary"});
                const sheetName = wb.SheetNames[0];
                const ws = wb.Sheets[sheetName];
                this.jsonData = XLSX.utils.sheet_to_json(ws, {header: 2});
                //console.log(this.jsonData);
                if (this.jsonData){
                    this.importToExcel();
                }
            };
            reader.readAsArrayBuffer(file);
        },
        exportToExcel(){
            var xlsx_data = [];
            for (let index = 0; index < this.items.length; index++) {
                const element = this.items[index];
                xlsx_data.push(
                    {
                        id: element.id,
                        name: element.name,
                        phone: element.phone,
                        hospital_name: element.hospital_name
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

            const wb = utils.book_new()
            utils.book_append_sheet(wb, ws, "departments");

            writeFile(wb, "departments.xlsx");
        },
        async importToExcel(){
            var imported = await post(`${this.url}/Import`, this.jsonData);
            if (imported.valid){
                this.jsonData = [];
                this.message_imported = "Imported success";
                var data = await getItems(this.url);
                if (data.valid){
                    this.items = data.data;
                    if (!isSupperAdmin(this.auth.accountLogin)){
                        var hospital_id = this.auth.accountLogin.hospital_id;
                        this.items = this.items.filter(li => li.hospital_id == hospital_id);
                    }
                }
            }
            else{
                this.jsonData = [];
                this.message_imported = imported.message;
            }
        },
        handleItemRemoval(index){
            this.items.splice(index, 1)
        }
    },
    async mounted() {
        var data = await getItems(this.url);
        if (data.valid){
            this.items = data.data;
            if (!isSupperAdmin(this.auth.accountLogin)){
                var hospital_id = this.auth.accountLogin.hospital_id;
                this.items = this.items.filter(li => li.hospital_id == hospital_id);
            }
        }
    }
}

</script>
