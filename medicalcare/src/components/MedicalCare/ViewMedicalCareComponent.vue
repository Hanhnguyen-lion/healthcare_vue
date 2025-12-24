<script setup>
import { getItems, getItemsWithParam } from '@/services/baseServices';
import { pad, formatDateToString, isSupperAdmin } from '../helper/helper';
import { enviroment } from '@/enviroments/enviroment';
import { useAuthStore } from '@/store/auth.module';

</script>

<template>
<div class="container mt-3">
    <div class="row mt-3">
        <div class="card">
            <div class="card-header">
                <form name="form">
                    <div class="mb-3 row">
                        <label class="col-sm-4 col-form-label">Patient</label>
                        <div class="col-sm-8">
                            <select class="form-select" name="patient_id" v-model="input_data.patient_id">
                                <option v-for="item in patientItems" :key="item.patient_id" :value ="item.patient_id">
                                    {{item.patient_code}}
                                </option>                            
                            </select>
                        </div>
                    </div>
                    <div class="mb-3 row">
                        <label class="col-sm-4 col-form-label">Month of admission date</label>
                        <div class="col-sm-8">
                            <select class="form-select" name="admission_month" v-model="input_data.admission_month">
                                <option v-for="month in months" :key="month" :value ="month">
                                    {{pad(month)}}
                                </option>
                            </select>
                        </div>
                    </div>
                    <div class="mb-3 row">
                        <label class="col-sm-4 col-form-label">Year of admission date</label>
                        <div class="col-sm-8">
                            <select class="form-select" name="admission_year" v-model="input_data.admission_year">
                                <option v-for="year in years" :key="year" :value="year">
                                    {{year}}
                                </option>
                            </select>
                        </div>
                    </div>
                </form>
                    <div class="input-group mb-3">
                        <button class="btn btn-outline-success my-2 my-sm-0" type="button" @click="onSearch()">Search</button>
                    </div>
                    <div v-if="error_message" class="input-group mb-3">
                        {{ error_message }}
                    </div>
                    <div v-if="searchMedical" class="row mb-3">
                        <div class="col-md-12">
                            <div class="tableFixHead">
                                <div class="container">
                                    <h2>Hồ sơ bệnh án</h2>
                                    <div id="MedicalReports">
                                        <table class="my-double-bordered-table">
                                            <tbody>
                                            <tr>
                                                <td>Số thứ tự</td>
                                                <td class="borderleft" colspan="3">{{patientItem.patient_code}}</td>
                                                <td>&nbsp;</td>
                                                <td class="alnrleft borderleft-double">Số bảo hiểm</td>
                                                <td class="alncenter borderleft">{{patientItem.insurance_policy_number}}</td>
                                            </tr>
                                            <tr>
                                                <td colspan="5" class=" bordertop">&nbsp;</td>
                                                <td class="alnrleft borderleft-double bordertop">Hạn sử dụng bảo hiểm</td>
                                                <td class="alncenter borderleft bordertop">{{formatDateToString(patientItem.insurance_expire, 'DD/MM/YYYY')}}
                                                </td>
                                            </tr>
                                            <tr>
                                                <td rowspan="4" class="bordertop-double">Người bệnh</td>
                                                <td class="borderleft bordertop-double">Họ Tên</td>
                                                <td class="borderleft bordertop-double">{{patientItem.patient_name}}</td>
                                                <td colspan="2" class="bordertop-double">&nbsp;</td>
                                                <td class="alnrleft borderleft-double bordertop">Tên người có bảo hiểm</td>
                                                <td class="alncenter borderleft bordertop">{{patientItem.patient_name}}</td>
                                            </tr>
                                            <tr>
                                                <td class="borderleft bordertop">Ngày sinh</td>
                                                <td class="borderleft bordertop">{{formatDateToString(patientItem.date_of_birth, 'DD/MM/YYYY')}}</td>
                                                <td class="borderleft bordertop">Giới tính</td>
                                                <td class="borderleft bordertop">{{patientItem.gender}}</td>
                                                <td rowspan="2" class="alnrleft borderleft-double bordertop">Địa chỉ</td>
                                                <td rowspan="2" class="alncenter borderleft bordertop">{{patientItem.home_address}}</td>
                                            </tr>
                                            <tr>
                                                <td class="borderleft bordertop">Địa chỉ</td>
                                                <td class="borderleft bordertop">{{patientItem.home_address}}</td>
                                                <td class="bordertop" colspan="2">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="borderleft bordertop">Công việc</td>
                                                <td class="borderleft bordertop">{{patientItem.job}}</td>
                                                <td class="borderleft bordertop">Dạng bảo hiểm</td>
                                                <td class="borderleft bordertop">{{patientItem.insurance_type}}</td>
                                                <td class="alnrleft borderleft-double bordertop">Địa chỉ công ty</td>
                                                <td class="alncenter borderleft bordertop">{{patientItem.office_address}}</td>
                                            </tr>
                                            <tr class="borderleft bordertop-double">
                                                <td class="alncenter" colspan="4">Tên bệnh</td>
                                                <td class="alncenter borderleft">Bắt đầu</td>
                                                <td class="alncenter borderleft">Kết thúc</td>
                                                <td class="alncenter borderleft">Chuyển bệnh</td>
                                            </tr>
                                            <tr v-for="item in medicalCares" :key="item.id" class="borderleft bordertop">
                                                <td class="alncenter" colspan="4">{{item.diagnostic}}</td>
                                                <td class="alncenter borderleft">
                                                    {{formatDateToString(item.start_date, "DD/MM/YYYY")}}
                                                </td>
                                                <td class="alncenter borderleft">
                                                    {{formatDateToString(item.end_date, "DD/MM/YYYY")}}
                                                </td>
                                                <td class="alncenter borderleft"></td>
                                            </tr>
                                            <tr class="borderleft bordertop-double borderbottom">
                                                <td class="alncenter" colspan="4">Lịch sử bệnh - Quá trình chữa bệnh</td>
                                                <td class="alncenter borderleft" colspan="3">Đơn thuốc - Phẫu thuật - Trị liệu</td>
                                            </tr>
                                            <tr v-for="item in medicalCares" :key="item.id">
                                                <td class="borderright" colspan="4">
                                                    <table class="table">
                                                        <tbody v-for="treatmentItem in item.treatments" :key="treatmentItem.treatment_id">
                                                        <tr>
                                                            <td class="fw-bold alncenter">{{formatDateToString(treatmentItem.treatment_date, 'DD/MM/YYYY')}}</td>
                                                        </tr>
                                                        <tr>
                                                            <td class="alnleft">{{treatmentItem.treatment_type}}</td>
                                                        </tr>
                                                        <tr>
                                                            <td class="alnleft">{{treatmentItem.description}}</td>
                                                        </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                                <td colspan="3">
                                                    <table class="table">
                                                        <tbody v-for="prescriptionItem in item.prescriptions" :key="prescriptionItem.prescription_id">
                                                        <tr>
                                                            <td colspan="2" class="ps-3">Đơn thuốc</td>
                                                        </tr>
                                                        <tr>
                                                            <td class="ps-5">{{prescriptionItem.medicine_name}}</td>
                                                            <td>{{prescriptionItem.quantity}} {{prescriptionItem.medicine}}</td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2" class="ps-5">{{prescriptionItem.dosage}}</td>
                                                        </tr>
                                                    </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="form-group">
                                            <button class="btn btn-outline-success my-2 my-sm-0" type="button" @click="onPrint()">Print</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
            </div>
        </div>
    </div>
</div>
<footer></footer>
</template>
<style>
    .my-double-bordered-table {
      border: 3px double black;
    }
    .alnright{
        text-align: right;
    }
    .alncenter{
        text-align: center;
    }
    .alnleft{
        text-align: left;
    }
    .borderleft{
        border-left: 1px solid black;
    }
    .borderright{
        border-right: 1px solid black;
    }
    .borderleft-double{
        border-left: 3px double black;
    }
    .borderbottom{
        border-bottom: 1px solid black;
    }
    .bordertop{
        border-top: 1px solid black;
    }
    .bordertop-double{
        border-top: 3px double black;
    }

</style>
<script>
    export default{
        data(){
            return{
                auth: useAuthStore(),
                searchMedical: false,
                error_message: "",
                currentYear:new Date().getFullYear(),
                months:[1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12],
                input_data:{
                    patient_id:0,
                    admission_month:new Date().getMonth() + 1,
                    admission_year: new Date().getFullYear(),
                },
                years:[],
                patientItems:[],
                patientItem:[],
                medicalCares:[],
                searchItem:[]
            }
        },
        methods:{
            setYears(){
                this.years.push(this.currentYear);
                for(var i = 1; i <= 20; i++){
                    this.years.push(this.currentYear + i);
                    this.years.push(this.currentYear - i);
                }
                this.years.sort();
            },
            getPatients(){
                return getItems(`${enviroment.apiUrl}/MedicalCares/Patients`);
            },
            
            onSearch(){
                this.searchMedical = true;
                this.medicalCares = [];
                var url = `${enviroment.apiUrl}/MedicalCares/Search`

                getItemsWithParam(url, this.input_data).
                then(data=>{
                    if (data.valid){
                        var item = data.data;
                        this.patientItem = item;
                        var patient_id = item.patient_id;
                        if (patient_id){
                            this.medicalCares = item.medical;
                        }
                    }
                    else{
                        this.searchMedical = false;
                        this.error_message = data.message;
                    }
                });
            },
            onPrint() {
                var printContents = document.getElementById('MedicalReports')?.innerHTML;
                var popupWin = window.open('', '_blank', 'top=0,left=0,height=100%,width=auto');
                if (popupWin){
                popupWin.document.open();
                popupWin.document.write(`<html><head><link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" type="text/css"/>
                    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/scss/mixins/_utilities.scss" rel="stylesheet" type="text/css"/>
                    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/scss/_tables.scss" rel="stylesheet" type="text/css"/>
                    <style>
                        .my-double-bordered-table {
                        border: 3px double black;
                        }
                        .alnright{
                            text-align: right;
                        }
                        .alncenter{
                            text-align: center;
                        }
                        .alnleft{
                            text-align: left;
                        }
                        .borderleft{
                            border-left: 1px solid black;
                        }
                        .borderright{
                            border-right: 1px solid black;
                        }
                        .borderleft-double{
                            border-left: 3px double black;
                        }
                        .borderbottom{
                            border-bottom: 1px solid black;
                        }
                        .bordertop{
                            border-top: 1px solid black;
                        }
                        .bordertop-double{
                            border-top: 3px double black;
                        }              
                    </style> 
                    </head><body onload="window.print();window.close()">${printContents}</body></html>`);
                popupWin.document.title = 'Hồ sơ bệnh án';
                popupWin.document.close();
                }
            }             
        },
        mounted(){
            this.getPatients().then(data=>{
                if (data.valid){
                    this.patientItems = data.data;
                    if (!isSupperAdmin(this.auth.accountLogin)){
                        var hospital_id = this.auth.accountLogin.hospital_id || 0;
                        this.patientItems = this.patientItems.filter(li=>li.hospital_id == hospital_id);
                    }
                }
            });
            this.setYears();
        }
    }
</script>