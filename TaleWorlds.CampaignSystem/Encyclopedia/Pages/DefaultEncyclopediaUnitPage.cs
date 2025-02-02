using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.Encyclopedia.Pages
{
	// Token: 0x02000165 RID: 357
	[EncyclopediaModel(new Type[]
	{
		typeof(CharacterObject)
	})]
	public class DefaultEncyclopediaUnitPage : EncyclopediaPage
	{
		// Token: 0x060018E7 RID: 6375 RVA: 0x0007E1CC File Offset: 0x0007C3CC
		public DefaultEncyclopediaUnitPage()
		{
			base.HomePageOrderIndex = 300;
		}

		// Token: 0x060018E8 RID: 6376 RVA: 0x0007E1DF File Offset: 0x0007C3DF
		protected override IEnumerable<EncyclopediaListItem> InitializeListItems()
		{
			using (List<CharacterObject>.Enumerator enumerator = CharacterObject.All.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					CharacterObject character = enumerator.Current;
					if (this.IsValidEncyclopediaItem(character))
					{
						yield return new EncyclopediaListItem(character, character.Name.ToString(), "", character.StringId, base.GetIdentifier(typeof(CharacterObject)), true, delegate()
						{
							InformationManager.ShowTooltip(typeof(CharacterObject), new object[]
							{
								character
							});
						});
					}
				}
			}
			List<CharacterObject>.Enumerator enumerator = default(List<CharacterObject>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x060018E9 RID: 6377 RVA: 0x0007E1F0 File Offset: 0x0007C3F0
		protected override IEnumerable<EncyclopediaFilterGroup> InitializeFilterItems()
		{
			List<EncyclopediaFilterGroup> list = new List<EncyclopediaFilterGroup>();
			List<EncyclopediaFilterItem> list2 = new List<EncyclopediaFilterItem>();
			list2.Add(new EncyclopediaFilterItem(new TextObject("{=1Bm1Wk1v}Infantry", null), (object s) => ((CharacterObject)s).IsInfantry));
			list2.Add(new EncyclopediaFilterItem(new TextObject("{=bIiBytSB}Archers", null), (object s) => ((CharacterObject)s).IsRanged && !((CharacterObject)s).IsMounted));
			list2.Add(new EncyclopediaFilterItem(new TextObject("{=YVGtcLHF}Cavalry", null), (object s) => ((CharacterObject)s).IsMounted && !((CharacterObject)s).IsRanged));
			list2.Add(new EncyclopediaFilterItem(new TextObject("{=I1CMeL9R}Mounted Archers", null), (object s) => ((CharacterObject)s).IsRanged && ((CharacterObject)s).IsMounted));
			List<EncyclopediaFilterItem> filters = list2;
			list.Add(new EncyclopediaFilterGroup(filters, new TextObject("{=zMMqgxb1}Type", null)));
			List<EncyclopediaFilterItem> list3 = new List<EncyclopediaFilterItem>();
			list3.Add(new EncyclopediaFilterItem(GameTexts.FindText("str_occupation", "Soldier"), (object s) => ((CharacterObject)s).Occupation == Occupation.Soldier));
			list3.Add(new EncyclopediaFilterItem(GameTexts.FindText("str_occupation", "Mercenary"), (object s) => ((CharacterObject)s).Occupation == Occupation.Mercenary));
			list3.Add(new EncyclopediaFilterItem(GameTexts.FindText("str_occupation", "Bandit"), (object s) => ((CharacterObject)s).Occupation == Occupation.Bandit));
			List<EncyclopediaFilterItem> filters2 = list3;
			list.Add(new EncyclopediaFilterGroup(filters2, new TextObject("{=GZxFIeiJ}Occupation", null)));
			List<EncyclopediaFilterItem> list4 = new List<EncyclopediaFilterItem>();
			using (List<CultureObject>.Enumerator enumerator = (from x in Game.Current.ObjectManager.GetObjectTypeList<CultureObject>()
			orderby !x.IsMainCulture descending
			select x).ThenBy((CultureObject f) => f.Name.ToString()).ToList<CultureObject>().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					CultureObject culture = enumerator.Current;
					if (culture.StringId != "neutral_culture")
					{
						list4.Add(new EncyclopediaFilterItem(culture.Name, (object c) => ((CharacterObject)c).Culture == culture));
					}
				}
			}
			list.Add(new EncyclopediaFilterGroup(list4, GameTexts.FindText("str_culture", null)));
			return list;
		}

		// Token: 0x060018EA RID: 6378 RVA: 0x0007E4BC File Offset: 0x0007C6BC
		protected override IEnumerable<EncyclopediaSortController> InitializeSortControllers()
		{
			return new List<EncyclopediaSortController>
			{
				new EncyclopediaSortController(new TextObject("{=cc1d7mkq}Tier", null), new DefaultEncyclopediaUnitPage.EncyclopediaListUnitTierComparer()),
				new EncyclopediaSortController(GameTexts.FindText("str_level_tag", null), new DefaultEncyclopediaUnitPage.EncyclopediaListUnitLevelComparer())
			};
		}

		// Token: 0x060018EB RID: 6379 RVA: 0x0007E4F9 File Offset: 0x0007C6F9
		public override string GetViewFullyQualifiedName()
		{
			return "EncyclopediaUnitPage";
		}

		// Token: 0x060018EC RID: 6380 RVA: 0x0007E500 File Offset: 0x0007C700
		public override TextObject GetName()
		{
			return GameTexts.FindText("str_encyclopedia_troops", null);
		}

		// Token: 0x060018ED RID: 6381 RVA: 0x0007E50D File Offset: 0x0007C70D
		public override TextObject GetDescriptionText()
		{
			return GameTexts.FindText("str_unit_description", null);
		}

		// Token: 0x060018EE RID: 6382 RVA: 0x0007E51A File Offset: 0x0007C71A
		public override string GetStringID()
		{
			return "EncyclopediaUnit";
		}

		// Token: 0x060018EF RID: 6383 RVA: 0x0007E524 File Offset: 0x0007C724
		public override bool IsValidEncyclopediaItem(object o)
		{
			CharacterObject characterObject = o as CharacterObject;
			return characterObject != null && !characterObject.IsTemplate && characterObject != null && !characterObject.HiddenInEncylopedia && (characterObject.Occupation == Occupation.Soldier || characterObject.Occupation == Occupation.Mercenary || characterObject.Occupation == Occupation.Bandit || characterObject.Occupation == Occupation.Gangster || characterObject.Occupation == Occupation.CaravanGuard || (characterObject.Occupation == Occupation.Villager && characterObject.UpgradeTargets.Length != 0));
		}

		// Token: 0x0200054A RID: 1354
		private class EncyclopediaListUnitTierComparer : DefaultEncyclopediaUnitPage.EncyclopediaListUnitComparer
		{
			// Token: 0x06004507 RID: 17671 RVA: 0x0014980B File Offset: 0x00147A0B
			public override int Compare(EncyclopediaListItem x, EncyclopediaListItem y)
			{
				return base.CompareUnits(x, y, DefaultEncyclopediaUnitPage.EncyclopediaListUnitTierComparer._comparison);
			}

			// Token: 0x06004508 RID: 17672 RVA: 0x0014981C File Offset: 0x00147A1C
			public override string GetComparedValueText(EncyclopediaListItem item)
			{
				CharacterObject characterObject;
				if ((characterObject = (item.Object as CharacterObject)) != null)
				{
					return characterObject.Tier.ToString();
				}
				Debug.FailedAssert("Unable to get the tier of a non-character object.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\Encyclopedia\\Pages\\DefaultEncyclopediaUnitPage.cs", "GetComparedValueText", 138);
				return "";
			}

			// Token: 0x04001657 RID: 5719
			private static Func<CharacterObject, CharacterObject, int> _comparison = (CharacterObject c1, CharacterObject c2) => c1.Tier.CompareTo(c2.Tier);
		}

		// Token: 0x0200054B RID: 1355
		private class EncyclopediaListUnitLevelComparer : DefaultEncyclopediaUnitPage.EncyclopediaListUnitComparer
		{
			// Token: 0x0600450B RID: 17675 RVA: 0x00149884 File Offset: 0x00147A84
			public override int Compare(EncyclopediaListItem x, EncyclopediaListItem y)
			{
				return base.CompareUnits(x, y, DefaultEncyclopediaUnitPage.EncyclopediaListUnitLevelComparer._comparison);
			}

			// Token: 0x0600450C RID: 17676 RVA: 0x00149894 File Offset: 0x00147A94
			public override string GetComparedValueText(EncyclopediaListItem item)
			{
				CharacterObject characterObject;
				if ((characterObject = (item.Object as CharacterObject)) != null)
				{
					return characterObject.Level.ToString();
				}
				Debug.FailedAssert("Unable to get the level of a non-character object.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\Encyclopedia\\Pages\\DefaultEncyclopediaUnitPage.cs", "GetComparedValueText", 159);
				return "";
			}

			// Token: 0x04001658 RID: 5720
			private static Func<CharacterObject, CharacterObject, int> _comparison = (CharacterObject c1, CharacterObject c2) => c1.Level.CompareTo(c2.Level);
		}

		// Token: 0x0200054C RID: 1356
		public abstract class EncyclopediaListUnitComparer : EncyclopediaListItemComparerBase
		{
			// Token: 0x0600450F RID: 17679 RVA: 0x001498FC File Offset: 0x00147AFC
			public int CompareUnits(EncyclopediaListItem x, EncyclopediaListItem y, Func<CharacterObject, CharacterObject, int> comparison)
			{
				CharacterObject arg;
				CharacterObject arg2;
				if ((arg = (x.Object as CharacterObject)) == null || (arg2 = (y.Object as CharacterObject)) == null)
				{
					Debug.FailedAssert("Both objects should be character objects.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\Encyclopedia\\Pages\\DefaultEncyclopediaUnitPage.cs", "CompareUnits", 174);
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
