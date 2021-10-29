using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Shared.Resources
{
    public class ValidationErrorResources
    {
        public static string CategoryNameMaxLength => "حداکثر طول نام دسته بندی 250 می باشد";

        public static string CategoryNameIsRequired => "وارد کردن نام دسته بندی اجباری می باشد";

        public static string CategoryNameIsExist => "نام دسته بندی وارد شده تکراری می باشد";

        public static string CategoryDeleteIdRequired => "شناسه دسته بندی برای حذف نمی تواند خالی باشد";
        public static string CategoryIdNotLessThanZero => "شناسه دسته بندی نمی تواند کوچکتر یا مساوی صفر باشد";
        public static string UserNameIsRequired => "وارد کردن نام کاربری اجباری می باشد";
        public static string PasswordIsRequired => "وارد کردن رمز عبور اجباری می باشد";
        public static string UserNameIsExist => "نام کاربری وارد شده تکراری می باشد";
    }
}
