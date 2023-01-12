using System.ComponentModel.DataAnnotations;

namespace AASA_Back_End.Models
{
    public class Portfolio : BaseEntity
    {
        public string ImageFirst { get; set; }

        public string ImageSecond { get; set; }

        public string ImageThird { get; set; }
        public string ImageFourth { get; set; }
        public string ImageFifth { get; set;}
        public string ImageSixth { get; set; }
        public string ImageSeventh { get; set; }
        public string ImageEighth { get; set; }
        public string ImageNinth { get; set;}

        [Required]
        [StringLength(40, ErrorMessage = "Please shorten your description")]
        public string TitleFirst { get; set; }
        public string TitleSecond { get; set; }
        public string TitleThird { get; set; }
        public string TitleFourth { get; set; }
        public string TitleFifth { get; set; }
        public string TitleSixth { get; set; }
        public string TitleSeventh { get; set; }
        public string TitleEighth { get; set; }
        public string TitleNinth { get; set;}
     


    }
    
    
}
