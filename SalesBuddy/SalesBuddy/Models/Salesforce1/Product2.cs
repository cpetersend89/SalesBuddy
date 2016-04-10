using Newtonsoft.Json;
using Salesforce.Common.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesBuddy.Models.Salesforce1
{
    public class Product2
    {
        [Key]
        [Display(Name = "Product ID")]
        [Createable(false), Updateable(false)]
        public string Id { get; set; }

        [Display(Name = "Product Name")]
        [StringLength(255)]
        public string Name { get; set; }

        [Display(Name = "Product Code")]
        [StringLength(255)]
        public string ProductCode { get; set; }

        [Display(Name = "Product Description")]
        public string Description { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Created Date")]
        [Createable(false), Updateable(false)]
        public DateTimeOffset CreatedDate { get; set; }

        [Display(Name = "Created By ID")]
        [Createable(false), Updateable(false)]
        public string CreatedById { get; set; }

        [Display(Name = "Last Modified Date")]
        [Createable(false), Updateable(false)]
        public DateTimeOffset LastModifiedDate { get; set; }

        [Display(Name = "Last Modified By ID")]
        [Createable(false), Updateable(false)]
        public string LastModifiedById { get; set; }

        [Display(Name = "System Modstamp")]
        [Createable(false), Updateable(false)]
        public DateTimeOffset SystemModstamp { get; set; }

        [Display(Name = "Product Family")]
        public string Family { get; set; }

        [Display(Name = "Deleted")]
        [Createable(false), Updateable(false)]
        public bool IsDeleted { get; set; }

        [Display(Name = "Last Viewed Date")]
        [Createable(false), Updateable(false)]
        public DateTimeOffset? LastViewedDate { get; set; }

        [Display(Name = "Last Referenced Date")]
        [Createable(false), Updateable(false)]
        public DateTimeOffset? LastReferencedDate { get; set; }

        [Display(Name = "Auto Loan Amount")]
        public decimal AutoLoan__c { get; set; }

        [Display(Name = "Mortgage Loan Amount")]
        public decimal MortgageLoan__c { get; set; }

        [Display(Name = "Credit Card Amount")]
        public decimal CreditCard__c { get; set; }

        [Display(Name = "Number of Checking Accounts")]
        public int CheckingAccount__c { get; set; }

        [Display(Name = "Employee")]
        public string UserEmail__c { get; set; }

        public string AutoLoan { get; set; } = "AUT";

        public string MortgageLoan { get; set; } = "MTG";
        public string CreditCard { get; set; } = "CRD";
        public string CheckingAccount { get; set; } = "CKG";

    }
}
