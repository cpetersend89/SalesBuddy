using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SalesBuddy.Models;

namespace SalesBuddy.Controllers
{
    public class Goal
    {
        [Key]
        public int GoalId { get; set; }

        [Display(Name = "Auto Goal")]
        [DataType(DataType.Currency)]
        public decimal AutoGoal { get; set; }

        [Display(Name = "Auto Actual")]
        [DataType(DataType.Currency)]
        public Product AutoActual { get; set; }

        [Display(Name = "Percentage")]
        public decimal AutoPercentage { get; set; }

        [Display(Name = "Mortgage Goal")]
        [DataType(DataType.Currency)]
        public decimal MortgageGoal { get; set; }

        [Display(Name = "Mortgage Actual")]
        [DataType(DataType.Currency)]
        public Product MortgageActual { get; set; }

        [Display(Name = "Credit Card Goal")]
        [DataType(DataType.Currency)]
        public decimal CreditCardGoal { get; set; }

        [Display(Name = "Credit Card Actual")]
        [DataType(DataType.Currency)]
        public Product CreditCardActual { get; set; }

        [Display(Name = "Checking Goal")]
        public int CheckingGoal { get; set; }

        [Display(Name = "Checking Actual")]
        public Product CheckingActual { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

    }
}