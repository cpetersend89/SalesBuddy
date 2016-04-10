using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SalesBuddy.Models.Salesforce1;
using Salesforce.Common.Attributes;

namespace SalesBuddy.Models
{
    public class Product
    {
        [Key]
        //[Createable(false), Updateable(false)]
        public int ProductId { get; set; }

        [Display(Name = "Type")]
        [StringLength(255)]
        public string ProductType { get; set; }

        [Display(Name = "Product Code")]
        [StringLength(255)]
        public string ProductCode { get; set; }

        [Display(Name = "Date Closed")]
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Auto Loan")]
        [DataType(DataType.Currency)]
        public decimal AutoLoan { get; set; }

        [Display(Name = "Mortgage Loan")]
        [DataType(DataType.Currency)]
        public decimal MortgageLoan { get; set; }

        [Display(Name = "Credit Card")]
        [DataType(DataType.Currency)]
        public decimal CreditCard { get; set; }

        [Display(Name = "Checking Account")]
        public int CheckingAccount { get; set; }

        [Display(Name = "Employee")]
        public string UserEmail { get; set; }
    }
}