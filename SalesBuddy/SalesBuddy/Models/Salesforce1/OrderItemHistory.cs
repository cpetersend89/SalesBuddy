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
    public class OrderItemHistory
    {
        [Key]
        [Display(Name = "Order Product History ID")]
        [Createable(false), Updateable(false)]
        public string Id { get; set; }

        [Display(Name = "Deleted")]
        [Createable(false), Updateable(false)]
        public bool IsDeleted { get; set; }

        [Display(Name = "Order Product ID")]
        [Createable(false), Updateable(false)]
        public string OrderItemId { get; set; }

        [Display(Name = "Created By ID")]
        [Createable(false), Updateable(false)]
        public string CreatedById { get; set; }

        [Display(Name = "Created Date")]
        [Createable(false), Updateable(false)]
        public DateTimeOffset CreatedDate { get; set; }

        [Display(Name = "Changed Field")]
        [Createable(false), Updateable(false)]
        public string Field { get; set; }

        [Display(Name = "Old Value")]
        [Createable(false), Updateable(false)]
        public object OldValue { get; set; }

        [Display(Name = "New Value")]
        [Createable(false), Updateable(false)]
        public object NewValue { get; set; }

    }
}
