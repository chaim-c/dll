using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromCustomBattleServer.ToCustomBattleServerManager
{
	// Token: 0x02000063 RID: 99
	[MessageDescription("CustomBattleServer", "CustomBattleServerManager")]
	[Serializable]
	public class PlayerDisconnectedMessage : Message
	{
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00003507 File Offset: 0x00001707
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x0000350F File Offset: 0x0000170F
		[JsonProperty]
		public PlayerId PlayerId { get; private set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x00003518 File Offset: 0x00001718
		// (set) Token: 0x060001E9 RID: 489 RVA: 0x00003520 File Offset: 0x00001720
		[JsonProperty]
		public DisconnectType Type { get; private set; }

		// Token: 0x060001EA RID: 490 RVA: 0x00003529 File Offset: 0x00001729
		public PlayerDisconnectedMessage()
		{
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00003531 File Offset: 0x00001731
		public PlayerDisconnectedMessage(PlayerId playerId, DisconnectType type)
		{
			this.PlayerId = playerId;
			this.Type = type;
		}
	}
}
