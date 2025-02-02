using System;
using TaleWorlds.MountAndBlade.ComponentInterfaces;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001F9 RID: 505
	public class DefaultMissionDifficultyModel : MissionDifficultyModel
	{
		// Token: 0x06001C17 RID: 7191 RVA: 0x00061870 File Offset: 0x0005FA70
		public override float GetDamageMultiplierOfCombatDifficulty(Agent victimAgent, Agent attackerAgent = null)
		{
			float result = 1f;
			victimAgent = (victimAgent.IsMount ? victimAgent.RiderAgent : victimAgent);
			if (victimAgent != null)
			{
				if (victimAgent.IsMainAgent)
				{
					result = Mission.Current.DamageToPlayerMultiplier;
				}
				else
				{
					Mission mission = Mission.Current;
					Agent agent = (mission != null) ? mission.MainAgent : null;
					if (agent != null && victimAgent.IsFriendOf(agent))
					{
						if (attackerAgent != null && attackerAgent == agent)
						{
							result = Mission.Current.DamageFromPlayerToFriendsMultiplier;
						}
						else
						{
							result = Mission.Current.DamageToFriendsMultiplier;
						}
					}
				}
			}
			return result;
		}
	}
}
