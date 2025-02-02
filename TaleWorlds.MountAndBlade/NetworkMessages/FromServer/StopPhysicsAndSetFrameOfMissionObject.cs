using System;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x020000C4 RID: 196
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class StopPhysicsAndSetFrameOfMissionObject : GameNetworkMessage
	{
		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000804 RID: 2052 RVA: 0x0000D9EA File Offset: 0x0000BBEA
		// (set) Token: 0x06000805 RID: 2053 RVA: 0x0000D9F2 File Offset: 0x0000BBF2
		public MissionObjectId ObjectId { get; private set; }

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000806 RID: 2054 RVA: 0x0000D9FB File Offset: 0x0000BBFB
		// (set) Token: 0x06000807 RID: 2055 RVA: 0x0000DA03 File Offset: 0x0000BC03
		public MissionObjectId ParentId { get; private set; }

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000808 RID: 2056 RVA: 0x0000DA0C File Offset: 0x0000BC0C
		// (set) Token: 0x06000809 RID: 2057 RVA: 0x0000DA14 File Offset: 0x0000BC14
		public MatrixFrame Frame { get; private set; }

		// Token: 0x0600080A RID: 2058 RVA: 0x0000DA1D File Offset: 0x0000BC1D
		public StopPhysicsAndSetFrameOfMissionObject(MissionObjectId objectId, MissionObjectId parentId, MatrixFrame frame)
		{
			this.ObjectId = objectId;
			this.ParentId = parentId;
			this.Frame = frame;
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x0000DA3A File Offset: 0x0000BC3A
		public StopPhysicsAndSetFrameOfMissionObject()
		{
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x0000DA44 File Offset: 0x0000BC44
		protected override bool OnRead()
		{
			bool result = true;
			this.ObjectId = GameNetworkMessage.ReadMissionObjectIdFromPacket(ref result);
			this.ParentId = GameNetworkMessage.ReadMissionObjectIdFromPacket(ref result);
			this.Frame = GameNetworkMessage.ReadNonUniformTransformFromPacket(CompressionBasic.PositionCompressionInfo, CompressionBasic.LowResQuaternionCompressionInfo, ref result);
			return result;
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x0000DA88 File Offset: 0x0000BC88
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteMissionObjectIdToPacket(this.ObjectId);
			GameNetworkMessage.WriteMissionObjectIdToPacket((this.ParentId.Id >= 0) ? this.ParentId : MissionObjectId.Invalid);
			GameNetworkMessage.WriteNonUniformTransformToPacket(this.Frame, CompressionBasic.PositionCompressionInfo, CompressionBasic.LowResQuaternionCompressionInfo);
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x0000DAD5 File Offset: 0x0000BCD5
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.MissionObjects;
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x0000DADD File Offset: 0x0000BCDD
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Stop physics and set frame of MissionObject with ID: ",
				this.ObjectId,
				" Parent Index: ",
				this.ParentId
			});
		}
	}
}
