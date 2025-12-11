<template>
    <div class="modal-overlay">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title">{{ title }}</h4>
                <button type="button" class="btn-close" aria-label="Close" @click="handleClose()"></button>
            </div>
            <div class="modal-body">
                <div class="container">
                    <form name="form">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">Medicine <span class="text-danger">*</span></label>
                                    <select class="form-select" name="medicine_id" v-model="output_data.medicine_id" placeholder="Enter Medicine">
                                        <option v-for="item in medicineItems" :key="item.id" :value="item.id">
                                            {{item.name}}
                                        </option>
                                    </select>
                                    <div v-if="medicine_id_error" class="invalid-feedback">
                                        <div>{{medicine_id_error}}</div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">Prescription Date <span class="text-danger">*</span></label>
                                    <input type="date" class="form-control" name="prescription_date" 
                                        v-model="output_data.prescription_date"   
                                        placeholder="Enter prescription date">
                                    <div v-if="prescription_date_error" class="invalid-feedback">
                                        <div>{{ prescription_date_error }}</div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">Quantity <span class="text-danger">*</span></label>
                                    <input type="number" min="0" class="form-control" name="quantity" v-model="output_data.quantity" placeholder="Enter quantity"/>
                                    <div v-if="quantity_error" class="invalid-feedback">
                                        <div>{{quantity_error}}</div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label></label>
                                    <select class="form-select" name="medicine_type" v-model="output_data.medicine_type">
                                        <option v-for="item in medicineTypeItems" :key="item.id" :value="item.name_en">
                                            {{item.name_en}}
                                        </option>
                                    </select>
                                </div>
                            </div>
                        </div>    
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">Duration <span class="text-danger">*</span></label>
                                    <input type="number" min="0" class="form-control" name="duration" v-model="output_data.duration" placeholder="Enter duration"/>
                                    <div v-if="duration_error" class="invalid-feedback">
                                        <div>{{duration_error}}</div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label></label>
                                    <select class="form-select" name="duration_type" v-model="output_data.duration_type">
                                        <option v-for="item in durationItems" :key="item.id" :value="item.name_en">
                                            {{item.name_en}}
                                        </option>
                                    </select>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label class="fw-bold">Dosage</label>
                                    <textarea rows="3" class="form-control" name="dosage" v-model="output_data.dosage" placeholder="Enter dosage"/>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label class="fw-bold">Notes</label>
                                    <textarea rows="3" class="form-control" name="notes" v-model="output_data.notes" placeholder="Enter notes"/>
                                </div>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-outline-secondary" @click="handleClose()">Cancel</button>
                <button type="button" class="btn btn-outline-primary" @click="handleSave()">OK</button>
            </div>
        </div>
    </div>
</template>
<style scoped>
  .modal-overlay {
    display: flex;
    position: fixed;
    top: 0;
    left: 0;
    width: 80%;
    height: 100%;
    background-color: rgba(0, 0, 0, 0.5);
    justify-content: center;
    align-items: center;
    z-index: 1001;
  }
  .modal-content {
    background-color: white;
    padding: 20px;
    border-radius: 8px;
  }    
</style>
<script>
import { enviroment } from "@/enviroments/enviroment";
import { getItemById, getItems, post, updateItem} from "@/services/baseServices";
import { formatDateYYYYMMDD } from "../helper/helper";

export default {
    name: "PrescriptionModal",
    props: ["input_data"],
    data() {
        return {
            title: "Add Prescription",
            billing_id: 0,
            prescription_id: 0,
            new_prescription_id: 0,
            medicine_id_error: "",
            quantity_error: "",
            duration_error: "",
            prescription_date_error: "",

            output_data: {
                medicine_id: 0,
                notes: "",
                dosage: "",
                duration_type: "day",
                duration: 0,
                medicine_type: "tablets",
                quantity: 0,
                prescription_date: formatDateYYYYMMDD(new Date()),
                prescriptionItemObs: {

                }
            },
            saveData: false,
            medicineItems: [],
            durationItems: [],
            medicineTypeItems: [],
            apiUrl: `${enviroment.apiUrl}/Prescriptions`
        };
    },
    methods: {
        validMedicine(){
            if (!this.output_data.medicine_id)
                this.medicine_id_error = "Medicine is required";
            else{
                this.medicine_id_error = "";
            }
        },
        validQuantity(){
            if (!this.output_data.quantity)
                this.quantity_error = "Quantity is required";
            else{
                this.quantity_error = "";
            }
        },
        validPrescriptionDate(){
            if (!this.output_data.prescription_date)
                this.prescription_date_error = "Prescription date is required";
            else{
                this.prescription_date_error = "";
            }
        },
        validDuration(){
            if (!this.output_data.duration)
                this.duration_error = "Duration is required";
            else{
                this.duration_error = "";
            }
        },
        async handleSave() {
            this.validMedicine();
            this.validQuantity();
            this.validDuration();
            this.validPrescriptionDate();
            if (!this.medicine_id_error &&
                !this.quantity_error &&
                !this.duration_error &&
                !this.prescription_date_error
            ){
                this.saveData = true;
                var url = `${this.apiUrl}`;
                var item = {
                    billing_id: this.billing_id,
                    id: this.prescription_id,
                    new_id: this.new_prescription_id,
                    medicine_id: this.output_data.medicine_id,
                    notes: this.output_data.notes,
                    dosage: this.output_data.dosage,
                    duration: this.output_data.duration,
                    medicine_type: this.output_data.medicine_type,
                    duration_type: this.output_data.duration_type,
                    quantity: this.output_data.quantity,
                    prescription_date: formatDateYYYYMMDD(this.output_data.prescription_date)
                }
                if (this.billing_id > 0) {
                    url = `${url}/Edit/${item.id}`;
                    updateItem(url, item).then(response=>{
                        if (response.valid)
                            this.handleClose();
                    });
                }
                else{
                    url = `${enviroment.apiUrl}/Billings/PrescriptionItem`;
                    post(url, item).then(response=>{
                        if (response.valid){
                            var data = response.data;
                            this.output_data = data;
                            this.output_data.id = data.id;
                            this.output_data.new_id = data.new_id;
                            this.handleClose();
                        }
                    });
                }
            }
        },
        handleClose() {
            this.$emit("close", this.output_data, this.saveData);
        }
    },
    mounted() {
        this.billing_id = this.input_data.billing_id;
        this.prescription_id = this.input_data.prescription_id;
        this.new_prescription_id = this.input_data.new_prescription_id;
        
        var url = `${this.apiUrl}`;

        getItems(`${enviroment.apiUrl}/Medicines`).then(data => {
            if (data.valid)
                this.medicineItems = data.data;
        });
        
        getItems(`${url}/DurationTypes`).then(data => {
            if (data.valid)
                this.durationItems = data.data;
        });
        
        getItems(`${url}/MedicineTypes`).then(data => {
            if (data.valid)
                this.medicineTypeItems = data.data;
        });

        if (this.prescription_id > 0) {
            url = `${url}/item/${this.prescription_id}`;
            getItemById(url).then(item => {
                if (item.valid){
                    this.output_data = item.data;
                    this.output_data.prescription_date = formatDateYYYYMMDD(item.data.prescription_date);
                }
            });
        }
        else {
            if (this.new_prescription_id > 0) {
                this.title = "Edit Prescription";
                this.output_data = {
                    medicine_id: this.input_data.prescriptionItemObs.medicine_id,
                    notes: this.input_data.prescriptionItemObs.notes,
                    dosage: this.input_data.prescriptionItemObs.dosage,
                    duration_type: this.input_data.prescriptionItemObs.duration_type,
                    duration: this.input_data.prescriptionItemObs.duration,
                    medicine_type: this.input_data.prescriptionItemObs.medicine_type,
                    quantity: this.input_data.prescriptionItemObs.quantity,
                    prescription_date: formatDateYYYYMMDD(this.input_data.prescriptionItemObs.prescription_date)
                };
            }
        }
    }
}
</script>