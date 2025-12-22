using medicalcare_mongodb.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace medicalcare_mongodb.controllers
{
    [ApiController]
    [Route("Medicalcare/api/[controller]")]    
    public class DoctorsController: BaseController
    {

        public DoctorsController(MedicalcareDbContext context): base(context)
        {
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add(Doctor data)
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

                await Task.Run(() =>
                {
                    this.context.m_doctor.Add(data);
                    this.context.SaveChanges();
                });
            }
            return Ok(new { message = "Doctor add successfully." });
        }        

        [HttpPut]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(string id, Doctor dataInput)
        {
            dataInput.id = new ObjectId(id);
            dataInput.hospital_id = (string.IsNullOrEmpty(dataInput.hospital_id_guid)) ? 
                null : new ObjectId(dataInput.hospital_id_guid);
            // Validate the incoming model.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Doctor? item = await this.context.m_doctor.FirstOrDefaultAsync(
                    m => m.id == dataInput.id);
            if (item == null)
            {
                return NotFound(new { message = "Doctor not found." });
            }
            else
            {
                await Task.Run(() =>
                {
                    this.context.m_doctor.Entry(item).CurrentValues.SetValues(dataInput);
                    this.context.SaveChanges();
                });

                return Ok(item);
            }
        }
    }
}