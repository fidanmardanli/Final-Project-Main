using System.ComponentModel.DataAnnotations;

namespace AASA_Back_End.ViewModel.AccountViewModels
{
    public class LoginVM
    {
        [Required]
        public string UsernameOrEmail { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
