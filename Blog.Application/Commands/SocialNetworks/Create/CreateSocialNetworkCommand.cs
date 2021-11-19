using Blog.Application.Dtos.SocialNetworks;
using MediatR;

namespace Blog.Application.Commands.SocialNetworks.Create
{
    public class CreateSocialNetworkCommand :IRequest<SocialNetworkDto>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Icon { get; set; }
        public string Color { get; set; }
        public string Image { get; set; }
    }
}