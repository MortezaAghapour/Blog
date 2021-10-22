using System;
using Blog.Shared.Markers.Entities;

namespace Blog.Domain.Entities.Base
{
    public  abstract class AuditableEntity:Entity,IAuditable
    {
        public DateTime CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public DateTime? DeleteDate { get; set; }
    }
}