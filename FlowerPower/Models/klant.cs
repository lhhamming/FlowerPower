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
    
    public partial class klant
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public klant()
        {
            this.bestellings = new HashSet<bestelling>();
        }
    
        public int klantid { get; set; }
        public string voorletters { get; set; }
        public string tussenvoegsels { get; set; }
        public string achternaam { get; set; }
        public string adres { get; set; }
        public string postcode { get; set; }
        public string woonplaats { get; set; }
        public Nullable<System.DateTime> geboortedatum { get; set; }
        public string AspNetUserID { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<bestelling> bestellings { get; set; }
    }
}
