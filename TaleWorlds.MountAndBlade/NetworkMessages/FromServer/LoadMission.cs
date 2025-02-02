using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x0200008F RID: 143
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class LoadMission : GameNetworkMessage
	{
		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060005A7 RID: 1447 RVA: 0x0000A40D File Offset: 0x0000860D
		// (set) Token: 0x060005A8 RID: 1448 RVA: 0x0000A415 File Offset: 0x00008615
		public string GameType { get; private set; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060005A9 RID: 1449 RVA: 0x0000A41E File Offset: 0x0000861E
		// (set) Token: 0x060005AA RID: 1450 RVA: 0x0000A426 File Offset: 0x00008626
		public string Map { get; private set; }

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060005AB RID: 1451 RVA: 0x0000A42F File Offset: 0x0000862F
		// (set) Token: 0x060005AC RID: 1452 RVA: 0x0000A437 File Offset: 0x00008637
		public int BattleIndex { get; private set; }

		// Token: 0x060005AD RID: 1453 RVA: 0x0000A440 File Offset: 0x00008640
		public LoadMission(string gameType, string map, int battleIndex)
		{
			this.GameType = gameType;
			this.Map = map;
			this.BattleIndex = battleIndex;
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x0000A45D File Offset: 0x0000865D
		public LoadMission()
		{
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x0000A468 File Offset: 0x00008668
		protected override bool OnRead()
		{
			bool result = true;
			this.GameType = GameNetworkMessage.ReadStringFromPacket(ref result);
			this.Map = GameNetworkMessage.ReadStringFromPacket(ref result);
			this.BattleIndex = GameNetworkMessage.ReadIntFromPacket(CompressionMission.AutomatedBattleIndexCompressionInfo, ref result);
			return result;
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x0000A4A4 File Offset: 0x000086A4
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteStringToPacket(this.GameType);
			GameNetworkMessage.WriteStringToPacket(this.Map);
			GameNetworkMessage.WriteIntToPacket(this.BattleIndex, CompressionMission.AutomatedBattleIndexCompressionInfo);
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x0000A4CC File Offset: 0x000086CC
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Mission;
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x0000A4D4 File Offset: 0x000086D4
		protected override string OnGetLogFormat()
		{
			return "Load Mission";
		}
	}
}
