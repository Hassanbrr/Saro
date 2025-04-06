using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

public class Category
{
    [Key]
    public int CategoryId { get; set; }
    [Required]
    [DisplayName("نام دسته بندی ")]
    public string CategoryName { get; set; }
    [DisplayName("اسلاگ")]
 
    public string Slug { get; set; } // فیلد خوانا برای URL  
    [DisplayName(" توضیحات")]

    public string Description  { get; set; } // فیلد خوانا برای URL  

    // ارتباط با دسته‌بندی والد و فرزند
    public int? ParentCategoryId { get; set; }
    [DisplayName(" دسته بندی والد")]

    public virtual Category? ParentCategory { get; set; }
    [DisplayName(" دسته بندی فرزند")]

    public virtual ICollection<Category>? SubCategories { get; set; }

    // ارتباط با خدمات
    public virtual ICollection<Service>? Services { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}