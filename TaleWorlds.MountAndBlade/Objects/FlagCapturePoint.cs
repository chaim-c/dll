using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.Objects
{
	// Token: 0x02000389 RID: 905
	public class FlagCapturePoint : SynchedMissionObject
	{
		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x06003180 RID: 12672 RVA: 0x000CC49B File Offset: 0x000CA69B
		[EditableScriptComponentVariable(false)]
		public Vec3 Position
		{
			get
			{
				return base.GameEntity.GlobalPosition;
			}
		}

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x06003181 RID: 12673 RVA: 0x000CC4A8 File Offset: 0x000CA6A8
		public int FlagChar
		{
			get
			{
				return 65 + this.FlagIndex;
			}
		}

		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x06003182 RID: 12674 RVA: 0x000CC4B3 File Offset: 0x000CA6B3
		public bool IsContested
		{
			get
			{
				return this._currentDirection == CaptureTheFlagFlagDirection.Down;
			}
		}

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x06003183 RID: 12675 RVA: 0x000CC4BE File Offset: 0x000CA6BE
		public bool IsFullyRaised
		{
			get
			{
				return this._currentDirection == CaptureTheFlagFlagDirection.None;
			}
		}

		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x06003184 RID: 12676 RVA: 0x000CC4C9 File Offset: 0x000CA6C9
		public bool IsDeactivated
		{
			get
			{
				return !base.GameEntity.IsVisibleIncludeParents();
			}
		}

		// Token: 0x06003185 RID: 12677 RVA: 0x000CC4D9 File Offset: 0x000CA6D9
		protected internal override void OnMissionReset()
		{
			this._currentDirection = CaptureTheFlagFlagDirection.None;
		}

		// Token: 0x06003186 RID: 12678 RVA: 0x000CC4E4 File Offset: 0x000CA6E4
		public void ResetPointAsServer(uint defaultColor, uint defaultColor2)
		{
			MatrixFrame globalFrame = this._flagTopBoundary.GetGlobalFrame();
			this._flagHolder.SetGlobalFrameSynched(ref globalFrame, false);
			this.SetTeamColorsWithAllSynched(defaultColor, defaultColor2);
			this.SetVisibleWithAllSynched(true, false);
		}

		// Token: 0x06003187 RID: 12679 RVA: 0x000CC51B File Offset: 0x000CA71B
		public void RemovePointAsServer()
		{
			this.SetVisibleWithAllSynched(false, false);
		}

		// Token: 0x06003188 RID: 12680 RVA: 0x000CC528 File Offset: 0x000CA728
		protected internal override void OnInit()
		{
			this._flagHolder = base.GameEntity.CollectChildrenEntitiesWithTag("score_stand").SingleOrDefault<GameEntity>().GetScriptComponents<SynchedMissionObject>().SingleOrDefault<SynchedMissionObject>();
			this._theFlag = this._flagHolder.GameEntity.CollectChildrenEntitiesWithTag("flag_white").SingleOrDefault<GameEntity>().GetScriptComponents<SynchedMissionObject>().SingleOrDefault<SynchedMissionObject>();
			this._flagBottomBoundary = base.GameEntity.GetChildren().Single((GameEntity q) => q.HasTag("flag_raising_bottom"));
			this._flagTopBoundary = base.GameEntity.GetChildren().Single((GameEntity q) => q.HasTag("flag_raising_top"));
			MatrixFrame globalFrame = this._flagTopBoundary.GetGlobalFrame();
			this._flagHolder.GameEntity.SetGlobalFrame(globalFrame);
			this._flagDependentObjects = new List<SynchedMissionObject>();
			foreach (GameEntity gameEntity in Mission.Current.Scene.FindEntitiesWithTag("depends_flag_" + this.FlagIndex).ToList<GameEntity>())
			{
				this._flagDependentObjects.Add(gameEntity.GetScriptComponents<SynchedMissionObject>().SingleOrDefault<SynchedMissionObject>());
			}
		}

		// Token: 0x06003189 RID: 12681 RVA: 0x000CC690 File Offset: 0x000CA890
		protected internal override void OnEditorTick(float dt)
		{
			base.OnEditorTick(dt);
			if (MBEditor.IsEntitySelected(base.GameEntity))
			{
				DebugExtensions.RenderDebugCircleOnTerrain(base.Scene, base.GameEntity.GetGlobalFrame(), 4f, 2852192000U, true, false);
				DebugExtensions.RenderDebugCircleOnTerrain(base.Scene, base.GameEntity.GetGlobalFrame(), 6f, 2868838400U, true, false);
			}
		}

		// Token: 0x0600318A RID: 12682 RVA: 0x000CC6F8 File Offset: 0x000CA8F8
		public void OnAfterTick(bool canOwnershipChange, out bool ownerTeamChanged)
		{
			ownerTeamChanged = false;
			if (this._flagHolder.SynchronizeCompleted)
			{
				bool flag = this._flagHolder.GameEntity.GlobalPosition.DistanceSquared(this._flagTopBoundary.GlobalPosition).ApproximatelyEqualsTo(0f, 1E-05f);
				if (canOwnershipChange)
				{
					if (!flag)
					{
						ownerTeamChanged = true;
						return;
					}
					this._currentDirection = CaptureTheFlagFlagDirection.None;
					return;
				}
				else if (flag)
				{
					this._currentDirection = CaptureTheFlagFlagDirection.None;
				}
			}
		}

		// Token: 0x0600318B RID: 12683 RVA: 0x000CC764 File Offset: 0x000CA964
		public void SetMoveFlag(CaptureTheFlagFlagDirection directionTo, float speedMultiplier = 1f)
		{
			float flagProgress = this.GetFlagProgress();
			float num = 1f / speedMultiplier;
			float num2 = (directionTo == CaptureTheFlagFlagDirection.Up) ? (1f - flagProgress) : flagProgress;
			float num3 = 10f * num;
			float duration = num2 * num3;
			this._currentDirection = directionTo;
			MatrixFrame frame;
			if (directionTo != CaptureTheFlagFlagDirection.Up)
			{
				if (directionTo != CaptureTheFlagFlagDirection.Down)
				{
					throw new ArgumentOutOfRangeException("directionTo", directionTo, null);
				}
				frame = this._flagBottomBoundary.GetFrame();
			}
			else
			{
				frame = this._flagTopBoundary.GetFrame();
			}
			this._flagHolder.SetFrameSynchedOverTime(ref frame, duration, false);
		}

		// Token: 0x0600318C RID: 12684 RVA: 0x000CC7E7 File Offset: 0x000CA9E7
		public void ChangeMovementSpeed(float speedMultiplier)
		{
			if (this._currentDirection != CaptureTheFlagFlagDirection.None)
			{
				this.SetMoveFlag(this._currentDirection, speedMultiplier);
			}
		}

		// Token: 0x0600318D RID: 12685 RVA: 0x000CC800 File Offset: 0x000CAA00
		public void SetMoveNone()
		{
			this._currentDirection = CaptureTheFlagFlagDirection.None;
			MatrixFrame frame = this._flagHolder.GameEntity.GetFrame();
			this._flagHolder.SetFrameSynched(ref frame, false);
		}

		// Token: 0x0600318E RID: 12686 RVA: 0x000CC834 File Offset: 0x000CAA34
		public void SetVisibleWithAllSynched(bool value, bool forceChildrenVisible = false)
		{
			this.SetVisibleSynched(value, forceChildrenVisible);
			foreach (SynchedMissionObject synchedMissionObject in this._flagDependentObjects)
			{
				synchedMissionObject.SetVisibleSynched(value, false);
			}
		}

		// Token: 0x0600318F RID: 12687 RVA: 0x000CC890 File Offset: 0x000CAA90
		public void SetTeamColorsWithAllSynched(uint color, uint color2)
		{
			this._theFlag.SetTeamColorsSynched(color, color2);
			foreach (SynchedMissionObject synchedMissionObject in this._flagDependentObjects)
			{
				synchedMissionObject.SetTeamColorsSynched(color, color2);
			}
		}

		// Token: 0x06003190 RID: 12688 RVA: 0x000CC8F0 File Offset: 0x000CAAF0
		public uint GetFlagColor()
		{
			return this._theFlag.Color;
		}

		// Token: 0x06003191 RID: 12689 RVA: 0x000CC8FD File Offset: 0x000CAAFD
		public uint GetFlagColor2()
		{
			return this._theFlag.Color2;
		}

		// Token: 0x06003192 RID: 12690 RVA: 0x000CC90C File Offset: 0x000CAB0C
		public float GetFlagProgress()
		{
			return MathF.Clamp((this._theFlag.GameEntity.GlobalPosition.z - this._flagBottomBoundary.GlobalPosition.z) / (this._flagTopBoundary.GlobalPosition.z - this._flagBottomBoundary.GlobalPosition.z), 0f, 1f);
		}

		// Token: 0x0400152F RID: 5423
		public const float PointRadius = 4f;

		// Token: 0x04001530 RID: 5424
		public const float RadiusMultiplierForContestedArea = 1.5f;

		// Token: 0x04001531 RID: 5425
		private const float TimeToTravelBetweenBoundaries = 10f;

		// Token: 0x04001532 RID: 5426
		public int FlagIndex;

		// Token: 0x04001533 RID: 5427
		private SynchedMissionObject _theFlag;

		// Token: 0x04001534 RID: 5428
		private SynchedMissionObject _flagHolder;

		// Token: 0x04001535 RID: 5429
		private GameEntity _flagBottomBoundary;

		// Token: 0x04001536 RID: 5430
		private GameEntity _flagTopBoundary;

		// Token: 0x04001537 RID: 5431
		private List<SynchedMissionObject> _flagDependentObjects;

		// Token: 0x04001538 RID: 5432
		private CaptureTheFlagFlagDirection _currentDirection = CaptureTheFlagFlagDirection.None;
	}
}
