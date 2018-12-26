using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SampleMVC.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string UserName { get; set; }

        [Required]
        [MinLength(6,ErrorMessage ="Password should be atleast 6 chars.")]
        public string Password { get; set; }

        [Required]
        [Display(Name ="Company")]
        public int CompanyId { get; set; }

        [Required]
        public string AccountId { get; set; }

        [Required]
        [StringLength(12,MinimumLength =12,ErrorMessage ="Key should be 12 chars")]
        public string LicenseKey { get; set; }


        [Display(Name = "Master")]
        [Required]
        public int MasterId { get; set; }

        [Required]
        [Display(Name = "Address 1")]
        public string Address1 { get; set; }

        [Display(Name = "Address 2")]
        [Required]
        public string Address2 { get; set; }

        [Display(Name = "City Code")]
        [Required]
        public string CityCode { get; set; }

        [Display(Name = "Region Code")]
        [Required]
        public string RegionCode { get; set; }

        [Display(Name = "Country Code")]
        [Required]
        public string CountryCode { get; set; }

        [Display(Name = "Postal Code")]
        [Required]
        public string PostalCode { get; set; }


        //public virtual Company Company { get; set; }


    }
}