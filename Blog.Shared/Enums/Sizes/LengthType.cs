using System.ComponentModel.DataAnnotations;

namespace Blog.Shared.Enums.Sizes
{
    public enum LengthType
    {
        [Display(Name = "بایت")]
        Byte,
        [Display(Name = "کیلوبایت")]
        KByte,
        [Display(Name = "مگابایت")]
        MBye,
        [Display(Name = "گیگابایت")]
        GByte
    }
}