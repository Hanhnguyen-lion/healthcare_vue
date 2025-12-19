using medicalcare_mongodb.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace medicalcare_mongodb.controllers
{
    [ApiController]
    [Route("Medicalcare/api/[controller]")]    
    public class DepartmentsController: ControllerBase
    {
        readonly MedicalcareDbContext context;

        public DepartmentsController(MedicalcareDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            var data = await this.context.m_department.ToListAsync();

            return Ok(data);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            Department? item = await this.context.m_department.FirstOrDefaultAsync(
                    m => m.id == new ObjectId(id));
            if (item == null)
            {
                return NotFound(new { message = "Department not found." });
            }
            return Ok(item);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add(Department data)
        {
            // Validate the incoming model.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (data != null)
            {
                var newItem = new Department{
                    hospital_id = (data?.hospital_id_str == null) ? null: new ObjectId(data.hospital_id_str),
                    doctor_id = (data?.doctor_id_str == null) ? null : new ObjectId(data.doctor_id_str),
                    name = data?.name,
                    phone = data?.phone
                };

                await Task.Run(() =>
                {
                    this.context.m_department.Add(newItem);
                    this.context.SaveChanges();
                });
            }
            return Ok(new { message = "Department add successfully." });
        }        

        [HttpPut]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(string id, Department dataInput)
        {
            dataInput.id = new ObjectId(id);
            // Validate the incoming model.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Department? item = await this.context.m_department.FirstOrDefaultAsync(
                    m => m.id == dataInput.id);
            if (item == null)
            {
                return NotFound(new { message = "Department not found." });
            }
            else
            {
                await Task.Run(() =>
                {
                    this.context.m_department.Entry(item).CurrentValues.SetValues(dataInput);
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

            Department? item = await this.context.m_department.FirstOrDefaultAsync(
                    m => m.id ==  new ObjectId(id));
            if (item == null)
            {
                return NotFound(new { message = "Department not found." });
            }
            else
            {
                await Task.Run(() =>
                {
                    this.context.m_department.Remove(item);
                    this.context.SaveChanges();
                });

                return Ok(new {message = "Department deleted "});
            }
        }
    }
}