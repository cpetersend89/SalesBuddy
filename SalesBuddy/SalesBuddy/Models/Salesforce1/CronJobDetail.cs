﻿using Newtonsoft.Json;
using Salesforce.Common.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesBuddy.Models.Salesforce1
{
    public class CronJobDetail
    {
        [Key]
        [Display(Name = "Job ID")]
        [Createable(false), Updateable(false)]
        public string Id { get; set; }

        [Display(Name = "Job Name")]
        [StringLength(255)]
        [Createable(false), Updateable(false)]
        public string Name { get; set; }

        [Display(Name = "Type")]
        [Createable(false), Updateable(false)]
        public string JobType { get; set; }

    }
}
