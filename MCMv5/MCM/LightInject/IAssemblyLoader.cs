using System;
using System.Collections.Generic;
using System.Reflection;

namespace MCM.LightInject
{
	// Token: 0x020000DB RID: 219
	internal interface IAssemblyLoader
	{
		// Token: 0x06000498 RID: 1176
		IEnumerable<Assembly> Load(string searchPattern);
	}
}
