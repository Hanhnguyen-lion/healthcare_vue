using medicalcare_mongodb.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace medicalcare_mongodb.controllers
{
    [ApiController]
    [Route("Medicalcare/api/[controller]")]    
    public class MedicinesController: BaseController
    {

        public MedicinesController(MedicalcareDbContext context): base(context)
        {
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add(Medicine data)
        {

            // Validate the incoming model.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (data != null)
            {
                var newItem = new Medicine{
                    category_id = (string.IsNullOrEmpty(data?.category_id_guid)) ? null: new ObjectId(data.category_id_guid),
                    name = data?.name,
                    type = data?.type,
                    price = data?.price
                };

                await Task.Run(() =>
                {
                    this.context.m_medicine.Add(newItem);
                    this.context.SaveChanges();
                });
            }
            return Ok(new { message = "Medicine add successfully." });
        }        

        [HttpPost]
        [Route("Import")]
        public async Task<IActionResult> Import(IList<IDictionary<string, object>> data)
        {

            // Validate the incoming model.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (data != null)
            {
                var categories = await context.m_medicine_type.ToListAsync();

                await Task.Run(() =>
                {
                    foreach (var item in data)
                    {
                        var id_guid = item["id"].ToString();
                        var name = item["Name"].ToString();
                        var type = item["Type"].ToString();
                        var price = Convert.ToDecimal(item["Price"].ToString());
                        var categoryItem = categories.Where(li=> li.name_en == type).FirstOrDefault();
                        var newItem = new Medicine{
                            category_id = (string.IsNullOrEmpty(categoryItem?.id_guid)) ? null: new ObjectId(categoryItem?.id_guid),
                            name = name,
                            type = type,
                            price = price
                        };
                        if (string.IsNullOrEmpty(id_guid))
                        {
                            this.context.m_medicine.Add(newItem);
                        }
                        else
                        {
                            Medicine? medicine = this.context.m_medicine.FirstOrDefault(m => m.id == new ObjectId(id_guid));
                            if (medicine == null)
                            {
                                this.context.m_medicine.Add(newItem);
                            }
                            else
                            {
                                newItem.id = new ObjectId(id_guid);
                                this.context.m_medicine.Entry(medicine).CurrentValues.SetValues(newItem);
                            }
                        }
                    }
                    this.context.SaveChanges();
                });

            }
            return Ok(new { message = "Import successfully." });
        }        

        [HttpPut]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(string id, Medicine dataInput)
        {
            dataInput.id = new ObjectId(id);
            dataInput.category_id = (string.IsNullOrEmpty(dataInput.category_id_guid)) ? 
                null : new ObjectId(dataInput.category_id_guid);
            // Validate the incoming model.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Medicine? item = await this.context.m_medicine.FirstOrDefaultAsync(
                    m => m.id == dataInput.id);
            if (item == null)
            {
                return NotFound(new { message = "Medicine not found." });
            }
            else
            {
                await Task.Run(() =>
                {
                    this.context.m_medicine.Entry(item).CurrentValues.SetValues(dataInput);
                    this.context.SaveChanges();
                });

                return Ok(item);
            }
        }
    }
}