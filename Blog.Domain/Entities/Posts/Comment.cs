using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Domain.Entities.Base;
using Newtonsoft.Json;

namespace Blog.Domain.Entities.Posts
{
    public class Comment:AuditableEntity
    {
        #region Fields

        #endregion
        #region Constrcutors
        #endregion
        #region Properties

        public long PostId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string AuthorResponse { get; set; }
        public bool IsPublish { get; set; }
        public DateTime PublishDate { get; set; }

        #endregion
        #region Navigation Properties
        [JsonIgnore]
        public Post Post { get; set; }
        #endregion
    }
}
