using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromClient
{
	// Token: 0x0200000F RID: 15
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromClient)]
	public sealed class RequestTroopIndexChange : GameNetworkMessage
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000063 RID: 99 RVA: 0x000027E0 File Offset: 0x000009E0
		// (set) Token: 0x06000064 RID: 100 RVA: 0x000027E8 File Offset: 0x000009E8
		public int SelectedTroopIndex { get; private set; }

		// Token: 0x06000065 RID: 101 RVA: 0x000027F1 File Offset: 0x000009F1
		public RequestTroopIndexChange(int selectedTroopIndex)
		{
			this.SelectedTroopIndex = selectedTroopIndex;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002800 File Offset: 0x00000A00
		public RequestTroopIndexChange()
		{
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002808 File Offset: 0x00000A08
		protected override bool OnRead()
		{
			bool result = true;
			this.SelectedTroopIndex = GameNetworkMessage.ReadIntFromPacket(CompressionMission.SelectedTroopIndexCompressionInfo, ref result);
			return result;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x0000282A File Offset: 0x00000A2A
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteIntToPacket(this.SelectedTroopIndex, CompressionMission.SelectedTroopIndexCompressionInfo);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x0000283C File Offset: 0x00000A3C
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Equipment;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002841 File Offset: 0x00000A41
		protected override string OnGetLogFormat()
		{
			return "Requesting selected troop change to " + this.SelectedTroopIndex;
		}
	}
}
