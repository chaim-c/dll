using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x020000C7 RID: 199
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class SynchronizeMissionObject : GameNetworkMessage
	{
		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000824 RID: 2084 RVA: 0x0000DCD1 File Offset: 0x0000BED1
		// (set) Token: 0x06000825 RID: 2085 RVA: 0x0000DCD9 File Offset: 0x0000BED9
		public MissionObjectId MissionObjectId { get; private set; }

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000826 RID: 2086 RVA: 0x0000DCE2 File Offset: 0x0000BEE2
		// (set) Token: 0x06000827 RID: 2087 RVA: 0x0000DCEA File Offset: 0x0000BEEA
		public int RecordTypeIndex { get; private set; }

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000828 RID: 2088 RVA: 0x0000DCF3 File Offset: 0x0000BEF3
		// (set) Token: 0x06000829 RID: 2089 RVA: 0x0000DCFB File Offset: 0x0000BEFB
		public ValueTuple<BaseSynchedMissionObjectReadableRecord, ISynchedMissionObjectReadableRecord> RecordPair { get; private set; }

		// Token: 0x0600082A RID: 2090 RVA: 0x0000DD04 File Offset: 0x0000BF04
		public SynchronizeMissionObject(SynchedMissionObject synchedMissionObject)
		{
			this._synchedMissionObject = synchedMissionObject;
			this.MissionObjectId = synchedMissionObject.Id;
			this.RecordTypeIndex = GameNetwork.GetSynchedMissionObjectReadableRecordIndexFromType(synchedMissionObject.GetType());
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x0000DD30 File Offset: 0x0000BF30
		public SynchronizeMissionObject()
		{
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x0000DD38 File Offset: 0x0000BF38
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteMissionObjectIdToPacket(this.MissionObjectId);
			GameNetworkMessage.WriteIntToPacket(this.RecordTypeIndex, CompressionMission.SynchedMissionObjectReadableRecordTypeIndex);
			this._synchedMissionObject.WriteToNetwork();
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x0000DD60 File Offset: 0x0000BF60
		protected override bool OnRead()
		{
			bool result = true;
			this.MissionObjectId = GameNetworkMessage.ReadMissionObjectIdFromPacket(ref result);
			this.RecordTypeIndex = GameNetworkMessage.ReadIntFromPacket(CompressionMission.SynchedMissionObjectReadableRecordTypeIndex, ref result);
			this.RecordPair = BaseSynchedMissionObjectReadableRecord.CreateFromNetworkWithTypeIndex(this.RecordTypeIndex);
			return result;
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x0000DDA0 File Offset: 0x0000BFA0
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Mission;
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x0000DDA8 File Offset: 0x0000BFA8
		protected override string OnGetLogFormat()
		{
			return "Synchronize MissionObject with Id: " + this.MissionObjectId;
		}

		// Token: 0x040001D7 RID: 471
		private SynchedMissionObject _synchedMissionObject;
	}
}
