using System;
using Blog.Shared.Markers.Entities;

namespace Blog.Domain.Entities.Base
{
    public  abstract class AuditableEntity:Entity,IAuditable
    {
        public AuditUserInfo UserInfo { get; set; }  =new AuditUserInfo();
        public DateTime CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public DateTime? DeleteDate { get; set; }

    }

    public  class AuditUserInfo
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
    }
}