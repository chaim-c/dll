using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;

namespace SandBox.Missions.MissionLogics
{
	// Token: 0x02000060 RID: 96
	public class SandboxGeneralsAndCaptainsAssignmentLogic : GeneralsAndCaptainsAssignmentLogic
	{
		// Token: 0x060003D4 RID: 980 RVA: 0x0001AA45 File Offset: 0x00018C45
		public SandboxGeneralsAndCaptainsAssignmentLogic(TextObject attackerGeneralName, TextObject defenderGeneralName, TextObject attackerAllyGeneralName = null, TextObject defenderAllyGeneralName = null, bool createBodyguard = true) : base(attackerGeneralName, defenderGeneralName, attackerAllyGeneralName, defenderAllyGeneralName, createBodyguard)
		{
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0001AA54 File Offset: 0x00018C54
		protected override void SortCaptainsByPriority(Team team, ref List<Agent> captains)
		{
			EncounterModel encounterModel = Campaign.Current.Models.EncounterModel;
			if (encounterModel != null)
			{
				captains = captains.OrderByDescending(delegate(Agent captain)
				{
					if (captain != team.GeneralAgent)
					{
						CharacterObject characterObject;
						return (float)(((characterObject = (captain.Character as CharacterObject)) != null && characterObject.HeroObject != null) ? encounterModel.GetCharacterSergeantScore(characterObject.HeroObject) : 0);
					}
					return float.MaxValue;
				}).ToList<Agent>();
				return;
			}
			base.SortCaptainsByPriority(team, ref captains);
		}
	}
}
