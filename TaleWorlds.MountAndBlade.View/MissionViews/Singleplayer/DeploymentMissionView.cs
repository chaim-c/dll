using System;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade.View.MissionViews.Order;

namespace TaleWorlds.MountAndBlade.View.MissionViews.Singleplayer
{
	// Token: 0x0200005F RID: 95
	public class DeploymentMissionView : MissionView
	{
		// Token: 0x06000404 RID: 1028 RVA: 0x0002212E File Offset: 0x0002032E
		public override void AfterStart()
		{
			this._orderTroopPlacer = base.Mission.GetMissionBehavior<OrderTroopPlacer>();
			this._entitySelectionHandler = base.Mission.GetMissionBehavior<MissionEntitySelectionUIHandler>();
			this._deploymentBoundaryMarkerHandler = base.Mission.GetMissionBehavior<MissionDeploymentBoundaryMarker>();
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x00022164 File Offset: 0x00020364
		public override void OnInitialDeploymentPlanMadeForSide(BattleSideEnum side, bool isFirstPlan)
		{
			if (side == base.Mission.PlayerTeam.Side && base.Mission.DeploymentPlan.HasDeploymentBoundaries(base.Mission.PlayerTeam.Side))
			{
				OrderTroopPlacer orderTroopPlacer = this._orderTroopPlacer;
				if (orderTroopPlacer == null)
				{
					return;
				}
				orderTroopPlacer.RestrictOrdersToDeploymentBoundaries(true);
			}
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x000221B8 File Offset: 0x000203B8
		public override void OnDeploymentFinished()
		{
			this.OnDeploymentFinish();
			if (this._entitySelectionHandler != null)
			{
				base.Mission.RemoveMissionBehavior(this._entitySelectionHandler);
			}
			if (this._deploymentBoundaryMarkerHandler != null)
			{
				if (base.Mission.DeploymentPlan.HasDeploymentBoundaries(base.Mission.PlayerTeam.Side))
				{
					OrderTroopPlacer orderTroopPlacer = this._orderTroopPlacer;
					if (orderTroopPlacer != null)
					{
						orderTroopPlacer.RestrictOrdersToDeploymentBoundaries(false);
					}
				}
				base.Mission.RemoveMissionBehavior(this._deploymentBoundaryMarkerHandler);
			}
			if (!base.Mission.HasMissionBehavior<MissionBoundaryWallView>())
			{
				MissionBoundaryWallView missionView = new MissionBoundaryWallView();
				base.MissionScreen.AddMissionView(missionView);
			}
		}

		// Token: 0x040002A2 RID: 674
		private OrderTroopPlacer _orderTroopPlacer;

		// Token: 0x040002A3 RID: 675
		private MissionDeploymentBoundaryMarker _deploymentBoundaryMarkerHandler;

		// Token: 0x040002A4 RID: 676
		private MissionEntitySelectionUIHandler _entitySelectionHandler;

		// Token: 0x040002A5 RID: 677
		public OnPlayerDeploymentFinishDelegate OnDeploymentFinish;
	}
}
