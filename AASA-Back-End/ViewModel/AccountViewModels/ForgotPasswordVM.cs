using System.ComponentModel.DataAnnotations;

namespace AASA_Back_End.ViewModel.AccountViewModels
{
    public class ForgotPasswordVM
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

    }
}
