
using System.ComponentModel.DataAnnotations;
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
        public string OriginalImagePath { get; set; }
        [Required]
        [ValidateNever]
        public string LeftPartPath { get; set; } // نیمه چپ
        [Required]
        [ValidateNever]
        public string RightPartPath { get; set; } // نیمه راست
        [Required]

        public string Slug { get; set; }  

    }
}
