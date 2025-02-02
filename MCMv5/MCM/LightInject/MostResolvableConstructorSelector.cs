using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace MCM.LightInject
{
	// Token: 0x020000F7 RID: 247
	[ExcludeFromCodeCoverage]
	internal class MostResolvableConstructorSelector : IConstructorSelector
	{
		// Token: 0x060005F6 RID: 1526 RVA: 0x000131A6 File Offset: 0x000113A6
		public MostResolvableConstructorSelector(Func<Type, string, bool> canGetInstance, bool enableOptionalArguments)
		{
			this.canGetInstance = canGetInstance;
			this.enableOptionalArguments = enableOptionalArguments;
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x000131BE File Offset: 0x000113BE
		public MostResolvableConstructorSelector(Func<Type, string, bool> canGetInstance) : this(canGetInstance, false)
		{
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x000131CC File Offset: 0x000113CC
		public ConstructorInfo Execute(Type implementingType)
		{
			ConstructorInfo[] constructorCandidates = (from c in implementingType.GetTypeInfo().DeclaredConstructors
			where c.IsPublic && !c.IsStatic
			select c).ToArray<ConstructorInfo>();
			bool flag = constructorCandidates.Length == 0;
			if (flag)
			{
				throw new InvalidOperationException("Missing public constructor for Type: " + implementingType.FullName);
			}
			bool flag2 = constructorCandidates.Length == 1;
			if (!flag2)
			{
				foreach (ConstructorInfo constructorCandidate in from c in constructorCandidates
				orderby c.GetParameters().Length descending
				select c)
				{
					ParameterInfo[] parameters = constructorCandidate.GetParameters();
					bool flag3 = this.CanCreateParameterDependencies(parameters);
					if (flag3)
					{
						return constructorCandidate;
					}
				}
				throw new InvalidOperationException("No resolvable constructor found for Type: " + implementingType.FullName);
			}
			return constructorCandidates[0];
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x000132DC File Offset: 0x000114DC
		protected virtual string GetServiceName(ParameterInfo parameter)
		{
			return parameter.Name;
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x000132F4 File Offset: 0x000114F4
		private bool CanCreateParameterDependencies(IEnumerable<ParameterInfo> parameters)
		{
			return parameters.All(new Func<ParameterInfo, bool>(this.CanCreateParameterDependency));
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x00013318 File Offset: 0x00011518
		private bool CanCreateParameterDependency(ParameterInfo parameterInfo)
		{
			return this.canGetInstance(parameterInfo.ParameterType, string.Empty) || this.canGetInstance(parameterInfo.ParameterType, this.GetServiceName(parameterInfo)) || (parameterInfo.HasDefaultValue && this.enableOptionalArguments);
		}

		// Token: 0x040001AE RID: 430
		private readonly Func<Type, string, bool> canGetInstance;

		// Token: 0x040001AF RID: 431
		private readonly bool enableOptionalArguments;
	}
}
