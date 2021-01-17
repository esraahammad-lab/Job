//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace JobApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Job
    {
        public Job()
        {
            this.ApplyForJob = new HashSet<ApplyForJob>();
        }
    
        public int IDJob { get; set; }
        public string JobTitle { get; set; }
        public string JobImage { get; set; }
        public string JobContent { get; set; }
        public string Location { get; set; }
        public Nullable<double> Salary { get; set; }
        public string CompanyName { get; set; }
        public string JobNature { get; set; }
        public Nullable<System.DateTime> publishon { get; set; }
        public Nullable<System.DateTime> Dateline { get; set; }
        public Nullable<int> IDUser { get; set; }
    
        public virtual ICollection<ApplyForJob> ApplyForJob { get; set; }
        public virtual User User { get; set; }
    }
}