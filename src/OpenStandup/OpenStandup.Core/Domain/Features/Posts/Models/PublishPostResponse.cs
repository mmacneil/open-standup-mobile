using Vessel;

namespace OpenStandup.Core.Domain.Features.Posts.Models
{
    public class PublishPostResponse
    {
        public Dto<bool> ApiResponse { get; }

        public PublishPostResponse(Dto<bool> apiResponse)
        {
            ApiResponse = apiResponse;
        }
    }
}
