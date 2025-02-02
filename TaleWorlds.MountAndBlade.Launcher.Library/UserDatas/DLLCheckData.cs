using System;

namespace TaleWorlds.MountAndBlade.Launcher.Library.UserDatas
{
	// Token: 0x0200001D RID: 29
	public class DLLCheckData
	{
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00005926 File Offset: 0x00003B26
		// (set) Token: 0x06000122 RID: 290 RVA: 0x0000592E File Offset: 0x00003B2E
		public string DLLName { get; set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00005937 File Offset: 0x00003B37
		// (set) Token: 0x06000124 RID: 292 RVA: 0x0000593F File Offset: 0x00003B3F
		public string DLLVerifyInformation { get; set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00005948 File Offset: 0x00003B48
		// (set) Token: 0x06000126 RID: 294 RVA: 0x00005950 File Offset: 0x00003B50
		public uint LatestSizeInBytes { get; set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00005959 File Offset: 0x00003B59
		// (set) Token: 0x06000128 RID: 296 RVA: 0x00005961 File Offset: 0x00003B61
		public bool IsDangerous { get; set; }

		// Token: 0x06000129 RID: 297 RVA: 0x0000596A File Offset: 0x00003B6A
		public DLLCheckData(string dllname)
		{
			this.LatestSizeInBytes = 0U;
			this.IsDangerous = true;
			this.DLLName = dllname;
			this.DLLVerifyInformation = "";
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00005992 File Offset: 0x00003B92
		public DLLCheckData()
		{
		}
	}
}
