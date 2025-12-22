using medicalcare_mongodb.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace medicalcare_mongodb.controllers
{
    [ApiController]
    [Route("Medicalcare/api/[controller]")]    
    public class AppointmentsController: BaseController
    {

        public AppointmentsController(MedicalcareDbContext context): base(context)
        {
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add(Appointment data)
        {

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

                await Task.Run(() =>
                {
                    this.context.h_appointment.Add(data);
                    this.context.SaveChanges();
                });
            }
            return Ok(new { message = "Appointment add successfully." });
        }        

        [HttpPut]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(string id, Appointment dataInput)
        {
            dataInput.id = new ObjectId(id);
            dataInput.doctor_id = (string.IsNullOrEmpty(dataInput.doctor_id_guid)) ? 
                null : new ObjectId(dataInput.doctor_id_guid);
            dataInput.patient_id = (string.IsNullOrEmpty(dataInput.patient_id_guid)) ? 
                null : new ObjectId(dataInput.patient_id_guid);
            // Validate the incoming model.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Appointment? item = await this.context.h_appointment.FirstOrDefaultAsync(
                    m => m.id == dataInput.id);
            if (item == null)
            {
                return NotFound(new { message = "Appointment not found." });
            }
            else
            {
                await Task.Run(() =>
                {
                    this.context.h_appointment.Entry(item).CurrentValues.SetValues(dataInput);
                    this.context.SaveChanges();
                });

                return Ok(item);
            }
        }
    }
}