using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x02000044 RID: 68
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class FlagRaisingStatus : GameNetworkMessage
	{
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000234 RID: 564 RVA: 0x00004911 File Offset: 0x00002B11
		// (set) Token: 0x06000235 RID: 565 RVA: 0x00004919 File Offset: 0x00002B19
		public float Progress { get; private set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000236 RID: 566 RVA: 0x00004922 File Offset: 0x00002B22
		// (set) Token: 0x06000237 RID: 567 RVA: 0x0000492A File Offset: 0x00002B2A
		public CaptureTheFlagFlagDirection Direction { get; private set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000238 RID: 568 RVA: 0x00004933 File Offset: 0x00002B33
		// (set) Token: 0x06000239 RID: 569 RVA: 0x0000493B File Offset: 0x00002B3B
		public float Speed { get; private set; }

		// Token: 0x0600023A RID: 570 RVA: 0x00004944 File Offset: 0x00002B44
		public FlagRaisingStatus()
		{
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000494C File Offset: 0x00002B4C
		public FlagRaisingStatus(float currProgress, CaptureTheFlagFlagDirection direction, float speed)
		{
			this.Progress = currProgress;
			this.Direction = direction;
			this.Speed = speed;
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000496C File Offset: 0x00002B6C
		protected override bool OnRead()
		{
			bool flag = true;
			this.Progress = GameNetworkMessage.ReadFloatFromPacket(CompressionMission.FlagClassicProgressCompressionInfo, ref flag);
			this.Direction = (CaptureTheFlagFlagDirection)GameNetworkMessage.ReadIntFromPacket(CompressionMission.FlagDirectionEnumCompressionInfo, ref flag);
			if (flag && this.Direction != CaptureTheFlagFlagDirection.None && this.Direction != CaptureTheFlagFlagDirection.Static)
			{
				this.Speed = GameNetworkMessage.ReadFloatFromPacket(CompressionMission.FlagSpeedCompressionInfo, ref flag);
			}
			return flag;
		}

		// Token: 0x0600023D RID: 573 RVA: 0x000049C8 File Offset: 0x00002BC8
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteFloatToPacket(this.Progress, CompressionMission.FlagClassicProgressCompressionInfo);
			GameNetworkMessage.WriteIntToPacket((int)this.Direction, CompressionMission.FlagDirectionEnumCompressionInfo);
			if (this.Direction != CaptureTheFlagFlagDirection.None && this.Direction != CaptureTheFlagFlagDirection.Static)
			{
				GameNetworkMessage.WriteFloatToPacket(this.Speed, CompressionMission.FlagSpeedCompressionInfo);
			}
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00004A17 File Offset: 0x00002C17
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.GameMode;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00004A20 File Offset: 0x00002C20
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Updating flag movement: Progress: ",
				this.Progress,
				", Direction: ",
				this.Direction,
				", Speed: ",
				this.Speed
			});
		}
	}
}
