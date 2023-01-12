using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AASA_Back_End.Models
{
    public class Product : BaseEntity
    {
        public string Title { get; set; }
        public string Image { get; set; }
        public Type Type { get; set; }
        public int TypeId { get; set; }

        [Required]
        [NotMapped]
        public IFormFile Photo { get; set; }  
    }
}
