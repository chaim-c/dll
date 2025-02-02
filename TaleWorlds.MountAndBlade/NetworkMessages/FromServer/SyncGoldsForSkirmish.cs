using System;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x02000064 RID: 100
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class SyncGoldsForSkirmish : GameNetworkMessage
	{
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600037A RID: 890 RVA: 0x00006ADC File Offset: 0x00004CDC
		// (set) Token: 0x0600037B RID: 891 RVA: 0x00006AE4 File Offset: 0x00004CE4
		public VirtualPlayer VirtualPlayer { get; private set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600037C RID: 892 RVA: 0x00006AED File Offset: 0x00004CED
		// (set) Token: 0x0600037D RID: 893 RVA: 0x00006AF5 File Offset: 0x00004CF5
		public int GoldAmount { get; private set; }

		// Token: 0x0600037E RID: 894 RVA: 0x00006AFE File Offset: 0x00004CFE
		public SyncGoldsForSkirmish()
		{
		}

		// Token: 0x0600037F RID: 895 RVA: 0x00006B06 File Offset: 0x00004D06
		public SyncGoldsForSkirmish(VirtualPlayer peer, int goldAmount)
		{
			this.VirtualPlayer = peer;
			this.GoldAmount = goldAmount;
		}

		// Token: 0x06000380 RID: 896 RVA: 0x00006B1C File Offset: 0x00004D1C
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteVirtualPlayerReferenceToPacket(this.VirtualPlayer);
			GameNetworkMessage.WriteIntToPacket(this.GoldAmount, CompressionBasic.RoundGoldAmountCompressionInfo);
		}

		// Token: 0x06000381 RID: 897 RVA: 0x00006B3C File Offset: 0x00004D3C
		protected override bool OnRead()
		{
			bool result = true;
			this.VirtualPlayer = GameNetworkMessage.ReadVirtualPlayerReferenceToPacket(ref result, false);
			this.GoldAmount = GameNetworkMessage.ReadIntFromPacket(CompressionBasic.RoundGoldAmountCompressionInfo, ref result);
			return result;
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00006B6C File Offset: 0x00004D6C
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.GameMode;
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00006B74 File Offset: 0x00004D74
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Gold amount set to ",
				this.GoldAmount,
				" for ",
				this.VirtualPlayer.UserName,
				"."
			});
		}
	}
}
