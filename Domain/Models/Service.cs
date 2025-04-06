﻿
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public class Service 
{
    [Key]

    public int ServiceId { get; set; }
    [DisplayName("نام خدمات")]
    public string ServiceName { get; set; }
    [DisplayName("قیمت")]
    public decimal Price { get; set; }
    [DisplayName("مدت زمان")]
    public int Duration { get; set; }
    [DisplayName("عکس")]
    [ValidateNever]
    public string ImageUrl { get; set; }
    [DisplayName("اسلاگ")]

    public string Slug { get; set; } // فیلد خوانا برای URL

    [DisplayName("توضیحات")]
    public string Description { get; set; }

    // کلید خارجی به دسته‌بندی.
    [DisplayName("دسته بندی")]

    public int? CategoryId { get; set; }
    [ValidateNever]
    public virtual Category? Category { get; set; }
    public  DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
   
}