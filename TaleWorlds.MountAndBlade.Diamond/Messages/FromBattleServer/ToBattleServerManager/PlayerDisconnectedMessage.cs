﻿using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromBattleServer.ToBattleServerManager
{
	// Token: 0x020000DE RID: 222
	[MessageDescription("BattleServer", "BattleServerManager")]
	[Serializable]
	public class PlayerDisconnectedMessage : Message
	{
		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000417 RID: 1047 RVA: 0x00004D09 File Offset: 0x00002F09
		// (set) Token: 0x06000418 RID: 1048 RVA: 0x00004D11 File Offset: 0x00002F11
		[JsonProperty]
		public PlayerId PlayerId { get; private set; }

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x00004D1A File Offset: 0x00002F1A
		// (set) Token: 0x0600041A RID: 1050 RVA: 0x00004D22 File Offset: 0x00002F22
		[JsonProperty]
		public DisconnectType Type { get; private set; }

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x00004D2B File Offset: 0x00002F2B
		// (set) Token: 0x0600041C RID: 1052 RVA: 0x00004D33 File Offset: 0x00002F33
		[JsonProperty]
		public bool IsAllowedLeave { get; private set; }

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x00004D3C File Offset: 0x00002F3C
		// (set) Token: 0x0600041E RID: 1054 RVA: 0x00004D44 File Offset: 0x00002F44
		[JsonProperty]
		public BattleResult BattleResult { get; private set; }

		// Token: 0x0600041F RID: 1055 RVA: 0x00004D4D File Offset: 0x00002F4D
		public PlayerDisconnectedMessage()
		{
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00004D55 File Offset: 0x00002F55
		public PlayerDisconnectedMessage(PlayerId playerId, DisconnectType type, bool isAllowedLeave, BattleResult battleResult)
		{
			this.PlayerId = playerId;
			this.Type = type;
			this.IsAllowedLeave = isAllowedLeave;
			this.BattleResult = battleResult;
		}
	}
}
