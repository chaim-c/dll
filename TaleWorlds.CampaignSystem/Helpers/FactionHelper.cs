﻿using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.Extensions;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Party.PartyComponents;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace Helpers
{
	// Token: 0x02000013 RID: 19
	public static class FactionHelper
	{
		// Token: 0x060000A3 RID: 163 RVA: 0x00008BB8 File Offset: 0x00006DB8
		public static float FindPotentialStrength(IFaction faction)
		{
			float num = 0f;
			if (faction.IsKingdomFaction)
			{
				Kingdom kingdom = (Kingdom)faction;
				using (List<Clan>.Enumerator enumerator = kingdom.Clans.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Clan clan = enumerator.Current;
						float num2 = clan.IsUnderMercenaryService ? (((float)kingdom.Leader.Gold > 100000f) ? 0.3f : (0.3f - (1f - (float)kingdom.Leader.Gold / 100000f) * 0.3f)) : 1f;
						num += num2 * (float)clan.Tier * 100f;
					}
					goto IL_C6;
				}
			}
			if (faction.IsClan)
			{
				num += (float)((Clan)faction).Tier * 100f;
			}
			IL_C6:
			return num * 2f;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00008CA4 File Offset: 0x00006EA4
		public static float GetPowerRatioToEnemies(Kingdom kingdom)
		{
			float totalStrength = kingdom.TotalStrength;
			float totalEnemyKingdomPower = FactionHelper.GetTotalEnemyKingdomPower(kingdom);
			return totalStrength / (totalEnemyKingdomPower + 0.0001f);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00008CC8 File Offset: 0x00006EC8
		private static List<TextObject> IsFactionNameApplicable(string name)
		{
			List<TextObject> list = new List<TextObject>();
			if (name.Length > 50 || name.Length < 1)
			{
				TextObject item = GameTexts.FindText("str_faction_name_invalid_character_count", null).SetTextVariable("MIN", 1).SetTextVariable("MAX", 50);
				list.Add(item);
			}
			if (Common.TextContainsSpecialCharacters(name))
			{
				list.Add(GameTexts.FindText("str_faction_name_invalid_characters", null));
			}
			if (name.StartsWith(" ") || name.EndsWith(" "))
			{
				list.Add(new TextObject("{=LCOZZMta}Faction name cannot start or end with a white space", null));
			}
			if (name.Contains("  "))
			{
				list.Add(new TextObject("{=CtsdrQ9N}Faction name cannot contain consecutive white spaces", null));
			}
			return list;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00008D84 File Offset: 0x00006F84
		public static Tuple<bool, string> IsClanNameApplicable(string name)
		{
			string item = string.Empty;
			List<TextObject> list = FactionHelper.IsFactionNameApplicable(name);
			if (Clan.All.Any((Clan x) => x != Clan.PlayerClan && string.Equals(x.Name.ToString(), name, StringComparison.InvariantCultureIgnoreCase)))
			{
				list.Add(GameTexts.FindText("str_clan_name_invalid_already_exist", null));
			}
			bool item2 = list.Count == 0;
			if (list.Count == 1)
			{
				item = list[0].ToString();
			}
			else if (list.Count > 1)
			{
				TextObject textObject = list[0];
				for (int i = 1; i < list.Count; i++)
				{
					textObject = GameTexts.FindText("str_string_newline_newline_string", null).SetTextVariable("STR1", textObject.ToString()).SetTextVariable("STR2", list[i].ToString());
				}
				item = textObject.ToString();
			}
			return new Tuple<bool, string>(item2, item);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00008E68 File Offset: 0x00007068
		public static Tuple<bool, string> IsKingdomNameApplicable(string name)
		{
			string item = string.Empty;
			List<TextObject> list = FactionHelper.IsFactionNameApplicable(name);
			if (Kingdom.All.Any((Kingdom x) => x != Clan.PlayerClan.Kingdom && string.Equals(x.Name.ToString(), name, StringComparison.InvariantCultureIgnoreCase)))
			{
				list.Add(GameTexts.FindText("str_kingdom_name_invalid_already_exist", null));
			}
			bool item2 = list.Count == 0;
			if (list.Count == 1)
			{
				item = list[0].ToString();
			}
			else if (list.Count > 1)
			{
				TextObject textObject = list[0];
				for (int i = 1; i < list.Count; i++)
				{
					textObject = GameTexts.FindText("str_string_newline_newline_string", null).SetTextVariable("STR1", textObject.ToString()).SetTextVariable("STR2", list[i].ToString());
				}
				item = textObject.ToString();
			}
			return new Tuple<bool, string>(item2, item);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00008F4C File Offset: 0x0000714C
		public static float GetPowerRatioToTributePayedKingdoms(Kingdom kingdom)
		{
			float totalStrength = kingdom.TotalStrength;
			float totalTributePayedKingdomsPower = FactionHelper.GetTotalTributePayedKingdomsPower(kingdom);
			return totalStrength / (totalTributePayedKingdomsPower + 0.0001f);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00008F6E File Offset: 0x0000716E
		public static bool CanClanBeGrantedFief(Clan clan)
		{
			return clan != Clan.PlayerClan && !clan.IsUnderMercenaryService;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00008F84 File Offset: 0x00007184
		public static bool CanPlayerEnterFaction(bool asVassal = false)
		{
			float num = (from settlement in Campaign.Current.Settlements
			where (settlement.IsVillage || settlement.IsTown || settlement.IsCastle) && settlement.OwnerClan.Leader == Hero.MainHero
			select settlement).Sum((Settlement settlement) => settlement.GetSettlementValueForFaction(Hero.OneToOneConversationHero.MapFaction));
			float num2 = asVassal ? 50f : 10f;
			float num3 = Clan.PlayerClan.Renown + (asVassal ? (num / 5000f) : 0f) + (asVassal ? ((float)Hero.MainHero.Gold / 10000f) : 0f) + MathF.Min(num2, Clan.PlayerClan.Renown) / num2 * 0.2f * Clan.PlayerClan.TotalStrength + Hero.OneToOneConversationHero.MapFaction.Leader.GetRelationWithPlayer() * 2f;
			if (!asVassal)
			{
				return num3 > 25f;
			}
			return num3 > 150f;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00009080 File Offset: 0x00007280
		public static float GetTotalEnemyKingdomPower(Kingdom kingdom)
		{
			float num = 0f;
			foreach (Kingdom kingdom2 in FactionManager.GetEnemyKingdoms(kingdom))
			{
				num += kingdom2.TotalStrength;
			}
			return num;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000090D8 File Offset: 0x000072D8
		public static float GetTotalTributePayedKingdomsPower(Kingdom kingdom)
		{
			float num = 0f;
			foreach (StanceLink stanceLink in kingdom.Stances)
			{
				if (stanceLink.IsNeutral)
				{
					int dailyTributePaid = stanceLink.GetDailyTributePaid(kingdom);
					if (dailyTributePaid < 0)
					{
						float num2 = MathF.Sqrt(MathF.Min(1f, (float)(-(float)dailyTributePaid) / 4000f));
						IFaction faction = (stanceLink.Faction1 == kingdom) ? stanceLink.Faction2 : stanceLink.Faction1;
						num += num2 * faction.TotalStrength;
					}
				}
			}
			return num;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x0000917C File Offset: 0x0000737C
		public static IEnumerable<Army> GetKingdomArmies(IFaction mapFaction)
		{
			if (!mapFaction.IsKingdomFaction)
			{
				return new List<Army>();
			}
			return ((Kingdom)mapFaction).Armies;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00009197 File Offset: 0x00007397
		public static float SettlementProsperityEffectOnGarrisonSizeConstant(Town town)
		{
			return 2.2f * (0.1f + 0.9f * MathF.Sqrt(MathF.Min(town.Prosperity, 5000f) / 5000f));
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000091C8 File Offset: 0x000073C8
		public static float SettlementFoodPotentialEffectOnGarrisonSizeConstant(Settlement settlement)
		{
			int num = 0;
			if (settlement.IsFortification)
			{
				foreach (Village village in settlement.Town.Villages)
				{
					num += 5 * ((village.Hearth < 200f) ? 1 : ((village.Hearth < 600f) ? 2 : 3));
				}
			}
			return 0.5f + 0.5f * (float)MathF.Min(50, num) / 50f;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00009264 File Offset: 0x00007464
		public static float OwnerClanEconomyEffectOnGarrisonSizeConstant(Clan clan)
		{
			if (clan == null || clan.Leader == null)
			{
				return 1f;
			}
			if ((float)clan.Leader.Gold > 160000f)
			{
				return 1.5f + 0.5f * MathF.Min(1f, ((float)clan.Leader.Gold - 160000f) / 160000f);
			}
			if ((float)clan.Leader.Gold > 80000f)
			{
				return 1f + 0.5f * MathF.Min(1f, ((float)clan.Leader.Gold - 80000f) / 80000f);
			}
			if ((float)clan.Leader.Gold < 40000f)
			{
				return 1f - 0.75f * (1f - (float)clan.Leader.Gold / 40000f);
			}
			return 1f;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x0000934C File Offset: 0x0000754C
		public static float FindIdealGarrisonStrengthPerWalledCenter(Kingdom kingdom, Clan clan = null)
		{
			if (kingdom == null && clan == null)
			{
				return 0f;
			}
			float num = 0f;
			int num2;
			if (kingdom == null)
			{
				num2 = 0;
			}
			else
			{
				num2 = kingdom.Clans.Count((Clan x) => !x.IsUnderMercenaryService);
			}
			int num3 = num2;
			List<Town> list = (kingdom != null) ? kingdom.Fiefs : clan.Fiefs;
			float num4 = (kingdom != null) ? ((kingdom.TotalStrength + (float)(num3 * 500)) / 2f) : clan.TotalStrength;
			float num5 = 0f;
			float num6 = 0f;
			foreach (Town town in list)
			{
				float num7 = FactionHelper.SettlementProsperityEffectOnGarrisonSizeConstant(town);
				float num8 = FactionHelper.SettlementFoodPotentialEffectOnGarrisonSizeConstant(town.Settlement);
				num7 *= num8;
				float num9 = FactionHelper.OwnerClanEconomyEffectOnGarrisonSizeConstant(town.OwnerClan);
				num6 += num7;
				num7 *= num9;
				num += num7 * 60f;
				num5 += num7;
			}
			float num10 = num4 * 0.5f / num5;
			float num11 = num / num6;
			return 5f + (num10 + num11) / 2f;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x0000947C File Offset: 0x0000767C
		public static void FinishAllRelatedHostileActionsOfNobleToFaction(Hero noble, IFaction faction)
		{
			if (noble.PartyBelongedTo != null && noble.PartyBelongedTo.MapEvent != null && ((noble.PartyBelongedTo.MapEvent.AttackerSide.LeaderParty == noble.PartyBelongedTo.Party && ((faction.IsKingdomFaction && noble.PartyBelongedTo.MapEvent.DefenderSide.LeaderParty.MapFaction == faction) || (!faction.IsKingdomFaction && noble.PartyBelongedTo.MapEvent.DefenderSide.LeaderParty.Owner != null && noble.PartyBelongedTo.MapEvent.DefenderSide.LeaderParty.Owner.Clan == faction))) || (noble.PartyBelongedTo.MapEvent.DefenderSide.LeaderParty == noble.PartyBelongedTo.Party && ((faction.IsKingdomFaction && noble.PartyBelongedTo.MapEvent.AttackerSide.LeaderParty.MapFaction == faction) || (!faction.IsKingdomFaction && noble.PartyBelongedTo.MapEvent.AttackerSide.LeaderParty.Owner != null && noble.PartyBelongedTo.MapEvent.AttackerSide.LeaderParty.Owner.Clan == faction)))))
			{
				noble.PartyBelongedTo.MapEvent.DiplomaticallyFinished = true;
				List<PartyBase> list = new List<PartyBase>();
				foreach (MapEventParty mapEventParty in noble.PartyBelongedTo.MapEvent.AttackerSide.Parties)
				{
					list.Add(mapEventParty.Party);
				}
				if (noble.PartyBelongedTo.MapEvent.MapEventSettlement != null)
				{
					foreach (WarPartyComponent warPartyComponent in noble.PartyBelongedTo.MapEvent.MapEventSettlement.MapFaction.WarPartyComponents)
					{
						MobileParty mobileParty = warPartyComponent.MobileParty;
						if (mobileParty.DefaultBehavior == AiBehavior.DefendSettlement && mobileParty.TargetSettlement == noble.PartyBelongedTo.MapEvent.MapEventSettlement && mobileParty.CurrentSettlement == null)
						{
							mobileParty.Ai.SetMoveModeHold();
						}
					}
				}
				noble.PartyBelongedTo.MapEvent.Update();
				foreach (PartyBase partyBase in list)
				{
					if (partyBase.IsMobile)
					{
						partyBase.MobileParty.Ai.SetMoveModeHold();
					}
				}
			}
			if (noble.PartyBelongedTo != null)
			{
				MobileParty partyBelongedTo = noble.PartyBelongedTo;
				if (partyBelongedTo.BesiegedSettlement != null && ((faction.IsKingdomFaction && partyBelongedTo.BesiegedSettlement.MapFaction == faction) || (!faction.IsKingdomFaction && partyBelongedTo.BesiegedSettlement.OwnerClan == faction)))
				{
					foreach (WarPartyComponent warPartyComponent2 in partyBelongedTo.BesiegedSettlement.MapFaction.WarPartyComponents)
					{
						MobileParty mobileParty2 = warPartyComponent2.MobileParty;
						if (mobileParty2.DefaultBehavior == AiBehavior.DefendSettlement && mobileParty2.TargetSettlement == partyBelongedTo.BesiegedSettlement && mobileParty2.CurrentSettlement == null)
						{
							mobileParty2.Ai.SetMoveModeHold();
						}
					}
					partyBelongedTo.BesiegerCamp = null;
					partyBelongedTo.Ai.SetMoveModeHold();
				}
				if ((partyBelongedTo.DefaultBehavior == AiBehavior.RaidSettlement || partyBelongedTo.DefaultBehavior == AiBehavior.BesiegeSettlement || partyBelongedTo.DefaultBehavior == AiBehavior.AssaultSettlement) && ((faction.IsKingdomFaction && partyBelongedTo.TargetSettlement.MapFaction == faction) || (!faction.IsKingdomFaction && partyBelongedTo.TargetSettlement.OwnerClan == faction)))
				{
					if (partyBelongedTo.Army != null)
					{
						partyBelongedTo.Army.FinishArmyObjective();
					}
					partyBelongedTo.Ai.SetMoveModeHold();
				}
				if (partyBelongedTo.ShortTermBehavior == AiBehavior.EngageParty && partyBelongedTo.ShortTermTargetParty != null && partyBelongedTo.ShortTermTargetParty.MapFaction == faction)
				{
					partyBelongedTo.Ai.SetMoveModeHold();
				}
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000098C4 File Offset: 0x00007AC4
		public static void FinishAllRelatedHostileActionsOfFactionToFaction(IFaction faction1, IFaction faction2)
		{
			foreach (Hero noble in faction1.Lords)
			{
				FactionHelper.FinishAllRelatedHostileActionsOfNobleToFaction(noble, faction2);
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00009918 File Offset: 0x00007B18
		public static void FinishAllRelatedHostileActions(Clan clan1, Clan clan2)
		{
			foreach (Hero noble in clan1.Lords)
			{
				FactionHelper.FinishAllRelatedHostileActionsOfNobleToFaction(noble, clan2);
			}
			foreach (Hero noble2 in clan2.Lords)
			{
				FactionHelper.FinishAllRelatedHostileActionsOfNobleToFaction(noble2, clan1);
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000099AC File Offset: 0x00007BAC
		public static void FinishAllRelatedHostileActions(Kingdom kingdom1, Kingdom kingdom2)
		{
			foreach (Clan faction in kingdom1.Clans)
			{
				FactionHelper.FinishAllRelatedHostileActionsOfFactionToFaction(faction, kingdom2);
			}
			foreach (Clan faction2 in kingdom2.Clans)
			{
				FactionHelper.FinishAllRelatedHostileActionsOfFactionToFaction(faction2, kingdom1);
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00009A40 File Offset: 0x00007C40
		public static void AdjustFactionStancesForClanJoiningKingdom(Clan joiningClan, Kingdom kingdomToJoin)
		{
			foreach (StanceLink stanceLink in new List<StanceLink>(joiningClan.Stances))
			{
				if (!stanceLink.IsAtConstantWar)
				{
					IFaction faction = (stanceLink.Faction1 == joiningClan) ? stanceLink.Faction2 : stanceLink.Faction1;
					if (stanceLink.IsAtWar)
					{
						if (!kingdomToJoin.IsAtWarWith(faction))
						{
							MakePeaceAction.Apply(joiningClan, faction, 0);
							FactionHelper.FinishAllRelatedHostileActionsOfFactionToFaction(joiningClan, faction);
							FactionHelper.FinishAllRelatedHostileActionsOfFactionToFaction(faction, joiningClan);
						}
					}
					else
					{
						stanceLink.ResetPeaceStats();
					}
				}
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00009AE0 File Offset: 0x00007CE0
		public static TextObject GetTermUsedByOtherFaction(IFaction faction, IFaction otherFaction, bool pejorative)
		{
			if (faction.IsMinorFaction || faction.IsEliminated)
			{
				TextObject textObject = new TextObject("{=n48jo6Qn}the {FACTION_NAME}", null);
				textObject.SetTextVariable("FACTION_NAME", faction.Name);
				return textObject;
			}
			if (otherFaction.Culture == faction.Culture)
			{
				TextObject textObject2 = (!pejorative) ? new TextObject("{=WWFnlL3O}{FACTION_LIEGE}'s followers", null) : new TextObject("{=uujU2fSA}{FACTION_LIEGE}'s scum", null);
				textObject2.SetTextVariable("FACTION_LIEGE", (faction.Leader != null) ? faction.Leader.Name : TextObject.Empty);
				return textObject2;
			}
			int num = 0;
			using (List<Kingdom>.Enumerator enumerator = Kingdom.All.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Culture == faction.Culture)
					{
						num++;
					}
				}
			}
			TextObject textObject3 = (num == 1) ? new TextObject("{=bIWDtytH}the {ETHNIC_TERM}", null) : new TextObject("{=JrT9bBEK}{FACTION_LIEGE}'s {ETHNIC_TERM}", null);
			textObject3.SetTextVariable("ETHNIC_TERM", GameTexts.FindText("str_neutral_term_for_culture", faction.Culture.StringId));
			textObject3.SetTextVariable("FACTION_LIEGE", (faction.Leader != null) ? faction.Leader.Name : TextObject.Empty);
			return textObject3;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00009C20 File Offset: 0x00007E20
		public static TextObject GetFormalNameForFactionCulture(CultureObject factionCulture)
		{
			return GameTexts.FindText("str_faction_formal_name_for_culture", factionCulture.StringId);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00009C32 File Offset: 0x00007E32
		public static TextObject GetInformalNameForFactionCulture(CultureObject factionCulture)
		{
			return GameTexts.FindText("str_faction_informal_name_for_culture", factionCulture.StringId);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00009C44 File Offset: 0x00007E44
		public static TextObject GetAdjectiveForFactionCulture(CultureObject factionCulture)
		{
			return GameTexts.FindText("str_adjective_for_culture", factionCulture.StringId);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00009C56 File Offset: 0x00007E56
		public static TextObject GetAdjectiveForFaction(IFaction faction)
		{
			if (faction is Kingdom)
			{
				return GameTexts.FindText("str_adjective_for_faction", faction.StringId);
			}
			return faction.Name;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00009C78 File Offset: 0x00007E78
		public static TextObject GenerateClanNameforPlayer()
		{
			CultureObject culture = CharacterObject.PlayerCharacter.Culture;
			TextObject result;
			if (culture.StringId == "vlandia")
			{
				result = new TextObject("{=Uk3qRuCS}dey Corvand", null);
			}
			else
			{
				result = NameGenerator.Current.GenerateClanName(culture, null);
			}
			return result;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00009CC0 File Offset: 0x00007EC0
		public static float GetDistanceToClosestNonAllyFortificationOfFaction(IFaction faction)
		{
			float num = float.MaxValue;
			if (faction.FactionMidSettlement != null)
			{
				foreach (Town town in Town.AllFiefs)
				{
					Settlement settlement = town.Settlement;
					float num2;
					if (settlement.MapFaction != faction && Campaign.Current.Models.MapDistanceModel.GetDistance(settlement, faction.FactionMidSettlement, num, out num2))
					{
						num = num2;
					}
				}
			}
			return num;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00009D44 File Offset: 0x00007F44
		public static Settlement FactionMidSettlement(IFaction faction)
		{
			Settlement result = null;
			if (faction.Settlements.Count > 0)
			{
				float num = float.MaxValue;
				result = faction.Settlements[0];
				using (List<Settlement>.Enumerator enumerator = faction.Settlements.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Settlement settlement = enumerator.Current;
						float num2 = 0f;
						foreach (Settlement settlement2 in faction.Settlements)
						{
							if (settlement != settlement2)
							{
								float num3 = Campaign.Current.Models.MapDistanceModel.GetDistance(settlement, settlement2);
								if (settlement2.IsVillage)
								{
									num3 *= 0.2f;
								}
								if (settlement2.IsFortification)
								{
									num3 *= (float)settlement2.BoundVillages.Count * 0.1f;
								}
								num2 += num3;
							}
						}
						if (num > num2)
						{
							num = num2;
							result = settlement;
						}
					}
					return result;
				}
			}
			Clan clan;
			Kingdom kingdom;
			if ((clan = (faction as Clan)) != null)
			{
				result = clan.HomeSettlement;
			}
			else if ((kingdom = (faction as Kingdom)) != null)
			{
				result = kingdom.InitialHomeLand;
			}
			return result;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00009E8C File Offset: 0x0000808C
		public static List<IFaction> GetPossibleKingdomsToDeclareWar(Kingdom kingdom)
		{
			List<IFaction> list = new List<IFaction>();
			foreach (Kingdom kingdom2 in Kingdom.All)
			{
				if (kingdom2 != kingdom && !FactionManager.IsAtWarAgainstFaction(kingdom2, kingdom))
				{
					list.Add(kingdom2);
				}
			}
			return list;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00009EF4 File Offset: 0x000080F4
		public static List<IFaction> GetPossibleKingdomsToDeclarePeace(Kingdom kingdom)
		{
			List<IFaction> list = new List<IFaction>();
			foreach (Kingdom kingdom2 in Kingdom.All)
			{
				if (kingdom2 != kingdom && FactionManager.IsAtWarAgainstFaction(kingdom2, kingdom))
				{
					list.Add(kingdom2);
				}
			}
			return list;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00009F5C File Offset: 0x0000815C
		public static IEnumerable<Clan> GetAllyMinorFactions(CharacterObject otherCharacter)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00009F64 File Offset: 0x00008164
		public static Clan ChooseHeirClanForFiefs(Clan oldClan)
		{
			Clan clan = null;
			if (oldClan.Kingdom != null)
			{
				if (!oldClan.Kingdom.IsEliminated && oldClan.Kingdom.RulingClan != oldClan)
				{
					clan = oldClan.Kingdom.RulingClan;
				}
				else
				{
					clan = oldClan.Kingdom.Clans.GetRandomElementWithPredicate(delegate(Clan t)
					{
						if (t != oldClan && !t.IsEliminated && !t.IsMinorFaction && !t.Lords.IsEmpty<Hero>())
						{
							return t.Lords.Any((Hero k) => !k.IsChild);
						}
						return false;
					});
				}
			}
			if (clan == null)
			{
				float num = float.MaxValue;
				IEnumerable<Clan> all = Clan.All;
				Func<Clan, bool> <>9__2;
				Func<Clan, bool> predicate;
				if ((predicate = <>9__2) == null)
				{
					predicate = (<>9__2 = delegate(Clan t)
					{
						if (t != oldClan && !t.IsEliminated && !t.IsMinorFaction && !t.Lords.IsEmpty<Hero>())
						{
							if (t.Lords.Any((Hero k) => !k.IsChild))
							{
								return !t.IsBanditFaction;
							}
						}
						return false;
					});
				}
				foreach (Clan clan2 in all.Where(predicate))
				{
					float distance = Campaign.Current.Models.MapDistanceModel.GetDistance(clan2.FactionMidSettlement, oldClan.FactionMidSettlement);
					if (distance < num)
					{
						clan = clan2;
						num = distance;
					}
				}
				if (((clan != null) ? clan.Kingdom : null) != null && !clan.Kingdom.IsEliminated)
				{
					clan = clan.Kingdom.RulingClan;
				}
			}
			if (clan == null)
			{
				clan = Clan.PlayerClan;
			}
			return clan;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x0000A0BC File Offset: 0x000082BC
		private static bool IsMainClanMemberAvailableForRelocate(Hero hero, out TextObject explanation)
		{
			if (hero.Age < (float)Campaign.Current.Models.AgeModel.HeroComesOfAge)
			{
				explanation = new TextObject("{=HAo6iIda}{HERO.NAME} is not eligible.", null);
				explanation.SetCharacterProperties("HERO", hero.CharacterObject, false);
				return false;
			}
			if (hero.PartyBelongedTo != null)
			{
				if (hero.PartyBelongedTo.LeaderHero == hero)
				{
					explanation = new TextObject("{=kNW1qYSi}{HERO.NAME} is leading a party right now.", null);
					explanation.SetCharacterProperties("HERO", hero.CharacterObject, false);
					return false;
				}
				if (hero.PartyBelongedTo.MapEvent != null)
				{
					explanation = new TextObject("{=haY6IEw2}{HERO.NAME} is currently in a battle right now.", null);
					explanation.SetCharacterProperties("HERO", hero.CharacterObject, false);
					return false;
				}
			}
			if (hero.IsPrisoner)
			{
				explanation = new TextObject("{=hv1ARuaU}{HERO.NAME} is in prison right now.", null);
				explanation.SetCharacterProperties("HERO", hero.CharacterObject, false);
				return false;
			}
			if (hero.IsReleased)
			{
				explanation = new TextObject("{=nMmYZ3xi}{HERO.NAME} is not available right now.", null);
				explanation.SetCharacterProperties("HERO", hero.CharacterObject, false);
				return false;
			}
			if (hero.IsTraveling)
			{
				explanation = new TextObject("{=287Epvf0}{HERO.NAME} is traveling right now.", null);
				explanation.SetCharacterProperties("HERO", hero.CharacterObject, false);
				return false;
			}
			if (Campaign.Current.IssueManager.IssueSolvingCompanionList.Contains(hero))
			{
				explanation = new TextObject("{=se5704KH}{HERO.NAME} is solving an issue right now.", null);
				explanation.SetCharacterProperties("HERO", hero.CharacterObject, false);
				return false;
			}
			if (Campaign.Current.GetCampaignBehavior<IAlleyCampaignBehavior>().IsHeroAlleyLeaderOfAnyPlayerAlley(hero))
			{
				explanation = new TextObject("{=WBcw6Z9W}{HERO.NAME} is leading an alley.", null);
				explanation.SetCharacterProperties("HERO", hero.CharacterObject, false);
				return false;
			}
			if (hero.IsFugitive || hero.IsDisabled || !hero.CanBeGovernorOrHavePartyRole())
			{
				explanation = new TextObject("{=nMmYZ3xi}{HERO.NAME} is not available right now.", null);
				explanation.SetCharacterProperties("HERO", hero.CharacterObject, false);
				return false;
			}
			explanation = TextObject.Empty;
			return true;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x0000A29C File Offset: 0x0000849C
		public static bool CanPlayerOfferMercenaryService(Kingdom offerKingdom, out List<IFaction> playerWars, out List<IFaction> warsOfFactionToJoin)
		{
			playerWars = new List<IFaction>();
			warsOfFactionToJoin = new List<IFaction>();
			float strengthThresholdForNonMutualWarsToBeIgnoredToJoinKingdom = Campaign.Current.Models.DiplomacyModel.GetStrengthThresholdForNonMutualWarsToBeIgnoredToJoinKingdom(offerKingdom);
			foreach (Kingdom kingdom in Kingdom.All)
			{
				if (Clan.PlayerClan.MapFaction.IsAtWarWith(kingdom) && kingdom.TotalStrength > strengthThresholdForNonMutualWarsToBeIgnoredToJoinKingdom)
				{
					playerWars.Add(kingdom);
				}
			}
			foreach (Kingdom kingdom2 in Kingdom.All)
			{
				if (offerKingdom.IsAtWarWith(kingdom2))
				{
					warsOfFactionToJoin.Add(kingdom2);
				}
			}
			return Clan.PlayerClan.Kingdom == null && !Clan.PlayerClan.IsAtWarWith(offerKingdom) && Clan.PlayerClan.Tier >= Campaign.Current.Models.ClanTierModel.MercenaryEligibleTier && offerKingdom.Leader.GetRelationWithPlayer() >= (float)Campaign.Current.Models.DiplomacyModel.MinimumRelationWithConversationCharacterToJoinKingdom && warsOfFactionToJoin.Intersect(playerWars).Count<IFaction>() == playerWars.Count && Clan.PlayerClan.Settlements.IsEmpty<Settlement>();
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x0000A3FC File Offset: 0x000085FC
		public static bool CanPlayerOfferVassalage(Kingdom offerKingdom, out List<IFaction> playerWars, out List<IFaction> warsOfFactionToJoin)
		{
			playerWars = new List<IFaction>();
			warsOfFactionToJoin = new List<IFaction>();
			float strengthThresholdForNonMutualWarsToBeIgnoredToJoinKingdom = Campaign.Current.Models.DiplomacyModel.GetStrengthThresholdForNonMutualWarsToBeIgnoredToJoinKingdom(offerKingdom);
			foreach (Kingdom kingdom in Kingdom.All)
			{
				if (Clan.PlayerClan.MapFaction.IsAtWarWith(kingdom) && kingdom.TotalStrength > strengthThresholdForNonMutualWarsToBeIgnoredToJoinKingdom)
				{
					playerWars.Add(kingdom);
				}
			}
			foreach (Kingdom kingdom2 in Kingdom.All)
			{
				if (offerKingdom.IsAtWarWith(kingdom2))
				{
					warsOfFactionToJoin.Add(kingdom2);
				}
			}
			return (Clan.PlayerClan.Kingdom == null || Clan.PlayerClan.IsUnderMercenaryService) && !Clan.PlayerClan.IsAtWarWith(offerKingdom) && Clan.PlayerClan.Tier >= Campaign.Current.Models.ClanTierModel.VassalEligibleTier && !offerKingdom.IsEliminated && offerKingdom.Leader.GetRelationWithPlayer() >= (float)Campaign.Current.Models.DiplomacyModel.MinimumRelationWithConversationCharacterToJoinKingdom && warsOfFactionToJoin.Intersect(playerWars).Count<IFaction>() == playerWars.Count;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x0000A564 File Offset: 0x00008764
		public static bool IsMainClanMemberAvailableForRecall(Hero hero, MobileParty targetParty, out TextObject explanation)
		{
			if (hero.PartyBelongedTo != null && hero.PartyBelongedTo.IsMainParty)
			{
				explanation = new TextObject("{=uhOCqJwd}{HERO.NAME} is already in the main party.", null);
				explanation.SetCharacterProperties("HERO", hero.CharacterObject, false);
				return false;
			}
			if (hero.CurrentSettlement != null && (hero.CurrentSettlement.IsUnderSiege || hero.CurrentSettlement.IsUnderRaid))
			{
				explanation = new TextObject("{=L9nn40qu}{HERO.NAME}{.o} location is under attack right now.", null);
				explanation.SetCharacterProperties("HERO", hero.CharacterObject, false);
				return false;
			}
			if (Hero.MainHero.IsPrisoner)
			{
				explanation = new TextObject("{=jRslIaiU}You can't recall a clan member while you are a prisoner.", null);
				return false;
			}
			if (MobileParty.MainParty.MapEvent != null)
			{
				explanation = new TextObject("{=h0pBxG09}You can't recall a clan member while you are in a map event.", null);
				return false;
			}
			if (!FactionHelper.IsMainClanMemberAvailableForRelocate(hero, out explanation))
			{
				return false;
			}
			explanation = new TextObject("{=NAseSXPl}It would take {HOUR} {?HOUR > 1}hours{?}hour{\\?} for {HERO.NAME} to arrive at your party.", null);
			explanation.SetCharacterProperties("HERO", hero.CharacterObject, false);
			float resultNumber = Campaign.Current.Models.DelayedTeleportationModel.GetTeleportationDelayAsHours(hero, targetParty.Party).ResultNumber;
			explanation.SetTextVariable("HOUR", (int)Math.Ceiling((double)resultNumber));
			return true;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x0000A688 File Offset: 0x00008888
		public static bool IsMainClanMemberAvailableForPartyLeaderChange(Hero hero, bool isSend, MobileParty targetParty, out TextObject explanation)
		{
			if (hero.PartyBelongedTo != null && hero.PartyBelongedTo.IsMainParty && !isSend)
			{
				explanation = new TextObject("{=uhOCqJwd}{HERO.NAME} is already in the main party.", null);
				explanation.SetCharacterProperties("HERO", hero.CharacterObject, false);
				return false;
			}
			if (targetParty.MemberRoster.Count == 1 && targetParty.LeaderHero != null)
			{
				explanation = new TextObject("{=pwuEqegC}Party leader is the only member of the party right now.", null);
				return false;
			}
			if (targetParty.MapEvent != null)
			{
				explanation = new TextObject("{=yC52EBCb}Target party is currently in a battle right now.", null);
				return false;
			}
			if (targetParty.Army != null)
			{
				explanation = new TextObject("{=2iRg3vpP}Target party is currently in an army right now.", null);
				return false;
			}
			if (hero.CurrentSettlement != null && (hero.CurrentSettlement.IsUnderSiege || hero.CurrentSettlement.IsUnderRaid))
			{
				explanation = new TextObject("{=L9nn40qu}{HERO.NAME}{.o} location is under attack right now.", null);
				explanation.SetCharacterProperties("HERO", hero.CharacterObject, false);
				return false;
			}
			if (hero.GovernorOf != null)
			{
				explanation = new TextObject("{=bgVZcd1I}{HERO.NAME} is a governor.", null);
				explanation.SetCharacterProperties("HERO", hero.CharacterObject, false);
				return false;
			}
			if (!FactionHelper.IsMainClanMemberAvailableForRelocate(hero, out explanation))
			{
				return false;
			}
			explanation = new TextObject("{=NAseSXPl}It would take {HOUR} {?HOUR > 1}hours{?}hour{\\?} for {HERO.NAME} to arrive at your party.", null);
			explanation.SetCharacterProperties("HERO", hero.CharacterObject, false);
			float resultNumber = Campaign.Current.Models.DelayedTeleportationModel.GetTeleportationDelayAsHours(hero, targetParty.Party).ResultNumber;
			explanation.SetTextVariable("HOUR", (int)Math.Ceiling((double)resultNumber));
			return true;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x0000A7F8 File Offset: 0x000089F8
		public static bool IsMainClanMemberAvailableForSendingSettlement(Hero hero, Settlement targetSettlement, out TextObject explanation)
		{
			if (hero.CurrentSettlement != null && (hero.CurrentSettlement.IsUnderSiege || hero.CurrentSettlement.IsUnderRaid))
			{
				explanation = new TextObject("{=L9nn40qu}{HERO.NAME}{.o} location is under attack right now.", null);
				explanation.SetCharacterProperties("HERO", hero.CharacterObject, false);
				return false;
			}
			if (targetSettlement.IsUnderRaid || targetSettlement.IsUnderSiege)
			{
				explanation = new TextObject("{=1tGP6vJn}Target settlement is under attack right now.", null);
				return false;
			}
			if (hero.GovernorOf != null)
			{
				explanation = new TextObject("{=bgVZcd1I}{HERO.NAME} is a governor.", null);
				explanation.SetCharacterProperties("HERO", hero.CharacterObject, false);
				return false;
			}
			if (!FactionHelper.IsMainClanMemberAvailableForRelocate(hero, out explanation))
			{
				return false;
			}
			explanation = new TextObject("{=NAseSXPl}It would take {HOUR} {?HOUR > 1}hours{?}hour{\\?} for {HERO.NAME} to arrive at your party.", null);
			explanation.SetCharacterProperties("HERO", hero.CharacterObject, false);
			float resultNumber = Campaign.Current.Models.DelayedTeleportationModel.GetTeleportationDelayAsHours(hero, targetSettlement.Party).ResultNumber;
			explanation.SetTextVariable("HOUR", (int)Math.Ceiling((double)resultNumber));
			return true;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x0000A8F8 File Offset: 0x00008AF8
		public static bool IsMainClanMemberAvailableForSendingSettlementAsGovernor(Hero hero, Settlement settlementOfGovernor, out TextObject explanation)
		{
			if (hero.PartyBelongedToAsPrisoner != null)
			{
				explanation = new TextObject("{=knwId8DG}You cannot assign a prisoner as a governor of a settlement", null);
				return false;
			}
			if (hero == Hero.MainHero)
			{
				explanation = new TextObject("{=uoDuiBZR}You cannot assign yourself as a governor", null);
				return false;
			}
			if (hero.PartyBelongedTo != null && hero.PartyBelongedTo.LeaderHero == hero)
			{
				explanation = new TextObject("{=pWObBhj5}You cannot assign a party leader as a new governor of a settlement", null);
				return false;
			}
			if (hero.IsFugitive)
			{
				explanation = new TextObject("{=KghY9qwl}You cannot assign a fugitive as the new governor of a settlement", null);
				return false;
			}
			if (hero.IsReleased)
			{
				explanation = new TextObject("{=mOFjZuSf}You cannot assign a newly released hero as the new governor of a settlement", null);
				return false;
			}
			if (settlementOfGovernor != null)
			{
				explanation = new TextObject("{=YbGu9rSH}This character is already the governor of {SETTLEMENT_NAME}", null);
				explanation.SetTextVariable("SETTLEMENT_NAME", settlementOfGovernor.Town.Name);
				return false;
			}
			if (!FactionHelper.IsMainClanMemberAvailableForRelocate(hero, out explanation))
			{
				return false;
			}
			explanation = TextObject.Empty;
			return true;
		}
	}
}
