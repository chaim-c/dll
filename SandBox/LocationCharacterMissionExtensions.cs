using System;
using TaleWorlds.CampaignSystem.Settlements.Locations;
using TaleWorlds.MountAndBlade;

namespace SandBox
{
	// Token: 0x02000020 RID: 32
	public static class LocationCharacterMissionExtensions
	{
		// Token: 0x060000CE RID: 206 RVA: 0x00005CD3 File Offset: 0x00003ED3
		public static AgentBuildData GetAgentBuildData(this LocationCharacter locationCharacter)
		{
			return new AgentBuildData(locationCharacter.AgentData);
		}
	}
}
