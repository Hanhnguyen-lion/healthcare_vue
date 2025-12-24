using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;

namespace medicalcare_mongodb.models
{
    [Collection("h_billing")]
    public class Billing
    {
        [Key]
        public ObjectId id;

        public ObjectId? patient_id{get;set;}
        public ObjectId? doctor_id{get;set;}
        public ObjectId? department_id{get;set;}
        public ObjectId? appointment_id{get;set;}

        public DateTime? admission_date{get;set;}
        public DateTime? discharge_date{get;set;}
        public DateTime? billing_date{get;set;}
        public string? diagnostic{get;set;}

        public string? notes{get;set;}
        public string? status{get;set;}
        public int? days{get;set;}
        public decimal? amount{get;set;}
        public string? id_guid
        {
            get
            {
                return id.ToString();
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

        string? _department_id_guid;
        public string? department_id_guid
        {
            get
            {
                if (_department_id_guid != null)
                    return _department_id_guid;
                return department_id.ToString();
            }
            set
            {
                _department_id_guid = value;
            }
        }

        string? _appointment_id_guid;
        public string? appointment_id_guid
        {
            get
            {
                if (_appointment_id_guid != null)
                    return _appointment_id_guid;
                return appointment_id.ToString();
            }
            set
            {
                _appointment_id_guid = value;
            }
        }
    }
}