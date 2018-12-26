using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SampleMVC.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int CompanyId { get; set; }
        public string AccountId { get; set; }
        public string LicenseKey { get; set; }
        public int MasterId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string CityCode { get; set; }
        public string RegionCode { get; set; }
        public string CountryCode { get; set; }
        public string PostalCode { get; set; }


        //public virtual Company Company { get; set; }


    }
}