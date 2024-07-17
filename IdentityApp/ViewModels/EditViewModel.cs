using System.ComponentModel.DataAnnotations;

namespace IdentityApp.ViewModels{

    public class EditViewModel{

        public string? Id {get;set;} 
        public string? FullName {get;set;} 

        [EmailAddress]
        public string? Email {get;set;}

        [DataType(DataType.Password)]
        public string? Password {get;set;} 
        [DataType(DataType.Password)]
        [Compare(nameof(Password),ErrorMessage = "Parolalar eşileşmiyor!")]
        public string? ConfirmPassword {get;set;}

    
    }
}