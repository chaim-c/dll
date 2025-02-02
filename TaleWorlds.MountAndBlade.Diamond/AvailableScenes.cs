using System;
using System.Collections.Generic;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x020000EF RID: 239
	[Serializable]
	public class AvailableScenes
	{
		// Token: 0x17000172 RID: 370
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x0000528C File Offset: 0x0000348C
		// (set) Token: 0x0600048B RID: 1163 RVA: 0x00005293 File Offset: 0x00003493
		public static AvailableScenes Empty { get; private set; } = new AvailableScenes(new Dictionary<string, string[]>());

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x000052AC File Offset: 0x000034AC
		// (set) Token: 0x0600048E RID: 1166 RVA: 0x000052B4 File Offset: 0x000034B4
		public Dictionary<string, string[]> ScenesByGameTypes { get; set; }

		// Token: 0x0600048F RID: 1167 RVA: 0x000052BD File Offset: 0x000034BD
		public AvailableScenes()
		{
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x000052C5 File Offset: 0x000034C5
		public AvailableScenes(Dictionary<string, string[]> scenesByGameTypes)
		{
			this.ScenesByGameTypes = scenesByGameTypes;
		}
	}
}
