using System;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x02000094 RID: 148
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class RemoveEquippedWeapon : GameNetworkMessage
	{
		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x0000A7E2 File Offset: 0x000089E2
		// (set) Token: 0x060005DC RID: 1500 RVA: 0x0000A7EA File Offset: 0x000089EA
		public EquipmentIndex SlotIndex { get; private set; }

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060005DD RID: 1501 RVA: 0x0000A7F3 File Offset: 0x000089F3
		// (set) Token: 0x060005DE RID: 1502 RVA: 0x0000A7FB File Offset: 0x000089FB
		public int AgentIndex { get; private set; }

		// Token: 0x060005DF RID: 1503 RVA: 0x0000A804 File Offset: 0x00008A04
		public RemoveEquippedWeapon(int agentIndex, EquipmentIndex slot)
		{
			this.AgentIndex = agentIndex;
			this.SlotIndex = slot;
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x0000A81A File Offset: 0x00008A1A
		public RemoveEquippedWeapon()
		{
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x0000A822 File Offset: 0x00008A22
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteAgentIndexToPacket(this.AgentIndex);
			GameNetworkMessage.WriteIntToPacket((int)this.SlotIndex, CompressionMission.ItemSlotCompressionInfo);
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0000A840 File Offset: 0x00008A40
		protected override bool OnRead()
		{
			bool result = true;
			this.AgentIndex = GameNetworkMessage.ReadAgentIndexFromPacket(ref result);
			this.SlotIndex = (EquipmentIndex)GameNetworkMessage.ReadIntFromPacket(CompressionMission.ItemSlotCompressionInfo, ref result);
			return result;
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x0000A86F File Offset: 0x00008A6F
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Items | MultiplayerMessageFilter.AgentsDetailed;
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x0000A877 File Offset: 0x00008A77
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Remove equipped weapon from SlotIndex: ",
				this.SlotIndex,
				" on agent with agent-index: ",
				this.AgentIndex
			});
		}
	}
}
