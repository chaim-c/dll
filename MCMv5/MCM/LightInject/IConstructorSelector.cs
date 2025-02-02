using System;
using System.Reflection;

namespace MCM.LightInject
{
	// Token: 0x020000D9 RID: 217
	internal interface IConstructorSelector
	{
		// Token: 0x06000492 RID: 1170
		ConstructorInfo Execute(Type implementingType);
	}
}
