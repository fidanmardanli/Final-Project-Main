namespace AASA_Back_End.Models
{
    public class Category : BaseEntity
    {
        public string Header { get; set; }
        public string Description { get; set; }
        public string ImageFirst { get; set; }
        public string ImageSecond { get; set; }
        public string ImageThird { get; set; }

        public string TitleFirst { get; set; }
        public string TitleSecond { get; set; }
        public string TitleThird { get; set; }
    }
}
