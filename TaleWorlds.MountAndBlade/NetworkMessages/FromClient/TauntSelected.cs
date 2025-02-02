using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromClient
{
	// Token: 0x02000038 RID: 56
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromClient)]
	public sealed class TauntSelected : GameNetworkMessage
	{
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x0000403D File Offset: 0x0000223D
		// (set) Token: 0x060001C1 RID: 449 RVA: 0x00004045 File Offset: 0x00002245
		public int IndexOfTaunt { get; private set; }

		// Token: 0x060001C2 RID: 450 RVA: 0x0000404E File Offset: 0x0000224E
		public TauntSelected(int indexOfTaunt)
		{
			this.IndexOfTaunt = indexOfTaunt;
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000405D File Offset: 0x0000225D
		public TauntSelected()
		{
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00004068 File Offset: 0x00002268
		protected override bool OnRead()
		{
			bool result = true;
			this.IndexOfTaunt = GameNetworkMessage.ReadIntFromPacket(CompressionMission.TauntIndexCompressionInfo, ref result);
			return result;
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000408A File Offset: 0x0000228A
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteIntToPacket(this.IndexOfTaunt, CompressionMission.TauntIndexCompressionInfo);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000409C File Offset: 0x0000229C
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.None;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x000040A0 File Offset: 0x000022A0
		protected override string OnGetLogFormat()
		{
			return "FromClient.CheerSelected: " + this.IndexOfTaunt;
		}
	}
}
