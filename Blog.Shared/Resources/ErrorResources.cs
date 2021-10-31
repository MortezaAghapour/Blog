namespace Blog.Shared.Resources
{
    public class ErrorResources
    {
        public static string BeginTransactionCallError => "Please Call `BeginTransaction()` Method First.";
        public static string ObjectNotFound => "آبجکت مورد نظر شما یافت نشد";
        public static string BadRequest => "خطایی در پارامترهای ارسالی وجود دارد";
        public static string InternalError => "خطایی درپردازش رخ داده است";
        public static string UnknownError => "خطایی نامعلوم وجود دارد";
        public static string NullArgumentError => "بعضی از پارامترهای ورودی خالی می باشد";
        public static string NullReference => "آبجکت مورد نظر نمونه سازی نشده است";
    }
}