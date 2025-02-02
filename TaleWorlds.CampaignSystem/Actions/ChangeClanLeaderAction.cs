using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x0200042B RID: 1067
	public static class ChangeClanLeaderAction
	{
		// Token: 0x06004056 RID: 16470 RVA: 0x0013D288 File Offset: 0x0013B488
		private static void ApplyInternal(Clan clan, Hero newLeader = null)
		{
			Hero leader = clan.Leader;
			if (newLeader == null)
			{
				Dictionary<Hero, int> heirApparents = clan.GetHeirApparents();
				if (heirApparents.Count == 0)
				{
					return;
				}
				int highestPoint = (from h in heirApparents
				orderby h.Value descending
				select h).FirstOrDefault<KeyValuePair<Hero, int>>().Value;
				newLeader = (from h in heirApparents
				where h.Value.Equals(highestPoint)
				select h).GetRandomElementInefficiently<KeyValuePair<Hero, int>>().Key;
			}
			GiveGoldAction.ApplyBetweenCharacters(leader, newLeader, leader.Gold, true);
			if (newLeader.GovernorOf != null)
			{
				ChangeGovernorAction.RemoveGovernorOf(newLeader);
			}
			if (!newLeader.IsPrisoner && !newLeader.IsFugitive && !newLeader.IsReleased && !newLeader.IsTraveling)
			{
				MobileParty mobileParty = newLeader.PartyBelongedTo;
				if (mobileParty == null)
				{
					mobileParty = clan.CreateNewMobileParty(newLeader);
				}
				if (mobileParty.LeaderHero != newLeader)
				{
					mobileParty.ChangePartyLeader(newLeader);
				}
			}
			foreach (Hero hero in Hero.AllAliveHeroes)
			{
				if (hero != newLeader)
				{
					int relationChangeAfterClanLeaderIsDead = Campaign.Current.Models.DiplomacyModel.GetRelationChangeAfterClanLeaderIsDead(leader, hero);
					int heroRelation = CharacterRelationManager.GetHeroRelation(newLeader, hero);
					newLeader.SetPersonalRelation(hero, heroRelation + relationChangeAfterClanLeaderIsDead);
				}
			}
			clan.SetLeader(newLeader);
			CampaignEventDispatcher.Instance.OnClanLeaderChanged(leader, newLeader);
		}

		// Token: 0x06004057 RID: 16471 RVA: 0x0013D3FC File Offset: 0x0013B5FC
		public static void ApplyWithSelectedNewLeader(Clan clan, Hero newLeader)
		{
			ChangeClanLeaderAction.ApplyInternal(clan, newLeader);
		}

		// Token: 0x06004058 RID: 16472 RVA: 0x0013D405 File Offset: 0x0013B605
		public static void ApplyWithoutSelectedNewLeader(Clan clan)
		{
			ChangeClanLeaderAction.ApplyInternal(clan, null);
		}
	}
}
