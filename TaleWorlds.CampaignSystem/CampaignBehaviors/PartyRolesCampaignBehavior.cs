using System;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Party.PartyComponents;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Settlements;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x020003BC RID: 956
	public class PartyRolesCampaignBehavior : CampaignBehaviorBase
	{
		// Token: 0x06003A57 RID: 14935 RVA: 0x001133E8 File Offset: 0x001115E8
		public override void RegisterEvents()
		{
			CampaignEvents.HeroKilledEvent.AddNonSerializedListener(this, new Action<Hero, Hero, KillCharacterAction.KillCharacterActionDetail, bool>(this.OnHeroKilled));
			CampaignEvents.OnGovernorChangedEvent.AddNonSerializedListener(this, new Action<Town, Hero, Hero>(this.OnGovernorChanged));
			CampaignEvents.MobilePartyCreated.AddNonSerializedListener(this, new Action<MobileParty>(this.OnPartySpawned));
			CampaignEvents.CompanionRemoved.AddNonSerializedListener(this, new Action<Hero, RemoveCompanionAction.RemoveCompanionDetail>(this.OnCompanionRemoved));
			CampaignEvents.OnHeroGetsBusyEvent.AddNonSerializedListener(this, new Action<Hero, HeroGetsBusyReasons>(this.OnHeroGetsBusy));
			CampaignEvents.HeroPrisonerTaken.AddNonSerializedListener(this, new Action<PartyBase, Hero>(this.OnHeroPrisonerTaken));
			CampaignEvents.OnHeroChangedClanEvent.AddNonSerializedListener(this, new Action<Hero, Clan>(this.OnHeroChangedClan));
		}

		// Token: 0x06003A58 RID: 14936 RVA: 0x00113496 File Offset: 0x00111696
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x06003A59 RID: 14937 RVA: 0x00113498 File Offset: 0x00111698
		private void OnHeroKilled(Hero victim, Hero killer, KillCharacterAction.KillCharacterActionDetail detail, bool showNotification = true)
		{
			if (victim.Clan == Clan.PlayerClan)
			{
				this.RemovePartyRoleIfExist(victim);
			}
		}

		// Token: 0x06003A5A RID: 14938 RVA: 0x001134AE File Offset: 0x001116AE
		private void OnHeroPrisonerTaken(PartyBase party, Hero prisoner)
		{
			if (prisoner.Clan == Clan.PlayerClan)
			{
				this.RemovePartyRoleIfExist(prisoner);
			}
		}

		// Token: 0x06003A5B RID: 14939 RVA: 0x001134C4 File Offset: 0x001116C4
		private void OnGovernorChanged(Town fortification, Hero oldGovernor, Hero newGovernor)
		{
			if (((newGovernor != null) ? newGovernor.Clan : null) == Clan.PlayerClan)
			{
				this.RemovePartyRoleIfExist(newGovernor);
			}
		}

		// Token: 0x06003A5C RID: 14940 RVA: 0x001134E0 File Offset: 0x001116E0
		private void OnPartySpawned(MobileParty spawnedParty)
		{
			if (spawnedParty.IsLordParty && spawnedParty.ActualClan == Clan.PlayerClan)
			{
				foreach (TroopRosterElement troopRosterElement in spawnedParty.MemberRoster.GetTroopRoster())
				{
					if (troopRosterElement.Character.IsHero)
					{
						this.RemovePartyRoleIfExist(troopRosterElement.Character.HeroObject);
					}
				}
			}
		}

		// Token: 0x06003A5D RID: 14941 RVA: 0x00113564 File Offset: 0x00111764
		private void OnCompanionRemoved(Hero companion, RemoveCompanionAction.RemoveCompanionDetail detail)
		{
			this.RemovePartyRoleIfExist(companion);
		}

		// Token: 0x06003A5E RID: 14942 RVA: 0x0011356D File Offset: 0x0011176D
		private void OnHeroGetsBusy(Hero hero, HeroGetsBusyReasons heroGetsBusyReason)
		{
			if (hero.Clan == Clan.PlayerClan)
			{
				this.RemovePartyRoleIfExist(hero);
			}
		}

		// Token: 0x06003A5F RID: 14943 RVA: 0x00113583 File Offset: 0x00111783
		private void OnHeroChangedClan(Hero hero, Clan oldClan)
		{
			if (oldClan == Clan.PlayerClan)
			{
				this.RemovePartyRoleIfExist(hero);
			}
		}

		// Token: 0x06003A60 RID: 14944 RVA: 0x00113594 File Offset: 0x00111794
		private void RemovePartyRoleIfExist(Hero hero)
		{
			foreach (WarPartyComponent warPartyComponent in Clan.PlayerClan.WarPartyComponents)
			{
				if (warPartyComponent.MobileParty.GetHeroPerkRole(hero) != SkillEffect.PerkRole.None)
				{
					warPartyComponent.MobileParty.RemoveHeroPerkRole(hero);
				}
			}
		}
	}
}
