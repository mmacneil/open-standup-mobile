using Autofac;
using MediatR;
using System.Reflection;

namespace OpenStandup.Core
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

            //builder.RegisterAssemblyTypes(typeof(LoginUseCase).GetTypeInfo().Assembly).AsImplementedInterfaces(); // via assembly scan
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).Where(t => t.Name.EndsWith("UseCase")).AsImplementedInterfaces();
        }
    }
}
