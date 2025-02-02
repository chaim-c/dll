﻿using System;
using Helpers;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Party.PartyComponents;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x020003CB RID: 971
	public class PrisonerReleaseCampaignBehavior : CampaignBehaviorBase
	{
		// Token: 0x06003B9F RID: 15263 RVA: 0x0011BC64 File Offset: 0x00119E64
		public override void RegisterEvents()
		{
			CampaignEvents.OnGameLoadedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnGameLoaded));
			CampaignEvents.DailyTickHeroEvent.AddNonSerializedListener(this, new Action<Hero>(this.DailyHeroTick));
			CampaignEvents.HourlyTickPartyEvent.AddNonSerializedListener(this, new Action<MobileParty>(this.HourlyPartyTick));
			CampaignEvents.MakePeace.AddNonSerializedListener(this, new Action<IFaction, IFaction, MakePeaceAction.MakePeaceDetail>(this.OnMakePeaceEvent));
			CampaignEvents.OnClanChangedKingdomEvent.AddNonSerializedListener(this, new Action<Clan, Kingdom, Kingdom, ChangeKingdomAction.ChangeKingdomActionDetail, bool>(this.ClanChangedKingdom));
			CampaignEvents.OnSettlementOwnerChangedEvent.AddNonSerializedListener(this, new Action<Settlement, bool, Hero, Hero, Hero, ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail>(this.OnSettlementOwnerChanged));
		}

		// Token: 0x06003BA0 RID: 15264 RVA: 0x0011BCFC File Offset: 0x00119EFC
		private void OnGameLoaded(CampaignGameStarter campaignGameStarter)
		{
			if (MBSaveLoad.IsUpdatingGameVersion && MBSaveLoad.LastLoadedGameVersion < ApplicationVersion.FromString("v1.2.0", 45697))
			{
				foreach (Hero hero in Hero.AllAliveHeroes)
				{
					if (hero != Hero.MainHero)
					{
						if (hero.IsPrisoner)
						{
							bool flag = hero.PartyBelongedToAsPrisoner != null && hero.PartyBelongedToAsPrisoner.IsMobile && hero.PartyBelongedToAsPrisoner.MobileParty.IsMilitia;
							bool flag2 = hero.PartyBelongedToAsPrisoner != null && !hero.PartyBelongedToAsPrisoner.MapFaction.IsAtWarWith(hero.MapFaction);
							if (hero.PartyBelongedToAsPrisoner == null)
							{
								if (hero.CurrentSettlement == null)
								{
									MakeHeroFugitiveAction.Apply(hero);
								}
							}
							else if (flag || flag2)
							{
								EndCaptivityAction.ApplyByEscape(hero, null);
								MakeHeroFugitiveAction.Apply(hero);
							}
						}
						else if (hero.PartyBelongedToAsPrisoner != null)
						{
							hero.PartyBelongedToAsPrisoner.PrisonRoster.RemoveTroop(hero.CharacterObject, 1, default(UniqueTroopDescriptor), 0);
							MakeHeroFugitiveAction.Apply(hero);
						}
					}
				}
			}
		}

		// Token: 0x06003BA1 RID: 15265 RVA: 0x0011BE34 File Offset: 0x0011A034
		private void ClanChangedKingdom(Clan clan, Kingdom oldKingdom, Kingdom newKingdom, ChangeKingdomAction.ChangeKingdomActionDetail detail, bool showNotification = true)
		{
			if (detail != ChangeKingdomAction.ChangeKingdomActionDetail.CreateKingdom)
			{
				PrisonerReleaseCampaignBehavior.ReleasePrisonersInternal(clan);
				if (oldKingdom != null)
				{
					PrisonerReleaseCampaignBehavior.ReleasePrisonersInternal(oldKingdom);
				}
				if (newKingdom != null)
				{
					this.OnAfterClanJoinedKingdom(clan, newKingdom);
					PrisonerReleaseCampaignBehavior.ReleasePrisonersInternal(newKingdom);
				}
			}
		}

		// Token: 0x06003BA2 RID: 15266 RVA: 0x0011BE5C File Offset: 0x0011A05C
		private void OnAfterClanJoinedKingdom(Clan clan, Kingdom newKingdom)
		{
			foreach (Kingdom kingdom in Kingdom.All)
			{
				if (kingdom != newKingdom && kingdom.IsAtWarWith(clan) && !kingdom.IsAtWarWith(newKingdom))
				{
					this.OnMakePeace(clan, kingdom);
				}
			}
		}

		// Token: 0x06003BA3 RID: 15267 RVA: 0x0011BEC8 File Offset: 0x0011A0C8
		private void OnMakePeaceEvent(IFaction side1Faction, IFaction side2Faction, MakePeaceAction.MakePeaceDetail detail)
		{
			this.OnMakePeace(side1Faction, side2Faction);
		}

		// Token: 0x06003BA4 RID: 15268 RVA: 0x0011BED2 File Offset: 0x0011A0D2
		private void OnMakePeace(IFaction side1Faction, IFaction side2Faction)
		{
			PrisonerReleaseCampaignBehavior.ReleasePrisonersInternal(side1Faction);
			PrisonerReleaseCampaignBehavior.ReleasePrisonersInternal(side2Faction);
		}

		// Token: 0x06003BA5 RID: 15269 RVA: 0x0011BEE0 File Offset: 0x0011A0E0
		private static void ReleasePrisonersInternal(IFaction faction)
		{
			foreach (Settlement settlement in faction.Settlements)
			{
				for (int i = settlement.Party.PrisonRoster.Count - 1; i >= 0; i--)
				{
					if (settlement.Party.PrisonRoster.GetElementNumber(i) > 0)
					{
						TroopRosterElement elementCopyAtIndex = settlement.Party.PrisonRoster.GetElementCopyAtIndex(i);
						if (elementCopyAtIndex.Character.IsHero && elementCopyAtIndex.Character.HeroObject != Hero.MainHero && !elementCopyAtIndex.Character.HeroObject.MapFaction.IsAtWarWith(faction.MapFaction))
						{
							EndCaptivityAction.ApplyByPeace(elementCopyAtIndex.Character.HeroObject, null);
							CampaignEventDispatcher.Instance.OnPrisonersChangeInSettlement(settlement, null, elementCopyAtIndex.Character.HeroObject, true);
						}
					}
				}
			}
			Clan clan = (faction.IsClan || faction.IsMinorFaction) ? ((Clan)faction) : null;
			Kingdom kingdom = faction.IsKingdomFaction ? ((Kingdom)faction) : null;
			if (clan != null)
			{
				PrisonerReleaseCampaignBehavior.ReleasePrisonersForClan(clan, faction);
				return;
			}
			if (kingdom != null)
			{
				foreach (Clan clan2 in kingdom.Clans)
				{
					PrisonerReleaseCampaignBehavior.ReleasePrisonersForClan(clan2, faction);
				}
			}
		}

		// Token: 0x06003BA6 RID: 15270 RVA: 0x0011C068 File Offset: 0x0011A268
		private static void ReleasePrisonersForClan(Clan clan, IFaction faction)
		{
			foreach (Hero hero in clan.Lords)
			{
				foreach (CaravanPartyComponent caravanPartyComponent in hero.OwnedCaravans)
				{
					PrisonerReleaseCampaignBehavior.ReleasePartyPrisoners(caravanPartyComponent.MobileParty, faction);
				}
			}
			foreach (Hero hero2 in clan.Companions)
			{
				foreach (CaravanPartyComponent caravanPartyComponent2 in hero2.OwnedCaravans)
				{
					PrisonerReleaseCampaignBehavior.ReleasePartyPrisoners(caravanPartyComponent2.MobileParty, faction);
				}
			}
			foreach (WarPartyComponent warPartyComponent in clan.WarPartyComponents)
			{
				PrisonerReleaseCampaignBehavior.ReleasePartyPrisoners(warPartyComponent.MobileParty, faction);
			}
			foreach (Settlement settlement in clan.Settlements)
			{
				if (settlement.IsVillage && settlement.Village.VillagerPartyComponent != null)
				{
					PrisonerReleaseCampaignBehavior.ReleasePartyPrisoners(settlement.Village.VillagerPartyComponent.MobileParty, faction);
				}
				else if ((settlement.IsCastle || settlement.IsTown) && settlement.Town.GarrisonParty != null)
				{
					PrisonerReleaseCampaignBehavior.ReleasePartyPrisoners(settlement.Town.GarrisonParty, faction);
				}
			}
		}

		// Token: 0x06003BA7 RID: 15271 RVA: 0x0011C258 File Offset: 0x0011A458
		private static void ReleasePartyPrisoners(MobileParty mobileParty, IFaction faction)
		{
			for (int i = mobileParty.PrisonRoster.Count - 1; i >= 0; i--)
			{
				if (mobileParty.Party.PrisonRoster.GetElementNumber(i) > 0)
				{
					TroopRosterElement elementCopyAtIndex = mobileParty.Party.PrisonRoster.GetElementCopyAtIndex(i);
					if (elementCopyAtIndex.Character.IsHero && elementCopyAtIndex.Character.HeroObject != Hero.MainHero && !elementCopyAtIndex.Character.HeroObject.MapFaction.IsAtWarWith(faction.MapFaction))
					{
						if (elementCopyAtIndex.Character.HeroObject.PartyBelongedToAsPrisoner == mobileParty.Party)
						{
							EndCaptivityAction.ApplyByPeace(elementCopyAtIndex.Character.HeroObject, null);
						}
						else
						{
							mobileParty.Party.PrisonRoster.RemoveTroop(elementCopyAtIndex.Character, 1, default(UniqueTroopDescriptor), 0);
						}
					}
				}
			}
		}

		// Token: 0x06003BA8 RID: 15272 RVA: 0x0011C334 File Offset: 0x0011A534
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x06003BA9 RID: 15273 RVA: 0x0011C338 File Offset: 0x0011A538
		private void DailyHeroTick(Hero hero)
		{
			if (hero.IsPrisoner && hero.PartyBelongedToAsPrisoner != null && hero != Hero.MainHero)
			{
				float num = 0.04f;
				if (hero.PartyBelongedToAsPrisoner.IsMobile && hero.PartyBelongedToAsPrisoner.MobileParty.CurrentSettlement == null)
				{
					num *= 5f - MathF.Pow((float)MathF.Min(81, hero.PartyBelongedToAsPrisoner.NumberOfHealthyMembers), 0.25f);
				}
				if (hero.PartyBelongedToAsPrisoner == PartyBase.MainParty || (hero.PartyBelongedToAsPrisoner.IsSettlement && hero.PartyBelongedToAsPrisoner.Settlement.OwnerClan == Clan.PlayerClan) || (hero.PartyBelongedToAsPrisoner.IsMobile && hero.PartyBelongedToAsPrisoner.MobileParty.CurrentSettlement != null && hero.PartyBelongedToAsPrisoner.MobileParty.CurrentSettlement.OwnerClan == Clan.PlayerClan))
				{
					num *= 0.5f;
				}
				ExplainedNumber explainedNumber = new ExplainedNumber(num, false, null);
				if (hero.PartyBelongedToAsPrisoner.IsSettlement && hero.PartyBelongedToAsPrisoner.Settlement.Town != null && hero.PartyBelongedToAsPrisoner.Settlement.Town.Governor != null)
				{
					Town town = hero.PartyBelongedToAsPrisoner.Settlement.Town;
					if (hero.PartyBelongedToAsPrisoner.Settlement.IsTown)
					{
						if (town.Governor.GetPerkValue(DefaultPerks.Riding.MountedPatrols))
						{
							explainedNumber.AddFactor(DefaultPerks.Riding.MountedPatrols.SecondaryBonus, DefaultPerks.Riding.MountedPatrols.Description);
						}
						if (town.Governor.GetPerkValue(DefaultPerks.Roguery.SweetTalker))
						{
							explainedNumber.AddFactor(DefaultPerks.Roguery.SweetTalker.SecondaryBonus, DefaultPerks.Roguery.SweetTalker.Description);
						}
					}
					if ((hero.PartyBelongedToAsPrisoner.Settlement.IsTown || hero.PartyBelongedToAsPrisoner.Settlement.IsCastle) && town.Governor.GetPerkValue(DefaultPerks.Engineering.DungeonArchitect))
					{
						explainedNumber.AddFactor(DefaultPerks.Engineering.DungeonArchitect.SecondaryBonus, DefaultPerks.Engineering.DungeonArchitect.Description);
					}
				}
				if (hero.PartyBelongedToAsPrisoner.IsMobile)
				{
					if (hero.GetPerkValue(DefaultPerks.Roguery.FleetFooted))
					{
						explainedNumber.AddFactor(DefaultPerks.Roguery.FleetFooted.SecondaryBonus, null);
					}
					if (hero.PartyBelongedToAsPrisoner.MobileParty.HasPerk(DefaultPerks.Riding.MountedPatrols, false))
					{
						PerkHelper.AddPerkBonusForParty(DefaultPerks.Riding.MountedPatrols, hero.PartyBelongedToAsPrisoner.MobileParty, true, ref explainedNumber);
					}
					if (hero.PartyBelongedToAsPrisoner.MobileParty.HasPerk(DefaultPerks.Roguery.RansomBroker, false))
					{
						PerkHelper.AddPerkBonusForParty(DefaultPerks.Roguery.RansomBroker, hero.PartyBelongedToAsPrisoner.MobileParty, false, ref explainedNumber);
					}
				}
				if (hero.PartyBelongedToAsPrisoner.IsMobile && hero.PartyBelongedToAsPrisoner.MobileParty.HasPerk(DefaultPerks.Scouting.KeenSight, true))
				{
					PerkHelper.AddPerkBonusForParty(DefaultPerks.Scouting.KeenSight, hero.PartyBelongedToAsPrisoner.MobileParty, false, ref explainedNumber);
				}
				if (MBRandom.RandomFloat < explainedNumber.ResultNumber)
				{
					EndCaptivityAction.ApplyByEscape(hero, null);
				}
			}
		}

		// Token: 0x06003BAA RID: 15274 RVA: 0x0011C61C File Offset: 0x0011A81C
		private void HourlyPartyTick(MobileParty mobileParty)
		{
			int prisonerSizeLimit = mobileParty.Party.PrisonerSizeLimit;
			if (mobileParty.MapEvent == null && mobileParty.SiegeEvent == null && mobileParty.PrisonRoster.TotalManCount > prisonerSizeLimit)
			{
				int num = mobileParty.PrisonRoster.TotalManCount - prisonerSizeLimit;
				for (int i = 0; i < num; i++)
				{
					bool flag = mobileParty.PrisonRoster.TotalRegulars > 0;
					float randomFloat = MBRandom.RandomFloat;
					int num2 = flag ? ((int)((float)mobileParty.PrisonRoster.TotalRegulars * randomFloat)) : ((int)((float)mobileParty.PrisonRoster.TotalManCount * randomFloat));
					CharacterObject character = null;
					foreach (TroopRosterElement troopRosterElement in mobileParty.PrisonRoster.GetTroopRoster())
					{
						if (!troopRosterElement.Character.IsHero || !flag)
						{
							num2 -= troopRosterElement.Number;
							if (num2 <= 0)
							{
								character = troopRosterElement.Character;
								break;
							}
						}
					}
					this.ApplyEscapeChanceToExceededPrisoners(character, mobileParty);
				}
			}
		}

		// Token: 0x06003BAB RID: 15275 RVA: 0x0011C73C File Offset: 0x0011A93C
		private void ApplyEscapeChanceToExceededPrisoners(CharacterObject character, MobileParty capturerParty)
		{
			ExplainedNumber explainedNumber = new ExplainedNumber(0.1f, false, null);
			if (capturerParty.HasPerk(DefaultPerks.Athletics.Stamina, true))
			{
				explainedNumber.AddFactor(-0.1f, DefaultPerks.Athletics.Stamina.Name);
			}
			if (capturerParty.IsGarrison || capturerParty.IsMilitia || character.IsPlayerCharacter)
			{
				return;
			}
			if (MBRandom.RandomFloat < explainedNumber.ResultNumber)
			{
				if (character.IsHero)
				{
					EndCaptivityAction.ApplyByEscape(character.HeroObject, null);
					return;
				}
				capturerParty.PrisonRoster.AddToCounts(character, -1, false, 0, 0, true, -1);
			}
		}

		// Token: 0x06003BAC RID: 15276 RVA: 0x0011C7CC File Offset: 0x0011A9CC
		private void OnSettlementOwnerChanged(Settlement settlement, bool openToClaim, Hero newOwner, Hero oldOwner, Hero capturerHero, ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail detail)
		{
			foreach (TroopRosterElement troopRosterElement in settlement.Party.PrisonRoster.GetTroopRoster())
			{
				if (troopRosterElement.Character.IsHero && troopRosterElement.Character.HeroObject != Hero.MainHero && !troopRosterElement.Character.HeroObject.MapFaction.IsAtWarWith(settlement.MapFaction))
				{
					if (troopRosterElement.Character.HeroObject.PartyBelongedToAsPrisoner == settlement.Party && troopRosterElement.Character.HeroObject.IsPrisoner)
					{
						EndCaptivityAction.ApplyByReleasedAfterBattle(troopRosterElement.Character.HeroObject);
					}
					else
					{
						settlement.Party.PrisonRoster.RemoveTroop(troopRosterElement.Character, troopRosterElement.Number, default(UniqueTroopDescriptor), 0);
					}
				}
			}
		}
	}
}
