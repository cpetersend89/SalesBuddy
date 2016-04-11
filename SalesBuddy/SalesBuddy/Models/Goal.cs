using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SalesBuddy.Models;
using SalesBuddy.Models.Salesforce1;

namespace SalesBuddy.Models
{
    public class Goal
    {
        [Key]
        public int GoalId { get; set; }
        [Display(Name = "Employee")]
        [EmailAddress]
        public string UserEmail { get; set; }

        [Display(Name = "Auto Goal")]
        [DataType(DataType.Currency)]
        public decimal AutoGoal { get; set; }

        [Display(Name = "Auto Actual")]
        [DataType(DataType.Currency)]
        public decimal AutoActual { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.##}%")]
        [Display(Name = "Auto Percentage")]
        public decimal AutoPercentage { get; set; }

        [Display(Name = "Mortgage Goal")]
        [DataType(DataType.Currency)]
        public decimal MortgageGoal { get; set; }

        [Display(Name = "Mortgage Actual")]
        [DataType(DataType.Currency)]
        public decimal MortgageActual { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.##}%")]
        [Display(Name = "Mortgage Percentage")]
        public decimal MortgagePercentage { get; set; }

        [Display(Name = "Credit Card Goal")]
        [DataType(DataType.Currency)]
        public decimal CreditCardGoal { get; set; }

        [Display(Name = "Credit Card Actual")]
        [DataType(DataType.Currency)]
        public decimal CreditCardActual { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.##}%")]
        [Display(Name = "Credit Percentage")]
        public decimal CreditCardPercentage { get; set; }

        [DisplayFormat(DataFormatString = "{0:0}")]
        [Display(Name = "Checking Goal")]
        public decimal CheckingGoal { get; set; }

        [DisplayFormat(DataFormatString = "{0:0}")]
        [Display(Name = "Checking Actual")]
        public decimal CheckingActual { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.##}%")]
        [Display(Name = "Checking Percentage")]
        public decimal CheckingPercentage { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

    }
}