//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Gameblog.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Photo
    {
        public int Id { get; set; }
        public string name { get; set; }
        public int Review_id { get; set; }
        public string imageName { get; set; }
        public Nullable<System.DateTime> timeStamp { get; set; }
    
        public virtual Review Review { get; set; }
    }
}
