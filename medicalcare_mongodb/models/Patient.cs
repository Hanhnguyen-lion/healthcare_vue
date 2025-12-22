using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;

namespace medicalcare_mongodb.models
{
    [Collection("m_patient")]
    public class Patient
    {
        [Key]
        public ObjectId id;

        public ObjectId? hospital_id{get;set;}

        public string? code{get;set;}
        
        public string? first_name{get;set;}
        public string? last_name{get;set;}
        public DateTime? date_of_birth{get;set;}
        public string? gender{get;set;}
        public string? home_address{get;set;}
        public string? office_address{get;set;}
        public string? phone_number{get;set;}
        public string? email{get;set;}
        public string? job{get;set;}
        public string? emergency_contact_name{get;set;}
        public string? emergency_contact_phone{get;set;}
        public string? insurance_type{get;set;}
        public string? insurance_policy_number{get;set;}
        public string? insurance_provider{get;set;}
        public DateTime? insurance_expire{get;set;}
        public string? insurance_info{get;set;}
        public string? medical_history{get;set;}

        public string? id_guid
        {
            get
            {
                return id.ToString();
            }
        }        

        string? _hospital_id_guid;
        public string? hospital_id_guid
        {
            get
            {
                if (_hospital_id_guid != null)
                    return _hospital_id_guid;
                return hospital_id.ToString();
            }
            set
            {
                _hospital_id_guid = value;
            }
        }
    }
}