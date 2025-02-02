using System;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.Objects.Siege
{
	// Token: 0x02000399 RID: 921
	public class SiegeTowerSpawner : SpawnerBase
	{
		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x060031C7 RID: 12743 RVA: 0x000CD5B1 File Offset: 0x000CB7B1
		public float RampRotationRadian
		{
			get
			{
				return this.RampRotationDegree * 0.017453292f;
			}
		}

		// Token: 0x060031C8 RID: 12744 RVA: 0x000CD5C0 File Offset: 0x000CB7C0
		protected internal override void OnEditorInit()
		{
			base.OnEditorInit();
			this._spawnerEditorHelper = new SpawnerEntityEditorHelper(this);
			this._spawnerEditorHelper.LockGhostParent = false;
			if (this._spawnerEditorHelper.IsValid)
			{
				this._spawnerEditorHelper.SetupGhostMovement(this.PathEntityName);
				this._spawnerEditorHelper.GivePermission("ramp", new SpawnerEntityEditorHelper.Permission(SpawnerEntityEditorHelper.PermissionType.rotation, SpawnerEntityEditorHelper.Axis.x), new Action<float>(this.SetRampRotation));
				this._spawnerEditorHelper.GivePermission("ai_barrier_r", new SpawnerEntityEditorHelper.Permission(SpawnerEntityEditorHelper.PermissionType.scale, SpawnerEntityEditorHelper.Axis.z), new Action<float>(this.SetAIBarrierRight));
				this._spawnerEditorHelper.GivePermission("ai_barrier_l", new SpawnerEntityEditorHelper.Permission(SpawnerEntityEditorHelper.PermissionType.scale, SpawnerEntityEditorHelper.Axis.z), new Action<float>(this.SetAIBarrierLeft));
			}
			this.OnEditorVariableChanged("RampRotationDegree");
			this.OnEditorVariableChanged("BarrierLength");
		}

		// Token: 0x060031C9 RID: 12745 RVA: 0x000CD688 File Offset: 0x000CB888
		private void SetRampRotation(float unusedArgument)
		{
			MatrixFrame frame = this._spawnerEditorHelper.GetGhostEntityOrChild("ramp").GetFrame();
			Vec3 vec = new Vec3(-frame.rotation.u.y, frame.rotation.u.x, 0f, -1f);
			float z = frame.rotation.u.z;
			float num = MathF.Atan2(vec.Length, z);
			if ((double)vec.x < 0.0)
			{
				num = -num;
				num += 6.2831855f;
			}
			float num2 = num;
			this.RampRotationDegree = num2 * 57.29578f;
		}

		// Token: 0x060031CA RID: 12746 RVA: 0x000CD72C File Offset: 0x000CB92C
		private void SetAIBarrierRight(float barrierScale)
		{
			this.BarrierLength = barrierScale;
			MatrixFrame frame = this._spawnerEditorHelper.GetGhostEntityOrChild("ai_barrier_l").GetFrame();
			MatrixFrame frame2 = this._spawnerEditorHelper.GetGhostEntityOrChild("ai_barrier_r").GetFrame();
			frame.rotation.u = frame2.rotation.u;
			this._spawnerEditorHelper.ChangeStableChildMatrixFrameAndApply("ai_barrier_l", frame, false);
		}

		// Token: 0x060031CB RID: 12747 RVA: 0x000CD798 File Offset: 0x000CB998
		private void SetAIBarrierLeft(float barrierScale)
		{
			this.BarrierLength = barrierScale;
			MatrixFrame frame = this._spawnerEditorHelper.GetGhostEntityOrChild("ai_barrier_l").GetFrame();
			MatrixFrame frame2 = this._spawnerEditorHelper.GetGhostEntityOrChild("ai_barrier_r").GetFrame();
			frame2.rotation.u = frame.rotation.u;
			this._spawnerEditorHelper.ChangeStableChildMatrixFrameAndApply("ai_barrier_r", frame2, false);
		}

		// Token: 0x060031CC RID: 12748 RVA: 0x000CD801 File Offset: 0x000CBA01
		protected internal override void OnEditorTick(float dt)
		{
			base.OnEditorTick(dt);
			this._spawnerEditorHelper.Tick(dt);
		}

		// Token: 0x060031CD RID: 12749 RVA: 0x000CD818 File Offset: 0x000CBA18
		protected internal override void OnEditorVariableChanged(string variableName)
		{
			base.OnEditorVariableChanged(variableName);
			if (variableName == "PathEntityName")
			{
				this._spawnerEditorHelper.SetupGhostMovement(this.PathEntityName);
				return;
			}
			if (variableName == "EnableAutoGhostMovement")
			{
				this._spawnerEditorHelper.SetEnableAutoGhostMovement(this.EnableAutoGhostMovement);
				return;
			}
			if (variableName == "RampRotationDegree")
			{
				MatrixFrame frame = this._spawnerEditorHelper.GetGhostEntityOrChild("ramp").GetFrame();
				frame.rotation = Mat3.Identity;
				frame.rotation.RotateAboutSide(this.RampRotationRadian);
				this._spawnerEditorHelper.ChangeStableChildMatrixFrameAndApply("ramp", frame, true);
				return;
			}
			if (variableName == "BarrierLength")
			{
				MatrixFrame frame2 = this._spawnerEditorHelper.GetGhostEntityOrChild("ai_barrier_l").GetFrame();
				frame2.rotation.u.Normalize();
				frame2.rotation.u = frame2.rotation.u * MathF.Max(0.01f, MathF.Abs(this.BarrierLength));
				MatrixFrame frame3 = this._spawnerEditorHelper.GetGhostEntityOrChild("ai_barrier_r").GetFrame();
				frame3.rotation.u = frame2.rotation.u;
				this._spawnerEditorHelper.ChangeStableChildMatrixFrameAndApply("ai_barrier_l", frame2, true);
				this._spawnerEditorHelper.ChangeStableChildMatrixFrameAndApply("ai_barrier_r", frame3, true);
				return;
			}
			if (variableName == "SpeedModifierFactor")
			{
				this.SpeedModifierFactor = MathF.Clamp(this.SpeedModifierFactor, 0.8f, 1.2f);
			}
		}

		// Token: 0x060031CE RID: 12750 RVA: 0x000CD9A1 File Offset: 0x000CBBA1
		protected internal override void OnPreInit()
		{
			base.OnPreInit();
			this._spawnerMissionHelper = new SpawnerEntityMissionHelper(this, false);
		}

		// Token: 0x060031CF RID: 12751 RVA: 0x000CD9B8 File Offset: 0x000CBBB8
		public override void AssignParameters(SpawnerEntityMissionHelper _spawnerMissionHelper)
		{
			SiegeTower firstScriptOfType = _spawnerMissionHelper.SpawnedEntity.GetFirstScriptOfType<SiegeTower>();
			firstScriptOfType.AddOnDeployTag = this.AddOnDeployTag;
			firstScriptOfType.RemoveOnDeployTag = this.RemoveOnDeployTag;
			firstScriptOfType.MaxSpeed *= this.SpeedModifierFactor;
			firstScriptOfType.MinSpeed *= this.SpeedModifierFactor;
			Mat3 identity = Mat3.Identity;
			identity.RotateAboutSide(this.RampRotationRadian);
			firstScriptOfType.AssignParametersFromSpawner(this.PathEntityName, this.TargetWallSegmentTag, this.SideTag, this.SoilNavMeshID1, this.SoilNavMeshID2, this.DitchNavMeshID1, this.DitchNavMeshID2, this.GroundToSoilNavMeshID1, this.GroundToSoilNavMeshID2, this.SoilGenericNavMeshID, this.GroundGenericNavMeshID, identity, this.BarrierTagToRemove);
		}

		// Token: 0x0400156D RID: 5485
		private const float _modifierFactorUpperLimit = 1.2f;

		// Token: 0x0400156E RID: 5486
		private const float _modifierFactorLowerLimit = 0.8f;

		// Token: 0x0400156F RID: 5487
		[SpawnerBase.SpawnerPermissionField]
		public MatrixFrame wait_pos_ground = MatrixFrame.Zero;

		// Token: 0x04001570 RID: 5488
		[EditorVisibleScriptComponentVariable(true)]
		public string SideTag;

		// Token: 0x04001571 RID: 5489
		[EditorVisibleScriptComponentVariable(true)]
		public string TargetWallSegmentTag = "";

		// Token: 0x04001572 RID: 5490
		[EditorVisibleScriptComponentVariable(true)]
		public string PathEntityName = "Path";

		// Token: 0x04001573 RID: 5491
		[EditorVisibleScriptComponentVariable(true)]
		public int SoilNavMeshID1 = -1;

		// Token: 0x04001574 RID: 5492
		[EditorVisibleScriptComponentVariable(true)]
		public int SoilNavMeshID2 = -1;

		// Token: 0x04001575 RID: 5493
		[EditorVisibleScriptComponentVariable(true)]
		public int DitchNavMeshID1 = -1;

		// Token: 0x04001576 RID: 5494
		[EditorVisibleScriptComponentVariable(true)]
		public int DitchNavMeshID2 = -1;

		// Token: 0x04001577 RID: 5495
		[EditorVisibleScriptComponentVariable(true)]
		public int GroundToSoilNavMeshID1 = -1;

		// Token: 0x04001578 RID: 5496
		[EditorVisibleScriptComponentVariable(true)]
		public int GroundToSoilNavMeshID2 = -1;

		// Token: 0x04001579 RID: 5497
		[EditorVisibleScriptComponentVariable(true)]
		public int SoilGenericNavMeshID = -1;

		// Token: 0x0400157A RID: 5498
		[EditorVisibleScriptComponentVariable(true)]
		public int GroundGenericNavMeshID = -1;

		// Token: 0x0400157B RID: 5499
		[EditorVisibleScriptComponentVariable(true)]
		public string AddOnDeployTag = "";

		// Token: 0x0400157C RID: 5500
		[EditorVisibleScriptComponentVariable(true)]
		public string RemoveOnDeployTag = "";

		// Token: 0x0400157D RID: 5501
		[EditorVisibleScriptComponentVariable(true)]
		public float RampRotationDegree;

		// Token: 0x0400157E RID: 5502
		[EditorVisibleScriptComponentVariable(true)]
		public float BarrierLength = 1f;

		// Token: 0x0400157F RID: 5503
		[EditorVisibleScriptComponentVariable(true)]
		public float SpeedModifierFactor = 1f;

		// Token: 0x04001580 RID: 5504
		public bool EnableAutoGhostMovement;

		// Token: 0x04001581 RID: 5505
		[SpawnerBase.SpawnerPermissionField]
		[RestrictedAccess]
		public MatrixFrame ai_barrier_l = MatrixFrame.Zero;

		// Token: 0x04001582 RID: 5506
		[SpawnerBase.SpawnerPermissionField]
		[RestrictedAccess]
		public MatrixFrame ai_barrier_r = MatrixFrame.Zero;

		// Token: 0x04001583 RID: 5507
		[EditorVisibleScriptComponentVariable(true)]
		public string BarrierTagToRemove = string.Empty;
	}
}
