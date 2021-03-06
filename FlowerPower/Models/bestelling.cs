//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FlowerPower.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class bestelling
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public bestelling()
        {
            this.bestelregels = new HashSet<bestelregel>();
        }
    
        public int bestellingid { get; set; }
        public int statusid { get; set; }
        public Nullable<System.DateTime> besteldatum { get; set; }
        public Nullable<System.DateTime> ophaaldatum { get; set; }
        public int bestelregelid { get; set; }
        public Nullable<int> medewerkerid { get; set; }
        public int klantid { get; set; }
        public int vestigingid { get; set; }
        public Nullable<long> totaalprijs { get; set; }
    
        public virtual klant klant { get; set; }
        public virtual medewerker medewerker { get; set; }
        public virtual status status { get; set; }
        public virtual vestiging vestiging { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<bestelregel> bestelregels { get; set; }
    }
}
