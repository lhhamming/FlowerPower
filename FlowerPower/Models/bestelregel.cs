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
    
    public partial class bestelregel
    {
        public int artikel_artikelid { get; set; }
        public int bestelling_bestellingid { get; set; }
        public Nullable<int> aantal { get; set; }
    
        public virtual artikel artikel { get; set; }
        public virtual bestelling bestelling { get; set; }
    }
}
