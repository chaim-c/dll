using System;
using System.Collections.Generic;

namespace MCM.LightInject
{
	// Token: 0x020000D3 RID: 211
	internal interface IPropertyDependencySelector
	{
		// Token: 0x0600048B RID: 1163
		IEnumerable<PropertyDependency> Execute(Type type);
	}
}
