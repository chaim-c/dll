using System;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x0200012F RID: 303
	[Serializable]
	public class AnotherPlayerData
	{
		// Token: 0x1700026A RID: 618
		// (get) Token: 0x060007C7 RID: 1991 RVA: 0x0000BECB File Offset: 0x0000A0CB
		// (set) Token: 0x060007C8 RID: 1992 RVA: 0x0000BED3 File Offset: 0x0000A0D3
		public AnotherPlayerState PlayerState { get; set; }

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x060007C9 RID: 1993 RVA: 0x0000BEDC File Offset: 0x0000A0DC
		// (set) Token: 0x060007CA RID: 1994 RVA: 0x0000BEE4 File Offset: 0x0000A0E4
		public int Experience { get; set; }

		// Token: 0x060007CB RID: 1995 RVA: 0x0000BEED File Offset: 0x0000A0ED
		public AnotherPlayerData()
		{
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x0000BEF5 File Offset: 0x0000A0F5
		public AnotherPlayerData(AnotherPlayerState anotherPlayerState, int anotherPlayerExperience)
		{
			this.PlayerState = anotherPlayerState;
			this.Experience = anotherPlayerExperience;
		}
	}
}
