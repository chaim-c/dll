using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x02000065 RID: 101
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class SynchronizeMissionTimeTracker : GameNetworkMessage
	{
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000384 RID: 900 RVA: 0x00006BC0 File Offset: 0x00004DC0
		// (set) Token: 0x06000385 RID: 901 RVA: 0x00006BC8 File Offset: 0x00004DC8
		public float CurrentTime { get; private set; }

		// Token: 0x06000386 RID: 902 RVA: 0x00006BD1 File Offset: 0x00004DD1
		public SynchronizeMissionTimeTracker(float currentTime)
		{
			this.CurrentTime = currentTime;
		}

		// Token: 0x06000387 RID: 903 RVA: 0x00006BE0 File Offset: 0x00004DE0
		public SynchronizeMissionTimeTracker()
		{
		}

		// Token: 0x06000388 RID: 904 RVA: 0x00006BE8 File Offset: 0x00004DE8
		protected override bool OnRead()
		{
			bool result = true;
			this.CurrentTime = GameNetworkMessage.ReadFloatFromPacket(CompressionMatchmaker.MissionTimeCompressionInfo, ref result);
			return result;
		}

		// Token: 0x06000389 RID: 905 RVA: 0x00006C0A File Offset: 0x00004E0A
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteFloatToPacket(this.CurrentTime, CompressionMatchmaker.MissionTimeCompressionInfo);
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00006C1C File Offset: 0x00004E1C
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.MissionDetailed;
		}

		// Token: 0x0600038B RID: 907 RVA: 0x00006C24 File Offset: 0x00004E24
		protected override string OnGetLogFormat()
		{
			return this.CurrentTime + " seconds have elapsed since the start of the mission.";
		}
	}
}
