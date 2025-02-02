using System;
using System.Collections.Generic;

namespace TaleWorlds.DotNet
{
	// Token: 0x02000015 RID: 21
	public interface ICallbackManager
	{
		// Token: 0x06000057 RID: 87
		void Initialize();

		// Token: 0x06000058 RID: 88
		Delegate[] GetDelegates();

		// Token: 0x06000059 RID: 89
		Dictionary<string, object> GetScriptingInterfaceObjects();

		// Token: 0x0600005A RID: 90
		void SetFunctionPointer(int id, IntPtr pointer);

		// Token: 0x0600005B RID: 91
		void CheckSharedStructureSizes();
	}
}
