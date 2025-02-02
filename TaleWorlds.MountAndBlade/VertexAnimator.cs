using System;
using System.Collections.Generic;
using NetworkMessages.FromServer;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200036C RID: 876
	public class VertexAnimator : SynchedMissionObject
	{
		// Token: 0x06003050 RID: 12368 RVA: 0x000C7853 File Offset: 0x000C5A53
		public VertexAnimator()
		{
			this.Speed = 20f;
		}

		// Token: 0x06003051 RID: 12369 RVA: 0x000C7871 File Offset: 0x000C5A71
		private void SetIsPlaying(bool value)
		{
			if (this._isPlaying != value)
			{
				this._isPlaying = value;
				base.SetScriptComponentToTick(this.GetTickRequirement());
			}
		}

		// Token: 0x06003052 RID: 12370 RVA: 0x000C788F File Offset: 0x000C5A8F
		protected internal override void OnInit()
		{
			base.OnInit();
			this.RefreshEditDataUsers();
			this.SetIsPlaying(true);
			base.SetScriptComponentToTick(this.GetTickRequirement());
		}

		// Token: 0x06003053 RID: 12371 RVA: 0x000C78B0 File Offset: 0x000C5AB0
		protected internal override void OnEditorInit()
		{
			this.OnInit();
		}

		// Token: 0x06003054 RID: 12372 RVA: 0x000C78B8 File Offset: 0x000C5AB8
		public override ScriptComponentBehavior.TickRequirement GetTickRequirement()
		{
			if (this._isPlaying)
			{
				return ScriptComponentBehavior.TickRequirement.Tick | base.GetTickRequirement();
			}
			return base.GetTickRequirement();
		}

		// Token: 0x06003055 RID: 12373 RVA: 0x000C78D4 File Offset: 0x000C5AD4
		protected internal override void OnTick(float dt)
		{
			base.OnTick(dt);
			if (this._isPlaying)
			{
				if (this._curAnimTime < (float)this.BeginKey)
				{
					this._curAnimTime = (float)this.BeginKey;
				}
				base.GameEntity.SetMorphFrameOfComponents(this._curAnimTime);
				this._curAnimTime += dt * this.Speed;
				if (this._curAnimTime > (float)this.EndKey)
				{
					if (this._curAnimTime > (float)this.EndKey && this._playOnce)
					{
						this.SetIsPlaying(false);
						this._curAnimTime = (float)this.EndKey;
						base.GameEntity.SetMorphFrameOfComponents(this._curAnimTime);
						return;
					}
					int num = 0;
					while (this._curAnimTime > (float)this.EndKey && ++num < 100)
					{
						this._curAnimTime = (float)this.BeginKey + (this._curAnimTime - (float)this.EndKey);
					}
				}
			}
		}

		// Token: 0x06003056 RID: 12374 RVA: 0x000C79B9 File Offset: 0x000C5BB9
		public void PlayOnce()
		{
			this.Play();
			this._playOnce = true;
		}

		// Token: 0x06003057 RID: 12375 RVA: 0x000C79C8 File Offset: 0x000C5BC8
		public void Pause()
		{
			this.SetIsPlaying(false);
		}

		// Token: 0x06003058 RID: 12376 RVA: 0x000C79D1 File Offset: 0x000C5BD1
		public void Play()
		{
			this.Stop();
			this.Resume();
		}

		// Token: 0x06003059 RID: 12377 RVA: 0x000C79DF File Offset: 0x000C5BDF
		public void Resume()
		{
			this.SetIsPlaying(true);
		}

		// Token: 0x0600305A RID: 12378 RVA: 0x000C79E8 File Offset: 0x000C5BE8
		public void Stop()
		{
			this.SetIsPlaying(false);
			this._curAnimTime = (float)this.BeginKey;
			Mesh firstMesh = base.GameEntity.GetFirstMesh();
			if (firstMesh != null)
			{
				firstMesh.MorphTime = this._curAnimTime;
			}
		}

		// Token: 0x0600305B RID: 12379 RVA: 0x000C7A2C File Offset: 0x000C5C2C
		public void StopAndGoToEnd()
		{
			this.SetIsPlaying(false);
			this._curAnimTime = (float)this.EndKey;
			Mesh firstMesh = base.GameEntity.GetFirstMesh();
			if (firstMesh != null)
			{
				firstMesh.MorphTime = this._curAnimTime;
			}
		}

		// Token: 0x0600305C RID: 12380 RVA: 0x000C7A6E File Offset: 0x000C5C6E
		public void SetAnimation(int beginKey, int endKey, float speed)
		{
			this.BeginKey = beginKey;
			this.EndKey = endKey;
			this.Speed = speed;
		}

		// Token: 0x0600305D RID: 12381 RVA: 0x000C7A88 File Offset: 0x000C5C88
		public void SetAnimationSynched(int beginKey, int endKey, float speed)
		{
			if (beginKey != this.BeginKey || endKey != this.EndKey || speed != this.Speed)
			{
				this.BeginKey = beginKey;
				this.EndKey = endKey;
				this.Speed = speed;
				if (GameNetwork.IsServerOrRecorder)
				{
					GameNetwork.BeginBroadcastModuleEvent();
					GameNetwork.WriteMessage(new SetMissionObjectVertexAnimation(base.Id, beginKey, endKey, speed));
					GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
				}
			}
		}

		// Token: 0x0600305E RID: 12382 RVA: 0x000C7AEC File Offset: 0x000C5CEC
		public void SetProgressSynched(float value)
		{
			if (MathF.Abs(this.Progress - value) > 0.0001f)
			{
				if (GameNetwork.IsServerOrRecorder)
				{
					GameNetwork.BeginBroadcastModuleEvent();
					GameNetwork.WriteMessage(new SetMissionObjectVertexAnimationProgress(base.Id, value));
					GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
				}
				this.Progress = value;
			}
		}

		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x0600305F RID: 12383 RVA: 0x000C7B39 File Offset: 0x000C5D39
		// (set) Token: 0x06003060 RID: 12384 RVA: 0x000C7B58 File Offset: 0x000C5D58
		private float Progress
		{
			get
			{
				return (this._curAnimTime - (float)this.BeginKey) / (float)(this.EndKey - this.BeginKey);
			}
			set
			{
				this._curAnimTime = (float)this.BeginKey + value * (float)(this.EndKey - this.BeginKey);
				Mesh firstMesh = base.GameEntity.GetFirstMesh();
				if (firstMesh != null)
				{
					firstMesh.MorphTime = this._curAnimTime;
				}
			}
		}

		// Token: 0x06003061 RID: 12385 RVA: 0x000C7BA4 File Offset: 0x000C5DA4
		protected override void OnRemoved(int removeReason)
		{
			base.OnRemoved(removeReason);
			int count = this._animatedMeshes.Count;
			for (int i = 0; i < count; i++)
			{
				this._animatedMeshes[i].ReleaseEditDataUser();
			}
		}

		// Token: 0x06003062 RID: 12386 RVA: 0x000C7BE4 File Offset: 0x000C5DE4
		protected internal override void OnEditorTick(float dt)
		{
			int componentCount = base.GameEntity.GetComponentCount(GameEntity.ComponentType.MetaMesh);
			bool flag = false;
			for (int i = 0; i < componentCount; i++)
			{
				MetaMesh metaMesh = base.GameEntity.GetComponentAtIndex(i, GameEntity.ComponentType.MetaMesh) as MetaMesh;
				for (int j = 0; j < metaMesh.MeshCount; j++)
				{
					int count = this._animatedMeshes.Count;
					bool flag2 = false;
					for (int k = 0; k < count; k++)
					{
						if (metaMesh.GetMeshAtIndex(j) == this._animatedMeshes[k])
						{
							flag2 = true;
							break;
						}
					}
					if (!flag2)
					{
						flag = true;
					}
				}
			}
			if (flag)
			{
				this.RefreshEditDataUsers();
			}
			this.OnTick(dt);
		}

		// Token: 0x06003063 RID: 12387 RVA: 0x000C7C8C File Offset: 0x000C5E8C
		private void RefreshEditDataUsers()
		{
			foreach (Mesh mesh in this._animatedMeshes)
			{
				mesh.ReleaseEditDataUser();
			}
			this._animatedMeshes.Clear();
			int componentCount = base.GameEntity.GetComponentCount(GameEntity.ComponentType.MetaMesh);
			for (int i = 0; i < componentCount; i++)
			{
				MetaMesh metaMesh = base.GameEntity.GetComponentAtIndex(i, GameEntity.ComponentType.MetaMesh) as MetaMesh;
				for (int j = 0; j < metaMesh.MeshCount; j++)
				{
					Mesh meshAtIndex = metaMesh.GetMeshAtIndex(j);
					meshAtIndex.AddEditDataUser();
					meshAtIndex.HintVerticesDynamic();
					meshAtIndex.HintIndicesDynamic();
					this._animatedMeshes.Add(meshAtIndex);
					Mesh baseMesh = meshAtIndex.GetBaseMesh();
					if (baseMesh != null)
					{
						baseMesh.AddEditDataUser();
						this._animatedMeshes.Add(baseMesh);
					}
				}
			}
		}

		// Token: 0x06003064 RID: 12388 RVA: 0x000C7D80 File Offset: 0x000C5F80
		public override void WriteToNetwork()
		{
			base.WriteToNetwork();
			GameNetworkMessage.WriteIntToPacket(this.BeginKey, CompressionBasic.AnimationKeyCompressionInfo);
			GameNetworkMessage.WriteIntToPacket(this.EndKey, CompressionBasic.AnimationKeyCompressionInfo);
			GameNetworkMessage.WriteFloatToPacket(this.Speed, CompressionBasic.VertexAnimationSpeedCompressionInfo);
			GameNetworkMessage.WriteFloatToPacket(this.Progress, CompressionBasic.AnimationProgressCompressionInfo);
		}

		// Token: 0x06003065 RID: 12389 RVA: 0x000C7DD4 File Offset: 0x000C5FD4
		public override void OnAfterReadFromNetwork(ValueTuple<BaseSynchedMissionObjectReadableRecord, ISynchedMissionObjectReadableRecord> synchedMissionObjectReadableRecord)
		{
			base.OnAfterReadFromNetwork(synchedMissionObjectReadableRecord);
			VertexAnimator.VertexAnimatorRecord vertexAnimatorRecord = (VertexAnimator.VertexAnimatorRecord)synchedMissionObjectReadableRecord.Item2;
			this.BeginKey = vertexAnimatorRecord.BeginKey;
			this.EndKey = vertexAnimatorRecord.EndKey;
			this.Speed = vertexAnimatorRecord.Speed;
			this.Progress = vertexAnimatorRecord.Progress;
		}

		// Token: 0x04001483 RID: 5251
		public float Speed;

		// Token: 0x04001484 RID: 5252
		public int BeginKey;

		// Token: 0x04001485 RID: 5253
		public int EndKey;

		// Token: 0x04001486 RID: 5254
		private bool _playOnce;

		// Token: 0x04001487 RID: 5255
		private float _curAnimTime;

		// Token: 0x04001488 RID: 5256
		private bool _isPlaying;

		// Token: 0x04001489 RID: 5257
		private readonly List<Mesh> _animatedMeshes = new List<Mesh>();

		// Token: 0x02000625 RID: 1573
		[DefineSynchedMissionObjectType(typeof(VertexAnimator))]
		public struct VertexAnimatorRecord : ISynchedMissionObjectReadableRecord
		{
			// Token: 0x170009F6 RID: 2550
			// (get) Token: 0x06003C46 RID: 15430 RVA: 0x000EA009 File Offset: 0x000E8209
			// (set) Token: 0x06003C47 RID: 15431 RVA: 0x000EA011 File Offset: 0x000E8211
			public int BeginKey { get; private set; }

			// Token: 0x170009F7 RID: 2551
			// (get) Token: 0x06003C48 RID: 15432 RVA: 0x000EA01A File Offset: 0x000E821A
			// (set) Token: 0x06003C49 RID: 15433 RVA: 0x000EA022 File Offset: 0x000E8222
			public int EndKey { get; private set; }

			// Token: 0x170009F8 RID: 2552
			// (get) Token: 0x06003C4A RID: 15434 RVA: 0x000EA02B File Offset: 0x000E822B
			// (set) Token: 0x06003C4B RID: 15435 RVA: 0x000EA033 File Offset: 0x000E8233
			public float Speed { get; private set; }

			// Token: 0x170009F9 RID: 2553
			// (get) Token: 0x06003C4C RID: 15436 RVA: 0x000EA03C File Offset: 0x000E823C
			// (set) Token: 0x06003C4D RID: 15437 RVA: 0x000EA044 File Offset: 0x000E8244
			public float Progress { get; private set; }

			// Token: 0x06003C4E RID: 15438 RVA: 0x000EA050 File Offset: 0x000E8250
			public bool ReadFromNetwork(ref bool bufferReadValid)
			{
				this.BeginKey = GameNetworkMessage.ReadIntFromPacket(CompressionBasic.AnimationKeyCompressionInfo, ref bufferReadValid);
				this.EndKey = GameNetworkMessage.ReadIntFromPacket(CompressionBasic.AnimationKeyCompressionInfo, ref bufferReadValid);
				this.Speed = GameNetworkMessage.ReadFloatFromPacket(CompressionBasic.VertexAnimationSpeedCompressionInfo, ref bufferReadValid);
				this.Progress = GameNetworkMessage.ReadFloatFromPacket(CompressionBasic.AnimationProgressCompressionInfo, ref bufferReadValid);
				return bufferReadValid;
			}
		}
	}
}
