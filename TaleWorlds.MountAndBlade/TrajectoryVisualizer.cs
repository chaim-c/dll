using System;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200034C RID: 844
	public class TrajectoryVisualizer : ScriptComponentBehavior
	{
		// Token: 0x06002E26 RID: 11814 RVA: 0x000BBEA8 File Offset: 0x000BA0A8
		public void SetTrajectoryParams(Vec3 missileShootingPositionOffset, float missileSpeed, float verticalAngleMinInDegrees, float verticalAngleMaxInDegrees, float horizontalAngleRangeInDegrees, float airFrictionConstant)
		{
			this._trajectoryParams.MissileShootingPositionOffset = missileShootingPositionOffset;
			this._trajectoryParams.MissileSpeed = missileSpeed;
			this._trajectoryParams.VerticalAngleMinInDegrees = verticalAngleMinInDegrees;
			this._trajectoryParams.VerticalAngleMaxInDegrees = verticalAngleMaxInDegrees;
			this._trajectoryParams.HorizontalAngleRangeInDegrees = horizontalAngleRangeInDegrees;
			this._trajectoryParams.AirFrictionConstant = airFrictionConstant;
			this._trajectoryParams.IsValid = true;
		}

		// Token: 0x06002E27 RID: 11815 RVA: 0x000BBF0C File Offset: 0x000BA10C
		protected internal override void OnEditorInit()
		{
			base.OnEditorInit();
		}

		// Token: 0x06002E28 RID: 11816 RVA: 0x000BBF14 File Offset: 0x000BA114
		protected internal override void OnEditorVariableChanged(string variableName)
		{
			if (variableName == "ShowTrajectory")
			{
				if (this.ShowTrajectory && this._trajectoryMeshHolder == null && !base.GameEntity.IsGhostObject() && this._trajectoryParams.IsValid)
				{
					this._trajectoryMeshHolder = GameEntity.CreateEmpty(base.Scene, false);
					if (this._trajectoryMeshHolder != null)
					{
						this._trajectoryMeshHolder.EntityFlags |= EntityFlags.DontSaveToScene;
						MatrixFrame globalFrame = base.GameEntity.GetGlobalFrame();
						Vec3 origin = globalFrame.origin + (globalFrame.rotation.s * this._trajectoryParams.MissileShootingPositionOffset.x + globalFrame.rotation.f * this._trajectoryParams.MissileShootingPositionOffset.y + globalFrame.rotation.u * this._trajectoryParams.MissileShootingPositionOffset.z);
						globalFrame.origin = origin;
						this._trajectoryMeshHolder.SetGlobalFrame(globalFrame);
						this._trajectoryMeshHolder.ComputeTrajectoryVolume(this._trajectoryParams.MissileSpeed, this._trajectoryParams.VerticalAngleMaxInDegrees, this._trajectoryParams.VerticalAngleMinInDegrees, this._trajectoryParams.HorizontalAngleRangeInDegrees, this._trajectoryParams.AirFrictionConstant);
						base.GameEntity.AddChild(this._trajectoryMeshHolder, true);
						this._trajectoryMeshHolder.SetVisibilityExcludeParents(false);
					}
				}
				if (this._trajectoryMeshHolder != null)
				{
					this._trajectoryMeshHolder.SetVisibilityExcludeParents(this.ShowTrajectory);
				}
			}
		}

		// Token: 0x06002E29 RID: 11817 RVA: 0x000BC0BD File Offset: 0x000BA2BD
		protected override void OnRemoved(int removeReason)
		{
			if (this._trajectoryMeshHolder != null)
			{
				this._trajectoryMeshHolder.Remove(removeReason);
			}
		}

		// Token: 0x0400133C RID: 4924
		public bool ShowTrajectory;

		// Token: 0x0400133D RID: 4925
		private GameEntity _trajectoryMeshHolder;

		// Token: 0x0400133E RID: 4926
		private TrajectoryVisualizer.TrajectoryParams _trajectoryParams;

		// Token: 0x0200060A RID: 1546
		private struct TrajectoryParams
		{
			// Token: 0x04001F63 RID: 8035
			public Vec3 MissileShootingPositionOffset;

			// Token: 0x04001F64 RID: 8036
			public float MissileSpeed;

			// Token: 0x04001F65 RID: 8037
			public float VerticalAngleMinInDegrees;

			// Token: 0x04001F66 RID: 8038
			public float VerticalAngleMaxInDegrees;

			// Token: 0x04001F67 RID: 8039
			public float HorizontalAngleRangeInDegrees;

			// Token: 0x04001F68 RID: 8040
			public float AirFrictionConstant;

			// Token: 0x04001F69 RID: 8041
			public bool IsValid;
		}
	}
}
