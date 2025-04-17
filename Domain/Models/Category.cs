using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Category
{
    [Key]
    public int CategoryId { get; set; }
    [Required]
    [DisplayName("نام دسته بندی ")]
    public string CategoryName { get; set; }
    [NotMapped]
    [DisplayName("اسلاگ")]
    public string Slug => CategoryName?.Replace(' ', '-').ToLower(); 
    [DisplayName(" توضیحات")]

    public string Description  { get; set; } // فیلد خوانا برای URL  
    [DisplayName(" عکس")]
    [ValidateNever]
    public string ImageUrl { get; set; } // فیلد خوانا برای URL  


    // ارتباط با دسته‌بندی والد و فرزند
    public int? ParentCategoryId { get; set; }
    [DisplayName(" دسته بندی والد")]

    public virtual Category? ParentCategory { get; set; }
    [DisplayName(" دسته بندی فرزند")]

    public virtual ICollection<Category>? SubCategories { get; set; }

    // ارتباط با خدمات
    public virtual ICollection<Service>? Services { get; set; }
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")] 
    public DateTime? CreatedAt { get; set; }
 
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")] 
    public DateTime? UpdatedAt { get; set; }  
}