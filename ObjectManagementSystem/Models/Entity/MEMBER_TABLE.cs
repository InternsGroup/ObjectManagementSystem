//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ObjectManagementSystem.Models.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class MEMBER_TABLE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MEMBER_TABLE()
        {
            this.ACTION_TABLE = new HashSet<ACTION_TABLE>();
            this.PENALTY_TABLE = new HashSet<PENALTY_TABLE>();
        }
    
        public int ID { get; set; }
        public string NAME { get; set; }
        public string SURNAME { get; set; }
        public string EMAIL { get; set; }
        public string USERNAME { get; set; }
        public string PASSWORD { get; set; }
        public string PHOTO { get; set; }
        public string TELNUMBER { get; set; }
        public string SCHOOL { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ACTION_TABLE> ACTION_TABLE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PENALTY_TABLE> PENALTY_TABLE { get; set; }
    }
}
