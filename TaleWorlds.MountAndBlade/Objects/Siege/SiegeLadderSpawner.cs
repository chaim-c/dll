using System;
using System.Collections.Generic;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.Objects.Siege
{
	// Token: 0x02000398 RID: 920
	public class SiegeLadderSpawner : SpawnerBase
	{
		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x060031BC RID: 12732 RVA: 0x000CD110 File Offset: 0x000CB310
		public float UpperStateRotationRadian
		{
			get
			{
				return this.UpperStateRotationDegree * 0.017453292f;
			}
		}

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x060031BD RID: 12733 RVA: 0x000CD11E File Offset: 0x000CB31E
		public float DownStateRotationRadian
		{
			get
			{
				return this.DownStateRotationDegree * 0.017453292f;
			}
		}

		// Token: 0x060031BE RID: 12734 RVA: 0x000CD12C File Offset: 0x000CB32C
		protected internal override void OnEditorInit()
		{
			base.OnEditorInit();
			this._spawnerEditorHelper = new SpawnerEntityEditorHelper(this);
			if (this._spawnerEditorHelper.IsValid)
			{
				this._spawnerEditorHelper.GivePermission("ladder_up_state", new SpawnerEntityEditorHelper.Permission(SpawnerEntityEditorHelper.PermissionType.rotation, SpawnerEntityEditorHelper.Axis.x), new Action<float>(this.OnLadderUpStateChange));
				this._spawnerEditorHelper.GivePermission("ladder_down_state", new SpawnerEntityEditorHelper.Permission(SpawnerEntityEditorHelper.PermissionType.rotation, SpawnerEntityEditorHelper.Axis.x), new Action<float>(this.OnLadderDownStateChange));
			}
			this.OnEditorVariableChanged("UpperStateRotationDegree");
			this.OnEditorVariableChanged("DownStateRotationDegree");
		}

		// Token: 0x060031BF RID: 12735 RVA: 0x000CD1B4 File Offset: 0x000CB3B4
		protected internal override void OnEditorTick(float dt)
		{
			base.OnEditorTick(dt);
			this._spawnerEditorHelper.Tick(dt);
		}

		// Token: 0x060031C0 RID: 12736 RVA: 0x000CD1C9 File Offset: 0x000CB3C9
		private void OnLadderUpStateChange(float rotation)
		{
			if (rotation > -0.20135832f)
			{
				rotation = -0.20135832f;
				this.UpperStateRotationDegree = rotation * 57.29578f;
				this.OnEditorVariableChanged("UpperStateRotationDegree");
				return;
			}
			this.UpperStateRotationDegree = rotation * 57.29578f;
		}

		// Token: 0x060031C1 RID: 12737 RVA: 0x000CD200 File Offset: 0x000CB400
		private void OnLadderDownStateChange(float unusedArgument)
		{
			GameEntity ghostEntityOrChild = this._spawnerEditorHelper.GetGhostEntityOrChild("ladder_down_state");
			this.DownStateRotationDegree = Vec3.AngleBetweenTwoVectors(Vec3.Up, ghostEntityOrChild.GetFrame().rotation.u) * 57.29578f;
		}

		// Token: 0x060031C2 RID: 12738 RVA: 0x000CD244 File Offset: 0x000CB444
		protected internal override void OnEditorVariableChanged(string variableName)
		{
			base.OnEditorVariableChanged(variableName);
			if (variableName == "UpperStateRotationDegree")
			{
				if (this.UpperStateRotationDegree > -11.536982f)
				{
					this.UpperStateRotationDegree = -11.536982f;
				}
				MatrixFrame frame = this._spawnerEditorHelper.GetGhostEntityOrChild("ladder_up_state").GetFrame();
				frame.rotation = Mat3.Identity;
				frame.rotation.RotateAboutSide(this.UpperStateRotationRadian);
				this._spawnerEditorHelper.ChangeStableChildMatrixFrameAndApply("ladder_up_state", frame, true);
				return;
			}
			if (variableName == "DownStateRotationDegree")
			{
				MatrixFrame frame2 = this._spawnerEditorHelper.GetGhostEntityOrChild("ladder_down_state").GetFrame();
				frame2.rotation = Mat3.Identity;
				frame2.rotation.RotateAboutUp(1.5707964f);
				frame2.rotation.RotateAboutSide(this.DownStateRotationRadian);
				this._spawnerEditorHelper.ChangeStableChildMatrixFrameAndApply("ladder_down_state", frame2, true);
			}
		}

		// Token: 0x060031C3 RID: 12739 RVA: 0x000CD328 File Offset: 0x000CB528
		protected internal override bool OnCheckForProblems()
		{
			bool result = base.OnCheckForProblems();
			if (base.Scene.IsMultiplayerScene())
			{
				if (this.OnWallNavMeshId == 0 || this.OnWallNavMeshId % 10 == 1)
				{
					MBEditor.AddEntityWarning(base.GameEntity, "OnWallNavMeshId's ones digit cannot be 1 and OnWallNavMeshId cannot be 0 in a multiplayer scene.");
					result = true;
				}
			}
			else if (this.OnWallNavMeshId == -1 || this.OnWallNavMeshId == 0 || this.OnWallNavMeshId % 10 == 1)
			{
				MBEditor.AddEntityWarning(base.GameEntity, "OnWallNavMeshId's ones digit cannot be 1 and OnWallNavMeshId cannot be -1 or 0 in a singleplayer scene.");
				result = true;
			}
			if (this.OnWallNavMeshId != -1)
			{
				List<GameEntity> list = new List<GameEntity>();
				base.Scene.GetEntities(ref list);
				foreach (GameEntity gameEntity in list)
				{
					SiegeLadderSpawner firstScriptOfType = gameEntity.GetFirstScriptOfType<SiegeLadderSpawner>();
					if (firstScriptOfType != null && gameEntity != base.GameEntity && this.OnWallNavMeshId == firstScriptOfType.OnWallNavMeshId && base.GameEntity.GetVisibilityLevelMaskIncludingParents() == gameEntity.GetVisibilityLevelMaskIncludingParents())
					{
						MBEditor.AddEntityWarning(base.GameEntity, "OnWallNavMeshId must not be shared with any other siege ladder.");
					}
				}
			}
			return result;
		}

		// Token: 0x060031C4 RID: 12740 RVA: 0x000CD448 File Offset: 0x000CB648
		protected internal override void OnPreInit()
		{
			base.OnPreInit();
			this._spawnerMissionHelper = new SpawnerEntityMissionHelper(this, false);
		}

		// Token: 0x060031C5 RID: 12741 RVA: 0x000CD460 File Offset: 0x000CB660
		public override void AssignParameters(SpawnerEntityMissionHelper _spawnerMissionHelper)
		{
			SiegeLadder firstScriptOfType = _spawnerMissionHelper.SpawnedEntity.GetFirstScriptOfType<SiegeLadder>();
			firstScriptOfType.AddOnDeployTag = this.AddOnDeployTag;
			firstScriptOfType.RemoveOnDeployTag = this.RemoveOnDeployTag;
			firstScriptOfType.AssignParametersFromSpawner(this.SideTag, this.TargetWallSegmentTag, this.OnWallNavMeshId, this.DownStateRotationRadian, this.UpperStateRotationRadian, this.BarrierTagToRemove, this.IndestructibleMerlonsTag);
			List<GameEntity> list = new List<GameEntity>();
			_spawnerMissionHelper.SpawnedEntity.GetChildrenRecursive(ref list);
			list.Find((GameEntity x) => x.Name == "initial_wait_pos").GetFirstScriptOfType<TacticalPosition>().SetWidth(this.TacticalPositionWidth);
		}

		// Token: 0x0400155D RID: 5469
		[SpawnerBase.SpawnerPermissionField]
		public MatrixFrame fork_holder = MatrixFrame.Zero;

		// Token: 0x0400155E RID: 5470
		[SpawnerBase.SpawnerPermissionField]
		public MatrixFrame initial_wait_pos = MatrixFrame.Zero;

		// Token: 0x0400155F RID: 5471
		[SpawnerBase.SpawnerPermissionField]
		public MatrixFrame use_push = MatrixFrame.Zero;

		// Token: 0x04001560 RID: 5472
		[SpawnerBase.SpawnerPermissionField]
		public MatrixFrame stand_position_wall_push = MatrixFrame.Zero;

		// Token: 0x04001561 RID: 5473
		[SpawnerBase.SpawnerPermissionField]
		public MatrixFrame distance_holder = MatrixFrame.Zero;

		// Token: 0x04001562 RID: 5474
		[SpawnerBase.SpawnerPermissionField]
		public MatrixFrame stand_position_ground_wait = MatrixFrame.Zero;

		// Token: 0x04001563 RID: 5475
		[EditorVisibleScriptComponentVariable(true)]
		public string SideTag;

		// Token: 0x04001564 RID: 5476
		[EditorVisibleScriptComponentVariable(true)]
		public string TargetWallSegmentTag = "";

		// Token: 0x04001565 RID: 5477
		[EditorVisibleScriptComponentVariable(true)]
		public int OnWallNavMeshId = -1;

		// Token: 0x04001566 RID: 5478
		[EditorVisibleScriptComponentVariable(true)]
		public string AddOnDeployTag = "";

		// Token: 0x04001567 RID: 5479
		[EditorVisibleScriptComponentVariable(true)]
		public string RemoveOnDeployTag = "";

		// Token: 0x04001568 RID: 5480
		[EditorVisibleScriptComponentVariable(true)]
		public float UpperStateRotationDegree;

		// Token: 0x04001569 RID: 5481
		[EditorVisibleScriptComponentVariable(true)]
		public float DownStateRotationDegree = 90f;

		// Token: 0x0400156A RID: 5482
		public float TacticalPositionWidth = 1f;

		// Token: 0x0400156B RID: 5483
		[EditorVisibleScriptComponentVariable(true)]
		public string BarrierTagToRemove = string.Empty;

		// Token: 0x0400156C RID: 5484
		[EditorVisibleScriptComponentVariable(true)]
		public string IndestructibleMerlonsTag = string.Empty;
	}
}
