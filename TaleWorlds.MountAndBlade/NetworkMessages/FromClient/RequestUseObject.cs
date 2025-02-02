using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromClient
{
	// Token: 0x02000030 RID: 48
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromClient)]
	public sealed class RequestUseObject : GameNetworkMessage
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600017E RID: 382 RVA: 0x00003C6F File Offset: 0x00001E6F
		// (set) Token: 0x0600017F RID: 383 RVA: 0x00003C77 File Offset: 0x00001E77
		public MissionObjectId UsableMissionObjectId { get; private set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00003C80 File Offset: 0x00001E80
		// (set) Token: 0x06000181 RID: 385 RVA: 0x00003C88 File Offset: 0x00001E88
		public int UsedObjectPreferenceIndex { get; private set; }

		// Token: 0x06000182 RID: 386 RVA: 0x00003C91 File Offset: 0x00001E91
		public RequestUseObject(MissionObjectId usableMissionObjectId, int usedObjectPreferenceIndex)
		{
			this.UsableMissionObjectId = usableMissionObjectId;
			this.UsedObjectPreferenceIndex = usedObjectPreferenceIndex;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00003CA7 File Offset: 0x00001EA7
		public RequestUseObject()
		{
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00003CB0 File Offset: 0x00001EB0
		protected override bool OnRead()
		{
			bool result = true;
			this.UsableMissionObjectId = GameNetworkMessage.ReadMissionObjectIdFromPacket(ref result);
			this.UsedObjectPreferenceIndex = GameNetworkMessage.ReadIntFromPacket(CompressionMission.WieldSlotCompressionInfo, ref result);
			return result;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00003CDF File Offset: 0x00001EDF
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteMissionObjectIdToPacket(this.UsableMissionObjectId);
			GameNetworkMessage.WriteIntToPacket(this.UsedObjectPreferenceIndex, CompressionMission.WieldSlotCompressionInfo);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00003CFC File Offset: 0x00001EFC
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.MissionObjectsDetailed;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00003D04 File Offset: 0x00001F04
		protected override string OnGetLogFormat()
		{
			return "Request to use UsableMissionObject with ID: " + this.UsableMissionObjectId;
		}
	}
}
