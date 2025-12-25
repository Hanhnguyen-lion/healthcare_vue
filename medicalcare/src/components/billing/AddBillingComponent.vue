<script setup>
    import { RouterLink } from 'vue-router';
    import { addItemToArray, deleteItemToArray, formatDateToString, formatDateYYYYMMDD, isSupperAdmin } from '../helper/helper';
    import { deleteItem, getItemById, getItems, post, updateItem } from '@/services/baseServices';
    import { enviroment } from '@/enviroments/enviroment';
    import treatmentModal from "@/components/modals/TreatmentModal";
    import prescriptionModal from "@/components/modals/PrescriptionModal";
    import numeral from 'numeral';
import FooterComponent from '../footer/FooterComponent.vue';
import { useAuthStore } from '@/store/auth.module';

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
                                            {{item.last_name}} {{item.first_name}}
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
                                            {{item.last_name}} {{item.first_name}}
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
                                @click="addOrEditPrescription(null, null, 'Treatment')">Add</button>
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
                                <button class="btn btn-outline-primary" type="button" @click="addOrEditPrescription(null, null, 'Prescription')">Add</button>
                            </div>
                        </div>
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
<transition name="prescriptionModal">
    <div v-if="showPrescriptionModal">
        <prescriptionModal :input_data="item" @close="closePrescriptionModal"/>
    </div>
</transition>
<FooterComponent></FooterComponent>
</template>

<script>
export default{
    
    components:[treatmentModal, prescriptionModal],

    data(){
        return {
            auth: useAuthStore(),
            apiUrl:`${enviroment.apiUrl}/Billings`,
            billing_id:null,
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
                patient_id :null,
                doctor_id :null,
                department_id :null,
                patient_id_guid :null,
                doctor_id_guid :null,
                appointment_id :null,
                appointment_id_guid: null,
                diagnostic :"",
                notes :"",
                discharge_date :formatDateYYYYMMDD(new Date()),
                admission_date :formatDateYYYYMMDD(new Date())
            },
            prescriptionItemObs:{
				dosage:"",
				quantity:0,
                prescription_date: ""
            },
            treatmentItemObs:{
				category_id: null,
				description:"",
				quantity:0,
                treatment_date: ""
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
        async addOrEditItem(){
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
                this.loading = true;
                var url = `${this.apiUrl}`;
                if (enviroment.mongo_db){
                    this.item.doctor_id_guid = this.item.doctor_id;
                    this.item.doctor_id = null;
                    this.item.patient_id_guid = this.item.patient_id;
                    this.item.patient_id = null;
                    this.item.department_id_guid = this.item.department_id;
                    this.item.department_id = null;
                    this.item.appointment_id_guid = null;
                    this.item.appointment_id = null;
                }
                if (this.billing_id){
                    this.item.id = this.billing_id;
                    url = `${url}/Edit/${this.billing_id}`
                    await updateItem(url, this.item);
                }
                else{
                    url = `${url}/Add`;
                    var data = await post(url, this.item);
                    if (data.valid){
                        var id = data.data;
                        url = `${this.apiUrl}/Add/Treatement/${id}`;

                        data = await post(url, this.treatmentItems);
                        if (data.valid){
                            url = `${this.apiUrl}/Add/Prescription/${id}`;
                            await post(url, this.prescriptionItems);
                        }
                    }
                }
                this.$router.push("/Billing");
            }
        },
        async closeModal(data, saveData){
            this.showModal = false;
            if (saveData){
                if (this.billing_id){
                    var treatments = await this.loadTreatments();
                    if (treatments.valid){
                        this.treatmentItems = treatments.data;
                    }
                }
                else{
                    this.treatmentItems = addItemToArray(this.treatmentItems, data, "new_id");
                }
            }
        },
        async closePrescriptionModal(data, saveData){
            this.showPrescriptionModal = false;
            if (saveData){
                if (this.billing_id){
                    var prescriptions = await this.loadPrescriptions();
                    this.prescriptionItems = prescriptions.data;
                }
                else{
                    this.prescriptionItems = addItemToArray(this.prescriptionItems, data, "new_id");
                }
            }
        },
        addOrEditPrescription(id, new_id, type){
            new_id = (new_id) ? new_id : null;
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
            new_id = (new_id) ? new_id: null;
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
                callback: async confirm => {
                    if (confirm) {
                        if (id){
                            url = `${url}/Delete/${id}`;
                            await deleteItem(url);
                            if (type == "Treatment"){
                                var treatments = await this.loadTreatments();
                                this.treatmentItems = treatments.data;
                            }
                            else{
                                var prescriptions = await this.loadPrescriptions();
                                this.prescriptionItems = prescriptions.data;
                            }
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
        async loadTreatments(){
            return await getItems(`${enviroment.apiUrl}/Treatments/items/${this.billing_id}`);
        },
        async loadPrescriptions(){
            return await getItems(`${enviroment.apiUrl}/Prescriptions/items/${this.billing_id}`);
        }
    },
    async mounted(){
        this.billing_id = this.$route.params.id;
        this.title = (this.billing_id) ? "Edit Billing" : "Add Billing";

        var data = await getItems(`${enviroment.apiUrl}/Patients`);
        if (data.valid){
            this.patientItems = data.data;
            if (!isSupperAdmin(this.auth.accountLogin)){
                var hospital_id = this.auth.accountLogin.hospital_id;
                this.patientItems = this.patientItems.filter(li => li.hospital_id == hospital_id);
            }
        }

        data = await getItems(`${enviroment.apiUrl}/Doctors`);
        if (data.valid){
            this.doctorItems = data.data;
            
            if (!isSupperAdmin(this.auth.accountLogin)){
                hospital_id = this.auth.accountLogin.hospital_id;
                this.doctorItems = this.doctorItems.filter(li => li.hospital_id == hospital_id);
            }
        }

        data = await getItems(`${enviroment.apiUrl}/Departments`);
        if (data.valid){
            this.departmentItems = data.data;
            
            if (!isSupperAdmin(this.auth.accountLogin)){
                hospital_id = this.auth.accountLogin.hospital_id;
                this.departmentItems = this.departmentItems.filter(li => li.hospital_id == hospital_id);
            }
        }

        if (this.billing_id){
            var url = `${this.apiUrl}/${this.billing_id}`;
            data = await getItemById(url);
            if (data.valid){
                this.item = data.data;
                console.log(this.item);

                this.item.admission_date = formatDateToString(this.item.admission_date, "YYYY-MM-DD");
                this.item.discharge_date = formatDateToString(this.item.discharge_date, "YYYY-MM-DD");
            }

            data = await this.loadTreatments();
            if (data.valid){
                this.treatmentItems = data.data;
            }

            data = await this.loadPrescriptions();
            if (data.valid){
                this.prescriptionItems = data.data;
            }
        }
    }
}
  
</script>