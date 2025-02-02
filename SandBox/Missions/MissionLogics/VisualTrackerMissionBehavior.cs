using System;
using System.Collections.Generic;
using System.Linq;
using SandBox.Objects.AreaMarkers;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace SandBox.Missions.MissionLogics
{
	// Token: 0x02000069 RID: 105
	public class VisualTrackerMissionBehavior : MissionLogic
	{
		// Token: 0x060003F3 RID: 1011 RVA: 0x0001B299 File Offset: 0x00019499
		public override void OnAgentCreated(Agent agent)
		{
			this.CheckAgent(agent);
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0001B2A2 File Offset: 0x000194A2
		private void CheckAgent(Agent agent)
		{
			if (agent.Character != null && this._visualTrackerManager.CheckTracked(agent.Character))
			{
				this.RegisterLocalOnlyObject(agent);
			}
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0001B2C6 File Offset: 0x000194C6
		public override void AfterStart()
		{
			this.Refresh();
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0001B2CE File Offset: 0x000194CE
		public override void OnMissionTick(float dt)
		{
			base.OnMissionTick(dt);
			if (this._visualTrackerManager.TrackedObjectsVersion != this._trackedObjectsVersion)
			{
				this.Refresh();
			}
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0001B2F0 File Offset: 0x000194F0
		private void Refresh()
		{
			foreach (Agent agent in base.Mission.Agents)
			{
				this.CheckAgent(agent);
			}
			this.RefreshCommonAreas();
			this._trackedObjectsVersion = this._visualTrackerManager.TrackedObjectsVersion;
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0001B360 File Offset: 0x00019560
		public void RegisterLocalOnlyObject(ITrackableBase obj)
		{
			using (List<TrackedObject>.Enumerator enumerator = this._currentTrackedObjects.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Object == obj)
					{
						return;
					}
				}
			}
			this._currentTrackedObjects.Add(new TrackedObject(obj));
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0001B3C8 File Offset: 0x000195C8
		private void RefreshCommonAreas()
		{
			Settlement settlement = PlayerEncounter.LocationEncounter.Settlement;
			foreach (CommonAreaMarker commonAreaMarker in base.Mission.ActiveMissionObjects.FindAllWithType<CommonAreaMarker>().ToList<CommonAreaMarker>())
			{
				if (settlement.Alleys.Count >= commonAreaMarker.AreaIndex)
				{
					this.RegisterLocalOnlyObject(commonAreaMarker);
				}
			}
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0001B448 File Offset: 0x00019648
		public override List<CompassItemUpdateParams> GetCompassTargets()
		{
			List<CompassItemUpdateParams> list = new List<CompassItemUpdateParams>();
			foreach (TrackedObject trackedObject in this._currentTrackedObjects)
			{
				list.Add(new CompassItemUpdateParams(trackedObject.Object, TargetIconType.Flag_A, trackedObject.Position, 4288256409U, uint.MaxValue));
			}
			return list;
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0001B4BC File Offset: 0x000196BC
		private void RemoveLocalObject(ITrackableBase obj)
		{
			this._currentTrackedObjects.RemoveAll((TrackedObject x) => x.Object == obj);
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0001B4EE File Offset: 0x000196EE
		public override void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent, AgentState agentState, KillingBlow blow)
		{
			this.RemoveLocalObject(affectedAgent);
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0001B4F7 File Offset: 0x000196F7
		public override void OnAgentDeleted(Agent affectedAgent)
		{
			this.RemoveLocalObject(affectedAgent);
		}

		// Token: 0x040001C9 RID: 457
		private List<TrackedObject> _currentTrackedObjects = new List<TrackedObject>();

		// Token: 0x040001CA RID: 458
		private int _trackedObjectsVersion = -1;

		// Token: 0x040001CB RID: 459
		private readonly VisualTrackerManager _visualTrackerManager = Campaign.Current.VisualTrackerManager;
	}
}
