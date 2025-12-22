using medicalcare_mongodb.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace medicalcare_mongodb.controllers
{
    [ApiController]
    [Route("Medicalcare/api/[controller]")]    
    public class MedicinesCategoryController: BaseController
    {
        public MedicinesCategoryController(MedicalcareDbContext context) : base(context)
        {
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddCategory(MedicineType item)
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
                    this.context.m_medicine_type.Add(item);
                    this.context.SaveChanges();
                });
            }
            return Ok(new { message = "Medicine Category add successfully." });
        }        

        [HttpPut]
        [Route("Edit/{id}")]
        public async Task<IActionResult> EditCategory(string id, MedicineType item)
        {
            item.id = new ObjectId(id);
            // Validate the incoming model.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if patient exists
            MedicineType? editItem = await this.context.m_medicine_type.FirstOrDefaultAsync(
                    m => m.id == item.id);
            if (editItem == null)
            {
                return NotFound(new { message = "Medicine Category not found." });
            }
            else
            {
                await Task.Run(() =>
                {
                    this.context.m_medicine_type.Entry(editItem).CurrentValues.SetValues(item);
                    this.context.SaveChanges();
                });

                return Ok(item);
            }
        }
    }
}