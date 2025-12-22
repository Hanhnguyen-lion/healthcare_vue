using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;

namespace medicalcare_mongodb.models
{
    [Collection("m_medicine")]
    public class Medicine
    {
        [Key]
        public ObjectId id;

        public ObjectId? category_id{get;set;}
        public string? name{get;set;}
        public string? type{get;set;}
        public decimal? price{get;set;}

        public string? id_guid
        {
            get
            {
                return id.ToString();
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