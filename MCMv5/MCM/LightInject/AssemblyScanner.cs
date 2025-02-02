using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace MCM.LightInject
{
	// Token: 0x02000113 RID: 275
	[ExcludeFromCodeCoverage]
	internal class AssemblyScanner : IAssemblyScanner
	{
		// Token: 0x06000690 RID: 1680 RVA: 0x00014BA0 File Offset: 0x00012DA0
		public AssemblyScanner(ITypeExtractor concreteTypeExtractor, ITypeExtractor compositionRootTypeExtractor, ICompositionRootExecutor compositionRootExecutor, IGenericArgumentMapper genericArgumentMapper)
		{
			this.concreteTypeExtractor = concreteTypeExtractor;
			this.compositionRootTypeExtractor = compositionRootTypeExtractor;
			this.compositionRootExecutor = compositionRootExecutor;
			this.genericArgumentMapper = genericArgumentMapper;
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x00014BC8 File Offset: 0x00012DC8
		public void Scan(Assembly assembly, IServiceRegistry serviceRegistry, Func<ILifetime> lifetimeFactory, Func<Type, Type, bool> shouldRegister, Func<Type, Type, string> serviceNameProvider)
		{
			Type[] concreteTypes = this.GetConcreteTypes(assembly);
			foreach (Type type in concreteTypes)
			{
				this.BuildImplementationMap(type, serviceRegistry, lifetimeFactory, shouldRegister, serviceNameProvider);
			}
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x00014C04 File Offset: 0x00012E04
		public void Scan(Assembly assembly, IServiceRegistry serviceRegistry)
		{
			Type[] compositionRootTypes = this.GetCompositionRootTypes(assembly);
			bool flag = compositionRootTypes.Length != 0 && !object.Equals(this.currentAssembly, assembly);
			if (flag)
			{
				this.currentAssembly = assembly;
				this.ExecuteCompositionRoots(compositionRootTypes);
			}
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x00014C45 File Offset: 0x00012E45
		private static IEnumerable<Type> GetBaseTypes(Type concreteType)
		{
			Type baseType = concreteType;
			while (baseType != typeof(object) && baseType != null)
			{
				yield return baseType;
				baseType = baseType.GetTypeInfo().BaseType;
			}
			yield break;
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x00014C58 File Offset: 0x00012E58
		private void ExecuteCompositionRoots(IEnumerable<Type> compositionRoots)
		{
			foreach (Type compositionRoot in compositionRoots)
			{
				this.compositionRootExecutor.Execute(compositionRoot);
			}
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x00014CAC File Offset: 0x00012EAC
		private Type[] GetConcreteTypes(Assembly assembly)
		{
			return this.concreteTypeExtractor.Execute(assembly);
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x00014CCC File Offset: 0x00012ECC
		private Type[] GetCompositionRootTypes(Assembly assembly)
		{
			return this.compositionRootTypeExtractor.Execute(assembly);
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x00014CEC File Offset: 0x00012EEC
		private void BuildImplementationMap(Type implementingType, IServiceRegistry serviceRegistry, Func<ILifetime> lifetimeFactory, Func<Type, Type, bool> shouldRegister, Func<Type, Type, string> serviceNameProvider)
		{
			Type[] interfaces = implementingType.GetTypeInfo().ImplementedInterfaces.ToArray<Type>();
			foreach (Type interfaceType in interfaces)
			{
				bool flag = shouldRegister(interfaceType, implementingType);
				if (flag)
				{
					this.RegisterInternal(interfaceType, implementingType, serviceRegistry, lifetimeFactory(), serviceNameProvider);
				}
			}
			foreach (Type baseType in AssemblyScanner.GetBaseTypes(implementingType))
			{
				bool flag2 = shouldRegister(baseType, implementingType);
				if (flag2)
				{
					this.RegisterInternal(baseType, implementingType, serviceRegistry, lifetimeFactory(), serviceNameProvider);
				}
			}
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x00014DB0 File Offset: 0x00012FB0
		private void RegisterInternal(Type serviceType, Type implementingType, IServiceRegistry serviceRegistry, ILifetime lifetime, Func<Type, Type, string> serviceNameProvider)
		{
			TypeInfo serviceTypeInfo = serviceType.GetTypeInfo();
			bool containsGenericParameters = implementingType.GetTypeInfo().ContainsGenericParameters;
			if (containsGenericParameters)
			{
				bool flag = !this.genericArgumentMapper.Map(serviceType, implementingType).IsValid;
				if (flag)
				{
					return;
				}
			}
			bool flag2 = serviceTypeInfo.IsGenericType && serviceTypeInfo.ContainsGenericParameters;
			if (flag2)
			{
				serviceType = serviceTypeInfo.GetGenericTypeDefinition();
			}
			serviceRegistry.Register(serviceType, implementingType, serviceNameProvider(serviceType, implementingType), lifetime);
		}

		// Token: 0x040001EA RID: 490
		private readonly ITypeExtractor concreteTypeExtractor;

		// Token: 0x040001EB RID: 491
		private readonly ITypeExtractor compositionRootTypeExtractor;

		// Token: 0x040001EC RID: 492
		private readonly ICompositionRootExecutor compositionRootExecutor;

		// Token: 0x040001ED RID: 493
		private readonly IGenericArgumentMapper genericArgumentMapper;

		// Token: 0x040001EE RID: 494
		private Assembly currentAssembly;
	}
}
