using System;
using TaleWorlds.DotNet;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x02000099 RID: 153
	[EngineStruct("rglWorld_position::Plain_world_position", false)]
	public struct WorldPosition
	{
		// Token: 0x06000BA2 RID: 2978 RVA: 0x0000CD55 File Offset: 0x0000AF55
		internal WorldPosition(UIntPtr scenePointer, Vec3 position)
		{
			this = new WorldPosition(scenePointer, UIntPtr.Zero, position, false);
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x0000CD68 File Offset: 0x0000AF68
		internal WorldPosition(UIntPtr scenePointer, UIntPtr navMesh, Vec3 position, bool hasValidZ)
		{
			this._scene = scenePointer;
			this._navMesh = navMesh;
			this._nearestNavMesh = this._navMesh;
			this._position = position;
			this.Normal = Vec3.Zero;
			if (hasValidZ)
			{
				this._lastValidZPosition = this._position.AsVec2;
				this.State = ZValidityState.Valid;
				return;
			}
			this._lastValidZPosition = Vec2.Invalid;
			this.State = ZValidityState.Invalid;
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x0000CDD0 File Offset: 0x0000AFD0
		public WorldPosition(Scene scene, Vec3 position)
		{
			this = new WorldPosition((scene != null) ? scene.Pointer : UIntPtr.Zero, UIntPtr.Zero, position, false);
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x0000CDF5 File Offset: 0x0000AFF5
		public WorldPosition(Scene scene, UIntPtr navMesh, Vec3 position, bool hasValidZ)
		{
			this = new WorldPosition((scene != null) ? scene.Pointer : UIntPtr.Zero, navMesh, position, hasValidZ);
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x0000CE18 File Offset: 0x0000B018
		public void SetVec3(UIntPtr navMesh, Vec3 position, bool hasValidZ)
		{
			this._navMesh = navMesh;
			this._nearestNavMesh = this._navMesh;
			this._position = position;
			this.Normal = Vec3.Zero;
			if (hasValidZ)
			{
				this._lastValidZPosition = this._position.AsVec2;
				this.State = ZValidityState.Valid;
				return;
			}
			this._lastValidZPosition = Vec2.Invalid;
			this.State = ZValidityState.Invalid;
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000BA7 RID: 2983 RVA: 0x0000CE78 File Offset: 0x0000B078
		public Vec2 AsVec2
		{
			get
			{
				return this._position.AsVec2;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000BA8 RID: 2984 RVA: 0x0000CE85 File Offset: 0x0000B085
		public float X
		{
			get
			{
				return this._position.x;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000BA9 RID: 2985 RVA: 0x0000CE92 File Offset: 0x0000B092
		public float Y
		{
			get
			{
				return this._position.y;
			}
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x0000CE9F File Offset: 0x0000B09F
		private void ValidateZ(ZValidityState minimumValidityState)
		{
			if (this.State < minimumValidityState)
			{
				EngineApplicationInterface.IScene.WorldPositionValidateZ(ref this, (int)minimumValidityState);
			}
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x0000CEB8 File Offset: 0x0000B0B8
		private void ValidateZMT(ZValidityState minimumValidityState)
		{
			if (this.State < minimumValidityState)
			{
				using (new TWSharedMutexReadLock(Scene.PhysicsAndRayCastLock))
				{
					EngineApplicationInterface.IScene.WorldPositionValidateZ(ref this, (int)minimumValidityState);
				}
			}
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x0000CF08 File Offset: 0x0000B108
		public UIntPtr GetNavMesh()
		{
			this.ValidateZ(ZValidityState.ValidAccordingToNavMesh);
			return this._navMesh;
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x0000CF17 File Offset: 0x0000B117
		public UIntPtr GetNearestNavMesh()
		{
			EngineApplicationInterface.IScene.WorldPositionComputeNearestNavMesh(ref this);
			return this._nearestNavMesh;
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x0000CF2A File Offset: 0x0000B12A
		public float GetNavMeshZ()
		{
			this.ValidateZ(ZValidityState.ValidAccordingToNavMesh);
			if (this.State >= ZValidityState.ValidAccordingToNavMesh)
			{
				return this._position.z;
			}
			return float.NaN;
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x0000CF4D File Offset: 0x0000B14D
		public float GetNavMeshZMT()
		{
			this.ValidateZMT(ZValidityState.ValidAccordingToNavMesh);
			if (this.State >= ZValidityState.ValidAccordingToNavMesh)
			{
				return this._position.z;
			}
			return float.NaN;
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x0000CF70 File Offset: 0x0000B170
		public float GetGroundZ()
		{
			this.ValidateZ(ZValidityState.Valid);
			if (this.State >= ZValidityState.Valid)
			{
				return this._position.z;
			}
			return float.NaN;
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x0000CF93 File Offset: 0x0000B193
		public float GetGroundZMT()
		{
			this.ValidateZMT(ZValidityState.Valid);
			if (this.State >= ZValidityState.Valid)
			{
				return this._position.z;
			}
			return float.NaN;
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x0000CFB6 File Offset: 0x0000B1B6
		public Vec3 GetNavMeshVec3()
		{
			return new Vec3(this._position.AsVec2, this.GetNavMeshZ(), -1f);
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x0000CFD3 File Offset: 0x0000B1D3
		public Vec3 GetNavMeshVec3MT()
		{
			return new Vec3(this._position.AsVec2, this.GetNavMeshZMT(), -1f);
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x0000CFF0 File Offset: 0x0000B1F0
		public Vec3 GetGroundVec3()
		{
			return new Vec3(this._position.AsVec2, this.GetGroundZ(), -1f);
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x0000D00D File Offset: 0x0000B20D
		public Vec3 GetGroundVec3MT()
		{
			return new Vec3(this._position.AsVec2, this.GetGroundZMT(), -1f);
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x0000D02C File Offset: 0x0000B22C
		public void SetVec2(Vec2 value)
		{
			if (this._position.AsVec2 != value)
			{
				if (this.State != ZValidityState.Invalid)
				{
					this.State = ZValidityState.Invalid;
				}
				else if (!this._lastValidZPosition.IsValid)
				{
					this.ValidateZ(ZValidityState.ValidAccordingToNavMesh);
					this.State = ZValidityState.Invalid;
				}
				this._position.x = value.x;
				this._position.y = value.y;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000BB7 RID: 2999 RVA: 0x0000D09C File Offset: 0x0000B29C
		public bool IsValid
		{
			get
			{
				return this.AsVec2.IsValid && this._scene != UIntPtr.Zero;
			}
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x0000D0CC File Offset: 0x0000B2CC
		public float DistanceSquaredWithLimit(in Vec3 targetPoint, float limitSquared)
		{
			Vec2 asVec = this._position.AsVec2;
			Vec3 vec = targetPoint;
			float num = asVec.DistanceSquared(vec.AsVec2);
			if (num <= limitSquared)
			{
				return this.GetGroundVec3().DistanceSquared(targetPoint);
			}
			return num;
		}

		// Token: 0x040001F3 RID: 499
		private readonly UIntPtr _scene;

		// Token: 0x040001F4 RID: 500
		private UIntPtr _navMesh;

		// Token: 0x040001F5 RID: 501
		private UIntPtr _nearestNavMesh;

		// Token: 0x040001F6 RID: 502
		private Vec3 _position;

		// Token: 0x040001F7 RID: 503
		[CustomEngineStructMemberData("normal_")]
		public Vec3 Normal;

		// Token: 0x040001F8 RID: 504
		private Vec2 _lastValidZPosition;

		// Token: 0x040001F9 RID: 505
		[CustomEngineStructMemberData("z_validity_state_")]
		public ZValidityState State;

		// Token: 0x040001FA RID: 506
		public static readonly WorldPosition Invalid = new WorldPosition(UIntPtr.Zero, UIntPtr.Zero, Vec3.Invalid, false);

		// Token: 0x020000CF RID: 207
		public enum WorldPositionEnforcedCache
		{
			// Token: 0x0400046A RID: 1130
			None,
			// Token: 0x0400046B RID: 1131
			NavMeshVec3,
			// Token: 0x0400046C RID: 1132
			GroundVec3
		}
	}
}
