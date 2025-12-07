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
                                        v-model="output_data.quantity" placeholder="Enter quantity">
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
import { getItemById, getItems} from "@/services/baseServices";
import axios from "axios";

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

            output_data: {
                category_id: 0,
                description: "",
                quantity: 0,
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
        async handleSave() {
            this.validQuantity();
            this.validTreatmentType();

            if (!this.quantity_error &&
                !this.treatment_type_error
            ){
                this.saveData = true;
                var item = {
                    billing_id: this.billing_id,
                    id: this.treatment_id,
                    new_id: this.new_treatment_id,
                    category_id: this.output_data.category_id,
                    description: this.output_data.description,
                    quantity: this.output_data.quantity
                }
                if (this.billing_id > 0) {
                    var url = `${this.apiUrl}/Edit/${item.id}`;
                    await axios.put(url, item).then(()=>{
                        this.handleClose();
                    });
                }
                else{
                    url = `${enviroment.apiUrl}/Billings/TreatmentItem`;
                    axios.post(url, item).then(data=>{
                        this.output_data = data.data;
                        this.output_data.id = data.data.id;
                        this.output_data.new_id = data.data.new_id;
                        this.handleClose();
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
        getItems(`${this.apiUrl}/Category`).then(data => {
            this.categoryItems = data;
        });
        if (this.treatment_id > 0) {
            var url = `${this.apiUrl}/item`;
            getItemById(url, this.treatment_id).then(item => {
                this.output_data = item;
            });
        }
        else {
            if (this.new_treatment_id > 0) {
                this.title = "Edit Treatment";
                this.output_data = {
                    category_id: this.input_data.treatmentItemObs.category_id,
                    description: this.input_data.treatmentItemObs.description,
                    quantity: this.input_data.treatmentItemObs.quantity
                };
            }
        }
    }
}
</script>