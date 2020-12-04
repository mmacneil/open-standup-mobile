using MediatR;
using Vessel;

namespace OpenStandup.Core.Domain.Features.Posts.Models
{
    public class PublishPostRequest : IRequest<Dto<bool>>, IRequest<Unit>
    {
        public string Text { get; }
        public string PhotoPath { get; }

        public PublishPostRequest(string text, string photoPath)
        {
            Text = text;
            PhotoPath = photoPath;
        }
    }
}
