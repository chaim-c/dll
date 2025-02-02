using System;
using TaleWorlds.DotNet;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x0200004C RID: 76
	public class GameEntityWithWorldPosition
	{
		// Token: 0x060006C2 RID: 1730 RVA: 0x00004D68 File Offset: 0x00002F68
		public GameEntityWithWorldPosition(GameEntity gameEntity)
		{
			this._gameEntity = new WeakNativeObjectReference(gameEntity);
			Scene scene = gameEntity.Scene;
			float groundHeightAtPosition = scene.GetGroundHeightAtPosition(gameEntity.GlobalPosition, BodyFlags.CommonCollisionExcludeFlags);
			this._worldPosition = new WorldPosition(scene, UIntPtr.Zero, new Vec3(gameEntity.GlobalPosition.AsVec2, groundHeightAtPosition, -1f), false);
			this._worldPosition.GetGroundVec3();
			this._orthonormalRotation = gameEntity.GetGlobalFrame().rotation;
			this._orthonormalRotation.Orthonormalize();
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060006C3 RID: 1731 RVA: 0x00004DF3 File Offset: 0x00002FF3
		public GameEntity GameEntity
		{
			get
			{
				WeakNativeObjectReference gameEntity = this._gameEntity;
				return ((gameEntity != null) ? gameEntity.GetNativeObject() : null) as GameEntity;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060006C4 RID: 1732 RVA: 0x00004E0C File Offset: 0x0000300C
		public WorldPosition WorldPosition
		{
			get
			{
				Vec3 origin = this.GameEntity.GetGlobalFrame().origin;
				if (!this._worldPosition.AsVec2.NearlyEquals(origin.AsVec2, 1E-05f))
				{
					this._worldPosition.SetVec3(UIntPtr.Zero, origin, false);
				}
				return this._worldPosition;
			}
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x00004E63 File Offset: 0x00003063
		public void InvalidateWorldPosition()
		{
			this._worldPosition.State = ZValidityState.Invalid;
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060006C6 RID: 1734 RVA: 0x00004E74 File Offset: 0x00003074
		public WorldFrame WorldFrame
		{
			get
			{
				Mat3 rotation = this.GameEntity.GetGlobalFrame().rotation;
				if (!rotation.NearlyEquals(this._orthonormalRotation, 1E-05f))
				{
					this._orthonormalRotation = rotation;
					this._orthonormalRotation.Orthonormalize();
				}
				return new WorldFrame(this._orthonormalRotation, this.WorldPosition);
			}
		}

		// Token: 0x040000A3 RID: 163
		private readonly WeakNativeObjectReference _gameEntity;

		// Token: 0x040000A4 RID: 164
		private WorldPosition _worldPosition;

		// Token: 0x040000A5 RID: 165
		private Mat3 _orthonormalRotation;
	}
}
