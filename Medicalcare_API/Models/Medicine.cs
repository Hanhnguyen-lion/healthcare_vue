
using System.ComponentModel.DataAnnotations;

namespace Medicalcare_API.Models{
    public class v_medicine{
        [Key]
        public int id{get;set;}
        public int? category_id{get;set;}

        [Required]
        public string? name{get;set;}
        
        public string? medicine_type{get;set;}

        public decimal price{get;set;}
  }
}