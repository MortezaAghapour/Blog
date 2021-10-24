using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Domain.Entities.Base;
using Blog.Domain.Entities.Posts;
using Newtonsoft.Json;

namespace Blog.Domain.Entities.Categories
{
    public class Category:Entity
    {
        #region Fields

        private ICollection<Category> _children;
        private ICollection<Post> _posts;
        #endregion
        #region Constrcutors
        #endregion
        #region Properties

        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Icon { get; set; }
        public long? ParentId { get; set; }
        public bool ShowInHomePage { get; set; }
        public int DisplayOrder { get; set; }
        #endregion
        #region Navigation Properties   
        [JsonIgnore]
        public ICollection<Category> Children
        {
            get => _children ?? new HashSet<Category>();
            set => _children = value;
        }
        [JsonIgnore]
        public ICollection<Post> Posts
        {
            get => _posts ?? new HashSet<Post>();
            set => _posts = value;
        }
        [JsonIgnore]
        public Category Parent { get; set; }
        #endregion
    }
}
