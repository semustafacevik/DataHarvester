//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataHarvester.Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblResult
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblResult()
        {
            this.tblEmails = new HashSet<tblEmail>();
            this.tblFileUrls = new HashSet<tblFileUrl>();
            this.tblHosts = new HashSet<tblHost>();
            this.tblIPs = new HashSet<tblIP>();
            this.tblLinkedInLinks = new HashSet<tblLinkedInLink>();
            this.tblLinkedInProfiles = new HashSet<tblLinkedInProfile>();
            this.tblPorts = new HashSet<tblPort>();
        }
    
        public int ID { get; set; }
        public string searchQuery { get; set; }
        public System.DateTime searchDate { get; set; }
        public int userID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblEmail> tblEmails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblFileUrl> tblFileUrls { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblHost> tblHosts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblIP> tblIPs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblLinkedInLink> tblLinkedInLinks { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblLinkedInProfile> tblLinkedInProfiles { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblPort> tblPorts { get; set; }
        public virtual tblUser tblUser { get; set; }
    }
}
