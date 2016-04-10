using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Salesforce.Common.Attributes;

namespace SalesBuddy.Models
{
    public class LeadViewModel
    {
        [Key]
        [Display(Name = "Lead ID")]
        [Createable(false), Updateable(false)]
        public string Id { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(80)]
        public string LastName { get; set; }

        [Display(Name = "First Name")]
        [StringLength(40)]
        public string FirstName { get; set; }

        public string Salutation { get; set; }
        [StringLength(255)]
        public string Company { get; set; }

        public string Street { get; set; }

        [StringLength(40)]
        public string City { get; set; }

        [Display(Name = "State/Province")]
        [StringLength(80)]
        public string State { get; set; }

        [Display(Name = "Zip/Postal Code")]
        [StringLength(20)]
        public string PostalCode { get; set; }

        [Phone]
        public string Phone { get; set; }

        [Display(Name = "Mobile Phone")]
        [Phone]
        public string MobilePhone { get; set; }

        [Phone]
        public string Fax { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        [Display(Name = "Created Date")]
        [Createable(false), Updateable(false)]
        public DateTimeOffset CreatedDate { get; set; }

        [Display(Name = "Created By")]
        public string UserEmail__c { get; set; }
    }
}