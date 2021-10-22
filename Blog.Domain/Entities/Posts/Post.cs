using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using Blog.Domain.Entities.Base;
using Blog.Domain.Entities.Categories;
using Newtonsoft.Json;

namespace Blog.Domain.Entities.Posts
{
    public class Post:AuditableEntity
    {
        #region Fields

        private ICollection<Comment> _comments;
        #endregion
        #region Constrcutors

        public Post()
        {
            Tags=new List<string>();
        }
        #endregion
        #region Properties

        public long CategoryId { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public string Image { get; set; }
        public string Author { get; set; }
        public List<string> Tags { get; set; }
        public string ShortLink { get; set; }
        public bool IsPublish { get; set; }
        public DateTime PublishDate { get; set; }

        #endregion
        #region Navigation Properties
        [JsonIgnore]
        public Category Category { get; set; }
         [JsonIgnore]
        public ICollection<Comment> Comments
        {
            get => _comments ?? new HashSet<Comment>();
            set => _comments = value;
        }
        #endregion
    }
}
