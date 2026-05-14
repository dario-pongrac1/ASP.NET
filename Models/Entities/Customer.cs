using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace lab_1.Models.Entities
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Ime je obavezno")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Ime mora sadržavati između 2 i 100 znakova")]
        [Display(Name = "Ime")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Prezime je obavezno")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Prezime mora sadržavati između 2 i 100 znakova")]
        [Display(Name = "Prezime")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Telefonski broj je obavezan")]
        [Phone(ErrorMessage = "Telefonski broj nije valjan")]
        [Display(Name = "Telefonski broj")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email je obavezan")]
        [EmailAddress(ErrorMessage = "Email nije valjan")]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Adresa je obavezna")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Adresa mora sadržavati između 5 i 200 znakova")]
        [Display(Name = "Adresa")]
        public string Address { get; set; } = string.Empty;

        [Display(Name = "Datum registracije")]
        public DateTime RegisteredAt { get; set; }

        [Display(Name = "Datum brisanja")]
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
        public virtual ICollection<ServiceOrder> ServiceOrders { get; set; } = new List<ServiceOrder>();
    }
}
