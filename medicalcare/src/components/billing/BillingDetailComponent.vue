<script setup>
    import { enviroment } from '@/enviroments/enviroment';
    import { formatDateToString } from '../helper/helper';
    import { getItemById } from '@/services/baseServices';
    var numberal = require('numeral');
</script>

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
<template>
<div class="container mt-3">
    <div class="row mt-3">
        <div class="card">
            <div class="card-header">
                <div class="row mb-3">
                    <div class="col-md-12">
                        <div class="tableFixHead">
                            <div class="container">
                                <h2>{{$t('billing.billingDetail.title')}}</h2>
                                <div id="MedicalReports">
                                    <table class="my-double-bordered-table">
                                        <tbody>
                                        <tr>
                                            <td>{{$t('medicaleCare.order')}}</td>
                                            <td class="borderleft" colspan="3">{{patientItem.patient_code}}</td>
                                            <td>&nbsp;</td>
                                            <td class="alnrleft borderleft-double">{{$t('medicaleCare.insuranceNumber')}}</td>
                                            <td class="alncenter borderleft">{{patientItem.insurance_policy_number}}</td>
                                        </tr>
                                        <tr>
                                            <td colspan="5" class=" bordertop">&nbsp;</td>
                                            <td class="alnrleft borderleft-double bordertop">{{$t('medicaleCare.insuranceValidDate')}}</td>
                                            <td class="alncenter borderleft bordertop">{{formatDateToString(patientItem.insurance_expire, 'DD/MM/YYYY')}}
                                            </td>
                                        </tr>
                                        <tr>
                                            <td rowspan="4" class="bordertop-double">{{$t('commonText.patient')}}</td>
                                            <td class="borderleft bordertop-double">{{$t('medicaleCare.fullName')}}</td>
                                            <td class="borderleft bordertop-double">{{patientItem.patient_name}}
                                            </td>
                                            <td colspan="2" class="bordertop-double">&nbsp;</td>
                                            <td class="alnrleft borderleft-double bordertop">{{$t('medicaleCare.nameOfInsurance')}}</td>
                                            <td class="alncenter borderleft bordertop">{{patientItem.patient_name}}
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="borderleft bordertop">{{$t('medicaleCare.dob')}}</td>
                                            <td class="borderleft bordertop">{{formatDateToString(patientItem.date_of_birth, 'DD/MM/YYYY')}}
                                            </td>
                                            <td class="borderleft bordertop">{{$t('commonText.gender')}}</td>
                                            <td class="borderleft bordertop">{{patientItem.gender}}
                                            </td>
                                            <td rowspan="2" class="alnrleft borderleft-double bordertop">{{$t('medicaleCare.address')}}</td>
                                            <td rowspan="2" class="alncenter borderleft bordertop">{{patientItem.home_address}}
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="borderleft bordertop">{{$t('medicaleCare.address')}}</td>
                                            <td class="borderleft bordertop">{{patientItem.home_address}}
                                            </td>
                                            <td class="bordertop" colspan="2">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="borderleft bordertop">{{$t('medicaleCare.job')}}</td>
                                            <td class="borderleft bordertop">{{patientItem?.job??""}}
                                            </td>
                                            <td class="borderleft bordertop">{{$t('medicaleCare.insuranceType')}}</td>
                                            <td class="borderleft bordertop">{{patientItem.insurance_type}}
                                            </td>
                                            <td class="alnrleft borderleft-double bordertop">{{$t('medicaleCare.workAddress')}}</td>
                                            <td class="alncenter borderleft bordertop">{{patientItem.office_address}}
                                            </td>
                                        </tr>
                                        <tr class="borderleft bordertop-double">
                                            <td class="alncenter" colspan="4">{{$t('medicaleCare.treatmentName')}}</td>
                                            <td class="alncenter borderleft">{{$t('medicaleCare.begin')}}</td>
                                            <td class="alncenter borderleft">{{$t('medicaleCare.end')}}</td>
                                            <td class="alncenter borderleft">{{$t('medicaleCare.diseaseTransfer')}}</td>
                                        </tr>
                                        <tr class="borderleft bordertop-double">
                                            <td colspan="7">
                                                <table class="table">
                                                    <tbody>
                                                    <tr>
                                                        <td>{{$t('billing.addBilling.admissionDate')}}: 
                                                            {{formatDateToString(billingItem.admission_date, "DD/MM/YYYY")}}
                                                        </td>
                                                        <td>{{$t('billing.addBilling.dischargeDate')}}: 
                                                            {{formatDateToString(billingItem.discharge_date, "DD/MM/YYYY")}}
                                                        </td>
                                                    </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr class="borderleft fw-bold">
                                            <td colspan="7">
                                                <div class="row mb-3">
                                                    <div class="col-md-12">{{$t('menu.treatment')}}
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>                                            
                                        <tr class="borderleft bordertop-double">
                                            <td class="alncenter" colspan="7">
                                                <div class="row mb-3">
                                                    <div class="col-md-12">
                                                        <div class="tableFixHead">
                                                            <table class="table table-striped">
                                                                <thead class="table-header">
                                                                    <tr>
                                                                        <th>#</th>
                                                                        <th>{{$t('billing.addBilling.date')}}</th>
                                                                        <th>{{$t('billing.addBilling.type')}}</th>
                                                                        <th>{{$t('billing.addBilling.quantity')}}</th>
                                                                        <th>{{$t('billing.addBilling.amount')}}</th>
                                                                        <th>{{$t('billing.addBilling.subTotal')}}</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr v-for="(item, index) in treatmemtItems" :key="item.id">
                                                                        <td>{{index + 1}}</td>
                                                                        <td>{{formatDateToString(item.treatment_date, "DD/MM/YYYY")}}</td>
                                                                        <td>{{item.treatment_type}}</td>
                                                                        <td>{{item.quantity}}</td>
                                                                        <td>{{numberal(item.amount).format("0,0.00")}}</td>
                                                                        <td>{{numberal(item.total).format("0,0.00")}}</td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr class="borderleft fw-bold">
                                            <td colspan="7">
                                                <div class="row mb-3">
                                                    <div class="col-md-12">{{$t('medicaleCare.prescription')}}
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>                                            
                                        <tr class="borderleft bordertop-double">
                                            <td class="alncenter" colspan="7">
                                                <div class="row mb-3">
                                                    <div class="col-md-12">
                                                        <div class="tableFixHead">
                                                            <table class="table table-striped">
                                                                <thead class="table-header">
                                                                    <tr>
                                                                        <th>#</th>
                                                                        <th>{{$t('billing.addBilling.date')}}</th>
                                                                        <th>{{$t('menu.medicine')}}</th>
                                                                        <th>{{$t('billing.addBilling.quantity')}}</th>
                                                                        <th>{{$t('billing.addBilling.amount')}}</th>
                                                                        <th>{{$t('billing.addBilling.subTotal')}}</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                        <tr v-for="(item, index) in prescriptionItems" :key="item.id">
                                                                            <td>{{index + 1}}</td>
                                                                            <td>{{formatDateToString(item.prescription_date, "DD/MM/YYYY")}}</td>
                                                                            <td>{{item.medicine_name}}</td>
                                                                            <td>{{item.quantity}}</td>
                                                                            <td>{{numberal(item.amount).format("0,0.00")}}</td>
                                                                            <td>{{numberal(item.total).format("0,0.00")}}</td>
                                                                        </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>                                            
                                        <tr class="borderleft fw-bold">
                                            <td colspan="7">
                                                <div class="row mb-3">
                                                    <div class="col-md-12">{{$t('menu.appointment')}}
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>                                            
                                        <tr v-if="appoitmentItems && appoitmentItems.length > 0" class="borderleft bordertop-double">
                                            <td class="alncenter" colspan="7">
                                                <div class="row mb-3">
                                                    <div class="col-md-12">
                                                        <div class="tableFixHead">
                                                            <table class="table table-striped">
                                                                <thead class="table-header">
                                                                    <tr>
                                                                        <th>#</th>
                                                                        <th>{{$t('billing.appointment.date')}}</th>
                                                                        <th>{{$t('commonText.patient')}}</th>
                                                                        <th>{{$t('commonText.doctor')}}</th>
                                                                        <th>{{$t('billing.appointment.reason')}}</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                        <tr v-for="(item, index) in appoitmentItems" :key="item.id">
                                                                            <td>{{index + 1}}</td>
                                                                            <td>{{formatDateToString(item.appointment_date, "DD/MM/YYYY")}}</td>
                                                                            <td>{{item.patient_name}}</td>
                                                                            <td>{{item.doctor_name}}</td>
                                                                            <td>{{item.reason_to_visit}}</td>
                                                                        </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>                                            
                                        <tr class="borderleft bordertop-double">
                                            <td class="alnright" colspan="7">
                                                <div class="row mb-3">
                                                    <div class="col-md-12">
                                                        <div class="tableFixHead">
                                                            <table class="table table-striped">
                                                                <thead class="table-header">
                                                                    <tr>
                                                                        <th colspan="5">{{$t('billing.billingDetail.total')}}</th>
                                                                        <th>
                                                                            {{numberal(billingItem.amount).format("0,0.00")}}
                                                                        </th>
                                                                    </tr>
                                                                </thead>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>                                            
                                    </tbody>
                                    </table>
                                
                                </div>
                                <div class="row mt-3">
                                    <div class="form-group">
                                        <button class="btn btn-outline-success my-2 my-sm-0" type="button" @click="onPrint()">{{$t('buttons.print')}}</button>
                                        <button class="btn btn-outline-success my-2 my-sm-0" type="button" @click="onBillingList()">{{$t('billing.billingDetail.billingList')}}</button>
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
</template>

<script>


export default{

    data(){
        return {
            apiUrl: `${enviroment.apiUrl}/Billings/BillingDetail`,
            patientItem: {
                patient_code: "",
                insurance_policy_number: "",
                insurance_expire: "",
                patient_name: "",
                date_of_birth: "",
                gender: "",
                home_address: "",
                job: "",
                insurance_type: "",
                office_address: ""
            },
            billingItem: {
                discharge_date: "",
                admission_date: "",
                amount:""
            },
            treatmemtItems: [],
            prescriptionItems: [],
            appoitmentItems: []
        };
    },
    methods:{
        getBillingDetail(billing_id){
            var url = `${this.apiUrl}/${billing_id}`;
            getItemById(url)
            .then(data =>{
                if (data.valid){
                    this.patientItem = data.data;
                    var patient_id = this.patientItem.patient_id;
                    if (patient_id){
                        this.prescriptionItems = this.patientItem.prescriptions;
                        this.treatmemtItems = this.patientItem.treatments;
                        this.appoitmentItems = this.patientItem.appointments;
                        console.log(this.patientItem.billing);
                        this.billingItem = (this.patientItem.billing) ? this.patientItem.billing[0]:null;
                        console.log("this.billingItem:", this.billingItem);
                    }
                }
            });
        },
        onPrint(){
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
                popupWin.document.title = 'Billing Detail';
                popupWin.document.close();
            }
        },
        onBillingList(){
            this.$router.push("/Billing");
        }
    },
    mounted(){
      var billing_id = this.$route.params.id||null;
      this.getBillingDetail(billing_id);
    }
}    
</script>
