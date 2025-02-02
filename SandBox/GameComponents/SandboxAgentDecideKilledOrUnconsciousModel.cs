using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.ComponentInterfaces;

namespace SandBox.GameComponents
{
	// Token: 0x02000093 RID: 147
	public class SandboxAgentDecideKilledOrUnconsciousModel : AgentDecideKilledOrUnconsciousModel
	{
		// Token: 0x060005B0 RID: 1456 RVA: 0x00026A9C File Offset: 0x00024C9C
		public override float GetAgentStateProbability(Agent affectorAgent, Agent effectedAgent, DamageTypes damageType, WeaponFlags weaponFlags, out float useSurgeryProbability)
		{
			useSurgeryProbability = 1f;
			if (effectedAgent.IsHuman)
			{
				CharacterObject characterObject = (CharacterObject)effectedAgent.Character;
				if (Campaign.Current != null)
				{
					if (characterObject.IsHero && !characterObject.HeroObject.CanDie(KillCharacterAction.KillCharacterActionDetail.DiedInBattle))
					{
						return 0f;
					}
					CampaignAgentComponent component = effectedAgent.GetComponent<CampaignAgentComponent>();
					PartyBase party = (component != null) ? component.OwnerParty : null;
					if (affectorAgent != null && affectorAgent.IsHuman)
					{
						CampaignAgentComponent component2 = affectorAgent.GetComponent<CampaignAgentComponent>();
						PartyBase enemyParty = (component2 != null) ? component2.OwnerParty : null;
						return 1f - Campaign.Current.Models.PartyHealingModel.GetSurvivalChance(party, characterObject, damageType, weaponFlags.HasAnyFlag(WeaponFlags.CanKillEvenIfBlunt), enemyParty);
					}
					return 1f - Campaign.Current.Models.PartyHealingModel.GetSurvivalChance(party, characterObject, damageType, weaponFlags.HasAnyFlag(WeaponFlags.CanKillEvenIfBlunt), null);
				}
			}
			return 1f;
		}
	}
}
