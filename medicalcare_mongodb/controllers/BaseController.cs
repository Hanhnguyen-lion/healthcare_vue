using medicalcare_mongodb.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace medicalcare_mongodb.controllers
{
    [Controller]
    public abstract class BaseController: ControllerBase
    {
        public readonly MedicalcareDbContext context;

        public BaseController(MedicalcareDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            var controllerName = this.GetType().Name;
            if (controllerName.IndexOf(MedicalcareDbContext.Department, StringComparison.OrdinalIgnoreCase) != -1)
                return Ok( await this.context.m_department.ToListAsync());
            else if (controllerName.IndexOf(MedicalcareDbContext.Hospital, StringComparison.OrdinalIgnoreCase) != -1)
                return Ok( await this.context.m_hospital.ToListAsync());
            else if (controllerName.IndexOf(MedicalcareDbContext.TreatmentsCategory, StringComparison.OrdinalIgnoreCase) != -1)
                return Ok( await this.context.m_treatment_category.ToListAsync());
            else if (controllerName.IndexOf(MedicalcareDbContext.MedicinesCategory, StringComparison.OrdinalIgnoreCase) != -1)
                return Ok( await this.context.m_medicine_type.ToListAsync());
            else if (controllerName.IndexOf(MedicalcareDbContext.Medicine, StringComparison.OrdinalIgnoreCase) != -1)
                return Ok( await this.context.GetMedicines());
            else if (controllerName.IndexOf(MedicalcareDbContext.Patient, StringComparison.OrdinalIgnoreCase) != -1){
                var data = await this.context.GetPatients();
                return Ok(data);
            }
            else if (controllerName.IndexOf(MedicalcareDbContext.Doctor, StringComparison.OrdinalIgnoreCase) != -1){
                var data = await this.context.GetDoctors();
                return Ok(data);
            }
            else if (controllerName.IndexOf(MedicalcareDbContext.Appointment, StringComparison.OrdinalIgnoreCase) != -1){
                var data = await this.context.GetAppointments();
                return Ok(data);
            }
            return Ok( await this.context.m_account.ToListAsync());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var controllerName = this.GetType().Name;
            object? item;
            item = await this.context.GetItem(id, controllerName);
            if (item == null)
            {
                return NotFound(new { message = "Item not found." });
            }
            else
            {
                if (controllerName.IndexOf(MedicalcareDbContext.Department, StringComparison.OrdinalIgnoreCase) != -1)
                    item = (Department?) item;
                else if (controllerName.IndexOf(MedicalcareDbContext.Hospital, StringComparison.OrdinalIgnoreCase) != -1)
                    item = (Hospital?) item;
                else if (controllerName.IndexOf(MedicalcareDbContext.TreatmentsCategory, StringComparison.OrdinalIgnoreCase) != -1)
                    item = (TreatmentCategory?) item;
                else if (controllerName.IndexOf(MedicalcareDbContext.MedicinesCategory, StringComparison.OrdinalIgnoreCase) != -1)
                    item = (MedicineType?) item;
                else if (controllerName.IndexOf(MedicalcareDbContext.Medicine, StringComparison.OrdinalIgnoreCase) != -1)
                    item = (Medicine?) item;
                else if (controllerName.IndexOf(MedicalcareDbContext.Patient, StringComparison.OrdinalIgnoreCase) != -1)
                    item = (Patient?) item;
                else if (controllerName.IndexOf(MedicalcareDbContext.Doctor, StringComparison.OrdinalIgnoreCase) != -1)
                    item = (Doctor?) item;
                else if (controllerName.IndexOf(MedicalcareDbContext.Appointment, StringComparison.OrdinalIgnoreCase) != -1)
                    item = (Appointment?) item;
            }
            return Ok(item);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }
            var name = this.GetType().Name;
            var isDelete = await this.context.DeleteItem(id, name);
            if (isDelete)
                return Ok(new {message = "Item deleted "});
            else
                return NotFound(new { message = "Item not found." });
        }
    }
}