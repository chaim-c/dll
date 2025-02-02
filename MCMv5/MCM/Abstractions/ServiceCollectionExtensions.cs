using System;
using System.Runtime.CompilerServices;
using BUTR.DependencyInjection;
using MCM.Abstractions.FluentBuilder;
using MCM.Abstractions.Global;
using MCM.Abstractions.PerCampaign;
using MCM.Abstractions.PerSave;
using MCM.Abstractions.Properties;

namespace MCM.Abstractions
{
	// Token: 0x02000054 RID: 84
	[NullableContext(1)]
	[Nullable(0)]
	public static class ServiceCollectionExtensions
	{
		// Token: 0x060001BA RID: 442 RVA: 0x00007978 File Offset: 0x00005B78
		public static IGenericServiceContainer AddSettingsProvider<[Nullable(0)] TService, TImplementation>(this IGenericServiceContainer services) where TService : BaseSettingsProvider where TImplementation : class, TService
		{
			services.RegisterSingleton<TImplementation>();
			services.RegisterSingleton<TService>((IGenericServiceFactory sp) => (TService)((object)sp.GetService<TImplementation>()));
			services.AddSettingsProvider<TImplementation>();
			return services;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x000079C0 File Offset: 0x00005BC0
		public static IGenericServiceContainer AddSettingsProvider<[Nullable(0)] TImplementation>(this IGenericServiceContainer services) where TImplementation : BaseSettingsProvider
		{
			services.RegisterSingleton<TImplementation>();
			services.RegisterSingleton<BaseSettingsProvider>((IGenericServiceFactory sp) => sp.GetService<TImplementation>());
			return services;
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00007A00 File Offset: 0x00005C00
		public static IGenericServiceContainer AddSettingsFormat<TService, TImplementation>(this IGenericServiceContainer services) where TService : class, ISettingsFormat where TImplementation : class, TService
		{
			services.RegisterSingleton<TImplementation>();
			services.RegisterSingleton<TService>((IGenericServiceFactory sp) => (TService)((object)sp.GetService<TImplementation>()));
			services.AddSettingsFormat<TImplementation>();
			return services;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00007A48 File Offset: 0x00005C48
		public static IGenericServiceContainer AddSettingsFormat<TImplementation>(this IGenericServiceContainer services) where TImplementation : class, ISettingsFormat
		{
			services.RegisterSingleton<TImplementation>();
			services.RegisterSingleton<ISettingsFormat>((IGenericServiceFactory sp) => sp.GetService<TImplementation>());
			return services;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00007A88 File Offset: 0x00005C88
		public static IGenericServiceContainer AddSettingsPropertyDiscoverer<TService, TImplementation>(this IGenericServiceContainer services) where TService : class, ISettingsPropertyDiscoverer where TImplementation : class, TService
		{
			services.RegisterSingleton<TImplementation>();
			services.RegisterSingleton<TService>((IGenericServiceFactory sp) => (TService)((object)sp.GetService<TImplementation>()));
			services.AddSettingsPropertyDiscoverer<TImplementation>();
			return services;
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00007AD0 File Offset: 0x00005CD0
		public static IGenericServiceContainer AddSettingsPropertyDiscoverer<TImplementation>(this IGenericServiceContainer services) where TImplementation : class, ISettingsPropertyDiscoverer
		{
			services.RegisterSingleton<TImplementation>();
			services.RegisterSingleton<ISettingsPropertyDiscoverer>((IGenericServiceFactory sp) => sp.GetService<TImplementation>());
			return services;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00007B10 File Offset: 0x00005D10
		public static IGenericServiceContainer AddSettingsContainer<TService, TImplementation>(this IGenericServiceContainer services) where TService : class, ISettingsContainer where TImplementation : class, TService
		{
			services.RegisterSingleton<TImplementation>();
			services.RegisterSingleton<TService>((IGenericServiceFactory sp) => (TService)((object)sp.GetService<TImplementation>()));
			services.AddSettingsContainer<TImplementation>();
			return services;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00007B58 File Offset: 0x00005D58
		public static IGenericServiceContainer AddSettingsContainer<TImplementation>(this IGenericServiceContainer services) where TImplementation : class, ISettingsContainer
		{
			services.RegisterSingleton<TImplementation>();
			services.RegisterSingleton<ISettingsContainer>((IGenericServiceFactory sp) => sp.GetService<TImplementation>());
			bool flag = typeof(IPerCampaignSettingsContainer).IsAssignableFrom(typeof(TImplementation));
			if (flag)
			{
				services.RegisterSingleton<IPerCampaignSettingsContainer>((IGenericServiceFactory sp) => (IPerCampaignSettingsContainer)((object)sp.GetService<TImplementation>()));
			}
			bool flag2 = typeof(IPerSaveSettingsContainer).IsAssignableFrom(typeof(TImplementation));
			if (flag2)
			{
				services.RegisterSingleton<IPerSaveSettingsContainer>((IGenericServiceFactory sp) => (IPerSaveSettingsContainer)((object)sp.GetService<TImplementation>()));
			}
			bool flag3 = typeof(IGlobalSettingsContainer).IsAssignableFrom(typeof(TImplementation));
			if (flag3)
			{
				services.RegisterSingleton<IGlobalSettingsContainer>((IGenericServiceFactory sp) => (IGlobalSettingsContainer)((object)sp.GetService<TImplementation>()));
			}
			bool flag4 = typeof(IFluentPerCampaignSettingsContainer).IsAssignableFrom(typeof(TImplementation));
			if (flag4)
			{
				services.RegisterSingleton<IFluentPerCampaignSettingsContainer>((IGenericServiceFactory sp) => (IFluentPerCampaignSettingsContainer)((object)sp.GetService<TImplementation>()));
			}
			bool flag5 = typeof(IFluentPerSaveSettingsContainer).IsAssignableFrom(typeof(TImplementation));
			if (flag5)
			{
				services.RegisterSingleton<IFluentPerSaveSettingsContainer>((IGenericServiceFactory sp) => (IFluentPerSaveSettingsContainer)((object)sp.GetService<TImplementation>()));
			}
			bool flag6 = typeof(IFluentGlobalSettingsContainer).IsAssignableFrom(typeof(TImplementation));
			if (flag6)
			{
				services.RegisterSingleton<IFluentGlobalSettingsContainer>((IGenericServiceFactory sp) => (IFluentGlobalSettingsContainer)((object)sp.GetService<TImplementation>()));
			}
			return services;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00007D30 File Offset: 0x00005F30
		public static IGenericServiceContainer AddExternalSettingsProvider<TImplementation>(this IGenericServiceContainer services) where TImplementation : class, IExternalSettingsProvider
		{
			services.RegisterSingleton<TImplementation>();
			services.RegisterSingleton<IExternalSettingsProvider>((IGenericServiceFactory sp) => sp.GetService<TImplementation>());
			return services;
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00007D70 File Offset: 0x00005F70
		public static IGenericServiceContainer AddSettingsBuilderFactory<TImplementation>(this IGenericServiceContainer services) where TImplementation : class, ISettingsBuilderFactory
		{
			services.RegisterSingleton<TImplementation>();
			services.RegisterSingleton<ISettingsBuilderFactory>((IGenericServiceFactory factory) => factory.GetService<TImplementation>());
			return services;
		}
	}
}
