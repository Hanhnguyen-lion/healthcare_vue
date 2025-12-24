<template>
<div class="container">
    <div class="row justify-content-center">
        <div class="col-md-6 col-md-4">
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
        </div>    
    </div>    
</div>    
</template>
<style scoped>
 .modal-overlay {
    display: flex;
    position: fixed;
    top: 0;
    left: 10%;
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
// import "./modal.css";
import { getItemById, getItems, post, updateItem} from "@/services/baseServices";
import { formatDateYYYYMMDD } from "../helper/helper";

export default {
    name: "TreatmentModal",
    props: ["input_data"],
    data() {
        return {
            title: "Add Treatment",
            billing_id: null,
            treatment_id: null,
            new_treatment_id: null,
            quantity_error: "",
            treatment_type_error: "",
            treatment_date_error: "",

            output_data: {
                category_id: null,
                description: "",
                quantity: 0,
                treatment_date: formatDateYYYYMMDD(new Date()),
                treatmentItemObs: {}
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
            if (!this.output_data.treatment_date)
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
                    billing_id: (enviroment.mongo_db) ? null: (this.billing_id || null),
                    billing_id_guid: this.billing_id || null,
                    id: this.treatment_id,
                    new_id: this.new_treatment_id,
                    category_id: (enviroment.mongo_db) ? null: this.output_data.category_id,
                    category_id_guid: this.output_data.category_id,
                    description: this.output_data.description,
                    treatment_date: this.output_data.treatment_date,
                    quantity: this.output_data.quantity
                }

                if (this.billing_id) {
                    item.id = (item.id) ? item.id : "0";
                    url = `${url}/Edit/${item.id}`;
                    updateItem(url, item).then((data)=>{
                        if (data.valid)
                            this.handleClose();
                    });
                }
                else{
                    url = `${enviroment.apiUrl}/Billings/TreatmentItem`;
                    post(url, item).then(data=>{
                        if (data.valid){
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
        
        getItems(`${enviroment.apiUrl}/TreatmentsCategory`).then(response => {
            if (response.valid){
                 for (let index = 0; index < response.data.length; index++) {
                    const element = response.data[index];
                    this.categoryItems.push({
                        id: (enviroment.mongo_db) ? element.id_guid: element.id,
                        name_en: element.name_en
                    });
                 }
            }
        });
        if (this.treatment_id) {
            this.title = "Edit Treatment";
            url = `${url}/${this.treatment_id}`;
            getItemById(url).then(item => {
                if (item.valid){
                    this.output_data = item.data;
                    this.output_data.treatment_date = formatDateYYYYMMDD(item.data.treatment_date);
                    this.output_data.category_id = (enviroment.mongo_db) ? 
                        item.data.category_id_guid : item.data.category_id
                }
            });
        }
        else {
            if (this.new_treatment_id) {
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