using System;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x0200008C RID: 140
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class HandleMissileCollisionReaction : GameNetworkMessage
	{
		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x00009CEE File Offset: 0x00007EEE
		// (set) Token: 0x06000570 RID: 1392 RVA: 0x00009CF6 File Offset: 0x00007EF6
		public int MissileIndex { get; private set; }

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x00009CFF File Offset: 0x00007EFF
		// (set) Token: 0x06000572 RID: 1394 RVA: 0x00009D07 File Offset: 0x00007F07
		public Mission.MissileCollisionReaction CollisionReaction { get; private set; }

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x00009D10 File Offset: 0x00007F10
		// (set) Token: 0x06000574 RID: 1396 RVA: 0x00009D18 File Offset: 0x00007F18
		public MatrixFrame AttachLocalFrame { get; private set; }

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x00009D21 File Offset: 0x00007F21
		// (set) Token: 0x06000576 RID: 1398 RVA: 0x00009D29 File Offset: 0x00007F29
		public bool IsAttachedFrameLocal { get; private set; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x00009D32 File Offset: 0x00007F32
		// (set) Token: 0x06000578 RID: 1400 RVA: 0x00009D3A File Offset: 0x00007F3A
		public int AttackerAgentIndex { get; private set; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x00009D43 File Offset: 0x00007F43
		// (set) Token: 0x0600057A RID: 1402 RVA: 0x00009D4B File Offset: 0x00007F4B
		public int AttachedAgentIndex { get; private set; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600057B RID: 1403 RVA: 0x00009D54 File Offset: 0x00007F54
		// (set) Token: 0x0600057C RID: 1404 RVA: 0x00009D5C File Offset: 0x00007F5C
		public bool AttachedToShield { get; private set; }

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600057D RID: 1405 RVA: 0x00009D65 File Offset: 0x00007F65
		// (set) Token: 0x0600057E RID: 1406 RVA: 0x00009D6D File Offset: 0x00007F6D
		public sbyte AttachedBoneIndex { get; private set; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600057F RID: 1407 RVA: 0x00009D76 File Offset: 0x00007F76
		// (set) Token: 0x06000580 RID: 1408 RVA: 0x00009D7E File Offset: 0x00007F7E
		public MissionObjectId AttachedMissionObjectId { get; private set; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x00009D87 File Offset: 0x00007F87
		// (set) Token: 0x06000582 RID: 1410 RVA: 0x00009D8F File Offset: 0x00007F8F
		public Vec3 BounceBackVelocity { get; private set; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000583 RID: 1411 RVA: 0x00009D98 File Offset: 0x00007F98
		// (set) Token: 0x06000584 RID: 1412 RVA: 0x00009DA0 File Offset: 0x00007FA0
		public Vec3 BounceBackAngularVelocity { get; private set; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000585 RID: 1413 RVA: 0x00009DA9 File Offset: 0x00007FA9
		// (set) Token: 0x06000586 RID: 1414 RVA: 0x00009DB1 File Offset: 0x00007FB1
		public int ForcedSpawnIndex { get; private set; }

		// Token: 0x06000587 RID: 1415 RVA: 0x00009DBC File Offset: 0x00007FBC
		public HandleMissileCollisionReaction(int missileIndex, Mission.MissileCollisionReaction collisionReaction, MatrixFrame attachLocalFrame, bool isAttachedFrameLocal, int attackerAgentIndex, int attachedAgentIndex, bool attachedToShield, sbyte attachedBoneIndex, MissionObjectId attachedMissionObjectId, Vec3 bounceBackVelocity, Vec3 bounceBackAngularVelocity, int forcedSpawnIndex)
		{
			this.MissileIndex = missileIndex;
			this.CollisionReaction = collisionReaction;
			this.AttachLocalFrame = attachLocalFrame;
			this.IsAttachedFrameLocal = isAttachedFrameLocal;
			this.AttackerAgentIndex = attackerAgentIndex;
			this.AttachedAgentIndex = attachedAgentIndex;
			this.AttachedToShield = attachedToShield;
			this.AttachedBoneIndex = attachedBoneIndex;
			this.AttachedMissionObjectId = attachedMissionObjectId;
			this.BounceBackVelocity = bounceBackVelocity;
			this.BounceBackAngularVelocity = bounceBackAngularVelocity;
			this.ForcedSpawnIndex = forcedSpawnIndex;
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x00009E2C File Offset: 0x0000802C
		public HandleMissileCollisionReaction()
		{
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x00009E34 File Offset: 0x00008034
		protected override bool OnRead()
		{
			bool result = true;
			this.MissileIndex = GameNetworkMessage.ReadIntFromPacket(CompressionMission.MissileCompressionInfo, ref result);
			this.CollisionReaction = (Mission.MissileCollisionReaction)GameNetworkMessage.ReadIntFromPacket(CompressionMission.MissileCollisionReactionCompressionInfo, ref result);
			this.AttackerAgentIndex = GameNetworkMessage.ReadAgentIndexFromPacket(ref result);
			this.AttachedAgentIndex = -1;
			this.AttachedToShield = false;
			this.AttachedBoneIndex = -1;
			this.AttachedMissionObjectId = MissionObjectId.Invalid;
			if (this.CollisionReaction == Mission.MissileCollisionReaction.Stick || this.CollisionReaction == Mission.MissileCollisionReaction.BounceBack)
			{
				if (GameNetworkMessage.ReadBoolFromPacket(ref result))
				{
					this.AttachedAgentIndex = GameNetworkMessage.ReadAgentIndexFromPacket(ref result);
					this.AttachedToShield = GameNetworkMessage.ReadBoolFromPacket(ref result);
					if (!this.AttachedToShield)
					{
						this.AttachedBoneIndex = (sbyte)GameNetworkMessage.ReadIntFromPacket(CompressionMission.BoneIndexCompressionInfo, ref result);
					}
				}
				else
				{
					this.AttachedMissionObjectId = GameNetworkMessage.ReadMissionObjectIdFromPacket(ref result);
				}
			}
			if (this.CollisionReaction != Mission.MissileCollisionReaction.BecomeInvisible && this.CollisionReaction != Mission.MissileCollisionReaction.PassThrough)
			{
				this.IsAttachedFrameLocal = GameNetworkMessage.ReadBoolFromPacket(ref result);
				if (this.IsAttachedFrameLocal)
				{
					this.AttachLocalFrame = GameNetworkMessage.ReadNonUniformTransformFromPacket(CompressionBasic.BigRangeLowResLocalPositionCompressionInfo, CompressionBasic.LowResQuaternionCompressionInfo, ref result);
				}
				else
				{
					this.AttachLocalFrame = GameNetworkMessage.ReadNonUniformTransformFromPacket(CompressionBasic.PositionCompressionInfo, CompressionBasic.LowResQuaternionCompressionInfo, ref result);
				}
			}
			else
			{
				this.AttachLocalFrame = MatrixFrame.Identity;
			}
			if (this.CollisionReaction == Mission.MissileCollisionReaction.BounceBack)
			{
				this.BounceBackVelocity = GameNetworkMessage.ReadVec3FromPacket(CompressionMission.SpawnedItemVelocityCompressionInfo, ref result);
				this.BounceBackAngularVelocity = GameNetworkMessage.ReadVec3FromPacket(CompressionMission.SpawnedItemAngularVelocityCompressionInfo, ref result);
			}
			else
			{
				this.BounceBackVelocity = Vec3.Zero;
				this.BounceBackAngularVelocity = Vec3.Zero;
			}
			if (this.CollisionReaction == Mission.MissileCollisionReaction.Stick || this.CollisionReaction == Mission.MissileCollisionReaction.BounceBack)
			{
				this.ForcedSpawnIndex = GameNetworkMessage.ReadIntFromPacket(CompressionBasic.MissionObjectIDCompressionInfo, ref result);
			}
			return result;
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00009FC0 File Offset: 0x000081C0
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteIntToPacket(this.MissileIndex, CompressionMission.MissileCompressionInfo);
			GameNetworkMessage.WriteIntToPacket((int)this.CollisionReaction, CompressionMission.MissileCollisionReactionCompressionInfo);
			GameNetworkMessage.WriteAgentIndexToPacket(this.AttackerAgentIndex);
			if (this.CollisionReaction == Mission.MissileCollisionReaction.Stick || this.CollisionReaction == Mission.MissileCollisionReaction.BounceBack)
			{
				bool flag = this.AttachedAgentIndex >= 0;
				GameNetworkMessage.WriteBoolToPacket(flag);
				if (flag)
				{
					GameNetworkMessage.WriteAgentIndexToPacket(this.AttachedAgentIndex);
					GameNetworkMessage.WriteBoolToPacket(this.AttachedToShield);
					if (!this.AttachedToShield)
					{
						GameNetworkMessage.WriteIntToPacket((int)this.AttachedBoneIndex, CompressionMission.BoneIndexCompressionInfo);
					}
				}
				else
				{
					GameNetworkMessage.WriteMissionObjectIdToPacket((this.AttachedMissionObjectId.Id >= 0) ? this.AttachedMissionObjectId : MissionObjectId.Invalid);
				}
			}
			if (this.CollisionReaction != Mission.MissileCollisionReaction.BecomeInvisible && this.CollisionReaction != Mission.MissileCollisionReaction.PassThrough)
			{
				GameNetworkMessage.WriteBoolToPacket(this.IsAttachedFrameLocal);
				if (this.IsAttachedFrameLocal)
				{
					GameNetworkMessage.WriteNonUniformTransformToPacket(this.AttachLocalFrame, CompressionBasic.BigRangeLowResLocalPositionCompressionInfo, CompressionBasic.LowResQuaternionCompressionInfo);
				}
				else
				{
					GameNetworkMessage.WriteNonUniformTransformToPacket(this.AttachLocalFrame, CompressionBasic.PositionCompressionInfo, CompressionBasic.LowResQuaternionCompressionInfo);
				}
			}
			if (this.CollisionReaction == Mission.MissileCollisionReaction.BounceBack)
			{
				GameNetworkMessage.WriteVec3ToPacket(this.BounceBackVelocity, CompressionMission.SpawnedItemVelocityCompressionInfo);
				GameNetworkMessage.WriteVec3ToPacket(this.BounceBackAngularVelocity, CompressionMission.SpawnedItemAngularVelocityCompressionInfo);
			}
			if (this.CollisionReaction == Mission.MissileCollisionReaction.Stick || this.CollisionReaction == Mission.MissileCollisionReaction.BounceBack)
			{
				GameNetworkMessage.WriteIntToPacket(this.ForcedSpawnIndex, CompressionBasic.MissionObjectIDCompressionInfo);
			}
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x0000A108 File Offset: 0x00008308
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Items;
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x0000A10C File Offset: 0x0000830C
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Handle Missile Collision with index: ",
				this.MissileIndex,
				" collision reaction: ",
				this.CollisionReaction,
				" AttackerAgent index: ",
				this.AttackerAgentIndex,
				" AttachedAgent index: ",
				this.AttachedAgentIndex,
				" AttachedToShield: ",
				this.AttachedToShield.ToString(),
				" AttachedBoneIndex: ",
				this.AttachedBoneIndex,
				" AttachedMissionObject id: ",
				(this.AttachedMissionObjectId != MissionObjectId.Invalid) ? this.AttachedMissionObjectId.Id.ToString() : "-1",
				" ForcedSpawnIndex: ",
				this.ForcedSpawnIndex
			});
		}
	}
}
