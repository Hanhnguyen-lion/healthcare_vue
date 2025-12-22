using medicalcare_mongodb.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace medicalcare_mongodb.controllers
{
    [ApiController]
    [Route("Medicalcare/api/[controller]")]    
    public class PatientsController: BaseController
    {

        public PatientsController(MedicalcareDbContext context): base(context)
        {
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add(Patient data)
        {

            // Validate the incoming model.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (data != null)
            {
                data.hospital_id = (string.IsNullOrEmpty(data.hospital_id_guid)) ?
                    null: new ObjectId(data.hospital_id_guid);

                string? code = data.code?.ToLower();
                // Check if the email already exists.
                Patient? item = await this.context.m_patient.FirstOrDefaultAsync(m => m.code == code);
                if (item != null)
                {
                    return Conflict(new { message = "Patient is already exists." });
                }
                
                await Task.Run(() =>
                {
                    this.context.m_patient.Add(data);
                    this.context.SaveChanges();
                });
            }
            return Ok(new { message = "Patient add successfully." });
        }        

        [HttpPut]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(string id, Patient dataInput)
        {
            dataInput.id = new ObjectId(id);
            dataInput.hospital_id = (string.IsNullOrEmpty(dataInput.hospital_id_guid)) ? 
                null : new ObjectId(dataInput.hospital_id_guid);
            // Validate the incoming model.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Patient? item = await this.context.m_patient.FirstOrDefaultAsync(
                    m => m.id == dataInput.id);
            if (item == null)
            {
                return NotFound(new { message = "Patient not found." });
            }
            else
            {
                await Task.Run(() =>
                {
                    this.context.m_patient.Entry(item).CurrentValues.SetValues(dataInput);
                    this.context.SaveChanges();
                });

                return Ok(item);
            }
        }
    }
}