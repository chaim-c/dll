using System;
using System.Collections.Generic;

namespace TaleWorlds.MountAndBlade.Launcher.Library.UserDatas
{
	// Token: 0x0200001C RID: 28
	public class DLLCheckDataCollection
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00005902 File Offset: 0x00003B02
		// (set) Token: 0x0600011F RID: 287 RVA: 0x0000590A File Offset: 0x00003B0A
		public List<DLLCheckData> DLLData { get; set; }

		// Token: 0x06000120 RID: 288 RVA: 0x00005913 File Offset: 0x00003B13
		public DLLCheckDataCollection()
		{
			this.DLLData = new List<DLLCheckData>();
		}
	}
}
