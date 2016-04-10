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
    public class OpportunityCompetitor
    {
        [Key]
        [Display(Name = "Opportunity: Competitor ID")]
        [Createable(false), Updateable(false)]
        public string Id { get; set; }

        [Display(Name = "Opportunity ID")]
        [Updateable(false)]
        public string OpportunityId { get; set; }

        [Display(Name = "Competitor Name")]
        public string CompetitorName { get; set; }

        [StringLength(1000)]
        public string Strengths { get; set; }

        [StringLength(1000)]
        public string Weaknesses { get; set; }

        [Display(Name = "Created By ID")]
        [Createable(false), Updateable(false)]
        public string CreatedById { get; set; }

        [Display(Name = "Created Date")]
        [Createable(false), Updateable(false)]
        public DateTimeOffset CreatedDate { get; set; }

        [Display(Name = "Last Modified By ID")]
        [Createable(false), Updateable(false)]
        public string LastModifiedById { get; set; }

        [Display(Name = "Last Modified Date")]
        [Createable(false), Updateable(false)]
        public DateTimeOffset LastModifiedDate { get; set; }

        [Display(Name = "System Modstamp")]
        [Createable(false), Updateable(false)]
        public DateTimeOffset SystemModstamp { get; set; }

        [Display(Name = "Deleted")]
        [Createable(false), Updateable(false)]
        public bool IsDeleted { get; set; }

    }
}