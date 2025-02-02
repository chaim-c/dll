using System;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace MCM.LightInject
{
	// Token: 0x02000126 RID: 294
	[ExcludeFromCodeCoverage]
	internal static class ReflectionHelper
	{
		// Token: 0x060006ED RID: 1773 RVA: 0x00016C8D File Offset: 0x00014E8D
		public static MethodInfo GetGetInstanceWithParametersMethod(Type serviceType)
		{
			ConcurrentDictionary<Type, MethodInfo> value = ReflectionHelper.GetInstanceWithParametersMethods.Value;
			Func<Type, MethodInfo> valueFactory;
			if ((valueFactory = ReflectionHelper.<>O.<0>__CreateGetInstanceWithParametersMethod) == null)
			{
				valueFactory = (ReflectionHelper.<>O.<0>__CreateGetInstanceWithParametersMethod = new Func<Type, MethodInfo>(ReflectionHelper.CreateGetInstanceWithParametersMethod));
			}
			return value.GetOrAdd(serviceType, valueFactory);
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x00016CBC File Offset: 0x00014EBC
		public static Delegate CreateGetNamedInstanceWithParametersDelegate(IServiceFactory factory, Type delegateType, string serviceName)
		{
			Type[] genericTypeArguments = delegateType.GetTypeInfo().GenericTypeArguments;
			MethodInfo openGenericMethod = typeof(ReflectionHelper).GetTypeInfo().DeclaredMethods.Single((MethodInfo m) => m.GetGenericArguments().Length == genericTypeArguments.Length && m.Name == "CreateGenericGetNamedParameterizedInstanceDelegate");
			MethodInfo closedGenericMethod = openGenericMethod.MakeGenericMethod(genericTypeArguments);
			return (Delegate)closedGenericMethod.Invoke(null, new object[]
			{
				factory,
				serviceName
			});
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x00016D32 File Offset: 0x00014F32
		private static Lazy<ThreadSafeDictionary<Type, MethodInfo>> CreateLazyGetInstanceWithParametersMethods()
		{
			return new Lazy<ThreadSafeDictionary<Type, MethodInfo>>(() => new ThreadSafeDictionary<Type, MethodInfo>());
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x00016D58 File Offset: 0x00014F58
		private static MethodInfo CreateGetInstanceWithParametersMethod(Type serviceType)
		{
			Type[] genericTypeArguments = serviceType.GetTypeInfo().GenericTypeArguments;
			MethodInfo openGenericMethod = typeof(ServiceFactoryExtensions).GetTypeInfo().DeclaredMethods.Single(delegate(MethodInfo m)
			{
				bool result;
				if (m.Name == "GetInstance" && m.GetGenericArguments().Length == genericTypeArguments.Length)
				{
					result = m.GetParameters().All((ParameterInfo p) => p.Name != "serviceName");
				}
				else
				{
					result = false;
				}
				return result;
			});
			return openGenericMethod.MakeGenericMethod(genericTypeArguments);
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x00016DB8 File Offset: 0x00014FB8
		private static Func<TArg, TService> CreateGenericGetNamedParameterizedInstanceDelegate<TArg, TService>(IServiceFactory factory, string serviceName)
		{
			return (TArg arg) => factory.GetInstance(arg, serviceName);
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x00016DE8 File Offset: 0x00014FE8
		private static Func<TArg1, TArg2, TService> CreateGenericGetNamedParameterizedInstanceDelegate<TArg1, TArg2, TService>(IServiceFactory factory, string serviceName)
		{
			return (TArg1 arg1, TArg2 arg2) => factory.GetInstance(arg1, arg2, serviceName);
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x00016E18 File Offset: 0x00015018
		private static Func<TArg1, TArg2, TArg3, TService> CreateGenericGetNamedParameterizedInstanceDelegate<TArg1, TArg2, TArg3, TService>(IServiceFactory factory, string serviceName)
		{
			return (TArg1 arg1, TArg2 arg2, TArg3 arg3) => factory.GetInstance(arg1, arg2, arg3, serviceName);
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x00016E48 File Offset: 0x00015048
		private static Func<TArg1, TArg2, TArg3, TArg4, TService> CreateGenericGetNamedParameterizedInstanceDelegate<TArg1, TArg2, TArg3, TArg4, TService>(IServiceFactory factory, string serviceName)
		{
			return (TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4) => factory.GetInstance(arg1, arg2, arg3, arg4, serviceName);
		}

		// Token: 0x0400021F RID: 543
		private static readonly Lazy<ThreadSafeDictionary<Type, MethodInfo>> GetInstanceWithParametersMethods = ReflectionHelper.CreateLazyGetInstanceWithParametersMethods();

		// Token: 0x0200023E RID: 574
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000508 RID: 1288
			public static Func<Type, MethodInfo> <0>__CreateGetInstanceWithParametersMethod;
		}
	}
}
