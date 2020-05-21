using Autofac;
using CleanXF.Core.Interfaces.Authentication;
using CleanXF.Mobile.Infrastructure.Authentication.GitHub;

namespace CleanXF.Mobile.Infrastructure
{
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<OAuthAuthenticator>().As<IAuthenticator>().SingleInstance();
        }
    }
}
