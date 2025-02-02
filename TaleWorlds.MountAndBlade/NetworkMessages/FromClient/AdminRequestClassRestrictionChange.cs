using System;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromClient
{
	// Token: 0x02000012 RID: 18
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromClient)]
	public sealed class AdminRequestClassRestrictionChange : GameNetworkMessage
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600007D RID: 125 RVA: 0x000029AF File Offset: 0x00000BAF
		// (set) Token: 0x0600007E RID: 126 RVA: 0x000029B7 File Offset: 0x00000BB7
		public FormationClass ClassToChangeRestriction { get; private set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600007F RID: 127 RVA: 0x000029C0 File Offset: 0x00000BC0
		// (set) Token: 0x06000080 RID: 128 RVA: 0x000029C8 File Offset: 0x00000BC8
		public bool NewValue { get; private set; }

		// Token: 0x06000081 RID: 129 RVA: 0x000029D1 File Offset: 0x00000BD1
		public AdminRequestClassRestrictionChange()
		{
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000029D9 File Offset: 0x00000BD9
		public AdminRequestClassRestrictionChange(FormationClass classToChangeRestriction, bool newValue)
		{
			this.ClassToChangeRestriction = classToChangeRestriction;
			this.NewValue = newValue;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000029F0 File Offset: 0x00000BF0
		protected override bool OnRead()
		{
			bool result = true;
			this.ClassToChangeRestriction = (FormationClass)GameNetworkMessage.ReadIntFromPacket(CompressionMission.FormationClassCompressionInfo, ref result);
			this.NewValue = GameNetworkMessage.ReadBoolFromPacket(ref result);
			return result;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00002A1F File Offset: 0x00000C1F
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteIntToPacket((int)this.ClassToChangeRestriction, CompressionMission.FormationClassCompressionInfo);
			GameNetworkMessage.WriteBoolToPacket(this.NewValue);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00002A3C File Offset: 0x00000C3C
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Administration;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00002A44 File Offset: 0x00000C44
		protected override string OnGetLogFormat()
		{
			return string.Format("AdminRequestClassRestrictionChange for {0} to be {1}", this.ClassToChangeRestriction, this.NewValue);
		}
	}
}
