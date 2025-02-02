using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromClient
{
	// Token: 0x02000033 RID: 51
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromClient)]
	public sealed class SelectFormation : GameNetworkMessage
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00003D50 File Offset: 0x00001F50
		// (set) Token: 0x06000193 RID: 403 RVA: 0x00003D58 File Offset: 0x00001F58
		public int FormationIndex { get; private set; }

		// Token: 0x06000194 RID: 404 RVA: 0x00003D61 File Offset: 0x00001F61
		public SelectFormation(int formationIndex)
		{
			this.FormationIndex = formationIndex;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00003D70 File Offset: 0x00001F70
		public SelectFormation()
		{
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00003D78 File Offset: 0x00001F78
		protected override bool OnRead()
		{
			bool result = true;
			this.FormationIndex = GameNetworkMessage.ReadIntFromPacket(CompressionMission.FormationClassCompressionInfo, ref result);
			return result;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00003D9A File Offset: 0x00001F9A
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteIntToPacket(this.FormationIndex, CompressionMission.FormationClassCompressionInfo);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00003DAC File Offset: 0x00001FAC
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Formations;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00003DB1 File Offset: 0x00001FB1
		protected override string OnGetLogFormat()
		{
			return "Select Formation with ID: " + this.FormationIndex;
		}
	}
}
