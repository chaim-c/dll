using System;
using System.Reflection;

namespace MCM.LightInject
{
	// Token: 0x020000D1 RID: 209
	internal interface ICompositionRootAttributeExtractor
	{
		// Token: 0x06000489 RID: 1161
		CompositionRootTypeAttribute[] GetAttributes(Assembly assembly);
	}
}
