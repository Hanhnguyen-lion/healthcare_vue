using medicalcare_mongodb.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace medicalcare_mongodb.controllers
{
    [ApiController]
    [Route("Medicalcare/api/[controller]")]    
    public class TreatmentsCategoryController: BaseController
    {
        public TreatmentsCategoryController(MedicalcareDbContext context) : base(context)
        {
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddCategory(TreatmentCategory item)
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
                    this.context.m_treatment_category.Add(item);
                    this.context.SaveChanges();
                });
            }
            return Ok(new { message = "Medicine Category add successfully." });
        }        

        [HttpPut]
        [Route("Edit/{id}")]
        public async Task<IActionResult> EditCategory(string id, TreatmentCategory item)
        {
            item.id = new ObjectId(id);
            // Validate the incoming model.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if patient exists
            TreatmentCategory? editItem = await this.context.m_treatment_category.FirstOrDefaultAsync(
                    m => m.id == item.id);
            if (editItem == null)
            {
                return NotFound(new { message = "Medicine Category not found." });
            }
            else
            {
                await Task.Run(() =>
                {
                    this.context.m_treatment_category.Entry(editItem).CurrentValues.SetValues(item);
                    this.context.SaveChanges();
                });

                return Ok(item);
            }
        }
    }
}