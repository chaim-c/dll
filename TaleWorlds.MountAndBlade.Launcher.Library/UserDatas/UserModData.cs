using System;
using TaleWorlds.ModuleManager;

namespace TaleWorlds.MountAndBlade.Launcher.Library.UserDatas
{
	// Token: 0x0200001B RID: 27
	public class UserModData
	{
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00005873 File Offset: 0x00003A73
		// (set) Token: 0x06000116 RID: 278 RVA: 0x0000587B File Offset: 0x00003A7B
		public string Id { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000117 RID: 279 RVA: 0x00005884 File Offset: 0x00003A84
		// (set) Token: 0x06000118 RID: 280 RVA: 0x0000588C File Offset: 0x00003A8C
		public string LastKnownVersion { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00005895 File Offset: 0x00003A95
		// (set) Token: 0x0600011A RID: 282 RVA: 0x0000589D File Offset: 0x00003A9D
		public bool IsSelected { get; set; }

		// Token: 0x0600011B RID: 283 RVA: 0x000058A6 File Offset: 0x00003AA6
		public UserModData()
		{
		}

		// Token: 0x0600011C RID: 284 RVA: 0x000058AE File Offset: 0x00003AAE
		public UserModData(string id, string lastKnownVersion, bool isSelected)
		{
			this.Id = id;
			this.LastKnownVersion = lastKnownVersion;
			this.IsSelected = isSelected;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x000058CC File Offset: 0x00003ACC
		public bool IsUpdatedToBeDefault(ModuleInfo module)
		{
			return this.LastKnownVersion != module.Version.ToString() && module.IsDefault;
		}
	}
}
