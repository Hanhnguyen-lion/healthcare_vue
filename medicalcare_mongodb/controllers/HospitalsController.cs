using medicalcare_mongodb.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace medicalcare_mongodb.controllers
{
    [ApiController]
    [Route("Medicalcare/api/[controller]")]    
    public class HospitalsController: ControllerBase
    {
        readonly MedicalcareDbContext context;

        public HospitalsController(MedicalcareDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            var data = await this.context.m_hospital.ToListAsync();

            return Ok(data);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            Hospital? item = await this.context.m_hospital.FirstOrDefaultAsync(
                    m => m.id == new ObjectId(id));
            if (item == null)
            {
                return NotFound(new { message = "Hospital not found." });
            }
            return Ok(item);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add(Hospital patient)
        {
            // Validate the incoming model.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (patient != null)
            {
                string? name = patient.name?.ToLower();
                // Check if the email already exists.
                Hospital? item = await this.context.m_hospital.FirstOrDefaultAsync(m => m.name == name);
                if (item != null)
                {
                    return Conflict(new { message = "Hospital is already exists." });
                }

                await Task.Run(() =>
                {
                    this.context.m_hospital.Add(patient);
                    this.context.SaveChanges();
                });
            }
            return Ok(new { message = "Hospital add successfully." });
        }        

        [HttpPut]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(string id, Hospital dataInput)
        {
            dataInput.id = new ObjectId(id);
            // Validate the incoming model.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Hospital? item = await this.context.m_hospital.FirstOrDefaultAsync(
                    m => m.id == dataInput.id);
            if (item == null)
            {
                return NotFound(new { message = "Hospital not found." });
            }
            else
            {
                await Task.Run(() =>
                {
                    this.context.m_hospital.Entry(item).CurrentValues.SetValues(dataInput);
                    this.context.SaveChanges();
                });

                return Ok(item);
            }
        }
 
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            // Validate the incoming model.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Hospital? item = await this.context.m_hospital.FirstOrDefaultAsync(
                    m => m.id ==  new ObjectId(id));
            if (item == null)
            {
                return NotFound(new { message = "Hospital not found." });
            }
            else
            {
                await Task.Run(() =>
                {
                    this.context.m_hospital.Remove(item);
                    this.context.SaveChanges();
                });

                return Ok(new {message = "Hospital deleted "});
            }
        }
    }
}