using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;

namespace medicalcare_mongodb.models
{
    [Collection("h_appointment")]
    public class Appointment
    {
        [Key]
        public ObjectId id;

        public ObjectId? doctor_id{get;set;}
        public ObjectId? patient_id{get;set;}

        public DateTime? appointment_date{get;set;}
        public string? reason_to_visit{get;set;}
        public string? status{get;set;}
        public int? hour{get;set;}
        public int? minute{get;set;}
        public string? id_guid
        {
            get
            {
                return id.ToString();
            }
        }        

        string? _doctor_id_guid;
        public string? doctor_id_guid
        {
            get
            {
                if (_doctor_id_guid != null)
                    return _doctor_id_guid;
                return doctor_id.ToString();
            }
            set
            {
                _doctor_id_guid = value;
            }
        }

        string? _patient_id_guid;
        public string? patient_id_guid
        {
            get
            {
                if (_patient_id_guid != null)
                    return _patient_id_guid;
                return patient_id.ToString();
            }
            set
            {
                _patient_id_guid = value;
            }
        }
    }
}