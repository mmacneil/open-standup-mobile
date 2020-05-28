

namespace CleanXF.Core.Domain.Features.Authenticate.Models
{
    public class AuthenticationResponse
    {
        public bool Status { get; }

        public AuthenticationResponse(bool status = false)
        {
            Status = status;
        }
    }
}
