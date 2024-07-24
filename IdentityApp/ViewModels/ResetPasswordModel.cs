using System.ComponentModel.DataAnnotations;

namespace IdentityApp.ViewModels{

    public class ResetPasswordModel{

        [Required]
        public string Token {get;set;} = string.Empty;

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string? Email {get;set;} = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string? Password {get;set;} = string.Empty;

        [Required] 
        [DataType(DataType.Password)]
        [Compare(nameof(Password),ErrorMessage = "Parolalar eşileşmiyor!")]
        public string? ConfirmPassword {get;set;}= string.Empty;

    
    }
}