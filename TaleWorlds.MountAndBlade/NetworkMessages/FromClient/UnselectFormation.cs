using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromClient
{
	// Token: 0x02000039 RID: 57
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromClient)]
	public sealed class UnselectFormation : GameNetworkMessage
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x000040B7 File Offset: 0x000022B7
		// (set) Token: 0x060001C9 RID: 457 RVA: 0x000040BF File Offset: 0x000022BF
		public int FormationIndex { get; private set; }

		// Token: 0x060001CA RID: 458 RVA: 0x000040C8 File Offset: 0x000022C8
		public UnselectFormation(int formationIndex)
		{
			this.FormationIndex = formationIndex;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x000040D7 File Offset: 0x000022D7
		public UnselectFormation()
		{
		}

		// Token: 0x060001CC RID: 460 RVA: 0x000040E0 File Offset: 0x000022E0
		protected override bool OnRead()
		{
			bool result = true;
			this.FormationIndex = GameNetworkMessage.ReadIntFromPacket(CompressionMission.FormationClassCompressionInfo, ref result);
			return result;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00004102 File Offset: 0x00002302
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteIntToPacket(this.FormationIndex, CompressionMission.FormationClassCompressionInfo);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00004114 File Offset: 0x00002314
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Formations;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00004119 File Offset: 0x00002319
		protected override string OnGetLogFormat()
		{
			return "Deselect Formation with index: " + this.FormationIndex;
		}
	}
}
