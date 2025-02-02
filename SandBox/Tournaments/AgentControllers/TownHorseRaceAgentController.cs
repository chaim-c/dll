using System;
using SandBox.Tournaments.MissionLogics;
using TaleWorlds.Engine;
using TaleWorlds.MountAndBlade;

namespace SandBox.Tournaments.AgentControllers
{
	// Token: 0x02000034 RID: 52
	public class TownHorseRaceAgentController : AgentController
	{
		// Token: 0x060001C3 RID: 451 RVA: 0x0000C239 File Offset: 0x0000A439
		public override void OnInitialize()
		{
			this._controller = base.Mission.GetMissionBehavior<TownHorseRaceMissionController>();
			this._checkPointIndex = 0;
			this._tourCount = 0;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000C25C File Offset: 0x0000A45C
		public void DisableMovement()
		{
			if (base.Owner.IsAIControlled)
			{
				WorldPosition worldPosition = base.Owner.GetWorldPosition();
				base.Owner.SetScriptedPositionAndDirection(ref worldPosition, base.Owner.Frame.rotation.f.AsVec2.RotationInRadians, false, Agent.AIScriptedFrameFlags.None);
			}
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000C2B8 File Offset: 0x0000A4B8
		public void Start()
		{
			if (this._checkPointIndex < this._controller.CheckPoints.Count)
			{
				TownHorseRaceMissionController.CheckPoint checkPoint = this._controller.CheckPoints[this._checkPointIndex];
				checkPoint.AddToCheckList(base.Owner);
				if (base.Owner.IsAIControlled)
				{
					WorldPosition worldPosition = new WorldPosition(Mission.Current.Scene, UIntPtr.Zero, checkPoint.GetBestTargetPosition(), false);
					base.Owner.SetScriptedPosition(ref worldPosition, false, Agent.AIScriptedFrameFlags.NeverSlowDown);
				}
			}
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000C33C File Offset: 0x0000A53C
		public void OnEnterCheckPoint(VolumeBox checkPoint)
		{
			this._controller.CheckPoints[this._checkPointIndex].RemoveFromCheckList(base.Owner);
			this._checkPointIndex++;
			if (this._checkPointIndex < this._controller.CheckPoints.Count)
			{
				if (base.Owner.IsAIControlled)
				{
					WorldPosition worldPosition = new WorldPosition(Mission.Current.Scene, UIntPtr.Zero, this._controller.CheckPoints[this._checkPointIndex].GetBestTargetPosition(), false);
					base.Owner.SetScriptedPosition(ref worldPosition, false, Agent.AIScriptedFrameFlags.NeverSlowDown);
				}
				this._controller.CheckPoints[this._checkPointIndex].AddToCheckList(base.Owner);
				return;
			}
			this._tourCount++;
			if (this._tourCount < 2)
			{
				this._checkPointIndex = 0;
				if (base.Owner.IsAIControlled)
				{
					WorldPosition worldPosition2 = new WorldPosition(Mission.Current.Scene, UIntPtr.Zero, this._controller.CheckPoints[this._checkPointIndex].GetBestTargetPosition(), false);
					base.Owner.SetScriptedPosition(ref worldPosition2, false, Agent.AIScriptedFrameFlags.NeverSlowDown);
				}
				this._controller.CheckPoints[this._checkPointIndex].AddToCheckList(base.Owner);
			}
		}

		// Token: 0x040000A7 RID: 167
		private TownHorseRaceMissionController _controller;

		// Token: 0x040000A8 RID: 168
		private int _checkPointIndex;

		// Token: 0x040000A9 RID: 169
		private int _tourCount;
	}
}
