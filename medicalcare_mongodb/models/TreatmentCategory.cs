using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;

namespace medicalcare_mongodb.models
{
    [Collection("m_treatment_category")]
    public class TreatmentCategory
    {
        [Key]
        public ObjectId id;

        public string? name_en{get;set;}
        public string? name_vn{get;set;}
        public string? name_jp{get;set;}
        public string? description{get;set;}

        public decimal? price{get;set;}

        public string id_guid
        {
            get
            {
                return id.ToString();
            }
        }
    }
}