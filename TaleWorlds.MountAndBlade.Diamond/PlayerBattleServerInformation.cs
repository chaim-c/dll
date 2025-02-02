using System;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000148 RID: 328
	[Serializable]
	public class PlayerBattleServerInformation
	{
		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x060008CA RID: 2250 RVA: 0x0000D212 File Offset: 0x0000B412
		// (set) Token: 0x060008CB RID: 2251 RVA: 0x0000D21A File Offset: 0x0000B41A
		public int PeerIndex { get; set; }

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x060008CC RID: 2252 RVA: 0x0000D223 File Offset: 0x0000B423
		// (set) Token: 0x060008CD RID: 2253 RVA: 0x0000D22B File Offset: 0x0000B42B
		public int SessionKey { get; set; }

		// Token: 0x060008CE RID: 2254 RVA: 0x0000D234 File Offset: 0x0000B434
		public PlayerBattleServerInformation()
		{
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x0000D23C File Offset: 0x0000B43C
		public PlayerBattleServerInformation(int peerIndex, int sessionKey)
		{
			this.PeerIndex = peerIndex;
			this.SessionKey = sessionKey;
		}
	}
}
