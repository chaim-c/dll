using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using TaleWorlds.ObjectSystem;

namespace SandBox.Missions.MissionLogics
{
	// Token: 0x0200005D RID: 93
	public class MountAgentLogic : MissionLogic
	{
		// Token: 0x060003CC RID: 972 RVA: 0x0001A6B4 File Offset: 0x000188B4
		public override void OnAgentBuild(Agent agent, Banner banner)
		{
			if (agent.IsMainAgent && agent.HasMount)
			{
				this._mainHeroMountAgent = agent.MountAgent;
			}
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0001A6D4 File Offset: 0x000188D4
		public override void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent, AgentState agentState, KillingBlow killingBlow)
		{
			if (affectedAgent.IsMount && agentState == AgentState.Killed && this._mainHeroMountAgent == affectedAgent)
			{
				Equipment equipment = Hero.MainHero.BattleEquipment;
				if (Mission.Current.DoesMissionRequireCivilianEquipment)
				{
					equipment = Hero.MainHero.CivilianEquipment;
				}
				float randomFloat = MBRandom.RandomFloat;
				float num = 0.05f;
				float num2 = 0.2f;
				if (Hero.MainHero.GetPerkValue(DefaultPerks.Riding.WellStraped))
				{
					float primaryBonus = DefaultPerks.Riding.WellStraped.PrimaryBonus;
					num += num * primaryBonus;
					num2 += num2 * primaryBonus;
				}
				bool flag = randomFloat < num2;
				bool flag2;
				if (randomFloat >= num)
				{
					ItemModifier itemModifier = equipment[EquipmentIndex.ArmorItemEndSlot].ItemModifier;
					flag2 = (((itemModifier != null) ? itemModifier.StringId : null) == "lame_horse");
				}
				else
				{
					flag2 = true;
				}
				if (flag2)
				{
					equipment[EquipmentIndex.ArmorItemEndSlot] = EquipmentElement.Invalid;
					EquipmentElement rosterElement = equipment[EquipmentIndex.HorseHarness];
					equipment[EquipmentIndex.HorseHarness] = EquipmentElement.Invalid;
					if (!rosterElement.IsInvalid() && !rosterElement.IsEmpty)
					{
						MobileParty.MainParty.ItemRoster.AddToCounts(rosterElement, 1);
					}
					MBInformationManager.AddQuickInformation(new TextObject("{=nZhPS83J}You lost your horse.", null), 0, null, "");
					return;
				}
				if (flag)
				{
					ItemModifier @object = MBObjectManager.Instance.GetObject<ItemModifier>("lame_horse");
					equipment[EquipmentIndex.ArmorItemEndSlot] = new EquipmentElement(equipment[EquipmentIndex.ArmorItemEndSlot].Item, @object, null, false);
					TextObject textObject = new TextObject("{=a6hwSEAK}Your horse is turned into a {MODIFIED_NAME}, since it got seriously injured.", null);
					textObject.SetTextVariable("MODIFIED_NAME", equipment[10].GetModifiedItemName());
					MBInformationManager.AddQuickInformation(textObject, 0, null, "");
				}
			}
		}

		// Token: 0x040001BE RID: 446
		private Agent _mainHeroMountAgent;
	}
}
