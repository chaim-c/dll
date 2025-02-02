using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace MCM.LightInject
{
	// Token: 0x02000110 RID: 272
	[ExcludeFromCodeCoverage]
	internal class ConcreteTypeExtractor : ITypeExtractor
	{
		// Token: 0x06000680 RID: 1664 RVA: 0x00014298 File Offset: 0x00012498
		static ConcreteTypeExtractor()
		{
			ConcreteTypeExtractor.InternalTypes.Add(typeof(ConstructorDependency));
			ConcreteTypeExtractor.InternalTypes.Add(typeof(PropertyDependency));
			ConcreteTypeExtractor.InternalTypes.Add(typeof(ThreadSafeDictionary<, >));
			ConcreteTypeExtractor.InternalTypes.Add(typeof(Scope));
			ConcreteTypeExtractor.InternalTypes.Add(typeof(PerContainerLifetime));
			ConcreteTypeExtractor.InternalTypes.Add(typeof(PerScopeLifetime));
			ConcreteTypeExtractor.InternalTypes.Add(typeof(ServiceRegistration));
			ConcreteTypeExtractor.InternalTypes.Add(typeof(DecoratorRegistration));
			ConcreteTypeExtractor.InternalTypes.Add(typeof(ServiceRequest));
			ConcreteTypeExtractor.InternalTypes.Add(typeof(Registration));
			ConcreteTypeExtractor.InternalTypes.Add(typeof(ServiceContainer));
			ConcreteTypeExtractor.InternalTypes.Add(typeof(ConstructionInfo));
			ConcreteTypeExtractor.InternalTypes.Add(typeof(AssemblyLoader));
			ConcreteTypeExtractor.InternalTypes.Add(typeof(TypeConstructionInfoBuilder));
			ConcreteTypeExtractor.InternalTypes.Add(typeof(ConstructionInfoProvider));
			ConcreteTypeExtractor.InternalTypes.Add(typeof(MostResolvableConstructorSelector));
			ConcreteTypeExtractor.InternalTypes.Add(typeof(PerRequestLifeTime));
			ConcreteTypeExtractor.InternalTypes.Add(typeof(PropertySelector));
			ConcreteTypeExtractor.InternalTypes.Add(typeof(AssemblyScanner));
			ConcreteTypeExtractor.InternalTypes.Add(typeof(ConstructorDependencySelector));
			ConcreteTypeExtractor.InternalTypes.Add(typeof(PropertyDependencySelector));
			ConcreteTypeExtractor.InternalTypes.Add(typeof(CompositionRootTypeAttribute));
			ConcreteTypeExtractor.InternalTypes.Add(typeof(ConcreteTypeExtractor));
			ConcreteTypeExtractor.InternalTypes.Add(typeof(CompositionRootExecutor));
			ConcreteTypeExtractor.InternalTypes.Add(typeof(CompositionRootTypeExtractor));
			ConcreteTypeExtractor.InternalTypes.Add(typeof(CachedTypeExtractor));
			ConcreteTypeExtractor.InternalTypes.Add(typeof(ImmutableList<>));
			ConcreteTypeExtractor.InternalTypes.Add(typeof(KeyValue<, >));
			ConcreteTypeExtractor.InternalTypes.Add(typeof(ImmutableHashTree<, >));
			ConcreteTypeExtractor.InternalTypes.Add(typeof(ImmutableHashTable<, >));
			ConcreteTypeExtractor.InternalTypes.Add(typeof(PerThreadScopeManagerProvider));
			ConcreteTypeExtractor.InternalTypes.Add(typeof(Emitter));
			ConcreteTypeExtractor.InternalTypes.Add(typeof(Instruction));
			ConcreteTypeExtractor.InternalTypes.Add(typeof(Instruction<>));
			ConcreteTypeExtractor.InternalTypes.Add(typeof(GetInstanceDelegate));
			ConcreteTypeExtractor.InternalTypes.Add(typeof(ContainerOptions));
			ConcreteTypeExtractor.InternalTypes.Add(typeof(CompositionRootAttributeExtractor));
			ConcreteTypeExtractor.InternalTypes.Add(typeof(PerLogicalCallContextScopeManagerProvider));
			ConcreteTypeExtractor.InternalTypes.Add(typeof(PerLogicalCallContextScopeManager));
			ConcreteTypeExtractor.InternalTypes.Add(typeof(LogicalThreadStorage<>));
			ConcreteTypeExtractor.InternalTypes.Add(typeof(DynamicMethod));
			ConcreteTypeExtractor.InternalTypes.Add(typeof(ILGenerator));
			ConcreteTypeExtractor.InternalTypes.Add(typeof(LocalBuilder));
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x00014638 File Offset: 0x00012838
		public Type[] Execute(Assembly assembly)
		{
			return (from info in assembly.DefinedTypes
			where ConcreteTypeExtractor.IsConcreteType(info)
			select info).Except(from i in ConcreteTypeExtractor.InternalTypes
			select i.GetTypeInfo()).Cast<Type>().ToArray<Type>();
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x000146AC File Offset: 0x000128AC
		private static bool IsConcreteType(TypeInfo typeInfo)
		{
			return typeInfo.IsClass && !typeInfo.IsNestedPrivate && !typeInfo.IsAbstract && !object.Equals(typeInfo.Assembly, typeof(string).GetTypeInfo().Assembly) && !ConcreteTypeExtractor.IsCompilerGenerated(typeInfo);
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x00014703 File Offset: 0x00012903
		private static bool IsCompilerGenerated(TypeInfo typeInfo)
		{
			return typeInfo.IsDefined(typeof(CompilerGeneratedAttribute), false);
		}

		// Token: 0x040001E5 RID: 485
		private static readonly List<Type> InternalTypes = new List<Type>();
	}
}
