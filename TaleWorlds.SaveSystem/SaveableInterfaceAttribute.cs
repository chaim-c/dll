using System;

namespace TaleWorlds.SaveSystem
{
	// Token: 0x02000008 RID: 8
	[AttributeUsage(AttributeTargets.Interface)]
	public class SaveableInterfaceAttribute : Attribute
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000021F7 File Offset: 0x000003F7
		// (set) Token: 0x06000013 RID: 19 RVA: 0x000021FF File Offset: 0x000003FF
		public int SaveId { get; set; }

		// Token: 0x06000014 RID: 20 RVA: 0x00002208 File Offset: 0x00000408
		public SaveableInterfaceAttribute(int saveId)
		{
			this.SaveId = saveId;
		}
	}
}
