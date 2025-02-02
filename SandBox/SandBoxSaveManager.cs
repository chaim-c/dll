using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.MountAndBlade;

namespace SandBox
{
	// Token: 0x02000029 RID: 41
	public class SandBoxSaveManager : ISaveManager
	{
		// Token: 0x0600010F RID: 271 RVA: 0x00007A38 File Offset: 0x00005C38
		public int GetAutoSaveInterval()
		{
			return BannerlordConfig.AutoSaveInterval;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00007A3F File Offset: 0x00005C3F
		public void OnSaveOver(bool isSuccessful, string newSaveGameName)
		{
			if (isSuccessful)
			{
				BannerlordConfig.LatestSaveGameName = newSaveGameName;
				BannerlordConfig.Save();
			}
		}
	}
}
