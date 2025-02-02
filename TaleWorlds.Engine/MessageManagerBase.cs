using System;
using TaleWorlds.DotNet;

namespace TaleWorlds.Engine
{
	// Token: 0x02000067 RID: 103
	public abstract class MessageManagerBase : DotNetObject
	{
		// Token: 0x06000829 RID: 2089
		[EngineCallback]
		protected internal abstract void PostWarningLine(string text);

		// Token: 0x0600082A RID: 2090
		[EngineCallback]
		protected internal abstract void PostSuccessLine(string text);

		// Token: 0x0600082B RID: 2091
		[EngineCallback]
		protected internal abstract void PostMessageLineFormatted(string text, uint color);

		// Token: 0x0600082C RID: 2092
		[EngineCallback]
		protected internal abstract void PostMessageLine(string text, uint color);
	}
}
