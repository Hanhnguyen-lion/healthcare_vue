using medicalcare_mongodb.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace medicalcare_mongodb.controllers
{
    [Controller]
    public abstract class BaseController: ControllerBase
    {
        public readonly MedicalcareDbContext context;

        public BaseController(MedicalcareDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            var controllerName = this.GetType().Name;
            if (controllerName.IndexOf(MedicalcareDbContext.Department, StringComparison.OrdinalIgnoreCase) != -1){
                var data = await this.context.GetDepartments();
                return Ok(data);
            }
            else if (controllerName.IndexOf(MedicalcareDbContext.Hospital, StringComparison.OrdinalIgnoreCase) != -1){
                var data = await this.context.GetHospitals();
                return Ok(data);
            }
            else if (controllerName.IndexOf(MedicalcareDbContext.TreatmentsCategory, StringComparison.OrdinalIgnoreCase) != -1){
                var data = await this.context.GettreatmentCategories();
                return Ok(data);
            }
            else if (controllerName.IndexOf(MedicalcareDbContext.MedicinesCategory, StringComparison.OrdinalIgnoreCase) != -1){
                var data = await this.context.GetMedicineTypes();
                return Ok(data);
            }
            else if (controllerName.IndexOf(MedicalcareDbContext.Medicine, StringComparison.OrdinalIgnoreCase) != -1)
                return Ok( await this.context.GetMedicines());
            else if (controllerName.IndexOf(MedicalcareDbContext.Patient, StringComparison.OrdinalIgnoreCase) != -1){
                var data = await this.context.GetPatients();
                return Ok(data);
            }
            else if (controllerName.IndexOf(MedicalcareDbContext.Doctor, StringComparison.OrdinalIgnoreCase) != -1){
                var data = await this.context.GetDoctors();
                return Ok(data);
            }
            else if (controllerName.IndexOf(MedicalcareDbContext.Appointment, StringComparison.OrdinalIgnoreCase) != -1){
                var data = await this.context.GetAppointments();
                return Ok(data);
            }
            else if (controllerName.IndexOf(MedicalcareDbContext.Billing, StringComparison.OrdinalIgnoreCase) != -1){
                var data = await this.context.GetBillings();
                return Ok(data);
            }
            else if (controllerName.IndexOf(MedicalcareDbContext.Prescription, StringComparison.OrdinalIgnoreCase) != -1)
                return Ok( await this.context.h_prescription.ToListAsync());
            else if (controllerName.IndexOf(MedicalcareDbContext.Treatment, StringComparison.OrdinalIgnoreCase) != -1)
                return Ok( await this.context.m_treatment.ToListAsync());
            else
            {
                var data = await this.context.GetAccounts();
                return Ok( data);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var controllerName = this.GetType().Name;
            object? item;
            item = await this.context.GetItem(id, controllerName);
            if (item == null)
            {
                return NotFound(new { message = "Item not found." });
            }
            else
            {
                if (controllerName.IndexOf(MedicalcareDbContext.Department, StringComparison.OrdinalIgnoreCase) != -1){
                    var data = (Department?) item;
                    item = new Dictionary<string, object>
                    {
                        ["id"] = data?.id_guid??"",
                        ["name"] = data?.name??"",
                        ["hospital_id"] = data?.hospital_id_guid??"",
                        ["phone"] = data?.phone??""
                    };
                }
                else if (controllerName.IndexOf(MedicalcareDbContext.Hospital, StringComparison.OrdinalIgnoreCase) != -1){
                    var data = (Hospital?) item;
                    item = new Dictionary<string, object>
                    {
                        ["id"] = data?.hospital_id_guid??"",
                        ["phone"] = data?.phone??"",
                        ["address"] = data?.address??"",
                        ["country"] = data?.country??"",
                        ["name"] = data?.name??"",
                        ["description"] = data?.description??"",
                        ["email"] = data?.email??""
                    };
                }
                else if (controllerName.IndexOf(MedicalcareDbContext.TreatmentsCategory, StringComparison.OrdinalIgnoreCase) != -1){
                    var data = (TreatmentCategory?) item;
                    item = new Dictionary<string, object>
                    {
                        ["id"] = data?.id_guid??"",
                        ["name_en"] = data?.name_en??"",
                        ["name_vn"] = data?.name_vn??"",
                        ["name_jp"] = data?.name_jp??"",
                        ["price"] = data?.price??0,
                        ["description"] = data?.description??""
                    };
                }
                else if (controllerName.IndexOf(MedicalcareDbContext.MedicinesCategory, StringComparison.OrdinalIgnoreCase) != -1){
                    var data = (MedicineType?) item;
                    item = new Dictionary<string, object>
                    {
                        ["id"] = data?.id_guid??"",
                        ["name_en"] = data?.name_en??"",
                        ["name_vn"] = data?.name_vn??"",
                        ["name_jp"] = data?.name_jp??"",
                        ["description"] = data?.description??""
                    };
                }
                else if (controllerName.IndexOf(MedicalcareDbContext.Medicine, StringComparison.OrdinalIgnoreCase) != -1){
                    var data = (Medicine?) item;
                    item = new Dictionary<string, object>
                    {
                        ["category_id"] = data?.category_id_guid??"",
                        ["id"] = data?.id_guid??"",
                        ["name"] = data?.name??"",
                        ["price"] = data?.price??0
                    };
                }
                else if (controllerName.IndexOf(MedicalcareDbContext.Patient, StringComparison.OrdinalIgnoreCase) != -1){
                    var data = (Patient?) item;
                    item = new Dictionary<string, object>
                    {
                        ["hospital_id"] = data?.hospital_id_guid??"",
                        ["id"] = data?.id_guid!,
                        ["code"] = data?.code??"",
                        ["date_of_birth"] = data?.date_of_birth!,
                        ["first_name"] = data?.first_name??"",
                        ["last_name"] = data?.last_name??"",
                        ["gender"] = data?.gender??"",
                        ["home_address"] = data?.home_address??"",
                        ["office_address"] = data?.office_address??"",
                        ["phone_number"] = data?.phone_number??"",
                        ["email"] = data?.email??"",
                        ["job"] = data?.job??"",
                        ["emergency_contact_name"] = data?.emergency_contact_name??"",
                        ["emergency_contact_phone"] = data?.emergency_contact_phone??"",
                        ["insurance_type"] = data?.insurance_type??"",
                        ["insurance_policy_number"] = data?.insurance_policy_number??"",
                        ["insurance_provider"] = data?.insurance_provider??"",
                        ["insurance_expire"] = data?.insurance_expire!,
                        ["insurance_info"] = data?.insurance_info??"",
                        ["medical_history"] = data?.medical_history??"",
                    };
                }
                else if (controllerName.IndexOf(MedicalcareDbContext.Doctor, StringComparison.OrdinalIgnoreCase) != -1){
                    var data = (Doctor?) item;
                    item = new Dictionary<string, object>
                    {
                        ["hospital_id"] = data?.hospital_id_guid??"",
                        ["id"] = data?.id_guid!,
                        ["first_name"] = data?.first_name??"",
                        ["last_name"] = data?.last_name??"",
                        ["gender"] = data?.gender??"",
                        ["phone"] = data?.phone??"",
                        ["email"] = data?.email??"",
                        ["quanlification"] = data?.quanlification??"",
                        ["job_specification"] = data?.job_specification??"",
                    };
                }
                else if (controllerName.IndexOf(MedicalcareDbContext.Appointment, StringComparison.OrdinalIgnoreCase) != -1){
                    var data = (Appointment?) item;
                    item = new Dictionary<string, object>
                    {
                        ["hour"] = data?.hour??0,
                        ["id"] = data?.id_guid??"",
                        ["minute"] = data?.minute??0,
                        ["patient_id"] = data?.patient_id_guid??"",
                        ["doctor_id"] = data?.doctor_id_guid??"",
                        ["appointment_date"] = data?.appointment_date??DateTime.Today,
                        ["reason_to_visit"] = data?.reason_to_visit??"",
                        ["status"] = data?.status??""
                    };
                }
                else if (controllerName.IndexOf(MedicalcareDbContext.Billing, StringComparison.OrdinalIgnoreCase) != -1){
                    var data = (Billing?) item;
                    item = new Dictionary<string, object>
                    {
                        ["id"] = data?.id_guid??"",
                        ["department_id"] = data?.department_id_guid??"",
                        ["patient_id"] = data?.patient_id_guid??"",
                        ["doctor_id"] = data?.doctor_id_guid??"",
                        ["appointment_id"] = data?.appointment_id_guid??"",
                        ["billing_date"] = data?.billing_date??DateTime.Today,
                        ["discharge_date"] = data?.discharge_date!,
                        ["admission_date"] = data?.admission_date!,
                        ["diagnostic"] = data?.diagnostic??"",
                        ["notes"] = data?.notes??"",
                        ["amount"] = data?.amount??0,
                        ["days"] = data?.days!,
                    };
                }
                else if (controllerName.IndexOf(MedicalcareDbContext.Account, StringComparison.OrdinalIgnoreCase) != -1){
                    var data = (Account?) item;
                    item = new Dictionary<string, object>
                    {
                        ["id"] = data?.id_guid??"",
                        ["hospital_id"] = data?.hospital_id_guid!,
                        ["email"] = data?.email??"",
                        ["password"] = data?.password??"",
                        ["first_name"] = data?.first_name??"",
                        ["dob"] = data?.dob??DateTime.Today,
                        ["last_name"] = data?.last_name!,
                        ["gender"] = data?.gender!,
                        ["role"] = data?.role??"",
                        ["account_type"] = data?.account_type??"",
                        ["address"] = data?.address!,
                        ["phone"] = data?.phone!,
                        ["create_date"] = data?.create_date!
                    };
                }
                else if (controllerName.IndexOf(MedicalcareDbContext.Treatment, StringComparison.OrdinalIgnoreCase) != -1)
                    item = (Treatment?) item;
                else if (controllerName.IndexOf(MedicalcareDbContext.Prescription, StringComparison.OrdinalIgnoreCase) != -1)
                    item = (Prescription?) item;
            }
            return Ok(item);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }
            var name = this.GetType().Name;
            var isDelete = await this.context.DeleteItem(id, name);
            if (isDelete)
                return Ok(new {message = "Item deleted "});
            else
                return NotFound(new { message = "Item not found." });
        }
    }
}