using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace MCM.LightInject
{
	// Token: 0x020000FA RID: 250
	[ExcludeFromCodeCoverage]
	internal class TypeConstructionInfoBuilder : IConstructionInfoBuilder
	{
		// Token: 0x06000602 RID: 1538 RVA: 0x0001343E File Offset: 0x0001163E
		public TypeConstructionInfoBuilder(IConstructorSelector constructorSelector, IConstructorDependencySelector constructorDependencySelector, IPropertyDependencySelector propertyDependencySelector, Func<Type, string, Delegate> getConstructorDependencyExpression, Func<Type, string, Delegate> getPropertyDependencyExpression)
		{
			this.constructorSelector = constructorSelector;
			this.constructorDependencySelector = constructorDependencySelector;
			this.propertyDependencySelector = propertyDependencySelector;
			this.getConstructorDependencyExpression = getConstructorDependencyExpression;
			this.getPropertyDependencyExpression = getPropertyDependencyExpression;
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x00013470 File Offset: 0x00011670
		public ConstructionInfo Execute(Registration registration)
		{
			bool flag = registration.FactoryExpression != null;
			ConstructionInfo result;
			if (flag)
			{
				result = new ConstructionInfo
				{
					FactoryDelegate = registration.FactoryExpression
				};
			}
			else
			{
				Type implementingType = registration.ImplementingType;
				ConstructionInfo constructionInfo = new ConstructionInfo
				{
					ImplementingType = implementingType
				};
				constructionInfo.PropertyDependencies.AddRange(this.GetPropertyDependencies(implementingType));
				constructionInfo.Constructor = this.constructorSelector.Execute(implementingType);
				constructionInfo.ConstructorDependencies.AddRange(this.GetConstructorDependencies(constructionInfo.Constructor));
				result = constructionInfo;
			}
			return result;
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x000134FC File Offset: 0x000116FC
		private IEnumerable<ConstructorDependency> GetConstructorDependencies(ConstructorInfo constructorInfo)
		{
			ConstructorDependency[] constructorDependencies = this.constructorDependencySelector.Execute(constructorInfo).ToArray<ConstructorDependency>();
			foreach (ConstructorDependency constructorDependency in constructorDependencies)
			{
				constructorDependency.FactoryExpression = this.getConstructorDependencyExpression(constructorDependency.ServiceType, constructorDependency.ServiceName);
			}
			return constructorDependencies;
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00013558 File Offset: 0x00011758
		private IEnumerable<PropertyDependency> GetPropertyDependencies(Type implementingType)
		{
			PropertyDependency[] propertyDependencies = this.propertyDependencySelector.Execute(implementingType).ToArray<PropertyDependency>();
			foreach (PropertyDependency property in propertyDependencies)
			{
				property.FactoryExpression = this.getPropertyDependencyExpression(property.ServiceType, property.ServiceName);
			}
			return propertyDependencies;
		}

		// Token: 0x040001B1 RID: 433
		private readonly IConstructorSelector constructorSelector;

		// Token: 0x040001B2 RID: 434
		private readonly IConstructorDependencySelector constructorDependencySelector;

		// Token: 0x040001B3 RID: 435
		private readonly IPropertyDependencySelector propertyDependencySelector;

		// Token: 0x040001B4 RID: 436
		private readonly Func<Type, string, Delegate> getConstructorDependencyExpression;

		// Token: 0x040001B5 RID: 437
		private readonly Func<Type, string, Delegate> getPropertyDependencyExpression;
	}
}
