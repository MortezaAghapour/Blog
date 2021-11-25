using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Commands.Posts.Delete
{
    public class DeletePostCommand:IRequest<bool>
    {
        public long Id { get; set; }
    }
}
