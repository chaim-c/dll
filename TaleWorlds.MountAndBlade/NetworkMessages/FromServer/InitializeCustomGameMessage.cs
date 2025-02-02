using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x0200008D RID: 141
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class InitializeCustomGameMessage : GameNetworkMessage
	{
		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600058D RID: 1421 RVA: 0x0000A200 File Offset: 0x00008400
		// (set) Token: 0x0600058E RID: 1422 RVA: 0x0000A208 File Offset: 0x00008408
		public bool InMission { get; private set; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600058F RID: 1423 RVA: 0x0000A211 File Offset: 0x00008411
		// (set) Token: 0x06000590 RID: 1424 RVA: 0x0000A219 File Offset: 0x00008419
		public string GameType { get; private set; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000591 RID: 1425 RVA: 0x0000A222 File Offset: 0x00008422
		// (set) Token: 0x06000592 RID: 1426 RVA: 0x0000A22A File Offset: 0x0000842A
		public string Map { get; private set; }

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x0000A233 File Offset: 0x00008433
		// (set) Token: 0x06000594 RID: 1428 RVA: 0x0000A23B File Offset: 0x0000843B
		public int BattleIndex { get; private set; }

		// Token: 0x06000595 RID: 1429 RVA: 0x0000A244 File Offset: 0x00008444
		public InitializeCustomGameMessage(bool inMission, string gameType, string map, int battleIndex)
		{
			this.InMission = inMission;
			this.GameType = gameType;
			this.Map = map;
			this.BattleIndex = battleIndex;
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0000A269 File Offset: 0x00008469
		public InitializeCustomGameMessage()
		{
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x0000A274 File Offset: 0x00008474
		protected override bool OnRead()
		{
			bool result = true;
			this.InMission = GameNetworkMessage.ReadBoolFromPacket(ref result);
			this.GameType = GameNetworkMessage.ReadStringFromPacket(ref result);
			this.Map = GameNetworkMessage.ReadStringFromPacket(ref result);
			this.BattleIndex = GameNetworkMessage.ReadIntFromPacket(CompressionMission.AutomatedBattleIndexCompressionInfo, ref result);
			return result;
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x0000A2BD File Offset: 0x000084BD
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteBoolToPacket(this.InMission);
			GameNetworkMessage.WriteStringToPacket(this.GameType);
			GameNetworkMessage.WriteStringToPacket(this.Map);
			GameNetworkMessage.WriteIntToPacket(this.BattleIndex, CompressionMission.AutomatedBattleIndexCompressionInfo);
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x0000A2F0 File Offset: 0x000084F0
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Mission;
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x0000A2F8 File Offset: 0x000084F8
		protected override string OnGetLogFormat()
		{
			return "Initialize Custom Game";
		}
	}
}
