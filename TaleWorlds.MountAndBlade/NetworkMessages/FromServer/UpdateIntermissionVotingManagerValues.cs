using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x02000069 RID: 105
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class UpdateIntermissionVotingManagerValues : GameNetworkMessage
	{
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060003AA RID: 938 RVA: 0x0000706A File Offset: 0x0000526A
		// (set) Token: 0x060003AB RID: 939 RVA: 0x00007072 File Offset: 0x00005272
		public bool IsAutomatedBattleSwitchingEnabled { get; private set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060003AC RID: 940 RVA: 0x0000707B File Offset: 0x0000527B
		// (set) Token: 0x060003AD RID: 941 RVA: 0x00007083 File Offset: 0x00005283
		public bool IsMapVoteEnabled { get; private set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060003AE RID: 942 RVA: 0x0000708C File Offset: 0x0000528C
		// (set) Token: 0x060003AF RID: 943 RVA: 0x00007094 File Offset: 0x00005294
		public bool IsCultureVoteEnabled { get; private set; }

		// Token: 0x060003B0 RID: 944 RVA: 0x0000709D File Offset: 0x0000529D
		public UpdateIntermissionVotingManagerValues()
		{
			this.IsAutomatedBattleSwitchingEnabled = MultiplayerIntermissionVotingManager.Instance.IsAutomatedBattleSwitchingEnabled;
			this.IsMapVoteEnabled = MultiplayerIntermissionVotingManager.Instance.IsMapVoteEnabled;
			this.IsCultureVoteEnabled = MultiplayerIntermissionVotingManager.Instance.IsCultureVoteEnabled;
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x000070D5 File Offset: 0x000052D5
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Administration;
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x000070DD File Offset: 0x000052DD
		protected override string OnGetLogFormat()
		{
			return string.Format("IsAutomatedBattleSwitchingEnabled: {0}, IsMapVoteEnabled: {1}, IsCultureVoteEnabled: {2}", this.IsAutomatedBattleSwitchingEnabled, this.IsMapVoteEnabled, this.IsCultureVoteEnabled);
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0000710C File Offset: 0x0000530C
		protected override bool OnRead()
		{
			bool result = true;
			this.IsAutomatedBattleSwitchingEnabled = GameNetworkMessage.ReadBoolFromPacket(ref result);
			this.IsMapVoteEnabled = GameNetworkMessage.ReadBoolFromPacket(ref result);
			this.IsCultureVoteEnabled = GameNetworkMessage.ReadBoolFromPacket(ref result);
			return result;
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00007143 File Offset: 0x00005343
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteBoolToPacket(this.IsAutomatedBattleSwitchingEnabled);
			GameNetworkMessage.WriteBoolToPacket(this.IsMapVoteEnabled);
			GameNetworkMessage.WriteBoolToPacket(this.IsCultureVoteEnabled);
		}
	}
}
