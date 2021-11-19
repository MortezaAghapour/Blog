using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Application.Dtos.Base;

namespace Blog.Application.Dtos.SocialNetworks
{
    public class SocialNetworkDto:EntityDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Icon { get; set; }
        public string Color { get; set; }

    }
}
