using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Medicalcare_API.Helpers;
using Medicalcare_API.Models;
using System.Text.Json;

namespace Medicalcare_API.Controllers{

    [ApiController]
    [Route("Medicalcare/api/[controller]")]
    public class AppointmentsController: ControllerBase{
        readonly DataContext context;
        public AppointmentsController(DataContext context){
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAppoiments()
        {
            var data = await this.context.v_appointment.ToListAsync();

            return Ok(data);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            AppointmentDTO? item = await this.context.h_appointment.FirstOrDefaultAsync(
                    m => m.id == id);
            if (item == null)
            {
                return NotFound(new { message = "Appoiment not found." });
            }
            return Ok(item);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add(AppointmentDTO patient)
        {
            // Validate the incoming model.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (patient != null)
            {

                await Task.Run(() =>
                {
                    this.context.h_appointment.Add(patient);
                    this.context.SaveChanges();
                });
            }
            return Ok(new { message = "Appoiment add successfully." });
        }        

        [HttpPut]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(int id, AppointmentDTO patient)
        {
            // Validate the incoming model.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != patient.id)
            {
                return BadRequest("ID mismatch in the URL and body.");
            }
            // Check if patient exists
            AppointmentDTO? item = await this.context.h_appointment.FirstOrDefaultAsync(
                    m => m.id == patient.id);
            if (item == null)
            {
                return NotFound(new { message = "Appoiment not found." });
            }
            else
            {
                await Task.Run(() =>
                {
                    this.context.h_appointment.Entry(item).CurrentValues.SetValues(patient);
                    this.context.SaveChanges();
                });

                return Ok(item);
            }
        }
 
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // Validate the incoming model.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if patient exists
            AppointmentDTO? item = await this.context.h_appointment.FirstOrDefaultAsync(
                    m => m.id == id);
            if (item == null)
            {
                return NotFound(new { message = "Appoiment not found." });
            }
            else
            {
                await Task.Run(() =>
                {
                    this.context.h_appointment.Remove(item);
                    this.context.SaveChanges();
                });

                return Ok(new {message = "Appoiment deleted "});
            }
        }

    }
}