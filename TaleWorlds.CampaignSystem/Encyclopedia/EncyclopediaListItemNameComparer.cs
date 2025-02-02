using System;

namespace TaleWorlds.CampaignSystem.Encyclopedia
{
	// Token: 0x0200015A RID: 346
	internal class EncyclopediaListItemNameComparer : EncyclopediaListItemComparerBase
	{
		// Token: 0x060018A1 RID: 6305 RVA: 0x0007D0FD File Offset: 0x0007B2FD
		public override int Compare(EncyclopediaListItem x, EncyclopediaListItem y)
		{
			return base.ResolveEquality(x, y);
		}

		// Token: 0x060018A2 RID: 6306 RVA: 0x0007D107 File Offset: 0x0007B307
		public override string GetComparedValueText(EncyclopediaListItem item)
		{
			return "";
		}
	}
}
