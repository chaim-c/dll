using System;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000146 RID: 326
	[Serializable]
	public class PlayerBattleInfo
	{
		// Token: 0x170002AA RID: 682
		// (get) Token: 0x060008B5 RID: 2229 RVA: 0x0000D048 File Offset: 0x0000B248
		// (set) Token: 0x060008B6 RID: 2230 RVA: 0x0000D050 File Offset: 0x0000B250
		public PlayerId PlayerId { get; set; }

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x060008B7 RID: 2231 RVA: 0x0000D059 File Offset: 0x0000B259
		// (set) Token: 0x060008B8 RID: 2232 RVA: 0x0000D061 File Offset: 0x0000B261
		public string Name { get; set; }

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x060008B9 RID: 2233 RVA: 0x0000D06A File Offset: 0x0000B26A
		// (set) Token: 0x060008BA RID: 2234 RVA: 0x0000D072 File Offset: 0x0000B272
		public int TeamNo { get; set; }

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x060008BB RID: 2235 RVA: 0x0000D07B File Offset: 0x0000B27B
		public bool Fled
		{
			get
			{
				return this._state == PlayerBattleInfo.State.Fled;
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x060008BC RID: 2236 RVA: 0x0000D086 File Offset: 0x0000B286
		public bool Disconnected
		{
			get
			{
				return this._state == PlayerBattleInfo.State.Disconnected;
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x060008BD RID: 2237 RVA: 0x0000D091 File Offset: 0x0000B291
		// (set) Token: 0x060008BE RID: 2238 RVA: 0x0000D099 File Offset: 0x0000B299
		public BattleJoinType JoinType { get; set; }

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x060008BF RID: 2239 RVA: 0x0000D0A2 File Offset: 0x0000B2A2
		// (set) Token: 0x060008C0 RID: 2240 RVA: 0x0000D0AA File Offset: 0x0000B2AA
		public int PeerIndex { get; set; }

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x060008C1 RID: 2241 RVA: 0x0000D0B3 File Offset: 0x0000B2B3
		public PlayerBattleInfo.State CurrentState
		{
			get
			{
				return this._state;
			}
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x0000D0BB File Offset: 0x0000B2BB
		public PlayerBattleInfo()
		{
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x0000D0C3 File Offset: 0x0000B2C3
		public PlayerBattleInfo(PlayerId playerId, string name, int teamNo)
		{
			this.PlayerId = playerId;
			this.Name = name;
			this.TeamNo = teamNo;
			this.PeerIndex = -1;
			this._state = PlayerBattleInfo.State.AssignedToBattle;
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x0000D0EE File Offset: 0x0000B2EE
		public PlayerBattleInfo(PlayerId playerId, string name, int teamNo, int peerIndex, PlayerBattleInfo.State state)
		{
			this.PlayerId = playerId;
			this.Name = name;
			this.TeamNo = teamNo;
			this.PeerIndex = peerIndex;
			this._state = state;
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x0000D11B File Offset: 0x0000B31B
		public void Flee()
		{
			if (this._state != PlayerBattleInfo.State.Disconnected && this._state != PlayerBattleInfo.State.AtBattle)
			{
				throw new Exception("PlayerBattleInfo incorrect state, expected AtBattle or Disconnected; got " + this._state);
			}
			this._state = PlayerBattleInfo.State.Fled;
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x0000D151 File Offset: 0x0000B351
		public void Disconnect()
		{
			if (this._state != PlayerBattleInfo.State.AtBattle)
			{
				throw new Exception("PlayerBattleInfo incorrect state, expected AtBattle got " + this._state);
			}
			this._state = PlayerBattleInfo.State.Disconnected;
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x0000D17E File Offset: 0x0000B37E
		public void Initialize(int peerIndex)
		{
			if (this._state != PlayerBattleInfo.State.AssignedToBattle)
			{
				throw new Exception("PlayerBattleInfo incorrect state, expected AssignedToBattle got " + this._state);
			}
			this.PeerIndex = peerIndex;
			this._state = PlayerBattleInfo.State.AtBattle;
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x0000D1B2 File Offset: 0x0000B3B2
		public void RejoinBattle(int teamNo)
		{
			if (this._state != PlayerBattleInfo.State.Disconnected)
			{
				throw new Exception("PlayerBattleInfo incorrect state, expected Fled got " + this._state);
			}
			this.TeamNo = teamNo;
			this.PeerIndex = -1;
			this._state = PlayerBattleInfo.State.AssignedToBattle;
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x0000D1ED File Offset: 0x0000B3ED
		public PlayerBattleInfo Clone()
		{
			return new PlayerBattleInfo(this.PlayerId, this.Name, this.TeamNo, this.PeerIndex, this._state);
		}

		// Token: 0x0400039E RID: 926
		private PlayerBattleInfo.State _state;

		// Token: 0x020001DB RID: 475
		public enum State
		{
			// Token: 0x040006C2 RID: 1730
			Created,
			// Token: 0x040006C3 RID: 1731
			AssignedToBattle,
			// Token: 0x040006C4 RID: 1732
			AtBattle,
			// Token: 0x040006C5 RID: 1733
			Disconnected,
			// Token: 0x040006C6 RID: 1734
			Fled
		}
	}
}
