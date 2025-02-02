using System;

namespace TaleWorlds.SaveSystem
{
	// Token: 0x02000007 RID: 7
	[AttributeUsage(AttributeTargets.Field)]
	public class SaveableFieldAttribute : Attribute
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000021D7 File Offset: 0x000003D7
		// (set) Token: 0x06000010 RID: 16 RVA: 0x000021DF File Offset: 0x000003DF
		public short LocalSaveId { get; set; }

		// Token: 0x06000011 RID: 17 RVA: 0x000021E8 File Offset: 0x000003E8
		public SaveableFieldAttribute(short localSaveId)
		{
			this.LocalSaveId = localSaveId;
		}
	}
}
