namespace Blog.Shared.Resources
{
    public class ValidationErrorResources
    {
        public static string CategoryNameMaxLength => "حداکثر طول نام دسته بندی 250 کاراکتر می باشد";

        public static string CategoryNameIsRequired => "وارد کردن نام دسته بندی اجباری می باشد";

        public static string CategoryNameIsExist => "نام دسته بندی وارد شده تکراری می باشد";

        public static string CategoryDeleteIdRequired => "شناسه دسته بندی برای حذف نمی تواند خالی باشد";
        public static string CategoryIdNotLessThanZero => "شناسه دسته بندی نمی تواند کوچکتر یا مساوی صفر باشد";
        public static string UserNameIsRequired => "وارد کردن نام کاربری اجباری می باشد";
        public static string PasswordIsRequired => "وارد کردن رمز عبور اجباری می باشد";
        public static string UserNameIsExist => "نام کاربری وارد شده تکراری می باشد";
        public static string UserNotFoundWithEnteredInfo => "کاربری با مشخصات وارد شده یافت نشد";
        public static string UserIsLockOut => "کاربر مورد نظر مسدود می باشد";
        public static string UserNotAllowed => "کاربر مورد نظر اجازه ورود به سیستم را ندارد";

        public static string TheSkillNameIsDuplicate => "نام مهارت وارد شده تکراری می باشد";
        public static string TheCategoryNameIsDuplicate => "نام دسته بندی وارد شده تکراری می باشد";

        public static string SkillNameMaxLength=>"حداکثر طول نام مهارت 500 کاراکتر می باشد";

        public static string SkillNameIsRequired => "وارد کردن نام مهارت اجباری می باشد";

        public static string SkillDeleteIdRequired => "برای حذف مهارت وارد کردن شناسه مهارت اجباری می باشد";
        public static string SkillIdNotLessThanZero => "شناسه مهارت نمی تواند کوچکتر یا مساوی با صفر باشد";
        public static string SliderTitleMaxLength =>"حداکثر کاراکتر عنوان اسلایدر 1500 کاراکتر می باشد";
        public static string SliderTitleIsRequired => "وارد کردن عنوان اسلایدر اجباری می باشد";
        public static string SliderDeleteIdRequired => "برای حذف اسلایدر وارد کردن شناسه اسلایدر اجباری می باشد";
        public static string SliderIdNotLessThanZero => "شناسه اسلایدر نمی تواند کوچکتر یا مساوی با صفر باشد";
        public static string SkillNameIsExist => "نام مهارت وارد شده تکراری می باشد";
        public static string SkillDescriptionMaxLength => "حداکثر کاراکتر توضیحات مهارت  1500 کاراکتر می باشد";
        public static string SliderDescriptionIsRequired => "وارد کردن توضیحات اسلایدر اجباری می باشد";
        public static string SliderImageIsRequired => "وارد کردن عکس اسلایدر اجباری می باشد";
    }
}
