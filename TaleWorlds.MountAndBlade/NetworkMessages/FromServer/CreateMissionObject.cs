using System;
using System.Collections.Generic;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x02000085 RID: 133
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class CreateMissionObject : GameNetworkMessage
	{
		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600052E RID: 1326 RVA: 0x00009755 File Offset: 0x00007955
		// (set) Token: 0x0600052F RID: 1327 RVA: 0x0000975D File Offset: 0x0000795D
		public MissionObjectId ObjectId { get; private set; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000530 RID: 1328 RVA: 0x00009766 File Offset: 0x00007966
		// (set) Token: 0x06000531 RID: 1329 RVA: 0x0000976E File Offset: 0x0000796E
		public string Prefab { get; private set; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x00009777 File Offset: 0x00007977
		// (set) Token: 0x06000533 RID: 1331 RVA: 0x0000977F File Offset: 0x0000797F
		public MatrixFrame Frame { get; private set; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x00009788 File Offset: 0x00007988
		// (set) Token: 0x06000535 RID: 1333 RVA: 0x00009790 File Offset: 0x00007990
		public List<MissionObjectId> ChildObjectIds { get; private set; }

		// Token: 0x06000536 RID: 1334 RVA: 0x00009799 File Offset: 0x00007999
		public CreateMissionObject(MissionObjectId objectId, string prefab, MatrixFrame frame, List<MissionObjectId> childObjectIds)
		{
			this.ObjectId = objectId;
			this.Prefab = prefab;
			this.Frame = frame;
			this.ChildObjectIds = childObjectIds;
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x000097BE File Offset: 0x000079BE
		public CreateMissionObject()
		{
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x000097C8 File Offset: 0x000079C8
		protected override bool OnRead()
		{
			bool flag = true;
			this.ObjectId = GameNetworkMessage.ReadMissionObjectIdFromPacket(ref flag);
			this.Prefab = GameNetworkMessage.ReadStringFromPacket(ref flag);
			this.Frame = GameNetworkMessage.ReadMatrixFrameFromPacket(ref flag);
			int num = GameNetworkMessage.ReadIntFromPacket(CompressionBasic.EntityChildCountCompressionInfo, ref flag);
			if (flag)
			{
				this.ChildObjectIds = new List<MissionObjectId>(num);
				for (int i = 0; i < num; i++)
				{
					if (flag)
					{
						this.ChildObjectIds.Add(GameNetworkMessage.ReadMissionObjectIdFromPacket(ref flag));
					}
				}
			}
			return flag;
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x0000983C File Offset: 0x00007A3C
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteMissionObjectIdToPacket(this.ObjectId);
			GameNetworkMessage.WriteStringToPacket(this.Prefab);
			GameNetworkMessage.WriteMatrixFrameToPacket(this.Frame);
			GameNetworkMessage.WriteIntToPacket(this.ChildObjectIds.Count, CompressionBasic.EntityChildCountCompressionInfo);
			foreach (MissionObjectId value in this.ChildObjectIds)
			{
				GameNetworkMessage.WriteMissionObjectIdToPacket(value);
			}
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x000098C4 File Offset: 0x00007AC4
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.MissionObjects;
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x000098CC File Offset: 0x00007ACC
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Create a MissionObject with index: ",
				this.ObjectId,
				" from prefab: ",
				this.Prefab,
				" at frame: ",
				this.Frame
			});
		}
	}
}
