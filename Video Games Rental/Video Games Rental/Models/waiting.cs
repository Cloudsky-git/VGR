//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Video_Games_Rental.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class waiting
    {
        public int waiting_id { get; set; }
        public Nullable<int> customer_id { get; set; }
        public Nullable<int> platform_id { get; set; }
        public Nullable<int> language_id { get; set; }
        public Nullable<int> genre_id { get; set; }
        public Nullable<int> condiiton_id { get; set; }
        public Nullable<int> status_id { get; set; }
        public string title { get; set; }
        public Nullable<decimal> price { get; set; }
    
        public virtual condition condition { get; set; }
        public virtual customer customer { get; set; }
        public virtual genre genre { get; set; }
        public virtual language language { get; set; }
        public virtual platform platform { get; set; }
        public virtual status status { get; set; }
    }
}
