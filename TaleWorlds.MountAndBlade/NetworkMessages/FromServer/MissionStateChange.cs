using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x02000057 RID: 87
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class MissionStateChange : GameNetworkMessage
	{
		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000312 RID: 786 RVA: 0x00005F11 File Offset: 0x00004111
		// (set) Token: 0x06000313 RID: 787 RVA: 0x00005F19 File Offset: 0x00004119
		public MissionLobbyComponent.MultiplayerGameState CurrentState { get; private set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000314 RID: 788 RVA: 0x00005F22 File Offset: 0x00004122
		// (set) Token: 0x06000315 RID: 789 RVA: 0x00005F2A File Offset: 0x0000412A
		public float StateStartTimeInSeconds { get; private set; }

		// Token: 0x06000316 RID: 790 RVA: 0x00005F33 File Offset: 0x00004133
		public MissionStateChange(MissionLobbyComponent.MultiplayerGameState currentState, long stateStartTimeInTicks)
		{
			this.CurrentState = currentState;
			this.StateStartTimeInSeconds = (float)stateStartTimeInTicks / 10000000f;
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00005F50 File Offset: 0x00004150
		public MissionStateChange()
		{
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00005F58 File Offset: 0x00004158
		protected override bool OnRead()
		{
			bool result = true;
			this.CurrentState = (MissionLobbyComponent.MultiplayerGameState)GameNetworkMessage.ReadIntFromPacket(CompressionMatchmaker.MissionCurrentStateCompressionInfo, ref result);
			if (this.CurrentState != MissionLobbyComponent.MultiplayerGameState.WaitingFirstPlayers)
			{
				this.StateStartTimeInSeconds = GameNetworkMessage.ReadFloatFromPacket(CompressionMatchmaker.MissionTimeCompressionInfo, ref result);
			}
			return result;
		}

		// Token: 0x06000319 RID: 793 RVA: 0x00005F94 File Offset: 0x00004194
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteIntToPacket((int)this.CurrentState, CompressionMatchmaker.MissionCurrentStateCompressionInfo);
			if (this.CurrentState != MissionLobbyComponent.MultiplayerGameState.WaitingFirstPlayers)
			{
				GameNetworkMessage.WriteFloatToPacket(this.StateStartTimeInSeconds, CompressionMatchmaker.MissionTimeCompressionInfo);
			}
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00005FBE File Offset: 0x000041BE
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Mission;
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00005FC6 File Offset: 0x000041C6
		protected override string OnGetLogFormat()
		{
			return "Mission State has changed to: " + this.CurrentState;
		}
	}
}
