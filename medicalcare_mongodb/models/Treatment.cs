using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;

namespace medicalcare_mongodb.models
{
    [Collection("m_treatment")]
    public class Treatment
    {
        [Key]
        public ObjectId id;

        public ObjectId? billing_id{get;set;}
        public ObjectId? category_id{get;set;}

        public DateTime? treatment_date{get;set;}
        public string? treatment_type{get;set;}
        public string? description{get;set;}
        public int? quantity{get;set;}
        public decimal? amount{get;set;}
        public string? id_guid
        {
            get
            {
                return id.ToString();
            }
        }        

        string? _billing_id_guid;
        public string? billing_id_guid
        {
            get
            {
                if (_billing_id_guid != null)
                    return _billing_id_guid;
                return billing_id.ToString();
            }
            set
            {
                _billing_id_guid = value;
            }
        }

        string? _category_id_guid;
        public string? category_id_guid
        {
            get
            {
                if (_category_id_guid != null)
                    return _category_id_guid;
                return category_id.ToString();
            }
            set
            {
                _category_id_guid = value;
            }
        }
    }
}