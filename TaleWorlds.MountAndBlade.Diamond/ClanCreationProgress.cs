using System;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000105 RID: 261
	[Serializable]
	public class ClanCreationProgress
	{
		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x000073E4 File Offset: 0x000055E4
		public Progress Progress
		{
			get
			{
				int num = 0;
				int num2 = 0;
				foreach (ClanCreationPlayerData clanCreationPlayerData2 in this.ClanCreationPlayerData)
				{
					if (clanCreationPlayerData2.ClanCreationAnswer == ClanCreationAnswer.Accepted)
					{
						num++;
					}
					else if (clanCreationPlayerData2.ClanCreationAnswer == ClanCreationAnswer.Declined)
					{
						num2++;
					}
				}
				if (num == this.ClanCreationPlayerData.Length)
				{
					return Progress.Success;
				}
				if (num2 > 0)
				{
					return Progress.Fail;
				}
				return Progress.Undecided;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x0600058A RID: 1418 RVA: 0x00007441 File Offset: 0x00005641
		// (set) Token: 0x0600058B RID: 1419 RVA: 0x00007449 File Offset: 0x00005649
		public ClanCreationPlayerData[] ClanCreationPlayerData { get; private set; }

		// Token: 0x0600058C RID: 1420 RVA: 0x00007452 File Offset: 0x00005652
		public ClanCreationProgress(ClanCreationPlayerData[] clanCreationPlayerData)
		{
			this.ClanCreationPlayerData = clanCreationPlayerData;
		}
	}
}
