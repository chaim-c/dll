using System;
using TaleWorlds.MountAndBlade;

namespace SandBox.Missions.AgentBehaviors
{
	// Token: 0x02000081 RID: 129
	public class InterruptingBehaviorGroup : AgentBehaviorGroup
	{
		// Token: 0x06000511 RID: 1297 RVA: 0x0002267D File Offset: 0x0002087D
		public InterruptingBehaviorGroup(AgentNavigator navigator, Mission mission) : base(navigator, mission)
		{
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00022688 File Offset: 0x00020888
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
			else
			{
				int bestBehaviorIndex = this.GetBestBehaviorIndex(isSimulation);
				if (bestBehaviorIndex != -1 && !this.Behaviors[bestBehaviorIndex].IsActive)
				{
					base.DisableAllBehaviors();
					this.Behaviors[bestBehaviorIndex].IsActive = true;
				}
			}
			this.TickActiveBehaviors(dt, isSimulation);
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00022700 File Offset: 0x00020900
		private void TickActiveBehaviors(float dt, bool isSimulation)
		{
			for (int i = this.Behaviors.Count - 1; i >= 0; i--)
			{
				AgentBehavior agentBehavior = this.Behaviors[i];
				if (agentBehavior.IsActive)
				{
					agentBehavior.Tick(dt, isSimulation);
				}
			}
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x00022742 File Offset: 0x00020942
		public override float GetScore(bool isSimulation)
		{
			if (this.GetBestBehaviorIndex(isSimulation) != -1)
			{
				return 0.75f;
			}
			return 0f;
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0002275C File Offset: 0x0002095C
		private int GetBestBehaviorIndex(bool isSimulation)
		{
			float num = 0f;
			int result = -1;
			for (int i = 0; i < this.Behaviors.Count; i++)
			{
				float availability = this.Behaviors[i].GetAvailability(isSimulation);
				if (availability > num)
				{
					num = availability;
					result = i;
				}
			}
			return result;
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x000227A3 File Offset: 0x000209A3
		public override void ForceThink(float inSeconds)
		{
			this.Navigator.RefreshBehaviorGroups(false);
		}
	}
}
