<script setup>
    import { RouterLink } from 'vue-router';
    import { addItemToArray, deleteItemToArray, formatDateToString } from '../helper/helper';
    import { deleteItem, getItemById, getItems, updateItem } from '@/services/baseServices';
    import { enviroment } from '@/enviroments/enviroment';
    import treatmentModal from "@/components/modals/TreatmentModal";
    import prescriptionModal from "@/components/modals/PrescriptionModal";
    import numeral from 'numeral';
    import axios from 'axios';
    

</script>

<template>
<div class="container">
    <div class="row justify-content-center">
        <div class="col-md-6 col-md-10">
            <div class="card">
                <div class="card-header">
                    <h3>{{title}}</h3>
                    <form name="form">
                        <div class="row mb-3">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label class="fw-bold">Patient <span class="text-danger">*</span></label>
                                    <select class="form-select" name="patient_id" v-model="item.patient_id" placeholder="Enter Patient">
                                        <option v-for="item in patientItems" :key="item.id" :value="item.id">
                                            {{item.first_name}} {{item.last_name}}
                                        </option>
                                    </select>
                                    <div v-if="patient_id_error" class="invalid-feedback">
                                        <div>{{patient_id_error}}</div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">Doctor <span class="text-danger">*</span></label>
                                    <select class="form-select" name="doctor_id"
                                     v-model="item.doctor_id" placeholder="Enter doctor">
                                        <option v-for="item in doctorItems" :key="item.id" :value="item.id">
                                            {{item.first_name}} {{item.last_name}}
                                        </option>
                                    </select>
                                    <div v-if="doctor_id_error" class="invalid-feedback">
                                        <div>{{doctor_id_error}}</div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">Department <span class="text-danger">*</span></label>
                                    <select class="form-select" name="department_id" 
                                        v-model="item.department_id" placeholder="Enter department">
                                        <option v-for="item in departmentItems" :key="item.id" :value="item.id">
                                            {{item.name}}
                                        </option>
                                    </select>
                                    <div v-if="department_id_error" class="invalid-feedback">
                                        <div>{{department_id_error}}</div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">Admission Date <span class="text-danger">*</span></label>
                                    <input type="date" class="form-control" name="admission_date" 
                                        v-model="item.admission_date" placeholder="Enter admission date">
                                    <div v-if="admission_date_error" class="invalid-feedback">
                                        <div>{{admission_date_error}}</div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">Discharge Date <span class="text-danger">*</span></label>
                                    <input type="date" class="form-control" name="discharge_date" 
                                    v-model="item.discharge_date" placeholder="Enter discharge date">
                                    <div v-if="discharge_date_error" class="invalid-feedback">
                                        <div>{{discharge_date_error}}</div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label class="fw-bold">Diagnostic</label>
                                    <textarea rows="3" class="form-control" name="diagnostic" 
                                    v-model="item.diagnostic" placeholder="Enter diagnostic"></textarea>
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label class="fw-bold">Notes</label>
                                    <textarea rows="3" class="form-control" name="notes" 
                                        v-model="item.notes" placeholder="Enter Notes"></textarea>
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label class="fw-bold">Treatment:</label>
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-12">
                                <button class="btn btn-outline-primary" type="button" 
                                @click="addOrEditPrescription(0, 0, 'Treatment')">Add</button>
                            </div>
                        </div>
                        <transition name="treatmentModal">
                            <div class="modal-mask" v-if="showModal">
                                <treatmentModal :input_data="item" @close="closeModal"/>
                            </div>
                        </transition>
                        <div class="row mb-3">
                            <div class="col-md-12">
                                <div class="tableFixHead">
                                    <table class="table table-striped">
                                        <thead class="table-header">
                                            <tr>
                                                <th>#</th>
                                                <th>Date</th>
                                                <th>Type</th>
                                                <th>Quantity</th>
                                                <th>Amount</th>
                                                <th>Sub Total</th>
                                                <th>Action</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr v-for="(item, index) in treatmentItems" :key="item.id">
                                                <td>{{index + 1}}</td>
                                                <td>{{formatDateToString(item.treatment_date, 'DD/MM/YYYY')}}</td>
                                                <td>{{item.treatment_type}}</td>
                                                <td>{{item.quantity}}</td>
                                                <td>{{numeral(item.amount).format("0,0.00")}}</td>
                                                <td>{{numeral(item.total).format("0,0.00")}}</td>
                                                <td>
                                                    <button class="btn btn-outline-primary" style="margin-left: 10px;" type="button"
                                                    @click="addOrEditPrescription(item.id, item.new_id, 'Treatment')">Edit</button>
                                                    <button class="btn btn-outline-danger" style="margin-left: 10px;" type="button"
                                                    @click="deletePrescription(item.id, item.new_id, 'Treatment')">Delete</button>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-12">
                                <label class="fw-bold">Prescriptions:</label>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-12">
                                <button class="btn btn-outline-primary" type="button" @click="addOrEditPrescription(0, 0, 'Prescription')">Add</button>
                            </div>
                        </div>
                        <transition name="prescriptionModal">
                            <div class="modal-mask" v-if="showPrescriptionModal">
                                <prescriptionModal :input_data="item" @close="closePrescriptionModal"/>
                            </div>
                        </transition>
                        <div class="row mb-3">
                            <div class="col-md-12">
                                <div class="tableFixHead">
                                    <table class="table table-striped">
                                        <thead class="table-header">
                                            <tr>
                                                <th>#</th>
                                                <th>Date</th>
                                                <th>Medicine</th>
                                                <th>Quantity</th>
                                                <th>Amount</th>
                                                <th>Sub Total</th>
                                                <th>Action</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                                <tr v-for="(item, index) in prescriptionItems" :key="item.id">
                                                    <td>{{index + 1}}</td>
                                                    <td>{{formatDateToString(item.prescription_date, 'DD/MM/YYYY')}}</td>
                                                    <td>{{item.medicine_name}}</td>
                                                    <td>{{item.quantity}}</td>
                                                    <td>{{numeral(item.amount).format("0,0.00")}}</td>
                                                    <td>{{numeral(item.total).format("0,0.00")}}</td>
                                                    <td>
                                                        <button type="button" class="btn btn-outline-primary" style="margin-left: 10px;" @click="addOrEditPrescription(item.id, item.new_id, 'Prescription')">Edit</button>
                                                        <button type="button" class="btn btn-outline-danger" style="margin-left: 10px;" @click="deletePrescription(item.id, item.new_id, 'Prescription')">Delete</button>
                                                    </td>
                                                </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col form-group mb-3 d-grid gap-2 d-md-flex">
                                <button type="button" :disabled="loading" class="btn btn-outline-primary" @click="addOrEditItem">
                                    <span v-if="loading" class="spinner-border spinner-border-sm mr-1"></span>
                                        Save
                                </button>
                                <RouterLink to="/Billing" class="btn btn-outline-secondary">Cancel</RouterLink>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</div>
</template>

<script>
export default{
    
    components:[treatmentModal],

    data(){
        return {
            apiUrl:`${enviroment.apiUrl}/Billings`,
            billing_id:0,
            title : "",
            loading : false,
            patient_id_error:"",
            doctor_id_error: "",
            department_id_error: "",
            admission_date_error: "",
            discharge_date_error:"",
            showModal: false,
            showPrescriptionModal: false,
            item:{
                patient_id :"",
                doctor_id :"",
                department_id :"",
                diagnostic :"",
                notes :"",
                discharge_date :"",
                admission_date :""
            },
            prescriptionItemObs:{
				dosage:"",
				quantity:0,
            },
            treatmentItemObs:{
				category_id:0,
				description:"",
				quantity:0,
            },
            patientItems: [],
            doctorItems: [],
            departmentItems: [],
            treatmentItems:[],
            prescriptionItems:[]
        };
    },
    methods:{
        validPatient(){
            if (!this.item.patient_id)
                this.patient_id_error = "Patient is required";
            else{
                this.patient_id_error = "";
            }
        },
        validDoctor(){
            if (!this.item.doctor_id)
                this.doctor_id_error = "Doctor is required";
            else{
                this.doctor_id_error = "";
            }
        },
        validDepartment(){
            if (!this.item.department_id)
                this.department_id_error = "Department is required";
            else{
                this.department_id_error = "";
            }
        },
        validDischargeDate(){
            if (!this.item.discharge_date)
                this.discharge_date_error = "Discharge date is required";
            else{
                this.discharge_date_error = "";
            }
        },
        validAdmissionDate(){
            if (!this.item.admission_date)
                this.admission_date_error = "Admission date is required";
            else{
                this.admission_date_error = "";
            }
        },
        addOrEditItem(){
            this.validPatient();
            this.validDepartment();
            this.validDoctor();
            this.validAdmissionDate();
            this.validDischargeDate();
            if (!this.patient_id_error &&
                !this.doctor_id_error &&
                !this.department_id_error &&
                !this.admission_date_error &&
                !this.discharge_date_error
            ){
                if (this.billing_id > 0){
                    this.item.id = this.billing_id;
                    updateItem(this.apiUrl, this.item, this.billing_id);
                }
                else{
                    var url = `${this.apiUrl}/Add`;
                    axios.post(url, this.item).then(data=>{
                        var id = data.data;
                        url = `${this.apiUrl}/Add/Treatement/${id}`;
                        axios.post(url, this.treatmentItems).then(()=>{
                            url = `${this.apiUrl}/Add/Prescription/${id}`;
                            axios.post(url, this.prescriptionItems).then(()=>{
                            })
                        });
                    });
                }
                this.$router.push("/Billing");
            }
        },
        closeModal(data, saveData){
            this.showModal = false;
            if (saveData){
                if (this.billing_id > 0){
                    this.loadTreatments().then(data =>{
                        this.treatmentItems = data;
                    });
                }
                else{
                    this.treatmentItems = addItemToArray(this.treatmentItems, data, "new_id");
                }
            }
        },
        closePrescriptionModal(data, saveData){
            this.showPrescriptionModal = false;
            if (saveData){
                if (this.billing_id > 0){
                    this.loadPrescriptions().then(data =>{
                        this.prescriptionItems = data;
                    });
                }
                else{
                    this.prescriptionItems = addItemToArray(this.prescriptionItems, data, "new_id");
                }
            }
        },
        addOrEditPrescription(id, new_id, type){
            new_id = (new_id) ? new_id : 0;
            var treatmentItem = {};
            this.item.billing_id = this.billing_id;
            if (type == "Treatment"){
                treatmentItem = this.treatmentItems.find(item => item.new_id === new_id);
                this.item.treatment_id = id;
                this.item.new_treatment_id = new_id;
                this.item.treatmentItemObs = treatmentItem;
                this.showModal = true;
            }
            else{
                treatmentItem = this.prescriptionItems.find(item => item.new_id === new_id);
                this.item.prescription_id = id;
                this.item.new_prescription_id = new_id;
                this.item.prescriptionItemObs = treatmentItem;
                this.showPrescriptionModal = true;
            }
        },
        deletePrescription(id, new_id, type){
            new_id = (new_id) ? new_id: 0;
            var title = "Delete Prescription";
            var url = `${enviroment.apiUrl}/Prescriptions`;
            if (type == "Treatment"){
                url = `${enviroment.apiUrl}/Treatments`;
                title = "Delete Treatment";
            }
            this.$confirm(
            {
                title: title,
                message: 'Are you sure to want delete this item?',
                button: {
                    no: 'No',
                    yes: 'Yes'
                },
                callback: confirm => {
                    if (confirm) {
                        if (id > 0){
                            deleteItem(url, id).then(()=>{
                                if (type == "Treatment"){
                                    this.loadTreatments().then(data =>{
                                        this.treatmentItems = data;
                                    });
                                }
                                else{
                                    this.loadPrescriptions().then(data=>{
                                        this.prescriptionItems = data;
                                    });
                                }
                            });
                        }
                        else{
                            if (type == "Treatment"){
                                this.treatmentItems = deleteItemToArray(this.treatmentItems, new_id, "new_id");
                            }
                            else{
                                this.prescriptionItems = deleteItemToArray(this.prescriptionItems, new_id, "new_id");
                            }
                        }
                    }
                }
            });
        },
        loadTreatments(){
            var url = `${enviroment.apiUrl}/Treatments/items/${this.billing_id}`;
            return getItems(url);
        },
        loadPrescriptions(){
            var url = `${enviroment.apiUrl}/Prescriptions/items/${this.billing_id}`;
            return getItems(url);
        }
    },
    mounted(){
        this.billing_id = (this.$route.params.id) ? +this.$route.params.id:0;
        this.title = (this.billing_id == 0) ? "Add Billing": "Edit Billing";

        getItems(`${enviroment.apiUrl}/Patients`)
        .then(data =>{
            this.patientItems = data;
        });

        getItems(`${enviroment.apiUrl}/Doctors`)
        .then(data =>{
            this.doctorItems = data;
        });

        getItems(`${enviroment.apiUrl}/Departments`)
        .then(data =>{
            this.departmentItems = data;
        });

        if (this.billing_id > 0){
            getItemById(this.apiUrl, this.billing_id)
            .then(data =>{
                this.item = data;
                this.item.admission_date = formatDateToString(this.item.admission_date, "YYYY-MM-DD");
                this.item.discharge_date = formatDateToString(this.item.discharge_date, "YYYY-MM-DD");
            });
        }

        this.loadTreatments().then(data =>{
            this.treatmentItems = data;
        });

        this.loadPrescriptions()
        .then(data=>{
            this.prescriptionItems = data;
        });
    }
}
  
</script>
