using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;

namespace medicalcare_mongodb.models
{
    [Collection("m_department")]
    public class Department
    {
        [Key]
        public ObjectId id;
        public ObjectId? hospital_id{get;set;}
        public ObjectId? doctor_id{get;set;}

        public string? name{get;set;}
	    public string? phone{get;set;}
        
        public string? id_guid
        {
            get
            {
                return id.ToString();
            }
        }
        
        public string? hospital_id_str{get;set;}
        
        public string? doctor_id_str{get;set;}
        public string? hospital_id_guid
        {
            get
            {
                return hospital_id.ToString();
            }
        }
        public string? doctor_id_guid
        {
            get
            {
                return doctor_id.ToString();
            }
        }
    }
}