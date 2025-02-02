using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.CampaignSystem.Encyclopedia.Pages
{
	// Token: 0x02000162 RID: 354
	[EncyclopediaModel(new Type[]
	{
		typeof(Kingdom)
	})]
	public class DefaultEncyclopediaFactionPage : EncyclopediaPage
	{
		// Token: 0x060018C8 RID: 6344 RVA: 0x0007D7F2 File Offset: 0x0007B9F2
		public DefaultEncyclopediaFactionPage()
		{
			base.HomePageOrderIndex = 400;
		}

		// Token: 0x060018C9 RID: 6345 RVA: 0x0007D805 File Offset: 0x0007BA05
		public override string GetViewFullyQualifiedName()
		{
			return "EncyclopediaFactionPage";
		}

		// Token: 0x060018CA RID: 6346 RVA: 0x0007D80C File Offset: 0x0007BA0C
		public override TextObject GetName()
		{
			return GameTexts.FindText("str_kingdoms_group", null);
		}

		// Token: 0x060018CB RID: 6347 RVA: 0x0007D819 File Offset: 0x0007BA19
		public override TextObject GetDescriptionText()
		{
			return GameTexts.FindText("str_faction_description", null);
		}

		// Token: 0x060018CC RID: 6348 RVA: 0x0007D826 File Offset: 0x0007BA26
		public override string GetStringID()
		{
			return "EncyclopediaKingdom";
		}

		// Token: 0x060018CD RID: 6349 RVA: 0x0007D82D File Offset: 0x0007BA2D
		public override MBObjectBase GetObject(string typeName, string stringID)
		{
			return Campaign.Current.CampaignObjectManager.Find<Kingdom>(stringID);
		}

		// Token: 0x060018CE RID: 6350 RVA: 0x0007D840 File Offset: 0x0007BA40
		public override bool IsValidEncyclopediaItem(object o)
		{
			IFaction faction = o as IFaction;
			return faction != null && !faction.IsBanditFaction;
		}

		// Token: 0x060018CF RID: 6351 RVA: 0x0007D862 File Offset: 0x0007BA62
		protected override IEnumerable<EncyclopediaListItem> InitializeListItems()
		{
			foreach (Kingdom kingdom in Kingdom.All)
			{
				if (this.IsValidEncyclopediaItem(kingdom))
				{
					yield return new EncyclopediaListItem(kingdom, kingdom.Name.ToString(), "", kingdom.StringId, base.GetIdentifier(typeof(Kingdom)), true, null);
				}
			}
			List<Kingdom>.Enumerator enumerator = default(List<Kingdom>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x060018D0 RID: 6352 RVA: 0x0007D874 File Offset: 0x0007BA74
		protected override IEnumerable<EncyclopediaFilterGroup> InitializeFilterItems()
		{
			List<EncyclopediaFilterGroup> list = new List<EncyclopediaFilterGroup>();
			List<EncyclopediaFilterItem> list2 = new List<EncyclopediaFilterItem>();
			list2 = new List<EncyclopediaFilterItem>();
			list2.Add(new EncyclopediaFilterItem(new TextObject("{=lEHjxPTs}Ally", null), (object f) => FactionManager.IsAlliedWithFaction((IFaction)f, Hero.MainHero.MapFaction)));
			list2.Add(new EncyclopediaFilterItem(new TextObject("{=sPmQz21k}Enemy", null), (object f) => FactionManager.IsAtWarAgainstFaction((IFaction)f, Hero.MainHero.MapFaction) && !((IFaction)f).IsBanditFaction));
			list2.Add(new EncyclopediaFilterItem(new TextObject("{=3PzgpFGq}Neutral", null), (object f) => FactionManager.IsNeutralWithFaction((IFaction)f, Hero.MainHero.MapFaction)));
			list.Add(new EncyclopediaFilterGroup(list2, new TextObject("{=L7wn49Uz}Diplomacy", null)));
			return list;
		}

		// Token: 0x060018D1 RID: 6353 RVA: 0x0007D948 File Offset: 0x0007BB48
		protected override IEnumerable<EncyclopediaSortController> InitializeSortControllers()
		{
			return new List<EncyclopediaSortController>
			{
				new EncyclopediaSortController(GameTexts.FindText("str_total_strength", null), new DefaultEncyclopediaFactionPage.EncyclopediaListKingdomTotalStrengthComparer()),
				new EncyclopediaSortController(GameTexts.FindText("str_fiefs", null), new DefaultEncyclopediaFactionPage.EncyclopediaListKingdomFiefsComparer()),
				new EncyclopediaSortController(GameTexts.FindText("str_clans", null), new DefaultEncyclopediaFactionPage.EncyclopediaListKingdomClanComparer())
			};
		}

		// Token: 0x02000531 RID: 1329
		private class EncyclopediaListKingdomTotalStrengthComparer : DefaultEncyclopediaFactionPage.EncyclopediaListKingdomComparer
		{
			// Token: 0x0600448F RID: 17551 RVA: 0x00148187 File Offset: 0x00146387
			public override int Compare(EncyclopediaListItem x, EncyclopediaListItem y)
			{
				return base.CompareKingdoms(x, y, DefaultEncyclopediaFactionPage.EncyclopediaListKingdomTotalStrengthComparer._comparison);
			}

			// Token: 0x06004490 RID: 17552 RVA: 0x00148198 File Offset: 0x00146398
			public override string GetComparedValueText(EncyclopediaListItem item)
			{
				Kingdom kingdom;
				if ((kingdom = (item.Object as Kingdom)) != null)
				{
					return ((int)kingdom.TotalStrength).ToString();
				}
				Debug.FailedAssert("Unable to get the total strength of a non-kingdom object.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\Encyclopedia\\Pages\\DefaultEncyclopediaFactionPage.cs", "GetComparedValueText", 107);
				return "";
			}

			// Token: 0x04001624 RID: 5668
			private static Func<Kingdom, Kingdom, int> _comparison = (Kingdom k1, Kingdom k2) => k1.TotalStrength.CompareTo(k2.TotalStrength);
		}

		// Token: 0x02000532 RID: 1330
		private class EncyclopediaListKingdomFiefsComparer : DefaultEncyclopediaFactionPage.EncyclopediaListKingdomComparer
		{
			// Token: 0x06004493 RID: 17555 RVA: 0x001481FE File Offset: 0x001463FE
			public override int Compare(EncyclopediaListItem x, EncyclopediaListItem y)
			{
				return base.CompareKingdoms(x, y, DefaultEncyclopediaFactionPage.EncyclopediaListKingdomFiefsComparer._comparison);
			}

			// Token: 0x06004494 RID: 17556 RVA: 0x00148210 File Offset: 0x00146410
			public override string GetComparedValueText(EncyclopediaListItem item)
			{
				Kingdom kingdom;
				if ((kingdom = (item.Object as Kingdom)) != null)
				{
					return kingdom.Fiefs.Count.ToString();
				}
				Debug.FailedAssert("Unable to get the fief count from a non-kingdom object.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\Encyclopedia\\Pages\\DefaultEncyclopediaFactionPage.cs", "GetComparedValueText", 128);
				return "";
			}

			// Token: 0x04001625 RID: 5669
			private static Func<Kingdom, Kingdom, int> _comparison = (Kingdom k1, Kingdom k2) => k1.Fiefs.Count.CompareTo(k2.Fiefs.Count);
		}

		// Token: 0x02000533 RID: 1331
		private class EncyclopediaListKingdomClanComparer : DefaultEncyclopediaFactionPage.EncyclopediaListKingdomComparer
		{
			// Token: 0x06004497 RID: 17559 RVA: 0x0014827D File Offset: 0x0014647D
			public override int Compare(EncyclopediaListItem x, EncyclopediaListItem y)
			{
				return base.CompareKingdoms(x, y, DefaultEncyclopediaFactionPage.EncyclopediaListKingdomClanComparer._comparison);
			}

			// Token: 0x06004498 RID: 17560 RVA: 0x0014828C File Offset: 0x0014648C
			public override string GetComparedValueText(EncyclopediaListItem item)
			{
				Kingdom kingdom;
				if ((kingdom = (item.Object as Kingdom)) != null)
				{
					return kingdom.Clans.Count.ToString();
				}
				Debug.FailedAssert("Unable to get the clan count from a non-kingdom object.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\Encyclopedia\\Pages\\DefaultEncyclopediaFactionPage.cs", "GetComparedValueText", 149);
				return "";
			}

			// Token: 0x04001626 RID: 5670
			private static Func<Kingdom, Kingdom, int> _comparison = (Kingdom k1, Kingdom k2) => k1.Clans.Count.CompareTo(k2.Clans.Count);
		}

		// Token: 0x02000534 RID: 1332
		public abstract class EncyclopediaListKingdomComparer : EncyclopediaListItemComparerBase
		{
			// Token: 0x0600449B RID: 17563 RVA: 0x001482FC File Offset: 0x001464FC
			public int CompareKingdoms(EncyclopediaListItem x, EncyclopediaListItem y, Func<Kingdom, Kingdom, int> comparison)
			{
				Kingdom arg;
				Kingdom arg2;
				if ((arg = (x.Object as Kingdom)) == null || (arg2 = (y.Object as Kingdom)) == null)
				{
					Debug.FailedAssert("Both objects should be kingdoms.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\Encyclopedia\\Pages\\DefaultEncyclopediaFactionPage.cs", "CompareKingdoms", 164);
					return 0;
				}
				int num = comparison(arg, arg2) * (base.IsAscending ? 1 : -1);
				if (num == 0)
				{
					return base.ResolveEquality(x, y);
				}
				return num;
			}
		}
	}
}
