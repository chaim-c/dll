using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Helpers;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.Conversation;
using TaleWorlds.CampaignSystem.CraftingSystem;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.Encyclopedia;
using TaleWorlds.CampaignSystem.Extensions;
using TaleWorlds.CampaignSystem.Issues;
using TaleWorlds.CampaignSystem.Map;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Workshops;
using TaleWorlds.CampaignSystem.Siege;
using TaleWorlds.CampaignSystem.ViewModelCollection.WeaponCrafting.WeaponDesign.Order;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Generic;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Core.ViewModelCollection.Information.RundownTooltip;
using TaleWorlds.Library;
using TaleWorlds.Library.Information;
using TaleWorlds.Localization;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.CampaignSystem.ViewModelCollection
{
	// Token: 0x02000015 RID: 21
	public static class CampaignUIHelper
	{
		// Token: 0x060000CC RID: 204 RVA: 0x00004208 File Offset: 0x00002408
		private static void TooltipAddPropertyTitleWithValue(List<TooltipProperty> properties, string propertyName, float currentValue)
		{
			string value = currentValue.ToString("0.##");
			properties.Add(new TooltipProperty(propertyName, value, 0, false, TooltipProperty.TooltipPropertyFlags.Title));
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00004236 File Offset: 0x00002436
		private static void TooltipAddPropertyTitleWithValue(List<TooltipProperty> properties, string propertyName, string currentValue)
		{
			properties.Add(new TooltipProperty(propertyName, currentValue, 0, false, TooltipProperty.TooltipPropertyFlags.Title));
		}

		// Token: 0x060000CE RID: 206 RVA: 0x0000424C File Offset: 0x0000244C
		private static void TooltipAddExplanation(List<TooltipProperty> properties, ref ExplainedNumber explainedNumber)
		{
			List<ValueTuple<string, float>> lines = explainedNumber.GetLines();
			if (lines.Count > 0)
			{
				for (int i = 0; i < lines.Count; i++)
				{
					ValueTuple<string, float> valueTuple = lines[i];
					string item = valueTuple.Item1;
					float item2 = valueTuple.Item2;
					if ((double)MathF.Abs(item2) >= 0.01)
					{
						string changeValueString = CampaignUIHelper.GetChangeValueString(item2);
						properties.Add(new TooltipProperty(item, changeValueString, 0, false, TooltipProperty.TooltipPropertyFlags.None));
					}
				}
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000042B9 File Offset: 0x000024B9
		private static void TooltipAddPropertyTitle(List<TooltipProperty> properties, string propertyName)
		{
			properties.Add(new TooltipProperty(propertyName, string.Empty, 0, false, TooltipProperty.TooltipPropertyFlags.Title));
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000042D4 File Offset: 0x000024D4
		private static void TooltipAddExplainedResultChange(List<TooltipProperty> properties, float changeValue)
		{
			string changeValueString = CampaignUIHelper.GetChangeValueString(changeValue);
			properties.Add(new TooltipProperty(CampaignUIHelper._changeStr.ToString(), changeValueString, 0, false, TooltipProperty.TooltipPropertyFlags.RundownResult));
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004305 File Offset: 0x00002505
		private static void TooltipAddExplanedChange(List<TooltipProperty> properties, ref ExplainedNumber explainedNumber)
		{
			CampaignUIHelper.TooltipAddExplanation(properties, ref explainedNumber);
			CampaignUIHelper.TooltipAddDoubleSeperator(properties, false);
			CampaignUIHelper.TooltipAddExplainedResultChange(properties, explainedNumber.ResultNumber);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004324 File Offset: 0x00002524
		private static void TooltipAddExplainedResultTotal(List<TooltipProperty> properties, float changeValue)
		{
			string changeValueString = CampaignUIHelper.GetChangeValueString(changeValue);
			properties.Add(new TooltipProperty(CampaignUIHelper._totalStr.ToString(), changeValueString, 0, false, TooltipProperty.TooltipPropertyFlags.RundownResult));
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00004355 File Offset: 0x00002555
		public static List<TooltipProperty> GetTooltipForAccumulatingProperty(string propertyName, float currentValue, ExplainedNumber explainedNumber)
		{
			List<TooltipProperty> list = new List<TooltipProperty>();
			CampaignUIHelper.TooltipAddPropertyTitleWithValue(list, propertyName, currentValue);
			CampaignUIHelper.TooltipAddExplanedChange(list, ref explainedNumber);
			return list;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0000436C File Offset: 0x0000256C
		public static List<TooltipProperty> GetTooltipForAccumulatingPropertyWithResult(string propertyName, float currentValue, ref ExplainedNumber explainedNumber)
		{
			List<TooltipProperty> list = new List<TooltipProperty>();
			CampaignUIHelper.TooltipAddPropertyTitle(list, propertyName);
			CampaignUIHelper.TooltipAddExplanation(list, ref explainedNumber);
			CampaignUIHelper.TooltipAddDoubleSeperator(list, false);
			CampaignUIHelper.TooltipAddExplainedResultTotal(list, currentValue);
			return list;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x0000438F File Offset: 0x0000258F
		public static List<TooltipProperty> GetTooltipForgProperty(string propertyName, float currentValue, ExplainedNumber explainedNumber)
		{
			List<TooltipProperty> list = new List<TooltipProperty>();
			CampaignUIHelper.TooltipAddPropertyTitleWithValue(list, propertyName, currentValue);
			CampaignUIHelper.TooltipAddDoubleSeperator(list, false);
			CampaignUIHelper.TooltipAddExplanation(list, ref explainedNumber);
			return list;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x000043AD File Offset: 0x000025AD
		private static void TooltipAddSeperator(List<TooltipProperty> properties, bool onlyShowOnExtend = false)
		{
			properties.Add(new TooltipProperty("", string.Empty, 0, onlyShowOnExtend, TooltipProperty.TooltipPropertyFlags.DefaultSeperator));
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000043CB File Offset: 0x000025CB
		private static void TooltipAddDoubleSeperator(List<TooltipProperty> properties, bool onlyShowOnExtend = false)
		{
			properties.Add(new TooltipProperty("", string.Empty, 0, onlyShowOnExtend, TooltipProperty.TooltipPropertyFlags.RundownSeperator));
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000043EC File Offset: 0x000025EC
		private static void TooltipAddExtendInfo(List<TooltipProperty> properties, bool onlyShowOnExtend = false)
		{
			TooltipProperty item = new TooltipProperty("", "", -1, false, TooltipProperty.TooltipPropertyFlags.None)
			{
				OnlyShowWhenNotExtended = true
			};
			properties.Add(item);
			GameTexts.SetVariable("EXTEND_KEY", Game.Current.GameTextManager.FindText("str_game_key_text", "anyalt").ToString());
			TooltipProperty item2 = new TooltipProperty("", GameTexts.FindText("str_map_tooltip_info", null).ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.None)
			{
				OnlyShowWhenNotExtended = true
			};
			properties.Add(item2);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x0000446E File Offset: 0x0000266E
		private static void TooltipAddEmptyLine(List<TooltipProperty> properties, bool onlyShowOnExtend = false)
		{
			properties.Add(new TooltipProperty(string.Empty, string.Empty, -1, onlyShowOnExtend, TooltipProperty.TooltipPropertyFlags.None));
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00004488 File Offset: 0x00002688
		public static string GetTownWallsTooltip(Town town)
		{
			TextObject textObject;
			bool flag = CampaignUIHelper.IsSettlementInformationHidden(town.Settlement, out textObject);
			GameTexts.SetVariable("newline", "\n");
			if (flag)
			{
				GameTexts.SetVariable("LEVEL", GameTexts.FindText("str_missing_info_indicator", null).ToString());
			}
			else
			{
				GameTexts.SetVariable("LEVEL", town.GetWallLevel());
			}
			return GameTexts.FindText("str_walls_with_value", null).ToString();
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000044EF File Offset: 0x000026EF
		public static List<TooltipProperty> GetVillageMilitiaTooltip(Village village)
		{
			return CampaignUIHelper.GetSettlementPropertyTooltip(village.Settlement, CampaignUIHelper._militiaStr.ToString(), village.Militia, village.MilitiaChangeExplanation);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004512 File Offset: 0x00002712
		public static List<TooltipProperty> GetTownMilitiaTooltip(Town town)
		{
			return CampaignUIHelper.GetSettlementPropertyTooltip(town.Settlement, CampaignUIHelper._militiaStr.ToString(), town.Militia, town.MilitiaChangeExplanation);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00004535 File Offset: 0x00002735
		public static List<TooltipProperty> GetTownFoodTooltip(Town town)
		{
			return CampaignUIHelper.GetSettlementPropertyTooltip(town.Settlement, CampaignUIHelper._foodStr.ToString(), town.FoodStocks, town.FoodChangeExplanation);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00004558 File Offset: 0x00002758
		public static List<TooltipProperty> GetTownLoyaltyTooltip(Town town)
		{
			TextObject textObject;
			bool flag = CampaignUIHelper.IsSettlementInformationHidden(town.Settlement, out textObject);
			ExplainedNumber loyaltyChangeExplanation = town.LoyaltyChangeExplanation;
			List<TooltipProperty> settlementPropertyTooltip = CampaignUIHelper.GetSettlementPropertyTooltip(town.Settlement, CampaignUIHelper._loyaltyStr.ToString(), town.Loyalty, loyaltyChangeExplanation);
			if (!flag)
			{
				if (!town.OwnerClan.IsRebelClan)
				{
					if (town.Loyalty < (float)Campaign.Current.Models.SettlementLoyaltyModel.RebellionStartLoyaltyThreshold)
					{
						CampaignUIHelper.TooltipAddSeperator(settlementPropertyTooltip, false);
						settlementPropertyTooltip.Add(new TooltipProperty(" ", new TextObject("{=NxEy5Nbt}High risk of rebellion", null).ToString(), 1, UIColors.NegativeIndicator, false, TooltipProperty.TooltipPropertyFlags.None));
					}
					else if (town.Loyalty < (float)Campaign.Current.Models.SettlementLoyaltyModel.RebelliousStateStartLoyaltyThreshold && loyaltyChangeExplanation.ResultNumber < 0f)
					{
						CampaignUIHelper.TooltipAddSeperator(settlementPropertyTooltip, false);
						settlementPropertyTooltip.Add(new TooltipProperty(" ", new TextObject("{=F0a7hyp0}Risk of rebellion", null).ToString(), 1, UIColors.NegativeIndicator, false, TooltipProperty.TooltipPropertyFlags.None));
					}
				}
				else
				{
					CampaignUIHelper.TooltipAddSeperator(settlementPropertyTooltip, false);
					settlementPropertyTooltip.Add(new TooltipProperty(" ", new TextObject("{=hOVPiG3z}Recently rebelled", null).ToString(), 1, UIColors.NegativeIndicator, false, TooltipProperty.TooltipPropertyFlags.None));
				}
			}
			return settlementPropertyTooltip;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004688 File Offset: 0x00002888
		public static List<TooltipProperty> GetTownProsperityTooltip(Town town)
		{
			return CampaignUIHelper.GetSettlementPropertyTooltip(town.Settlement, CampaignUIHelper._prosperityStr.ToString(), town.Prosperity, town.ProsperityChangeExplanation);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000046AC File Offset: 0x000028AC
		public static List<TooltipProperty> GetTownDailyProductionTooltip(Town town)
		{
			ExplainedNumber constructionExplanation = town.ConstructionExplanation;
			return CampaignUIHelper.GetSettlementPropertyTooltipWithResult(town.Settlement, CampaignUIHelper._dailyProductionStr.ToString(), constructionExplanation.ResultNumber, ref constructionExplanation);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000046E0 File Offset: 0x000028E0
		public static List<TooltipProperty> GetTownSecurityTooltip(Town town)
		{
			ExplainedNumber securityChangeExplanation = town.SecurityChangeExplanation;
			return CampaignUIHelper.GetSettlementPropertyTooltip(town.Settlement, CampaignUIHelper._securityStr.ToString(), town.Security, securityChangeExplanation);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00004710 File Offset: 0x00002910
		public static List<TooltipProperty> GetVillageProsperityTooltip(Village village)
		{
			return CampaignUIHelper.GetSettlementPropertyTooltip(village.Settlement, CampaignUIHelper._hearthStr.ToString(), village.Hearth, village.HearthChangeExplanation);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00004733 File Offset: 0x00002933
		public static List<TooltipProperty> GetTownGarrisonTooltip(Town town)
		{
			Settlement settlement = town.Settlement;
			string valueName = CampaignUIHelper._garrisonStr.ToString();
			MobileParty garrisonParty = town.GarrisonParty;
			return CampaignUIHelper.GetSettlementPropertyTooltip(settlement, valueName, (float)((garrisonParty != null) ? garrisonParty.MemberRoster.TotalManCount : 0), town.GarrisonChangeExplanation);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004768 File Offset: 0x00002968
		public static List<TooltipProperty> GetPartyTroopSizeLimitTooltip(PartyBase party)
		{
			ExplainedNumber partySizeLimitExplainer = party.PartySizeLimitExplainer;
			return CampaignUIHelper.GetTooltipForAccumulatingPropertyWithResult(CampaignUIHelper._partyTroopSizeLimitStr.ToString(), partySizeLimitExplainer.ResultNumber, ref partySizeLimitExplainer);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00004794 File Offset: 0x00002994
		public static List<TooltipProperty> GetPartyPrisonerSizeLimitTooltip(PartyBase party)
		{
			ExplainedNumber prisonerSizeLimitExplainer = party.PrisonerSizeLimitExplainer;
			return CampaignUIHelper.GetTooltipForAccumulatingPropertyWithResult(CampaignUIHelper._partyPrisonerSizeLimitStr.ToString(), prisonerSizeLimitExplainer.ResultNumber, ref prisonerSizeLimitExplainer);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000047C0 File Offset: 0x000029C0
		public static List<TooltipProperty> GetUsedHorsesTooltip(List<Tuple<EquipmentElement, int>> usedUpgradeHorsesHistory)
		{
			List<TooltipProperty> list = new List<TooltipProperty>();
			if (usedUpgradeHorsesHistory.Count > 0)
			{
				foreach (IGrouping<ItemCategory, Tuple<EquipmentElement, int>> grouping in from h in usedUpgradeHorsesHistory
				group h by h.Item1.Item.ItemCategory)
				{
					int num = grouping.Sum((Tuple<EquipmentElement, int> c) => c.Item2);
					list.Add(new TooltipProperty(grouping.Key.GetName().ToString(), num.ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.None));
				}
			}
			return list;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00004884 File Offset: 0x00002A84
		public static List<TooltipProperty> GetArmyCohesionTooltip(Army army)
		{
			return CampaignUIHelper.GetTooltipForAccumulatingProperty(CampaignUIHelper._armyCohesionStr.ToString(), army.Cohesion, army.DailyCohesionChangeExplanation);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000048A4 File Offset: 0x00002AA4
		public static List<TooltipProperty> GetArmyManCountTooltip(Army army)
		{
			List<TooltipProperty> list = new List<TooltipProperty>();
			if (army.LeaderParty != null)
			{
				list.Add(new TooltipProperty("", CampaignUIHelper._numTotalTroopsInTheArmyStr.ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.Title));
				Dictionary<FormationClass, int> dictionary = new Dictionary<FormationClass, int>();
				Dictionary<FormationClass, int> dictionary2 = new Dictionary<FormationClass, int>();
				for (int i = 0; i < army.LeaderParty.MemberRoster.Count; i++)
				{
					TroopRosterElement elementCopyAtIndex = army.LeaderParty.MemberRoster.GetElementCopyAtIndex(i);
					int num;
					dictionary.TryGetValue(elementCopyAtIndex.Character.DefaultFormationClass, out num);
					dictionary[elementCopyAtIndex.Character.DefaultFormationClass] = num + elementCopyAtIndex.WoundedNumber;
					int num2;
					dictionary2.TryGetValue(elementCopyAtIndex.Character.DefaultFormationClass, out num2);
					dictionary2[elementCopyAtIndex.Character.DefaultFormationClass] = num2 + elementCopyAtIndex.Number - elementCopyAtIndex.WoundedNumber;
				}
				int num3 = army.LeaderParty.MemberRoster.TotalManCount;
				foreach (MobileParty mobileParty in army.LeaderParty.AttachedParties)
				{
					for (int j = 0; j < mobileParty.MemberRoster.Count; j++)
					{
						TroopRosterElement elementCopyAtIndex2 = mobileParty.MemberRoster.GetElementCopyAtIndex(j);
						int num4;
						dictionary.TryGetValue(elementCopyAtIndex2.Character.DefaultFormationClass, out num4);
						dictionary[elementCopyAtIndex2.Character.DefaultFormationClass] = num4 + elementCopyAtIndex2.WoundedNumber;
						int num5;
						dictionary2.TryGetValue(elementCopyAtIndex2.Character.DefaultFormationClass, out num5);
						dictionary2[elementCopyAtIndex2.Character.DefaultFormationClass] = num5 + elementCopyAtIndex2.Number - elementCopyAtIndex2.WoundedNumber;
					}
					num3 += mobileParty.MemberRoster.TotalManCount;
				}
				foreach (FormationClass formationClass in FormationClassExtensions.FormationClassValues)
				{
					int num6;
					dictionary.TryGetValue(formationClass, out num6);
					int num7;
					dictionary2.TryGetValue(formationClass, out num7);
					if (num6 + num7 > 0)
					{
						TextObject textObject = new TextObject("{=Dqydb21E} {PARTY_SIZE}", null);
						textObject.SetTextVariable("PARTY_SIZE", PartyBaseHelper.GetPartySizeText(num7, num6, true));
						TextObject textObject2 = GameTexts.FindText("str_troop_type_name", formationClass.GetName());
						list.Add(new TooltipProperty(textObject2.ToString(), textObject.ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.None));
					}
				}
				list.Add(new TooltipProperty("", "", 0, false, TooltipProperty.TooltipPropertyFlags.RundownSeperator));
				list.Add(new TooltipProperty(CampaignUIHelper._totalStr.ToString(), num3.ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.RundownResult));
			}
			return list;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004B70 File Offset: 0x00002D70
		public static string GetDaysUntilNoFood(float totalFood, float foodChange)
		{
			if (totalFood <= 1E-45f)
			{
				totalFood = 0f;
			}
			if (foodChange >= -1E-45f)
			{
				return GameTexts.FindText("str_days_until_no_food_never", null).ToString();
			}
			return MathF.Ceiling(MathF.Abs(totalFood / foodChange)).ToString();
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004BBC File Offset: 0x00002DBC
		public static List<TooltipProperty> GetSettlementPropertyTooltip(Settlement settlement, string valueName, float value, ExplainedNumber explainedNumber)
		{
			TextObject textObject;
			if (CampaignUIHelper.IsSettlementInformationHidden(settlement, out textObject))
			{
				List<TooltipProperty> list = new List<TooltipProperty>();
				string currentValue = GameTexts.FindText("str_missing_info_indicator", null).ToString();
				CampaignUIHelper.TooltipAddPropertyTitleWithValue(list, valueName, currentValue);
				CampaignUIHelper.TooltipAddEmptyLine(list, false);
				list.Add(new TooltipProperty(string.Empty, textObject.ToString(), -1, false, TooltipProperty.TooltipPropertyFlags.None));
				return list;
			}
			return CampaignUIHelper.GetTooltipForAccumulatingProperty(valueName, value, explainedNumber);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004C1C File Offset: 0x00002E1C
		public static List<TooltipProperty> GetSettlementPropertyTooltipWithResult(Settlement settlement, string valueName, float value, ref ExplainedNumber explainedNumber)
		{
			TextObject textObject;
			if (CampaignUIHelper.IsSettlementInformationHidden(settlement, out textObject))
			{
				List<TooltipProperty> list = new List<TooltipProperty>();
				string currentValue = GameTexts.FindText("str_missing_info_indicator", null).ToString();
				CampaignUIHelper.TooltipAddPropertyTitleWithValue(list, valueName, currentValue);
				CampaignUIHelper.TooltipAddEmptyLine(list, false);
				list.Add(new TooltipProperty(string.Empty, textObject.ToString(), -1, false, TooltipProperty.TooltipPropertyFlags.None));
				return list;
			}
			return CampaignUIHelper.GetTooltipForAccumulatingPropertyWithResult(valueName, value, ref explainedNumber);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004C7C File Offset: 0x00002E7C
		public static List<TooltipProperty> GetArmyFoodTooltip(Army army)
		{
			List<TooltipProperty> list = new List<TooltipProperty>();
			list.Add(new TooltipProperty(new TextObject("{=Q8dhryRX}Parties' Food", null).ToString(), "", 0, false, TooltipProperty.TooltipPropertyFlags.Title));
			float num = army.LeaderParty.Food;
			foreach (MobileParty mobileParty in army.LeaderParty.AttachedParties)
			{
				num += mobileParty.Food;
			}
			list.Add(new TooltipProperty(GameTexts.FindText("str_total_army_food", null).ToString(), CampaignUIHelper.FloatToString(num), 0, false, TooltipProperty.TooltipPropertyFlags.None));
			list.Add(new TooltipProperty("", string.Empty, 0, false, TooltipProperty.TooltipPropertyFlags.DefaultSeperator));
			double num2 = 0.0;
			foreach (MobileParty mobileParty2 in army.Parties)
			{
				float val = mobileParty2.Party.MobileParty.Food / -mobileParty2.Party.MobileParty.FoodChange;
				num2 += (double)Math.Max(val, 0f);
				string daysUntilNoFood = CampaignUIHelper.GetDaysUntilNoFood(mobileParty2.Party.MobileParty.Food, mobileParty2.Party.MobileParty.FoodChange);
				list.Add(new TooltipProperty(mobileParty2.Party.MobileParty.Name.ToString(), daysUntilNoFood, 0, false, TooltipProperty.TooltipPropertyFlags.None));
			}
			list.Add(new TooltipProperty("", string.Empty, 0, false, TooltipProperty.TooltipPropertyFlags.RundownSeperator));
			list.Add(new TooltipProperty(new TextObject("{=rwKBR4NE}Average Days Until Food Runs Out", null).ToString(), MathF.Ceiling(num2 / (double)army.LeaderPartyAndAttachedPartiesCount).ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.None));
			return list;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00004E74 File Offset: 0x00003074
		public static string GetClanWealthStatusText(Clan clan)
		{
			string result = string.Empty;
			if (clan.Leader.Gold < 15000)
			{
				result = new TextObject("{=SixPXaNh}Very Poor", null).ToString();
			}
			else if (clan.Leader.Gold < 45000)
			{
				result = new TextObject("{=poorWealthStatus}Poor", null).ToString();
			}
			else if (clan.Leader.Gold < 135000)
			{
				result = new TextObject("{=averageWealthStatus}Average", null).ToString();
			}
			else if (clan.Leader.Gold < 405000)
			{
				result = new TextObject("{=UbRqC0Yz}Rich", null).ToString();
			}
			else
			{
				result = new TextObject("{=oJmRg2ms}Very Rich", null).ToString();
			}
			return result;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00004F30 File Offset: 0x00003130
		public static List<TooltipProperty> GetClanProsperityTooltip(Clan clan)
		{
			List<TooltipProperty> list = new List<TooltipProperty>
			{
				new TooltipProperty("", GameTexts.FindText("str_prosperity", null).ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.Title)
			};
			int num = 0;
			EncyclopediaPage pageOf = Campaign.Current.EncyclopediaManager.GetPageOf(typeof(Hero));
			for (int i = 0; i < clan.Heroes.Count; i++)
			{
				Hero hero = clan.Heroes[i];
				if (hero.Gold != 0 && hero.IsAlive && hero.Age >= 18f && pageOf.IsValidEncyclopediaItem(hero))
				{
					int gold = hero.Gold;
					list.Add(new TooltipProperty(hero.Name.ToString(), gold.ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.None));
					num += gold;
				}
			}
			for (int j = 0; j < clan.Companions.Count; j++)
			{
				Hero hero2 = clan.Companions[j];
				if (hero2.Gold != 0 && hero2.IsAlive && hero2.Age >= 18f && pageOf.IsValidEncyclopediaItem(hero2))
				{
					int gold = hero2.Gold;
					list.Add(new TooltipProperty(hero2.Name.ToString(), hero2.Gold.ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.None));
					num += gold;
				}
			}
			CampaignUIHelper.TooltipAddDoubleSeperator(list, false);
			list.Add(new TooltipProperty(GameTexts.FindText("str_total_gold", null).ToString(), num.ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.RundownResult));
			return list;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000050C8 File Offset: 0x000032C8
		private static List<TooltipProperty> GetDiplomacySettlementStatComparisonTooltip(List<Settlement> settlements, string title, string emptyExplanation = "")
		{
			List<TooltipProperty> list = new List<TooltipProperty>();
			list.Add(new TooltipProperty("", title, 0, false, TooltipProperty.TooltipPropertyFlags.Title));
			if (settlements.Count == 0)
			{
				list.Add(new TooltipProperty(emptyExplanation, "", 0, false, TooltipProperty.TooltipPropertyFlags.None));
				list.Add(new TooltipProperty("", "", -1, false, TooltipProperty.TooltipPropertyFlags.None));
				return list;
			}
			for (int i = 0; i < settlements.Count; i++)
			{
				Settlement settlement = settlements[i];
				list.Add(new TooltipProperty(settlement.Name.ToString(), "", 0, false, TooltipProperty.TooltipPropertyFlags.None));
			}
			list.Add(new TooltipProperty("", "", -1, false, TooltipProperty.TooltipPropertyFlags.None));
			return list;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x0000517C File Offset: 0x0000337C
		public static List<TooltipProperty> GetTruceOwnedSettlementsTooltip(List<Settlement> settlements, TextObject factionName, bool isTown)
		{
			TextObject textObject = isTown ? new TextObject("{=o79dIa3L}Towns owned by {FACTION}", null) : new TextObject("{=z3Xg0IaG}Castles owned by {FACTION}", null);
			TextObject textObject2 = isTown ? new TextObject("{=cedvCZ73}There is no town owned by {FACTION}", null) : new TextObject("{=ZZmlYrgL}There is no castle owned by {FACTION}", null);
			textObject.SetTextVariable("FACTION", factionName);
			textObject2.SetTextVariable("FACTION", factionName);
			return CampaignUIHelper.GetDiplomacySettlementStatComparisonTooltip(settlements, textObject.ToString(), textObject2.ToString());
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000051F0 File Offset: 0x000033F0
		public static List<TooltipProperty> GetWarSuccessfulRaidsTooltip(List<Settlement> settlements, TextObject factionName)
		{
			TextObject textObject = new TextObject("{=1qm74K2t}Successful raids by {FACTION}", null);
			TextObject textObject2 = new TextObject("{=huqEEfGD}There is no successful raid for {FACTION}", null);
			textObject.SetTextVariable("FACTION", factionName);
			textObject2.SetTextVariable("FACTION", factionName);
			return CampaignUIHelper.GetDiplomacySettlementStatComparisonTooltip(settlements, textObject.ToString(), textObject2.ToString());
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00005244 File Offset: 0x00003444
		public static List<TooltipProperty> GetWarSuccessfulSiegesTooltip(List<Settlement> settlements, TextObject factionName, bool isTown)
		{
			TextObject textObject = isTown ? new TextObject("{=mSPyh91Q}Towns conquered by {FACTION}", null) : new TextObject("{=eTxcYvRr}Castles conquered by {FACTION}", null);
			TextObject textObject2 = isTown ? new TextObject("{=Zemk86FK}There is no town conquered by {FACTION}", null) : new TextObject("{=nKQmaSDO}There is no castle conquered by {FACTION}", null);
			textObject.SetTextVariable("FACTION", factionName);
			textObject2.SetTextVariable("FACTION", factionName);
			return CampaignUIHelper.GetDiplomacySettlementStatComparisonTooltip(settlements, textObject.ToString(), textObject2.ToString());
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000052B8 File Offset: 0x000034B8
		public static List<TooltipProperty> GetWarPrisonersTooltip(List<Hero> capturedPrisoners, TextObject factionName)
		{
			List<TooltipProperty> list = new List<TooltipProperty>();
			TextObject textObject = new TextObject("{=8BJDQe6o}Prisoners captured by {FACTION}", null);
			textObject.SetTextVariable("FACTION", factionName);
			list.Add(new TooltipProperty("", textObject.ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.Title));
			if (capturedPrisoners.Count == 0)
			{
				TextObject textObject2 = new TextObject("{=CK68QXen}There is no prisoner captured by {FACTION}", null);
				textObject2.SetTextVariable("FACTION", factionName);
				list.Add(new TooltipProperty(textObject2.ToString(), "", 0, false, TooltipProperty.TooltipPropertyFlags.None));
				list.Add(new TooltipProperty("", "", -1, false, TooltipProperty.TooltipPropertyFlags.None));
				return list;
			}
			string text = new TextObject("{=MT4b8H9h}Unknown", null).ToString();
			TextObject textObject3 = new TextObject("{=btoiLePb}{HERO} ({PLACE})", null);
			for (int i = 0; i < capturedPrisoners.Count; i++)
			{
				Hero hero = capturedPrisoners[i];
				PartyBase partyBelongedToAsPrisoner = hero.PartyBelongedToAsPrisoner;
				string variable = ((partyBelongedToAsPrisoner != null) ? partyBelongedToAsPrisoner.Name.ToString() : null) ?? text;
				textObject3.SetTextVariable("HERO", hero.Name.ToString());
				textObject3.SetTextVariable("PLACE", variable);
				list.Add(new TooltipProperty(textObject3.ToString(), "", 0, false, TooltipProperty.TooltipPropertyFlags.None));
			}
			list.Add(new TooltipProperty("", "", -1, false, TooltipProperty.TooltipPropertyFlags.None));
			return list;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x0000540C File Offset: 0x0000360C
		public static List<TooltipProperty> GetClanStrengthTooltip(Clan clan)
		{
			List<TooltipProperty> list = new List<TooltipProperty>
			{
				new TooltipProperty("", GameTexts.FindText("str_strength", null).ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.Title)
			};
			float num = 0f;
			for (int i = 0; i < MobileParty.AllLordParties.Count; i++)
			{
				MobileParty mobileParty = MobileParty.AllLordParties[i];
				if (mobileParty.ActualClan == clan && !mobileParty.IsDisbanding)
				{
					float totalStrength = mobileParty.Party.TotalStrength;
					list.Add(new TooltipProperty(mobileParty.Name.ToString(), totalStrength.ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.None));
					num += totalStrength;
				}
			}
			CampaignUIHelper.TooltipAddDoubleSeperator(list, false);
			list.Add(new TooltipProperty(GameTexts.FindText("str_total_strength", null).ToString(), num.ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.RundownResult));
			return list;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000054EC File Offset: 0x000036EC
		public static List<TooltipProperty> GetCrimeTooltip(Settlement settlement)
		{
			if (settlement.MapFaction == null)
			{
				ExplainedNumber explainedNumber = new ExplainedNumber(0f, true, null);
				return CampaignUIHelper.GetTooltipForAccumulatingProperty(CampaignUIHelper._criminalRatingStr.ToString(), 0f, explainedNumber);
			}
			return CampaignUIHelper.GetTooltipForAccumulatingProperty(CampaignUIHelper._criminalRatingStr.ToString(), settlement.MapFaction.MainHeroCrimeRating, settlement.MapFaction.DailyCrimeRatingChangeExplained);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x0000554C File Offset: 0x0000374C
		public static List<TooltipProperty> GetInfluenceTooltip(Clan clan)
		{
			List<TooltipProperty> tooltipForAccumulatingProperty = CampaignUIHelper.GetTooltipForAccumulatingProperty(CampaignUIHelper._influenceStr.ToString(), clan.Influence, clan.InfluenceChangeExplained);
			if (tooltipForAccumulatingProperty != null && clan.IsUnderMercenaryService)
			{
				tooltipForAccumulatingProperty.Add(new TooltipProperty("", CampaignUIHelper._mercenaryClanInfluenceStr.ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.None));
			}
			return tooltipForAccumulatingProperty;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000055A0 File Offset: 0x000037A0
		public static List<TooltipProperty> GetClanRenownTooltip(Clan clan)
		{
			List<TooltipProperty> list = new List<TooltipProperty>();
			TextObject variable;
			ValueTuple<ExplainedNumber, bool> valueTuple = Campaign.Current.Models.ClanTierModel.HasUpcomingTier(clan, out variable, true);
			ExplainedNumber item = valueTuple.Item1;
			bool item2 = valueTuple.Item2;
			list.Add(new TooltipProperty(GameTexts.FindText("str_enc_sf_renown", null).ToString(), ((int)clan.Renown).ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.None));
			if (item2)
			{
				CampaignUIHelper.TooltipAddEmptyLine(list, false);
				list.Add(new TooltipProperty(GameTexts.FindText("str_clan_next_tier", null).ToString(), clan.RenownRequirementForNextTier.ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.None));
				CampaignUIHelper.TooltipAddEmptyLine(list, false);
				GameTexts.SetVariable("LEFT", GameTexts.FindText("str_clan_tier_bonus", null).ToString());
				TextObject textObject = GameTexts.FindText("str_string_newline_string", null);
				textObject.SetTextVariable("STR1", item.GetExplanations());
				textObject.SetTextVariable("STR2", variable);
				list.Add(new TooltipProperty(GameTexts.FindText("str_LEFT_colon_wSpace", null).ToString(), textObject.ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.None));
			}
			return list;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000056B4 File Offset: 0x000038B4
		public static TooltipTriggerVM GetDenarTooltip()
		{
			ClanFinanceModel clanFinanceModel = Campaign.Current.Models.ClanFinanceModel;
			Func<ExplainedNumber> func = () => clanFinanceModel.CalculateClanGoldChange(Clan.PlayerClan, true, false, false);
			Func<ExplainedNumber> func2 = () => clanFinanceModel.CalculateClanGoldChange(Clan.PlayerClan, true, false, true);
			RundownTooltipVM.ValueCategorization valueCategorization = RundownTooltipVM.ValueCategorization.LargeIsBetter;
			TextObject changeStr = CampaignUIHelper._changeStr;
			TextObject totalStr = CampaignUIHelper._totalStr;
			return new TooltipTriggerVM(typeof(ExplainedNumber), new object[]
			{
				func,
				func2,
				changeStr,
				totalStr,
				valueCategorization
			});
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00005732 File Offset: 0x00003932
		public static List<TooltipProperty> GetPartyMoraleTooltip(MobileParty mainParty)
		{
			return CampaignUIHelper.GetTooltipForgProperty(CampaignUIHelper._partyMoraleStr.ToString(), mainParty.Morale, mainParty.MoraleExplained);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00005750 File Offset: 0x00003950
		public static List<TooltipProperty> GetPartyHealthTooltip(PartyBase party)
		{
			List<TooltipProperty> list = new List<TooltipProperty>();
			CampaignUIHelper.TooltipAddPropertyTitleWithValue(list, CampaignUIHelper._battleReadyTroopsStr.ToString(), (float)party.NumberOfHealthyMembers);
			int num = party.NumberOfAllMembers - party.NumberOfHealthyMembers;
			CampaignUIHelper.TooltipAddPropertyTitleWithValue(list, CampaignUIHelper._woundedTroopsStr.ToString(), (float)num);
			if (num > 0)
			{
				ExplainedNumber healingRateForRegularsExplained = MobileParty.MainParty.HealingRateForRegularsExplained;
				CampaignUIHelper.TooltipAddDoubleSeperator(list, false);
				CampaignUIHelper.TooltipAddPropertyTitleWithValue(list, CampaignUIHelper._regularsHealingRateStr.ToString(), healingRateForRegularsExplained.ResultNumber);
				CampaignUIHelper.TooltipAddSeperator(list, false);
				CampaignUIHelper.TooltipAddExplanation(list, ref healingRateForRegularsExplained);
			}
			return list;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000057D8 File Offset: 0x000039D8
		public static List<TooltipProperty> GetPlayerHitpointsTooltip()
		{
			List<TooltipProperty> list = new List<TooltipProperty>();
			ExplainedNumber maxHitPointsExplanation = Hero.MainHero.CharacterObject.MaxHitPointsExplanation;
			CampaignUIHelper.TooltipAddPropertyTitleWithValue(list, CampaignUIHelper._hitPointsStr.ToString(), (float)Hero.MainHero.HitPoints);
			CampaignUIHelper.TooltipAddSeperator(list, false);
			CampaignUIHelper.TooltipAddPropertyTitleWithValue(list, CampaignUIHelper._maxhitPointsStr.ToString(), maxHitPointsExplanation.ResultNumber);
			CampaignUIHelper.TooltipAddDoubleSeperator(list, false);
			CampaignUIHelper.TooltipAddExplanation(list, ref maxHitPointsExplanation);
			if (Hero.MainHero.HitPoints < Hero.MainHero.MaxHitPoints)
			{
				ExplainedNumber healingRateForHeroesExplained = MobileParty.MainParty.HealingRateForHeroesExplained;
				CampaignUIHelper.TooltipAddDoubleSeperator(list, false);
				CampaignUIHelper.TooltipAddPropertyTitleWithValue(list, CampaignUIHelper._heroesHealingRateStr.ToString(), healingRateForHeroesExplained.ResultNumber);
				CampaignUIHelper.TooltipAddSeperator(list, false);
				CampaignUIHelper.TooltipAddExplanation(list, ref healingRateForHeroesExplained);
			}
			return list;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00005894 File Offset: 0x00003A94
		public static List<TooltipProperty> GetPartyFoodTooltip(MobileParty mainParty)
		{
			List<TooltipProperty> list = new List<TooltipProperty>();
			float num = (mainParty.Food > 0f) ? mainParty.Food : 0f;
			CampaignUIHelper.TooltipAddPropertyTitleWithValue(list, CampaignUIHelper._foodStr.ToString(), num);
			ExplainedNumber foodChangeExplained = mainParty.FoodChangeExplained;
			CampaignUIHelper.TooltipAddExplanedChange(list, ref foodChangeExplained);
			CampaignUIHelper.TooltipAddEmptyLine(list, false);
			List<TooltipProperty> list2 = new List<TooltipProperty>();
			int num2 = 0;
			List<TooltipProperty> list3 = new List<TooltipProperty>();
			int num3 = 0;
			for (int i = 0; i < mainParty.ItemRoster.Count; i++)
			{
				ItemRosterElement itemRosterElement = mainParty.ItemRoster[i];
				if (!itemRosterElement.IsEmpty)
				{
					ItemObject item = itemRosterElement.EquipmentElement.Item;
					if (item != null && item.IsFood)
					{
						list2.Add(new TooltipProperty(itemRosterElement.EquipmentElement.GetModifiedItemName().ToString(), itemRosterElement.Amount.ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.None));
						num2 += itemRosterElement.Amount;
					}
					else
					{
						ItemObject item2 = itemRosterElement.EquipmentElement.Item;
						bool flag;
						if (item2 == null)
						{
							flag = false;
						}
						else
						{
							HorseComponent horseComponent = item2.HorseComponent;
							bool? flag2 = (horseComponent != null) ? new bool?(horseComponent.IsLiveStock) : null;
							bool flag3 = true;
							flag = (flag2.GetValueOrDefault() == flag3 & flag2 != null);
						}
						if (flag)
						{
							GameTexts.SetVariable("RANK", itemRosterElement.EquipmentElement.Item.HorseComponent.MeatCount);
							GameTexts.SetVariable("NUMBER", GameTexts.FindText("str_meat", null));
							GameTexts.SetVariable("NUM2", GameTexts.FindText("str_RANK_with_NUM_between_parenthesis", null).ToString());
							GameTexts.SetVariable("NUM1", itemRosterElement.Amount);
							list3.Add(new TooltipProperty(itemRosterElement.EquipmentElement.GetModifiedItemName().ToString(), GameTexts.FindText("str_NUM_times_NUM_with_space", null).ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.None));
							num3 += itemRosterElement.Amount * itemRosterElement.EquipmentElement.Item.HorseComponent.MeatCount;
						}
					}
				}
			}
			if (num2 > 0)
			{
				list.Add(new TooltipProperty(CampaignUIHelper._foodItemsStr.ToString(), num2.ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.None));
				CampaignUIHelper.TooltipAddDoubleSeperator(list, false);
				list.AddRange(list2);
				CampaignUIHelper.TooltipAddEmptyLine(list, false);
			}
			if (num3 > 0)
			{
				list.Add(new TooltipProperty(CampaignUIHelper._livestockStr.ToString(), num3.ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.None));
				CampaignUIHelper.TooltipAddDoubleSeperator(list, false);
				list.AddRange(list3);
				CampaignUIHelper.TooltipAddEmptyLine(list, false);
			}
			list.Add(new TooltipProperty(GameTexts.FindText("str_total_days_until_no_food", null).ToString(), CampaignUIHelper.GetDaysUntilNoFood(num, foodChangeExplained.ResultNumber), 0, false, TooltipProperty.TooltipPropertyFlags.None));
			return list;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00005B4C File Offset: 0x00003D4C
		public static List<TooltipProperty> GetPartySpeedTooltip()
		{
			Game.Current.EventManager.TriggerEvent<PlayerInspectedPartySpeedEvent>(new PlayerInspectedPartySpeedEvent());
			List<TooltipProperty> list = new List<TooltipProperty>();
			if (Hero.MainHero.IsPrisoner)
			{
				list.Add(new TooltipProperty(string.Empty, GameTexts.FindText("str_main_hero_is_imprisoned", null).ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.None));
			}
			else
			{
				ExplainedNumber speedExplained = MobileParty.MainParty.SpeedExplained;
				float resultNumber = speedExplained.ResultNumber;
				list = CampaignUIHelper.GetTooltipForAccumulatingPropertyWithResult(CampaignUIHelper._partySpeedStr.ToString(), resultNumber, ref speedExplained);
			}
			return list;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005BCC File Offset: 0x00003DCC
		public static List<TooltipProperty> GetPartyWageTooltip()
		{
			ExplainedNumber totalWageExplained = MobileParty.MainParty.TotalWageExplained;
			return CampaignUIHelper.GetTooltipForAccumulatingPropertyWithResult(GameTexts.FindText("str_party_wage", null).ToString(), totalWageExplained.ResultNumber, ref totalWageExplained);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00005C04 File Offset: 0x00003E04
		public static List<TooltipProperty> GetPartyWageTooltip(MobileParty mobileParty)
		{
			ExplainedNumber totalWageExplained = mobileParty.TotalWageExplained;
			return CampaignUIHelper.GetTooltipForAccumulatingPropertyWithResult(GameTexts.FindText("str_party_wage", null).ToString(), totalWageExplained.ResultNumber, ref totalWageExplained);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00005C38 File Offset: 0x00003E38
		public static List<TooltipProperty> GetViewDistanceTooltip()
		{
			ExplainedNumber seeingRangeExplanation = MobileParty.MainParty.SeeingRangeExplanation;
			return CampaignUIHelper.GetTooltipForAccumulatingPropertyWithResult(CampaignUIHelper._viewDistanceFoodStr.ToString(), seeingRangeExplanation.ResultNumber, ref seeingRangeExplanation);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00005C68 File Offset: 0x00003E68
		public static List<TooltipProperty> GetMainPartyHealthTooltip()
		{
			PartyBase party = MobileParty.MainParty.Party;
			List<TooltipProperty> list = new List<TooltipProperty>();
			list.Add(new TooltipProperty(CampaignUIHelper._battleReadyTroopsStr.ToString(), party.NumberOfHealthyMembers.ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.None));
			CampaignUIHelper.TooltipAddEmptyLine(list, false);
			int num = party.NumberOfAllMembers - party.NumberOfHealthyMembers;
			list.Add(new TooltipProperty(CampaignUIHelper._woundedTroopsStr.ToString(), num.ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.None));
			if (num > 0)
			{
				CampaignUIHelper.TooltipAddDoubleSeperator(list, false);
				list.Add(new TooltipProperty(CampaignUIHelper._regularsHealingRateStr.ToString(), MobileParty.MainParty.HealingRateForRegulars.ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.None));
				CampaignUIHelper.TooltipAddSeperator(list, false);
				ExplainedNumber healingRateForRegularsExplained = MobileParty.MainParty.HealingRateForRegularsExplained;
				CampaignUIHelper.TooltipAddExplanation(list, ref healingRateForRegularsExplained);
				CampaignUIHelper.TooltipAddEmptyLine(list, false);
			}
			int totalManCount = party.PrisonRoster.TotalManCount;
			if (totalManCount > 0)
			{
				list.Add(new TooltipProperty(CampaignUIHelper._prisonersStr.ToString(), totalManCount.ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.None));
			}
			return list;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00005D6C File Offset: 0x00003F6C
		public static List<TooltipProperty> GetPartyInventoryCapacityTooltip(MobileParty party)
		{
			List<TooltipProperty> list = new List<TooltipProperty>();
			CampaignUIHelper.TooltipAddPropertyTitleWithValue(list, CampaignUIHelper._partyInventoryCapacityStr.ToString(), (float)party.InventoryCapacity);
			CampaignUIHelper.TooltipAddSeperator(list, false);
			ExplainedNumber inventoryCapacityExplainedNumber = party.InventoryCapacityExplainedNumber;
			CampaignUIHelper.TooltipAddExplanation(list, ref inventoryCapacityExplainedNumber);
			return list;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00005DAC File Offset: 0x00003FAC
		public static List<TooltipProperty> GetPerkEffectText(PerkObject perk, bool isActive)
		{
			List<TooltipProperty> list = new List<TooltipProperty>
			{
				new TooltipProperty("", perk.Name.ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.Title)
			};
			TextObject perkRoleText = CampaignUIHelper.GetPerkRoleText(perk, false);
			if (perkRoleText != null)
			{
				list.Add(new TooltipProperty("", perkRoleText.ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.None));
				list.Add(new TooltipProperty("", perk.PrimaryDescription.ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.None));
				list.Add(new TooltipProperty("", "", 0, false, TooltipProperty.TooltipPropertyFlags.None));
			}
			TextObject perkRoleText2 = CampaignUIHelper.GetPerkRoleText(perk, true);
			if (perkRoleText2 != null)
			{
				list.Add(new TooltipProperty("", perkRoleText2.ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.None));
				list.Add(new TooltipProperty("", perk.SecondaryDescription.ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.None));
				list.Add(new TooltipProperty("", "", 0, false, TooltipProperty.TooltipPropertyFlags.None));
			}
			if (isActive)
			{
				list.Add(new TooltipProperty("", GameTexts.FindText("str_perk_active", null).ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.None));
			}
			list.Add(new TooltipProperty(GameTexts.FindText("str_required_level_perk", null).ToString(), ((int)perk.RequiredSkillValue).ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.None));
			return list;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00005EEC File Offset: 0x000040EC
		public static TextObject GetPerkRoleText(PerkObject perk, bool getSecondary)
		{
			TextObject textObject = null;
			if (!getSecondary && perk.PrimaryRole != SkillEffect.PerkRole.None)
			{
				textObject = GameTexts.FindText("str_perk_one_role", null);
				textObject.SetTextVariable("PRIMARY_ROLE", GameTexts.FindText("role", perk.PrimaryRole.ToString()));
			}
			else if (getSecondary && perk.SecondaryRole != SkillEffect.PerkRole.None)
			{
				textObject = GameTexts.FindText("str_perk_one_role", null);
				textObject.SetTextVariable("PRIMARY_ROLE", GameTexts.FindText("role", perk.SecondaryRole.ToString()));
			}
			return textObject;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00005F80 File Offset: 0x00004180
		public static TextObject GetCombinedPerkRoleText(PerkObject perk)
		{
			TextObject textObject = null;
			if (perk.PrimaryRole != SkillEffect.PerkRole.None && perk.SecondaryRole != SkillEffect.PerkRole.None)
			{
				textObject = GameTexts.FindText("str_perk_two_roles", null);
				textObject.SetTextVariable("PRIMARY_ROLE", GameTexts.FindText("role", perk.PrimaryRole.ToString()));
				textObject.SetTextVariable("SECONDARY_ROLE", GameTexts.FindText("role", perk.SecondaryRole.ToString()));
			}
			else if (perk.PrimaryRole != SkillEffect.PerkRole.None)
			{
				textObject = GameTexts.FindText("str_perk_one_role", null);
				textObject.SetTextVariable("PRIMARY_ROLE", GameTexts.FindText("role", perk.PrimaryRole.ToString()));
			}
			return textObject;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00006040 File Offset: 0x00004240
		public static List<TooltipProperty> GetSiegeMachineTooltip(SiegeEngineType engineType, bool showDescription = true, int hoursUntilCompletion = 0)
		{
			List<TooltipProperty> list = new List<TooltipProperty>();
			if (showDescription)
			{
				GameTexts.SetVariable("NEWLINE", "\n");
			}
			string value = showDescription ? GameTexts.FindText("str_siege_weapon_tooltip_text", engineType.StringId).ToString() : engineType.Name.ToString();
			list.Add(new TooltipProperty(" ", value, 0, false, TooltipProperty.TooltipPropertyFlags.None));
			if (hoursUntilCompletion > 0)
			{
				TooltipProperty siegeMachineProgressLine = CampaignUIHelper.GetSiegeMachineProgressLine(hoursUntilCompletion);
				if (siegeMachineProgressLine != null)
				{
					list.Add(siegeMachineProgressLine);
				}
			}
			return list;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000060B5 File Offset: 0x000042B5
		public static string GetSiegeMachineName(SiegeEngineType engineType)
		{
			if (engineType != null)
			{
				return engineType.Name.ToString();
			}
			return "";
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000060CB File Offset: 0x000042CB
		public static string GetSiegeMachineNameWithDesctiption(SiegeEngineType engineType)
		{
			if (engineType != null)
			{
				return GameTexts.FindText("str_siege_weapon_tooltip_text", engineType.StringId).ToString();
			}
			return "";
		}

		// Token: 0x06000109 RID: 265 RVA: 0x000060EC File Offset: 0x000042EC
		public static List<TooltipProperty> GetTroopConformityTooltip(TroopRosterElement troop)
		{
			List<TooltipProperty> list = new List<TooltipProperty>();
			if (troop.Character != null)
			{
				int elementXp = PartyBase.MainParty.PrisonRoster.GetElementXp(troop.Character);
				int conformityNeededToRecruitPrisoner = troop.Character.ConformityNeededToRecruitPrisoner;
				int num = (elementXp >= conformityNeededToRecruitPrisoner * troop.Number) ? conformityNeededToRecruitPrisoner : (elementXp % conformityNeededToRecruitPrisoner);
				list.Add(new TooltipProperty(GameTexts.FindText("str_party_troop_current_conformity", null).ToString(), num.ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.None));
				list.Add(new TooltipProperty(GameTexts.FindText("str_party_troop_recruit_conformity_cost", null).ToString(), conformityNeededToRecruitPrisoner.ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.None));
				list.Add(new TooltipProperty(GameTexts.FindText("str_party_recruitable_troops", null).ToString(), MathF.Min(elementXp / conformityNeededToRecruitPrisoner, troop.Number).ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.None));
				if (elementXp < conformityNeededToRecruitPrisoner * troop.Number)
				{
					GameTexts.SetVariable("CONFORMITY_AMOUNT", (conformityNeededToRecruitPrisoner - num).ToString());
					list.Add(new TooltipProperty("", GameTexts.FindText("str_party_troop_conformity_explanation", null).ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.MultiLine));
				}
			}
			return list;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00006208 File Offset: 0x00004408
		public static List<TooltipProperty> GetLearningRateTooltip(int attributeValue, int focusValue, int skillValue, int characterLevel, TextObject attributeName)
		{
			ExplainedNumber explainedNumber = Campaign.Current.Models.CharacterDevelopmentModel.CalculateLearningRate(attributeValue, focusValue, skillValue, characterLevel, attributeName, true);
			return CampaignUIHelper.GetTooltipForAccumulatingPropertyWithResult(CampaignUIHelper._learningRateStr.ToString(), explainedNumber.ResultNumber, ref explainedNumber);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x0000624C File Offset: 0x0000444C
		public static List<TooltipProperty> GetTroopXPTooltip(TroopRosterElement troop)
		{
			List<TooltipProperty> list = new List<TooltipProperty>();
			if (troop.Character != null && troop.Character.UpgradeTargets.Length != 0)
			{
				int xp = troop.Xp;
				int upgradeXpCost = troop.Character.GetUpgradeXpCost(PartyBase.MainParty, 0);
				int num = (xp >= upgradeXpCost * troop.Number) ? upgradeXpCost : (xp % upgradeXpCost);
				list.Add(new TooltipProperty(GameTexts.FindText("str_party_troop_current_xp", null).ToString(), num.ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.None));
				list.Add(new TooltipProperty(GameTexts.FindText("str_party_troop_upgrade_xp_cost", null).ToString(), upgradeXpCost.ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.None));
				list.Add(new TooltipProperty(GameTexts.FindText("str_party_upgradable_troops", null).ToString(), MathF.Min(xp / upgradeXpCost, troop.Number).ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.None));
				if (xp < upgradeXpCost * troop.Number)
				{
					int content = upgradeXpCost - num;
					GameTexts.SetVariable("XP_AMOUNT", content);
					list.Add(new TooltipProperty("", GameTexts.FindText("str_party_troop_xp_explanation", null).ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.MultiLine));
				}
			}
			return list;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x0000636C File Offset: 0x0000456C
		public static List<TooltipProperty> GetLearningLimitTooltip(int attributeValue, int focusValue, TextObject attributeName)
		{
			ExplainedNumber explainedNumber = Campaign.Current.Models.CharacterDevelopmentModel.CalculateLearningLimit(attributeValue, focusValue, attributeName, true);
			return CampaignUIHelper.GetTooltipForAccumulatingPropertyWithResult(CampaignUIHelper._learningLimitStr.ToString(), explainedNumber.ResultNumber, ref explainedNumber);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000063AC File Offset: 0x000045AC
		public static List<TooltipProperty> GetSettlementConsumptionTooltip(Settlement settlement)
		{
			List<TooltipProperty> list = new List<TooltipProperty>();
			list.Add(new TooltipProperty("", GameTexts.FindText("str_consumption", null).ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.Title));
			if (settlement.IsTown)
			{
				using (IEnumerator<Town.SellLog> enumerator = settlement.Town.SoldItems.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Town.SellLog sellLog = enumerator.Current;
						list.Add(new TooltipProperty(sellLog.Category.GetName().ToString(), sellLog.Number.ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.None));
					}
					return list;
				}
			}
			Debug.FailedAssert("Only towns' consumptions are tracked", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem.ViewModelCollection\\CampaignUIHelper.cs", "GetSettlementConsumptionTooltip", 1157);
			return list;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00006474 File Offset: 0x00004674
		public static StringItemWithHintVM GetCharacterTierData(CharacterObject character, bool isBig = false)
		{
			int tier = character.Tier;
			if (tier <= 0 || tier > 7)
			{
				return new StringItemWithHintVM("", TextObject.Empty);
			}
			string str = isBig ? (tier.ToString() + "_big") : tier.ToString();
			string text = "General\\TroopTierIcons\\icon_tier_" + str;
			GameTexts.SetVariable("TIER_LEVEL", tier);
			TextObject hint = new TextObject("{=!}" + GameTexts.FindText("str_party_troop_tier", null).ToString(), null);
			return new StringItemWithHintVM(text, hint);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000064FC File Offset: 0x000046FC
		public static List<TooltipProperty> GetSettlementProductionTooltip(Settlement settlement)
		{
			List<TooltipProperty> list = new List<TooltipProperty>();
			list.Add(new TooltipProperty("", GameTexts.FindText("str_production", null).ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.Title));
			if (settlement.IsFortification)
			{
				list.Add(new TooltipProperty(GameTexts.FindText("str_villages", null).ToString(), " ", 0, false, TooltipProperty.TooltipPropertyFlags.None));
				CampaignUIHelper.TooltipAddDoubleSeperator(list, false);
				for (int i = 0; i < settlement.BoundVillages.Count; i++)
				{
					Village village = settlement.BoundVillages[i];
					list.Add(new TooltipProperty(village.Name.ToString(), village.VillageType.PrimaryProduction.Name.ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.None));
				}
				list.Add(new TooltipProperty(GameTexts.FindText("str_shops_in_town", null).ToString(), " ", 0, false, TooltipProperty.TooltipPropertyFlags.None));
				CampaignUIHelper.TooltipAddDoubleSeperator(list, false);
				using (IEnumerator<Workshop> enumerator = (from w in settlement.Town.Workshops
				where w.WorkshopType != null && !w.WorkshopType.IsHidden
				select w).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Workshop workshop = enumerator.Current;
						list.Add(new TooltipProperty(" ", workshop.WorkshopType.Name.ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.None));
					}
					return list;
				}
			}
			if (settlement.IsVillage)
			{
				list.Add(new TooltipProperty(GameTexts.FindText("str_production_in_village", null).ToString(), " ", 0, false, TooltipProperty.TooltipPropertyFlags.None));
				CampaignUIHelper.TooltipAddDoubleSeperator(list, false);
				for (int j = 0; j < settlement.Village.VillageType.Productions.Count; j++)
				{
					ValueTuple<ItemObject, float> valueTuple = settlement.Village.VillageType.Productions[j];
					list.Add(new TooltipProperty(" ", valueTuple.Item1.Name.ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.None));
				}
			}
			return list;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00006708 File Offset: 0x00004908
		public static string GetHintTextFromReasons(List<TextObject> reasons)
		{
			TextObject textObject = TextObject.Empty;
			for (int i = 0; i < reasons.Count; i++)
			{
				if (i >= 1)
				{
					GameTexts.SetVariable("STR1", textObject.ToString());
					GameTexts.SetVariable("STR2", reasons[i]);
					textObject = GameTexts.FindText("str_string_newline_string", null);
				}
				else
				{
					textObject = reasons[i];
				}
			}
			return textObject.ToString();
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00006770 File Offset: 0x00004970
		public static TextObject GetHoursAndDaysTextFromHourValue(int hours)
		{
			TextObject textObject = TextObject.Empty;
			if (hours > 0)
			{
				int num = hours / 24;
				int num2 = hours % 24;
				textObject = ((num > 0) ? ((num2 > 0) ? GameTexts.FindText("str_days_hours", null) : GameTexts.FindText("str_days", null)) : GameTexts.FindText("str_hours", null));
				textObject.SetTextVariable("DAY", num);
				textObject.SetTextVariable("PLURAL_DAYS", (num > 1) ? 1 : 0);
				textObject.SetTextVariable("HOUR", num2);
				textObject.SetTextVariable("PLURAL_HOURS", (num2 > 1) ? 1 : 0);
			}
			return textObject;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00006804 File Offset: 0x00004A04
		public static TextObject GetTeleportationDelayText(Hero hero, PartyBase target)
		{
			TextObject result = TextObject.Empty;
			if (hero != null && target != null)
			{
				float resultNumber = Campaign.Current.Models.DelayedTeleportationModel.GetTeleportationDelayAsHours(hero, target).ResultNumber;
				if (hero.IsTraveling)
				{
					result = CampaignUIHelper._travelingText.CopyTextObject();
				}
				else if (resultNumber > 0f)
				{
					TextObject textObject = new TextObject("{=P0To9aRW}Travel time: {TRAVEL_TIME}", null);
					textObject.SetTextVariable("TRAVEL_TIME", CampaignUIHelper.GetHoursAndDaysTextFromHourValue((int)Math.Ceiling((double)resultNumber)));
					result = textObject;
				}
				else
				{
					result = CampaignUIHelper._noDelayText.CopyTextObject();
				}
			}
			return result;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0000688C File Offset: 0x00004A8C
		public static List<TooltipProperty> GetTimeOfDayAndResetCameraTooltip()
		{
			List<TooltipProperty> list = new List<TooltipProperty>();
			int getHourOfDay = CampaignTime.Now.GetHourOfDay;
			TextObject textObject = TextObject.Empty;
			if (getHourOfDay >= 6 && getHourOfDay < 12)
			{
				textObject = new TextObject("{=X3gcUz7C}Morning", null);
			}
			else if (getHourOfDay >= 12 && getHourOfDay < 15)
			{
				textObject = new TextObject("{=CTtjSwRb}Noon", null);
			}
			else if (getHourOfDay >= 15 && getHourOfDay < 18)
			{
				textObject = new TextObject("{=J2gvnexb}Afternoon", null);
			}
			else if (getHourOfDay >= 18 && getHourOfDay < 22)
			{
				textObject = new TextObject("{=gENb9SSW}Evening", null);
			}
			else
			{
				textObject = new TextObject("{=fAxjyMt5}Night", null);
			}
			list.Add(new TooltipProperty(textObject.ToString(), "", 0, false, TooltipProperty.TooltipPropertyFlags.None));
			list.Add(new TooltipProperty("", new TextObject("{=sFiU3Ss2}Click to Reset Camera", null).ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.None));
			return list;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000695C File Offset: 0x00004B5C
		public static List<TooltipProperty> GetTournamentChampionRewardsTooltip(Hero hero, Town town)
		{
			List<TooltipProperty> list = new List<TooltipProperty>();
			CampaignUIHelper.TooltipAddPropertyTitle(list, new TextObject("{=CGVK6l8I}Champion Benefits", null).ToString());
			TextObject textObject = new TextObject("{=4vZLpzPi}+1 Renown / Day", null);
			list.Add(new TooltipProperty(textObject.ToString(), "", 0, false, TooltipProperty.TooltipPropertyFlags.None));
			return list;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000069AC File Offset: 0x00004BAC
		public static StringItemWithHintVM GetCharacterTypeData(CharacterObject character, bool isBig = false)
		{
			if (character.IsHero)
			{
				return new StringItemWithHintVM("", TextObject.Empty);
			}
			TextObject textObject = TextObject.Empty;
			string str;
			if (character.IsRanged && character.IsMounted)
			{
				str = (isBig ? "horse_archer_big" : "horse_archer");
				textObject = GameTexts.FindText("str_troop_type_name", "HorseArcher");
			}
			else if (character.IsRanged)
			{
				str = (isBig ? "bow_big" : "bow");
				textObject = GameTexts.FindText("str_troop_type_name", "Ranged");
			}
			else if (character.IsMounted)
			{
				str = (isBig ? "cavalry_big" : "cavalry");
				textObject = GameTexts.FindText("str_troop_type_name", "Cavalry");
			}
			else
			{
				if (!character.IsInfantry)
				{
					return new StringItemWithHintVM("", TextObject.Empty);
				}
				str = (isBig ? "infantry_big" : "infantry");
				textObject = GameTexts.FindText("str_troop_type_name", "Infantry");
			}
			return new StringItemWithHintVM("General\\TroopTypeIcons\\icon_troop_type_" + str, new TextObject("{=!}" + textObject.ToString(), null));
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00006AC0 File Offset: 0x00004CC0
		public static List<TooltipProperty> GetHeroHealthTooltip(Hero hero)
		{
			List<TooltipProperty> list = new List<TooltipProperty>();
			GameTexts.SetVariable("LEFT", hero.HitPoints.ToString("0.##"));
			GameTexts.SetVariable("RIGHT", hero.MaxHitPoints.ToString("0.##"));
			list.Add(new TooltipProperty(CampaignUIHelper._hitPointsStr.ToString(), GameTexts.FindText("str_LEFT_over_RIGHT", null).ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.Title));
			CampaignUIHelper.TooltipAddSeperator(list, false);
			CampaignUIHelper.TooltipAddPropertyTitleWithValue(list, CampaignUIHelper._maxhitPointsStr.ToString(), (float)hero.MaxHitPoints);
			CampaignUIHelper.TooltipAddDoubleSeperator(list, false);
			ExplainedNumber maxHitPointsExplanation = hero.CharacterObject.MaxHitPointsExplanation;
			CampaignUIHelper.TooltipAddExplanation(list, ref maxHitPointsExplanation);
			return list;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00006B74 File Offset: 0x00004D74
		public static List<TooltipProperty> GetSiegeWallTooltip(int wallLevel, int wallHitpoints)
		{
			return new List<TooltipProperty>
			{
				new TooltipProperty(GameTexts.FindText("str_map_tooltip_wall_level", null).ToString(), wallLevel.ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.None),
				new TooltipProperty(GameTexts.FindText("str_map_tooltip_wall_hitpoints", null).ToString(), wallHitpoints.ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.None)
			};
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00006BD0 File Offset: 0x00004DD0
		public static List<TooltipProperty> GetGovernorPerksTooltipForHero(Hero hero)
		{
			List<TooltipProperty> list = new List<TooltipProperty>();
			list.Add(new TooltipProperty(GameTexts.FindText("str_clan_governor_perks", null).ToString(), " ", 0, false, TooltipProperty.TooltipPropertyFlags.None));
			CampaignUIHelper.TooltipAddSeperator(list, false);
			List<PerkObject> governorPerksForHero = PerkHelper.GetGovernorPerksForHero(hero);
			for (int i = 0; i < governorPerksForHero.Count; i++)
			{
				if (governorPerksForHero[i].PrimaryRole == SkillEffect.PerkRole.Governor)
				{
					list.Add(new TooltipProperty(governorPerksForHero[i].Name.ToString(), governorPerksForHero[i].PrimaryDescription.ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.None));
				}
				if (governorPerksForHero[i].SecondaryRole == SkillEffect.PerkRole.Governor)
				{
					list.Add(new TooltipProperty(governorPerksForHero[i].Name.ToString(), governorPerksForHero[i].SecondaryDescription.ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.None));
				}
			}
			return list;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00006CAC File Offset: 0x00004EAC
		[return: TupleElementNames(new string[]
		{
			"titleText",
			"bodyText"
		})]
		public static ValueTuple<TextObject, TextObject> GetGovernorSelectionConfirmationPopupTexts(Hero currentGovernor, Hero newGovernor, Settlement settlement)
		{
			if (settlement != null)
			{
				bool flag = newGovernor == null;
				DelayedTeleportationModel delayedTeleportationModel = Campaign.Current.Models.DelayedTeleportationModel;
				int num = (!flag) ? ((int)Math.Ceiling((double)delayedTeleportationModel.GetTeleportationDelayAsHours(newGovernor, settlement.Party).ResultNumber)) : 0;
				MBTextManager.SetTextVariable("TRAVEL_DURATION", CampaignUIHelper.GetHoursAndDaysTextFromHourValue(num).ToString(), false);
				CharacterObject characterObject = flag ? ((currentGovernor != null) ? currentGovernor.CharacterObject : null) : ((newGovernor != null) ? newGovernor.CharacterObject : null);
				if (characterObject != null)
				{
					StringHelpers.SetCharacterProperties("GOVERNOR", characterObject, null, false);
				}
				string variableName = "SETTLEMENT_NAME";
				TextObject name = settlement.Name;
				MBTextManager.SetTextVariable(variableName, ((name != null) ? name.ToString() : null) ?? string.Empty, false);
				TextObject item = GameTexts.FindText(flag ? "str_clan_remove_governor" : "str_clan_assign_governor", null);
				TextObject item2 = GameTexts.FindText(flag ? "str_remove_governor_inquiry" : ((num == 0) ? "str_change_governor_instantly_inquiry" : "str_change_governor_inquiry"), null);
				return new ValueTuple<TextObject, TextObject>(item, item2);
			}
			return new ValueTuple<TextObject, TextObject>(TextObject.Empty, TextObject.Empty);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00006DB0 File Offset: 0x00004FB0
		public static List<TooltipProperty> GetHeroGovernorEffectsTooltip(Hero hero, Settlement settlement)
		{
			List<TooltipProperty> list = new List<TooltipProperty>
			{
				new TooltipProperty("", hero.Name.ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.Title)
			};
			list.Add(new TooltipProperty(string.Empty, CampaignUIHelper.GetTeleportationDelayText(hero, settlement.Party).ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.None));
			MBTextManager.SetTextVariable("LEFT", GameTexts.FindText("str_tooltip_label_relation", null), false);
			string definition = GameTexts.FindText("str_LEFT_ONLY", null).ToString();
			list.Add(new TooltipProperty(definition, ((int)hero.GetRelationWithPlayer()).ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.None));
			MBTextManager.SetTextVariable("LEFT", GameTexts.FindText("str_tooltip_label_type", null), false);
			string definition2 = GameTexts.FindText("str_LEFT_ONLY", null).ToString();
			list.Add(new TooltipProperty(definition2, HeroHelper.GetCharacterTypeName(hero).ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.None));
			MobileParty partyBelongedTo = hero.PartyBelongedTo;
			SkillEffect.PerkRole? perkRole = (partyBelongedTo != null) ? new SkillEffect.PerkRole?(partyBelongedTo.GetHeroPerkRole(hero)) : null;
			if (perkRole != null)
			{
				SkillEffect.PerkRole? perkRole2 = perkRole;
				SkillEffect.PerkRole perkRole3 = SkillEffect.PerkRole.None;
				if (!(perkRole2.GetValueOrDefault() == perkRole3 & perkRole2 != null))
				{
					TextObject textObject = GameTexts.FindText("role", perkRole.Value.ToString());
					list.Add(new TooltipProperty(new TextObject("{=9FJi2SaE}Party Role", null).ToString(), textObject.ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.None));
				}
			}
			CampaignUIHelper.TooltipAddEmptyLine(list, false);
			list.Add(new TooltipProperty(new TextObject("{=J8ddrAOf}Governor Effects", null).ToString(), " ", 0, false, TooltipProperty.TooltipPropertyFlags.None));
			CampaignUIHelper.TooltipAddSeperator(list, false);
			ValueTuple<TextObject, TextObject> governorEngineeringSkillEffectForHero = PerkHelper.GetGovernorEngineeringSkillEffectForHero(hero);
			list.Add(new TooltipProperty(governorEngineeringSkillEffectForHero.Item1.ToString(), governorEngineeringSkillEffectForHero.Item2.ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.None));
			CampaignUIHelper.TooltipAddEmptyLine(list, false);
			List<TooltipProperty> governorPerksTooltipForHero = CampaignUIHelper.GetGovernorPerksTooltipForHero(hero);
			list.AddRange(governorPerksTooltipForHero);
			return list;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00006F94 File Offset: 0x00005194
		public static List<TooltipProperty> GetEncounterPartyMoraleTooltip(List<MobileParty> parties)
		{
			return new List<TooltipProperty>();
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00006F9C File Offset: 0x0000519C
		public static TextObject GetCraftingTemplatePieceUnlockProgressHint(float progress)
		{
			TextObject textObject = GameTexts.FindText("str_LEFT_over_RIGHT_in_paranthesis", null);
			textObject.SetTextVariable("LEFT", progress.ToString("F0"));
			textObject.SetTextVariable("RIGHT", "100");
			TextObject variable = new TextObject("{=opU0Nr2G}Progress for unlocking a new piece.", null);
			TextObject textObject2 = GameTexts.FindText("str_STR1_space_STR2", null);
			textObject2.SetTextVariable("STR1", variable);
			textObject2.SetTextVariable("STR2", textObject);
			return textObject2;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00007010 File Offset: 0x00005210
		public static List<ValueTuple<string, TextObject>> GetWeaponFlagDetails(WeaponFlags weaponFlags, CharacterObject character = null)
		{
			List<ValueTuple<string, TextObject>> list = new List<ValueTuple<string, TextObject>>();
			if (weaponFlags.HasAnyFlag(WeaponFlags.BonusAgainstShield))
			{
				string item = "WeaponFlagIcons\\bonus_against_shield";
				TextObject item2 = GameTexts.FindText("str_inventory_flag_bonus_against_shield", null);
				list.Add(new ValueTuple<string, TextObject>(item, item2));
			}
			if (weaponFlags.HasAnyFlag(WeaponFlags.CanKnockDown))
			{
				string item = "WeaponFlagIcons\\can_knock_down";
				TextObject item2 = GameTexts.FindText("str_inventory_flag_can_knockdown", null);
				list.Add(new ValueTuple<string, TextObject>(item, item2));
			}
			if (weaponFlags.HasAnyFlag(WeaponFlags.CanDismount))
			{
				string item = "WeaponFlagIcons\\can_dismount";
				TextObject item2 = GameTexts.FindText("str_inventory_flag_can_dismount", null);
				list.Add(new ValueTuple<string, TextObject>(item, item2));
			}
			if (weaponFlags.HasAnyFlag(WeaponFlags.CanHook))
			{
				string item = "WeaponFlagIcons\\can_dismount";
				TextObject item2 = GameTexts.FindText("str_inventory_flag_can_hook", null);
				list.Add(new ValueTuple<string, TextObject>(item, item2));
			}
			if (weaponFlags.HasAnyFlag(WeaponFlags.CanCrushThrough))
			{
				string item = "WeaponFlagIcons\\can_crush_through";
				TextObject item2 = GameTexts.FindText("str_inventory_flag_can_crush_through", null);
				list.Add(new ValueTuple<string, TextObject>(item, item2));
			}
			if (weaponFlags.HasAnyFlag(WeaponFlags.NotUsableWithTwoHand))
			{
				string item = "WeaponFlagIcons\\not_usable_with_two_hand";
				TextObject item2 = GameTexts.FindText("str_inventory_flag_not_usable_two_hand", null);
				list.Add(new ValueTuple<string, TextObject>(item, item2));
			}
			if (weaponFlags.HasAnyFlag(WeaponFlags.NotUsableWithOneHand))
			{
				string item = "WeaponFlagIcons\\not_usable_with_one_hand";
				TextObject item2 = GameTexts.FindText("str_inventory_flag_not_usable_one_hand", null);
				list.Add(new ValueTuple<string, TextObject>(item, item2));
			}
			if (weaponFlags.HasAnyFlag(WeaponFlags.CantReloadOnHorseback) && (character == null || !character.GetPerkValue(DefaultPerks.Crossbow.MountedCrossbowman)))
			{
				string item = "WeaponFlagIcons\\cant_reload_on_horseback";
				TextObject item2 = GameTexts.FindText("str_inventory_flag_cant_reload_on_horseback", null);
				list.Add(new ValueTuple<string, TextObject>(item, item2));
			}
			return list;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x0000719C File Offset: 0x0000539C
		public static List<Tuple<string, TextObject>> GetItemFlagDetails(ItemFlags itemFlags)
		{
			List<Tuple<string, TextObject>> list = new List<Tuple<string, TextObject>>();
			if (itemFlags.HasAnyFlag(ItemFlags.Civilian))
			{
				string item = "GeneralFlagIcons\\civillian";
				TextObject item2 = GameTexts.FindText("str_inventory_flag_civillian", null);
				list.Add(new Tuple<string, TextObject>(item, item2));
			}
			return list;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000071DC File Offset: 0x000053DC
		public static List<ValueTuple<string, TextObject>> GetItemUsageSetFlagDetails(ItemObject.ItemUsageSetFlags flags, CharacterObject character = null)
		{
			List<ValueTuple<string, TextObject>> list = new List<ValueTuple<string, TextObject>>();
			if (flags.HasAnyFlag(ItemObject.ItemUsageSetFlags.RequiresNoMount) && (character == null || !character.GetPerkValue(DefaultPerks.Bow.HorseMaster)))
			{
				list.Add(new ValueTuple<string, TextObject>("WeaponFlagIcons\\cant_use_on_horseback", GameTexts.FindText("str_inventory_flag_cant_use_with_mounts", null)));
			}
			if (flags.HasAnyFlag(ItemObject.ItemUsageSetFlags.RequiresNoShield))
			{
				list.Add(new ValueTuple<string, TextObject>("WeaponFlagIcons\\cant_use_with_shields", GameTexts.FindText("str_inventory_flag_cant_use_with_shields", null)));
			}
			return list;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00007250 File Offset: 0x00005450
		public static List<ValueTuple<string, TextObject>> GetFlagDetailsForWeapon(WeaponComponentData weapon, ItemObject.ItemUsageSetFlags itemUsageFlags, CharacterObject character = null)
		{
			List<ValueTuple<string, TextObject>> list = new List<ValueTuple<string, TextObject>>();
			if (weapon == null)
			{
				return list;
			}
			if (weapon.RelevantSkill == DefaultSkills.Bow)
			{
				list.Add(new ValueTuple<string, TextObject>("WeaponFlagIcons\\bow", GameTexts.FindText("str_inventory_flag_bow", null)));
			}
			if (weapon.RelevantSkill == DefaultSkills.Crossbow)
			{
				list.Add(new ValueTuple<string, TextObject>("WeaponFlagIcons\\crossbow", GameTexts.FindText("str_inventory_flag_crossbow", null)));
			}
			if (weapon.RelevantSkill == DefaultSkills.Polearm)
			{
				list.Add(new ValueTuple<string, TextObject>("WeaponFlagIcons\\polearm", GameTexts.FindText("str_inventory_flag_polearm", null)));
			}
			if (weapon.RelevantSkill == DefaultSkills.OneHanded)
			{
				list.Add(new ValueTuple<string, TextObject>("WeaponFlagIcons\\one_handed", GameTexts.FindText("str_inventory_flag_one_handed", null)));
			}
			if (weapon.RelevantSkill == DefaultSkills.TwoHanded)
			{
				list.Add(new ValueTuple<string, TextObject>("WeaponFlagIcons\\two_handed", GameTexts.FindText("str_inventory_flag_two_handed", null)));
			}
			if (weapon.RelevantSkill == DefaultSkills.Throwing)
			{
				list.Add(new ValueTuple<string, TextObject>("WeaponFlagIcons\\throwing", GameTexts.FindText("str_inventory_flag_throwing", null)));
			}
			List<ValueTuple<string, TextObject>> weaponFlagDetails = CampaignUIHelper.GetWeaponFlagDetails(weapon.WeaponFlags, character);
			list.AddRange(weaponFlagDetails);
			List<ValueTuple<string, TextObject>> itemUsageSetFlagDetails = CampaignUIHelper.GetItemUsageSetFlagDetails(itemUsageFlags, character);
			list.AddRange(itemUsageSetFlagDetails);
			string weaponDescriptionId = weapon.WeaponDescriptionId;
			if (weaponDescriptionId != null && weaponDescriptionId.IndexOf("couch", StringComparison.OrdinalIgnoreCase) >= 0)
			{
				list.Add(new ValueTuple<string, TextObject>("WeaponFlagIcons\\can_couchable", GameTexts.FindText("str_inventory_flag_couchable", null)));
			}
			string weaponDescriptionId2 = weapon.WeaponDescriptionId;
			if (weaponDescriptionId2 != null && weaponDescriptionId2.IndexOf("bracing", StringComparison.OrdinalIgnoreCase) >= 0)
			{
				list.Add(new ValueTuple<string, TextObject>("WeaponFlagIcons\\braceable", GameTexts.FindText("str_inventory_flag_braceable", null)));
			}
			return list;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000073F4 File Offset: 0x000055F4
		public static string GetFormattedItemPropertyText(float propertyValue, bool typeRequiresInteger)
		{
			bool flag = propertyValue >= 100f || (propertyValue % 1f).ApproximatelyEqualsTo(0f, 0.001f);
			if (typeRequiresInteger || flag)
			{
				return propertyValue.ToString("F0");
			}
			if (propertyValue >= 10f)
			{
				return propertyValue.ToString("F1");
			}
			return propertyValue.ToString("F2");
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00007458 File Offset: 0x00005658
		public static List<TooltipProperty> GetCraftingHeroTooltip(Hero hero, CraftingOrder order)
		{
			object obj = order != null && !order.IsOrderAvailableForHero(hero);
			ICraftingCampaignBehavior campaignBehavior = Campaign.Current.GetCampaignBehavior<ICraftingCampaignBehavior>();
			List<TooltipProperty> list = new List<TooltipProperty>();
			object obj2 = obj;
			string propertyName = (obj2 != null) ? GameTexts.FindText("str_crafting_hero_can_not_craft_item", null).ToString() : hero.Name.ToString();
			CampaignUIHelper.TooltipAddPropertyTitle(list, propertyName);
			if (obj2 != null)
			{
				List<Hero> list2 = (from h in CraftingHelper.GetAvailableHeroesForCrafting()
				where order.IsOrderAvailableForHero(h)
				select h).ToList<Hero>();
				if (list2.Count > 0)
				{
					GameTexts.SetVariable("SKILL", GameTexts.FindText("str_crafting", null).ToString());
					list.Add(new TooltipProperty(GameTexts.FindText("str_crafting_hero_not_enough_skills", null).ToString(), "", 0, false, TooltipProperty.TooltipPropertyFlags.None));
					CampaignUIHelper.TooltipAddEmptyLine(list, false);
					list.Add(new TooltipProperty(GameTexts.FindText("str_crafting_following_can_craft_order", null).ToString(), "", 0, false, TooltipProperty.TooltipPropertyFlags.None));
					for (int i = 0; i < list2.Count; i++)
					{
						Hero hero2 = list2[i];
						GameTexts.SetVariable("HERO_NAME", hero2.Name);
						list.Add(new TooltipProperty(GameTexts.FindText("str_crafting_hero_able_to_craft", null).ToString(), "", 0, false, TooltipProperty.TooltipPropertyFlags.None));
					}
				}
				else
				{
					list.Add(new TooltipProperty(GameTexts.FindText("str_crafting_no_one_can_craft_order", null).ToString(), "", 0, false, TooltipProperty.TooltipPropertyFlags.None));
				}
			}
			else
			{
				list.Add(new TooltipProperty(new TextObject("{=cUUI8u2G}Smithy Stamina", null).ToString(), campaignBehavior.GetHeroCraftingStamina(hero).ToString() + " / " + campaignBehavior.GetMaxHeroCraftingStamina(hero).ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.None));
				list.Add(new TooltipProperty(new TextObject("{=lVuGCYPC}Smithing Skill", null).ToString(), hero.GetSkillValue(DefaultSkills.Crafting).ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.None));
			}
			return list;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00007658 File Offset: 0x00005858
		public static List<TooltipProperty> GetOrderCannotBeCompletedReasonTooltip(CraftingOrder order, ItemObject item)
		{
			CampaignUIHelper.<>c__DisplayClass145_0 CS$<>8__locals1;
			CS$<>8__locals1.order = order;
			CS$<>8__locals1.properties = new List<TooltipProperty>();
			CampaignUIHelper.TooltipAddPropertyTitle(CS$<>8__locals1.properties, new TextObject("{=Syha8biz}Order Can Not Be Completed", null).ToString());
			CS$<>8__locals1.properties.Add(new TooltipProperty(new TextObject("{=gTbE6t9I}Following requirements are not met:", null).ToString(), "", 0, false, TooltipProperty.TooltipPropertyFlags.None));
			if (CS$<>8__locals1.order.PreCraftedWeaponDesignItem.PrimaryWeapon.SwingDamageType != item.PrimaryWeapon.SwingDamageType)
			{
				DamageTypes swingDamageType = CS$<>8__locals1.order.PreCraftedWeaponDesignItem.PrimaryWeapon.SwingDamageType;
				int thrustDamageType = (int)item.PrimaryWeapon.ThrustDamageType;
				TextObject textObject = TextObject.Empty;
				if (thrustDamageType == -1)
				{
					textObject = TextObject.Empty;
				}
				else
				{
					textObject = new TextObject("{=MT5A04X8} - Swing Damage Type does not match. Should be: {TYPE}", null);
					TextObject textObject2 = textObject;
					string tag = "TYPE";
					string id = "str_inventory_dmg_type";
					int i = (int)swingDamageType;
					textObject2.SetTextVariable(tag, GameTexts.FindText(id, i.ToString()));
				}
				CS$<>8__locals1.properties.Add(new TooltipProperty(textObject.ToString(), "", 0, false, TooltipProperty.TooltipPropertyFlags.None));
			}
			if (CS$<>8__locals1.order.PreCraftedWeaponDesignItem.PrimaryWeapon.ThrustDamageType != item.PrimaryWeapon.ThrustDamageType)
			{
				DamageTypes thrustDamageType2 = CS$<>8__locals1.order.PreCraftedWeaponDesignItem.PrimaryWeapon.ThrustDamageType;
				int thrustDamageType3 = (int)item.PrimaryWeapon.ThrustDamageType;
				TextObject textObject3 = TextObject.Empty;
				if (thrustDamageType3 == -1)
				{
					textObject3 = TextObject.Empty;
				}
				else
				{
					textObject3 = new TextObject("{=Tx9Mynbt} - Thrust Damage Type does not match. Should be: {TYPE}", null);
					TextObject textObject4 = textObject3;
					string tag2 = "TYPE";
					string id2 = "str_inventory_dmg_type";
					int i = (int)thrustDamageType2;
					textObject4.SetTextVariable(tag2, GameTexts.FindText(id2, i.ToString()).ToString());
				}
				CS$<>8__locals1.properties.Add(new TooltipProperty(textObject3.ToString(), "", 0, false, TooltipProperty.TooltipPropertyFlags.None));
			}
			float num = (float)CS$<>8__locals1.order.PreCraftedWeaponDesignItem.PrimaryWeapon.ThrustSpeed;
			float num2 = (float)item.PrimaryWeapon.ThrustSpeed;
			if (num > num2)
			{
				CampaignUIHelper.<GetOrderCannotBeCompletedReasonTooltip>g__AddProperty|145_0(CraftingTemplate.CraftingStatTypes.ThrustSpeed, num, ref CS$<>8__locals1);
			}
			num = (float)CS$<>8__locals1.order.PreCraftedWeaponDesignItem.PrimaryWeapon.SwingSpeed;
			num2 = (float)item.PrimaryWeapon.SwingSpeed;
			if (num > num2)
			{
				CampaignUIHelper.<GetOrderCannotBeCompletedReasonTooltip>g__AddProperty|145_0(CraftingTemplate.CraftingStatTypes.SwingSpeed, num, ref CS$<>8__locals1);
			}
			num = (float)CS$<>8__locals1.order.PreCraftedWeaponDesignItem.PrimaryWeapon.MissileSpeed;
			num2 = (float)item.PrimaryWeapon.MissileSpeed;
			if (num > num2)
			{
				CampaignUIHelper.<GetOrderCannotBeCompletedReasonTooltip>g__AddProperty|145_0(CraftingTemplate.CraftingStatTypes.MissileSpeed, num, ref CS$<>8__locals1);
			}
			num = (float)CS$<>8__locals1.order.PreCraftedWeaponDesignItem.PrimaryWeapon.ThrustDamage;
			num2 = (float)item.PrimaryWeapon.ThrustDamage;
			if (num > num2)
			{
				CampaignUIHelper.<GetOrderCannotBeCompletedReasonTooltip>g__AddProperty|145_0(CraftingTemplate.CraftingStatTypes.ThrustDamage, num, ref CS$<>8__locals1);
			}
			num = (float)CS$<>8__locals1.order.PreCraftedWeaponDesignItem.PrimaryWeapon.SwingDamage;
			num2 = (float)item.PrimaryWeapon.SwingDamage;
			if (num > num2)
			{
				CampaignUIHelper.<GetOrderCannotBeCompletedReasonTooltip>g__AddProperty|145_0(CraftingTemplate.CraftingStatTypes.SwingDamage, num, ref CS$<>8__locals1);
			}
			num = (float)CS$<>8__locals1.order.PreCraftedWeaponDesignItem.PrimaryWeapon.Accuracy;
			num2 = (float)item.PrimaryWeapon.Accuracy;
			if (num > num2)
			{
				CampaignUIHelper.<GetOrderCannotBeCompletedReasonTooltip>g__AddProperty|145_0(CraftingTemplate.CraftingStatTypes.Accuracy, num, ref CS$<>8__locals1);
			}
			num = (float)CS$<>8__locals1.order.PreCraftedWeaponDesignItem.PrimaryWeapon.Handling;
			num2 = (float)item.PrimaryWeapon.Handling;
			if (num > num2)
			{
				CampaignUIHelper.<GetOrderCannotBeCompletedReasonTooltip>g__AddProperty|145_0(CraftingTemplate.CraftingStatTypes.Handling, num, ref CS$<>8__locals1);
			}
			bool flag = true;
			WeaponDescription[] weaponDescriptions = CS$<>8__locals1.order.PreCraftedWeaponDesignItem.WeaponDesign.Template.WeaponDescriptions;
			for (int i = 0; i < weaponDescriptions.Length; i++)
			{
				WeaponDescription weaponDescription = weaponDescriptions[i];
				if (item.WeaponDesign.Template.WeaponDescriptions.All((WeaponDescription d) => d.WeaponClass != weaponDescription.WeaponClass))
				{
					flag = false;
					break;
				}
			}
			if (!flag)
			{
				CS$<>8__locals1.properties.Add(new TooltipProperty(new TextObject("{=Q1KwpZYu}Weapon usage does not match requirements", null).ToString(), "", 0, false, TooltipProperty.TooltipPropertyFlags.None));
			}
			return CS$<>8__locals1.properties;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00007A14 File Offset: 0x00005C14
		public static List<TooltipProperty> GetCraftingOrderDisabledReasonTooltip(Hero heroToCheck, CraftingOrder order)
		{
			List<TooltipProperty> list = new List<TooltipProperty>();
			if (order.IsOrderAvailableForHero(heroToCheck))
			{
				return list;
			}
			GameTexts.SetVariable("SKILL", GameTexts.FindText("str_crafting", null).ToString());
			CampaignUIHelper.TooltipAddPropertyTitle(list, GameTexts.FindText("str_crafting_cannot_be_crafted", null).ToString());
			if (!order.IsOrderAvailableForHero(heroToCheck))
			{
				List<Hero> list2 = (from h in CraftingHelper.GetAvailableHeroesForCrafting()
				where order.IsOrderAvailableForHero(h)
				select h).ToList<Hero>();
				if (list2.Count > 0)
				{
					GameTexts.SetVariable("HERO", heroToCheck.Name.ToString());
					list.Add(new TooltipProperty(GameTexts.FindText("str_crafting_player_not_enough_skills", null).ToString(), "", 0, false, TooltipProperty.TooltipPropertyFlags.None));
					CampaignUIHelper.TooltipAddEmptyLine(list, false);
					list.Add(new TooltipProperty(GameTexts.FindText("str_crafting_following_can_craft_order", null).ToString(), "", 0, false, TooltipProperty.TooltipPropertyFlags.None));
					for (int i = 0; i < list2.Count; i++)
					{
						Hero hero = list2[i];
						GameTexts.SetVariable("HERO_NAME", hero.Name);
						list.Add(new TooltipProperty(GameTexts.FindText("str_crafting_hero_able_to_craft", null).ToString(), "", 0, false, TooltipProperty.TooltipPropertyFlags.None));
					}
				}
				else
				{
					int content = MathF.Ceiling(order.OrderDifficulty) - heroToCheck.GetSkillValue(DefaultSkills.Crafting) - 50;
					GameTexts.SetVariable("AMOUNT", content);
					list.Add(new TooltipProperty(GameTexts.FindText("str_crafting_no_one_can_craft_order", null).ToString(), "", 0, false, TooltipProperty.TooltipPropertyFlags.None));
				}
			}
			return list;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00007BB0 File Offset: 0x00005DB0
		public static List<TooltipProperty> GetOrdersDisabledReasonTooltip(MBBindingList<CraftingOrderItemVM> craftingOrders, Hero heroToCheck)
		{
			List<TooltipProperty> list = new List<TooltipProperty>();
			if (craftingOrders != null)
			{
				if (craftingOrders.Count((CraftingOrderItemVM x) => x.IsEnabled) > 0)
				{
					return list;
				}
			}
			bool flag = false;
			CampaignUIHelper.TooltipAddPropertyTitle(list, GameTexts.FindText("str_crafting_cannot_complete_orders", null).ToString());
			GameTexts.SetVariable("HERO_NAME", heroToCheck.Name);
			list.Add(new TooltipProperty(GameTexts.FindText("str_crafting_no_available_orders_for_hero", null).ToString(), "", 0, false, TooltipProperty.TooltipPropertyFlags.None));
			CampaignUIHelper.TooltipAddEmptyLine(list, false);
			IEnumerable<Hero> availableHeroesForCrafting = CraftingHelper.GetAvailableHeroesForCrafting();
			for (int i = 0; i < availableHeroesForCrafting.Count<Hero>(); i++)
			{
				Hero hero = availableHeroesForCrafting.ToList<Hero>()[i];
				int num = craftingOrders.Count((CraftingOrderItemVM x) => x.CraftingOrder.IsOrderAvailableForHero(hero));
				if (num > 0)
				{
					flag = true;
					GameTexts.SetVariable("HERO_NAME", hero.Name);
					GameTexts.SetVariable("NUMBER", num);
					list.Add(new TooltipProperty(GameTexts.FindText("str_crafting_available_orders_for_other_hero", null).ToString(), "", 0, false, TooltipProperty.TooltipPropertyFlags.None));
				}
			}
			if (!flag)
			{
				GameTexts.SetVariable("SKILL", GameTexts.FindText("str_crafting", null).ToString());
				list.Add(new TooltipProperty(GameTexts.FindText("str_crafting_no_available_orders_for_party", null).ToString(), "", 0, false, TooltipProperty.TooltipPropertyFlags.None));
			}
			return list;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00007D18 File Offset: 0x00005F18
		public static string GetCraftingOrderMissingPropertyWarningText(CraftingOrder order, ItemObject craftedItem)
		{
			if (order == null)
			{
				return string.Empty;
			}
			bool flag = true;
			bool flag2 = true;
			WeaponComponentData statWeapon = order.GetStatWeapon();
			WeaponComponentData weaponComponentData = null;
			for (int i = 0; i < craftedItem.Weapons.Count; i++)
			{
				if (craftedItem.Weapons[i].WeaponDescriptionId == statWeapon.WeaponDescriptionId)
				{
					weaponComponentData = craftedItem.Weapons[i];
					break;
				}
			}
			if (weaponComponentData == null)
			{
				weaponComponentData = craftedItem.PrimaryWeapon;
			}
			string variable = string.Empty;
			if (statWeapon.SwingDamageType != DamageTypes.Invalid && statWeapon.SwingDamageType != weaponComponentData.SwingDamageType)
			{
				flag = false;
				variable = GameTexts.FindText("str_damage_types", statWeapon.SwingDamageType.ToString()).ToString();
			}
			else if (statWeapon.ThrustDamageType != DamageTypes.Invalid && statWeapon.ThrustDamageType != weaponComponentData.ThrustDamageType)
			{
				flag2 = false;
				variable = GameTexts.FindText("str_damage_types", statWeapon.ThrustDamageType.ToString()).ToString();
			}
			if (!flag)
			{
				return GameTexts.FindText("str_crafting_should_have_swing_damage", null).SetTextVariable("SWING_DAMAGE_TYPE", variable).ToString();
			}
			if (!flag2)
			{
				return GameTexts.FindText("str_crafting_should_have_thrust_damage", null).SetTextVariable("THRUST_DAMAGE_TYPE", variable).ToString();
			}
			return string.Empty;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00007E5C File Offset: 0x0000605C
		public static List<TooltipProperty> GetInventoryCharacterTooltip(Hero hero)
		{
			List<TooltipProperty> list = new List<TooltipProperty>();
			CampaignUIHelper._inventorySkillTooltipTitle.SetTextVariable("HERO_NAME", hero.Name);
			CampaignUIHelper.TooltipAddPropertyTitle(list, CampaignUIHelper._inventorySkillTooltipTitle.ToString());
			CampaignUIHelper.TooltipAddDoubleSeperator(list, false);
			for (int i = 0; i < Skills.All.Count; i++)
			{
				SkillObject skillObject = Skills.All[i];
				list.Add(new TooltipProperty(skillObject.Name.ToString(), hero.GetSkillValue(skillObject).ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.None));
			}
			return list;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00007EE8 File Offset: 0x000060E8
		public static string GetHeroOccupationName(Hero hero)
		{
			string id;
			if (hero.IsWanderer)
			{
				id = "str_wanderer";
			}
			else if (hero.IsGangLeader)
			{
				id = "str_gang_leader";
			}
			else if (hero.IsPreacher)
			{
				id = "str_preacher";
			}
			else if (hero.IsMerchant)
			{
				id = "str_merchant";
			}
			else
			{
				Clan clan = hero.Clan;
				if (clan != null && clan.IsClanTypeMercenary)
				{
					id = "str_mercenary";
				}
				else if (hero.IsArtisan)
				{
					id = "str_artisan";
				}
				else if (hero.IsRuralNotable)
				{
					id = "str_charactertype_ruralnotable";
				}
				else if (hero.IsHeadman)
				{
					id = "str_charactertype_headman";
				}
				else if (hero.IsMinorFactionHero)
				{
					id = "str_charactertype_minorfaction";
				}
				else
				{
					if (!hero.IsLord)
					{
						return "";
					}
					if (hero.IsFemale)
					{
						id = "str_charactertype_lady";
					}
					else
					{
						id = "str_charactertype_lord";
					}
				}
			}
			return GameTexts.FindText(id, null).ToString();
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00007FD8 File Offset: 0x000061D8
		private static TooltipProperty GetSiegeMachineProgressLine(int hoursRemaining)
		{
			if (hoursRemaining > 0)
			{
				string text = CampaignUIHelper.GetHoursAndDaysTextFromHourValue(hoursRemaining).ToString();
				MBTextManager.SetTextVariable("PREPARATION_TIME", text, false);
				string value = GameTexts.FindText("str_preparations_complete_in_hours", null).ToString();
				return new TooltipProperty(" ", value, 0, false, TooltipProperty.TooltipPropertyFlags.None);
			}
			return null;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00008024 File Offset: 0x00006224
		public static TextObject GetCommaSeparatedText(TextObject label, IEnumerable<TextObject> texts)
		{
			TextObject textObject = new TextObject("{=!}{RESULT}", null);
			int num = 0;
			foreach (TextObject text in texts)
			{
				if (num == 0)
				{
					MBTextManager.SetTextVariable("STR1", label ?? TextObject.Empty, false);
					MBTextManager.SetTextVariable("STR2", text, false);
					string text2 = GameTexts.FindText("str_STR1_STR2", null).ToString();
					MBTextManager.SetTextVariable("LEFT", text2, false);
					textObject.SetTextVariable("RESULT", text2);
				}
				else
				{
					MBTextManager.SetTextVariable("RIGHT", text, false);
					string text3 = GameTexts.FindText("str_LEFT_comma_RIGHT", null).ToString();
					MBTextManager.SetTextVariable("LEFT", text3, false);
					textObject.SetTextVariable("RESULT", text3);
				}
				num++;
			}
			return textObject;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0000810C File Offset: 0x0000630C
		public static string GetHeroKingdomRank(Hero hero)
		{
			if (hero.Clan.Kingdom != null)
			{
				bool isUnderMercenaryService = hero.Clan.IsUnderMercenaryService;
				bool flag = hero == hero.Clan.Kingdom.Leader;
				bool flag2 = hero.Clan.Leader == hero;
				bool flag3 = !flag && !flag2;
				bool flag4 = hero.PartyBelongedTo != null && hero.PartyBelongedTo.LeaderHero == hero;
				TextObject textObject = TextObject.Empty;
				GameTexts.SetVariable("FACTION", hero.Clan.Kingdom.Name);
				GameTexts.SetVariable("FACTION_INFORMAL_NAME", hero.Clan.Kingdom.InformalName);
				if (flag)
				{
					textObject = GameTexts.FindText("str_hero_rank_of_faction", 1.ToString());
				}
				else if (isUnderMercenaryService)
				{
					textObject = GameTexts.FindText("str_hero_rank_of_faction_mercenary", null);
				}
				else if (flag2 || flag4)
				{
					textObject = GameTexts.FindText("str_hero_rank_of_faction", 0.ToString());
				}
				else if (flag3)
				{
					textObject = GameTexts.FindText("str_hero_rank_of_faction_nobleman", null);
				}
				textObject.SetCharacterProperties("HERO", hero.CharacterObject, false);
				return textObject.ToString();
			}
			return string.Empty;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00008230 File Offset: 0x00006430
		public static string GetHeroRank(Hero hero)
		{
			if (hero.Clan != null)
			{
				bool isUnderMercenaryService = hero.Clan.IsUnderMercenaryService;
				Kingdom kingdom = hero.Clan.Kingdom;
				bool flag = hero == ((kingdom != null) ? kingdom.Leader : null);
				bool flag2 = hero.Clan.Leader == hero && hero.Clan.Kingdom != null;
				bool flag3 = !flag && !flag2 && hero.Clan.Kingdom != null;
				if (flag)
				{
					return GameTexts.FindText("str_hero_rank", 1.ToString()).ToString();
				}
				if (isUnderMercenaryService)
				{
					return GameTexts.FindText("str_hero_rank_mercenary", null).ToString();
				}
				if (flag2)
				{
					return GameTexts.FindText("str_hero_rank", 0.ToString()).ToString();
				}
				if (flag3)
				{
					return GameTexts.FindText("str_hero_rank_nobleman", null).ToString();
				}
			}
			return string.Empty;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000830C File Offset: 0x0000650C
		public static bool IsSettlementInformationHidden(Settlement settlement, out TextObject disableReason)
		{
			bool flag = !Campaign.Current.Models.InformationRestrictionModel.DoesPlayerKnowDetailsOf(settlement);
			disableReason = (flag ? new TextObject("{=cDkHJOkl}You need to be in the viewing range, control this settlement with your kingdom or have a clan member in the settlement to see its details.", null) : TextObject.Empty);
			return flag;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x0000834C File Offset: 0x0000654C
		public static bool IsHeroInformationHidden(Hero hero, out TextObject disableReason)
		{
			bool flag = !Campaign.Current.Models.InformationRestrictionModel.DoesPlayerKnowDetailsOf(hero);
			disableReason = (flag ? new TextObject("{=akHsjtPh}You haven't met this hero yet.", null) : TextObject.Empty);
			return flag;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x0000838C File Offset: 0x0000658C
		public static string GetPartyNameplateText(MobileParty party, bool includeAttachedParties)
		{
			int num = party.MemberRoster.TotalHealthyCount;
			int num2 = party.MemberRoster.TotalWounded;
			if (includeAttachedParties && party.Army != null && party.Army.LeaderParty == party)
			{
				for (int i = 0; i < party.Army.LeaderParty.AttachedParties.Count; i++)
				{
					MobileParty mobileParty = party.Army.LeaderParty.AttachedParties[i];
					num += mobileParty.MemberRoster.TotalHealthyCount;
					num2 += mobileParty.MemberRoster.TotalWounded;
				}
			}
			string abbreviatedValueTextFromValue = CampaignUIHelper.GetAbbreviatedValueTextFromValue(num);
			string abbreviatedValueTextFromValue2 = CampaignUIHelper.GetAbbreviatedValueTextFromValue(num2);
			return abbreviatedValueTextFromValue + ((num2 > 0) ? (" + " + abbreviatedValueTextFromValue2 + GameTexts.FindText("str_party_nameplate_wounded_abbr", null).ToString()) : "");
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00008458 File Offset: 0x00006658
		public static string GetPartyNameplateText(PartyBase party)
		{
			int totalHealthyCount = party.MemberRoster.TotalHealthyCount;
			int totalWounded = party.MemberRoster.TotalWounded;
			string abbreviatedValueTextFromValue = CampaignUIHelper.GetAbbreviatedValueTextFromValue(totalHealthyCount);
			string abbreviatedValueTextFromValue2 = CampaignUIHelper.GetAbbreviatedValueTextFromValue(totalWounded);
			return abbreviatedValueTextFromValue + ((totalWounded > 0) ? (" + " + abbreviatedValueTextFromValue2 + GameTexts.FindText("str_party_nameplate_wounded_abbr", null).ToString()) : "");
		}

		// Token: 0x06000131 RID: 305 RVA: 0x000084B4 File Offset: 0x000066B4
		public static string GetValueChangeText(float originalValue, float valueChange, string valueFormat = "F0")
		{
			string text = originalValue.ToString(valueFormat);
			TextObject textObject = GameTexts.FindText("str_clan_workshop_material_daily_Change", null).SetTextVariable("IS_POSITIVE", (valueChange >= 0f) ? 1 : 0).SetTextVariable("CHANGE", MathF.Abs(valueChange).ToString(valueFormat));
			TextObject textObject2 = GameTexts.FindText("str_STR_in_parentheses", null);
			textObject2.SetTextVariable("STR", textObject.ToString());
			return GameTexts.FindText("str_STR1_space_STR2", null).SetTextVariable("STR1", text.ToString()).SetTextVariable("STR2", textObject2.ToString()).ToString();
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00008554 File Offset: 0x00006754
		public static string GetUpgradeHint(int index, int numOfItems, int availableUpgrades, int upgradeCoinCost, bool hasRequiredPerk, PerkObject requiredPerk, CharacterObject character, TroopRosterElement troop, int partyGoldChangeAmount, string entireStackShortcutKeyText, string fiveStackShortcutKeyText)
		{
			string text = null;
			CharacterObject characterObject = character.UpgradeTargets[index];
			int level = characterObject.Level;
			if (character.Culture.IsBandit ? (level >= character.Level) : (level > character.Level))
			{
				int upgradeXpCost = character.GetUpgradeXpCost(PartyBase.MainParty, index);
				GameTexts.SetVariable("newline", "\n");
				TextObject textObject = new TextObject("{=f4nc7FfE}Upgrade to {UPGRADE_NAME}", null);
				textObject.SetTextVariable("UPGRADE_NAME", characterObject.Name);
				text = textObject.ToString();
				if (troop.Xp < upgradeXpCost)
				{
					TextObject textObject2 = new TextObject("{=Voa0sinH}Required: {NEEDED_EXP_AMOUNT}xp (You have {CURRENT_EXP_AMOUNT})", null);
					textObject2.SetTextVariable("NEEDED_EXP_AMOUNT", upgradeXpCost);
					textObject2.SetTextVariable("CURRENT_EXP_AMOUNT", troop.Xp);
					GameTexts.SetVariable("STR1", text);
					GameTexts.SetVariable("STR2", textObject2);
					text = GameTexts.FindText("str_string_newline_string", null).ToString();
				}
				if (characterObject.UpgradeRequiresItemFromCategory != null)
				{
					TextObject textObject3 = new TextObject((numOfItems > 0) ? "{=Raa4j4rF}Required: {UPGRADE_ITEM}" : "{=rThSy9ed}Required: {UPGRADE_ITEM} (You have none)", null);
					textObject3.SetTextVariable("UPGRADE_ITEM", characterObject.UpgradeRequiresItemFromCategory.GetName().ToString());
					GameTexts.SetVariable("STR1", text);
					GameTexts.SetVariable("STR2", textObject3.ToString());
					text = GameTexts.FindText("str_string_newline_string", null).ToString();
				}
				TextObject textObject4 = new TextObject((Hero.MainHero.Gold + partyGoldChangeAmount < upgradeCoinCost) ? "{=63Ic1Ahe}Cost: {UPGRADE_COST} (You don't have)" : "{=McJjNM50}Cost: {UPGRADE_COST}", null);
				textObject4.SetTextVariable("UPGRADE_COST", upgradeCoinCost);
				GameTexts.SetVariable("STR1", textObject4);
				GameTexts.SetVariable("STR2", "{=!}<img src=\"General\\Icons\\Coin@2x\" extend=\"8\">");
				string content = GameTexts.FindText("str_STR1_STR2", null).ToString();
				GameTexts.SetVariable("STR1", text);
				GameTexts.SetVariable("STR2", content);
				text = GameTexts.FindText("str_string_newline_string", null).ToString();
				if (!hasRequiredPerk)
				{
					GameTexts.SetVariable("STR1", text);
					TextObject textObject5 = new TextObject("{=68IlDbA2}You need to have {PERK_NAME} perk to upgrade a bandit troop to a normal troop.", null);
					textObject5.SetTextVariable("PERK_NAME", requiredPerk.Name);
					GameTexts.SetVariable("STR2", textObject5);
					text = GameTexts.FindText("str_string_newline_string", null).ToString();
				}
				GameTexts.SetVariable("STR2", "");
				if (availableUpgrades > 0 && !string.IsNullOrEmpty(entireStackShortcutKeyText))
				{
					GameTexts.SetVariable("KEY_NAME", entireStackShortcutKeyText);
					string content2 = GameTexts.FindText("str_entire_stack_shortcut_upgrade_units", null).ToString();
					GameTexts.SetVariable("STR1", content2);
					GameTexts.SetVariable("STR2", "");
					if (availableUpgrades >= 5 && !string.IsNullOrEmpty(fiveStackShortcutKeyText))
					{
						GameTexts.SetVariable("KEY_NAME", fiveStackShortcutKeyText);
						string content3 = GameTexts.FindText("str_five_stack_shortcut_upgrade_units", null).ToString();
						GameTexts.SetVariable("STR2", content3);
					}
					string content4 = GameTexts.FindText("str_string_newline_string", null).ToString();
					GameTexts.SetVariable("STR2", content4);
				}
				GameTexts.SetVariable("STR1", text);
				text = GameTexts.FindText("str_string_newline_string", null).ToString();
			}
			return text;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00008854 File Offset: 0x00006A54
		public static string ConvertToHexColor(uint color)
		{
			uint num = color % 4278190080U;
			return "#" + Convert.ToString((long)((ulong)num), 16).PadLeft(6, '0').ToUpper() + "FF";
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00008890 File Offset: 0x00006A90
		public static bool GetMapScreenActionIsEnabledWithReason(out TextObject disabledReason)
		{
			if (Hero.MainHero.IsPrisoner)
			{
				disabledReason = GameTexts.FindText("str_action_disabled_reason_prisoner", null);
				return false;
			}
			GameStateManager gameStateManager = GameStateManager.Current;
			bool flag;
			if (gameStateManager == null)
			{
				flag = false;
			}
			else
			{
				flag = gameStateManager.GameStates.Any((GameState x) => x.IsMission);
			}
			if (flag)
			{
				disabledReason = new TextObject("{=FdzsOvDq}This action is disabled while in a mission", null);
				return false;
			}
			if (PlayerEncounter.Current != null)
			{
				if (PlayerEncounter.EncounterSettlement == null)
				{
					disabledReason = GameTexts.FindText("str_action_disabled_reason_encounter", null);
					return false;
				}
				Village village = PlayerEncounter.EncounterSettlement.Village;
				if (village != null && village.VillageState == Village.VillageStates.BeingRaided)
				{
					MapEvent mapEvent = MobileParty.MainParty.MapEvent;
					if (mapEvent != null && mapEvent.IsRaid)
					{
						disabledReason = GameTexts.FindText("str_action_disabled_reason_raid", null);
						return false;
					}
				}
				if (PlayerEncounter.EncounterSettlement.IsUnderSiege)
				{
					disabledReason = GameTexts.FindText("str_action_disabled_reason_siege", null);
					return false;
				}
			}
			if (PlayerSiege.PlayerSiegeEvent != null)
			{
				disabledReason = GameTexts.FindText("str_action_disabled_reason_siege", null);
				return false;
			}
			if (MobileParty.MainParty.MapEvent != null)
			{
				disabledReason = new TextObject("{=MIylzRc5}You can't perform this action while you are in a map event.", null);
				return false;
			}
			disabledReason = TextObject.Empty;
			return true;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x000089B4 File Offset: 0x00006BB4
		public static string GetClanSupportDisableReasonString(bool hasEnoughInfluence, bool isTargetMainClan, bool isMainClanMercenary)
		{
			if (Hero.MainHero.IsPrisoner)
			{
				return GameTexts.FindText("str_action_disabled_reason_prisoner", null).ToString();
			}
			if (isTargetMainClan)
			{
				return GameTexts.FindText("str_cannot_support_your_clan", null).ToString();
			}
			if (isMainClanMercenary)
			{
				return GameTexts.FindText("str_mercenaries_cannot_support_clans", null).ToString();
			}
			if (!hasEnoughInfluence)
			{
				return GameTexts.FindText("str_warning_you_dont_have_enough_influence", null).ToString();
			}
			return null;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00008A1C File Offset: 0x00006C1C
		public static string GetClanExpelDisableReasonString(bool hasEnoughInfluence, bool isTargetMainClan, bool isTargetRulingClan, bool isMainClanMercenary)
		{
			if (Hero.MainHero.IsPrisoner)
			{
				return GameTexts.FindText("str_action_disabled_reason_prisoner", null).ToString();
			}
			if (isMainClanMercenary)
			{
				return GameTexts.FindText("str_mercenaries_cannot_expel_clans", null).ToString();
			}
			if (isTargetMainClan)
			{
				return GameTexts.FindText("str_cannot_expel_your_clan", null).ToString();
			}
			if (isTargetRulingClan)
			{
				return GameTexts.FindText("str_cannot_expel_ruling_clan", null).ToString();
			}
			if (!hasEnoughInfluence)
			{
				return GameTexts.FindText("str_warning_you_dont_have_enough_influence", null).ToString();
			}
			return null;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00008A98 File Offset: 0x00006C98
		public static string GetArmyDisbandDisableReasonString(bool hasEnoughInfluence, bool isArmyInAnyEvent, bool isPlayerClanMercenary, bool isPlayerInThisArmy)
		{
			if (Hero.MainHero.IsPrisoner)
			{
				return GameTexts.FindText("str_action_disabled_reason_prisoner", null).ToString();
			}
			if (isPlayerClanMercenary)
			{
				return GameTexts.FindText("str_cannot_disband_army_while_mercenary", null).ToString();
			}
			if (isArmyInAnyEvent)
			{
				return GameTexts.FindText("str_cannot_disband_army_while_in_event", null).ToString();
			}
			if (isPlayerInThisArmy)
			{
				return GameTexts.FindText("str_cannot_disband_army_while_in_that_army", null).ToString();
			}
			if (!hasEnoughInfluence)
			{
				return GameTexts.FindText("str_warning_you_dont_have_enough_influence", null).ToString();
			}
			return null;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00008B13 File Offset: 0x00006D13
		public static TextObject GetCreateNewPartyReasonString(bool haveEmptyPartySlots, bool haveAvailableHero)
		{
			if (Hero.MainHero.IsPrisoner)
			{
				return GameTexts.FindText("str_action_disabled_reason_prisoner", null);
			}
			if (!haveEmptyPartySlots)
			{
				return GameTexts.FindText("str_clan_doesnt_have_empty_party_slots", null);
			}
			if (!haveAvailableHero)
			{
				return GameTexts.FindText("str_clan_doesnt_have_available_heroes", null);
			}
			return TextObject.Empty;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00008B50 File Offset: 0x00006D50
		public static string GetCraftingDisableReasonString(bool playerHasEnoughMaterials)
		{
			if (!playerHasEnoughMaterials)
			{
				return GameTexts.FindText("str_warning_crafing_materials", null).ToString();
			}
			return string.Empty;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00008B6C File Offset: 0x00006D6C
		public static string GetAddFocusHintString(bool playerHasEnoughPoints, bool isMaxedSkill, int currentFocusAmount, int currentAttributeAmount, int currentSkillValue, IHeroDeveloper developer, SkillObject skill)
		{
			GameTexts.SetVariable("newline", "\n");
			string content = GameTexts.FindText("str_focus_points", null).ToString();
			TextObject textObject = new TextObject("{=j3iwQmoA}Current focus amount: {CURRENT_AMOUNT}", null);
			textObject.SetTextVariable("CURRENT_AMOUNT", currentFocusAmount);
			GameTexts.SetVariable("STR1", content);
			GameTexts.SetVariable("STR2", textObject);
			content = GameTexts.FindText("str_string_newline_string", null).ToString();
			if (!playerHasEnoughPoints)
			{
				GameTexts.SetVariable("STR1", content);
				GameTexts.SetVariable("STR2", GameTexts.FindText("str_player_doesnt_have_enough_points", null));
				return GameTexts.FindText("str_string_newline_string", null).ToString();
			}
			if (isMaxedSkill)
			{
				GameTexts.SetVariable("STR1", content);
				GameTexts.SetVariable("STR2", GameTexts.FindText("str_player_cannot_give_more_points", null));
				return GameTexts.FindText("str_string_newline_string", null).ToString();
			}
			GameTexts.SetVariable("COST", 1);
			GameTexts.SetVariable("STR1", content);
			GameTexts.SetVariable("STR2", GameTexts.FindText("str_cost_COUNT", null));
			return GameTexts.FindText("str_string_newline_string", null).ToString();
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00008C7C File Offset: 0x00006E7C
		public static string GetSkillEffectText(SkillEffect effect, int skillLevel)
		{
			MBTextManager.SetTextVariable("a0", effect.GetPrimaryValue(skillLevel).ToString("0.0"), false);
			MBTextManager.SetTextVariable("a1", effect.GetSecondaryValue(skillLevel).ToString("0.0"), false);
			string text = effect.Description.ToString();
			if (effect.PrimaryRole == SkillEffect.PerkRole.None || effect.PrimaryBonus == 0f)
			{
				return text;
			}
			TextObject textObject = GameTexts.FindText("role", effect.PrimaryRole.ToString());
			if (effect.SecondaryRole != SkillEffect.PerkRole.None && effect.SecondaryBonus != 0f)
			{
				TextObject textObject2 = GameTexts.FindText("role", effect.SecondaryRole.ToString());
				return string.Format("({0} / {1}){2} ", textObject.ToString(), textObject2.ToString(), text);
			}
			return string.Format("({0}) {1} ", textObject.ToString(), text);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00008D70 File Offset: 0x00006F70
		public static string GetMobilePartyBehaviorText(MobileParty party)
		{
			TextObject textObject;
			if (party.Army != null && party.Army.LeaderParty == party && !party.Ai.IsFleeing())
			{
				textObject = party.Army.GetBehaviorText(false);
			}
			else if (party.DefaultBehavior == AiBehavior.Hold || party.ShortTermBehavior == AiBehavior.Hold || (party.IsMainParty && Campaign.Current.IsMainPartyWaiting))
			{
				textObject = new TextObject("{=RClxLG6N}Holding", null);
			}
			else if (party.ShortTermBehavior == AiBehavior.EngageParty && party.ShortTermTargetParty != null)
			{
				textObject = new TextObject("{=5bzk75Ql}Engaging {TARGET_PARTY}.", null);
				textObject.SetTextVariable("TARGET_PARTY", party.ShortTermTargetParty.Name);
			}
			else if (party.DefaultBehavior == AiBehavior.GoAroundParty && party.ShortTermBehavior == AiBehavior.GoToPoint)
			{
				textObject = new TextObject("{=XYAVu2f0}Chasing {TARGET_PARTY}.", null);
				textObject.SetTextVariable("TARGET_PARTY", party.TargetParty.Name);
			}
			else if (party.ShortTermBehavior == AiBehavior.FleeToParty && party.ShortTermTargetParty != null)
			{
				textObject = new TextObject("{=R8vuwKaf}Running from {TARGET_PARTY} to ally party.", null);
				textObject.SetTextVariable("TARGET_PARTY", party.ShortTermTargetParty.Name);
			}
			else if (party.ShortTermBehavior == AiBehavior.FleeToPoint && party.ShortTermTargetParty != null)
			{
				textObject = new TextObject("{=AcMayd1p}Running from {TARGET_PARTY}", null);
				textObject.SetTextVariable("TARGET_PARTY", party.ShortTermTargetParty.Name);
			}
			else if (party.ShortTermBehavior == AiBehavior.FleeToGate && party.ShortTermTargetParty != null)
			{
				textObject = new TextObject("{=J5u0uOKc}Running from {TARGET_PARTY} to settlement.", null);
				textObject.SetTextVariable("TARGET_PARTY", party.ShortTermTargetParty.Name);
			}
			else if (party.DefaultBehavior == AiBehavior.DefendSettlement)
			{
				if (party.ShortTermBehavior == AiBehavior.EscortParty)
				{
					textObject = new TextObject("{=yD7rL5Nc}Helping ally party to defend {TARGET_SETTLEMENT}.", null);
				}
				else
				{
					textObject = new TextObject("{=rGy8vjOv}Defending {TARGET_SETTLEMENT}.", null);
				}
				textObject.SetTextVariable("TARGET_SETTLEMENT", party.TargetSettlement.Name);
			}
			else if (party.DefaultBehavior == AiBehavior.RaidSettlement)
			{
				textObject = new TextObject("{=VtWa9Pmh}Raiding {TARGET_SETTLEMENT}.", null);
				textObject.SetTextVariable("TARGET_SETTLEMENT", party.TargetSettlement.Name);
			}
			else if (party.DefaultBehavior == AiBehavior.BesiegeSettlement)
			{
				textObject = new TextObject("{=JTxI3sW2}Besieging {TARGET_SETTLEMENT}", null);
				textObject.SetTextVariable("TARGET_SETTLEMENT", party.TargetSettlement.Name);
			}
			else if (party.ShortTermBehavior == AiBehavior.GoToPoint)
			{
				if (party.ShortTermTargetParty != null)
				{
					textObject = new TextObject("{=AcMayd1p}Running from {TARGET_PARTY}", null);
					textObject.SetTextVariable("TARGET_PARTY", party.ShortTermTargetParty.Name);
				}
				else if (party.TargetSettlement != null)
				{
					if (party.DefaultBehavior == AiBehavior.PatrolAroundPoint)
					{
						if (party.TargetSettlement.GatePosition.Distance(party.Position2D) > Campaign.AverageDistanceBetweenTwoFortifications)
						{
							textObject = new TextObject("{=rba7kgwS}Travelling to {TARGET_SETTLEMENT}.", null);
						}
						else
						{
							textObject = new TextObject("{=yUVv3z5V}Patrolling around {TARGET_SETTLEMENT}.", null);
						}
						textObject.SetTextVariable("TARGET_SETTLEMENT", (party.TargetSettlement != null) ? party.TargetSettlement.Name : party.HomeSettlement.Name);
					}
					else
					{
						textObject = new TextObject("{=TaK6ydAx}Travelling.", null);
					}
				}
				else if (party.DefaultBehavior == AiBehavior.PatrolAroundPoint)
				{
					textObject = new TextObject("{=BifGz0h4}Patrolling", null);
				}
				else
				{
					textObject = new TextObject("{=XAL3t1bs}Going to a point", null);
				}
			}
			else if (party.ShortTermBehavior == AiBehavior.GoToSettlement || party.DefaultBehavior == AiBehavior.GoToSettlement)
			{
				if (party.ShortTermBehavior == AiBehavior.GoToSettlement && party.ShortTermTargetSettlement != null && party.ShortTermTargetSettlement != party.TargetSettlement)
				{
					textObject = new TextObject("{=NRpbagbZ}Running to {TARGET_PARTY}", null);
					textObject.SetTextVariable("TARGET_PARTY", party.ShortTermTargetSettlement.Name);
				}
				else if (party.DefaultBehavior == AiBehavior.GoToSettlement && party.TargetSettlement != null)
				{
					if (party.CurrentSettlement == party.TargetSettlement)
					{
						textObject = new TextObject("{=Y65gdbrx}Waiting in {TARGET_PARTY}", null);
					}
					else
					{
						textObject = new TextObject("{=EQHq3bHM}Travelling to {TARGET_PARTY}", null);
					}
					textObject.SetTextVariable("TARGET_PARTY", party.TargetSettlement.Name);
				}
				else if (party.ShortTermTargetParty != null)
				{
					textObject = new TextObject("{=AcMayd1p}Running from {TARGET_PARTY}", null);
					textObject.SetTextVariable("TARGET_PARTY", party.ShortTermTargetParty.Name);
				}
				else
				{
					textObject = new TextObject("{=QGyoSLeY}Traveling to a settlement", null);
				}
			}
			else if (party.ShortTermBehavior == AiBehavior.AssaultSettlement)
			{
				textObject = new TextObject("{=exnL6SS7}Attacking {TARGET_SETTLEMENT}", null);
				textObject.SetTextVariable("TARGET_SETTLEMENT", party.ShortTermTargetSettlement.Name);
			}
			else if (party.DefaultBehavior == AiBehavior.EscortParty || party.ShortTermBehavior == AiBehavior.EscortParty)
			{
				textObject = new TextObject("{=OpzzCPiP}Following {TARGET_PARTY}", null);
				textObject.SetTextVariable("TARGET_PARTY", (party.ShortTermTargetParty != null) ? party.ShortTermTargetParty.Name : party.TargetParty.Name);
			}
			else
			{
				textObject = new TextObject("{=QXBf26Rv}Unknown Behavior", null);
			}
			return textObject.ToString();
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00009230 File Offset: 0x00007430
		public static string GetHeroBehaviorText(Hero hero, ITeleportationCampaignBehavior teleportationBehavior = null)
		{
			if (hero.CurrentSettlement != null)
			{
				GameTexts.SetVariable("SETTLEMENT_NAME", hero.CurrentSettlement.Name);
			}
			if (hero.IsPrisoner)
			{
				if (hero.CurrentSettlement != null)
				{
					return GameTexts.FindText("str_prisoner_at_settlement", null).ToString();
				}
				if (hero.PartyBelongedToAsPrisoner != null)
				{
					CampaignUIHelper._prisonerOfText.SetTextVariable("PARTY_NAME", hero.PartyBelongedToAsPrisoner.Name);
					return CampaignUIHelper._prisonerOfText.ToString();
				}
				return new TextObject("{=tYz4D8Or}Prisoner", null).ToString();
			}
			else
			{
				if (hero.IsTraveling)
				{
					IMapPoint mapPoint = null;
					bool flag = false;
					bool flag2 = false;
					if (teleportationBehavior == null || !teleportationBehavior.GetTargetOfTeleportingHero(hero, out flag, out flag2, out mapPoint))
					{
						return CampaignUIHelper._travelingText.ToString();
					}
					Settlement settlement;
					if (flag && (settlement = (mapPoint as Settlement)) != null)
					{
						TextObject textObject = new TextObject("{=gUUnZNGk}Moving to {SETTLEMENT_NAME} to be the new governor", null);
						textObject.SetTextVariable("SETTLEMENT_NAME", settlement.Name.ToString());
						return textObject.ToString();
					}
					if (flag2 && mapPoint is MobileParty)
					{
						return new TextObject("{=g08mptth}Moving to a party to be the new leader", null).ToString();
					}
					MobileParty mobileParty;
					if ((mobileParty = (mapPoint as MobileParty)) != null)
					{
						TextObject textObject2 = new TextObject("{=qaQqAYGc}Moving to {LEADER.NAME}{.o} Party", null);
						bool flag3;
						if (mobileParty == null)
						{
							flag3 = (null != null);
						}
						else
						{
							Hero leaderHero = mobileParty.LeaderHero;
							flag3 = (((leaderHero != null) ? leaderHero.CharacterObject : null) != null);
						}
						if (flag3)
						{
							StringHelpers.SetCharacterProperties("LEADER", mobileParty.LeaderHero.CharacterObject, textObject2, false);
						}
						return textObject2.ToString();
					}
					Settlement settlement2;
					if ((settlement2 = (mapPoint as Settlement)) != null)
					{
						TextObject textObject3 = new TextObject("{=UUaW0dba}Moving to {SETTLEMENT_NAME}", null);
						string tag = "SETTLEMENT_NAME";
						string text;
						if (settlement2 == null)
						{
							text = null;
						}
						else
						{
							TextObject name = settlement2.Name;
							text = ((name != null) ? name.ToString() : null);
						}
						textObject3.SetTextVariable(tag, text ?? string.Empty);
						return textObject3.ToString();
					}
				}
				if (hero.PartyBelongedTo != null)
				{
					if (hero.PartyBelongedTo.LeaderHero == hero && hero.PartyBelongedTo.Army != null)
					{
						CampaignUIHelper._attachedToText.SetTextVariable("PARTY_NAME", hero.PartyBelongedTo.Army.Name);
						return CampaignUIHelper._attachedToText.ToString();
					}
					if (hero.PartyBelongedTo == MobileParty.MainParty)
					{
						return CampaignUIHelper._inYourPartyText.ToString();
					}
					Settlement closestSettlementForNavigationMesh = Campaign.Current.Models.MapDistanceModel.GetClosestSettlementForNavigationMesh(hero.PartyBelongedTo.CurrentNavigationFace);
					CampaignUIHelper._nearSettlementText.SetTextVariable("SETTLEMENT_NAME", closestSettlementForNavigationMesh.Name);
					return CampaignUIHelper._nearSettlementText.ToString();
				}
				else if (hero.CurrentSettlement != null)
				{
					if (hero.CurrentSettlement.Town != null && hero.GovernorOf == hero.CurrentSettlement.Town)
					{
						return GameTexts.FindText("str_governing_at_settlement", null).ToString();
					}
					if (Campaign.Current.GetCampaignBehavior<IAlleyCampaignBehavior>().IsHeroAlleyLeaderOfAnyPlayerAlley(hero))
					{
						return GameTexts.FindText("str_alley_leader_at_settlement", null).ToString();
					}
					return GameTexts.FindText("str_staying_at_settlement", null).ToString();
				}
				else
				{
					if (Campaign.Current.IssueManager.IssueSolvingCompanionList.Contains(hero))
					{
						return GameTexts.FindText("str_solving_issue", null).ToString();
					}
					if (hero.IsFugitive)
					{
						return CampaignUIHelper._regroupingText.ToString();
					}
					if (hero.IsReleased)
					{
						GameTexts.SetVariable("LEFT", CampaignUIHelper._recoveringText);
						GameTexts.SetVariable("RIGHT", CampaignUIHelper._recentlyReleasedText);
						return GameTexts.FindText("str_LEFT_comma_RIGHT", null).ToString();
					}
					return new TextObject("{=RClxLG6N}Holding", null).ToString();
				}
			}
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0000957C File Offset: 0x0000777C
		public static Hero GetTeleportingLeaderHero(MobileParty party, ITeleportationCampaignBehavior teleportationBehavior)
		{
			if (party != null && teleportationBehavior != null)
			{
				foreach (Hero hero in from x in Hero.MainHero.Clan.Heroes
				where x.IsAlive && x.IsTraveling
				select x)
				{
					bool flag;
					bool flag2;
					IMapPoint mapPoint;
					MobileParty mobileParty;
					if (teleportationBehavior.GetTargetOfTeleportingHero(hero, out flag, out flag2, out mapPoint) && flag2 && (mobileParty = (mapPoint as MobileParty)) != null && mobileParty == party)
					{
						return hero;
					}
				}
			}
			return null;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00009624 File Offset: 0x00007824
		public static Hero GetTeleportingGovernor(Settlement settlement, ITeleportationCampaignBehavior teleportationBehavior)
		{
			if (settlement != null && teleportationBehavior != null)
			{
				foreach (Hero hero in from x in Hero.MainHero.Clan.Heroes
				where x.IsAlive && x.IsTraveling
				select x)
				{
					bool flag;
					bool flag2;
					IMapPoint mapPoint;
					Settlement settlement2;
					if (teleportationBehavior.GetTargetOfTeleportingHero(hero, out flag, out flag2, out mapPoint) && flag && (settlement2 = (mapPoint as Settlement)) != null && settlement2 == settlement)
					{
						return hero;
					}
				}
			}
			return null;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x000096CC File Offset: 0x000078CC
		public static TextObject GetHeroRelationToHeroText(Hero queriedHero, Hero baseHero, bool uppercaseFirst)
		{
			GameTexts.SetVariable("RELATION_TEXT", ConversationHelper.GetHeroRelationToHeroTextShort(queriedHero, baseHero, uppercaseFirst));
			StringHelpers.SetCharacterProperties("BASE_HERO", baseHero.CharacterObject, null, false);
			return GameTexts.FindText("str_hero_family_relation", null);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00009700 File Offset: 0x00007900
		public static string GetAbbreviatedValueTextFromValue(int valueAmount)
		{
			string variable = "";
			decimal num = valueAmount;
			if (valueAmount < 10000)
			{
				return valueAmount.ToString();
			}
			if (valueAmount >= 10000 && valueAmount < 1000000)
			{
				variable = new TextObject("{=thousandabbr}k", null).ToString();
				num /= 1000m;
			}
			else if (valueAmount >= 1000000 && valueAmount < 1000000000)
			{
				variable = new TextObject("{=millionabbr}m", null).ToString();
				num /= 1000000m;
			}
			else if (valueAmount >= 1000000000 && valueAmount <= 2147483647)
			{
				variable = new TextObject("{=billionabbr}b", null).ToString();
				num /= 1000000000m;
			}
			int num2 = (int)num;
			string text = num2.ToString();
			if (text.Length < 3)
			{
				text += ".";
				string text2 = num.ToString("F3").Split(new char[]
				{
					'.'
				}).ElementAtOrDefault(1);
				if (text2 != null)
				{
					for (int i = 0; i < 3 - num2.ToString().Length; i++)
					{
						if (text2.ElementAtOrDefault(i) != '\0')
						{
							text += text2.ElementAtOrDefault(i).ToString();
						}
					}
				}
			}
			CampaignUIHelper._denarValueInfoText.SetTextVariable("DENAR_AMOUNT", text);
			CampaignUIHelper._denarValueInfoText.SetTextVariable("VALUE_ABBREVIATION", variable);
			return CampaignUIHelper._denarValueInfoText.ToString();
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00009880 File Offset: 0x00007A80
		public static string GetPartyDistanceByTimeText(float distance, float speed)
		{
			int num = MathF.Ceiling(distance / speed);
			int num2 = num / 24;
			num %= 24;
			GameTexts.SetVariable("IS_UNDER_A_DAY", (num2 <= 0) ? 1 : 0);
			GameTexts.SetVariable("IS_MORE_THAN_ONE_DAY", (num2 > 1) ? 1 : 0);
			GameTexts.SetVariable("DAY_VALUE", num2);
			GameTexts.SetVariable("IS_UNDER_ONE_HOUR", (num <= 0) ? 1 : 0);
			GameTexts.SetVariable("IS_MORE_THAN_AN_HOUR", (num > 1) ? 1 : 0);
			GameTexts.SetVariable("HOUR_VALUE", num);
			return GameTexts.FindText("str_distance_by_time", null).ToString();
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00009910 File Offset: 0x00007B10
		public static CharacterCode GetCharacterCode(CharacterObject character, bool useCivilian = false)
		{
			TextObject textObject;
			if (character == null || (character.IsHero && CampaignUIHelper.IsHeroInformationHidden(character.HeroObject, out textObject)))
			{
				return CharacterCode.CreateEmpty();
			}
			Hero heroObject = character.HeroObject;
			uint? num;
			if (heroObject == null)
			{
				num = null;
			}
			else
			{
				IFaction mapFaction = heroObject.MapFaction;
				num = ((mapFaction != null) ? new uint?(mapFaction.Color) : null);
			}
			uint color = num ?? ((character.Culture != null) ? character.Culture.Color : Color.White.ToUnsignedInteger());
			Hero heroObject2 = character.HeroObject;
			uint? num2;
			if (heroObject2 == null)
			{
				num2 = null;
			}
			else
			{
				IFaction mapFaction2 = heroObject2.MapFaction;
				num2 = ((mapFaction2 != null) ? new uint?(mapFaction2.Color2) : null);
			}
			uint color2 = num2 ?? ((character.Culture != null) ? character.Culture.Color2 : Color.White.ToUnsignedInteger());
			string equipmentCode = string.Empty;
			BodyProperties bodyProperties = character.GetBodyProperties(character.Equipment, -1);
			bool flag;
			if (!useCivilian)
			{
				Hero heroObject3 = character.HeroObject;
				flag = (heroObject3 != null && heroObject3.IsNoncombatant);
			}
			else
			{
				flag = true;
			}
			useCivilian = flag;
			if (character.IsHero && character.HeroObject.IsLord)
			{
				Equipment equipment = (useCivilian && character.FirstCivilianEquipment != null) ? character.FirstCivilianEquipment.Clone(false) : character.Equipment.Clone(false);
				equipment[EquipmentIndex.NumAllWeaponSlots] = new EquipmentElement(null, null, null, false);
				for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.NumAllWeaponSlots; equipmentIndex++)
				{
					ItemObject item = equipment[equipmentIndex].Item;
					bool flag2;
					if (item == null)
					{
						flag2 = false;
					}
					else
					{
						WeaponComponent weaponComponent = item.WeaponComponent;
						bool? flag3;
						if (weaponComponent == null)
						{
							flag3 = null;
						}
						else
						{
							WeaponComponentData primaryWeapon = weaponComponent.PrimaryWeapon;
							flag3 = ((primaryWeapon != null) ? new bool?(primaryWeapon.IsShield) : null);
						}
						bool? flag4 = flag3;
						bool flag5 = true;
						flag2 = (flag4.GetValueOrDefault() == flag5 & flag4 != null);
					}
					if (flag2)
					{
						equipment.AddEquipmentToSlotWithoutAgent(equipmentIndex, default(EquipmentElement));
					}
				}
				equipmentCode = equipment.CalculateEquipmentCode();
			}
			else
			{
				equipmentCode = ((useCivilian && character.FirstCivilianEquipment != null) ? character.FirstCivilianEquipment.Clone(false) : character.FirstBattleEquipment.Clone(false)).CalculateEquipmentCode();
			}
			return CharacterCode.CreateFrom(equipmentCode, bodyProperties, character.IsFemale, character.IsHero, color, color2, character.DefaultFormationClass, character.Race);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00009B80 File Offset: 0x00007D80
		public static string GetTraitNameText(TraitObject traitObject, Hero hero)
		{
			if (traitObject != DefaultTraits.Mercy && traitObject != DefaultTraits.Valor && traitObject != DefaultTraits.Honor && traitObject != DefaultTraits.Generosity && traitObject != DefaultTraits.Calculating)
			{
				Debug.FailedAssert("Cannot show this trait as text.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem.ViewModelCollection\\CampaignUIHelper.cs", "GetTraitNameText", 3056);
				return "";
			}
			int traitLevel = hero.GetTraitLevel(traitObject);
			if (traitLevel != 0)
			{
				return GameTexts.FindText("str_trait_name_" + traitObject.StringId.ToLower(), (traitLevel + MathF.Abs(traitObject.MinValue)).ToString()).ToString();
			}
			return "";
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00009C1C File Offset: 0x00007E1C
		public static string GetTraitTooltipText(TraitObject traitObject, int traitValue)
		{
			if (traitObject != DefaultTraits.Mercy && traitObject != DefaultTraits.Valor && traitObject != DefaultTraits.Honor && traitObject != DefaultTraits.Generosity && traitObject != DefaultTraits.Calculating)
			{
				Debug.FailedAssert("Cannot show this trait's tooltip.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem.ViewModelCollection\\CampaignUIHelper.cs", "GetTraitTooltipText", 3081);
				return null;
			}
			GameTexts.SetVariable("NEWLINE", "\n");
			if (traitValue != 0)
			{
				TextObject content = GameTexts.FindText("str_trait_name_" + traitObject.StringId.ToLower(), (traitValue + MathF.Abs(traitObject.MinValue)).ToString());
				GameTexts.SetVariable("TRAIT_VALUE", traitValue);
				GameTexts.SetVariable("TRAIT_NAME", content);
				TextObject content2 = GameTexts.FindText("str_trait", traitObject.StringId.ToLower());
				GameTexts.SetVariable("TRAIT", content2);
				GameTexts.SetVariable("TRAIT_DESCRIPTION", traitObject.Description);
				return GameTexts.FindText("str_trait_tooltip", null).ToString();
			}
			TextObject content3 = GameTexts.FindText("str_trait", traitObject.StringId.ToLower());
			GameTexts.SetVariable("TRAIT", content3);
			GameTexts.SetVariable("TRAIT_DESCRIPTION", traitObject.Description);
			return GameTexts.FindText("str_trait_description_tooltip", null).ToString();
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00009D4C File Offset: 0x00007F4C
		public static string GetTextForRole(SkillEffect.PerkRole role)
		{
			switch (role)
			{
			case SkillEffect.PerkRole.None:
				return new TextObject("{=koX9okuG}None", null).ToString();
			case SkillEffect.PerkRole.Ruler:
				return new TextObject("{=IcgVKFxZ}Ruler", null).ToString();
			case SkillEffect.PerkRole.ClanLeader:
				return new TextObject("{=pqfz386V}Clan Leader", null).ToString();
			case SkillEffect.PerkRole.Governor:
				return new TextObject("{=Fa2nKXxI}Governor", null).ToString();
			case SkillEffect.PerkRole.ArmyCommander:
				return new TextObject("{=g9VIbA9s}Sergeant", null).ToString();
			case SkillEffect.PerkRole.PartyLeader:
				return new TextObject("{=ggpRTQQl}Party Leader", null).ToString();
			case SkillEffect.PerkRole.PartyOwner:
				return new TextObject("{=YifFZaG7}Party Owner", null).ToString();
			case SkillEffect.PerkRole.Surgeon:
				return new TextObject("{=QBPrRdQJ}Surgeon", null).ToString();
			case SkillEffect.PerkRole.Engineer:
				return new TextObject("{=7h6cXdW7}Engineer", null).ToString();
			case SkillEffect.PerkRole.Scout:
				return new TextObject("{=92M0Pb5T}Scout", null).ToString();
			case SkillEffect.PerkRole.Quartermaster:
				return new TextObject("{=redwEIlW}Quartermaster", null).ToString();
			case SkillEffect.PerkRole.PartyMember:
				return new TextObject("{=HcAV8x7p}Party Member", null).ToString();
			case SkillEffect.PerkRole.Personal:
				return new TextObject("{=UxAl9iyi}Personal", null).ToString();
			default:
				return "";
			}
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00009E7C File Offset: 0x0000807C
		public static int GetHeroCompareSortIndex(Hero x, Hero y)
		{
			int num;
			if (x.Clan == null && y.Clan == null)
			{
				num = 0;
			}
			else if (x.Clan == null)
			{
				num = 1;
			}
			else if (y.Clan == null)
			{
				num = -1;
			}
			else if (x.IsLord && !y.IsLord)
			{
				num = -1;
			}
			else if (!x.IsLord && y.IsLord)
			{
				num = 1;
			}
			else
			{
				num = -x.Clan.Renown.CompareTo(y.Clan.Renown);
			}
			if (num != 0)
			{
				return num;
			}
			int num2 = x.IsGangLeader.CompareTo(y.IsGangLeader);
			if (num2 != 0)
			{
				return num2;
			}
			num2 = y.Power.CompareTo(x.Power);
			if (num2 == 0)
			{
				return x.Name.ToString().CompareTo(y.Name.ToString());
			}
			return num2;
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00009F54 File Offset: 0x00008154
		public static string GetHeroClanRoleText(Hero hero, Clan clan)
		{
			return GameTexts.FindText("role", MobileParty.MainParty.GetHeroPerkRole(hero).ToString()).ToString();
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00009F8C File Offset: 0x0000818C
		public static int GetItemObjectTypeSortIndex(ItemObject item)
		{
			if (item == null)
			{
				return -1;
			}
			int num = CampaignUIHelper._itemObjectTypeSortIndices.IndexOf(item.Type) * 100;
			switch (item.Type)
			{
			case ItemObject.ItemTypeEnum.Invalid:
			case ItemObject.ItemTypeEnum.HeadArmor:
			case ItemObject.ItemTypeEnum.BodyArmor:
			case ItemObject.ItemTypeEnum.LegArmor:
			case ItemObject.ItemTypeEnum.HandArmor:
			case ItemObject.ItemTypeEnum.Animal:
			case ItemObject.ItemTypeEnum.Book:
			case ItemObject.ItemTypeEnum.ChestArmor:
			case ItemObject.ItemTypeEnum.Cape:
			case ItemObject.ItemTypeEnum.HorseHarness:
			case ItemObject.ItemTypeEnum.Banner:
				return num;
			case ItemObject.ItemTypeEnum.Horse:
				if (!item.HorseComponent.IsRideable)
				{
					return num;
				}
				return num + 1;
			case ItemObject.ItemTypeEnum.OneHandedWeapon:
			case ItemObject.ItemTypeEnum.TwoHandedWeapon:
			case ItemObject.ItemTypeEnum.Polearm:
			case ItemObject.ItemTypeEnum.Arrows:
			case ItemObject.ItemTypeEnum.Bolts:
			case ItemObject.ItemTypeEnum.Shield:
			case ItemObject.ItemTypeEnum.Bow:
			case ItemObject.ItemTypeEnum.Crossbow:
			case ItemObject.ItemTypeEnum.Thrown:
			case ItemObject.ItemTypeEnum.Pistol:
			case ItemObject.ItemTypeEnum.Musket:
			case ItemObject.ItemTypeEnum.Bullets:
				return (int)(num + item.PrimaryWeapon.WeaponClass);
			case ItemObject.ItemTypeEnum.Goods:
				if (!item.IsFood)
				{
					return num + 1;
				}
				return num;
			default:
				return 1;
			}
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0000A057 File Offset: 0x00008257
		public static string GetItemLockStringID(EquipmentElement equipmentElement)
		{
			return equipmentElement.Item.StringId + ((equipmentElement.ItemModifier != null) ? equipmentElement.ItemModifier.StringId : "");
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000A086 File Offset: 0x00008286
		public static string GetTroopLockStringID(TroopRosterElement rosterElement)
		{
			return rosterElement.Character.StringId;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000A094 File Offset: 0x00008294
		public static List<ValueTuple<CampaignUIHelper.IssueQuestFlags, TextObject, TextObject>> GetQuestStateOfHero(Hero queriedHero)
		{
			List<ValueTuple<CampaignUIHelper.IssueQuestFlags, TextObject, TextObject>> list = new List<ValueTuple<CampaignUIHelper.IssueQuestFlags, TextObject, TextObject>>();
			if (Campaign.Current != null)
			{
				IssueBase relatedIssue;
				Campaign.Current.IssueManager.Issues.TryGetValue(queriedHero, out relatedIssue);
				if (relatedIssue == null)
				{
					relatedIssue = queriedHero.Issue;
				}
				List<QuestBase> questsRelatedToHero = CampaignUIHelper.GetQuestsRelatedToHero(queriedHero);
				if (questsRelatedToHero.Count > 0)
				{
					for (int i = 0; i < questsRelatedToHero.Count; i++)
					{
						if (questsRelatedToHero[i].QuestGiver == queriedHero)
						{
							list.Add(new ValueTuple<CampaignUIHelper.IssueQuestFlags, TextObject, TextObject>(questsRelatedToHero[i].IsSpecialQuest ? CampaignUIHelper.IssueQuestFlags.ActiveStoryQuest : CampaignUIHelper.IssueQuestFlags.ActiveIssue, questsRelatedToHero[i].Title, (questsRelatedToHero[i].JournalEntries.Count > 0) ? questsRelatedToHero[i].JournalEntries[0].LogText : TextObject.Empty));
						}
						else
						{
							list.Add(new ValueTuple<CampaignUIHelper.IssueQuestFlags, TextObject, TextObject>(questsRelatedToHero[i].IsSpecialQuest ? CampaignUIHelper.IssueQuestFlags.TrackedStoryQuest : CampaignUIHelper.IssueQuestFlags.TrackedIssue, questsRelatedToHero[i].Title, (questsRelatedToHero[i].JournalEntries.Count > 0) ? questsRelatedToHero[i].JournalEntries[0].LogText : TextObject.Empty));
						}
					}
				}
				bool flag;
				if (questsRelatedToHero != null)
				{
					IssueBase relatedIssue2 = relatedIssue;
					if (((relatedIssue2 != null) ? relatedIssue2.IssueQuest : null) != null)
					{
						flag = questsRelatedToHero.Any((QuestBase q) => q == relatedIssue.IssueQuest);
						goto IL_171;
					}
				}
				flag = false;
				IL_171:
				bool flag2 = flag;
				if (relatedIssue != null && !flag2)
				{
					ValueTuple<CampaignUIHelper.IssueQuestFlags, TextObject, TextObject> item = new ValueTuple<CampaignUIHelper.IssueQuestFlags, TextObject, TextObject>(CampaignUIHelper.GetIssueType(relatedIssue), relatedIssue.Title, relatedIssue.Description);
					list.Add(item);
				}
			}
			return list;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000A250 File Offset: 0x00008450
		public static string GetQuestExplanationOfHero(CampaignUIHelper.IssueQuestFlags questType)
		{
			bool flag = (questType & CampaignUIHelper.IssueQuestFlags.ActiveIssue) != CampaignUIHelper.IssueQuestFlags.None || (questType & CampaignUIHelper.IssueQuestFlags.AvailableIssue) > CampaignUIHelper.IssueQuestFlags.None;
			bool flag2 = (questType & CampaignUIHelper.IssueQuestFlags.ActiveIssue) > CampaignUIHelper.IssueQuestFlags.None;
			string result = null;
			if (questType != CampaignUIHelper.IssueQuestFlags.None)
			{
				if (flag)
				{
					result = GameTexts.FindText("str_hero_has_" + (flag2 ? "active" : "available") + "_issue", null).ToString();
				}
				else
				{
					result = GameTexts.FindText("str_hero_has_active_quest", null).ToString();
				}
			}
			return result;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000A2BC File Offset: 0x000084BC
		public static List<QuestBase> GetQuestsRelatedToHero(Hero hero)
		{
			List<QuestBase> list = new List<QuestBase>();
			List<QuestBase> list2;
			Campaign.Current.QuestManager.TrackedObjects.TryGetValue(hero, out list2);
			if (list2 != null)
			{
				for (int i = 0; i < list2.Count; i++)
				{
					if (list2[i].IsTrackEnabled)
					{
						list.Add(list2[i]);
					}
				}
			}
			IssueBase issue = hero.Issue;
			if (((issue != null) ? issue.IssueQuest : null) != null && hero.Issue.IssueQuest.IsTrackEnabled && !hero.Issue.IssueQuest.IsTracked(hero))
			{
				list.Add(hero.Issue.IssueQuest);
			}
			return list;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000A364 File Offset: 0x00008564
		public static List<QuestBase> GetQuestsRelatedToParty(MobileParty party)
		{
			List<QuestBase> list = new List<QuestBase>();
			List<QuestBase> list2;
			Campaign.Current.QuestManager.TrackedObjects.TryGetValue(party, out list2);
			if (list2 != null)
			{
				for (int i = 0; i < list2.Count; i++)
				{
					if (list2[i].IsTrackEnabled)
					{
						list.Add(list2[i]);
					}
				}
			}
			if (party.MemberRoster.TotalHeroes > 0)
			{
				if (party.LeaderHero != null && party.MemberRoster.TotalHeroes == 1)
				{
					List<QuestBase> questsRelatedToHero = CampaignUIHelper.GetQuestsRelatedToHero(party.LeaderHero);
					if (questsRelatedToHero != null && questsRelatedToHero.Count > 0)
					{
						list.AddRange(questsRelatedToHero);
					}
				}
				else
				{
					for (int j = 0; j < party.MemberRoster.Count; j++)
					{
						CharacterObject characterAtIndex = party.MemberRoster.GetCharacterAtIndex(j);
						Hero hero = (characterAtIndex != null) ? characterAtIndex.HeroObject : null;
						if (hero != null)
						{
							List<QuestBase> questsRelatedToHero2 = CampaignUIHelper.GetQuestsRelatedToHero(hero);
							if (questsRelatedToHero2 != null && questsRelatedToHero2.Count > 0)
							{
								list.AddRange(questsRelatedToHero2);
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0000A460 File Offset: 0x00008660
		public static List<QuestBase> GetQuestsRelatedToSettlement(Settlement settlement)
		{
			List<QuestBase> list = new List<QuestBase>();
			foreach (KeyValuePair<ITrackableCampaignObject, List<QuestBase>> keyValuePair in Campaign.Current.QuestManager.TrackedObjects)
			{
				Hero hero;
				MobileParty mobileParty;
				if (((hero = (keyValuePair.Key as Hero)) != null && hero.CurrentSettlement == settlement) || ((mobileParty = (keyValuePair.Key as MobileParty)) != null && mobileParty.CurrentSettlement == settlement))
				{
					for (int i = 0; i < keyValuePair.Value.Count; i++)
					{
						if (!list.Contains(keyValuePair.Value[i]) && keyValuePair.Value[i].IsTrackEnabled)
						{
							list.Add(keyValuePair.Value[i]);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000A550 File Offset: 0x00008750
		public static CampaignUIHelper.IssueQuestFlags GetIssueType(IssueBase issue)
		{
			if (issue.IsSolvingWithAlternative || issue.IsSolvingWithLordSolution || issue.IsSolvingWithQuest)
			{
				return CampaignUIHelper.IssueQuestFlags.ActiveIssue;
			}
			return CampaignUIHelper.IssueQuestFlags.AvailableIssue;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000A56D File Offset: 0x0000876D
		public static CampaignUIHelper.IssueQuestFlags GetQuestType(QuestBase quest, Hero queriedQuestGiver)
		{
			if (quest.QuestGiver != null && quest.QuestGiver == queriedQuestGiver)
			{
				if (!quest.IsSpecialQuest)
				{
					return CampaignUIHelper.IssueQuestFlags.ActiveIssue;
				}
				return CampaignUIHelper.IssueQuestFlags.ActiveStoryQuest;
			}
			else
			{
				if (!quest.IsSpecialQuest)
				{
					return CampaignUIHelper.IssueQuestFlags.TrackedIssue;
				}
				return CampaignUIHelper.IssueQuestFlags.TrackedStoryQuest;
			}
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000A598 File Offset: 0x00008798
		public static IEnumerable<TraitObject> GetHeroTraits()
		{
			yield return DefaultTraits.Generosity;
			yield return DefaultTraits.Honor;
			yield return DefaultTraits.Valor;
			yield return DefaultTraits.Mercy;
			yield return DefaultTraits.Calculating;
			yield break;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000A5A1 File Offset: 0x000087A1
		public static bool IsItemUsageApplicable(WeaponComponentData weapon)
		{
			WeaponDescription weaponDescription = (weapon != null && weapon.WeaponDescriptionId != null) ? MBObjectManager.Instance.GetObject<WeaponDescription>(weapon.WeaponDescriptionId) : null;
			return weaponDescription != null && !weaponDescription.IsHiddenFromUI;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000A5CF File Offset: 0x000087CF
		public static string FloatToString(float x)
		{
			return x.ToString("F1");
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000A5E0 File Offset: 0x000087E0
		private static Tuple<bool, TextObject> GetIsStringApplicableForHeroName(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return new Tuple<bool, TextObject>(false, new TextObject("{=C9tKA0ul}Character name cannot be empty", null));
			}
			bool flag;
			if (name.Length < 3)
			{
				if (!name.Any((char c) => Common.IsCharAsian(c)))
				{
					flag = false;
					goto IL_5A;
				}
			}
			flag = (name.Length <= 50);
			IL_5A:
			if (!flag)
			{
				TextObject textObject = new TextObject("{=fPoB2u5m}Character name should be between {MIN} and {MAX} characters", null);
				textObject.SetTextVariable("MIN", 3);
				textObject.SetTextVariable("MAX", 50);
				return new Tuple<bool, TextObject>(false, textObject);
			}
			if (!name.All((char x) => (char.IsLetterOrDigit(x) || char.IsWhiteSpace(x) || char.IsPunctuation(x)) && x != '{' && x != '}'))
			{
				return new Tuple<bool, TextObject>(false, new TextObject("{=P1hk0m4o}Character name cannot contain special characters", null));
			}
			if (name.StartsWith(" ") || name.EndsWith(" "))
			{
				return new Tuple<bool, TextObject>(false, new TextObject("{=oofSja21}Character name cannot start or end with a white space", null));
			}
			if (name.Contains("  "))
			{
				return new Tuple<bool, TextObject>(false, new TextObject("{=wcSSgFyK}Character name cannot contain consecutive white spaces", null));
			}
			return new Tuple<bool, TextObject>(true, TextObject.Empty);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000A708 File Offset: 0x00008908
		public static Tuple<bool, string> IsStringApplicableForHeroName(string name)
		{
			Tuple<bool, TextObject> isStringApplicableForHeroName = CampaignUIHelper.GetIsStringApplicableForHeroName(name);
			return new Tuple<bool, string>(isStringApplicableForHeroName.Item1, isStringApplicableForHeroName.Item2.ToString());
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000A734 File Offset: 0x00008934
		public static Tuple<bool, TextObject> IsStringApplicableForItemName(string name)
		{
			bool flag;
			if (name.Length < 3)
			{
				if (!name.Any((char c) => Common.IsCharAsian(c)))
				{
					flag = false;
					goto IL_40;
				}
			}
			flag = (name.Length <= 50);
			IL_40:
			if (!flag)
			{
				TextObject textObject = new TextObject("{=h0xoKxxo}Item name should be between {MIN} and {MAX} characters.", null);
				textObject.SetTextVariable("MIN", 3);
				textObject.SetTextVariable("MAX", 50);
				return new Tuple<bool, TextObject>(false, textObject);
			}
			if (string.IsNullOrEmpty(name))
			{
				return new Tuple<bool, TextObject>(false, new TextObject("{=QQ03J6sf}Item name can not be empty.", null));
			}
			if (!name.All((char x) => (char.IsLetterOrDigit(x) || char.IsWhiteSpace(x) || char.IsPunctuation(x)) && x != '{' && x != '}'))
			{
				return new Tuple<bool, TextObject>(false, new TextObject("{=NkY3Kq9l}Item name cannot contain special characters.", null));
			}
			if (name.StartsWith(" ") || name.EndsWith(" "))
			{
				return new Tuple<bool, TextObject>(false, new TextObject("{=2Hbr4TEj}Item name cannot start or end with a white space.", null));
			}
			if (name.Contains("  "))
			{
				return new Tuple<bool, TextObject>(false, new TextObject("{=Z4GdqdgV}Item name cannot contain consecutive white spaces.", null));
			}
			return new Tuple<bool, TextObject>(true, TextObject.Empty);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000A85B File Offset: 0x00008A5B
		public static CharacterObject GetVisualPartyLeader(PartyBase party)
		{
			return PartyBaseHelper.GetVisualPartyLeader(party);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000A864 File Offset: 0x00008A64
		private static string GetChangeValueString(float value)
		{
			string text = value.ToString("0.##");
			if (value > 0.001f)
			{
				MBTextManager.SetTextVariable("NUMBER", text, false);
				return GameTexts.FindText("str_plus_with_number", null).ToString();
			}
			return text;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000ACE4 File Offset: 0x00008EE4
		[CompilerGenerated]
		internal static void <GetOrderCannotBeCompletedReasonTooltip>g__AddProperty|145_0(CraftingTemplate.CraftingStatTypes type, float reqValue, ref CampaignUIHelper.<>c__DisplayClass145_0 A_2)
		{
			TextObject textObject = GameTexts.FindText("str_crafting_stat", type.ToString());
			TextObject variable = GameTexts.FindText("str_inventory_dmg_type", ((int)A_2.order.PreCraftedWeaponDesignItem.PrimaryWeapon.ThrustDamageType).ToString());
			textObject.SetTextVariable("THRUST_DAMAGE_TYPE", variable);
			TextObject variable2 = GameTexts.FindText("str_inventory_dmg_type", ((int)A_2.order.PreCraftedWeaponDesignItem.PrimaryWeapon.SwingDamageType).ToString());
			textObject.SetTextVariable("SWING_DAMAGE_TYPE", variable2);
			CampaignUIHelper._orderRequirementText.SetTextVariable("STAT", textObject);
			CampaignUIHelper._orderRequirementText.SetTextVariable("REQUIREMENT", reqValue);
			A_2.properties.Add(new TooltipProperty(CampaignUIHelper._orderRequirementText.ToString(), "", 0, false, TooltipProperty.TooltipPropertyFlags.None));
		}

		// Token: 0x0400006E RID: 110
		private static readonly TextObject _changeStr = new TextObject("{=R2AaCaPJ}Expected Change", null);

		// Token: 0x0400006F RID: 111
		private static readonly TextObject _totalStr = new TextObject("{=kWVbHPtT}Total", null);

		// Token: 0x04000070 RID: 112
		private static readonly TextObject _noChangesStr = new TextObject("{=XIioBPi0}No changes", null);

		// Token: 0x04000071 RID: 113
		private static readonly TextObject _hitPointsStr = new TextObject("{=oBbiVeKE}Hit Points", null);

		// Token: 0x04000072 RID: 114
		private static readonly TextObject _maxhitPointsStr = new TextObject("{=mDFhzEMC}Max. Hit Points", null);

		// Token: 0x04000073 RID: 115
		private static readonly TextObject _prosperityStr = new TextObject("{=IagYTD5O}Prosperity", null);

		// Token: 0x04000074 RID: 116
		private static readonly TextObject _hearthStr = new TextObject("{=2GWR9Cba}Hearth", null);

		// Token: 0x04000075 RID: 117
		private static readonly TextObject _dailyProductionStr = new TextObject("{=94aHU6nD}Construction", null);

		// Token: 0x04000076 RID: 118
		private static readonly TextObject _securityStr = new TextObject("{=MqCH7R4A}Security", null);

		// Token: 0x04000077 RID: 119
		private static readonly TextObject _criminalRatingStr = new TextObject("{=it8oPzb1}Criminal Rating", null);

		// Token: 0x04000078 RID: 120
		private static readonly TextObject _militiaStr = new TextObject("{=gsVtO9A7}Militia", null);

		// Token: 0x04000079 RID: 121
		private static readonly TextObject _foodStr = new TextObject("{=qSi4DlT4}Food", null);

		// Token: 0x0400007A RID: 122
		private static readonly TextObject _foodItemsStr = new TextObject("{=IQY9yykn}Food Items", null);

		// Token: 0x0400007B RID: 123
		private static readonly TextObject _livestockStr = new TextObject("{=UI0q8rWw}Livestock", null);

		// Token: 0x0400007C RID: 124
		private static readonly TextObject _armyCohesionStr = new TextObject("{=iZ3w6opW}Cohesion", null);

		// Token: 0x0400007D RID: 125
		private static readonly TextObject _loyaltyStr = new TextObject("{=YO0x7ZAo}Loyalty", null);

		// Token: 0x0400007E RID: 126
		private static readonly TextObject _wallsStr = new TextObject("{=LsZEdD2z}Walls", null);

		// Token: 0x0400007F RID: 127
		private static readonly TextObject _plusStr = new TextObject("{=eTw2aNV5}+", null);

		// Token: 0x04000080 RID: 128
		private static readonly TextObject _heroesHealingRateStr = new TextObject("{=HHTQVp52}Heroes Healing Rate", null);

		// Token: 0x04000081 RID: 129
		private static readonly TextObject _numTotalTroopsInTheArmyStr = new TextObject("{=DRJOxrRF}Troops in Army", null);

		// Token: 0x04000082 RID: 130
		private static readonly TextObject _garrisonStr = new TextObject("{=jlgjLDo7}Garrison", null);

		// Token: 0x04000083 RID: 131
		private static readonly TextObject _hitPoints = new TextObject("{=UbZL2BJQ}Hitpoints", null);

		// Token: 0x04000084 RID: 132
		private static readonly TextObject _maxhitPoints = new TextObject("{=KTTyBbsp}Max HP", null);

		// Token: 0x04000085 RID: 133
		private static readonly TextObject _goldStr = new TextObject("{=Hxf6bzmR}Current Denars", null);

		// Token: 0x04000086 RID: 134
		private static readonly TextObject _resultGold = new TextObject("{=NC9bbrt5}End-of-day denars", null);

		// Token: 0x04000087 RID: 135
		private static readonly TextObject _influenceStr = new TextObject("{=RVPidk5a}Influence", null);

		// Token: 0x04000088 RID: 136
		private static readonly TextObject _partyMoraleStr = GameTexts.FindText("str_party_morale", null);

		// Token: 0x04000089 RID: 137
		private static readonly TextObject _partyFoodStr = new TextObject("{=mg7id9om}Number of Consumable Items", null);

		// Token: 0x0400008A RID: 138
		private static readonly TextObject _partySpeedStr = new TextObject("{=zWaVxD6T}Party Speed", null);

		// Token: 0x0400008B RID: 139
		private static readonly TextObject _partySizeLimitStr = new TextObject("{=mp68RYnD}Party Size Limit", null);

		// Token: 0x0400008C RID: 140
		private static readonly TextObject _viewDistanceFoodStr = new TextObject("{=hTzTMLsf}View Distance", null);

		// Token: 0x0400008D RID: 141
		private static readonly TextObject _battleReadyTroopsStr = new TextObject("{=LVmkE2Ow}Battle Ready Troops", null);

		// Token: 0x0400008E RID: 142
		private static readonly TextObject _woundedTroopsStr = new TextObject("{=TzLtVzdg}Wounded Troops", null);

		// Token: 0x0400008F RID: 143
		private static readonly TextObject _prisonersStr = new TextObject("{=N6QTvjMf}Prisoners", null);

		// Token: 0x04000090 RID: 144
		private static readonly TextObject _regularsHealingRateStr = new TextObject("{=tf7301NC}Healing Rate", null);

		// Token: 0x04000091 RID: 145
		private static readonly TextObject _learningRateStr = new TextObject("{=q1J4a8rr}Learning Rate", null);

		// Token: 0x04000092 RID: 146
		private static readonly TextObject _learningLimitStr = new TextObject("{=YT9giTet}Learning Limit", null);

		// Token: 0x04000093 RID: 147
		private static readonly TextObject _partyInventoryCapacityStr = new TextObject("{=fI7a7RoE}Inventory Capacity", null);

		// Token: 0x04000094 RID: 148
		private static readonly TextObject _partyTroopSizeLimitStr = new TextObject("{=2Cq3tViJ}Party Troop Size Limit", null);

		// Token: 0x04000095 RID: 149
		private static readonly TextObject _partyPrisonerSizeLimitStr = new TextObject("{=UHLcmf9A}Party Prisoner Size Limit", null);

		// Token: 0x04000096 RID: 150
		private static readonly TextObject _inventorySkillTooltipTitle = new TextObject("{=Y7qbwrWE}{HERO_NAME}'s Skills", null);

		// Token: 0x04000097 RID: 151
		private static readonly TextObject _mercenaryClanInfluenceStr = new TextObject("{=GP3jpU0X}Influence is periodically converted to denars for mercenary clans.", null);

		// Token: 0x04000098 RID: 152
		private static readonly TextObject _orderRequirementText = new TextObject("{=dVqowrRz} - {STAT} {REQUIREMENT}", null);

		// Token: 0x04000099 RID: 153
		private static readonly TextObject _denarValueInfoText = new TextObject("{=mapbardenarvalue}{DENAR_AMOUNT}{VALUE_ABBREVIATION}", null);

		// Token: 0x0400009A RID: 154
		private static readonly TextObject _prisonerOfText = new TextObject("{=a8nRxITn}Prisoner of {PARTY_NAME}", null);

		// Token: 0x0400009B RID: 155
		private static readonly TextObject _attachedToText = new TextObject("{=8Jy9DnKk}Attached to {PARTY_NAME}", null);

		// Token: 0x0400009C RID: 156
		private static readonly TextObject _inYourPartyText = new TextObject("{=CRi905Ao}In your party", null);

		// Token: 0x0400009D RID: 157
		private static readonly TextObject _travelingText = new TextObject("{=vdKiLwaf}Traveling", null);

		// Token: 0x0400009E RID: 158
		private static readonly TextObject _recoveringText = new TextObject("{=heroRecovering}Recovering", null);

		// Token: 0x0400009F RID: 159
		private static readonly TextObject _recentlyReleasedText = new TextObject("{=NLFeyz7m}Recently Released From Captivity", null);

		// Token: 0x040000A0 RID: 160
		private static readonly TextObject _recentlyEscapedText = new TextObject("{=84oSzquz}Recently Escaped Captivity", null);

		// Token: 0x040000A1 RID: 161
		private static readonly TextObject _nearSettlementText = new TextObject("{=XjT8S4ng}Near {SETTLEMENT_NAME}", null);

		// Token: 0x040000A2 RID: 162
		private static readonly TextObject _noDelayText = new TextObject("{=bDwTWrru}No delay", null);

		// Token: 0x040000A3 RID: 163
		private static readonly TextObject _regroupingText = new TextObject("{=KxLoeSEO}Regrouping", null);

		// Token: 0x040000A4 RID: 164
		public static readonly CampaignUIHelper.MobilePartyPrecedenceComparer MobilePartyPrecedenceComparerInstance = new CampaignUIHelper.MobilePartyPrecedenceComparer();

		// Token: 0x040000A5 RID: 165
		private static readonly List<ItemObject.ItemTypeEnum> _itemObjectTypeSortIndices = new List<ItemObject.ItemTypeEnum>
		{
			ItemObject.ItemTypeEnum.Horse,
			ItemObject.ItemTypeEnum.OneHandedWeapon,
			ItemObject.ItemTypeEnum.TwoHandedWeapon,
			ItemObject.ItemTypeEnum.Polearm,
			ItemObject.ItemTypeEnum.Shield,
			ItemObject.ItemTypeEnum.Bow,
			ItemObject.ItemTypeEnum.Arrows,
			ItemObject.ItemTypeEnum.Crossbow,
			ItemObject.ItemTypeEnum.Bolts,
			ItemObject.ItemTypeEnum.Thrown,
			ItemObject.ItemTypeEnum.Pistol,
			ItemObject.ItemTypeEnum.Musket,
			ItemObject.ItemTypeEnum.Bullets,
			ItemObject.ItemTypeEnum.Goods,
			ItemObject.ItemTypeEnum.HeadArmor,
			ItemObject.ItemTypeEnum.Cape,
			ItemObject.ItemTypeEnum.BodyArmor,
			ItemObject.ItemTypeEnum.ChestArmor,
			ItemObject.ItemTypeEnum.HandArmor,
			ItemObject.ItemTypeEnum.LegArmor,
			ItemObject.ItemTypeEnum.Invalid,
			ItemObject.ItemTypeEnum.Animal,
			ItemObject.ItemTypeEnum.Book,
			ItemObject.ItemTypeEnum.HorseHarness,
			ItemObject.ItemTypeEnum.Banner
		};

		// Token: 0x0200014B RID: 331
		[Flags]
		public enum IssueQuestFlags
		{
			// Token: 0x04000F12 RID: 3858
			None = 0,
			// Token: 0x04000F13 RID: 3859
			AvailableIssue = 1,
			// Token: 0x04000F14 RID: 3860
			ActiveIssue = 2,
			// Token: 0x04000F15 RID: 3861
			ActiveStoryQuest = 4,
			// Token: 0x04000F16 RID: 3862
			TrackedIssue = 8,
			// Token: 0x04000F17 RID: 3863
			TrackedStoryQuest = 16
		}

		// Token: 0x0200014C RID: 332
		public enum SortState
		{
			// Token: 0x04000F19 RID: 3865
			Default,
			// Token: 0x04000F1A RID: 3866
			Ascending,
			// Token: 0x04000F1B RID: 3867
			Descending
		}

		// Token: 0x0200014D RID: 333
		public class MobilePartyPrecedenceComparer : IComparer<MobileParty>
		{
			// Token: 0x06001FCE RID: 8142 RVA: 0x00070F5C File Offset: 0x0006F15C
			public int Compare(MobileParty x, MobileParty y)
			{
				if (x.IsGarrison && !y.IsGarrison)
				{
					return -1;
				}
				if (x.IsGarrison && y.IsGarrison)
				{
					return -x.Party.TotalStrength.CompareTo(y.Party.TotalStrength);
				}
				if (x.IsMilitia && y.IsGarrison)
				{
					return 1;
				}
				if (x.IsMilitia && !y.IsGarrison && !y.IsMilitia)
				{
					return -1;
				}
				if (x.IsMilitia && y.IsMilitia)
				{
					return -x.Party.TotalStrength.CompareTo(y.Party.TotalStrength);
				}
				if (x.LeaderHero != null && (y.IsGarrison || y.IsMilitia))
				{
					return 1;
				}
				if (x.LeaderHero != null && y.LeaderHero == null)
				{
					return -1;
				}
				if (x.LeaderHero != null && y.LeaderHero != null)
				{
					return -x.Party.TotalStrength.CompareTo(y.Party.TotalStrength);
				}
				if (x.LeaderHero == null && (y.IsGarrison || y.IsMilitia || y.LeaderHero != null))
				{
					return 1;
				}
				if (x.LeaderHero == null)
				{
					Hero leaderHero = y.LeaderHero;
					return -x.Party.TotalStrength.CompareTo(y.Party.TotalStrength);
				}
				return -x.Party.TotalStrength.CompareTo(y.Party.TotalStrength);
			}
		}

		// Token: 0x0200014E RID: 334
		public class ProductInputOutputEqualityComparer : IEqualityComparer<ValueTuple<ItemCategory, int>>
		{
			// Token: 0x06001FD0 RID: 8144 RVA: 0x000710DB File Offset: 0x0006F2DB
			public bool Equals(ValueTuple<ItemCategory, int> x, ValueTuple<ItemCategory, int> y)
			{
				return x.Item1 == y.Item1;
			}

			// Token: 0x06001FD1 RID: 8145 RVA: 0x000710EB File Offset: 0x0006F2EB
			public int GetHashCode(ValueTuple<ItemCategory, int> obj)
			{
				return obj.Item1.GetHashCode();
			}
		}
	}
}
