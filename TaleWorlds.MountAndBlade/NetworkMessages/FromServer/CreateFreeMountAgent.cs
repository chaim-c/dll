using System;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x02000083 RID: 131
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class CreateFreeMountAgent : GameNetworkMessage
	{
		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000502 RID: 1282 RVA: 0x0000926D File Offset: 0x0000746D
		// (set) Token: 0x06000503 RID: 1283 RVA: 0x00009275 File Offset: 0x00007475
		public int AgentIndex { get; private set; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000504 RID: 1284 RVA: 0x0000927E File Offset: 0x0000747E
		// (set) Token: 0x06000505 RID: 1285 RVA: 0x00009286 File Offset: 0x00007486
		public EquipmentElement HorseItem { get; private set; }

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000506 RID: 1286 RVA: 0x0000928F File Offset: 0x0000748F
		// (set) Token: 0x06000507 RID: 1287 RVA: 0x00009297 File Offset: 0x00007497
		public EquipmentElement HorseHarnessItem { get; private set; }

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000508 RID: 1288 RVA: 0x000092A0 File Offset: 0x000074A0
		// (set) Token: 0x06000509 RID: 1289 RVA: 0x000092A8 File Offset: 0x000074A8
		public Vec3 Position { get; private set; }

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600050A RID: 1290 RVA: 0x000092B1 File Offset: 0x000074B1
		// (set) Token: 0x0600050B RID: 1291 RVA: 0x000092B9 File Offset: 0x000074B9
		public Vec2 Direction { get; private set; }

		// Token: 0x0600050C RID: 1292 RVA: 0x000092C4 File Offset: 0x000074C4
		public CreateFreeMountAgent(Agent agent, Vec3 position, Vec2 direction)
		{
			this.AgentIndex = agent.Index;
			this.HorseItem = agent.SpawnEquipment.GetEquipmentFromSlot(EquipmentIndex.ArmorItemEndSlot);
			this.HorseHarnessItem = agent.SpawnEquipment.GetEquipmentFromSlot(EquipmentIndex.HorseHarness);
			this.Position = position;
			this.Direction = direction.Normalized();
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0000931D File Offset: 0x0000751D
		public CreateFreeMountAgent()
		{
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x00009328 File Offset: 0x00007528
		protected override bool OnRead()
		{
			bool result = true;
			this.AgentIndex = GameNetworkMessage.ReadAgentIndexFromPacket(ref result);
			this.HorseItem = ModuleNetworkData.ReadItemReferenceFromPacket(Game.Current.ObjectManager, ref result);
			this.HorseHarnessItem = ModuleNetworkData.ReadItemReferenceFromPacket(Game.Current.ObjectManager, ref result);
			this.Position = GameNetworkMessage.ReadVec3FromPacket(CompressionBasic.PositionCompressionInfo, ref result);
			this.Direction = GameNetworkMessage.ReadVec2FromPacket(CompressionBasic.UnitVectorCompressionInfo, ref result);
			return result;
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x00009398 File Offset: 0x00007598
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteAgentIndexToPacket(this.AgentIndex);
			ModuleNetworkData.WriteItemReferenceToPacket(this.HorseItem);
			ModuleNetworkData.WriteItemReferenceToPacket(this.HorseHarnessItem);
			GameNetworkMessage.WriteVec3ToPacket(this.Position, CompressionBasic.PositionCompressionInfo);
			GameNetworkMessage.WriteVec2ToPacket(this.Direction, CompressionBasic.UnitVectorCompressionInfo);
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x000093E6 File Offset: 0x000075E6
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Agents;
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x000093EE File Offset: 0x000075EE
		protected override string OnGetLogFormat()
		{
			return "Create a mount-agent with index: " + this.AgentIndex;
		}
	}
}
