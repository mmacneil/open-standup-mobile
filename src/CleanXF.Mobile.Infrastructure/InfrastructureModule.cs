using Autofac;
using CleanXF.Core.Interfaces.Authentication;
using CleanXF.Mobile.Infrastructure.Authentication.GitHub;
using System;
using System.Net.Http;

namespace CleanXF.Mobile.Infrastructure
{
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<OAuthAuthenticator>().As<IAuthenticator>().SingleInstance();
           
            builder.Register(ctx => new HttpClient(new HttpClientHandler())
            {             
                Timeout = new TimeSpan(0, 0, 0, 15)
            }).SingleInstance();
        }
    }
}
