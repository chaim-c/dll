using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Threading;

namespace MCM.LightInject
{
	// Token: 0x020000ED RID: 237
	[ExcludeFromCodeCoverage]
	internal class ServiceContainer : IServiceContainer, IServiceRegistry, IServiceFactory, IDisposable
	{
		// Token: 0x06000519 RID: 1305 RVA: 0x0000DDB0 File Offset: 0x0000BFB0
		public ServiceContainer() : this(ContainerOptions.Default)
		{
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0000DDC0 File Offset: 0x0000BFC0
		public ServiceContainer(Action<ContainerOptions> configureOptions)
		{
			this.emitters = new ServiceContainer.ServiceRegistry<Action<IEmitter>>();
			this.constructorDependencyFactories = new ServiceContainer.ServiceRegistry<Delegate>();
			this.propertyDependencyFactories = new ServiceContainer.ServiceRegistry<Delegate>();
			this.availableServices = new ServiceContainer.ServiceRegistry<ServiceRegistration>();
			this.lockObject = new object();
			this.constants = new ServiceContainer.Storage<object>();
			this.disposableLifeTimes = new ServiceContainer.Storage<ILifetime>();
			this.decorators = new ServiceContainer.Storage<DecoratorRegistration>();
			this.overrides = new ServiceContainer.Storage<ServiceContainer.ServiceOverride>();
			this.factoryRules = new ServiceContainer.Storage<ServiceContainer.FactoryRule>();
			this.initializers = new ServiceContainer.Storage<ServiceContainer.Initializer>();
			this.dependencyStack = new Stack<Action<IEmitter>>();
			this.disposableObjects = new List<IDisposable>();
			this.servicesToDelegatesIndex = new LazyConcurrentDictionary<ServiceRegistration, int>();
			this.delegates = ImmutableHashTable<Type, GetInstanceDelegate>.Empty;
			this.namedDelegates = ImmutableHashTable<ValueTuple<Type, string>, GetInstanceDelegate>.Empty;
			this.propertyInjectionDelegates = ImmutableHashTree<Type, Func<object[], Scope, object, object>>.Empty;
			base..ctor();
			this.options = new ContainerOptions();
			configureOptions(this.options);
			this.log = this.options.LogFactory(typeof(ServiceContainer));
			CachedTypeExtractor concreteTypeExtractor = new CachedTypeExtractor(new ConcreteTypeExtractor());
			this.CompositionRootTypeExtractor = new CachedTypeExtractor(new CompositionRootTypeExtractor(new CompositionRootAttributeExtractor()));
			this.CompositionRootExecutor = new CompositionRootExecutor(this, (Type type) => (ICompositionRoot)Activator.CreateInstance(type));
			this.ServiceNameProvider = new ServiceNameProvider();
			IPropertyDependencySelector propertyDependencySelector2;
			if (!this.options.EnablePropertyInjection)
			{
				IPropertyDependencySelector propertyDependencySelector = new ServiceContainer.PropertyDependencyDisabler();
				propertyDependencySelector2 = propertyDependencySelector;
			}
			else
			{
				IPropertyDependencySelector propertyDependencySelector = new PropertyDependencySelector(new PropertySelector());
				propertyDependencySelector2 = propertyDependencySelector;
			}
			this.PropertyDependencySelector = propertyDependencySelector2;
			this.GenericArgumentMapper = new GenericArgumentMapper();
			this.AssemblyScanner = new AssemblyScanner(concreteTypeExtractor, this.CompositionRootTypeExtractor, this.CompositionRootExecutor, this.GenericArgumentMapper);
			this.ConstructorDependencySelector = new ConstructorDependencySelector();
			this.ConstructorSelector = new MostResolvableConstructorSelector(new Func<Type, string, bool>(this.CanGetInstance), this.options.EnableOptionalArguments);
			this.constructionInfoProvider = new Lazy<IConstructionInfoProvider>(new Func<IConstructionInfoProvider>(this.CreateConstructionInfoProvider));
			this.methodSkeletonFactory = ((Type returnType, Type[] parameterTypes) => new ServiceContainer.DynamicMethodSkeleton(returnType, parameterTypes));
			this.ScopeManagerProvider = new PerLogicalCallContextScopeManagerProvider();
			this.AssemblyLoader = new AssemblyLoader();
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0000DFFC File Offset: 0x0000C1FC
		public ServiceContainer(ContainerOptions options) : this(delegate(ContainerOptions o)
		{
			o.LogFactory = options.LogFactory;
			o.DefaultServiceSelector = options.DefaultServiceSelector;
			o.EnableCurrentScope = options.EnableCurrentScope;
			o.EnablePropertyInjection = options.EnablePropertyInjection;
			o.EnableVariance = options.EnableVariance;
			o.VarianceFilter = options.VarianceFilter;
			o.EnableOptionalArguments = options.EnableOptionalArguments;
			o.OptimizeForLargeObjectGraphs = options.OptimizeForLargeObjectGraphs;
		})
		{
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0000E02C File Offset: 0x0000C22C
		private ServiceContainer(ContainerOptions options, ServiceContainer.ServiceRegistry<Delegate> constructorDependencyFactories, ServiceContainer.ServiceRegistry<Delegate> propertyDependencyFactories, ServiceContainer.ServiceRegistry<ServiceRegistration> availableServices, ServiceContainer.Storage<DecoratorRegistration> decorators, ServiceContainer.Storage<ServiceContainer.ServiceOverride> overrides, ServiceContainer.Storage<ServiceContainer.FactoryRule> factoryRules, ServiceContainer.Storage<ServiceContainer.Initializer> initializers, Lazy<IConstructionInfoProvider> constructionInfoProvider, Func<Type, Type[], IMethodSkeleton> methodSkeletonFactory, Action<LogEntry> log, ICompositionRootExecutor compositionRootExecutor, IServiceNameProvider serviceNameProvider, IPropertyDependencySelector propertyDependencySelector, IGenericArgumentMapper genericArgumentMapper, IAssemblyScanner assemblyScanner, IConstructorDependencySelector constructorDependencySelector, IConstructorSelector constructorSelector, IAssemblyLoader assemblyLoader, IScopeManagerProvider scopeManagerProvider)
		{
			this.emitters = new ServiceContainer.ServiceRegistry<Action<IEmitter>>();
			this.constructorDependencyFactories = new ServiceContainer.ServiceRegistry<Delegate>();
			this.propertyDependencyFactories = new ServiceContainer.ServiceRegistry<Delegate>();
			this.availableServices = new ServiceContainer.ServiceRegistry<ServiceRegistration>();
			this.lockObject = new object();
			this.constants = new ServiceContainer.Storage<object>();
			this.disposableLifeTimes = new ServiceContainer.Storage<ILifetime>();
			this.decorators = new ServiceContainer.Storage<DecoratorRegistration>();
			this.overrides = new ServiceContainer.Storage<ServiceContainer.ServiceOverride>();
			this.factoryRules = new ServiceContainer.Storage<ServiceContainer.FactoryRule>();
			this.initializers = new ServiceContainer.Storage<ServiceContainer.Initializer>();
			this.dependencyStack = new Stack<Action<IEmitter>>();
			this.disposableObjects = new List<IDisposable>();
			this.servicesToDelegatesIndex = new LazyConcurrentDictionary<ServiceRegistration, int>();
			this.delegates = ImmutableHashTable<Type, GetInstanceDelegate>.Empty;
			this.namedDelegates = ImmutableHashTable<ValueTuple<Type, string>, GetInstanceDelegate>.Empty;
			this.propertyInjectionDelegates = ImmutableHashTree<Type, Func<object[], Scope, object, object>>.Empty;
			base..ctor();
			this.options = options;
			this.constructorDependencyFactories = constructorDependencyFactories;
			this.propertyDependencyFactories = propertyDependencyFactories;
			this.availableServices = availableServices;
			this.decorators = decorators;
			this.overrides = overrides;
			this.factoryRules = factoryRules;
			this.initializers = initializers;
			this.constructionInfoProvider = constructionInfoProvider;
			this.methodSkeletonFactory = methodSkeletonFactory;
			this.log = log;
			this.CompositionRootExecutor = compositionRootExecutor;
			this.ServiceNameProvider = serviceNameProvider;
			this.PropertyDependencySelector = propertyDependencySelector;
			this.GenericArgumentMapper = genericArgumentMapper;
			this.AssemblyScanner = assemblyScanner;
			this.ConstructorDependencySelector = constructorDependencySelector;
			this.ConstructorSelector = constructorSelector;
			this.ScopeManagerProvider = scopeManagerProvider;
			this.AssemblyLoader = assemblyLoader;
			foreach (ServiceRegistration availableService in this.AvailableServices)
			{
				this.Register(availableService);
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x0600051D RID: 1309 RVA: 0x0000E1E8 File Offset: 0x0000C3E8
		// (set) Token: 0x0600051E RID: 1310 RVA: 0x0000E1F0 File Offset: 0x0000C3F0
		public IScopeManagerProvider ScopeManagerProvider { get; set; }

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x0600051F RID: 1311 RVA: 0x0000E1F9 File Offset: 0x0000C3F9
		// (set) Token: 0x06000520 RID: 1312 RVA: 0x0000E201 File Offset: 0x0000C401
		public IPropertyDependencySelector PropertyDependencySelector { get; set; }

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x0000E20A File Offset: 0x0000C40A
		// (set) Token: 0x06000522 RID: 1314 RVA: 0x0000E212 File Offset: 0x0000C412
		public ITypeExtractor CompositionRootTypeExtractor { get; set; }

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000523 RID: 1315 RVA: 0x0000E21B File Offset: 0x0000C41B
		// (set) Token: 0x06000524 RID: 1316 RVA: 0x0000E223 File Offset: 0x0000C423
		public IServiceNameProvider ServiceNameProvider { get; set; }

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x0000E22C File Offset: 0x0000C42C
		// (set) Token: 0x06000526 RID: 1318 RVA: 0x0000E234 File Offset: 0x0000C434
		public ICompositionRootExecutor CompositionRootExecutor { get; set; }

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000527 RID: 1319 RVA: 0x0000E23D File Offset: 0x0000C43D
		// (set) Token: 0x06000528 RID: 1320 RVA: 0x0000E245 File Offset: 0x0000C445
		public IConstructorDependencySelector ConstructorDependencySelector { get; set; }

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000529 RID: 1321 RVA: 0x0000E24E File Offset: 0x0000C44E
		// (set) Token: 0x0600052A RID: 1322 RVA: 0x0000E256 File Offset: 0x0000C456
		public IConstructorSelector ConstructorSelector { get; set; }

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x0000E25F File Offset: 0x0000C45F
		// (set) Token: 0x0600052C RID: 1324 RVA: 0x0000E267 File Offset: 0x0000C467
		public IGenericArgumentMapper GenericArgumentMapper { get; set; }

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x0000E270 File Offset: 0x0000C470
		// (set) Token: 0x0600052E RID: 1326 RVA: 0x0000E278 File Offset: 0x0000C478
		public IAssemblyScanner AssemblyScanner { get; set; }

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x0000E281 File Offset: 0x0000C481
		// (set) Token: 0x06000530 RID: 1328 RVA: 0x0000E289 File Offset: 0x0000C489
		public IAssemblyLoader AssemblyLoader { get; set; }

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x0000E294 File Offset: 0x0000C494
		public IEnumerable<ServiceRegistration> AvailableServices
		{
			get
			{
				return this.availableServices.Values.SelectMany((ThreadSafeDictionary<string, ServiceRegistration> t) => t.Values);
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x0000E2D5 File Offset: 0x0000C4D5
		private ILifetime DefaultLifetime
		{
			get
			{
				return (ILifetime)((this.defaultLifetimeType != null) ? Activator.CreateInstance(this.defaultLifetimeType) : null);
			}
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0000E2F8 File Offset: 0x0000C4F8
		public bool CanGetInstance(Type serviceType, string serviceName)
		{
			bool flag = serviceType.IsFuncRepresentingService() || serviceType.IsFuncRepresentingNamedService() || serviceType.IsFuncWithParameters() || serviceType.IsLazy();
			bool result;
			if (flag)
			{
				Type returnType = serviceType.GenericTypeArguments.Last<Type>();
				result = (this.GetEmitMethod(returnType, serviceName) != null || this.availableServices.ContainsKey(serviceType));
			}
			else
			{
				result = (this.GetEmitMethod(serviceType, serviceName) != null);
			}
			return result;
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x0000E364 File Offset: 0x0000C564
		public Scope BeginScope()
		{
			bool enableCurrentScope = this.options.EnableCurrentScope;
			Scope result;
			if (enableCurrentScope)
			{
				result = this.ScopeManagerProvider.GetScopeManager(this).BeginScope();
			}
			else
			{
				result = new Scope(this);
			}
			return result;
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x0000E3A4 File Offset: 0x0000C5A4
		public object InjectProperties(object instance)
		{
			Type type = instance.GetType();
			Func<object[], Scope, object, object> del = this.propertyInjectionDelegates.Search(type);
			bool flag = del == null;
			if (flag)
			{
				del = this.CreatePropertyInjectionDelegate(type);
				this.propertyInjectionDelegates = this.propertyInjectionDelegates.Add(type, del);
			}
			return del(this.constants.Items, null, instance);
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x0000E404 File Offset: 0x0000C604
		public IServiceRegistry Register<TService>(Func<IServiceFactory, TService> factory, string serviceName, ILifetime lifetime)
		{
			this.RegisterServiceFromLambdaExpression<TService>(factory, lifetime, serviceName);
			return this;
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x0000E421 File Offset: 0x0000C621
		public IServiceRegistry RegisterFallback(Func<Type, string, bool> predicate, Func<ServiceRequest, object> factory)
		{
			return this.RegisterFallback(predicate, factory, this.DefaultLifetime);
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0000E434 File Offset: 0x0000C634
		public IServiceRegistry RegisterFallback(Func<Type, string, bool> predicate, Func<ServiceRequest, object> factory, ILifetime lifetime)
		{
			this.factoryRules.Add(new ServiceContainer.FactoryRule
			{
				CanCreateInstance = predicate,
				Factory = factory,
				LifeTime = lifetime
			});
			return this;
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x0000E470 File Offset: 0x0000C670
		public IServiceRegistry Register(ServiceRegistration serviceRegistration)
		{
			ThreadSafeDictionary<string, ServiceRegistration> services = this.GetAvailableServices(serviceRegistration.ServiceType);
			ServiceRegistration sr = serviceRegistration;
			services.AddOrUpdate(serviceRegistration.ServiceName, (string s) => this.AddServiceRegistration(sr), (string k, ServiceRegistration existing) => this.UpdateServiceRegistration(existing, sr));
			return this;
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x0000E4CC File Offset: 0x0000C6CC
		public IServiceRegistry RegisterAssembly(Assembly assembly)
		{
			Type[] compositionRootTypes = this.CompositionRootTypeExtractor.Execute(assembly);
			bool flag = compositionRootTypes.Length == 0;
			if (flag)
			{
				this.RegisterAssembly(assembly, (Type serviceType, Type implementingType) => true);
			}
			else
			{
				this.AssemblyScanner.Scan(assembly, this);
			}
			return this;
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x0000E530 File Offset: 0x0000C730
		public IServiceRegistry RegisterAssembly(Assembly assembly, Func<Type, Type, bool> shouldRegister)
		{
			return this.RegisterAssembly(assembly, () => this.DefaultLifetime, shouldRegister);
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0000E546 File Offset: 0x0000C746
		public IServiceRegistry RegisterAssembly(Assembly assembly, Func<ILifetime> lifetimeFactory)
		{
			return this.RegisterAssembly(assembly, lifetimeFactory, (Type serviceType, Type implementingType) => true);
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0000E56F File Offset: 0x0000C76F
		public IServiceRegistry RegisterAssembly(Assembly assembly, Func<ILifetime> lifetimeFactory, Func<Type, Type, bool> shouldRegister)
		{
			return this.RegisterAssembly(assembly, lifetimeFactory, shouldRegister, new Func<Type, Type, string>(this.ServiceNameProvider.GetServiceName));
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x0000E58C File Offset: 0x0000C78C
		public IServiceRegistry RegisterAssembly(Assembly assembly, Func<ILifetime> lifetimeFactory, Func<Type, Type, bool> shouldRegister, Func<Type, Type, string> serviceNameProvider)
		{
			this.AssemblyScanner.Scan(assembly, this, lifetimeFactory, shouldRegister, serviceNameProvider);
			return this;
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0000E5B4 File Offset: 0x0000C7B4
		public IServiceRegistry RegisterFrom<TCompositionRoot>() where TCompositionRoot : ICompositionRoot, new()
		{
			this.CompositionRootExecutor.Execute(typeof(TCompositionRoot));
			return this;
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0000E5E0 File Offset: 0x0000C7E0
		public IServiceRegistry RegisterFrom<TCompositionRoot>(TCompositionRoot compositionRoot) where TCompositionRoot : ICompositionRoot
		{
			this.CompositionRootExecutor.Execute<TCompositionRoot>(compositionRoot);
			return this;
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x0000E600 File Offset: 0x0000C800
		public IServiceRegistry RegisterConstructorDependency<TDependency>(Func<IServiceFactory, ParameterInfo, TDependency> factory)
		{
			bool flag = this.isLocked;
			if (flag)
			{
				string message = string.Format("Attempt to register a constructor dependency {0} after the first call to GetInstance.", typeof(TDependency)) + string.Format("This might lead to incorrect behavior if a service with a {0} dependency has already been resolved", typeof(TDependency));
				this.log.Warning(message);
			}
			this.GetConstructorDependencyFactories(typeof(TDependency)).AddOrUpdate(string.Empty, (string s) => factory, (string s, Delegate e) => this.isLocked ? e : factory);
			return this;
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x0000E6A4 File Offset: 0x0000C8A4
		public IServiceRegistry RegisterConstructorDependency<TDependency>(Func<IServiceFactory, ParameterInfo, object[], TDependency> factory)
		{
			bool flag = this.isLocked;
			if (flag)
			{
				string message = string.Format("Attempt to register a constructor dependency {0} after the first call to GetInstance.", typeof(TDependency)) + string.Format("This might lead to incorrect behavior if a service with a {0} dependency has already been resolved", typeof(TDependency));
				this.log.Warning(message);
			}
			this.GetConstructorDependencyFactories(typeof(TDependency)).AddOrUpdate(string.Empty, (string s) => factory, (string s, Delegate e) => this.isLocked ? e : factory);
			return this;
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x0000E748 File Offset: 0x0000C948
		public IServiceRegistry RegisterPropertyDependency<TDependency>(Func<IServiceFactory, PropertyInfo, TDependency> factory)
		{
			bool flag = this.isLocked;
			if (flag)
			{
				string message = string.Format("Attempt to register a property dependency {0} after the first call to GetInstance.", typeof(TDependency)) + string.Format("This might lead to incorrect behavior if a service with a {0} dependency has already been resolved", typeof(TDependency));
				this.log.Warning(message);
			}
			this.GetPropertyDependencyFactories(typeof(TDependency)).AddOrUpdate(string.Empty, (string s) => factory, (string s, Delegate e) => this.isLocked ? e : factory);
			return this;
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x0000E7EC File Offset: 0x0000C9EC
		public IServiceRegistry RegisterAssembly(string searchPattern)
		{
			foreach (Assembly assembly in this.AssemblyLoader.Load(searchPattern))
			{
				this.RegisterAssembly(assembly);
			}
			return this;
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x0000E84C File Offset: 0x0000CA4C
		public IServiceRegistry Decorate(Type serviceType, Type decoratorType, Func<ServiceRegistration, bool> predicate)
		{
			DecoratorRegistration decoratorRegistration = new DecoratorRegistration
			{
				ServiceType = serviceType,
				ImplementingType = decoratorType,
				CanDecorate = predicate
			};
			this.Decorate(decoratorRegistration);
			return this;
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x0000E888 File Offset: 0x0000CA88
		public IServiceRegistry Decorate(Type serviceType, Type decoratorType)
		{
			this.Decorate(serviceType, decoratorType, (ServiceRegistration si) => true);
			return this;
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x0000E8C4 File Offset: 0x0000CAC4
		public IServiceRegistry Decorate<TService, TDecorator>() where TDecorator : TService
		{
			this.Decorate(typeof(TService), typeof(TDecorator));
			return this;
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x0000E8F4 File Offset: 0x0000CAF4
		public IServiceRegistry Decorate<TService>(Func<IServiceFactory, TService, TService> factory)
		{
			DecoratorRegistration decoratorRegistration2 = new DecoratorRegistration();
			decoratorRegistration2.FactoryExpression = factory;
			decoratorRegistration2.ServiceType = typeof(TService);
			decoratorRegistration2.CanDecorate = ((ServiceRegistration si) => true);
			DecoratorRegistration decoratorRegistration = decoratorRegistration2;
			this.Decorate(decoratorRegistration);
			return this;
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x0000E958 File Offset: 0x0000CB58
		public IServiceRegistry Decorate(DecoratorRegistration decoratorRegistration)
		{
			int index = this.decorators.Add(decoratorRegistration);
			decoratorRegistration.Index = index;
			return this;
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x0000E980 File Offset: 0x0000CB80
		public IServiceRegistry Override(Func<ServiceRegistration, bool> serviceSelector, Func<IServiceFactory, ServiceRegistration, ServiceRegistration> serviceRegistrationFactory)
		{
			ServiceContainer.ServiceOverride serviceOverride = new ServiceContainer.ServiceOverride(serviceSelector, serviceRegistrationFactory);
			this.overrides.Add(serviceOverride);
			return this;
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x0000E9A8 File Offset: 0x0000CBA8
		public IServiceRegistry Initialize(Func<ServiceRegistration, bool> predicate, Action<IServiceFactory, object> processor)
		{
			this.initializers.Add(new ServiceContainer.Initializer
			{
				Predicate = predicate,
				Initialize = processor
			});
			return this;
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x0000E9DC File Offset: 0x0000CBDC
		public IServiceRegistry Register(Type serviceType, Type implementingType, ILifetime lifetime)
		{
			this.Register(serviceType, implementingType, string.Empty, lifetime);
			return this;
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x0000EA00 File Offset: 0x0000CC00
		public IServiceRegistry Register(Type serviceType, Type implementingType, string serviceName, ILifetime lifetime)
		{
			this.RegisterService(serviceType, implementingType, lifetime, serviceName);
			return this;
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x0000EA20 File Offset: 0x0000CC20
		public IServiceRegistry Register<TService, TImplementation>() where TImplementation : TService
		{
			this.Register(typeof(TService), typeof(TImplementation));
			return this;
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x0000EA50 File Offset: 0x0000CC50
		public IServiceRegistry Register<TService, TImplementation>(ILifetime lifetime) where TImplementation : TService
		{
			this.Register(typeof(TService), typeof(TImplementation), lifetime);
			return this;
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x0000EA80 File Offset: 0x0000CC80
		public IServiceRegistry Register<TService, TImplementation>(string serviceName) where TImplementation : TService
		{
			this.Register<TService, TImplementation>(serviceName, null);
			return this;
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x0000EA9C File Offset: 0x0000CC9C
		public IServiceRegistry Register<TService, TImplementation>(string serviceName, ILifetime lifetime) where TImplementation : TService
		{
			this.Register(typeof(TService), typeof(TImplementation), serviceName, lifetime);
			return this;
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x0000EACC File Offset: 0x0000CCCC
		public IServiceRegistry Register<TService>(Func<IServiceFactory, TService> factory, ILifetime lifetime)
		{
			this.RegisterServiceFromLambdaExpression<TService>(factory, lifetime, string.Empty);
			return this;
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x0000EAF0 File Offset: 0x0000CCF0
		public IServiceRegistry Register<TService>(Func<IServiceFactory, TService> factory, string serviceName)
		{
			this.RegisterServiceFromLambdaExpression<TService>(factory, null, serviceName);
			return this;
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x0000EB10 File Offset: 0x0000CD10
		public IServiceRegistry Register<TService>()
		{
			this.Register<TService, TService>();
			return this;
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x0000EB2C File Offset: 0x0000CD2C
		public IServiceRegistry Register(Type serviceType)
		{
			this.Register(serviceType, serviceType);
			return this;
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x0000EB48 File Offset: 0x0000CD48
		public IServiceRegistry Register(Type serviceType, ILifetime lifetime)
		{
			this.Register(serviceType, serviceType, lifetime);
			return this;
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x0000EB68 File Offset: 0x0000CD68
		public IServiceRegistry Register<TService>(ILifetime lifetime)
		{
			this.Register<TService, TService>(lifetime);
			return this;
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x0000EB84 File Offset: 0x0000CD84
		public IServiceRegistry RegisterInstance<TService>(TService instance, string serviceName)
		{
			this.RegisterInstance(typeof(TService), instance, serviceName);
			return this;
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x0000EBB0 File Offset: 0x0000CDB0
		public IServiceRegistry RegisterInstance<TService>(TService instance)
		{
			this.RegisterInstance(typeof(TService), instance);
			return this;
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x0000EBDC File Offset: 0x0000CDDC
		public IServiceRegistry RegisterInstance(Type serviceType, object instance)
		{
			this.RegisterInstance(serviceType, instance, string.Empty);
			return this;
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x0000EC00 File Offset: 0x0000CE00
		public IServiceRegistry RegisterInstance(Type serviceType, object instance, string serviceName)
		{
			Ensure.IsNotNull<object>(instance, "instance");
			Ensure.IsNotNull<Type>(serviceType, "type");
			Ensure.IsNotNull<string>(serviceName, "serviceName");
			this.RegisterValue(serviceType, instance, serviceName);
			return this;
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x0000EC44 File Offset: 0x0000CE44
		public IServiceRegistry Register<TService>(Func<IServiceFactory, TService> factory)
		{
			this.RegisterServiceFromLambdaExpression<TService>(factory, null, string.Empty);
			return this;
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x0000EC68 File Offset: 0x0000CE68
		public IServiceRegistry Register<T, TService>(Func<IServiceFactory, T, TService> factory)
		{
			this.RegisterServiceFromLambdaExpression<TService>(factory, null, string.Empty);
			return this;
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x0000EC8C File Offset: 0x0000CE8C
		public IServiceRegistry Register<T, TService>(Func<IServiceFactory, T, TService> factory, string serviceName)
		{
			this.RegisterServiceFromLambdaExpression<TService>(factory, null, serviceName);
			return this;
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x0000ECAC File Offset: 0x0000CEAC
		public IServiceRegistry Register<T1, T2, TService>(Func<IServiceFactory, T1, T2, TService> factory)
		{
			this.RegisterServiceFromLambdaExpression<TService>(factory, null, string.Empty);
			return this;
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x0000ECD0 File Offset: 0x0000CED0
		public IServiceRegistry Register<T1, T2, TService>(Func<IServiceFactory, T1, T2, TService> factory, string serviceName)
		{
			this.RegisterServiceFromLambdaExpression<TService>(factory, null, serviceName);
			return this;
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x0000ECF0 File Offset: 0x0000CEF0
		public IServiceRegistry Register<T1, T2, T3, TService>(Func<IServiceFactory, T1, T2, T3, TService> factory)
		{
			this.RegisterServiceFromLambdaExpression<TService>(factory, null, string.Empty);
			return this;
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x0000ED14 File Offset: 0x0000CF14
		public IServiceRegistry Register<T1, T2, T3, TService>(Func<IServiceFactory, T1, T2, T3, TService> factory, string serviceName)
		{
			this.RegisterServiceFromLambdaExpression<TService>(factory, null, serviceName);
			return this;
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x0000ED34 File Offset: 0x0000CF34
		public IServiceRegistry Register<T1, T2, T3, T4, TService>(Func<IServiceFactory, T1, T2, T3, T4, TService> factory)
		{
			this.RegisterServiceFromLambdaExpression<TService>(factory, null, string.Empty);
			return this;
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x0000ED58 File Offset: 0x0000CF58
		public IServiceRegistry Register<T1, T2, T3, T4, TService>(Func<IServiceFactory, T1, T2, T3, T4, TService> factory, string serviceName)
		{
			this.RegisterServiceFromLambdaExpression<TService>(factory, null, serviceName);
			return this;
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x0000ED78 File Offset: 0x0000CF78
		public IServiceRegistry Register(Type serviceType, Type implementingType, string serviceName)
		{
			this.RegisterService(serviceType, implementingType, null, serviceName);
			return this;
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x0000ED98 File Offset: 0x0000CF98
		public IServiceRegistry Register(Type serviceType, Type implementingType)
		{
			this.RegisterService(serviceType, implementingType, null, string.Empty);
			return this;
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x0000EDBC File Offset: 0x0000CFBC
		public IServiceRegistry RegisterOrdered(Type serviceType, Type[] implementingTypes, Func<Type, ILifetime> lifeTimeFactory)
		{
			return this.RegisterOrdered(serviceType, implementingTypes, lifeTimeFactory, (int i) => i.ToString().PadLeft(3, '0'));
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x0000EDF8 File Offset: 0x0000CFF8
		public IServiceRegistry RegisterOrdered(Type serviceType, Type[] implementingTypes, Func<Type, ILifetime> lifeTimeFactory, Func<int, string> serviceNameFormatter)
		{
			int offset = this.GetAvailableServices(serviceType).Count;
			foreach (Type implementingType in implementingTypes)
			{
				offset++;
				this.Register(serviceType, implementingType, serviceNameFormatter(offset), lifeTimeFactory(implementingType));
			}
			return this;
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0000EE50 File Offset: 0x0000D050
		public void Compile(Func<ServiceRegistration, bool> predicate)
		{
			ServiceRegistration[] rootServices = this.AvailableServices.Where(predicate).ToArray<ServiceRegistration>();
			foreach (ServiceRegistration rootService in rootServices)
			{
				bool isGenericTypeDefinition = rootService.ServiceType.GetTypeInfo().IsGenericTypeDefinition;
				if (isGenericTypeDefinition)
				{
					this.log.Warning("Unable to precompile open generic type '" + ServiceContainer.<Compile>g__GetPrettyName|127_0(rootService.ServiceType) + "'");
				}
				else
				{
					bool flag = string.IsNullOrWhiteSpace(rootService.ServiceName);
					if (flag)
					{
						this.CreateDefaultDelegate(rootService.ServiceType, true);
					}
					else
					{
						this.CreateNamedDelegate(new ValueTuple<Type, string>(rootService.ServiceType, rootService.ServiceName), true);
					}
				}
			}
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0000EF0B File Offset: 0x0000D10B
		public void Compile()
		{
			this.Compile((ServiceRegistration sr) => true);
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x0000EF34 File Offset: 0x0000D134
		public void Compile<TService>(string serviceName = null)
		{
			bool flag = string.IsNullOrWhiteSpace(serviceName);
			if (flag)
			{
				this.CreateDefaultDelegate(typeof(TService), true);
			}
			else
			{
				this.CreateNamedDelegate(new ValueTuple<Type, string>(typeof(TService), serviceName), true);
			}
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x0000EF7C File Offset: 0x0000D17C
		public object GetInstance(Type serviceType)
		{
			GetInstanceDelegate instanceDelegate = this.delegates.Search(serviceType);
			bool flag = instanceDelegate == null;
			if (flag)
			{
				instanceDelegate = this.CreateDefaultDelegate(serviceType, true);
			}
			return instanceDelegate(this.constants.Items, null);
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x0000EFC0 File Offset: 0x0000D1C0
		public object GetInstance(Type serviceType, object[] arguments)
		{
			GetInstanceDelegate instanceDelegate = this.delegates.Search(serviceType);
			bool flag = instanceDelegate == null;
			if (flag)
			{
				instanceDelegate = this.CreateDefaultDelegate(serviceType, true);
			}
			object[] constantsWithArguments = this.constants.Items.Concat(new object[]
			{
				arguments
			}).ToArray<object>();
			return instanceDelegate(constantsWithArguments, null);
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x0000F01C File Offset: 0x0000D21C
		public object GetInstance(Type serviceType, string serviceName, object[] arguments)
		{
			ValueTuple<Type, string> key = new ValueTuple<Type, string>(serviceType, serviceName);
			GetInstanceDelegate instanceDelegate = this.namedDelegates.Search(key);
			bool flag = instanceDelegate == null;
			if (flag)
			{
				instanceDelegate = this.CreateNamedDelegate(key, true);
			}
			object[] constantsWithArguments = this.constants.Items.Concat(new object[]
			{
				arguments
			}).ToArray<object>();
			return instanceDelegate(constantsWithArguments, null);
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x0000F084 File Offset: 0x0000D284
		public object TryGetInstance(Type serviceType)
		{
			GetInstanceDelegate instanceDelegate = this.delegates.Search(serviceType);
			bool flag = instanceDelegate == null;
			if (flag)
			{
				instanceDelegate = this.CreateDefaultDelegate(serviceType, false);
			}
			return instanceDelegate(this.constants.Items, null);
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x0000F0C8 File Offset: 0x0000D2C8
		public object TryGetInstance(Type serviceType, string serviceName)
		{
			ValueTuple<Type, string> key = new ValueTuple<Type, string>(serviceType, serviceName);
			GetInstanceDelegate instanceDelegate = this.namedDelegates.Search(key);
			bool flag = instanceDelegate == null;
			if (flag)
			{
				instanceDelegate = this.CreateNamedDelegate(key, false);
			}
			return instanceDelegate(this.constants.Items, null);
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x0000F118 File Offset: 0x0000D318
		public object GetInstance(Type serviceType, string serviceName)
		{
			ValueTuple<Type, string> key = new ValueTuple<Type, string>(serviceType, serviceName);
			GetInstanceDelegate instanceDelegate = this.namedDelegates.Search(key);
			bool flag = instanceDelegate == null;
			if (flag)
			{
				instanceDelegate = this.CreateNamedDelegate(key, true);
			}
			return instanceDelegate(this.constants.Items, null);
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x0000F168 File Offset: 0x0000D368
		public IEnumerable<object> GetAllInstances(Type serviceType)
		{
			return (IEnumerable<object>)this.GetInstance(serviceType.GetEnumerableType());
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x0000F18C File Offset: 0x0000D38C
		public object Create(Type serviceType)
		{
			this.Register(serviceType);
			return this.GetInstance(serviceType);
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x0000F1B0 File Offset: 0x0000D3B0
		public IServiceRegistry SetDefaultLifetime<T>() where T : ILifetime, new()
		{
			this.defaultLifetimeType = typeof(T);
			return this;
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x0000F1D4 File Offset: 0x0000D3D4
		public void Dispose()
		{
			IEnumerable<IDisposable> disposableLifetimeInstances = (from lt in this.disposableLifeTimes.Items
			where lt is IDisposable
			select lt).Cast<IDisposable>().Reverse<IDisposable>();
			foreach (IDisposable disposableLifetimeInstance in disposableLifetimeInstances)
			{
				disposableLifetimeInstance.Dispose();
			}
			IEnumerable<IDisposable> perContainerDisposables = this.disposableObjects.AsEnumerable<IDisposable>().Reverse<IDisposable>();
			foreach (IDisposable perContainerDisposable in perContainerDisposables)
			{
				perContainerDisposable.Dispose();
			}
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x0000F2B0 File Offset: 0x0000D4B0
		public ServiceContainer Clone()
		{
			return new ServiceContainer(this.options, this.constructorDependencyFactories, this.propertyDependencyFactories, this.availableServices, this.decorators, this.overrides, this.factoryRules, this.initializers, this.constructionInfoProvider, this.methodSkeletonFactory, this.log, this.CompositionRootExecutor, this.ServiceNameProvider, this.PropertyDependencySelector, this.GenericArgumentMapper, this.AssemblyScanner, this.ConstructorDependencySelector, this.ConstructorSelector, this.AssemblyLoader, this.ScopeManagerProvider);
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x0000F340 File Offset: 0x0000D540
		internal static TService TrackInstance<TService>(TService instance, ServiceContainer container)
		{
			IDisposable disposable = instance as IDisposable;
			bool flag = disposable != null;
			if (flag)
			{
				container.disposableObjects.Add(disposable);
			}
			return instance;
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x0000F378 File Offset: 0x0000D578
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal object GetInstance(Type serviceType, Scope scope)
		{
			GetInstanceDelegate instanceDelegate = this.delegates.Search(serviceType);
			bool flag = instanceDelegate == null;
			if (flag)
			{
				instanceDelegate = this.CreateDefaultDelegate(serviceType, true);
			}
			return instanceDelegate(this.constants.Items, scope);
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0000F3BC File Offset: 0x0000D5BC
		internal object GetInstance(Type serviceType, Scope scope, string serviceName)
		{
			ValueTuple<Type, string> key = new ValueTuple<Type, string>(serviceType, serviceName);
			GetInstanceDelegate instanceDelegate = this.namedDelegates.Search(key);
			bool flag = instanceDelegate == null;
			if (flag)
			{
				instanceDelegate = this.CreateNamedDelegate(key, true);
			}
			return instanceDelegate(this.constants.Items, scope);
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x0000F40C File Offset: 0x0000D60C
		internal IEnumerable<object> GetAllInstances(Type serviceType, Scope scope)
		{
			return (IEnumerable<object>)this.GetInstance(serviceType.GetEnumerableType(), scope);
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x0000F430 File Offset: 0x0000D630
		internal object GetInstance(Type serviceType, object[] arguments, Scope scope)
		{
			GetInstanceDelegate instanceDelegate = this.delegates.Search(serviceType);
			bool flag = instanceDelegate == null;
			if (flag)
			{
				instanceDelegate = this.CreateDefaultDelegate(serviceType, true);
			}
			object[] constantsWithArguments = this.constants.Items.Concat(new object[]
			{
				arguments
			}).ToArray<object>();
			return instanceDelegate(constantsWithArguments, scope);
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x0000F48C File Offset: 0x0000D68C
		internal object GetInstance(Type serviceType, string serviceName, object[] arguments, Scope scope)
		{
			ValueTuple<Type, string> key = new ValueTuple<Type, string>(serviceType, serviceName);
			GetInstanceDelegate instanceDelegate = this.namedDelegates.Search(key);
			bool flag = instanceDelegate == null;
			if (flag)
			{
				instanceDelegate = this.CreateNamedDelegate(key, true);
			}
			object[] constantsWithArguments = this.constants.Items.Concat(new object[]
			{
				arguments
			}).ToArray<object>();
			return instanceDelegate(constantsWithArguments, scope);
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x0000F4F4 File Offset: 0x0000D6F4
		internal object TryGetInstance(Type serviceType, Scope scope)
		{
			GetInstanceDelegate instanceDelegate = this.delegates.Search(serviceType);
			bool flag = instanceDelegate == null;
			if (flag)
			{
				instanceDelegate = this.CreateDefaultDelegate(serviceType, false);
			}
			return instanceDelegate(this.constants.Items, scope);
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x0000F538 File Offset: 0x0000D738
		internal object TryGetInstance(Type serviceType, string serviceName, Scope scope)
		{
			ValueTuple<Type, string> key = new ValueTuple<Type, string>(serviceType, serviceName);
			GetInstanceDelegate instanceDelegate = this.namedDelegates.Search(key);
			bool flag = instanceDelegate == null;
			if (flag)
			{
				instanceDelegate = this.CreateNamedDelegate(key, false);
			}
			return instanceDelegate(this.constants.Items, scope);
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x0000F588 File Offset: 0x0000D788
		internal object Create(Type serviceType, Scope scope)
		{
			this.Register(serviceType);
			return this.GetInstance(serviceType, scope);
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0000F5AA File Offset: 0x0000D7AA
		private static void EmitEnumerable(IList<Action<IEmitter>> serviceEmitters, Type elementType, IEmitter emitter)
		{
			ServiceContainer.EmitNewArray(serviceEmitters, elementType, emitter);
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x0000F5B8 File Offset: 0x0000D7B8
		private static void EmitNewArray(IList<Action<IEmitter>> emitMethods, Type elementType, IEmitter emitter)
		{
			LocalBuilder array = emitter.DeclareLocal(elementType.MakeArrayType());
			emitter.Push(emitMethods.Count);
			emitter.PushNewArray(elementType);
			emitter.Store(array);
			for (int index = 0; index < emitMethods.Count; index++)
			{
				emitter.Push(array);
				emitter.Push(index);
				emitMethods[index](emitter);
				emitter.UnboxOrCast(elementType);
				emitter.Emit(OpCodes.Stelem, elementType);
			}
			emitter.Push(array);
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x0000F644 File Offset: 0x0000D844
		private static ILifetime CloneLifeTime(ILifetime lifetime)
		{
			ICloneableLifeTime cloneable = lifetime as ICloneableLifeTime;
			bool flag = cloneable != null;
			ILifetime result;
			if (flag)
			{
				result = cloneable.Clone();
			}
			else
			{
				result = ((lifetime == null) ? null : ((ILifetime)Activator.CreateInstance(lifetime.GetType())));
			}
			return result;
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x0000F688 File Offset: 0x0000D888
		private static ConstructorDependency GetConstructorDependencyThatRepresentsDecoratorTarget(DecoratorRegistration decoratorRegistration, ConstructionInfo constructionInfo)
		{
			return constructionInfo.ConstructorDependencies.FirstOrDefault((ConstructorDependency cd) => cd.ServiceType == decoratorRegistration.ServiceType || (cd.ServiceType.IsLazy() && cd.ServiceType.GetTypeInfo().GenericTypeArguments[0] == decoratorRegistration.ServiceType));
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x0000F6C0 File Offset: 0x0000D8C0
		private static void PushRuntimeArguments(IEmitter emitter)
		{
			MethodInfo loadMethod = typeof(RuntimeArgumentsLoader).GetTypeInfo().GetDeclaredMethod("Load");
			emitter.Emit(OpCodes.Ldarg_0);
			emitter.Emit(OpCodes.Call, loadMethod);
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0000F704 File Offset: 0x0000D904
		private DecoratorRegistration CreateClosedGenericDecoratorRegistration(ServiceRegistration serviceRegistration, DecoratorRegistration openGenericDecorator)
		{
			Type implementingType = openGenericDecorator.ImplementingType;
			Type serviceType = serviceRegistration.ServiceType;
			Type[] genericTypeArguments = serviceType.GenericTypeArguments;
			Type closedGenericDecoratorType;
			bool flag = !this.TryCreateClosedGenericDecoratorType(serviceType, implementingType, out closedGenericDecoratorType);
			DecoratorRegistration result;
			if (flag)
			{
				this.log.Info(string.Concat(new string[]
				{
					"Skipping decorator [",
					implementingType.FullName,
					"] since it is incompatible with the service type [",
					serviceType.FullName,
					"]"
				}));
				result = null;
			}
			else
			{
				DecoratorRegistration decoratorInfo = new DecoratorRegistration
				{
					ServiceType = serviceRegistration.ServiceType,
					ImplementingType = closedGenericDecoratorType,
					CanDecorate = openGenericDecorator.CanDecorate,
					Index = openGenericDecorator.Index
				};
				result = decoratorInfo;
			}
			return result;
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x0000F7C0 File Offset: 0x0000D9C0
		private bool TryCreateClosedGenericDecoratorType(Type serviceType, Type implementingType, out Type closedGenericDecoratorType)
		{
			closedGenericDecoratorType = null;
			GenericMappingResult mapResult = this.GenericArgumentMapper.Map(serviceType, implementingType);
			bool flag = !mapResult.IsValid;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				closedGenericDecoratorType = TypeHelper.TryMakeGenericType(implementingType, mapResult.GetMappedArguments());
				bool flag2 = closedGenericDecoratorType == null;
				if (flag2)
				{
					result = false;
				}
				else
				{
					bool flag3 = !serviceType.GetTypeInfo().IsAssignableFrom(closedGenericDecoratorType.GetTypeInfo());
					result = !flag3;
				}
			}
			return result;
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x0000F838 File Offset: 0x0000DA38
		private Func<object[], Scope, object, object> CreatePropertyInjectionDelegate(Type concreteType)
		{
			object obj = this.lockObject;
			Func<object[], Scope, object, object> result;
			lock (obj)
			{
				IMethodSkeleton methodSkeleton = this.methodSkeletonFactory(typeof(object), new Type[]
				{
					typeof(object[]),
					typeof(Scope),
					typeof(object)
				});
				ConstructionInfo constructionInfo = new ConstructionInfo();
				constructionInfo.PropertyDependencies.AddRange(this.PropertyDependencySelector.Execute(concreteType));
				constructionInfo.ImplementingType = concreteType;
				IEmitter emitter = methodSkeleton.GetEmitter();
				emitter.PushArgument(2);
				emitter.Cast(concreteType);
				try
				{
					this.EmitPropertyDependencies(constructionInfo, emitter);
				}
				catch (Exception)
				{
					this.dependencyStack.Clear();
					throw;
				}
				emitter.Return();
				this.isLocked = true;
				result = (Func<object[], Scope, object, object>)methodSkeleton.CreateDelegate(typeof(Func<object[], Scope, object, object>));
			}
			return result;
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x0000F94C File Offset: 0x0000DB4C
		private ConstructionInfoProvider CreateConstructionInfoProvider()
		{
			return new ConstructionInfoProvider(this.CreateTypeConstructionInfoBuilder());
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x0000F96C File Offset: 0x0000DB6C
		private TypeConstructionInfoBuilder CreateTypeConstructionInfoBuilder()
		{
			return new TypeConstructionInfoBuilder(this.ConstructorSelector, this.ConstructorDependencySelector, this.PropertyDependencySelector, new Func<Type, string, Delegate>(this.GetConstructorDependencyDelegate), new Func<Type, string, Delegate>(this.GetPropertyDependencyExpression));
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x0000F9B0 File Offset: 0x0000DBB0
		private Delegate GetConstructorDependencyDelegate(Type type, string serviceName)
		{
			Delegate dependencyDelegate;
			this.GetConstructorDependencyFactories(type).TryGetValue(serviceName, out dependencyDelegate);
			return dependencyDelegate;
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x0000F9D4 File Offset: 0x0000DBD4
		private Delegate GetPropertyDependencyExpression(Type type, string serviceName)
		{
			Delegate dependencyDelegate;
			this.GetPropertyDependencyFactories(type).TryGetValue(serviceName, out dependencyDelegate);
			return dependencyDelegate;
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x0000F9F8 File Offset: 0x0000DBF8
		private GetInstanceDelegate CreateDynamicMethodDelegate(Action<IEmitter> serviceEmitter)
		{
			IMethodSkeleton methodSkeleton = this.methodSkeletonFactory(typeof(object), new Type[]
			{
				typeof(object[]),
				typeof(Scope)
			});
			IEmitter emitter = methodSkeleton.GetEmitter();
			serviceEmitter(emitter);
			bool isValueType = emitter.StackType.GetTypeInfo().IsValueType;
			if (isValueType)
			{
				emitter.Emit(OpCodes.Box, emitter.StackType);
			}
			Instruction lastInstruction = emitter.Instructions.Last<Instruction>();
			bool flag = lastInstruction.Code == OpCodes.Castclass;
			if (flag)
			{
				emitter.Instructions.Remove(lastInstruction);
			}
			emitter.Return();
			this.isLocked = true;
			return (GetInstanceDelegate)methodSkeleton.CreateDelegate(typeof(GetInstanceDelegate));
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0000FAD0 File Offset: 0x0000DCD0
		private Action<IEmitter> GetEmitMethod(Type serviceType, string serviceName)
		{
			Action<IEmitter> emitMethod = this.GetRegisteredEmitMethod(serviceType, serviceName);
			bool flag = emitMethod == null;
			if (flag)
			{
				emitMethod = this.TryGetFallbackEmitMethod(serviceType, serviceName);
			}
			bool flag2 = emitMethod == null;
			if (flag2)
			{
				this.AssemblyScanner.Scan(serviceType.GetTypeInfo().Assembly, this);
				emitMethod = this.GetRegisteredEmitMethod(serviceType, serviceName);
			}
			bool flag3 = emitMethod == null;
			if (flag3)
			{
				emitMethod = this.TryGetFallbackEmitMethod(serviceType, serviceName);
			}
			return this.CreateEmitMethodWrapper(emitMethod, serviceType, serviceName);
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x0000FB48 File Offset: 0x0000DD48
		private Action<IEmitter> TryGetFallbackEmitMethod(Type serviceType, string serviceName)
		{
			Action<IEmitter> emitMethod = null;
			ServiceContainer.FactoryRule rule = this.factoryRules.Items.FirstOrDefault((ServiceContainer.FactoryRule r) => r.CanCreateInstance(serviceType, serviceName));
			bool flag = rule != null;
			if (flag)
			{
				emitMethod = this.CreateServiceEmitterBasedOnFactoryRule(rule, serviceType, serviceName);
				this.RegisterEmitMethod(serviceType, serviceName, emitMethod);
			}
			return emitMethod;
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x0000FBC4 File Offset: 0x0000DDC4
		private Action<IEmitter> CreateEmitMethodWrapper(Action<IEmitter> emitter, Type serviceType, string serviceName)
		{
			bool flag = emitter == null;
			Action<IEmitter> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = delegate(IEmitter ms)
				{
					bool flag2 = this.dependencyStack.Contains(emitter);
					if (flag2)
					{
						throw new InvalidOperationException(string.Format("Recursive dependency detected: ServiceType:{0}, ServiceName:{1}]", serviceType, serviceName));
					}
					this.dependencyStack.Push(emitter);
					try
					{
						emitter(ms);
					}
					finally
					{
						bool flag3 = this.dependencyStack.Count > 0;
						if (flag3)
						{
							this.dependencyStack.Pop();
						}
					}
				};
			}
			return result;
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x0000FC18 File Offset: 0x0000DE18
		private Action<IEmitter> GetRegisteredEmitMethod(Type serviceType, string serviceName)
		{
			ThreadSafeDictionary<string, Action<IEmitter>> registrations = this.GetEmitMethods(serviceType);
			bool flag = string.IsNullOrWhiteSpace(serviceName);
			if (flag)
			{
				bool flag2 = registrations.Count > 1;
				if (flag2)
				{
					string[] serviceNames = (from k in registrations.Keys
					orderby k
					select k).ToArray<string>();
					serviceName = this.options.DefaultServiceSelector(serviceNames);
				}
				else
				{
					serviceName = string.Empty;
				}
			}
			Action<IEmitter> emitMethod;
			registrations.TryGetValue(serviceName, out emitMethod);
			return emitMethod ?? this.CreateEmitMethodForUnknownService(serviceType, serviceName);
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x0000FCB8 File Offset: 0x0000DEB8
		private ServiceRegistration AddServiceRegistration(ServiceRegistration serviceRegistration)
		{
			Action<IEmitter> emitMethod = this.ResolveEmitMethod(serviceRegistration);
			this.RegisterEmitMethod(serviceRegistration.ServiceType, serviceRegistration.ServiceName, emitMethod);
			return serviceRegistration;
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x0000FCE7 File Offset: 0x0000DEE7
		private void RegisterEmitMethod(Type serviceType, string serviceName, Action<IEmitter> emitMethod)
		{
			this.GetEmitMethods(serviceType).TryAdd(serviceName, emitMethod);
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x0000FCFC File Offset: 0x0000DEFC
		private ServiceRegistration UpdateServiceRegistration(ServiceRegistration existingRegistration, ServiceRegistration newRegistration)
		{
			bool flag = this.isLocked;
			ServiceRegistration result;
			if (flag)
			{
				string message = string.Format("Cannot overwrite existing serviceregistration {0} after the first call to GetInstance.", existingRegistration);
				this.log.Warning(message);
				result = existingRegistration;
			}
			else
			{
				Action<IEmitter> emitMethod = this.ResolveEmitMethod(newRegistration);
				ThreadSafeDictionary<string, Action<IEmitter>> serviceEmitters = this.GetEmitMethods(newRegistration.ServiceType);
				serviceEmitters[newRegistration.ServiceName] = emitMethod;
				result = newRegistration;
			}
			return result;
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x0000FD60 File Offset: 0x0000DF60
		private DecoratorRegistration[] GetDecorators(ServiceRegistration serviceRegistration)
		{
			List<DecoratorRegistration> registeredDecorators = (from d in this.decorators.Items
			where d.ServiceType == serviceRegistration.ServiceType
			select d).ToList<DecoratorRegistration>();
			registeredDecorators.AddRange(this.GetOpenGenericDecoratorRegistrations(serviceRegistration));
			registeredDecorators.AddRange(this.GetDeferredDecoratorRegistrations(serviceRegistration));
			return (from d in registeredDecorators
			orderby d.Index
			select d).ToArray<DecoratorRegistration>();
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0000FDF4 File Offset: 0x0000DFF4
		private IEnumerable<DecoratorRegistration> GetOpenGenericDecoratorRegistrations(ServiceRegistration serviceRegistration)
		{
			List<DecoratorRegistration> registrations = new List<DecoratorRegistration>();
			TypeInfo serviceTypeInfo = serviceRegistration.ServiceType.GetTypeInfo();
			bool isGenericType = serviceTypeInfo.IsGenericType;
			if (isGenericType)
			{
				Type openGenericServiceType = serviceTypeInfo.GetGenericTypeDefinition();
				IEnumerable<DecoratorRegistration> openGenericDecorators = from d in this.decorators.Items
				where d.ServiceType == openGenericServiceType
				select d;
				registrations.AddRange(from openGenericDecorator in openGenericDecorators
				select this.CreateClosedGenericDecoratorRegistration(serviceRegistration, openGenericDecorator) into dr
				where dr != null
				select dr);
			}
			return registrations;
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0000FEB4 File Offset: 0x0000E0B4
		private IEnumerable<DecoratorRegistration> GetDeferredDecoratorRegistrations(ServiceRegistration serviceRegistration)
		{
			List<DecoratorRegistration> registrations = new List<DecoratorRegistration>();
			IEnumerable<DecoratorRegistration> deferredDecorators = from ds in this.decorators.Items
			where ds.CanDecorate(serviceRegistration) && ds.HasDeferredImplementingType
			select ds;
			foreach (DecoratorRegistration deferredDecorator in deferredDecorators)
			{
				DecoratorRegistration decoratorRegistration2 = new DecoratorRegistration();
				decoratorRegistration2.ServiceType = serviceRegistration.ServiceType;
				decoratorRegistration2.ImplementingType = deferredDecorator.ImplementingTypeFactory(this, serviceRegistration);
				decoratorRegistration2.CanDecorate = ((ServiceRegistration sr) => true);
				decoratorRegistration2.Index = deferredDecorator.Index;
				DecoratorRegistration decoratorRegistration = decoratorRegistration2;
				registrations.Add(decoratorRegistration);
			}
			return registrations;
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x0000FFA8 File Offset: 0x0000E1A8
		private void EmitNewDecoratorInstance(DecoratorRegistration decoratorRegistration, IEmitter emitter, Action<IEmitter> pushInstance)
		{
			ConstructionInfo constructionInfo = this.GetConstructionInfo(decoratorRegistration);
			ConstructorDependency constructorDependency = ServiceContainer.GetConstructorDependencyThatRepresentsDecoratorTarget(decoratorRegistration, constructionInfo);
			bool flag = constructorDependency != null;
			if (flag)
			{
				constructorDependency.IsDecoratorTarget = true;
			}
			bool flag2 = constructionInfo.FactoryDelegate != null;
			if (flag2)
			{
				this.EmitNewDecoratorUsingFactoryDelegate(constructionInfo.FactoryDelegate, emitter, pushInstance);
			}
			else
			{
				this.EmitNewInstanceUsingImplementingType(emitter, constructionInfo, pushInstance);
			}
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00010004 File Offset: 0x0000E204
		private void EmitNewDecoratorUsingFactoryDelegate(Delegate factoryDelegate, IEmitter emitter, Action<IEmitter> pushInstance)
		{
			int factoryDelegateIndex = this.constants.Add(factoryDelegate);
			Type funcType = factoryDelegate.GetType();
			emitter.PushConstant(factoryDelegateIndex, funcType);
			int serviceFactoryIndex = this.constants.Add(this);
			emitter.PushConstant(serviceFactoryIndex, typeof(IServiceFactory));
			int scopeManagerIndex = this.CreateScopeManagerIndex();
			emitter.PushConstant(scopeManagerIndex, typeof(IScopeManager));
			emitter.PushArgument(1);
			emitter.Emit(OpCodes.Call, ServiceFactoryLoader.LoadServiceFactoryMethod);
			pushInstance(emitter);
			MethodInfo invokeMethod = funcType.GetTypeInfo().GetDeclaredMethod("Invoke");
			emitter.Emit(OpCodes.Callvirt, invokeMethod);
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x000100A8 File Offset: 0x0000E2A8
		private void EmitNewInstance(ServiceRegistration serviceRegistration, IEmitter emitter)
		{
			bool flag = serviceRegistration.Value != null;
			if (flag)
			{
				int index = this.constants.Add(serviceRegistration.Value);
				Type serviceType = serviceRegistration.ServiceType;
				emitter.PushConstant(index, serviceType);
			}
			else
			{
				ConstructionInfo constructionInfo = this.GetConstructionInfo(serviceRegistration);
				bool flag2 = serviceRegistration.FactoryExpression != null;
				if (flag2)
				{
					this.EmitNewInstanceUsingFactoryDelegate(serviceRegistration, emitter);
				}
				else
				{
					this.EmitNewInstanceUsingImplementingType(emitter, constructionInfo, null);
				}
			}
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x0001011C File Offset: 0x0000E31C
		private void EmitDecorators(ServiceRegistration serviceRegistration, IEnumerable<DecoratorRegistration> serviceDecorators, IEmitter emitter, Action<IEmitter> decoratorTargetEmitMethod)
		{
			foreach (DecoratorRegistration decorator in serviceDecorators)
			{
				bool flag = !decorator.CanDecorate(serviceRegistration);
				if (!flag)
				{
					Action<IEmitter> currentDecoratorTargetEmitter = decoratorTargetEmitMethod;
					DecoratorRegistration currentDecorator = decorator;
					decoratorTargetEmitMethod = delegate(IEmitter e)
					{
						this.EmitNewDecoratorInstance(currentDecorator, e, currentDecoratorTargetEmitter);
					};
				}
			}
			decoratorTargetEmitMethod(emitter);
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x000101AC File Offset: 0x0000E3AC
		private void EmitNewInstanceUsingImplementingType(IEmitter emitter, ConstructionInfo constructionInfo, Action<IEmitter> decoratorTargetEmitMethod)
		{
			this.EmitConstructorDependencies(constructionInfo, emitter, decoratorTargetEmitMethod);
			emitter.Emit(OpCodes.Newobj, constructionInfo.Constructor);
			this.EmitPropertyDependencies(constructionInfo, emitter);
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x000101D4 File Offset: 0x0000E3D4
		private void EmitNewInstanceUsingFactoryDelegate(ServiceRegistration serviceRegistration, IEmitter emitter)
		{
			int factoryDelegateIndex = this.constants.Add(serviceRegistration.FactoryExpression);
			Type funcType = serviceRegistration.FactoryExpression.GetType();
			MethodInfo invokeMethod = funcType.GetTypeInfo().GetDeclaredMethod("Invoke");
			emitter.PushConstant(factoryDelegateIndex, funcType);
			ParameterInfo[] parameters = invokeMethod.GetParameters();
			bool flag = parameters.Length == 1 && parameters[0].ParameterType == typeof(ServiceRequest);
			if (flag)
			{
				MethodInfo createServiceRequestMethod = ServiceRequestHelper.CreateServiceRequestMethod.MakeGenericMethod(new Type[]
				{
					serviceRegistration.ServiceType
				});
				emitter.Emit(OpCodes.Ldstr, serviceRegistration.ServiceName);
				int serviceFactoryIndex = this.constants.Add(this);
				emitter.PushConstant(serviceFactoryIndex, typeof(IServiceFactory));
				int scopeManagerIndex = this.CreateScopeManagerIndex();
				emitter.PushConstant(scopeManagerIndex, typeof(IScopeManager));
				emitter.PushArgument(1);
				emitter.Emit(OpCodes.Call, ServiceFactoryLoader.LoadServiceFactoryMethod);
				emitter.Emit(OpCodes.Call, createServiceRequestMethod);
				emitter.Call(invokeMethod);
				emitter.UnboxOrCast(serviceRegistration.ServiceType);
			}
			else
			{
				int serviceFactoryIndex2 = this.constants.Add(this);
				emitter.PushConstant(serviceFactoryIndex2, typeof(IServiceFactory));
				int scopeManagerIndex2 = this.CreateScopeManagerIndex();
				emitter.PushConstant(scopeManagerIndex2, typeof(IScopeManager));
				emitter.PushArgument(1);
				emitter.Emit(OpCodes.Call, ServiceFactoryLoader.LoadServiceFactoryMethod);
				bool flag2 = parameters.Length > 1;
				if (flag2)
				{
					emitter.PushArguments(parameters.Skip(1).ToArray<ParameterInfo>());
				}
				emitter.Call(invokeMethod);
			}
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x00010378 File Offset: 0x0000E578
		private void EmitConstructorDependencies(ConstructionInfo constructionInfo, IEmitter emitter, Action<IEmitter> decoratorTargetEmitter)
		{
			foreach (ConstructorDependency dependency in constructionInfo.ConstructorDependencies)
			{
				bool flag = !dependency.IsDecoratorTarget;
				if (flag)
				{
					this.EmitConstructorDependency(emitter, dependency);
				}
				else
				{
					bool flag2 = dependency.ServiceType.IsLazy();
					if (flag2)
					{
						LocalBuilder scopeVariable = emitter.DeclareLocal(typeof(Scope));
						emitter.PushArgument(1);
						int scopeManagerIndex = this.CreateScopeManagerIndex();
						emitter.PushConstant(scopeManagerIndex, typeof(IScopeManager));
						emitter.Emit(OpCodes.Call, ScopeLoader.GetThisOrCurrentScopeMethod);
						emitter.Store(scopeVariable);
						int instanceDelegateIndex = this.CreateInstanceDelegateIndex(decoratorTargetEmitter);
						emitter.PushConstant(instanceDelegateIndex, typeof(GetInstanceDelegate));
						emitter.PushArgument(0);
						emitter.Push(scopeVariable);
						MethodInfo createScopedLazyFromDelegateMethod = LazyHelper.CreateScopedLazyFromDelegateMethod.MakeGenericMethod(new Type[]
						{
							dependency.ServiceType.GetTypeInfo().GenericTypeArguments.Last<Type>()
						});
						emitter.Emit(OpCodes.Call, createScopedLazyFromDelegateMethod);
					}
					else
					{
						decoratorTargetEmitter(emitter);
					}
				}
			}
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x000104D0 File Offset: 0x0000E6D0
		private void EmitConstructorDependency(IEmitter emitter, Dependency dependency)
		{
			Action<IEmitter> emitMethod = this.GetEmitMethodForDependency(dependency);
			try
			{
				emitMethod(emitter);
				emitter.UnboxOrCast(dependency.ServiceType);
			}
			catch (InvalidOperationException ex)
			{
				throw new InvalidOperationException(string.Format("Unresolved dependency {0}", dependency), ex);
			}
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x00010524 File Offset: 0x0000E724
		private void EmitPropertyDependency(IEmitter emitter, PropertyDependency propertyDependency, LocalBuilder instanceVariable)
		{
			Action<IEmitter> propertyDependencyEmitMethod = this.GetEmitMethodForDependency(propertyDependency);
			bool flag = propertyDependencyEmitMethod == null;
			if (!flag)
			{
				emitter.Push(instanceVariable);
				propertyDependencyEmitMethod(emitter);
				emitter.UnboxOrCast(propertyDependency.ServiceType);
				emitter.Call(propertyDependency.Property.SetMethod);
			}
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x00010574 File Offset: 0x0000E774
		private Action<IEmitter> GetEmitMethodForDependency(Dependency dependency)
		{
			bool flag = dependency.FactoryExpression != null;
			Action<IEmitter> result;
			if (flag)
			{
				result = delegate(IEmitter skeleton)
				{
					this.EmitDependencyUsingFactoryExpression(skeleton, dependency);
				};
			}
			else
			{
				Action<IEmitter> emitter = null;
				string dependencyName = string.IsNullOrWhiteSpace(dependency.ServiceName) ? dependency.Name : dependency.ServiceName;
				ThreadSafeDictionary<string, Action<IEmitter>> registrations = this.GetEmitMethods(dependency.ServiceType);
				bool flag2 = registrations.Count > 1;
				if (flag2)
				{
					bool flag3 = registrations.TryGetValue(dependencyName, out emitter);
					if (flag3)
					{
						return emitter;
					}
				}
				emitter = this.GetEmitMethod(dependency.ServiceType, dependency.ServiceName);
				bool flag4 = emitter == null;
				if (flag4)
				{
					emitter = this.GetEmitMethod(dependency.ServiceType, dependency.Name);
					bool flag5 = emitter == null && dependency.IsRequired;
					if (flag5)
					{
						ConstructorDependency constructorDependency = dependency as ConstructorDependency;
						bool flag6 = constructorDependency != null && constructorDependency.Parameter.HasDefaultValue && this.options.EnableOptionalArguments;
						if (!flag6)
						{
							throw new InvalidOperationException(string.Format("Unresolved dependency {0}", dependency));
						}
						emitter = this.GetEmitMethodForDefaultValue(constructorDependency);
					}
				}
				result = emitter;
			}
			return result;
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x000106E8 File Offset: 0x0000E8E8
		private Action<IEmitter> GetEmitMethodForDefaultValue(ConstructorDependency constructorDependency)
		{
			Type parameterType = constructorDependency.Parameter.ParameterType;
			return delegate(IEmitter emitter)
			{
				object defaultValue = constructorDependency.Parameter.DefaultValue;
				bool flag = defaultValue == null;
				if (flag)
				{
					defaultValue = TypeHelper.GetDefaultValue(parameterType);
				}
				emitter.PushConstantValue(defaultValue, parameterType);
			};
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x0001072C File Offset: 0x0000E92C
		private void EmitDependencyUsingFactoryExpression(IEmitter emitter, Dependency dependency)
		{
			List<Action<IEmitter>> actions = new List<Action<IEmitter>>();
			ParameterInfo[] parameters = dependency.FactoryExpression.GetMethodInfo().GetParameters();
			Action<IEmitter> <>9__0;
			Action<IEmitter> <>9__1;
			foreach (ParameterInfo parameter in parameters)
			{
				bool flag = parameter.ParameterType == typeof(IServiceFactory);
				if (flag)
				{
					int serviceFactoryIndex = this.constants.Add(this);
					int scopeManagerIndex = this.CreateScopeManagerIndex();
					actions.Add(delegate(IEmitter e)
					{
						e.PushConstant(serviceFactoryIndex, typeof(IServiceFactory));
					});
					actions.Add(delegate(IEmitter e)
					{
						e.PushConstant(scopeManagerIndex, typeof(IScopeManager));
					});
					actions.Add(delegate(IEmitter e)
					{
						e.PushArgument(1);
					});
					actions.Add(delegate(IEmitter e)
					{
						e.Emit(OpCodes.Call, ServiceFactoryLoader.LoadServiceFactoryMethod);
					});
				}
				bool flag2 = parameter.ParameterType == typeof(ParameterInfo);
				if (flag2)
				{
					List<Action<IEmitter>> list = actions;
					Action<IEmitter> item;
					if ((item = <>9__0) == null)
					{
						item = (<>9__0 = delegate(IEmitter e)
						{
							e.PushConstant(this.constants.Add(((ConstructorDependency)dependency).Parameter), typeof(ParameterInfo));
						});
					}
					list.Add(item);
				}
				bool flag3 = parameter.ParameterType == typeof(PropertyInfo);
				if (flag3)
				{
					List<Action<IEmitter>> list2 = actions;
					Action<IEmitter> item2;
					if ((item2 = <>9__1) == null)
					{
						item2 = (<>9__1 = delegate(IEmitter e)
						{
							e.PushConstant(this.constants.Add(((PropertyDependency)dependency).Property), typeof(PropertyInfo));
						});
					}
					list2.Add(item2);
				}
				bool flag4 = parameter.ParameterType == typeof(object[]);
				if (flag4)
				{
					actions.Add(delegate(IEmitter e)
					{
						ServiceContainer.PushRuntimeArguments(e);
					});
				}
			}
			int factoryDelegateIndex = this.constants.Add(dependency.FactoryExpression);
			Type funcType = dependency.FactoryExpression.GetType();
			MethodInfo invokeMethod = funcType.GetTypeInfo().GetDeclaredMethod("Invoke");
			emitter.PushConstant(factoryDelegateIndex, funcType);
			foreach (Action<IEmitter> action in actions)
			{
				action(emitter);
			}
			emitter.Call(invokeMethod);
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x000109B4 File Offset: 0x0000EBB4
		private void EmitPropertyDependencies(ConstructionInfo constructionInfo, IEmitter emitter)
		{
			bool flag = constructionInfo.PropertyDependencies.Count == 0;
			if (!flag)
			{
				LocalBuilder instanceVariable = emitter.DeclareLocal(constructionInfo.ImplementingType);
				emitter.Store(instanceVariable);
				foreach (PropertyDependency propertyDependency in constructionInfo.PropertyDependencies)
				{
					this.EmitPropertyDependency(emitter, propertyDependency, instanceVariable);
				}
				emitter.Push(instanceVariable);
			}
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x00010A44 File Offset: 0x0000EC44
		private Action<IEmitter> CreateEmitMethodForUnknownService(Type serviceType, string serviceName)
		{
			Action<IEmitter> emitter = null;
			bool flag = this.CanRedirectRequestForDefaultServiceToSingleNamedService(serviceType, serviceName);
			if (flag)
			{
				emitter = this.CreateServiceEmitterBasedOnSingleNamedInstance(serviceType);
			}
			else
			{
				bool flag2 = serviceType.IsLazy();
				if (flag2)
				{
					emitter = this.CreateEmitMethodBasedOnLazyServiceRequest(serviceType);
				}
				else
				{
					bool flag3 = serviceType.IsFuncWithParameters();
					if (flag3)
					{
						emitter = this.CreateEmitMethodBasedParameterizedFuncRequest(serviceType, serviceName);
					}
					else
					{
						bool flag4 = serviceType.IsFuncRepresentingService() || serviceType.IsFuncRepresentingNamedService();
						if (flag4)
						{
							emitter = this.CreateEmitMethodBasedOnFuncServiceRequest(serviceType);
						}
						else
						{
							bool flag5 = serviceType.IsEnumerableOfT();
							if (flag5)
							{
								emitter = this.CreateEmitMethodBasedOnClosedGenericServiceRequest(serviceType, serviceName);
								bool flag6 = emitter == null;
								if (flag6)
								{
									emitter = this.CreateEmitMethodForEnumerableServiceServiceRequest(serviceType);
								}
							}
							else
							{
								bool isArray = serviceType.IsArray;
								if (isArray)
								{
									emitter = this.CreateEmitMethodForArrayServiceRequest(serviceType);
								}
								else
								{
									bool flag7 = serviceType.IsReadOnlyCollectionOfT() || serviceType.IsReadOnlyListOfT();
									if (flag7)
									{
										emitter = this.CreateEmitMethodBasedOnClosedGenericServiceRequest(serviceType, serviceName);
										bool flag8 = emitter == null;
										if (flag8)
										{
											emitter = this.CreateEmitMethodForReadOnlyCollectionServiceRequest(serviceType);
										}
									}
									else
									{
										bool flag9 = serviceType.IsListOfT();
										if (flag9)
										{
											emitter = this.CreateEmitMethodBasedOnClosedGenericServiceRequest(serviceType, serviceName);
											bool flag10 = emitter == null;
											if (flag10)
											{
												emitter = this.CreateEmitMethodForListServiceRequest(serviceType);
											}
										}
										else
										{
											bool flag11 = serviceType.IsCollectionOfT();
											if (flag11)
											{
												emitter = this.CreateEmitMethodBasedOnClosedGenericServiceRequest(serviceType, serviceName);
												bool flag12 = emitter == null;
												if (flag12)
												{
													emitter = this.CreateEmitMethodForListServiceRequest(serviceType);
												}
											}
											else
											{
												bool flag13 = serviceType.IsClosedGeneric();
												if (flag13)
												{
													emitter = this.CreateEmitMethodBasedOnClosedGenericServiceRequest(serviceType, serviceName);
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			return emitter;
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x00010BC4 File Offset: 0x0000EDC4
		private Action<IEmitter> CreateEmitMethodBasedOnFuncServiceRequest(Type serviceType)
		{
			Type returnType = serviceType.GetTypeInfo().GenericTypeArguments.Last<Type>();
			bool flag = serviceType.IsFuncRepresentingService();
			Action<IEmitter> result;
			if (flag)
			{
				MethodInfo createScopedGenericFuncMethod = FuncHelper.CreateScopedGenericFuncMethod.MakeGenericMethod(new Type[]
				{
					returnType
				});
				result = delegate(IEmitter e)
				{
					e.PushConstant(this.constants.Add(this), typeof(ServiceContainer));
					int scopeManagerIndex = this.CreateScopeManagerIndex();
					e.PushArgument(1);
					e.PushConstant(scopeManagerIndex, typeof(IScopeManager));
					e.Emit(OpCodes.Call, ScopeLoader.GetThisOrCurrentScopeMethod);
					e.Emit(OpCodes.Call, createScopedGenericFuncMethod);
				};
			}
			else
			{
				MethodInfo createScopedGenericNamedFuncMethod = FuncHelper.CreateScopedGenericNamedFuncMethod.MakeGenericMethod(new Type[]
				{
					returnType
				});
				result = delegate(IEmitter e)
				{
					e.PushConstant(this.constants.Add(this), typeof(ServiceContainer));
					int scopeManagerIndex = this.CreateScopeManagerIndex();
					e.PushArgument(1);
					e.PushConstant(scopeManagerIndex, typeof(IScopeManager));
					e.Emit(OpCodes.Call, ScopeLoader.GetThisOrCurrentScopeMethod);
					e.Emit(OpCodes.Call, createScopedGenericNamedFuncMethod);
				};
			}
			return result;
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x00010C60 File Offset: 0x0000EE60
		private Action<IEmitter> CreateEmitMethodBasedParameterizedFuncRequest(Type serviceType, string serviceName)
		{
			bool flag = string.IsNullOrEmpty(serviceName);
			Delegate getInstanceDelegate;
			if (flag)
			{
				getInstanceDelegate = this.CreateGetInstanceWithParametersDelegate(serviceType);
			}
			else
			{
				getInstanceDelegate = ReflectionHelper.CreateGetNamedInstanceWithParametersDelegate(this, serviceType, serviceName);
			}
			int constantIndex = this.constants.Add(getInstanceDelegate);
			return delegate(IEmitter e)
			{
				e.PushConstant(constantIndex, serviceType);
			};
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x00010CC8 File Offset: 0x0000EEC8
		private Delegate CreateGetInstanceWithParametersDelegate(Type serviceType)
		{
			MethodInfo getInstanceMethod = ReflectionHelper.GetGetInstanceWithParametersMethod(serviceType);
			return getInstanceMethod.CreateDelegate(serviceType, this);
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x00010CEC File Offset: 0x0000EEEC
		private Action<IEmitter> CreateServiceEmitterBasedOnFactoryRule(ServiceContainer.FactoryRule rule, Type serviceType, string serviceName)
		{
			ServiceRegistration serviceRegistration = new ServiceRegistration
			{
				ServiceType = serviceType,
				ServiceName = serviceName,
				FactoryExpression = rule.Factory,
				Lifetime = (ServiceContainer.CloneLifeTime(rule.LifeTime) ?? this.DefaultLifetime)
			};
			bool flag = rule.LifeTime != null;
			Action<IEmitter> result;
			if (flag)
			{
				Action<IEmitter> <>9__2;
				result = delegate(IEmitter emitter)
				{
					ServiceContainer <>4__this = this;
					ServiceRegistration serviceRegistration = serviceRegistration;
					Action<IEmitter> emitMethod;
					if ((emitMethod = <>9__2) == null)
					{
						emitMethod = (<>9__2 = delegate(IEmitter e)
						{
							this.EmitNewInstanceWithDecorators(serviceRegistration, e);
						});
					}
					<>4__this.EmitLifetime(serviceRegistration, emitMethod, emitter);
				};
			}
			else
			{
				result = delegate(IEmitter emitter)
				{
					this.EmitNewInstanceWithDecorators(serviceRegistration, emitter);
				};
			}
			return result;
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x00010D78 File Offset: 0x0000EF78
		private Action<IEmitter> CreateEmitMethodForArrayServiceRequest(Type serviceType)
		{
			return this.CreateEmitMethodForEnumerableServiceServiceRequest(serviceType);
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x00010D94 File Offset: 0x0000EF94
		private Action<IEmitter> CreateEmitMethodForListServiceRequest(Type serviceType)
		{
			Action<IEmitter> enumerableEmitter = this.CreateEmitMethodForEnumerableServiceServiceRequest(serviceType);
			MethodInfo openGenericToArrayMethod = typeof(Enumerable).GetTypeInfo().GetDeclaredMethod("ToList");
			MethodInfo closedGenericToListMethod = openGenericToArrayMethod.MakeGenericMethod(new Type[]
			{
				TypeHelper.GetElementType(serviceType)
			});
			return delegate(IEmitter ms)
			{
				enumerableEmitter(ms);
				ms.Emit(OpCodes.Call, closedGenericToListMethod);
			};
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x00010DFC File Offset: 0x0000EFFC
		private Action<IEmitter> CreateEmitMethodForReadOnlyCollectionServiceRequest(Type serviceType)
		{
			Type elementType = TypeHelper.GetElementType(serviceType);
			Type closedGenericReadOnlyCollectionType = typeof(ReadOnlyCollection<>).MakeGenericType(new Type[]
			{
				elementType
			});
			ConstructorInfo constructorInfo = closedGenericReadOnlyCollectionType.GetTypeInfo().DeclaredConstructors.Single<ConstructorInfo>();
			Action<IEmitter> listEmitMethod = this.CreateEmitMethodForListServiceRequest(serviceType);
			return delegate(IEmitter emitter)
			{
				listEmitMethod(emitter);
				emitter.New(constructorInfo);
			};
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x00010E64 File Offset: 0x0000F064
		private Action<IEmitter> CreateEmitMethodBasedOnLazyServiceRequest(Type serviceType)
		{
			Type returnType = serviceType.GetTypeInfo().GenericTypeArguments.Last<Type>();
			MethodInfo createScopedLazyMethod = LazyHelper.CreateScopedLazyMethod.MakeGenericMethod(new Type[]
			{
				returnType
			});
			return delegate(IEmitter e)
			{
				e.PushConstant(this.constants.Add(this), typeof(ServiceContainer));
				int scopeManagerIndex = this.CreateScopeManagerIndex();
				e.PushArgument(1);
				e.PushConstant(scopeManagerIndex, typeof(IScopeManager));
				e.Emit(OpCodes.Call, ScopeLoader.GetThisOrCurrentScopeMethod);
				e.Emit(OpCodes.Call, createScopedLazyMethod);
			};
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x00010EBC File Offset: 0x0000F0BC
		private ThreadSafeDictionary<string, ServiceRegistration> GetOpenGenericServiceRegistrations(Type openGenericServiceType)
		{
			return this.GetAvailableServices(openGenericServiceType);
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x00010ED8 File Offset: 0x0000F0D8
		private Action<IEmitter> CreateEmitMethodBasedOnClosedGenericServiceRequest(Type closedGenericServiceType, string serviceName)
		{
			ServiceContainer.<>c__DisplayClass196_0 CS$<>8__locals1;
			CS$<>8__locals1.closedGenericServiceType = closedGenericServiceType;
			CS$<>8__locals1.serviceName = serviceName;
			CS$<>8__locals1.<>4__this = this;
			Type openGenericServiceType = CS$<>8__locals1.closedGenericServiceType.GetGenericTypeDefinition();
			ThreadSafeDictionary<string, ServiceRegistration> openGenericServiceRegistrations = this.GetOpenGenericServiceRegistrations(openGenericServiceType);
			Dictionary<string, ServiceContainer.ClosedGenericCandidate> candidates = new Dictionary<string, ServiceContainer.ClosedGenericCandidate>(StringComparer.OrdinalIgnoreCase);
			foreach (ServiceRegistration openGenericServiceRegistration in openGenericServiceRegistrations.Values)
			{
				Type closedGenericImplementingTypeCandidate = this.GenericArgumentMapper.TryMakeGenericType(CS$<>8__locals1.closedGenericServiceType, openGenericServiceRegistration.ImplementingType);
				bool flag = closedGenericImplementingTypeCandidate != null;
				if (flag)
				{
					bool flag2 = CS$<>8__locals1.closedGenericServiceType.IsAssignableFrom(closedGenericImplementingTypeCandidate);
					if (flag2)
					{
						candidates.Add(openGenericServiceRegistration.ServiceName, new ServiceContainer.ClosedGenericCandidate(closedGenericImplementingTypeCandidate, openGenericServiceRegistration.Lifetime));
					}
				}
			}
			bool flag3 = string.IsNullOrWhiteSpace(CS$<>8__locals1.serviceName);
			Action<IEmitter> result;
			if (flag3)
			{
				string defaultServiceName = string.Empty;
				bool flag4 = candidates.Count > 0;
				if (flag4)
				{
					defaultServiceName = this.options.DefaultServiceSelector((from k in candidates.Keys
					orderby k
					select k).ToArray<string>());
				}
				bool flag5 = candidates.TryGetValue(defaultServiceName, out CS$<>8__locals1.candidate);
				if (flag5)
				{
					result = this.<CreateEmitMethodBasedOnClosedGenericServiceRequest>g__RegisterAndGetEmitMethod|196_0(ref CS$<>8__locals1);
				}
				else
				{
					bool flag6 = candidates.Count == 1;
					if (flag6)
					{
						CS$<>8__locals1.candidate = candidates.First<KeyValuePair<string, ServiceContainer.ClosedGenericCandidate>>().Value;
						result = this.<CreateEmitMethodBasedOnClosedGenericServiceRequest>g__RegisterAndGetEmitMethod|196_0(ref CS$<>8__locals1);
					}
					else
					{
						result = null;
					}
				}
			}
			else
			{
				bool flag7 = candidates.TryGetValue(CS$<>8__locals1.serviceName, out CS$<>8__locals1.candidate);
				if (flag7)
				{
					result = this.<CreateEmitMethodBasedOnClosedGenericServiceRequest>g__RegisterAndGetEmitMethod|196_0(ref CS$<>8__locals1);
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x000110B4 File Offset: 0x0000F2B4
		private Action<IEmitter> CreateEmitMethodForEnumerableServiceServiceRequest(Type serviceType)
		{
			Type actualServiceType = TypeHelper.GetElementType(serviceType);
			bool isGenericType = actualServiceType.GetTypeInfo().IsGenericType;
			if (isGenericType)
			{
				Type openGenericServiceType = actualServiceType.GetGenericTypeDefinition();
				ThreadSafeDictionary<string, ServiceRegistration> openGenericServiceRegistrations = this.GetOpenGenericServiceRegistrations(openGenericServiceType);
				var constructableOpenGenericServices = from r in openGenericServiceRegistrations.Values
				select new
				{
					Lifetime = r.Lifetime,
					ServiceName = r.ServiceName,
					closedGenericImplementingType = this.GenericArgumentMapper.TryMakeGenericType(actualServiceType, r.ImplementingType)
				} into t
				where t.closedGenericImplementingType != null
				select t;
				foreach (var constructableOpenGenericService in constructableOpenGenericServices)
				{
					ServiceRegistration serviceRegistration = new ServiceRegistration
					{
						ServiceType = actualServiceType,
						ImplementingType = constructableOpenGenericService.closedGenericImplementingType,
						ServiceName = constructableOpenGenericService.ServiceName,
						Lifetime = (ServiceContainer.CloneLifeTime(constructableOpenGenericService.Lifetime) ?? this.DefaultLifetime)
					};
					this.Register(serviceRegistration);
				}
			}
			bool flag = this.options.EnableVariance && this.options.VarianceFilter(serviceType);
			List<Action<IEmitter>> emitMethods;
			if (flag)
			{
				emitMethods = (from kv in (from kv in this.emitters
				where actualServiceType.GetTypeInfo().IsAssignableFrom(kv.Key.GetTypeInfo())
				select kv).SelectMany((KeyValuePair<Type, ThreadSafeDictionary<string, Action<IEmitter>>> kv) => kv.Value)
				orderby kv.Key
				select kv.Value).ToList<Action<IEmitter>>();
			}
			else
			{
				emitMethods = (from kv in this.GetEmitMethods(actualServiceType)
				orderby kv.Key
				select kv.Value).ToList<Action<IEmitter>>();
			}
			bool flag2 = this.dependencyStack.Count > 0 && emitMethods.Contains(this.dependencyStack.Peek());
			if (flag2)
			{
				emitMethods.Remove(this.dependencyStack.Peek());
			}
			return delegate(IEmitter e)
			{
				ServiceContainer.EmitEnumerable(emitMethods, actualServiceType, e);
			};
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x00011350 File Offset: 0x0000F550
		private Action<IEmitter> CreateServiceEmitterBasedOnSingleNamedInstance(Type serviceType)
		{
			return this.GetEmitMethod(serviceType, this.GetEmitMethods(serviceType).First<KeyValuePair<string, Action<IEmitter>>>().Key);
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x00011380 File Offset: 0x0000F580
		private bool CanRedirectRequestForDefaultServiceToSingleNamedService(Type serviceType, string serviceName)
		{
			return string.IsNullOrEmpty(serviceName) && this.GetEmitMethods(serviceType).Count == 1;
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x000113AC File Offset: 0x0000F5AC
		private ConstructionInfo GetConstructionInfo(Registration registration)
		{
			return this.constructionInfoProvider.Value.GetConstructionInfo(registration);
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x000113D0 File Offset: 0x0000F5D0
		private ThreadSafeDictionary<string, Action<IEmitter>> GetEmitMethods(Type serviceType)
		{
			return this.emitters.GetOrAdd(serviceType, (Type s) => new ThreadSafeDictionary<string, Action<IEmitter>>(StringComparer.CurrentCultureIgnoreCase));
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x00011410 File Offset: 0x0000F610
		private ThreadSafeDictionary<string, ServiceRegistration> GetAvailableServices(Type serviceType)
		{
			return this.availableServices.GetOrAdd(serviceType, (Type s) => new ThreadSafeDictionary<string, ServiceRegistration>(StringComparer.CurrentCultureIgnoreCase));
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x00011450 File Offset: 0x0000F650
		private ThreadSafeDictionary<string, Delegate> GetConstructorDependencyFactories(Type dependencyType)
		{
			return this.constructorDependencyFactories.GetOrAdd(dependencyType, (Type d) => new ThreadSafeDictionary<string, Delegate>(StringComparer.CurrentCultureIgnoreCase));
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x00011490 File Offset: 0x0000F690
		private ThreadSafeDictionary<string, Delegate> GetPropertyDependencyFactories(Type dependencyType)
		{
			return this.propertyDependencyFactories.GetOrAdd(dependencyType, (Type d) => new ThreadSafeDictionary<string, Delegate>(StringComparer.CurrentCultureIgnoreCase));
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x000114D0 File Offset: 0x0000F6D0
		private void RegisterService(Type serviceType, Type implementingType, ILifetime lifetime, string serviceName)
		{
			Ensure.IsNotNull<Type>(serviceType, "type");
			Ensure.IsNotNull<Type>(implementingType, "implementingType");
			Ensure.IsNotNull<string>(serviceName, "serviceName");
			this.EnsureConstructable(serviceType, implementingType);
			ServiceRegistration serviceRegistration = new ServiceRegistration
			{
				ServiceType = serviceType,
				ImplementingType = implementingType,
				ServiceName = serviceName,
				Lifetime = (lifetime ?? this.DefaultLifetime)
			};
			this.Register(serviceRegistration);
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x00011548 File Offset: 0x0000F748
		private void EnsureConstructable(Type serviceType, Type implementingType)
		{
			bool containsGenericParameters = implementingType.GetTypeInfo().ContainsGenericParameters;
			if (containsGenericParameters)
			{
				try
				{
					this.GenericArgumentMapper.Map(serviceType, implementingType).GetMappedArguments();
				}
				catch (InvalidOperationException ex)
				{
					throw new ArgumentOutOfRangeException("implementingType", ex.Message);
				}
			}
			else
			{
				bool flag = !serviceType.GetTypeInfo().IsAssignableFrom(implementingType.GetTypeInfo());
				if (flag)
				{
					throw new ArgumentOutOfRangeException("implementingType", string.Concat(new string[]
					{
						"The implementing type ",
						implementingType.FullName,
						" is not assignable from ",
						serviceType.FullName,
						"."
					}));
				}
			}
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x000115FC File Offset: 0x0000F7FC
		private Action<IEmitter> ResolveEmitMethod(ServiceRegistration serviceRegistration)
		{
			Func<ServiceContainer.ServiceOverride, bool> <>9__1;
			Action<IEmitter> <>9__2;
			Action<IEmitter> <>9__3;
			return delegate(IEmitter emitter)
			{
				IEnumerable<ServiceContainer.ServiceOverride> items = this.overrides.Items;
				Func<ServiceContainer.ServiceOverride, bool> predicate;
				if ((predicate = <>9__1) == null)
				{
					predicate = (<>9__1 = ((ServiceContainer.ServiceOverride so) => so.CanOverride(serviceRegistration)));
				}
				ServiceContainer.ServiceOverride[] serviceOverrides = items.Where(predicate).ToArray<ServiceContainer.ServiceOverride>();
				foreach (ServiceContainer.ServiceOverride serviceOverride in serviceOverrides)
				{
					serviceRegistration = serviceOverride.Execute(this, serviceRegistration);
				}
				bool flag = serviceRegistration.Lifetime == null;
				if (flag)
				{
					bool optimizeForLargeObjectGraphs = this.options.OptimizeForLargeObjectGraphs;
					if (optimizeForLargeObjectGraphs)
					{
						ServiceContainer <>4__this = this;
						ServiceRegistration serviceRegistration2 = serviceRegistration;
						Action<IEmitter> emitMethod;
						if ((emitMethod = <>9__2) == null)
						{
							emitMethod = (<>9__2 = delegate(IEmitter e)
							{
								this.EmitNewInstanceWithDecorators(serviceRegistration, e);
							});
						}
						<>4__this.EmitLifetime(serviceRegistration2, emitMethod, emitter);
					}
					else
					{
						this.EmitNewInstanceWithDecorators(serviceRegistration, emitter);
					}
				}
				else
				{
					ServiceContainer <>4__this2 = this;
					ServiceRegistration serviceRegistration3 = serviceRegistration;
					Action<IEmitter> emitMethod2;
					if ((emitMethod2 = <>9__3) == null)
					{
						emitMethod2 = (<>9__3 = delegate(IEmitter e)
						{
							this.EmitNewInstanceWithDecorators(serviceRegistration, e);
						});
					}
					<>4__this2.EmitLifetime(serviceRegistration3, emitMethod2, emitter);
				}
			};
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x00011630 File Offset: 0x0000F830
		private void EmitNewInstanceWithDecorators(ServiceRegistration serviceRegistration, IEmitter emitter)
		{
			DecoratorRegistration[] serviceDecorators = this.GetDecorators(serviceRegistration);
			bool flag = serviceDecorators.Length != 0;
			if (flag)
			{
				this.EmitDecorators(serviceRegistration, serviceDecorators, emitter, delegate(IEmitter dm)
				{
					this.EmitNewInstance(serviceRegistration, dm);
				});
			}
			else
			{
				this.EmitNewInstance(serviceRegistration, emitter);
			}
			bool flag2 = serviceRegistration.Lifetime is PerContainerLifetime && ServiceContainer.<EmitNewInstanceWithDecorators>g__IsNotServiceFactory|208_2(serviceRegistration.ServiceType);
			if (flag2)
			{
				MethodInfo closedGenericTrackInstanceMethod = ServiceContainer.OpenGenericTrackInstanceMethod.MakeGenericMethod(new Type[]
				{
					emitter.StackType
				});
				int containerIndex = this.constants.Add(this);
				emitter.PushConstant(containerIndex, typeof(ServiceContainer));
				emitter.Emit(OpCodes.Call, closedGenericTrackInstanceMethod);
			}
			ServiceContainer.Initializer[] processors = (from i in this.initializers.Items
			where i.Predicate(serviceRegistration)
			select i).ToArray<ServiceContainer.Initializer>();
			bool flag3 = processors.Length == 0;
			if (!flag3)
			{
				LocalBuilder instanceVariable = emitter.DeclareLocal(serviceRegistration.ServiceType);
				emitter.Store(instanceVariable);
				foreach (ServiceContainer.Initializer postProcessor in processors)
				{
					Type delegateType = postProcessor.Initialize.GetType();
					int delegateIndex = this.constants.Add(postProcessor.Initialize);
					emitter.PushConstant(delegateIndex, delegateType);
					int serviceFactoryIndex = this.constants.Add(this);
					emitter.PushConstant(serviceFactoryIndex, typeof(IServiceFactory));
					int scopeManagerIndex = this.CreateScopeManagerIndex();
					emitter.PushConstant(scopeManagerIndex, typeof(IScopeManager));
					emitter.PushArgument(1);
					emitter.Emit(OpCodes.Call, ServiceFactoryLoader.LoadServiceFactoryMethod);
					emitter.Push(instanceVariable);
					MethodInfo invokeMethod = delegateType.GetTypeInfo().GetDeclaredMethod("Invoke");
					emitter.Call(invokeMethod);
				}
				emitter.Push(instanceVariable);
			}
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x00011834 File Offset: 0x0000FA34
		private int GetInstanceDelegateIndex(ServiceRegistration serviceRegistration, Action<IEmitter> emitMethod)
		{
			bool flag = this.servicesToDelegatesIndex.ContainsUninitializedValue(serviceRegistration);
			if (flag)
			{
				throw new InvalidOperationException(string.Format("Recursive dependency detected: ServiceType:{0}, ServiceName:{1}]", serviceRegistration.ServiceType, serviceRegistration.ServiceName));
			}
			return this.servicesToDelegatesIndex.GetOrAdd(serviceRegistration, (ServiceRegistration _) => this.CreateInstanceDelegateIndex(emitMethod));
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x000118A0 File Offset: 0x0000FAA0
		private void EmitLifetime(ServiceRegistration serviceRegistration, Action<IEmitter> emitMethod, IEmitter emitter)
		{
			bool flag = serviceRegistration.Lifetime == null;
			if (flag)
			{
				int instanceDelegateIndex = this.GetInstanceDelegateIndex(serviceRegistration, emitMethod);
				MethodInfo invokeMethod = typeof(GetInstanceDelegate).GetTypeInfo().GetDeclaredMethod("Invoke");
				emitter.PushConstant(instanceDelegateIndex, typeof(GetInstanceDelegate));
				emitter.PushArgument(0);
				this.PushScope(emitter);
				emitter.Emit(OpCodes.Callvirt, invokeMethod);
			}
			else
			{
				bool flag2 = serviceRegistration.Lifetime is PerScopeLifetime;
				if (flag2)
				{
					int instanceDelegateIndex2 = this.servicesToDelegatesIndex.GetOrAdd(serviceRegistration, (ServiceRegistration _) => this.CreateInstanceDelegateIndex(emitMethod));
					this.PushScope(emitter);
					emitter.Emit(OpCodes.Call, ScopeLoader.ValidateScopeMethod.MakeGenericMethod(new Type[]
					{
						serviceRegistration.ServiceType.UnderlyingSystemType
					}));
					emitter.PushConstant(instanceDelegateIndex2, typeof(GetInstanceDelegate));
					emitter.PushArgument(0);
					emitter.Push(instanceDelegateIndex2);
					emitter.Call(ScopeLoader.GetScopedInstanceMethod);
				}
				else
				{
					bool flag3 = serviceRegistration.Lifetime is PerRequestLifeTime;
					if (flag3)
					{
						LocalBuilder scopeVariable = emitter.DeclareLocal(typeof(Scope));
						int instanceDelegateIndex3 = this.servicesToDelegatesIndex.GetOrAdd(serviceRegistration, (ServiceRegistration _) => this.CreateInstanceDelegateIndex(emitMethod));
						MethodInfo invokeMethod2 = typeof(GetInstanceDelegate).GetTypeInfo().GetDeclaredMethod("Invoke");
						emitter.PushConstant(instanceDelegateIndex3, typeof(GetInstanceDelegate));
						emitter.PushArgument(0);
						this.PushScope(emitter);
						emitter.Store(scopeVariable);
						emitter.Push(scopeVariable);
						emitter.Emit(OpCodes.Callvirt, invokeMethod2);
						emitter.Push(scopeVariable);
						emitter.Emit(OpCodes.Call, ScopeLoader.ValidateTrackedTransientMethod);
					}
					else
					{
						MethodInfo nonClosingGetInstanceMethod = LifetimeHelper.GetNonClosingGetInstanceMethod(serviceRegistration.Lifetime.GetType());
						bool flag4 = nonClosingGetInstanceMethod != null;
						if (flag4)
						{
							int instanceDelegateIndex4 = this.servicesToDelegatesIndex.GetOrAdd(serviceRegistration, (ServiceRegistration _) => this.CreateInstanceDelegateIndex(emitMethod));
							int lifetimeIndex = this.CreateLifetimeIndex(serviceRegistration.Lifetime);
							emitter.PushConstant(lifetimeIndex, serviceRegistration.Lifetime.GetType());
							emitter.PushConstant(instanceDelegateIndex4, typeof(GetInstanceDelegate));
							this.PushScope(emitter);
							emitter.PushArgument(0);
							emitter.Call(nonClosingGetInstanceMethod);
						}
						else
						{
							int instanceDelegateIndex5 = this.servicesToDelegatesIndex.GetOrAdd(serviceRegistration, (ServiceRegistration _) => this.CreateInstanceDelegateIndex(emitMethod));
							int lifetimeIndex2 = this.CreateLifetimeIndex(serviceRegistration.Lifetime);
							LocalBuilder scopeVariable2 = emitter.DeclareLocal(typeof(Scope));
							this.PushScope(emitter);
							emitter.Store(scopeVariable2);
							emitter.PushConstant(lifetimeIndex2, typeof(ILifetime));
							emitter.PushConstant(instanceDelegateIndex5, typeof(GetInstanceDelegate));
							emitter.PushArgument(0);
							emitter.Push(scopeVariable2);
							emitter.Emit(OpCodes.Call, FuncHelper.CreateScopedFuncMethod);
							emitter.Push(scopeVariable2);
							emitter.Call(LifetimeHelper.GetInstanceMethod);
						}
					}
				}
				bool flag5 = ServiceContainer.<EmitLifetime>g__IsNotServiceFactory|210_0(serviceRegistration.ServiceType) && !(serviceRegistration.Lifetime is PerContainerLifetime);
				if (flag5)
				{
					this.disposableLifeTimes.Add(serviceRegistration.Lifetime);
				}
			}
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x00011C00 File Offset: 0x0000FE00
		private void PushScope(IEmitter emitter)
		{
			bool enableCurrentScope = this.options.EnableCurrentScope;
			if (enableCurrentScope)
			{
				int scopeManagerIndex = this.CreateScopeManagerIndex();
				emitter.PushArgument(1);
				emitter.PushConstant(scopeManagerIndex, typeof(IScopeManager));
				emitter.Emit(OpCodes.Call, ScopeLoader.GetThisOrCurrentScopeMethod);
			}
			else
			{
				emitter.PushArgument(1);
			}
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x00011C60 File Offset: 0x0000FE60
		private int CreateScopeManagerIndex()
		{
			return this.constants.Add(this.ScopeManagerProvider.GetScopeManager(this));
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x00011C8C File Offset: 0x0000FE8C
		private int CreateInstanceDelegateIndex(Action<IEmitter> emitMethod)
		{
			return this.constants.Add(this.CreateDynamicMethodDelegate(emitMethod));
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x00011CB0 File Offset: 0x0000FEB0
		private int CreateLifetimeIndex(ILifetime lifetime)
		{
			return this.constants.Add(lifetime);
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x00011CD0 File Offset: 0x0000FED0
		private GetInstanceDelegate CreateDefaultDelegate(Type serviceType, bool throwError)
		{
			this.log.Info(string.Format("Compiling delegate for resolving service : {0}", serviceType));
			GetInstanceDelegate instanceDelegate = this.CreateDelegate(serviceType, string.Empty, throwError);
			bool flag = instanceDelegate == null;
			GetInstanceDelegate result;
			if (flag)
			{
				result = ((object[] args, Scope scope) => null);
			}
			else
			{
				Interlocked.Exchange<ImmutableHashTable<Type, GetInstanceDelegate>>(ref this.delegates, this.delegates.Add(serviceType, instanceDelegate));
				result = instanceDelegate;
			}
			return result;
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x00011D4C File Offset: 0x0000FF4C
		private GetInstanceDelegate CreateNamedDelegate(ValueTuple<Type, string> key, bool throwError)
		{
			this.log.Info(string.Format("Compiling delegate for resolving service : {0}, name: {1}", key.Item1, key.Item2));
			GetInstanceDelegate instanceDelegate = this.CreateDelegate(key.Item1, key.Item2, throwError);
			bool flag = instanceDelegate == null;
			GetInstanceDelegate result;
			if (flag)
			{
				result = ((object[] args, Scope scope) => null);
			}
			else
			{
				Interlocked.Exchange<ImmutableHashTable<ValueTuple<Type, string>, GetInstanceDelegate>>(ref this.namedDelegates, this.namedDelegates.Add(key, instanceDelegate));
				result = instanceDelegate;
			}
			return result;
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x00011DDC File Offset: 0x0000FFDC
		private GetInstanceDelegate CreateDelegate(Type serviceType, string serviceName, bool throwError)
		{
			object obj = this.lockObject;
			GetInstanceDelegate result;
			lock (obj)
			{
				Action<IEmitter> serviceEmitter = this.GetEmitMethod(serviceType, serviceName);
				bool flag2 = serviceEmitter == null && throwError;
				if (flag2)
				{
					this.servicesToDelegatesIndex.Remove(new ServiceRegistration
					{
						ServiceType = serviceType,
						ServiceName = serviceName
					});
					throw new InvalidOperationException(string.Format("Unable to resolve type: {0}, service name: {1}", serviceType, serviceName));
				}
				bool flag3 = serviceEmitter != null;
				if (flag3)
				{
					try
					{
						return this.CreateDynamicMethodDelegate(serviceEmitter);
					}
					catch (InvalidOperationException ex)
					{
						this.dependencyStack.Clear();
						this.servicesToDelegatesIndex.Remove(new ServiceRegistration
						{
							ServiceType = serviceType,
							ServiceName = serviceName
						});
						throw new InvalidOperationException(string.Format("Unable to resolve type: {0}, service name: {1}", serviceType, serviceName), ex);
					}
				}
				result = null;
			}
			return result;
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x00011ED0 File Offset: 0x000100D0
		private void RegisterValue(Type serviceType, object value, string serviceName)
		{
			ServiceRegistration serviceRegistration = new ServiceRegistration
			{
				ServiceType = serviceType,
				ServiceName = serviceName,
				Value = value,
				Lifetime = new PerContainerLifetime()
			};
			this.Register(serviceRegistration);
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x00011F10 File Offset: 0x00010110
		private void RegisterServiceFromLambdaExpression<TService>(Delegate factory, ILifetime lifetime, string serviceName)
		{
			ServiceRegistration serviceRegistration = new ServiceRegistration
			{
				ServiceType = typeof(TService),
				FactoryExpression = factory,
				ServiceName = serviceName,
				Lifetime = (lifetime ?? this.DefaultLifetime)
			};
			this.Register(serviceRegistration);
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x00011F8C File Offset: 0x0001018C
		[CompilerGenerated]
		internal static string <Compile>g__GetPrettyName|127_0(Type type)
		{
			bool isGenericType = type.GetTypeInfo().IsGenericType;
			string result;
			if (isGenericType)
			{
				result = type.FullName.Substring(0, type.FullName.LastIndexOf("`", StringComparison.OrdinalIgnoreCase)) + "<" + string.Join(", ", type.GetTypeInfo().GenericTypeParameters.Select(new Func<Type, string>(ServiceContainer.<Compile>g__GetPrettyName|127_0))) + ">";
			}
			else
			{
				result = type.Name;
			}
			return result;
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x0001200C File Offset: 0x0001020C
		[CompilerGenerated]
		private Action<IEmitter> <CreateEmitMethodBasedOnClosedGenericServiceRequest>g__RegisterAndGetEmitMethod|196_0(ref ServiceContainer.<>c__DisplayClass196_0 A_1)
		{
			ServiceRegistration serviceRegistration = new ServiceRegistration
			{
				ServiceType = A_1.closedGenericServiceType,
				ImplementingType = A_1.candidate.ClosedGenericImplementingType,
				ServiceName = A_1.serviceName,
				Lifetime = (ServiceContainer.CloneLifeTime(A_1.candidate.Lifetime) ?? this.DefaultLifetime)
			};
			this.Register(serviceRegistration);
			return this.GetEmitMethod(serviceRegistration.ServiceType, serviceRegistration.ServiceName);
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0001208C File Offset: 0x0001028C
		[CompilerGenerated]
		internal static bool <EmitNewInstanceWithDecorators>g__IsNotServiceFactory|208_2(Type serviceType)
		{
			return !typeof(IServiceFactory).GetTypeInfo().IsAssignableFrom(serviceType.GetTypeInfo());
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x000120BC File Offset: 0x000102BC
		[CompilerGenerated]
		internal static bool <EmitLifetime>g__IsNotServiceFactory|210_0(Type serviceType)
		{
			return !typeof(IServiceFactory).GetTypeInfo().IsAssignableFrom(serviceType.GetTypeInfo());
		}

		// Token: 0x0400017C RID: 380
		private const string UnresolvedDependencyError = "Unresolved dependency {0}";

		// Token: 0x0400017D RID: 381
		private static readonly MethodInfo OpenGenericTrackInstanceMethod = typeof(ServiceContainer).GetTypeInfo().GetDeclaredMethod("TrackInstance");

		// Token: 0x0400017E RID: 382
		private readonly Action<LogEntry> log;

		// Token: 0x0400017F RID: 383
		private readonly Func<Type, Type[], IMethodSkeleton> methodSkeletonFactory;

		// Token: 0x04000180 RID: 384
		private readonly ServiceContainer.ServiceRegistry<Action<IEmitter>> emitters;

		// Token: 0x04000181 RID: 385
		private readonly ServiceContainer.ServiceRegistry<Delegate> constructorDependencyFactories;

		// Token: 0x04000182 RID: 386
		private readonly ServiceContainer.ServiceRegistry<Delegate> propertyDependencyFactories;

		// Token: 0x04000183 RID: 387
		private readonly ServiceContainer.ServiceRegistry<ServiceRegistration> availableServices;

		// Token: 0x04000184 RID: 388
		private readonly object lockObject;

		// Token: 0x04000185 RID: 389
		private readonly ContainerOptions options;

		// Token: 0x04000186 RID: 390
		private readonly ServiceContainer.Storage<object> constants;

		// Token: 0x04000187 RID: 391
		private readonly ServiceContainer.Storage<ILifetime> disposableLifeTimes;

		// Token: 0x04000188 RID: 392
		private readonly ServiceContainer.Storage<DecoratorRegistration> decorators;

		// Token: 0x04000189 RID: 393
		private readonly ServiceContainer.Storage<ServiceContainer.ServiceOverride> overrides;

		// Token: 0x0400018A RID: 394
		private readonly ServiceContainer.Storage<ServiceContainer.FactoryRule> factoryRules;

		// Token: 0x0400018B RID: 395
		private readonly ServiceContainer.Storage<ServiceContainer.Initializer> initializers;

		// Token: 0x0400018C RID: 396
		private readonly Stack<Action<IEmitter>> dependencyStack;

		// Token: 0x0400018D RID: 397
		private readonly Lazy<IConstructionInfoProvider> constructionInfoProvider;

		// Token: 0x0400018E RID: 398
		private readonly List<IDisposable> disposableObjects;

		// Token: 0x0400018F RID: 399
		private readonly LazyConcurrentDictionary<ServiceRegistration, int> servicesToDelegatesIndex;

		// Token: 0x04000190 RID: 400
		private ImmutableHashTable<Type, GetInstanceDelegate> delegates;

		// Token: 0x04000191 RID: 401
		private ImmutableHashTable<ValueTuple<Type, string>, GetInstanceDelegate> namedDelegates;

		// Token: 0x04000192 RID: 402
		private ImmutableHashTree<Type, Func<object[], Scope, object, object>> propertyInjectionDelegates;

		// Token: 0x04000193 RID: 403
		private bool isLocked;

		// Token: 0x04000194 RID: 404
		private Type defaultLifetimeType;

		// Token: 0x020001F3 RID: 499
		private struct ClosedGenericCandidate
		{
			// Token: 0x06000C5D RID: 3165 RVA: 0x0002683E File Offset: 0x00024A3E
			public ClosedGenericCandidate(Type closedGenericImplementingType, ILifetime lifetime)
			{
				this.ClosedGenericImplementingType = closedGenericImplementingType;
				this.Lifetime = lifetime;
			}

			// Token: 0x1700023B RID: 571
			// (get) Token: 0x06000C5E RID: 3166 RVA: 0x0002684F File Offset: 0x00024A4F
			public readonly Type ClosedGenericImplementingType { get; }

			// Token: 0x1700023C RID: 572
			// (get) Token: 0x06000C5F RID: 3167 RVA: 0x00026857 File Offset: 0x00024A57
			public readonly ILifetime Lifetime { get; }
		}

		// Token: 0x020001F4 RID: 500
		private class Storage<T>
		{
			// Token: 0x06000C60 RID: 3168 RVA: 0x00026860 File Offset: 0x00024A60
			public int Add(T value)
			{
				int index = Array.IndexOf<T>(this.Items, value);
				bool flag = index == -1;
				int result;
				if (flag)
				{
					result = this.TryAddValue(value);
				}
				else
				{
					result = index;
				}
				return result;
			}

			// Token: 0x06000C61 RID: 3169 RVA: 0x00026894 File Offset: 0x00024A94
			private int TryAddValue(T value)
			{
				object obj = this.lockObject;
				int result;
				lock (obj)
				{
					int index = Array.IndexOf<T>(this.Items, value);
					bool flag2 = index == -1;
					if (flag2)
					{
						index = this.AddValue(value);
					}
					result = index;
				}
				return result;
			}

			// Token: 0x06000C62 RID: 3170 RVA: 0x000268F8 File Offset: 0x00024AF8
			private int AddValue(T value)
			{
				int index = this.Items.Length;
				T[] snapshot = this.CreateSnapshot();
				snapshot[index] = value;
				this.Items = snapshot;
				return index;
			}

			// Token: 0x06000C63 RID: 3171 RVA: 0x0002692C File Offset: 0x00024B2C
			private T[] CreateSnapshot()
			{
				T[] snapshot = new T[this.Items.Length + 1];
				Array.Copy(this.Items, snapshot, this.Items.Length);
				return snapshot;
			}

			// Token: 0x04000447 RID: 1095
			public T[] Items = Array.Empty<T>();

			// Token: 0x04000448 RID: 1096
			private readonly object lockObject = new object();
		}

		// Token: 0x020001F5 RID: 501
		private class PropertyDependencyDisabler : IPropertyDependencySelector
		{
			// Token: 0x06000C65 RID: 3173 RVA: 0x00026983 File Offset: 0x00024B83
			public IEnumerable<PropertyDependency> Execute(Type type)
			{
				return Array.Empty<PropertyDependency>();
			}
		}

		// Token: 0x020001F6 RID: 502
		private class DynamicMethodSkeleton : IMethodSkeleton
		{
			// Token: 0x06000C67 RID: 3175 RVA: 0x00026993 File Offset: 0x00024B93
			public DynamicMethodSkeleton(Type returnType, Type[] parameterTypes)
			{
				this.CreateDynamicMethod(returnType, parameterTypes);
			}

			// Token: 0x06000C68 RID: 3176 RVA: 0x000269A5 File Offset: 0x00024BA5
			public IEmitter GetEmitter()
			{
				return this.emitter;
			}

			// Token: 0x06000C69 RID: 3177 RVA: 0x000269AD File Offset: 0x00024BAD
			public Delegate CreateDelegate(Type delegateType)
			{
				return this.dynamicMethod.CreateDelegate(delegateType);
			}

			// Token: 0x06000C6A RID: 3178 RVA: 0x000269BB File Offset: 0x00024BBB
			private void CreateDynamicMethod(Type returnType, Type[] parameterTypes)
			{
				this.dynamicMethod = new DynamicMethod(returnType, parameterTypes);
				this.emitter = new Emitter(this.dynamicMethod.GetILGenerator(), parameterTypes);
			}

			// Token: 0x04000449 RID: 1097
			private IEmitter emitter;

			// Token: 0x0400044A RID: 1098
			private DynamicMethod dynamicMethod;
		}

		// Token: 0x020001F7 RID: 503
		private class ServiceRegistry<T> : ThreadSafeDictionary<Type, ThreadSafeDictionary<string, T>>
		{
		}

		// Token: 0x020001F8 RID: 504
		private class FactoryRule
		{
			// Token: 0x1700023D RID: 573
			// (get) Token: 0x06000C6C RID: 3180 RVA: 0x000269EB File Offset: 0x00024BEB
			// (set) Token: 0x06000C6D RID: 3181 RVA: 0x000269F3 File Offset: 0x00024BF3
			public Func<Type, string, bool> CanCreateInstance { get; set; }

			// Token: 0x1700023E RID: 574
			// (get) Token: 0x06000C6E RID: 3182 RVA: 0x000269FC File Offset: 0x00024BFC
			// (set) Token: 0x06000C6F RID: 3183 RVA: 0x00026A04 File Offset: 0x00024C04
			public Func<ServiceRequest, object> Factory { get; set; }

			// Token: 0x1700023F RID: 575
			// (get) Token: 0x06000C70 RID: 3184 RVA: 0x00026A0D File Offset: 0x00024C0D
			// (set) Token: 0x06000C71 RID: 3185 RVA: 0x00026A15 File Offset: 0x00024C15
			public ILifetime LifeTime { get; set; }
		}

		// Token: 0x020001F9 RID: 505
		private class Initializer
		{
			// Token: 0x17000240 RID: 576
			// (get) Token: 0x06000C73 RID: 3187 RVA: 0x00026A27 File Offset: 0x00024C27
			// (set) Token: 0x06000C74 RID: 3188 RVA: 0x00026A2F File Offset: 0x00024C2F
			public Func<ServiceRegistration, bool> Predicate { get; set; }

			// Token: 0x17000241 RID: 577
			// (get) Token: 0x06000C75 RID: 3189 RVA: 0x00026A38 File Offset: 0x00024C38
			// (set) Token: 0x06000C76 RID: 3190 RVA: 0x00026A40 File Offset: 0x00024C40
			public Action<IServiceFactory, object> Initialize { get; set; }
		}

		// Token: 0x020001FA RID: 506
		private class ServiceOverride
		{
			// Token: 0x06000C78 RID: 3192 RVA: 0x00026A52 File Offset: 0x00024C52
			public ServiceOverride(Func<ServiceRegistration, bool> canOverride, Func<IServiceFactory, ServiceRegistration, ServiceRegistration> serviceRegistrationFactory)
			{
				this.CanOverride = canOverride;
				this.serviceRegistrationFactory = serviceRegistrationFactory;
			}

			// Token: 0x17000242 RID: 578
			// (get) Token: 0x06000C79 RID: 3193 RVA: 0x00026A75 File Offset: 0x00024C75
			public Func<ServiceRegistration, bool> CanOverride { get; }

			// Token: 0x06000C7A RID: 3194 RVA: 0x00026A80 File Offset: 0x00024C80
			[ExcludeFromCodeCoverage]
			public ServiceRegistration Execute(IServiceFactory serviceFactory, ServiceRegistration serviceRegistration)
			{
				bool flag = this.hasExecuted;
				ServiceRegistration result;
				if (flag)
				{
					result = serviceRegistration;
				}
				else
				{
					object obj = this.lockObject;
					lock (obj)
					{
						bool flag3 = this.hasExecuted;
						if (flag3)
						{
							result = serviceRegistration;
						}
						else
						{
							this.hasExecuted = true;
							ServiceRegistration registration = this.serviceRegistrationFactory(serviceFactory, serviceRegistration);
							result = registration;
						}
					}
				}
				return result;
			}

			// Token: 0x04000450 RID: 1104
			private readonly object lockObject = new object();

			// Token: 0x04000451 RID: 1105
			private readonly Func<IServiceFactory, ServiceRegistration, ServiceRegistration> serviceRegistrationFactory;

			// Token: 0x04000452 RID: 1106
			private bool hasExecuted;
		}
	}
}
