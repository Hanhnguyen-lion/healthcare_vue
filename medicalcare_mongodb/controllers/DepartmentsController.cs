using medicalcare_mongodb.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace medicalcare_mongodb.controllers
{
    [ApiController]
    [Route("Medicalcare/api/[controller]")]    
    public class DepartmentsController: BaseController
    {

        public DepartmentsController(MedicalcareDbContext context): base(context)
        {
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
                    hospital_id = (string.IsNullOrEmpty(data.hospital_id_guid)) ? null: new ObjectId(data.hospital_id_guid),
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
            dataInput.hospital_id = string.IsNullOrEmpty(dataInput.hospital_id_guid) ?
            null: new ObjectId(dataInput.hospital_id_guid);
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
    }
}