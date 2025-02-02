using System;
using System.Collections.Generic;
using System.Reflection;

namespace MCM.LightInject
{
	// Token: 0x020000CA RID: 202
	internal interface IServiceRegistry
	{
		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000440 RID: 1088
		IEnumerable<ServiceRegistration> AvailableServices { get; }

		// Token: 0x06000441 RID: 1089
		IServiceRegistry Register(Type serviceType, Type implementingType);

		// Token: 0x06000442 RID: 1090
		IServiceRegistry Register(Type serviceType, Type implementingType, ILifetime lifetime);

		// Token: 0x06000443 RID: 1091
		IServiceRegistry Register(Type serviceType, Type implementingType, string serviceName);

		// Token: 0x06000444 RID: 1092
		IServiceRegistry Register(Type serviceType, Type implementingType, string serviceName, ILifetime lifetime);

		// Token: 0x06000445 RID: 1093
		IServiceRegistry Register<TService, TImplementation>() where TImplementation : TService;

		// Token: 0x06000446 RID: 1094
		IServiceRegistry Register<TService, TImplementation>(ILifetime lifetime) where TImplementation : TService;

		// Token: 0x06000447 RID: 1095
		IServiceRegistry Register<TService, TImplementation>(string serviceName) where TImplementation : TService;

		// Token: 0x06000448 RID: 1096
		IServiceRegistry Register<TService, TImplementation>(string serviceName, ILifetime lifetime) where TImplementation : TService;

		// Token: 0x06000449 RID: 1097
		IServiceRegistry RegisterInstance<TService>(TService instance);

		// Token: 0x0600044A RID: 1098
		IServiceRegistry RegisterInstance<TService>(TService instance, string serviceName);

		// Token: 0x0600044B RID: 1099
		IServiceRegistry RegisterInstance(Type serviceType, object instance);

		// Token: 0x0600044C RID: 1100
		IServiceRegistry RegisterInstance(Type serviceType, object instance, string serviceName);

		// Token: 0x0600044D RID: 1101
		IServiceRegistry Register<TService>();

		// Token: 0x0600044E RID: 1102
		IServiceRegistry Register<TService>(ILifetime lifetime);

		// Token: 0x0600044F RID: 1103
		IServiceRegistry Register(Type serviceType);

		// Token: 0x06000450 RID: 1104
		IServiceRegistry Register(Type serviceType, ILifetime lifetime);

		// Token: 0x06000451 RID: 1105
		IServiceRegistry Register<TService>(Func<IServiceFactory, TService> factory);

		// Token: 0x06000452 RID: 1106
		IServiceRegistry Register<T, TService>(Func<IServiceFactory, T, TService> factory);

		// Token: 0x06000453 RID: 1107
		IServiceRegistry Register<T, TService>(Func<IServiceFactory, T, TService> factory, string serviceName);

		// Token: 0x06000454 RID: 1108
		IServiceRegistry Register<T1, T2, TService>(Func<IServiceFactory, T1, T2, TService> factory);

		// Token: 0x06000455 RID: 1109
		IServiceRegistry Register<T1, T2, TService>(Func<IServiceFactory, T1, T2, TService> factory, string serviceName);

		// Token: 0x06000456 RID: 1110
		IServiceRegistry Register<T1, T2, T3, TService>(Func<IServiceFactory, T1, T2, T3, TService> factory);

		// Token: 0x06000457 RID: 1111
		IServiceRegistry Register<T1, T2, T3, TService>(Func<IServiceFactory, T1, T2, T3, TService> factory, string serviceName);

		// Token: 0x06000458 RID: 1112
		IServiceRegistry Register<T1, T2, T3, T4, TService>(Func<IServiceFactory, T1, T2, T3, T4, TService> factory);

		// Token: 0x06000459 RID: 1113
		IServiceRegistry Register<T1, T2, T3, T4, TService>(Func<IServiceFactory, T1, T2, T3, T4, TService> factory, string serviceName);

		// Token: 0x0600045A RID: 1114
		IServiceRegistry Register<TService>(Func<IServiceFactory, TService> factory, ILifetime lifetime);

		// Token: 0x0600045B RID: 1115
		IServiceRegistry Register<TService>(Func<IServiceFactory, TService> factory, string serviceName);

		// Token: 0x0600045C RID: 1116
		IServiceRegistry Register<TService>(Func<IServiceFactory, TService> factory, string serviceName, ILifetime lifetime);

		// Token: 0x0600045D RID: 1117
		IServiceRegistry RegisterOrdered(Type serviceType, Type[] implementingTypes, Func<Type, ILifetime> lifetimeFactory);

		// Token: 0x0600045E RID: 1118
		IServiceRegistry RegisterOrdered(Type serviceType, Type[] implementingTypes, Func<Type, ILifetime> lifeTimeFactory, Func<int, string> serviceNameFormatter);

		// Token: 0x0600045F RID: 1119
		IServiceRegistry RegisterFallback(Func<Type, string, bool> predicate, Func<ServiceRequest, object> factory);

		// Token: 0x06000460 RID: 1120
		IServiceRegistry RegisterFallback(Func<Type, string, bool> predicate, Func<ServiceRequest, object> factory, ILifetime lifetime);

		// Token: 0x06000461 RID: 1121
		IServiceRegistry Register(ServiceRegistration serviceRegistration);

		// Token: 0x06000462 RID: 1122
		IServiceRegistry RegisterAssembly(Assembly assembly);

		// Token: 0x06000463 RID: 1123
		IServiceRegistry RegisterAssembly(Assembly assembly, Func<Type, Type, bool> shouldRegister);

		// Token: 0x06000464 RID: 1124
		IServiceRegistry RegisterAssembly(Assembly assembly, Func<ILifetime> lifetimeFactory);

		// Token: 0x06000465 RID: 1125
		IServiceRegistry RegisterAssembly(Assembly assembly, Func<ILifetime> lifetimeFactory, Func<Type, Type, bool> shouldRegister);

		// Token: 0x06000466 RID: 1126
		IServiceRegistry RegisterAssembly(Assembly assembly, Func<ILifetime> lifetimeFactory, Func<Type, Type, bool> shouldRegister, Func<Type, Type, string> serviceNameProvider);

		// Token: 0x06000467 RID: 1127
		IServiceRegistry RegisterFrom<TCompositionRoot>() where TCompositionRoot : ICompositionRoot, new();

		// Token: 0x06000468 RID: 1128
		IServiceRegistry RegisterFrom<TCompositionRoot>(TCompositionRoot compositionRoot) where TCompositionRoot : ICompositionRoot;

		// Token: 0x06000469 RID: 1129
		IServiceRegistry RegisterConstructorDependency<TDependency>(Func<IServiceFactory, ParameterInfo, TDependency> factory);

		// Token: 0x0600046A RID: 1130
		IServiceRegistry RegisterConstructorDependency<TDependency>(Func<IServiceFactory, ParameterInfo, object[], TDependency> factory);

		// Token: 0x0600046B RID: 1131
		IServiceRegistry RegisterPropertyDependency<TDependency>(Func<IServiceFactory, PropertyInfo, TDependency> factory);

		// Token: 0x0600046C RID: 1132
		IServiceRegistry RegisterAssembly(string searchPattern);

		// Token: 0x0600046D RID: 1133
		IServiceRegistry Decorate(Type serviceType, Type decoratorType, Func<ServiceRegistration, bool> predicate);

		// Token: 0x0600046E RID: 1134
		IServiceRegistry Decorate(Type serviceType, Type decoratorType);

		// Token: 0x0600046F RID: 1135
		IServiceRegistry Decorate<TService, TDecorator>() where TDecorator : TService;

		// Token: 0x06000470 RID: 1136
		IServiceRegistry Decorate<TService>(Func<IServiceFactory, TService, TService> factory);

		// Token: 0x06000471 RID: 1137
		IServiceRegistry Decorate(DecoratorRegistration decoratorRegistration);

		// Token: 0x06000472 RID: 1138
		IServiceRegistry Override(Func<ServiceRegistration, bool> serviceSelector, Func<IServiceFactory, ServiceRegistration, ServiceRegistration> serviceRegistrationFactory);

		// Token: 0x06000473 RID: 1139
		IServiceRegistry Initialize(Func<ServiceRegistration, bool> predicate, Action<IServiceFactory, object> processor);

		// Token: 0x06000474 RID: 1140
		IServiceRegistry SetDefaultLifetime<T>() where T : ILifetime, new();
	}
}
