using Autofac;
using CleanXF.Core.Interfaces.Authentication;
using CleanXF.Mobile.Infrastructure.Authentication.GitHub;
using CleanXF.Mobile.Infrastructure.Data;
using System;
using System.Net.Http;
using System.Reflection;

namespace CleanXF.Mobile.Infrastructure
{
    public class InfrastructureModule : Autofac.Module
    {
        public string ApplicationDataPath { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces().SingleInstance();
      
            builder.RegisterType<OAuthAuthenticator>().As<IAuthenticator>().SingleInstance();

            builder.RegisterInstance(new AppDb("app.sqlite3", ApplicationDataPath)).SingleInstance();

            builder.Register(ctx => new HttpClient(new HttpClientHandler())
            {
                Timeout = new TimeSpan(0, 0, 0, 15)
            }).SingleInstance();
        }
    }
}
