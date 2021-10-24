using System.ComponentModel.DataAnnotations;

namespace Blog.Shared.Enums.DateTime
{
    public enum Period
    {
        [Display(Name = "میلی ثانیه")]
        Millisecond,
        [Display(Name = "ثانیه")]
        Second,
        [Display(Name = "دقیقه")]
        Minute,
        [Display(Name = "ساعت")]
        Hour,
        [Display(Name = "روز")]
        Day,
        [Display(Name = "هفته")]
        Week,
        [Display(Name = "ماه")]
        Month,
        [Display(Name = "سال")]
        Year
    }
}