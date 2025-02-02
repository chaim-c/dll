using System;
using System.Collections.Generic;
using System.Reflection;

namespace MCM.LightInject
{
	// Token: 0x020000D2 RID: 210
	internal interface IPropertySelector
	{
		// Token: 0x0600048A RID: 1162
		IEnumerable<PropertyInfo> Execute(Type type);
	}
}
