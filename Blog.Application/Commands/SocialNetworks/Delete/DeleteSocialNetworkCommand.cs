using MediatR;

namespace Blog.Application.Commands.SocialNetworks.Delete
{
    public class DeleteSocialNetworkCommand   :IRequest<bool>
    {
        public long Id { get; set; }
    }
}