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
using MediatR;
using OpenStandup.Common.Infrastructure;
using OpenStandup.Common.Interfaces.Infrastructure;
using OpenStandup.Core.Interfaces;
using OpenStandup.Core.Interfaces.Apis;
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
            // Repositories
            builder.RegisterAssemblyTypes(assembly).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces().SingleInstance();
            // Services
            builder.RegisterAssemblyTypes(assembly).Where(t => t.Name.EndsWith("Service")).AsImplementedInterfaces().SingleInstance();

            builder.RegisterType<FileUtilities>().As<IFileUtilities>().SingleInstance();
            builder.RegisterType<ImageUtilities>().As<IImageUtilities>().SingleInstance();

            builder.RegisterType<OAuthAuthenticator>().As<IAuthenticator>().SingleInstance();

            builder.RegisterType<ConfigurationLoader>().As<IConfigurationLoader>().SingleInstance();

            builder.RegisterType<AppContext>().As<IAppContext>().SingleInstance();

            builder.RegisterType<GitHubGraphQLApi>().As<IGitHubGraphQLApi>().SingleInstance();
            builder.RegisterType<OpenStandupApi>().As<IOpenStandupApi>().SingleInstance();

            builder.RegisterType<AppSettings>().As<IAppSettings>().SingleInstance();

            builder.RegisterType<JobService>().SingleInstance();

            builder.RegisterType<VersionInfo>().As<IVersionInfo>().SingleInstance();

            builder.RegisterInstance(new AppDb("app.sqlite3", ApplicationDataPath)).SingleInstance();

            builder.Register(ctx => new HttpClient(new HttpClientHandler())
            {
                Timeout = new TimeSpan(0, 0, 0, 15)
            }).SingleInstance();

            builder.Register(ctx => new GraphQLHttpClient(new GraphQLHttpClientOptions
            {
                EndPoint = new Uri("https://api.github.com/graphql")
            }, new NewtonsoftJsonSerializer())).SingleInstance();

            builder.Register(ctx => new HttpClient(new AuthorizedRequestHandler(ctx.Resolve<IMediator>())
            {
                ServerCertificateCustomValidationCallback =
                    (message, certificate, chain, sslPolicyErrors) => true
            })
            {
                MaxResponseContentBufferSize = 256000,
                Timeout = new TimeSpan(0, 0, 0, 250)
            }).SingleInstance();
        }
    }
}



