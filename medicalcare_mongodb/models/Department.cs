using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
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
        public string? doctor_id_guid
        {
            get
            {
                return doctor_id.ToString();
            }
        }
    }
}