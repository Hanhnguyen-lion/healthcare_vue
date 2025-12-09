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
                        <div class="row mb-3">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label class="fw-bold">Treatment Date <span class="text-danger">*</span></label>
                                    <input type="date" class="form-control" name="treatment_date" 
                                        v-model="output_data.treatment_date"   
                                        placeholder="Enter treatment date">
                                    <div v-if="treatment_date_error" class="invalid-feedback">
                                        <div>{{ treatment_date_error }}</div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">Treatment Type <span class="text-danger">*</span></label>
                                    <select class="form-select" name="category_id" v-model="output_data.category_id">
                                        <option v-for="item in categoryItems" :key="item.id" :value="item.id">
                                            {{ item.name_en }}
                                        </option>
                                    </select>
                                    <div v-if="treatment_type_error" class="invalid-feedback">
                                        <div>{{ treatment_type_error }}</div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="fw-bold">Quantity <span class="text-danger">*</span></label>
                                    <input type="number" class="form-control" name="quantity"
                                        v-model="output_data.quantity" min="0" placeholder="Enter quantity">
                                    <div v-if="quantity_error" class="invalid-feedback">
                                        <div>{{ quantity_error }}</div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label class="fw-bold">Description</label>
                                    <textarea rows="3" class="form-control" name="description"
                                        v-model="output_data.description" placeholder="Enter description" />
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

<script>
import { enviroment } from "@/enviroments/enviroment";
import "./modal.css";
import { getItemById, getItems, post, updateItem} from "@/services/baseServices";
import { formatDateYYYYMMDD } from "../helper/helper";

export default {
    name: "TreatmentModal",
    props: ["input_data"],
    data() {
        return {
            title: "Add Treatment",
            billing_id: 0,
            treatment_id: 0,
            new_treatment_id: 0,
            quantity_error: "",
            treatment_type_error: "",
            treatment_date_error: "",

            output_data: {
                category_id: 0,
                description: "",
                quantity: 0,
                treatment_date: formatDateYYYYMMDD(new Date()),
                treatmentItemObs: {

                }
            },
            saveData: false,
            categoryItems: [],
            apiUrl: `${enviroment.apiUrl}/Treatments`
        };
    },
    methods: {
        validQuantity(){
            if (!this.output_data.quantity)
                this.quantity_error = "Quantity is required";
            else{
                this.quantity_error = "";
            }
        },
        validTreatmentType(){
            if (!this.output_data.category_id)
                this.treatment_type_error = "Treatment type is required";
            else{
                this.treatment_type_error = "";
            }
        },
        validTreatmentDate(){
            if (!this.output_data.category_id)
                this.treatment_date_error = "Treatment date is required";
            else{
                this.treatment_date_error = "";
            }
        },
        async handleSave() {
            this.validQuantity();
            this.validTreatmentType();
            this.validTreatmentDate();
            if (!this.quantity_error &&
                !this.treatment_type_error &&
                !this.treatment_date_error
            ){
                this.saveData = true;
                var url = `${this.apiUrl}`;
                var item = {
                    billing_id: this.billing_id,
                    id: this.treatment_id,
                    new_id: this.new_treatment_id,
                    category_id: this.output_data.category_id,
                    description: this.output_data.description,
                    treatment_date: this.output_data.treatment_date,
                    quantity: this.output_data.quantity
                }
                if (this.billing_id > 0) {
                    url = `${url}/Edit/${item.id}`;
                    updateItem(url, item).then((data)=>{
                        if (data.valid)
                            this.handleClose();
                    });
                }
                else{
                    url = `${enviroment.apiUrl}/Billings/TreatmentItem`;
                    post(url, item).then(data=>{
                        console.log("data:", data);
                        if (data.valid){
                            console.log("data.data:", data.data);
                            var item = data.data;
                            this.output_data = item;
                            this.output_data.id = item.id;
                            this.output_data.new_id = item.new_id;
                            this.handleClose();
                        }
                    })
                }
            }
        },
        handleClose() {
            this.$emit("close", this.output_data, this.saveData);
        }
    },
    mounted() {
        
        this.billing_id = this.input_data.billing_id;
        this.treatment_id = this.input_data.treatment_id;
        this.new_treatment_id = this.input_data.new_treatment_id;
        var url = `${this.apiUrl}`; 
        
        getItems(`${url}/Category`).then(data => {
            if (data.valid)
                this.categoryItems = data.data;
        });

        if (this.treatment_id > 0) {
            url = `${url}/item/${this.treatment_id}`;
            getItemById(url).then(item => {
                if (item.valid){
                    this.output_data = item.data;
                    this.output_data.treatment_date = formatDateYYYYMMDD(item.data.treatment_date);
                }
            });
        }
        else {
            if (this.new_treatment_id > 0) {
                this.title = "Edit Treatment";
                this.output_data = {
                    category_id: this.input_data.treatmentItemObs.category_id,
                    description: this.input_data.treatmentItemObs.description,
                    quantity: this.input_data.treatmentItemObs.quantity,
                    treatment_date: formatDateYYYYMMDD(this.input_data.treatmentItemObs.treatment_date)
                };
            }
        }
    }
}
</script>