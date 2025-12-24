using System.Collections;
using medicalcare_mongodb.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace medicalcare_mongodb.controllers
{
    [ApiController]
    [Route("Medicalcare/api/[controller]")]    
    public class BillingsController: BaseController
    {

        public BillingsController(MedicalcareDbContext context): base(context)
        {
        }

        [HttpPost]
        [Route("PrescriptionItem")]
        public async Task<IActionResult> GetPrescriptionItem([FromBody] Dictionary<string, object> item)
        {
            IDictionary? data = new Dictionary<string, object>();
            await Task.Run(() =>
            {
               data = this.context.GetPrescriptionItem(item); 
            });

            return Ok(data);
        }

        [HttpGet]
        [Route("BillingDetail/{id}")]
        public async Task<IActionResult> GetBillingDetail(string id)
        {

            IDictionary? item = new Dictionary<string, object>();
            await Task.Run(() =>
            {
                item = this.context.GetBillingDetails(id);
            });
            return Ok(item);
        }
        [HttpPost]
        [Route("TreatmentItem")]
        public async Task<IActionResult> GetTreatmentItem([FromBody] Dictionary<string, object> item)
        {
            IDictionary? data = new Dictionary<string, object>();
            await Task.Run(() =>
            {
               data = this.context.GetTreatmentItem(item); 
            });

            return Ok(data);
        }
        
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add(Billing data)
        {
            string? billing_id = "";
            // Validate the incoming model.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (data != null)
            {
                data.doctor_id = (string.IsNullOrEmpty(data.doctor_id_guid)) ?
                    null: new ObjectId(data.doctor_id_guid);

                data.patient_id = (string.IsNullOrEmpty(data.patient_id_guid)) ?
                    null: new ObjectId(data.patient_id_guid);

                data.department_id = (string.IsNullOrEmpty(data.department_id_guid)) ?
                    null: new ObjectId(data.department_id_guid);

                data.appointment_id = (string.IsNullOrEmpty(data.appointment_id_guid)) ?
                    null: new ObjectId(data.appointment_id_guid);
                data.billing_date = DateTime.Now;
                await Task.Run(() =>
                {
                    this.context.h_billing.Add(data);
                    this.context.SaveChanges();
                });
                billing_id = data.id_guid;
            }
            return Ok(billing_id);
        }        

        [HttpPost]
        [Route("Add/Treatement/{id}")]
        public async Task<IActionResult> AddTreatment(string id, IList<Dictionary<string, object>> treatements)
        {

            // Validate the incoming model.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (treatements != null)
            {
                await Task.Run(() =>
                {
                    if (treatements != null)
                    {
                        foreach (var item in treatements)
                        {
                            var treatmentItem = new Treatment
                            {
                                billing_id = new ObjectId(id), 
                                quantity = Convert.ToInt32(item["quantity"].ToString()),
                                description = item["description"].ToString(),
                                category_id = new ObjectId(item["category_id"].ToString()),
                                treatment_date = Convert.ToDateTime(item["treatment_date"].ToString())
                            };
                            this.context.m_treatment.Add(treatmentItem);
                        } 
                        this.context.SaveChanges();
                    }
                });
            }
            return Ok(new { message = "Treatment add successfully." });
        } 

        [HttpPost]
        [Route("Add/Prescription/{id}")]
        public async Task<IActionResult> AddPrescription(string id, IList<Dictionary<string, object>> items)
        {

            // Validate the incoming model.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (items != null)
            {
                await Task.Run(() =>
                {
                    foreach (var item in items)
                    {
                        var prescriptionItem = new Prescription
                        {
                            billing_id = new ObjectId(id), 
                            medicine_id = new ObjectId(item["medicine_id"].ToString()),
                            quantity = Convert.ToInt32(item["quantity"].ToString()),
                            duration = Convert.ToInt32(item["duration"].ToString()),
                            duration_type = item["duration_type"].ToString(),
                            dosage = item["dosage"].ToString(),
                            notes = item["notes"].ToString(),
                            medicine_type = item["medicine_type"].ToString(),
                            prescription_date = Convert.ToDateTime(item["prescription_date"].ToString()),
                        };
                        this.context.h_prescription.Add(prescriptionItem);
                    } 
                    this.context.SaveChanges();
                });
            }
            return Ok(new { message = "Prescription add successfully." });
        } 

        [HttpPut]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(string id, Billing dataInput)
        {
            dataInput.id = new ObjectId(id);
            dataInput.doctor_id = (string.IsNullOrEmpty(dataInput.doctor_id_guid)) ? 
                null : new ObjectId(dataInput.doctor_id_guid);
            dataInput.patient_id = (string.IsNullOrEmpty(dataInput.patient_id_guid)) ? 
                null : new ObjectId(dataInput.patient_id_guid);
            dataInput.appointment_id = (string.IsNullOrEmpty(dataInput.appointment_id_guid)) ? 
                null : new ObjectId(dataInput.appointment_id_guid);
            dataInput.department_id = (string.IsNullOrEmpty(dataInput.department_id_guid)) ? 
                null : new ObjectId(dataInput.department_id_guid);

            // Validate the incoming model.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Billing? item = await this.context.h_billing.FirstOrDefaultAsync(
                    m => m.id == dataInput.id);
            if (item == null)
            {
                return NotFound(new { message = "Billing not found." });
            }
            else
            {
                await Task.Run(() =>
                {
                    this.context.h_billing.Entry(item).CurrentValues.SetValues(dataInput);
                    this.context.SaveChanges();
                });

                return Ok(item);
            }
        }
    }
}