using medicalcare_mongodb.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace medicalcare_mongodb.controllers
{
    [ApiController]
    [Route("Medicalcare/api/[controller]")]    
    public class PrescriptionsController: BaseController
    {
        public PrescriptionsController(MedicalcareDbContext context) : base(context)
        {
        }

        [HttpGet]
        [Route("items/{billing_id}")]
        public async Task<IActionResult> Getprescriptions(string billing_id)
        {
            
            var prescriptions = await this.context.h_prescription.Where(li=> li.billing_id_guid == billing_id).ToArrayAsync();
            var medicines = await this.context.m_medicine.ToArrayAsync();
            
            var data = from p in prescriptions
                        join m in medicines on p?.medicine_id_guid equals m.id_guid into mGroup
                        from mDefault in mGroup.DefaultIfEmpty()
                        select new
                        {
                            id = p?.id_guid,
                            billing_id = billing_id,
                            prescription_date = p?.prescription_date,
                            medicine_id = p?.medicine_id_guid,
                            medicine_name = mDefault?.name,
                            quantity = p?.quantity??0,
                            amount = mDefault?.price??0,
                            total = Convert.ToDecimal((p?.quantity??0) * (mDefault?.price??0))
                        };

            return Ok(data);
        }

        [HttpGet]
        [Route("DurationTypes")]
        public async Task<IActionResult> GetDurationTypes()
        {
            
            var durationTypes = await this.context.m_duration_type.ToArrayAsync();
            
            var data = from p in durationTypes
                        select new
                        {
                            id = p?.id_guid,
                            name_en = p?.name_en
                        };

            return Ok(data);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add(Prescription item)
        {
            // Validate the incoming model.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (item != null)
            {
                await Task.Run(() =>
                {
                    this.context.h_prescription.Add(item);
                    this.context.SaveChanges();
                });
            }
            return Ok(new { message = "Prescription add successfully." });
        }        

        [HttpPut]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(string id, Prescription item)
        {
            if (id != "0")
                item.id = new ObjectId(id);

            item.billing_id = string.IsNullOrEmpty(item.billing_id_guid) ? 
                null: new ObjectId(item.billing_id_guid);
            item.medicine_id = string.IsNullOrEmpty(item.medicine_id_guid) ? 
                null: new ObjectId(item.medicine_id_guid);

            // Validate the incoming model.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if patient exists
            Prescription? editItem = await this.context.h_prescription.FirstOrDefaultAsync(
                    m => m.id == item.id);
            if (editItem == null)
            {
                if (!string.IsNullOrEmpty(item.billing_id_guid))
                {
                    if (item != null)
                    {
                        this.context.h_prescription.Add(item);
                        this.context.SaveChanges();
                    }
                    return Ok(item);
                }
                else
                    return NotFound(new { message = "Prescription not found." });
            }
            else
            {
                await Task.Run(() =>
                {
                    this.context.h_prescription.Entry(editItem).CurrentValues.SetValues(item);
                    this.context.SaveChanges();
                });

                return Ok(item);
            }
        }
    }
}