<script setup>
import { getItems, post } from '@/services/baseServices';
import FooterComponent from '../footer/FooterComponent.vue';
import { enviroment } from '@/enviroments/enviroment';
import AddButton from '../AddButton.vue';
import EditDeleteButtons from '../EditDeleteButtons.vue';
import * as XLSX from 'xlsx';
import { utils, writeFile } from 'xlsx-js-style';

</script>

<template>
    <div class="container">
        <h2>Medicine Category List</h2>
        <div class="form-group mb-3">
            <AddButton :title="$t('buttons.add')" router-link-to="/Medicine/Category/Add"></AddButton>
            <button class="btn btn-outline-primary" 
                style="margin-left: 10px;" 
                @click="exportToExcel" type="button">{{$t('buttons.export')}}</button>
            <input type="file" ref="fileInput" id="fileInput" class="form-control" style="display: none;" @change="handleFileUpload" accept=".xlsx, .xls" />
            <button class="btn btn-outline-primary" style="margin-left: 10px;"  
                @click="onImport" type="button">{{$t('buttons.import')}}</button>
        </div>
        <div class="form-group mb-3">
            <div v-if="message_imported" class="form-group fw-bold">{{ message_imported }}</div>
        </div>
        <div class="tableFixHead">
            <table class="table table-striped">
                <thead class="table-header">
                <tr>
                    <th>Name EN</th>
                    <th>Name VN</th>
                    <th>Name JP</th>
                    <th>Action</th>
                </tr>
                </thead>
                <tbody>
                <tr v-for="item in data" :key="item.id">
                    <td>{{item.name_en}}</td>
                    <td>{{item.name_vn}}</td>
                    <td>{{item.name_jp}}</td>
                    <td>
                        <EditDeleteButtons 
                            :id="item.id" 
                            :apiUrlDelete="apiUrl"
                            :items="data"
                            titleDialog="Delete Medicine Category"
                            routerLinkTo="/Medicine/Category/Edit/"
                            @removeItem="handleItemRemoval">
                        </EditDeleteButtons>
                    </td>
                </tr>
                </tbody>
            </table>
        </div>
    </div>
    <RouterView></RouterView>
    <FooterComponent></FooterComponent>
</template>
<script>
    export default{
        data:()=>{
            return {
                data: [],
                jsonData:[],
                message_imported: "",
                apiUrl:`${enviroment.apiUrl}/MedicinesCategory`
            };
        },
        methods:{
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
                for (let index = 0; index < this.data.length; index++) {
                    const element = this.data[index];
                    xlsx_data.push(
                        {
                            id: element.id,
                            name_en: element.name_en,
                            name_vn: element.name_vn,
                            name_jp: element.name_jp,
                            description: element.description
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
                utils.book_append_sheet(wb, ws, "medicinesCategory");

                writeFile(wb, "medicineCategory.xlsx");
            },
            async importToExcel(){
                var imported = await post(`${this.apiUrl}/Import`, this.jsonData);
                if (imported.valid){
                    this.jsonData = [];
                    this.message_imported = "Imported success";
                    var items = await getItems(this.apiUrl);
                    if (items.valid)
                        this.data = items.data;
                }
                else{
                    this.jsonData = [];
                    this.message_imported = imported.message;
                }
            },
            handleItemRemoval(index){
                this.data.splice(index, 1)
            },
        },
        async mounted(){
            var response = await getItems(this.apiUrl);
            if (response.valid){
                this.data = response.data;
            }
        }
    }
</script>