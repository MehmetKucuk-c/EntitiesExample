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
    
    public partial class TBL_Notlar
    {
        public int NotId { get; set; }
        public Nullable<int> Ogr { get; set; }
        public Nullable<int> Ders { get; set; }
        public Nullable<short> Sinav1 { get; set; }
        public Nullable<short> Sinav2 { get; set; }
        public Nullable<short> Sinav3 { get; set; }
        public Nullable<decimal> Ortalama { get; set; }
        public Nullable<bool> Durum { get; set; }
    
        public virtual TBL_Dersler TBL_Dersler { get; set; }
        public virtual TBL_Ogrenci TBL_Ogrenci { get; set; }
    }
}
