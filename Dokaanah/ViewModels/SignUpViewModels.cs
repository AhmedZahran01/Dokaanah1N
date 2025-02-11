using System.ComponentModel.DataAnnotations;

namespace Dokaanah.PresentationLayer.ViewModels
{

    public class SignUpViewModels
    {
        [RegularExpression("[A-Za-z0-9]+")]
        //[RegularExpression("[a-zA-Z0-9]")]
        [Required(ErrorMessage = "User Name is Required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = " Invalid Email ")]
        [RegularExpression("^([\\w]*[\\w\\.]*(?!\\.)@gmail.com)")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [MinLength(5, ErrorMessage = "minmum Password is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "confirm Password is Required")]
        [Compare(nameof(Password), ErrorMessage = "confirm Password does not match Password")]
        [DataType(DataType.Password)]
        public string confirmPassword { get; set; }

        public string? Address { get; set; }

        public bool IsAgree { get; set; }
        public bool RemmeberMe { get; set; } = true;


    }
}
