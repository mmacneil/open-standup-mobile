using CleanXF.Core.Domain.Features.Authenticate.Models;
using CleanXF.Core.Interfaces;


namespace CleanXF.Mobile.Presenters
{
    public class AuthenticationPresenter : IOutputPort<AuthenticationResponse>
    {
        public void Handle(AuthenticationResponse response)
        {
            throw new System.NotImplementedException();
        }
    }
}

/*
  public sealed class LoginPresenter : IOutputPort<LoginResponse>
  {
    public JsonContentResult ContentResult { get; }

    public LoginPresenter()
    {
      ContentResult = new JsonContentResult();
    }

    public void Handle(LoginResponse response)
    {
      ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.Unauthorized);
      ContentResult.Content = response.Success ? JsonSerializer.SerializeObject(response.Token) : JsonSerializer.SerializeObject(response.Errors);
    }
  }
*/