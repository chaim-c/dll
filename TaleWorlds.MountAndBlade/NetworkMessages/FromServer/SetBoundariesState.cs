using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x020000A0 RID: 160
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class SetBoundariesState : GameNetworkMessage
	{
		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000663 RID: 1635 RVA: 0x0000B39A File Offset: 0x0000959A
		// (set) Token: 0x06000664 RID: 1636 RVA: 0x0000B3A2 File Offset: 0x000095A2
		public bool IsOutside { get; private set; }

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000665 RID: 1637 RVA: 0x0000B3AB File Offset: 0x000095AB
		// (set) Token: 0x06000666 RID: 1638 RVA: 0x0000B3B3 File Offset: 0x000095B3
		public float StateStartTimeInSeconds { get; private set; }

		// Token: 0x06000667 RID: 1639 RVA: 0x0000B3BC File Offset: 0x000095BC
		public SetBoundariesState()
		{
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x0000B3C4 File Offset: 0x000095C4
		public SetBoundariesState(bool isOutside)
		{
			this.IsOutside = isOutside;
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x0000B3D3 File Offset: 0x000095D3
		public SetBoundariesState(bool isOutside, long stateStartTimeInTicks) : this(isOutside)
		{
			this.StateStartTimeInSeconds = (float)stateStartTimeInTicks / 10000000f;
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x0000B3EA File Offset: 0x000095EA
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteBoolToPacket(this.IsOutside);
			if (this.IsOutside)
			{
				GameNetworkMessage.WriteFloatToPacket(this.StateStartTimeInSeconds, CompressionMatchmaker.MissionTimeCompressionInfo);
			}
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x0000B410 File Offset: 0x00009610
		protected override bool OnRead()
		{
			bool result = true;
			this.IsOutside = GameNetworkMessage.ReadBoolFromPacket(ref result);
			if (this.IsOutside)
			{
				this.StateStartTimeInSeconds = GameNetworkMessage.ReadFloatFromPacket(CompressionMatchmaker.MissionTimeCompressionInfo, ref result);
			}
			return result;
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x0000B447 File Offset: 0x00009647
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.AgentsDetailed;
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x0000B44F File Offset: 0x0000964F
		protected override string OnGetLogFormat()
		{
			if (!this.IsOutside)
			{
				return "I am now inside the level boundaries";
			}
			return "I am now outside of the level boundaries";
		}
	}
}
