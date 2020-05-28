using Autofac;
using CleanXF.Core.Domain.Features.Authenticate;
using MediatR;
using System.Reflection;

namespace CleanXF.Core
{
    public class CoreModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Mediator itself
            builder
                .RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope();

            // request & notification handlers
            builder.Register<ServiceFactory>(context =>
            {
                var c = context.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            builder.RegisterAssemblyTypes(typeof(LoginUseCase).GetTypeInfo().Assembly).AsImplementedInterfaces(); // via assembly scan
        }
    }
}
