//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EntitiesExample
{
    using System;
    using System.Collections.Generic;
    
    public partial class TBL_Ogrenci
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TBL_Ogrenci()
        {
            this.TBL_Notlar = new HashSet<TBL_Notlar>();
        }
    
        public int Id { get; set; }
        public string OgrAdi { get; set; }
        public string OgrSoyadi { get; set; }
        public string Fotograf { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_Notlar> TBL_Notlar { get; set; }
    }
}
