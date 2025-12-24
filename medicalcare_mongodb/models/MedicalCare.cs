using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;

namespace medicalcare_mongodb.models
{
    public class MedicalCare
    {
        public string? billing_id{get;set;}
        public string? patient_id{get;set;}
        public string? doctor_id{get;set;}
        public string? department_id{get;set;}
        public string? hospital_id{get;set;}
        public int? admission_month{get;set;}
        public int? admission_year{get;set;}

        public DateTime? admission_date{get;set;}
        public string? diagnostic{get;set;}
        public string? notes{get;set;}
        public string? doctor_name{get;set;}
        public string? department_name{get;set;}
        public string? patient_name{get;set;}
        public string? patient_code{get;set;}
    }
}