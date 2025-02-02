using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace MCM.LightInject
{
	// Token: 0x020000F8 RID: 248
	[ExcludeFromCodeCoverage]
	internal class ConstructorDependencySelector : IConstructorDependencySelector
	{
		// Token: 0x060005FC RID: 1532 RVA: 0x00013370 File Offset: 0x00011570
		public virtual IEnumerable<ConstructorDependency> Execute(ConstructorInfo constructor)
		{
			return from p in constructor.GetParameters()
			orderby p.Position
			select new ConstructorDependency
			{
				ServiceName = string.Empty,
				ServiceType = p.ParameterType,
				Parameter = p,
				IsRequired = true
			};
		}
	}
}
