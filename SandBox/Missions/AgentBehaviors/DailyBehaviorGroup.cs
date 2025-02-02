using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Settlements.Locations;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace SandBox.Missions.AgentBehaviors
{
	// Token: 0x0200007C RID: 124
	public class DailyBehaviorGroup : AgentBehaviorGroup
	{
		// Token: 0x060004D1 RID: 1233 RVA: 0x000203B4 File Offset: 0x0001E5B4
		public DailyBehaviorGroup(AgentNavigator navigator, Mission mission) : base(navigator, mission)
		{
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x000203C0 File Offset: 0x0001E5C0
		public override void Tick(float dt, bool isSimulation)
		{
			if (base.ScriptedBehavior != null)
			{
				if (!base.ScriptedBehavior.IsActive)
				{
					base.DisableAllBehaviors();
					base.ScriptedBehavior.IsActive = true;
				}
			}
			else if (this.CheckBehaviorTimer == null || this.CheckBehaviorTimer.Check(base.Mission.CurrentTime))
			{
				this.Think(isSimulation);
			}
			this.TickActiveBehaviors(dt, isSimulation);
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x00020428 File Offset: 0x0001E628
		public override void ConversationTick()
		{
			foreach (AgentBehavior agentBehavior in this.Behaviors)
			{
				if (agentBehavior.IsActive)
				{
					agentBehavior.ConversationTick();
				}
			}
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x00020484 File Offset: 0x0001E684
		private void Think(bool isSimulation)
		{
			float num = 0f;
			float[] array = new float[this.Behaviors.Count];
			for (int i = 0; i < this.Behaviors.Count; i++)
			{
				array[i] = this.Behaviors[i].GetAvailability(isSimulation);
				num += array[i];
			}
			if (num > 0f)
			{
				float num2 = MBRandom.RandomFloat * num;
				int j = 0;
				while (j < array.Length)
				{
					float num3 = array[j];
					num2 -= num3;
					if (num2 < 0f)
					{
						if (!this.Behaviors[j].IsActive)
						{
							base.DisableAllBehaviors();
							this.Behaviors[j].IsActive = true;
							this.CheckBehaviorTime = this.Behaviors[j].CheckTime;
							this.SetCheckBehaviorTimer(this.CheckBehaviorTime);
							return;
						}
						break;
					}
					else
					{
						j++;
					}
				}
			}
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x00020560 File Offset: 0x0001E760
		private void TickActiveBehaviors(float dt, bool isSimulation)
		{
			foreach (AgentBehavior agentBehavior in this.Behaviors)
			{
				if (agentBehavior.IsActive)
				{
					agentBehavior.Tick(dt, isSimulation);
				}
			}
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x000205BC File Offset: 0x0001E7BC
		private void SetCheckBehaviorTimer(float time)
		{
			if (this.CheckBehaviorTimer == null)
			{
				this.CheckBehaviorTimer = new Timer(base.Mission.CurrentTime, time, true);
				return;
			}
			this.CheckBehaviorTimer.Reset(base.Mission.CurrentTime, time);
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x000205F6 File Offset: 0x0001E7F6
		public override float GetScore(bool isSimulation)
		{
			return 0.5f;
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x00020600 File Offset: 0x0001E800
		public override void OnAgentRemoved(Agent agent)
		{
			foreach (AgentBehavior agentBehavior in this.Behaviors)
			{
				if (agentBehavior.IsActive)
				{
					agentBehavior.OnAgentRemoved(agent);
				}
			}
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x0002065C File Offset: 0x0001E85C
		protected override void OnActivate()
		{
			LocationCharacter locationCharacter = CampaignMission.Current.Location.GetLocationCharacter(base.OwnerAgent.Origin);
			if (locationCharacter != null && locationCharacter.ActionSetCode != locationCharacter.AlarmedActionSetCode)
			{
				AnimationSystemData animationSystemData = locationCharacter.GetAgentBuildData().AgentMonster.FillAnimationSystemData(MBGlobals.GetActionSet(locationCharacter.ActionSetCode), locationCharacter.Character.GetStepSize(), false);
				base.OwnerAgent.SetActionSet(ref animationSystemData);
			}
			this.Navigator.SetItemsVisibility(true);
			this.Navigator.SetSpecialItem();
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x000206E6 File Offset: 0x0001E8E6
		protected override void OnDeactivate()
		{
			base.OnDeactivate();
			this.CheckBehaviorTimer = null;
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x000206F5 File Offset: 0x0001E8F5
		public override void ForceThink(float inSeconds)
		{
			if (MathF.Abs(inSeconds) < 1E-45f)
			{
				this.Think(false);
				return;
			}
			this.SetCheckBehaviorTimer(inSeconds);
		}
	}
}
