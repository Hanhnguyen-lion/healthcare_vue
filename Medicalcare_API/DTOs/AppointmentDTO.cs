
using System.ComponentModel.DataAnnotations;

namespace Medicalcare_API.Models{
    public class AppointmentDTO{
        [Key]
        public int id{get;set;}

        public string? reason_to_visit{get;set;}

        public string? status{get;set;}
        
        public DateTime? appointment_date{get;set;}

        public int? patient_id{get;set;}
        public int? doctor_id{get;set;}
  }
}