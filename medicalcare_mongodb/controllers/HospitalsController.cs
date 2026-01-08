using medicalcare_mongodb.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace medicalcare_mongodb.controllers
{
    [ApiController]
    [Route("Medicalcare/api/[controller]")]    
    public class HospitalsController: BaseController
    {
        public HospitalsController(MedicalcareDbContext context): base(context)
        {
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
                await Task.Run(() =>
                {
                    foreach (var item in data)
                    {
                        var id_guid = item["id"].ToString();
                        var name = item["name"].ToString();
                        var address = item["address"].ToString();
                        var email = item["email"].ToString();
                        var phone = item["phone"].ToString();
                        var description = item["description"].ToString();
                        var country = item["country"].ToString();
                        var newItem = new Hospital{
                            name = name,
                            address = address,
                            email = email,
                            phone = phone,
                            country = country,
                            description = description
                        };
                        if (string.IsNullOrEmpty(id_guid))
                        {
                            this.context.m_hospital.Add(newItem);
                        }
                        else
                        {
                            Hospital? medicine = this.context.m_hospital.FirstOrDefault(m => m.id == new ObjectId(id_guid));
                            if (medicine == null)
                            {
                                this.context.m_hospital.Add(newItem);
                            }
                            else
                            {
                                newItem.id = new ObjectId(id_guid);
                                this.context.m_hospital.Entry(medicine).CurrentValues.SetValues(newItem);
                            }
                        }
                    }
                    this.context.SaveChanges();
                });

            }
            return Ok(new { message = "Import successfully." });
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
    }
}