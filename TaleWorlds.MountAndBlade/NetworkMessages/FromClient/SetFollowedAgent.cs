using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromClient
{
	// Token: 0x02000035 RID: 53
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromClient)]
	public sealed class SetFollowedAgent : GameNetworkMessage
	{
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00003E39 File Offset: 0x00002039
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x00003E41 File Offset: 0x00002041
		public int AgentIndex { get; private set; }

		// Token: 0x060001A4 RID: 420 RVA: 0x00003E4A File Offset: 0x0000204A
		public SetFollowedAgent(int agentIndex)
		{
			this.AgentIndex = agentIndex;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00003E59 File Offset: 0x00002059
		public SetFollowedAgent()
		{
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00003E64 File Offset: 0x00002064
		protected override bool OnRead()
		{
			bool result = true;
			this.AgentIndex = GameNetworkMessage.ReadAgentIndexFromPacket(ref result);
			return result;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00003E81 File Offset: 0x00002081
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteAgentIndexToPacket(this.AgentIndex);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00003E8E File Offset: 0x0000208E
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.MissionDetailed;
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00003E96 File Offset: 0x00002096
		protected override string OnGetLogFormat()
		{
			return "Peer switched spectating an agent";
		}
	}
}
