using System;

namespace TaleWorlds.DotNet
{
	// Token: 0x0200000E RID: 14
	public class EditableScriptComponentVariable : Attribute
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002D84 File Offset: 0x00000F84
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00002D8C File Offset: 0x00000F8C
		public bool Visible { get; set; }

		// Token: 0x06000040 RID: 64 RVA: 0x00002D95 File Offset: 0x00000F95
		public EditableScriptComponentVariable(bool visible)
		{
			this.Visible = visible;
		}
	}
}
