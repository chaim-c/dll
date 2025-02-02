using System;
using TaleWorlds.DotNet;

namespace TaleWorlds.Engine
{
	// Token: 0x02000082 RID: 130
	[EngineClass("rglScript_component")]
	public abstract class ScriptComponent : NativeObject
	{
		// Token: 0x06000A04 RID: 2564 RVA: 0x0000AC44 File Offset: 0x00008E44
		protected ScriptComponent()
		{
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x0000AC4C File Offset: 0x00008E4C
		internal ScriptComponent(UIntPtr pointer)
		{
			base.Construct(pointer);
		}
	}
}
