//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Project8.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class AspNetUserRole
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public string Role_userid { get; set; }
    
        public virtual AspNetRole AspNetRole { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
    }
}
