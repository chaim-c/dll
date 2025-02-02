using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromClient
{
	// Token: 0x0200001D RID: 29
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromClient)]
	public sealed class TeamChange : GameNetworkMessage
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00003177 File Offset: 0x00001377
		// (set) Token: 0x060000E0 RID: 224 RVA: 0x0000317F File Offset: 0x0000137F
		public bool AutoAssign { get; private set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00003188 File Offset: 0x00001388
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x00003190 File Offset: 0x00001390
		public int TeamIndex { get; private set; }

		// Token: 0x060000E3 RID: 227 RVA: 0x00003199 File Offset: 0x00001399
		public TeamChange(bool autoAssign, int teamIndex)
		{
			this.AutoAssign = autoAssign;
			this.TeamIndex = teamIndex;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000031AF File Offset: 0x000013AF
		public TeamChange()
		{
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000031B8 File Offset: 0x000013B8
		protected override bool OnRead()
		{
			bool result = true;
			this.AutoAssign = GameNetworkMessage.ReadBoolFromPacket(ref result);
			if (!this.AutoAssign)
			{
				this.TeamIndex = GameNetworkMessage.ReadIntFromPacket(CompressionMission.TeamCompressionInfo, ref result);
			}
			return result;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000031EF File Offset: 0x000013EF
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteBoolToPacket(this.AutoAssign);
			if (!this.AutoAssign)
			{
				GameNetworkMessage.WriteIntToPacket(this.TeamIndex, CompressionMission.TeamCompressionInfo);
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00003214 File Offset: 0x00001414
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Mission;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x0000321C File Offset: 0x0000141C
		protected override string OnGetLogFormat()
		{
			return "Changed team to: " + this.TeamIndex;
		}
	}
}
