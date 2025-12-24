using System.Collections;
using medicalcare_mongodb.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace medicalcare_mongodb.controllers
{
    [ApiController]
    [Route("Medicalcare/api/[controller]")]    
    public class MedicalCaresController: ControllerBase
    {
        readonly MedicalcareDbContext context;
        public MedicalCaresController(MedicalcareDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [Route("Patients")]
        public async Task<IActionResult> GetPatients()
        {
            var billings = await this.context.h_billing.ToArrayAsync();
            var patients = await this.context.m_patient.ToArrayAsync();
            var v_medicalcare = from b in billings
            join p in patients on b.patient_id equals p.id
            select new
            {
                patient_id = b.patient_id_guid,
                patient_code = $"{p.last_name} {p.first_name}",
                hospital_id = p.hospital_id_guid
            };

            var data = v_medicalcare.Select(li =>
                new {
                    li.patient_id, 
                    li.patient_code, 
                    li.hospital_id
                }).Distinct().ToList();
            return Ok(data);
        }

        [HttpGet]
        [Route("Search")]
        public async Task<IActionResult> SearchMedicalCares(
            string patient_id, 
            int admission_month,  
            int admission_year)
        {

            IDictionary? item = new Dictionary<string, object>();
            await Task.Run(() =>
            {
                item = this.context.GetMedicalDetails(
                    patient_id: patient_id,
                    admission_month: admission_month,
                    admission_year: admission_year);
            });
            return Ok(item);
        }
    }
}