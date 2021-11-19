using Blog.Application.Dtos.SocialNetworks;
using MediatR;

namespace Blog.Application.Commands.SocialNetworks.Update
{
    public class UpdateSocialNetworkCommand :IRequest<SocialNetworkDto>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Icon { get; set; }
        public string Color { get; set; }
        public string Image { get; set; }
    }
}