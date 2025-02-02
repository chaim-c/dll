using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace MCM.LightInject
{
	// Token: 0x02000115 RID: 277
	[ExcludeFromCodeCoverage]
	internal class PropertySelector : IPropertySelector
	{
		// Token: 0x0600069B RID: 1691 RVA: 0x00014EE8 File Offset: 0x000130E8
		public IEnumerable<PropertyInfo> Execute(Type type)
		{
			return type.GetRuntimeProperties().Where(new Func<PropertyInfo, bool>(this.IsInjectable)).ToList<PropertyInfo>();
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x00014F18 File Offset: 0x00013118
		protected virtual bool IsInjectable(PropertyInfo propertyInfo)
		{
			return !PropertySelector.IsReadOnly(propertyInfo);
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x00014F34 File Offset: 0x00013134
		private static bool IsReadOnly(PropertyInfo propertyInfo)
		{
			return propertyInfo.SetMethod == null || propertyInfo.SetMethod.IsStatic || propertyInfo.SetMethod.IsPrivate || propertyInfo.GetIndexParameters().Length != 0;
		}
	}
}
