using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }
        public DateTime? DateAdded { get; set; }
        [Display(Name = "Number In Stock")]
        [Range(1,20)]
        public int NumberInStock { get; set; }
        public Genres Genre { get; set; }
        [Display(Name = "Genre")]
        [Required]
        public byte GenreId { get; set; }

        [Range(1, 20)]
        public int NumberAvaliable { get; set; }
    }
}