using System;
using System.Collections.Generic;
using TaleWorlds.DotNet;

namespace ManagedCallbacks
{
	// Token: 0x02000006 RID: 6
	public class CallbackManager : ICallbackManager
	{
		// Token: 0x0600000A RID: 10 RVA: 0x00002534 File Offset: 0x00000734
		public void Initialize()
		{
			CoreCallbacksGenerated.Initialize();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000253B File Offset: 0x0000073B
		public Delegate[] GetDelegates()
		{
			return CoreCallbacksGenerated.Delegates;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002542 File Offset: 0x00000742
		public Dictionary<string, object> GetScriptingInterfaceObjects()
		{
			return ScriptingInterfaceObjects.GetObjects();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002549 File Offset: 0x00000749
		public void SetFunctionPointer(int id, IntPtr pointer)
		{
			ScriptingInterfaceObjects.SetFunctionPointer(id, pointer);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002552 File Offset: 0x00000752
		public void CheckSharedStructureSizes()
		{
		}
	}
}
