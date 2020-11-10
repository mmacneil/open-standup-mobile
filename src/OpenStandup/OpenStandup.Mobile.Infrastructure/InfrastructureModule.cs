using Autofac;
using OpenStandup.Core.Interfaces.Authentication;
using OpenStandup.Core.Interfaces.Data.GraphQL;
using OpenStandup.Mobile.Infrastructure.Authentication.GitHub;
using OpenStandup.Mobile.Infrastructure.Data;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using System;
using System.Net.Http;
using System.Reflection;
using OpenStandup.Core.Interfaces;
using OpenStandup.Core.Interfaces.Apis;
using OpenStandup.Core.Interfaces.Services;
using OpenStandup.Mobile.Infrastructure.Apis;
using OpenStandup.Mobile.Infrastructure.Configuration;
using OpenStandup.Mobile.Infrastructure.Interfaces;
using OpenStandup.Mobile.Infrastructure.Services;


namespace OpenStandup.Mobile.Infrastructure
{
    public class InfrastructureModule : Autofac.Module
    {
        public string ApplicationDataPath { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            var assembly = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces().SingleInstance();

            builder.RegisterType<OAuthAuthenticator>().As<IAuthenticator>().SingleInstance();

            builder.RegisterType<ConfigurationLoader>().As<IConfigurationLoader>().SingleInstance();

            builder.RegisterType<AppContext>().As<IAppContext>().SingleInstance();

            builder.RegisterType<GitHubGraphQLApi>().As<IGitHubGraphQLApi>().SingleInstance();
            builder.RegisterType<OpenStandupApi>().As<IOpenStandupApi>().SingleInstance();

            builder.RegisterType<AppSettings>().As<IAppSettings>().SingleInstance();

            builder.RegisterInstance(new AppDb("app.sqlite3", ApplicationDataPath)).SingleInstance();

            builder.Register(ctx => new HttpClient(new HttpClientHandler())
            {
                Timeout = new TimeSpan(0, 0, 0, 15)
            }).SingleInstance();

            builder.Register(ctx => new GraphQLHttpClient(new GraphQLHttpClientOptions
            {
                EndPoint = new Uri("https://api.github.com/graphql")
            }, new NewtonsoftJsonSerializer())).SingleInstance();

            builder.Register(ctx => new HttpClient(new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                    (message, certificate, chain, sslPolicyErrors) => true
            })
            {
                MaxResponseContentBufferSize = 256000,
                Timeout = new TimeSpan(0, 0, 0, 5)
            }).SingleInstance();

            builder.RegisterType<LocationService>().As<ILocationService>().SingleInstance();

            builder.RegisterType<CameraService>().As<ICameraService>().SingleInstance();
        }
    }
}



