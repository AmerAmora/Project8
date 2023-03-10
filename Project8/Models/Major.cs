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
    using System.ComponentModel.DataAnnotations;

    public partial class Major
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Major()
        {
            this.AspNetUsers = new HashSet<AspNetUser>();
            this.Courses = new HashSet<Cours>();
        }
    
        public int Major_Id { get; set; }
        [Required]
        [Display(Name = "Major Name")]
        public string Major_Name { get; set; }
        [Required]
        [Display(Name = "Major Description")]
        public string Major_Description { get; set; }
        [Required]
        [Display(Name = "Major Image")]
        public string Major_Image { get; set; }
        [Required]
        [Display(Name = "Major Price")]
        public Nullable<int> Price { get; set; }
        public Nullable<int> College_Id { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AspNetUser> AspNetUsers { get; set; }
        public virtual College College { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cours> Courses { get; set; }
    }
}
