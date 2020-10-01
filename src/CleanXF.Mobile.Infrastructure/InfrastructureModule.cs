using Autofac;
using CleanXF.Core.Interfaces.Authentication;
using CleanXF.Core.Interfaces.Data.GraphQL;
using CleanXF.Mobile.Infrastructure.Authentication.GitHub;
using CleanXF.Mobile.Infrastructure.Data;
using CleanXF.Mobile.Infrastructure.Data.GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using System;
using System.Net.Http;
using System.Reflection;
using CleanXF.Core.Interfaces.Apis;
using CleanXF.Mobile.Infrastructure.Apis;

namespace CleanXF.Mobile.Infrastructure
{
    public class InfrastructureModule : Autofac.Module
    {
        public string ApplicationDataPath { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            var assembly = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces().SingleInstance();

            builder.RegisterType<OAuthAuthenticator>().As<IAuthenticator>().SingleInstance();

            builder.RegisterType<GitHubGraphQLApi>().As<IGitHubGraphQLApi>().SingleInstance();
            builder.RegisterType<OpenStandupApi>().As<IOpenStandupApi>().SingleInstance();

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
                Timeout = new TimeSpan(0, 0, 0, 15)
            }).SingleInstance();
        }
    }
}



