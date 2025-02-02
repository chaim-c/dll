using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace MCM.LightInject
{
	// Token: 0x020000F9 RID: 249
	[ExcludeFromCodeCoverage]
	internal class PropertyDependencySelector : IPropertyDependencySelector
	{
		// Token: 0x060005FE RID: 1534 RVA: 0x000133D9 File Offset: 0x000115D9
		public PropertyDependencySelector(IPropertySelector propertySelector)
		{
			this.PropertySelector = propertySelector;
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060005FF RID: 1535 RVA: 0x000133EB File Offset: 0x000115EB
		// (set) Token: 0x06000600 RID: 1536 RVA: 0x000133F3 File Offset: 0x000115F3
		private protected IPropertySelector PropertySelector { protected get; private set; }

		// Token: 0x06000601 RID: 1537 RVA: 0x000133FC File Offset: 0x000115FC
		public virtual IEnumerable<PropertyDependency> Execute(Type type)
		{
			return from p in this.PropertySelector.Execute(type)
			select new PropertyDependency
			{
				Property = p,
				ServiceName = string.Empty,
				ServiceType = p.PropertyType
			};
		}
	}
}
