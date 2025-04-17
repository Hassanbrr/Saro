
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Domain.Models.Website
{
    public class HeroSection
    {
        [Key]
        public int Id { get; set; }
       
        public string? Title { get; set; }
        public string? Subtitle { get; set; }

        [Required]
        [ValidateNever]
        public string ImageUrl { get; set; }
 
        [Required]
        [NotMapped]
        [DisplayName("اسلاگ")]
        public string Slug => Title?.Replace(' ', '-').ToLower();

    }
}
