using System.Net.Sockets;
using medicalcare_mongodb.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace medicalcare_mongodb.controllers
{
    [ApiController]
    [Route("Medicalcare/api/[controller]")]    
    public class TreatmentsController: BaseController
    {
        public TreatmentsController(MedicalcareDbContext context) : base(context)
        {
        }

        [HttpGet]
        [Route("items/{billing_id}")]
        public async Task<IActionResult> GetTreatments(string billing_id)
        {
            
            var treatments = await this.context.m_treatment.Where(li => li.billing_id == new ObjectId(billing_id)).ToListAsync();
            var treatment_categories = await this.context.m_treatment_category.ToArrayAsync();
            
            var v_treatments = from t in treatments
                        join tc in treatment_categories on t?.category_id_guid equals tc.id_guid into tcGroup
                        from tcDefault in tcGroup.DefaultIfEmpty()
                        select new
                        {
                            id = t?.id_guid,
                            billing_id = billing_id,
                            treatment_type = tcDefault?.name_en,
                            treatment_date = t?.treatment_date,
                            category_id = t?.category_id_guid,
                            quantity = t?.quantity??0,
                            amount = tcDefault?.price??0,
                            total = Convert.ToDecimal((t?.quantity??0) * (tcDefault?.price??0))
                        };
            return Ok(v_treatments);

        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add(Treatment item)
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
                    this.context.m_treatment.Add(item);
                    this.context.SaveChanges();
                });
            }
            return Ok(new { message = "Treatment add successfully." });
        }        

        [HttpPut]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(string id, Treatment item)
        {
            if (id != "0")
                item.id = new ObjectId(id);

            item.category_id = string.IsNullOrEmpty(item.category_id_guid) ?
                null: new ObjectId(item.category_id_guid);

            item.billing_id = string.IsNullOrEmpty(item.billing_id_guid) ? null: new ObjectId(item.billing_id_guid);

            // Validate the incoming model.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if patient exists
            Treatment? editItem = await this.context.m_treatment.FirstOrDefaultAsync(
                    m => m.id == item.id);
            if (editItem == null)
            {
                if (!string.IsNullOrEmpty(item?.billing_id_guid))
                {
                    if (item != null)
                    {
                        this.context.m_treatment.Add(item);
                        this.context.SaveChanges();
                    }
                    return Ok(item);
                }
                else
                    return NotFound(new { message = "Treatment not found." });
            }
            else
            {
                await Task.Run(() =>
                {
                    this.context.m_treatment.Entry(editItem).CurrentValues.SetValues(item);
                    this.context.SaveChanges();
                });

                return Ok(item);
            }
        }
    }
}