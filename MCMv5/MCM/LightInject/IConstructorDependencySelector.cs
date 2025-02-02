using System;
using System.Collections.Generic;
using System.Reflection;

namespace MCM.LightInject
{
	// Token: 0x020000D4 RID: 212
	internal interface IConstructorDependencySelector
	{
		// Token: 0x0600048C RID: 1164
		IEnumerable<ConstructorDependency> Execute(ConstructorInfo constructor);
	}
}
