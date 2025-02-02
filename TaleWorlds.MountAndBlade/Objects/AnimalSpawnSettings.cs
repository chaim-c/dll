using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade.Objects
{
	// Token: 0x02000386 RID: 902
	public class AnimalSpawnSettings : ScriptComponentBehavior
	{
		// Token: 0x0600316F RID: 12655 RVA: 0x000CC240 File Offset: 0x000CA440
		public static void CheckAndSetAnimalAgentFlags(GameEntity spawnEntity, Agent animalAgent)
		{
			if (spawnEntity.HasScriptOfType<AnimalSpawnSettings>() && spawnEntity.GetFirstScriptOfType<AnimalSpawnSettings>().DisableWandering)
			{
				animalAgent.SetAgentFlags(animalAgent.GetAgentFlags() & ~AgentFlag.CanWander);
			}
		}

		// Token: 0x0400152A RID: 5418
		public bool DisableWandering = true;
	}
}
