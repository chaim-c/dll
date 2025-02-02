using System;
using System.Collections.Generic;
using SandBox.Missions.MissionLogics;
using SandBox.Objects.Usables;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Settlements.Locations;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace SandBox.Missions.AgentBehaviors
{
	// Token: 0x0200007B RID: 123
	public class ChangeLocationBehavior : AgentBehavior
	{
		// Token: 0x060004CA RID: 1226 RVA: 0x000200C8 File Offset: 0x0001E2C8
		public ChangeLocationBehavior(AgentBehaviorGroup behaviorGroup) : base(behaviorGroup)
		{
			this._missionAgentHandler = base.Mission.GetMissionBehavior<MissionAgentHandler>();
			this._initializeTime = base.Mission.CurrentTime;
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x000200F4 File Offset: 0x0001E2F4
		public override void Tick(float dt, bool isSimulation)
		{
			if (this._selectedDoor == null)
			{
				Passage passage = this.SelectADoor();
				if (passage != null)
				{
					this._selectedDoor = passage;
					base.Navigator.SetTarget(this._selectedDoor, false);
					return;
				}
			}
			else if (this._selectedDoor.ToLocation.CharacterCount >= this._selectedDoor.ToLocation.ProsperityMax)
			{
				base.Navigator.SetTarget(null, false);
				base.Navigator.ForceThink(0f);
				this._selectedDoor = null;
			}
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x00020174 File Offset: 0x0001E374
		private Passage SelectADoor()
		{
			Passage result = null;
			List<Passage> list = new List<Passage>();
			foreach (UsableMachine usableMachine in this._missionAgentHandler.TownPassageProps)
			{
				Passage passage = (Passage)usableMachine;
				if (passage.GetVacantStandingPointForAI(base.OwnerAgent) != null && passage.ToLocation.CharacterCount < passage.ToLocation.ProsperityMax)
				{
					list.Add(passage);
				}
			}
			if (list.Count > 0)
			{
				result = list[MBRandom.RandomInt(list.Count)];
			}
			return result;
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x0002021C File Offset: 0x0001E41C
		protected override void OnActivate()
		{
			base.OnActivate();
			this._selectedDoor = null;
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x0002022B File Offset: 0x0001E42B
		protected override void OnDeactivate()
		{
			base.OnDeactivate();
			this._selectedDoor = null;
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x0002023A File Offset: 0x0001E43A
		public override string GetDebugInfo()
		{
			if (this._selectedDoor != null)
			{
				return "Go to " + this._selectedDoor.ToLocation.StringId;
			}
			return "Change Location no target";
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x00020264 File Offset: 0x0001E464
		public override float GetAvailability(bool isSimulation)
		{
			float result = 0f;
			bool flag = false;
			bool flag2 = false;
			LocationCharacter locationCharacter = CampaignMission.Current.Location.GetLocationCharacter(base.OwnerAgent.Origin);
			if (base.Mission.CurrentTime < 5f || locationCharacter.FixedLocation || !this._missionAgentHandler.HasPassages())
			{
				return 0f;
			}
			foreach (UsableMachine usableMachine in this._missionAgentHandler.TownPassageProps)
			{
				Passage passage = usableMachine as Passage;
				if (passage.ToLocation.CanAIEnter(locationCharacter) && passage.ToLocation.CharacterCount < passage.ToLocation.ProsperityMax)
				{
					flag = true;
					if (passage.PilotStandingPoint.GameEntity.GetGlobalFrame().origin.Distance(base.OwnerAgent.Position) < 1f)
					{
						flag2 = true;
						break;
					}
				}
			}
			if (flag)
			{
				if (!flag2)
				{
					result = (CampaignMission.Current.Location.IsIndoor ? 0.1f : 0.05f);
				}
				else if (base.Mission.CurrentTime - this._initializeTime > 10f)
				{
					result = 0.01f;
				}
			}
			return result;
		}

		// Token: 0x04000233 RID: 563
		private readonly MissionAgentHandler _missionAgentHandler;

		// Token: 0x04000234 RID: 564
		private readonly float _initializeTime;

		// Token: 0x04000235 RID: 565
		private Passage _selectedDoor;
	}
}
