
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Service 
{
    [Key]

    public int ServiceId { get; set; }
    [DisplayName("نام خدمات")]
    public string ServiceName { get; set; }
    [DisplayName("قیمت")]
    [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)] 
    public decimal Price { get; set; }
    [DisplayName("مدت زمان")]
    public int Duration { get; set; }
    [DisplayName("عکس")]
    [ValidateNever]
    public string ImageUrl { get; set; }
    [NotMapped]
    [DisplayName("اسلاگ")]  
    public string Slug => ServiceName?.Replace(' ', '-').ToLower(); // فیلد خوانا برای URL

    [DisplayName("توضیحات")]
    public string Description { get; set; }

    // کلید خارجی به دسته‌بندی.
    [DisplayName("دسته بندی")]

    public int? CategoryId { get; set; }
    [ValidateNever]
    public virtual Category? Category { get; set; }
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]


    public DateTime? CreatedAt { get; set; }
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]

    public DateTime? UpdatedAt { get; set; }
   
}