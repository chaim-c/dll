using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.CampaignSystem.Encyclopedia.Pages
{
	// Token: 0x02000160 RID: 352
	[EncyclopediaModel(new Type[]
	{
		typeof(Clan)
	})]
	public class DefaultEncyclopediaClanPage : EncyclopediaPage
	{
		// Token: 0x060018B5 RID: 6325 RVA: 0x0007D1DF File Offset: 0x0007B3DF
		public DefaultEncyclopediaClanPage()
		{
			base.HomePageOrderIndex = 500;
		}

		// Token: 0x060018B6 RID: 6326 RVA: 0x0007D1F2 File Offset: 0x0007B3F2
		protected override IEnumerable<EncyclopediaListItem> InitializeListItems()
		{
			foreach (Clan clan in Clan.NonBanditFactions)
			{
				if (this.IsValidEncyclopediaItem(clan))
				{
					yield return new EncyclopediaListItem(clan, clan.Name.ToString(), "", clan.StringId, base.GetIdentifier(typeof(Clan)), true, null);
				}
			}
			IEnumerator<Clan> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060018B7 RID: 6327 RVA: 0x0007D204 File Offset: 0x0007B404
		protected override IEnumerable<EncyclopediaFilterGroup> InitializeFilterItems()
		{
			List<EncyclopediaFilterGroup> list = new List<EncyclopediaFilterGroup>();
			List<EncyclopediaFilterItem> list2 = new List<EncyclopediaFilterItem>();
			list2.Add(new EncyclopediaFilterItem(new TextObject("{=QwpHoMJu}Minor", null), (object f) => ((IFaction)f).IsMinorFaction));
			list.Add(new EncyclopediaFilterGroup(list2, new TextObject("{=zMMqgxb1}Type", null)));
			List<EncyclopediaFilterItem> list3 = new List<EncyclopediaFilterItem>();
			list3.Add(new EncyclopediaFilterItem(new TextObject("{=lEHjxPTs}Ally", null), (object f) => FactionManager.IsAlliedWithFaction((IFaction)f, Hero.MainHero.MapFaction)));
			list3.Add(new EncyclopediaFilterItem(new TextObject("{=sPmQz21k}Enemy", null), (object f) => FactionManager.IsAtWarAgainstFaction((IFaction)f, Hero.MainHero.MapFaction) && !((IFaction)f).IsBanditFaction));
			list3.Add(new EncyclopediaFilterItem(new TextObject("{=3PzgpFGq}Neutral", null), (object f) => FactionManager.IsNeutralWithFaction((IFaction)f, Hero.MainHero.MapFaction)));
			list.Add(new EncyclopediaFilterGroup(list3, new TextObject("{=L7wn49Uz}Diplomacy", null)));
			List<EncyclopediaFilterItem> list4 = new List<EncyclopediaFilterItem>();
			list4.Add(new EncyclopediaFilterItem(new TextObject("{=SlubkZ1A}Eliminated", null), (object f) => ((IFaction)f).IsEliminated));
			list4.Add(new EncyclopediaFilterItem(new TextObject("{=YRbSBxqT}Active", null), (object f) => !((IFaction)f).IsEliminated));
			list.Add(new EncyclopediaFilterGroup(list4, new TextObject("{=DXczLzml}Status", null)));
			List<EncyclopediaFilterItem> list5 = new List<EncyclopediaFilterItem>();
			using (List<CultureObject>.Enumerator enumerator = (from x in Game.Current.ObjectManager.GetObjectTypeList<CultureObject>()
			orderby !x.IsMainCulture descending
			select x).ThenBy((CultureObject f) => f.Name.ToString()).ToList<CultureObject>().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					CultureObject culture = enumerator.Current;
					if (culture.StringId != "neutral_culture" && !culture.IsBandit)
					{
						list5.Add(new EncyclopediaFilterItem(culture.Name, (object c) => ((IFaction)c).Culture == culture));
					}
				}
			}
			list.Add(new EncyclopediaFilterGroup(list5, GameTexts.FindText("str_culture", null)));
			return list;
		}

		// Token: 0x060018B8 RID: 6328 RVA: 0x0007D4BC File Offset: 0x0007B6BC
		protected override IEnumerable<EncyclopediaSortController> InitializeSortControllers()
		{
			return new List<EncyclopediaSortController>
			{
				new EncyclopediaSortController(new TextObject("{=qtII2HbK}Wealth", null), new DefaultEncyclopediaClanPage.EncyclopediaListClanWealthComparer()),
				new EncyclopediaSortController(new TextObject("{=cc1d7mkq}Tier", null), new DefaultEncyclopediaClanPage.EncyclopediaListClanTierComparer()),
				new EncyclopediaSortController(GameTexts.FindText("str_strength", null), new DefaultEncyclopediaClanPage.EncyclopediaListClanStrengthComparer()),
				new EncyclopediaSortController(GameTexts.FindText("str_fiefs", null), new DefaultEncyclopediaClanPage.EncyclopediaListClanFiefComparer()),
				new EncyclopediaSortController(GameTexts.FindText("str_members", null), new DefaultEncyclopediaClanPage.EncyclopediaListClanMemberComparer())
			};
		}

		// Token: 0x060018B9 RID: 6329 RVA: 0x0007D555 File Offset: 0x0007B755
		public override string GetViewFullyQualifiedName()
		{
			return "EncyclopediaClanPage";
		}

		// Token: 0x060018BA RID: 6330 RVA: 0x0007D55C File Offset: 0x0007B75C
		public override TextObject GetName()
		{
			return GameTexts.FindText("str_clans", null);
		}

		// Token: 0x060018BB RID: 6331 RVA: 0x0007D569 File Offset: 0x0007B769
		public override TextObject GetDescriptionText()
		{
			return GameTexts.FindText("str_clan_description", null);
		}

		// Token: 0x060018BC RID: 6332 RVA: 0x0007D576 File Offset: 0x0007B776
		public override string GetStringID()
		{
			return "EncyclopediaClan";
		}

		// Token: 0x060018BD RID: 6333 RVA: 0x0007D57D File Offset: 0x0007B77D
		public override MBObjectBase GetObject(string typeName, string stringID)
		{
			return Campaign.Current.CampaignObjectManager.Find<Clan>(stringID);
		}

		// Token: 0x060018BE RID: 6334 RVA: 0x0007D58F File Offset: 0x0007B78F
		public override bool IsValidEncyclopediaItem(object o)
		{
			return o is IFaction;
		}

		// Token: 0x02000526 RID: 1318
		private class EncyclopediaListClanWealthComparer : DefaultEncyclopediaClanPage.EncyclopediaListClanComparer
		{
			// Token: 0x06004450 RID: 17488 RVA: 0x00147928 File Offset: 0x00145B28
			private string GetClanWealthStatusText(Clan _clan)
			{
				string result = string.Empty;
				if (_clan.Leader.Gold < 15000)
				{
					result = new TextObject("{=SixPXaNh}Very Poor", null).ToString();
				}
				else if (_clan.Leader.Gold < 45000)
				{
					result = new TextObject("{=poorWealthStatus}Poor", null).ToString();
				}
				else if (_clan.Leader.Gold < 135000)
				{
					result = new TextObject("{=averageWealthStatus}Average", null).ToString();
				}
				else if (_clan.Leader.Gold < 405000)
				{
					result = new TextObject("{=UbRqC0Yz}Rich", null).ToString();
				}
				else
				{
					result = new TextObject("{=oJmRg2ms}Very Rich", null).ToString();
				}
				return result;
			}

			// Token: 0x06004451 RID: 17489 RVA: 0x001479E4 File Offset: 0x00145BE4
			public override int Compare(EncyclopediaListItem x, EncyclopediaListItem y)
			{
				return base.CompareClans(x, y, DefaultEncyclopediaClanPage.EncyclopediaListClanWealthComparer._comparison);
			}

			// Token: 0x06004452 RID: 17490 RVA: 0x001479F4 File Offset: 0x00145BF4
			public override string GetComparedValueText(EncyclopediaListItem item)
			{
				Clan clan;
				if ((clan = (item.Object as Clan)) != null)
				{
					return this.GetClanWealthStatusText(clan);
				}
				Debug.FailedAssert("Unable to get the gold of a non-clan object.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\Encyclopedia\\Pages\\DefaultEncyclopediaClanPage.cs", "GetComparedValueText", 157);
				return "";
			}

			// Token: 0x04001602 RID: 5634
			private static Func<Clan, Clan, int> _comparison = (Clan c1, Clan c2) => c1.Gold.CompareTo(c2.Gold);
		}

		// Token: 0x02000527 RID: 1319
		private class EncyclopediaListClanTierComparer : DefaultEncyclopediaClanPage.EncyclopediaListClanComparer
		{
			// Token: 0x06004455 RID: 17493 RVA: 0x00147A55 File Offset: 0x00145C55
			public override int Compare(EncyclopediaListItem x, EncyclopediaListItem y)
			{
				return base.CompareClans(x, y, DefaultEncyclopediaClanPage.EncyclopediaListClanTierComparer._comparison);
			}

			// Token: 0x06004456 RID: 17494 RVA: 0x00147A64 File Offset: 0x00145C64
			public override string GetComparedValueText(EncyclopediaListItem item)
			{
				Clan clan;
				if ((clan = (item.Object as Clan)) != null)
				{
					return clan.Tier.ToString();
				}
				Debug.FailedAssert("Unable to get the tier of a non-clan object.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\Encyclopedia\\Pages\\DefaultEncyclopediaClanPage.cs", "GetComparedValueText", 178);
				return "";
			}

			// Token: 0x04001603 RID: 5635
			private static Func<Clan, Clan, int> _comparison = (Clan c1, Clan c2) => c1.Tier.CompareTo(c2.Tier);
		}

		// Token: 0x02000528 RID: 1320
		private class EncyclopediaListClanStrengthComparer : DefaultEncyclopediaClanPage.EncyclopediaListClanComparer
		{
			// Token: 0x06004459 RID: 17497 RVA: 0x00147ACC File Offset: 0x00145CCC
			public override int Compare(EncyclopediaListItem x, EncyclopediaListItem y)
			{
				return base.CompareClans(x, y, DefaultEncyclopediaClanPage.EncyclopediaListClanStrengthComparer._comparison);
			}

			// Token: 0x0600445A RID: 17498 RVA: 0x00147ADC File Offset: 0x00145CDC
			public override string GetComparedValueText(EncyclopediaListItem item)
			{
				Clan clan;
				if ((clan = (item.Object as Clan)) != null)
				{
					return ((int)clan.TotalStrength).ToString();
				}
				Debug.FailedAssert("Unable to get the strength of a non-clan object.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\Encyclopedia\\Pages\\DefaultEncyclopediaClanPage.cs", "GetComparedValueText", 199);
				return "";
			}

			// Token: 0x04001604 RID: 5636
			private static Func<Clan, Clan, int> _comparison = (Clan c1, Clan c2) => c1.TotalStrength.CompareTo(c2.TotalStrength);
		}

		// Token: 0x02000529 RID: 1321
		private class EncyclopediaListClanFiefComparer : DefaultEncyclopediaClanPage.EncyclopediaListClanComparer
		{
			// Token: 0x0600445D RID: 17501 RVA: 0x00147B45 File Offset: 0x00145D45
			public override int Compare(EncyclopediaListItem x, EncyclopediaListItem y)
			{
				return base.CompareClans(x, y, DefaultEncyclopediaClanPage.EncyclopediaListClanFiefComparer._comparison);
			}

			// Token: 0x0600445E RID: 17502 RVA: 0x00147B54 File Offset: 0x00145D54
			public override string GetComparedValueText(EncyclopediaListItem item)
			{
				Clan clan;
				if ((clan = (item.Object as Clan)) != null)
				{
					return clan.Fiefs.Count.ToString();
				}
				Debug.FailedAssert("Unable to get the fief count of a non-clan object.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\Encyclopedia\\Pages\\DefaultEncyclopediaClanPage.cs", "GetComparedValueText", 220);
				return "";
			}

			// Token: 0x04001605 RID: 5637
			private static Func<Clan, Clan, int> _comparison = (Clan c1, Clan c2) => c1.Fiefs.Count.CompareTo(c2.Fiefs.Count);
		}

		// Token: 0x0200052A RID: 1322
		private class EncyclopediaListClanMemberComparer : DefaultEncyclopediaClanPage.EncyclopediaListClanComparer
		{
			// Token: 0x06004461 RID: 17505 RVA: 0x00147BC1 File Offset: 0x00145DC1
			public override int Compare(EncyclopediaListItem x, EncyclopediaListItem y)
			{
				return base.CompareClans(x, y, DefaultEncyclopediaClanPage.EncyclopediaListClanMemberComparer._comparison);
			}

			// Token: 0x06004462 RID: 17506 RVA: 0x00147BD0 File Offset: 0x00145DD0
			public override string GetComparedValueText(EncyclopediaListItem item)
			{
				Clan clan;
				if ((clan = (item.Object as Clan)) != null)
				{
					return clan.Heroes.Count.ToString();
				}
				Debug.FailedAssert("Unable to get members of a non-clan object.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\Encyclopedia\\Pages\\DefaultEncyclopediaClanPage.cs", "GetComparedValueText", 241);
				return "";
			}

			// Token: 0x04001606 RID: 5638
			private static Func<Clan, Clan, int> _comparison = (Clan c1, Clan c2) => c1.Heroes.Count.CompareTo(c2.Heroes.Count);
		}

		// Token: 0x0200052B RID: 1323
		public abstract class EncyclopediaListClanComparer : EncyclopediaListItemComparerBase
		{
			// Token: 0x06004465 RID: 17509 RVA: 0x00147C40 File Offset: 0x00145E40
			public int CompareClans(EncyclopediaListItem x, EncyclopediaListItem y, Func<Clan, Clan, int> comparison)
			{
				Clan arg;
				Clan arg2;
				if ((arg = (x.Object as Clan)) == null || (arg2 = (y.Object as Clan)) == null)
				{
					Debug.FailedAssert("Both objects should be clans.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\Encyclopedia\\Pages\\DefaultEncyclopediaClanPage.cs", "CompareClans", 256);
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
