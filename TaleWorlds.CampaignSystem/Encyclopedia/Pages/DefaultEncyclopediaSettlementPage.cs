﻿using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.Encyclopedia.Pages
{
	// Token: 0x02000164 RID: 356
	[EncyclopediaModel(new Type[]
	{
		typeof(Settlement)
	})]
	public class DefaultEncyclopediaSettlementPage : EncyclopediaPage
	{
		// Token: 0x060018DD RID: 6365 RVA: 0x0007DEB1 File Offset: 0x0007C0B1
		public DefaultEncyclopediaSettlementPage()
		{
			base.HomePageOrderIndex = 100;
		}

		// Token: 0x060018DE RID: 6366 RVA: 0x0007DEC1 File Offset: 0x0007C0C1
		protected override IEnumerable<EncyclopediaListItem> InitializeListItems()
		{
			using (List<Settlement>.Enumerator enumerator = Settlement.All.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Settlement settlement = enumerator.Current;
					if (this.IsValidEncyclopediaItem(settlement))
					{
						yield return new EncyclopediaListItem(settlement, settlement.Name.ToString(), "", settlement.StringId, base.GetIdentifier(typeof(Settlement)), DefaultEncyclopediaSettlementPage.CanPlayerSeeValuesOf(settlement), delegate()
						{
							InformationManager.ShowTooltip(typeof(Settlement), new object[]
							{
								settlement,
								false
							});
						});
					}
				}
			}
			List<Settlement>.Enumerator enumerator = default(List<Settlement>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x060018DF RID: 6367 RVA: 0x0007DED4 File Offset: 0x0007C0D4
		protected override IEnumerable<EncyclopediaFilterGroup> InitializeFilterItems()
		{
			List<EncyclopediaFilterGroup> list = new List<EncyclopediaFilterGroup>();
			List<EncyclopediaFilterItem> list2 = new List<EncyclopediaFilterItem>();
			list2.Add(new EncyclopediaFilterItem(new TextObject("{=bOTQ7Pta}Town", null), (object s) => ((Settlement)s).IsTown));
			list2.Add(new EncyclopediaFilterItem(new TextObject("{=sVXa3zFx}Castle", null), (object s) => ((Settlement)s).IsCastle));
			list2.Add(new EncyclopediaFilterItem(new TextObject("{=Ua6CNLBZ}Village", null), (object s) => ((Settlement)s).IsVillage));
			List<EncyclopediaFilterItem> filters = list2;
			list.Add(new EncyclopediaFilterGroup(filters, new TextObject("{=zMMqgxb1}Type", null)));
			List<EncyclopediaFilterItem> list3 = new List<EncyclopediaFilterItem>();
			using (List<CultureObject>.Enumerator enumerator = (from x in Game.Current.ObjectManager.GetObjectTypeList<CultureObject>()
			orderby !x.IsMainCulture descending
			select x).ThenBy((CultureObject f) => f.Name.ToString()).ToList<CultureObject>().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					CultureObject culture = enumerator.Current;
					if (culture.StringId != "neutral_culture" && culture.CanHaveSettlement)
					{
						list3.Add(new EncyclopediaFilterItem(culture.Name, (object c) => ((Settlement)c).Culture == culture));
					}
				}
			}
			list.Add(new EncyclopediaFilterGroup(list3, GameTexts.FindText("str_culture", null)));
			return list;
		}

		// Token: 0x060018E0 RID: 6368 RVA: 0x0007E0B0 File Offset: 0x0007C2B0
		protected override IEnumerable<EncyclopediaSortController> InitializeSortControllers()
		{
			return new List<EncyclopediaSortController>
			{
				new EncyclopediaSortController(GameTexts.FindText("str_garrison", null), new DefaultEncyclopediaSettlementPage.EncyclopediaListSettlementGarrisonComparer()),
				new EncyclopediaSortController(GameTexts.FindText("str_food", null), new DefaultEncyclopediaSettlementPage.EncyclopediaListSettlementFoodComparer()),
				new EncyclopediaSortController(GameTexts.FindText("str_security", null), new DefaultEncyclopediaSettlementPage.EncyclopediaListSettlementSecurityComparer()),
				new EncyclopediaSortController(GameTexts.FindText("str_loyalty", null), new DefaultEncyclopediaSettlementPage.EncyclopediaListSettlementLoyaltyComparer()),
				new EncyclopediaSortController(GameTexts.FindText("str_militia", null), new DefaultEncyclopediaSettlementPage.EncyclopediaListSettlementMilitiaComparer()),
				new EncyclopediaSortController(GameTexts.FindText("str_prosperity", null), new DefaultEncyclopediaSettlementPage.EncyclopediaListSettlementProsperityComparer())
			};
		}

		// Token: 0x060018E1 RID: 6369 RVA: 0x0007E164 File Offset: 0x0007C364
		public override string GetViewFullyQualifiedName()
		{
			return "EncyclopediaSettlementPage";
		}

		// Token: 0x060018E2 RID: 6370 RVA: 0x0007E16B File Offset: 0x0007C36B
		public override TextObject GetName()
		{
			return GameTexts.FindText("str_settlements", null);
		}

		// Token: 0x060018E3 RID: 6371 RVA: 0x0007E178 File Offset: 0x0007C378
		public override TextObject GetDescriptionText()
		{
			return GameTexts.FindText("str_settlement_description", null);
		}

		// Token: 0x060018E4 RID: 6372 RVA: 0x0007E185 File Offset: 0x0007C385
		public override string GetStringID()
		{
			return "EncyclopediaSettlement";
		}

		// Token: 0x060018E5 RID: 6373 RVA: 0x0007E18C File Offset: 0x0007C38C
		public override bool IsValidEncyclopediaItem(object o)
		{
			Settlement settlement = o as Settlement;
			return settlement != null && (settlement.IsFortification || settlement.IsVillage);
		}

		// Token: 0x060018E6 RID: 6374 RVA: 0x0007E1B5 File Offset: 0x0007C3B5
		private static bool CanPlayerSeeValuesOf(Settlement settlement)
		{
			return Campaign.Current.Models.InformationRestrictionModel.DoesPlayerKnowDetailsOf(settlement);
		}

		// Token: 0x0200053F RID: 1343
		private class EncyclopediaListSettlementGarrisonComparer : DefaultEncyclopediaSettlementPage.EncyclopediaListSettlementComparer
		{
			// Token: 0x060044D6 RID: 17622 RVA: 0x00148E6C File Offset: 0x0014706C
			private static int GarrisonComparison(Town t1, Town t2)
			{
				return t1.GarrisonParty.MemberRoster.TotalManCount.CompareTo(t2.GarrisonParty.MemberRoster.TotalManCount);
			}

			// Token: 0x060044D7 RID: 17623 RVA: 0x00148EA4 File Offset: 0x001470A4
			protected override bool CompareVisibility(Settlement s1, Settlement s2, out int comparisonResult)
			{
				if (s1.IsTown && s2.IsTown)
				{
					if (s1.Town.GarrisonParty == null && s2.Town.GarrisonParty == null)
					{
						comparisonResult = 0;
						return true;
					}
					if (s1.Town.GarrisonParty == null)
					{
						comparisonResult = (base.IsAscending ? 2 : -2);
						return true;
					}
					if (s2.Town.GarrisonParty == null)
					{
						comparisonResult = (base.IsAscending ? -2 : 2);
						return true;
					}
				}
				return base.CompareVisibility(s1, s2, out comparisonResult);
			}

			// Token: 0x060044D8 RID: 17624 RVA: 0x00148F25 File Offset: 0x00147125
			public override int Compare(EncyclopediaListItem x, EncyclopediaListItem y)
			{
				return base.CompareFiefs(x, y, new DefaultEncyclopediaSettlementPage.EncyclopediaListSettlementComparer.SettlementVisibilityComparerDelegate(this.CompareVisibility), new Func<Town, Town, int>(DefaultEncyclopediaSettlementPage.EncyclopediaListSettlementGarrisonComparer.GarrisonComparison));
			}

			// Token: 0x060044D9 RID: 17625 RVA: 0x00148F48 File Offset: 0x00147148
			public override string GetComparedValueText(EncyclopediaListItem item)
			{
				Settlement settlement;
				if ((settlement = (item.Object as Settlement)) == null)
				{
					Debug.FailedAssert("Unable to get the garrison of a non-settlement object.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\Encyclopedia\\Pages\\DefaultEncyclopediaSettlementPage.cs", "GetComparedValueText", 159);
					return "";
				}
				if (settlement.IsVillage)
				{
					return this._emptyValue.ToString();
				}
				if (!DefaultEncyclopediaSettlementPage.CanPlayerSeeValuesOf(settlement))
				{
					return this._missingValue.ToString();
				}
				MobileParty garrisonParty = settlement.Town.GarrisonParty;
				return ((garrisonParty != null) ? garrisonParty.MemberRoster.TotalManCount.ToString() : null) ?? 0.ToString();
			}
		}

		// Token: 0x02000540 RID: 1344
		private class EncyclopediaListSettlementFoodComparer : DefaultEncyclopediaSettlementPage.EncyclopediaListSettlementComparer
		{
			// Token: 0x060044DB RID: 17627 RVA: 0x00148FE4 File Offset: 0x001471E4
			public override int Compare(EncyclopediaListItem x, EncyclopediaListItem y)
			{
				return base.CompareFiefs(x, y, new DefaultEncyclopediaSettlementPage.EncyclopediaListSettlementComparer.SettlementVisibilityComparerDelegate(this.CompareVisibility), new Func<Town, Town, int>(DefaultEncyclopediaSettlementPage.EncyclopediaListSettlementFoodComparer.FoodComparison));
			}

			// Token: 0x060044DC RID: 17628 RVA: 0x00149008 File Offset: 0x00147208
			private static int FoodComparison(Town t1, Town t2)
			{
				return t1.FoodStocks.CompareTo(t2.FoodStocks);
			}

			// Token: 0x060044DD RID: 17629 RVA: 0x0014902C File Offset: 0x0014722C
			public override string GetComparedValueText(EncyclopediaListItem item)
			{
				Settlement settlement;
				if ((settlement = (item.Object as Settlement)) == null)
				{
					Debug.FailedAssert("Unable to get the food stocks of a non-settlement object.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\Encyclopedia\\Pages\\DefaultEncyclopediaSettlementPage.cs", "GetComparedValueText", 194);
					return "";
				}
				if (settlement.IsVillage)
				{
					return this._emptyValue.ToString();
				}
				if (!DefaultEncyclopediaSettlementPage.CanPlayerSeeValuesOf(settlement))
				{
					return this._missingValue.ToString();
				}
				return ((int)settlement.Town.FoodStocks).ToString();
			}
		}

		// Token: 0x02000541 RID: 1345
		private class EncyclopediaListSettlementSecurityComparer : DefaultEncyclopediaSettlementPage.EncyclopediaListSettlementComparer
		{
			// Token: 0x060044DF RID: 17631 RVA: 0x001490AB File Offset: 0x001472AB
			public override int Compare(EncyclopediaListItem x, EncyclopediaListItem y)
			{
				return base.CompareFiefs(x, y, new DefaultEncyclopediaSettlementPage.EncyclopediaListSettlementComparer.SettlementVisibilityComparerDelegate(this.CompareVisibility), new Func<Town, Town, int>(DefaultEncyclopediaSettlementPage.EncyclopediaListSettlementSecurityComparer.SecurityComparison));
			}

			// Token: 0x060044E0 RID: 17632 RVA: 0x001490D0 File Offset: 0x001472D0
			private static int SecurityComparison(Town t1, Town t2)
			{
				return t1.Security.CompareTo(t2.Security);
			}

			// Token: 0x060044E1 RID: 17633 RVA: 0x001490F4 File Offset: 0x001472F4
			public override string GetComparedValueText(EncyclopediaListItem item)
			{
				Settlement settlement;
				if ((settlement = (item.Object as Settlement)) == null)
				{
					Debug.FailedAssert("Unable to get the security of a non-settlement object.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\Encyclopedia\\Pages\\DefaultEncyclopediaSettlementPage.cs", "GetComparedValueText", 229);
					return "";
				}
				if (settlement.IsVillage)
				{
					return this._emptyValue.ToString();
				}
				if (!DefaultEncyclopediaSettlementPage.CanPlayerSeeValuesOf(settlement))
				{
					return this._missingValue.ToString();
				}
				return ((int)settlement.Town.Security).ToString();
			}
		}

		// Token: 0x02000542 RID: 1346
		private class EncyclopediaListSettlementLoyaltyComparer : DefaultEncyclopediaSettlementPage.EncyclopediaListSettlementComparer
		{
			// Token: 0x060044E3 RID: 17635 RVA: 0x00149173 File Offset: 0x00147373
			public override int Compare(EncyclopediaListItem x, EncyclopediaListItem y)
			{
				return base.CompareFiefs(x, y, new DefaultEncyclopediaSettlementPage.EncyclopediaListSettlementComparer.SettlementVisibilityComparerDelegate(this.CompareVisibility), new Func<Town, Town, int>(DefaultEncyclopediaSettlementPage.EncyclopediaListSettlementLoyaltyComparer.LoyaltyComparison));
			}

			// Token: 0x060044E4 RID: 17636 RVA: 0x00149198 File Offset: 0x00147398
			private static int LoyaltyComparison(Town t1, Town t2)
			{
				return t1.Loyalty.CompareTo(t2.Loyalty);
			}

			// Token: 0x060044E5 RID: 17637 RVA: 0x001491BC File Offset: 0x001473BC
			public override string GetComparedValueText(EncyclopediaListItem item)
			{
				Settlement settlement;
				if ((settlement = (item.Object as Settlement)) == null)
				{
					Debug.FailedAssert("Unable to get the loyalty of a non-settlement object.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\Encyclopedia\\Pages\\DefaultEncyclopediaSettlementPage.cs", "GetComparedValueText", 264);
					return "";
				}
				if (settlement.IsVillage)
				{
					return this._emptyValue.ToString();
				}
				if (!DefaultEncyclopediaSettlementPage.CanPlayerSeeValuesOf(settlement))
				{
					return this._missingValue.ToString();
				}
				return ((int)settlement.Town.Loyalty).ToString();
			}
		}

		// Token: 0x02000543 RID: 1347
		private class EncyclopediaListSettlementMilitiaComparer : DefaultEncyclopediaSettlementPage.EncyclopediaListSettlementComparer
		{
			// Token: 0x060044E7 RID: 17639 RVA: 0x0014923B File Offset: 0x0014743B
			public override int Compare(EncyclopediaListItem x, EncyclopediaListItem y)
			{
				return base.CompareSettlements(x, y, new DefaultEncyclopediaSettlementPage.EncyclopediaListSettlementComparer.SettlementVisibilityComparerDelegate(this.CompareVisibility), new Func<Settlement, Settlement, int>(DefaultEncyclopediaSettlementPage.EncyclopediaListSettlementMilitiaComparer.MilitiaComparison));
			}

			// Token: 0x060044E8 RID: 17640 RVA: 0x00149260 File Offset: 0x00147460
			private static int MilitiaComparison(Settlement t1, Settlement t2)
			{
				return t1.Militia.CompareTo(t2.Militia);
			}

			// Token: 0x060044E9 RID: 17641 RVA: 0x00149284 File Offset: 0x00147484
			public override string GetComparedValueText(EncyclopediaListItem item)
			{
				Settlement settlement;
				if ((settlement = (item.Object as Settlement)) == null)
				{
					Debug.FailedAssert("Unable to get the militia of a non-settlement object.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\Encyclopedia\\Pages\\DefaultEncyclopediaSettlementPage.cs", "GetComparedValueText", 295);
					return "";
				}
				if (!DefaultEncyclopediaSettlementPage.CanPlayerSeeValuesOf(settlement))
				{
					return this._missingValue.ToString();
				}
				return ((int)settlement.Militia).ToString();
			}
		}

		// Token: 0x02000544 RID: 1348
		private class EncyclopediaListSettlementProsperityComparer : DefaultEncyclopediaSettlementPage.EncyclopediaListSettlementComparer
		{
			// Token: 0x060044EB RID: 17643 RVA: 0x001492EA File Offset: 0x001474EA
			public override int Compare(EncyclopediaListItem x, EncyclopediaListItem y)
			{
				return base.CompareFiefs(x, y, new DefaultEncyclopediaSettlementPage.EncyclopediaListSettlementComparer.SettlementVisibilityComparerDelegate(this.CompareVisibility), new Func<Town, Town, int>(DefaultEncyclopediaSettlementPage.EncyclopediaListSettlementProsperityComparer.ProsperityComparison));
			}

			// Token: 0x060044EC RID: 17644 RVA: 0x00149310 File Offset: 0x00147510
			private static int ProsperityComparison(Town t1, Town t2)
			{
				return t1.Prosperity.CompareTo(t2.Prosperity);
			}

			// Token: 0x060044ED RID: 17645 RVA: 0x00149334 File Offset: 0x00147534
			public override string GetComparedValueText(EncyclopediaListItem item)
			{
				Settlement settlement;
				if ((settlement = (item.Object as Settlement)) == null)
				{
					Debug.FailedAssert("Unable to get the prosperity of a non-settlement object.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\Encyclopedia\\Pages\\DefaultEncyclopediaSettlementPage.cs", "GetComparedValueText", 330);
					return "";
				}
				if (settlement.IsVillage)
				{
					return this._emptyValue.ToString();
				}
				if (!DefaultEncyclopediaSettlementPage.CanPlayerSeeValuesOf(settlement))
				{
					return this._missingValue.ToString();
				}
				return ((int)settlement.Town.Prosperity).ToString();
			}
		}

		// Token: 0x02000545 RID: 1349
		public abstract class EncyclopediaListSettlementComparer : EncyclopediaListItemComparerBase
		{
			// Token: 0x060044EF RID: 17647 RVA: 0x001493B4 File Offset: 0x001475B4
			protected virtual bool CompareVisibility(Settlement s1, Settlement s2, out int comparisonResult)
			{
				bool flag = DefaultEncyclopediaSettlementPage.CanPlayerSeeValuesOf(s1);
				bool flag2 = DefaultEncyclopediaSettlementPage.CanPlayerSeeValuesOf(s2);
				if (!flag && !flag2)
				{
					comparisonResult = 0;
					return true;
				}
				if (!flag)
				{
					comparisonResult = (base.IsAscending ? 1 : -1);
					return true;
				}
				if (!flag2)
				{
					comparisonResult = (base.IsAscending ? -1 : 1);
					return true;
				}
				comparisonResult = 0;
				return false;
			}

			// Token: 0x060044F0 RID: 17648 RVA: 0x00149404 File Offset: 0x00147604
			protected int CompareSettlements(EncyclopediaListItem x, EncyclopediaListItem y, DefaultEncyclopediaSettlementPage.EncyclopediaListSettlementComparer.SettlementVisibilityComparerDelegate visibilityComparison, Func<Settlement, Settlement, int> comparison)
			{
				Settlement settlement;
				Settlement settlement2;
				if ((settlement = (x.Object as Settlement)) == null || (settlement2 = (y.Object as Settlement)) == null)
				{
					Debug.FailedAssert("Both objects should be settlements.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\Encyclopedia\\Pages\\DefaultEncyclopediaSettlementPage.cs", "CompareSettlements", 379);
					return 0;
				}
				int num;
				if (visibilityComparison(settlement, settlement2, out num))
				{
					if (num == 0)
					{
						return base.ResolveEquality(x, y);
					}
					return num * (base.IsAscending ? 1 : -1);
				}
				else
				{
					int num2 = comparison(settlement, settlement2) * (base.IsAscending ? 1 : -1);
					if (num2 == 0)
					{
						return base.ResolveEquality(x, y);
					}
					return num2;
				}
			}

			// Token: 0x060044F1 RID: 17649 RVA: 0x00149498 File Offset: 0x00147698
			protected int CompareFiefs(EncyclopediaListItem x, EncyclopediaListItem y, DefaultEncyclopediaSettlementPage.EncyclopediaListSettlementComparer.SettlementVisibilityComparerDelegate visibilityComparison, Func<Town, Town, int> comparison)
			{
				Settlement settlement;
				Settlement settlement2;
				if ((settlement = (x.Object as Settlement)) == null || (settlement2 = (y.Object as Settlement)) == null)
				{
					Debug.FailedAssert("Unable to compare loyalty of non-fief (castle or town) objects.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\Encyclopedia\\Pages\\DefaultEncyclopediaSettlementPage.cs", "CompareFiefs", 403);
					return 0;
				}
				int num = settlement.IsVillage.CompareTo(settlement2.IsVillage);
				if (num != 0)
				{
					return num;
				}
				if (settlement.IsVillage && settlement2.IsVillage)
				{
					return base.ResolveEquality(x, y);
				}
				int num2;
				if (visibilityComparison(settlement, settlement2, out num2))
				{
					if (num2 == 0)
					{
						return base.ResolveEquality(x, y);
					}
					return num2 * (base.IsAscending ? 1 : -1);
				}
				else
				{
					num = comparison(settlement.Town, settlement2.Town) * (base.IsAscending ? 1 : -1);
					if (num == 0)
					{
						return base.ResolveEquality(x, y);
					}
					return num;
				}
			}

			// Token: 0x02000794 RID: 1940
			// (Invoke) Token: 0x06005A49 RID: 23113
			protected delegate bool SettlementVisibilityComparerDelegate(Settlement s1, Settlement s2, out int comparisonResult);
		}
	}
}
