using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x0200006C RID: 108
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class RoundStateChange : GameNetworkMessage
	{
		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060003C5 RID: 965 RVA: 0x0000727B File Offset: 0x0000547B
		// (set) Token: 0x060003C6 RID: 966 RVA: 0x00007283 File Offset: 0x00005483
		public MultiplayerRoundState RoundState { get; private set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x0000728C File Offset: 0x0000548C
		// (set) Token: 0x060003C8 RID: 968 RVA: 0x00007294 File Offset: 0x00005494
		public float StateStartTimeInSeconds { get; private set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x0000729D File Offset: 0x0000549D
		// (set) Token: 0x060003CA RID: 970 RVA: 0x000072A5 File Offset: 0x000054A5
		public int RemainingTimeOnPreviousState { get; private set; }

		// Token: 0x060003CB RID: 971 RVA: 0x000072AE File Offset: 0x000054AE
		public RoundStateChange(MultiplayerRoundState roundState, long stateStartTimeInTicks, int remainingTimeOnPreviousState)
		{
			this.RoundState = roundState;
			this.StateStartTimeInSeconds = (float)stateStartTimeInTicks / 10000000f;
			this.RemainingTimeOnPreviousState = remainingTimeOnPreviousState;
		}

		// Token: 0x060003CC RID: 972 RVA: 0x000072D2 File Offset: 0x000054D2
		public RoundStateChange()
		{
		}

		// Token: 0x060003CD RID: 973 RVA: 0x000072DC File Offset: 0x000054DC
		protected override bool OnRead()
		{
			bool result = true;
			this.RoundState = (MultiplayerRoundState)GameNetworkMessage.ReadIntFromPacket(CompressionMission.MissionRoundStateCompressionInfo, ref result);
			this.StateStartTimeInSeconds = GameNetworkMessage.ReadFloatFromPacket(CompressionMatchmaker.MissionTimeCompressionInfo, ref result);
			this.RemainingTimeOnPreviousState = GameNetworkMessage.ReadIntFromPacket(CompressionMission.RoundTimeCompressionInfo, ref result);
			return result;
		}

		// Token: 0x060003CE RID: 974 RVA: 0x00007322 File Offset: 0x00005522
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteIntToPacket((int)this.RoundState, CompressionMission.MissionRoundStateCompressionInfo);
			GameNetworkMessage.WriteFloatToPacket(this.StateStartTimeInSeconds, CompressionMatchmaker.MissionTimeCompressionInfo);
			GameNetworkMessage.WriteIntToPacket(this.RemainingTimeOnPreviousState, CompressionMission.RoundTimeCompressionInfo);
		}

		// Token: 0x060003CF RID: 975 RVA: 0x00007354 File Offset: 0x00005554
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Mission;
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0000735C File Offset: 0x0000555C
		protected override string OnGetLogFormat()
		{
			return "Changing round state to: " + this.RoundState;
		}
	}
}
