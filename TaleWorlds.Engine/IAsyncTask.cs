using System;
using TaleWorlds.DotNet;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x0200002C RID: 44
	[ApplicationInterfaceBase]
	internal interface IAsyncTask
	{
		// Token: 0x06000411 RID: 1041
		[EngineMethod("create_with_function", false)]
		AsyncTask CreateWithDelegate(ManagedDelegate function, bool isBackground);

		// Token: 0x06000412 RID: 1042
		[EngineMethod("invoke", false)]
		void Invoke(UIntPtr Pointer);

		// Token: 0x06000413 RID: 1043
		[EngineMethod("wait", false)]
		void Wait(UIntPtr Pointer);
	}
}
