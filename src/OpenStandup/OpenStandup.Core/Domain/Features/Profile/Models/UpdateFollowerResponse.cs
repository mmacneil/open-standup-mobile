using Vessel;

namespace OpenStandup.Core.Domain.Features.Profile.Models
{
    public class UpdateFollowerResponse
    {
        public UpdateFollowerRequest Request { get; }
        public Dto<bool> ApiResponse { get; }
        
        public UpdateFollowerResponse(UpdateFollowerRequest request, Dto<bool> apiResponse) 
        {
            Request = request;
            ApiResponse = apiResponse;
        }
    }
}
