using System;
using System.Collections.Generic;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x020000F2 RID: 242
	public class BattlePeer
	{
		// Token: 0x17000178 RID: 376
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x00005438 File Offset: 0x00003638
		// (set) Token: 0x0600049D RID: 1181 RVA: 0x00005440 File Offset: 0x00003640
		public int Index { get; private set; }

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x00005449 File Offset: 0x00003649
		// (set) Token: 0x0600049F RID: 1183 RVA: 0x00005451 File Offset: 0x00003651
		public string Name { get; private set; }

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x0000545A File Offset: 0x0000365A
		public PlayerId PlayerId
		{
			get
			{
				return this.PlayerData.PlayerId;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x00005467 File Offset: 0x00003667
		// (set) Token: 0x060004A2 RID: 1186 RVA: 0x0000546F File Offset: 0x0000366F
		public int TeamNo { get; private set; }

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x00005478 File Offset: 0x00003678
		// (set) Token: 0x060004A4 RID: 1188 RVA: 0x00005480 File Offset: 0x00003680
		public BattleJoinType BattleJoinType { get; private set; }

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060004A5 RID: 1189 RVA: 0x00005489 File Offset: 0x00003689
		public bool Quit
		{
			get
			{
				return this.QuitType > BattlePeerQuitType.None;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x00005494 File Offset: 0x00003694
		// (set) Token: 0x060004A7 RID: 1191 RVA: 0x0000549C File Offset: 0x0000369C
		public PlayerData PlayerData { get; private set; }

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x000054A5 File Offset: 0x000036A5
		// (set) Token: 0x060004A9 RID: 1193 RVA: 0x000054AD File Offset: 0x000036AD
		public Dictionary<string, List<string>> UsedCosmetics { get; private set; }

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x000054B6 File Offset: 0x000036B6
		// (set) Token: 0x060004AB RID: 1195 RVA: 0x000054BE File Offset: 0x000036BE
		public int SessionKey { get; private set; }

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x000054C7 File Offset: 0x000036C7
		// (set) Token: 0x060004AD RID: 1197 RVA: 0x000054CF File Offset: 0x000036CF
		public BattlePeerQuitType QuitType { get; private set; }

		// Token: 0x060004AE RID: 1198 RVA: 0x000054D8 File Offset: 0x000036D8
		public BattlePeer(string name, PlayerData playerData, Dictionary<string, List<string>> usedCosmetics, int teamNo, BattleJoinType battleJoinType)
		{
			this.Index = -1;
			this.Name = name;
			this.PlayerData = playerData;
			this.UsedCosmetics = usedCosmetics;
			this.TeamNo = teamNo;
			this.BattleJoinType = battleJoinType;
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x0000550C File Offset: 0x0000370C
		internal void Flee()
		{
			this.QuitType = BattlePeerQuitType.Fled;
			this.Index = -1;
			this.SessionKey = 0;
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x00005523 File Offset: 0x00003723
		internal void SetPlayerDisconnectdFromLobby()
		{
			this.QuitType = BattlePeerQuitType.DisconnectedFromLobby;
			this.Index = -1;
			this.SessionKey = 0;
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x0000553A File Offset: 0x0000373A
		internal void SetPlayerDisconnectdFromGameSession()
		{
			this.QuitType = BattlePeerQuitType.DisconnectedFromGameSession;
			this.Index = -1;
			this.SessionKey = 0;
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x00005551 File Offset: 0x00003751
		public void Rejoin(int teamNo)
		{
			this.QuitType = BattlePeerQuitType.None;
			this.TeamNo = teamNo;
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x00005561 File Offset: 0x00003761
		public void InitializeSession(int index, int sessionKey)
		{
			this.Index = index;
			this.SessionKey = sessionKey;
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x00005571 File Offset: 0x00003771
		internal void SetPlayerKickedDueToFriendlyDamage()
		{
			this.QuitType = BattlePeerQuitType.KickedDueToFriendlyDamage;
			this.Index = -1;
			this.SessionKey = 0;
		}
	}
}
