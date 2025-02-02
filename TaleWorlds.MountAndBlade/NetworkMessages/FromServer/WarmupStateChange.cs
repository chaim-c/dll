using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x020000CE RID: 206
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class WarmupStateChange : GameNetworkMessage
	{
		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000874 RID: 2164 RVA: 0x0000E434 File Offset: 0x0000C634
		// (set) Token: 0x06000875 RID: 2165 RVA: 0x0000E43C File Offset: 0x0000C63C
		public MultiplayerWarmupComponent.WarmupStates WarmupState { get; private set; }

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000876 RID: 2166 RVA: 0x0000E445 File Offset: 0x0000C645
		// (set) Token: 0x06000877 RID: 2167 RVA: 0x0000E44D File Offset: 0x0000C64D
		public float StateStartTimeInSeconds { get; private set; }

		// Token: 0x06000878 RID: 2168 RVA: 0x0000E456 File Offset: 0x0000C656
		public WarmupStateChange(MultiplayerWarmupComponent.WarmupStates warmupState, long stateStartTimeInTicks)
		{
			this.WarmupState = warmupState;
			this.StateStartTimeInSeconds = (float)stateStartTimeInTicks / 10000000f;
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x0000E473 File Offset: 0x0000C673
		public WarmupStateChange()
		{
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x0000E47B File Offset: 0x0000C67B
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteIntToPacket((int)this.WarmupState, CompressionMission.MissionRoundStateCompressionInfo);
			GameNetworkMessage.WriteFloatToPacket(this.StateStartTimeInSeconds, CompressionMatchmaker.MissionTimeCompressionInfo);
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x0000E4A0 File Offset: 0x0000C6A0
		protected override bool OnRead()
		{
			bool result = true;
			this.WarmupState = (MultiplayerWarmupComponent.WarmupStates)GameNetworkMessage.ReadIntFromPacket(CompressionMission.MissionRoundStateCompressionInfo, ref result);
			this.StateStartTimeInSeconds = GameNetworkMessage.ReadFloatFromPacket(CompressionMatchmaker.MissionTimeCompressionInfo, ref result);
			return result;
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x0000E4D4 File Offset: 0x0000C6D4
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.MissionDetailed;
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x0000E4DC File Offset: 0x0000C6DC
		protected override string OnGetLogFormat()
		{
			return "Warmup state set to " + this.WarmupState;
		}
	}
}
