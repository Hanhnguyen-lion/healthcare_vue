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
                        var name_en = item["name_en"].ToString();
                        var name_jp = item["name_jp"].ToString();
                        var name_vn = item["name_vn"].ToString();
                        var description = item["description"].ToString();
                        var newItem = new MedicineType{
                            name_en = name_en,
                            name_jp = name_jp,
                            name_vn = name_vn,
                            description = description
                        };
                        if (string.IsNullOrEmpty(id_guid))
                        {
                            this.context.m_medicine_type.Add(newItem);
                        }
                        else
                        {
                            MedicineType? medicine = this.context.m_medicine_type.FirstOrDefault(m => m.id == new ObjectId(id_guid));
                            if (medicine == null)
                            {
                                this.context.m_medicine_type.Add(newItem);
                            }
                            else
                            {
                                newItem.id = new ObjectId(id_guid);
                                this.context.m_medicine_type.Entry(medicine).CurrentValues.SetValues(newItem);
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