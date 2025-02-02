using System;
using System.Collections.Generic;

namespace TaleWorlds.MountAndBlade.Launcher.Library.UserDatas
{
	// Token: 0x0200001A RID: 26
	public class UserGameTypeData
	{
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000112 RID: 274 RVA: 0x0000584F File Offset: 0x00003A4F
		// (set) Token: 0x06000113 RID: 275 RVA: 0x00005857 File Offset: 0x00003A57
		public List<UserModData> ModDatas { get; set; }

		// Token: 0x06000114 RID: 276 RVA: 0x00005860 File Offset: 0x00003A60
		public UserGameTypeData()
		{
			this.ModDatas = new List<UserModData>();
		}
	}
}
