using Dokaanah.Models;
using System.ComponentModel.DataAnnotations;

namespace Dokaanah.PresentationLayer.ViewModels
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        public string? NameView { get; set; }

        [DataType(DataType.Password)]
        public string PasswordView { get; set; }

        [Compare(nameof(PasswordView), ErrorMessage = "confirm Password does not match Password")]
        [DataType(DataType.Password)]
        public string? confirmPasswordView { get; set; }

        public int? PhoneNumberView { get; set; }
        public string? AddressView { get; set; }
        public bool? isAgreeView { get; set; }


        public virtual string? Email { get; set; }
        public virtual bool EmailConfirmed { get; set; }

        public virtual string? UserName { get; set; }


        public List<Customer> CustomersViewModelProp { get; set; }

    }
}