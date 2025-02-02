using System;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x0200004F RID: 79
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class ChangeClassRestrictions : GameNetworkMessage
	{
		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x000054FA File Offset: 0x000036FA
		// (set) Token: 0x060002B1 RID: 689 RVA: 0x00005502 File Offset: 0x00003702
		public FormationClass ClassToChangeRestriction { get; private set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x0000550B File Offset: 0x0000370B
		// (set) Token: 0x060002B3 RID: 691 RVA: 0x00005513 File Offset: 0x00003713
		public bool NewValue { get; private set; }

		// Token: 0x060002B4 RID: 692 RVA: 0x0000551C File Offset: 0x0000371C
		public ChangeClassRestrictions()
		{
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x00005524 File Offset: 0x00003724
		public ChangeClassRestrictions(FormationClass classToChangeRestriction, bool newValue)
		{
			this.ClassToChangeRestriction = classToChangeRestriction;
			this.NewValue = newValue;
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000553C File Offset: 0x0000373C
		protected override bool OnRead()
		{
			bool result = true;
			this.ClassToChangeRestriction = (FormationClass)GameNetworkMessage.ReadIntFromPacket(CompressionMission.FormationClassCompressionInfo, ref result);
			this.NewValue = GameNetworkMessage.ReadBoolFromPacket(ref result);
			return result;
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000556B File Offset: 0x0000376B
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteIntToPacket((int)this.ClassToChangeRestriction, CompressionMission.FormationClassCompressionInfo);
			GameNetworkMessage.WriteBoolToPacket(this.NewValue);
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x00005588 File Offset: 0x00003788
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Administration;
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00005590 File Offset: 0x00003790
		protected override string OnGetLogFormat()
		{
			return string.Format("ChangeClassRestrictions for {0} to be {1}", this.ClassToChangeRestriction, this.NewValue);
		}
	}
}
