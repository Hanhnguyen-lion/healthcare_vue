using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;

namespace medicalcare_mongodb.models
{
    [Collection("m_account")]
    public class Account
    {
        [Key]
        public ObjectId id;
        public ObjectId? hospital_id;

        public string? password{get;set;}
	    public string? first_name{get;set;}
        public string? last_name{get;set;}

        public DateTime? dob{get;set;}
        public string? gender{get;set;}
        public string? address{get;set;}

        public string? phone{get;set;}
        public string? email{get;set;}
        public string? account_type{get;set;}
        public string? token{get;set;}
        
        public string account_id_guid
        {
            get
            {
                return id.ToString();
            }
        }
        
        public string? hospital_id_guid
        {
            get
            {
                return hospital_id.ToString();
            }
        }
    }
}