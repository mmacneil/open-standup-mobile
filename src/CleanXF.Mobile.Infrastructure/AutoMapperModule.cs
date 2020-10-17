using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using AutoMapper;

namespace CleanXF.Mobile.Infrastructure
{
    public class AutoMapperModule : Autofac.Module
    {
        private readonly IEnumerable<Assembly> _assembliesToScan;
        public AutoMapperModule(IEnumerable<Assembly> assembliesToScan)
        {
            _assembliesToScan = assembliesToScan;
        }

        public AutoMapperModule(params Assembly[] assembliesToScan) : this((IEnumerable<Assembly>)assembliesToScan) { }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            var assembliesToScan = this._assembliesToScan as Assembly[] ?? this._assembliesToScan.ToArray();

            var allTypes = assembliesToScan
                          .Where(a => !a.IsDynamic && a.GetName().Name != nameof(AutoMapper))
                          .Distinct() // avoid AutoMapper.DuplicateTypeMapConfigurationException
                          .SelectMany(a => a.DefinedTypes)
                          .ToArray();

            var openTypes = new[] {
                            typeof(IValueResolver<,,>),
                            typeof(IMemberValueResolver<,,,>),
                            typeof(ITypeConverter<,>),
                            typeof(IValueConverter<,>),
                            typeof(IMappingAction<,>)
            };

            foreach (var type in openTypes.SelectMany(openType =>
             allTypes.Where(t => t.IsClass && !t.IsAbstract && ImplementsGenericInterface(t.AsType(), openType))))
            {
                builder.RegisterType(type.AsType()).InstancePerDependency();
            }

            builder.Register<IConfigurationProvider>(ctx => new MapperConfiguration(cfg => cfg.AddMaps(assembliesToScan))).SingleInstance();

            builder.Register<IMapper>(ctx => new Mapper(ctx.Resolve<IConfigurationProvider>(), ctx.Resolve)).InstancePerDependency();
        }

        private static bool ImplementsGenericInterface(Type type, Type interfaceType)
                  => IsGenericType(type, interfaceType) || type.GetTypeInfo().ImplementedInterfaces.Any(@interface => IsGenericType(@interface, interfaceType));

        private static bool IsGenericType(Type type, Type genericType)
                  => type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == genericType;
    }

}
