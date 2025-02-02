using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.Encyclopedia.Pages
{
	// Token: 0x02000161 RID: 353
	[EncyclopediaModel(new Type[]
	{
		typeof(Concept)
	})]
	public class DefaultEncyclopediaConceptPage : EncyclopediaPage
	{
		// Token: 0x060018BF RID: 6335 RVA: 0x0007D59A File Offset: 0x0007B79A
		public DefaultEncyclopediaConceptPage()
		{
			base.HomePageOrderIndex = 600;
		}

		// Token: 0x060018C0 RID: 6336 RVA: 0x0007D5AD File Offset: 0x0007B7AD
		protected override IEnumerable<EncyclopediaListItem> InitializeListItems()
		{
			foreach (Concept concept in Concept.All)
			{
				yield return new EncyclopediaListItem(concept, concept.Title.ToString(), concept.Description.ToString(), concept.StringId, base.GetIdentifier(typeof(Concept)), true, null);
			}
			List<Concept>.Enumerator enumerator = default(List<Concept>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x060018C1 RID: 6337 RVA: 0x0007D5C0 File Offset: 0x0007B7C0
		protected override IEnumerable<EncyclopediaFilterGroup> InitializeFilterItems()
		{
			List<EncyclopediaFilterGroup> list = new List<EncyclopediaFilterGroup>();
			List<EncyclopediaFilterItem> list2 = new List<EncyclopediaFilterItem>();
			list2.Add(new EncyclopediaFilterItem(new TextObject("{=uauMia0D} Characters", null), (object c) => Concept.IsGroupMember("Characters", (Concept)c)));
			list2.Add(new EncyclopediaFilterItem(new TextObject("{=cwRkqIt4} Kingdoms", null), (object c) => Concept.IsGroupMember("Kingdoms", (Concept)c)));
			list2.Add(new EncyclopediaFilterItem(new TextObject("{=x6knoNnC} Clans", null), (object c) => Concept.IsGroupMember("Clans", (Concept)c)));
			list2.Add(new EncyclopediaFilterItem(new TextObject("{=GYzkb4iB} Parties", null), (object c) => Concept.IsGroupMember("Parties", (Concept)c)));
			list2.Add(new EncyclopediaFilterItem(new TextObject("{=u6GM5Spa} Armies", null), (object c) => Concept.IsGroupMember("Armies", (Concept)c)));
			list2.Add(new EncyclopediaFilterItem(new TextObject("{=zPYRGJtD} Troops", null), (object c) => Concept.IsGroupMember("Troops", (Concept)c)));
			list2.Add(new EncyclopediaFilterItem(new TextObject("{=3PUkH5Zf} Items", null), (object c) => Concept.IsGroupMember("Items", (Concept)c)));
			list2.Add(new EncyclopediaFilterItem(new TextObject("{=xKVBAL3m} Campaign Issues", null), (object c) => Concept.IsGroupMember("CampaignIssues", (Concept)c)));
			list.Add(new EncyclopediaFilterGroup(list2, new TextObject("{=tBx7XXps}Types", null)));
			return list;
		}

		// Token: 0x060018C2 RID: 6338 RVA: 0x0007D797 File Offset: 0x0007B997
		protected override IEnumerable<EncyclopediaSortController> InitializeSortControllers()
		{
			return new List<EncyclopediaSortController>();
		}

		// Token: 0x060018C3 RID: 6339 RVA: 0x0007D79E File Offset: 0x0007B99E
		public override string GetViewFullyQualifiedName()
		{
			return "EncyclopediaConceptPage";
		}

		// Token: 0x060018C4 RID: 6340 RVA: 0x0007D7A5 File Offset: 0x0007B9A5
		public override TextObject GetName()
		{
			return GameTexts.FindText("str_concepts", null);
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x0007D7B2 File Offset: 0x0007B9B2
		public override TextObject GetDescriptionText()
		{
			return GameTexts.FindText("str_concepts_description", null);
		}

		// Token: 0x060018C6 RID: 6342 RVA: 0x0007D7BF File Offset: 0x0007B9BF
		public override string GetStringID()
		{
			return "EncyclopediaConcept";
		}

		// Token: 0x060018C7 RID: 6343 RVA: 0x0007D7C8 File Offset: 0x0007B9C8
		public override bool IsValidEncyclopediaItem(object o)
		{
			Concept concept = o as Concept;
			return concept != null && concept.Title != null && concept.Description != null;
		}
	}
}
