using System.ComponentModel.DataAnnotations;

namespace MovieStore.Core.Models.Request
{
    public class UserRegisterRequestModel
    {
        // DataAnnotation are useful validation in ASP.NET Core/Framework
        [Required]
        [EmailAddress]
        [StringLength(50)]
        public string Email { get; set; }
        
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        
        [Required]
        [StringLength(20, ErrorMessage = "Make sure password length is between 8 and 20", MinimumLength = 8)]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}$", ErrorMessage = "Password should at least have one upper, lower, number and special character")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}