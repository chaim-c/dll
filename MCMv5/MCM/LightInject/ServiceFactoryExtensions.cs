using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MCM.LightInject
{
	// Token: 0x020000E5 RID: 229
	[ExcludeFromCodeCoverage]
	internal static class ServiceFactoryExtensions
	{
		// Token: 0x060004D6 RID: 1238 RVA: 0x0000CE23 File Offset: 0x0000B023
		public static TService GetInstance<TService>(this IServiceFactory factory)
		{
			return (TService)((object)factory.GetInstance(typeof(TService)));
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0000CE3A File Offset: 0x0000B03A
		public static TService GetInstance<TService>(this IServiceFactory factory, string serviceName)
		{
			return (TService)((object)factory.GetInstance(typeof(TService), serviceName));
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0000CE52 File Offset: 0x0000B052
		public static TService GetInstance<T, TService>(this IServiceFactory factory, T value)
		{
			return (TService)((object)factory.GetInstance(typeof(TService), new object[]
			{
				value
			}));
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x0000CE78 File Offset: 0x0000B078
		public static TService GetInstance<T, TService>(this IServiceFactory factory, T value, string serviceName)
		{
			return (TService)((object)factory.GetInstance(typeof(TService), serviceName, new object[]
			{
				value
			}));
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x0000CE9F File Offset: 0x0000B09F
		public static TService GetInstance<T1, T2, TService>(this IServiceFactory factory, T1 arg1, T2 arg2)
		{
			return (TService)((object)factory.GetInstance(typeof(TService), new object[]
			{
				arg1,
				arg2
			}));
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0000CECE File Offset: 0x0000B0CE
		public static TService GetInstance<T1, T2, TService>(this IServiceFactory factory, T1 arg1, T2 arg2, string serviceName)
		{
			return (TService)((object)factory.GetInstance(typeof(TService), serviceName, new object[]
			{
				arg1,
				arg2
			}));
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0000CEFE File Offset: 0x0000B0FE
		public static TService GetInstance<T1, T2, T3, TService>(this IServiceFactory factory, T1 arg1, T2 arg2, T3 arg3)
		{
			return (TService)((object)factory.GetInstance(typeof(TService), new object[]
			{
				arg1,
				arg2,
				arg3
			}));
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0000CF36 File Offset: 0x0000B136
		public static TService GetInstance<T1, T2, T3, TService>(this IServiceFactory factory, T1 arg1, T2 arg2, T3 arg3, string serviceName)
		{
			return (TService)((object)factory.GetInstance(typeof(TService), serviceName, new object[]
			{
				arg1,
				arg2,
				arg3
			}));
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0000CF70 File Offset: 0x0000B170
		public static TService GetInstance<T1, T2, T3, T4, TService>(this IServiceFactory factory, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
		{
			return (TService)((object)factory.GetInstance(typeof(TService), new object[]
			{
				arg1,
				arg2,
				arg3,
				arg4
			}));
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0000CFC0 File Offset: 0x0000B1C0
		public static TService GetInstance<T1, T2, T3, T4, TService>(this IServiceFactory factory, T1 arg1, T2 arg2, T3 arg3, T4 arg4, string serviceName)
		{
			return (TService)((object)factory.GetInstance(typeof(TService), serviceName, new object[]
			{
				arg1,
				arg2,
				arg3,
				arg4
			}));
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0000D00F File Offset: 0x0000B20F
		public static TService TryGetInstance<TService>(this IServiceFactory factory)
		{
			return (TService)((object)factory.TryGetInstance(typeof(TService)));
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0000D026 File Offset: 0x0000B226
		public static TService TryGetInstance<TService>(this IServiceFactory factory, string serviceName)
		{
			return (TService)((object)factory.TryGetInstance(typeof(TService), serviceName));
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0000D03E File Offset: 0x0000B23E
		public static IEnumerable<TService> GetAllInstances<TService>(this IServiceFactory factory)
		{
			return factory.GetInstance<IEnumerable<TService>>();
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x0000D046 File Offset: 0x0000B246
		public static TService Create<TService>(this IServiceFactory factory) where TService : class
		{
			return (TService)((object)factory.Create(typeof(TService)));
		}
	}
}
