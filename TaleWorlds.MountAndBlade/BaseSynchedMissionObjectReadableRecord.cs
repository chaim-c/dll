using System;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000352 RID: 850
	[DefineSynchedMissionObjectType(typeof(SynchedMissionObject))]
	public struct BaseSynchedMissionObjectReadableRecord
	{
		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x06002E91 RID: 11921 RVA: 0x000BF010 File Offset: 0x000BD210
		// (set) Token: 0x06002E92 RID: 11922 RVA: 0x000BF018 File Offset: 0x000BD218
		public bool SetVisibilityExcludeParents { get; private set; }

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x06002E93 RID: 11923 RVA: 0x000BF021 File Offset: 0x000BD221
		// (set) Token: 0x06002E94 RID: 11924 RVA: 0x000BF029 File Offset: 0x000BD229
		public bool SynchTransform { get; private set; }

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x06002E95 RID: 11925 RVA: 0x000BF032 File Offset: 0x000BD232
		// (set) Token: 0x06002E96 RID: 11926 RVA: 0x000BF03A File Offset: 0x000BD23A
		public MatrixFrame GameObjectFrame { get; private set; }

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x06002E97 RID: 11927 RVA: 0x000BF043 File Offset: 0x000BD243
		// (set) Token: 0x06002E98 RID: 11928 RVA: 0x000BF04B File Offset: 0x000BD24B
		public bool SynchronizeFrameOverTime { get; private set; }

		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x06002E99 RID: 11929 RVA: 0x000BF054 File Offset: 0x000BD254
		// (set) Token: 0x06002E9A RID: 11930 RVA: 0x000BF05C File Offset: 0x000BD25C
		public MatrixFrame LastSynchedFrame { get; private set; }

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x06002E9B RID: 11931 RVA: 0x000BF065 File Offset: 0x000BD265
		// (set) Token: 0x06002E9C RID: 11932 RVA: 0x000BF06D File Offset: 0x000BD26D
		public float Duration { get; private set; }

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x06002E9D RID: 11933 RVA: 0x000BF076 File Offset: 0x000BD276
		// (set) Token: 0x06002E9E RID: 11934 RVA: 0x000BF07E File Offset: 0x000BD27E
		public bool HasSkeleton { get; private set; }

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x06002E9F RID: 11935 RVA: 0x000BF087 File Offset: 0x000BD287
		// (set) Token: 0x06002EA0 RID: 11936 RVA: 0x000BF08F File Offset: 0x000BD28F
		public bool SynchAnimation { get; private set; }

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x06002EA1 RID: 11937 RVA: 0x000BF098 File Offset: 0x000BD298
		// (set) Token: 0x06002EA2 RID: 11938 RVA: 0x000BF0A0 File Offset: 0x000BD2A0
		public int AnimationIndex { get; private set; }

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x06002EA3 RID: 11939 RVA: 0x000BF0A9 File Offset: 0x000BD2A9
		// (set) Token: 0x06002EA4 RID: 11940 RVA: 0x000BF0B1 File Offset: 0x000BD2B1
		public float AnimationSpeed { get; private set; }

		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x06002EA5 RID: 11941 RVA: 0x000BF0BA File Offset: 0x000BD2BA
		// (set) Token: 0x06002EA6 RID: 11942 RVA: 0x000BF0C2 File Offset: 0x000BD2C2
		public float AnimationParameter { get; private set; }

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x06002EA7 RID: 11943 RVA: 0x000BF0CB File Offset: 0x000BD2CB
		// (set) Token: 0x06002EA8 RID: 11944 RVA: 0x000BF0D3 File Offset: 0x000BD2D3
		public bool IsSkeletonAnimationPaused { get; private set; }

		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x06002EA9 RID: 11945 RVA: 0x000BF0DC File Offset: 0x000BD2DC
		// (set) Token: 0x06002EAA RID: 11946 RVA: 0x000BF0E4 File Offset: 0x000BD2E4
		public bool SynchColors { get; private set; }

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x06002EAB RID: 11947 RVA: 0x000BF0ED File Offset: 0x000BD2ED
		// (set) Token: 0x06002EAC RID: 11948 RVA: 0x000BF0F5 File Offset: 0x000BD2F5
		public uint Color { get; private set; }

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x06002EAD RID: 11949 RVA: 0x000BF0FE File Offset: 0x000BD2FE
		// (set) Token: 0x06002EAE RID: 11950 RVA: 0x000BF106 File Offset: 0x000BD306
		public uint Color2 { get; private set; }

		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x06002EAF RID: 11951 RVA: 0x000BF10F File Offset: 0x000BD30F
		// (set) Token: 0x06002EB0 RID: 11952 RVA: 0x000BF117 File Offset: 0x000BD317
		public bool IsDisabled { get; private set; }

		// Token: 0x06002EB1 RID: 11953 RVA: 0x000BF120 File Offset: 0x000BD320
		public bool ReadFromNetwork(ref bool bufferReadValid)
		{
			this.SetVisibilityExcludeParents = GameNetworkMessage.ReadBoolFromPacket(ref bufferReadValid);
			this.SynchTransform = GameNetworkMessage.ReadBoolFromPacket(ref bufferReadValid);
			if (this.SynchTransform)
			{
				this.GameObjectFrame = GameNetworkMessage.ReadMatrixFrameFromPacket(ref bufferReadValid);
				this.SynchronizeFrameOverTime = GameNetworkMessage.ReadBoolFromPacket(ref bufferReadValid);
				if (this.SynchronizeFrameOverTime)
				{
					this.LastSynchedFrame = GameNetworkMessage.ReadMatrixFrameFromPacket(ref bufferReadValid);
					this.Duration = GameNetworkMessage.ReadFloatFromPacket(CompressionMission.FlagCapturePointDurationCompressionInfo, ref bufferReadValid);
				}
			}
			this.HasSkeleton = GameNetworkMessage.ReadBoolFromPacket(ref bufferReadValid);
			if (this.HasSkeleton)
			{
				this.SynchAnimation = GameNetworkMessage.ReadBoolFromPacket(ref bufferReadValid);
				if (this.SynchAnimation)
				{
					this.AnimationIndex = GameNetworkMessage.ReadIntFromPacket(CompressionBasic.AnimationIndexCompressionInfo, ref bufferReadValid);
					this.AnimationSpeed = GameNetworkMessage.ReadFloatFromPacket(CompressionBasic.AnimationSpeedCompressionInfo, ref bufferReadValid);
					this.AnimationParameter = GameNetworkMessage.ReadFloatFromPacket(CompressionBasic.AnimationProgressCompressionInfo, ref bufferReadValid);
					this.IsSkeletonAnimationPaused = GameNetworkMessage.ReadBoolFromPacket(ref bufferReadValid);
				}
			}
			this.SynchColors = GameNetworkMessage.ReadBoolFromPacket(ref bufferReadValid);
			if (this.SynchColors)
			{
				this.Color = GameNetworkMessage.ReadUintFromPacket(CompressionBasic.ColorCompressionInfo, ref bufferReadValid);
				this.Color2 = GameNetworkMessage.ReadUintFromPacket(CompressionBasic.ColorCompressionInfo, ref bufferReadValid);
			}
			this.IsDisabled = GameNetworkMessage.ReadBoolFromPacket(ref bufferReadValid);
			return bufferReadValid;
		}

		// Token: 0x06002EB2 RID: 11954 RVA: 0x000BF238 File Offset: 0x000BD438
		public static ValueTuple<BaseSynchedMissionObjectReadableRecord, ISynchedMissionObjectReadableRecord> CreateFromNetworkWithTypeIndex(int typeIndex)
		{
			bool flag = true;
			BaseSynchedMissionObjectReadableRecord item = default(BaseSynchedMissionObjectReadableRecord);
			item.ReadFromNetwork(ref flag);
			ISynchedMissionObjectReadableRecord synchedMissionObjectReadableRecord = null;
			if (typeIndex >= 0)
			{
				synchedMissionObjectReadableRecord = (Activator.CreateInstance(GameNetwork.GetSynchedMissionObjectReadableRecordTypeFromIndex(typeIndex)) as ISynchedMissionObjectReadableRecord);
				synchedMissionObjectReadableRecord.ReadFromNetwork(ref flag);
			}
			return new ValueTuple<BaseSynchedMissionObjectReadableRecord, ISynchedMissionObjectReadableRecord>(item, synchedMissionObjectReadableRecord);
		}
	}
}
