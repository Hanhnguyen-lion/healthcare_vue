using System.Collections;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;

namespace medicalcare_mongodb.models
{
    [Collection("m_account")]
    public class Account
    {
        [Key]
        public ObjectId id{get;set;}
        public ObjectId? hospital_id{get;set;}

        public string? password{get;set;}
	    public string? first_name{get;set;}
        public string? last_name{get;set;}

        public DateTime? dob{get;set;}
        public string? gender{get;set;}
        public string? address{get;set;}

        public string? phone{get;set;}
        public string? email{get;set;}
        public string? account_type{get;set;}
        public string? role{get;set;}
        public string? token{get;set;}
        public DateTime create_date{get;set;}
        public string? reset_password_code{get;set;}
        public DateTime? reset_password_date{get;set;}
        
        public string id_guid
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