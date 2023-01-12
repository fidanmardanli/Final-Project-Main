using AASA_Back_End.Models;
using System.Collections;
using System.Collections.Generic;

namespace AASA_Back_End.ViewModel
{
    public class ShopVM
    {
        public IEnumerable<Type> Types { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
