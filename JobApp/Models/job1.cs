using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobApp.Models
{
    [MetadataType(typeof(metadatajob))]
    public partial class Job
    {
    }
    public class metadatajob
    {
       [AllowHtml]
        public string JobContent { get; set; }
    }
}