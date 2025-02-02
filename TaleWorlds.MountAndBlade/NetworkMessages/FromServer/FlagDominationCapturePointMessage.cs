using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x0200008A RID: 138
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class FlagDominationCapturePointMessage : GameNetworkMessage
	{
		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000560 RID: 1376 RVA: 0x00009C34 File Offset: 0x00007E34
		// (set) Token: 0x06000561 RID: 1377 RVA: 0x00009C3C File Offset: 0x00007E3C
		public int FlagIndex { get; private set; }

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000562 RID: 1378 RVA: 0x00009C45 File Offset: 0x00007E45
		// (set) Token: 0x06000563 RID: 1379 RVA: 0x00009C4D File Offset: 0x00007E4D
		public int OwnerTeamIndex { get; private set; }

		// Token: 0x06000564 RID: 1380 RVA: 0x00009C56 File Offset: 0x00007E56
		public FlagDominationCapturePointMessage()
		{
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x00009C5E File Offset: 0x00007E5E
		public FlagDominationCapturePointMessage(int flagIndex, int ownerTeamIndex)
		{
			this.FlagIndex = flagIndex;
			this.OwnerTeamIndex = ownerTeamIndex;
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00009C74 File Offset: 0x00007E74
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteIntToPacket(this.FlagIndex, CompressionMission.FlagCapturePointIndexCompressionInfo);
			GameNetworkMessage.WriteTeamIndexToPacket(this.OwnerTeamIndex);
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x00009C94 File Offset: 0x00007E94
		protected override bool OnRead()
		{
			bool result = true;
			this.FlagIndex = GameNetworkMessage.ReadIntFromPacket(CompressionMission.FlagCapturePointIndexCompressionInfo, ref result);
			this.OwnerTeamIndex = GameNetworkMessage.ReadTeamIndexFromPacket(ref result);
			return result;
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00009CC3 File Offset: 0x00007EC3
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.GameMode;
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00009CCB File Offset: 0x00007ECB
		protected override string OnGetLogFormat()
		{
			return "Flag owner changed.";
		}
	}
}
