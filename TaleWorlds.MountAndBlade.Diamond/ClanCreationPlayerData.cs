using System;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000106 RID: 262
	[Serializable]
	public class ClanCreationPlayerData
	{
		// Token: 0x170001CD RID: 461
		// (get) Token: 0x0600058D RID: 1421 RVA: 0x00007461 File Offset: 0x00005661
		// (set) Token: 0x0600058E RID: 1422 RVA: 0x00007469 File Offset: 0x00005669
		public PlayerSessionId PlayerSessionId { get; private set; }

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x0600058F RID: 1423 RVA: 0x00007472 File Offset: 0x00005672
		// (set) Token: 0x06000590 RID: 1424 RVA: 0x0000747A File Offset: 0x0000567A
		public ClanCreationAnswer ClanCreationAnswer { get; private set; }

		// Token: 0x06000591 RID: 1425 RVA: 0x00007483 File Offset: 0x00005683
		public ClanCreationPlayerData(PlayerSessionId playerSessionId, ClanCreationAnswer answer)
		{
			this.PlayerSessionId = playerSessionId;
			this.ClanCreationAnswer = answer;
		}
	}
}
