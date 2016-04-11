using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesBuddy.Models
{
    public class TextEditor
    {
        [Key]
        public int TextId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        public string Title { get; set; }

        [AllowHtml]
        [UIHint("tinymce_jquery_full")]
        public string Message { get; set; }
    }
}