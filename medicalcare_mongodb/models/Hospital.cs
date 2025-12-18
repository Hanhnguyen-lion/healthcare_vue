using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;

namespace medicalcare_mongodb.models
{
    [Collection("m_hospital")]
    public class Hospital
    {
        [Key]
        public ObjectId id;

        public string? name{get;set;}
	    public string? description{get;set;}
        public string? country{get;set;}
        public string? address{get;set;}
        public string? phone{get;set;}
        public string? email{get;set;}

        public string hospital_id_guid
        {
            get
            {
                return id.ToString();
            }
        }
    }
}