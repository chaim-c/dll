using System;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.CampaignSystem
{
	// Token: 0x02000082 RID: 130
	public struct MeetingSceneData
	{
		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x0600105A RID: 4186 RVA: 0x0004A5F0 File Offset: 0x000487F0
		// (set) Token: 0x0600105B RID: 4187 RVA: 0x0004A5F8 File Offset: 0x000487F8
		public string SceneID { get; private set; }

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x0600105C RID: 4188 RVA: 0x0004A601 File Offset: 0x00048801
		// (set) Token: 0x0600105D RID: 4189 RVA: 0x0004A609 File Offset: 0x00048809
		public string CultureString { get; private set; }

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x0600105E RID: 4190 RVA: 0x0004A612 File Offset: 0x00048812
		public CultureObject Culture
		{
			get
			{
				return MBObjectManager.Instance.GetObject<CultureObject>(this.CultureString);
			}
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x0004A624 File Offset: 0x00048824
		public MeetingSceneData(string sceneID, string cultureString)
		{
			this.SceneID = sceneID;
			this.CultureString = cultureString;
		}
	}
}
