using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="please enter customer's name.")]
        [StringLength(255)]
        public string Name { get; set; }
        public bool IsSubscibedToNewsletter { get; set; }

        [Column(TypeName = "Date")]
        [Display(Name ="Date of Birth")]
        [Min18YearsIfAMember]
        public DateTime? Birthdate { get; set; }
        public MembershipType MembershipType { get; set; }

        [Display(Name ="Membership Type")]
        public byte MemberShipTypeId { get; set; }
    }
}