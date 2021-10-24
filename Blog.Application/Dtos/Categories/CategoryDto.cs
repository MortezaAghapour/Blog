using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Application.Dtos.Base;

namespace Blog.Application.Dtos.Categories
{
    public class CategoryDto:EntityDto
    {
        
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Icon { get; set; }
        public int? ParentId { get; set; }
        public bool ShowInHomePage { get; set; }
        public int DisplayOrder { get; set; }
    }
}
