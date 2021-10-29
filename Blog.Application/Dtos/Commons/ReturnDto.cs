using System.Collections.Generic;

namespace Blog.Application.Dtos.Commons
{
    public class ReturnDto
    {

        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }

    public class ReturnDto<T> : ReturnDto where T : class
    {
        public T Data { get; set; }
    }

}