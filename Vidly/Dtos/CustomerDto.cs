using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.DTOs
{
    public class CustomerDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public bool IsSubscibedToNewsletter { get; set; }

        [Column(TypeName = "Date")]
        //[Min18YearsIfAMember]
        public DateTime? Birthdate { get; set; }

        public byte MemberShipTypeId { get; set; }

        public MembershipTypeDto MembershipType { get; set; }
    }
}