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
    
    public partial class artikel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public artikel()
        {
            this.bestellings = new HashSet<bestelling>();
        }
    
        public int artikelid { get; set; }
        public string artikelnaam { get; set; }
        public Nullable<decimal> prijs { get; set; }
        public bool actief { get; set; }
        public string image { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<bestelling> bestellings { get; set; }
    }
}
