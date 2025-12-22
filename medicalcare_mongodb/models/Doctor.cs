using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;

namespace medicalcare_mongodb.models
{
    [Collection("m_doctor")]
    public class Doctor
    {
        [Key]
        public ObjectId id;

        public ObjectId? hospital_id{get;set;}

        public string? first_name{get;set;}
        public string? last_name{get;set;}
        public string? gender{get;set;}
        public string? phone{get;set;}
        public string? email{get;set;}
        public string? quanlification{get;set;}
        public string? job_specification{get;set;}

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