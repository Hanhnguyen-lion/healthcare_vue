using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;

namespace medicalcare_mongodb.models
{
    [Collection("h_prescription")]
    public class Prescription
    {
        [Key]
        public ObjectId id;

        public ObjectId? billing_id{get;set;}
        public ObjectId? medicine_id{get;set;}

        public DateTime? prescription_date{get;set;}
        public string? notes{get;set;}
        public string? dosage{get;set;}
        public string? medicine_type{get;set;}
        public string? duration_type{get;set;}
        public int? duration{get;set;}
        public int? quantity{get;set;}
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

        string? _medicine_id_guid;
        public string? medicine_id_guid
        {
            get
            {
                if (_medicine_id_guid != null)
                    return _medicine_id_guid;
                return medicine_id.ToString();
            }
            set
            {
                _medicine_id_guid = value;
            }
        }
    }
}