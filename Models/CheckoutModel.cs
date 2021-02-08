using System;
using System.ComponentModel.DataAnnotations;

namespace dt102g_moment2.Models
{
    // Model with annotations, validation and custom error messages. 
    public class CheckoutModel
    {
        [Required(ErrorMessage = "Ange ditt för och efternamn!")]
        [Display(Name = "Namn")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Ange ditt personnummer!")]
        [Display(Name = "Personnummer")]
        public string Ssn { get; set; }

        [Required(ErrorMessage = "Ange din gautadress!")]
        [Display(Name = "Adress")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Ange din postkod!")]
        [Display(Name = "Postkod")]
        public string Zip { get; set; }

        [Required(ErrorMessage = "Ange din stad!")]
        [Display(Name = "Stad")]
        public string City { get; set; }

        [Phone(ErrorMessage = "Numret du har angett har inte rätt format!")]
        [Display(Name = "Telefon")]
        public string Phone { get; set; }

        [EmailAddress]
        [Display(Name = "E-post")]
        public string Email { get; set; }

        [Range(typeof(bool), "true", "true", ErrorMessage = "Du måste acceptera villkoren!")]
        [Display(Name = "Acceptera villkor")]
        public bool Accept { get; set; }
    }
}
