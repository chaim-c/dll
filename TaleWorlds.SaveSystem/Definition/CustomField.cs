using System;

namespace TaleWorlds.SaveSystem.Definition
{
	// Token: 0x0200006B RID: 107
	public class CustomField
	{
		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000345 RID: 837 RVA: 0x0000EA50 File Offset: 0x0000CC50
		// (set) Token: 0x06000346 RID: 838 RVA: 0x0000EA58 File Offset: 0x0000CC58
		public string Name { get; private set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000347 RID: 839 RVA: 0x0000EA61 File Offset: 0x0000CC61
		// (set) Token: 0x06000348 RID: 840 RVA: 0x0000EA69 File Offset: 0x0000CC69
		public short SaveId { get; private set; }

		// Token: 0x06000349 RID: 841 RVA: 0x0000EA72 File Offset: 0x0000CC72
		public CustomField(string name, short saveId)
		{
			this.Name = name;
			this.SaveId = saveId;
		}
	}
}
