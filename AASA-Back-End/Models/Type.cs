using System.Collections.Generic;

namespace AASA_Back_End.Models
{
    public class Type :BaseEntity
    {
        public string Name { get; set; }
        public List<Product> Product { get; set; }
    }
}
