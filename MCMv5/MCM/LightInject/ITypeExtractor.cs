using System;
using System.Reflection;

namespace MCM.LightInject
{
	// Token: 0x020000D0 RID: 208
	internal interface ITypeExtractor
	{
		// Token: 0x06000488 RID: 1160
		Type[] Execute(Assembly assembly);
	}
}
