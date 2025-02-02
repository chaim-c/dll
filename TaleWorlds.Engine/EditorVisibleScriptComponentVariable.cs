using System;

namespace TaleWorlds.Engine
{
	// Token: 0x02000083 RID: 131
	public class EditorVisibleScriptComponentVariable : Attribute
	{
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000A06 RID: 2566 RVA: 0x0000AC5B File Offset: 0x00008E5B
		// (set) Token: 0x06000A07 RID: 2567 RVA: 0x0000AC63 File Offset: 0x00008E63
		public bool Visible { get; set; }

		// Token: 0x06000A08 RID: 2568 RVA: 0x0000AC6C File Offset: 0x00008E6C
		public EditorVisibleScriptComponentVariable(bool visible)
		{
			this.Visible = visible;
		}
	}
}
