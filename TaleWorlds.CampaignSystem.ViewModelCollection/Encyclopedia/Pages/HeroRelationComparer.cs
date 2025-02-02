using System;
using System.Collections.Generic;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Pages
{
	// Token: 0x020000BA RID: 186
	public class HeroRelationComparer : IComparer<HeroVM>
	{
		// Token: 0x06001298 RID: 4760 RVA: 0x00048F34 File Offset: 0x00047134
		public HeroRelationComparer(Hero pageHero, bool isAscending)
		{
			this._pageHero = pageHero;
			this._isAscending = isAscending;
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x00048F4C File Offset: 0x0004714C
		int IComparer<HeroVM>.Compare(HeroVM x, HeroVM y)
		{
			int heroRelation = CharacterRelationManager.GetHeroRelation(this._pageHero, x.Hero);
			int heroRelation2 = CharacterRelationManager.GetHeroRelation(this._pageHero, y.Hero);
			return heroRelation.CompareTo(heroRelation2) * (this._isAscending ? 1 : -1);
		}

		// Token: 0x040008A7 RID: 2215
		private readonly Hero _pageHero;

		// Token: 0x040008A8 RID: 2216
		private readonly bool _isAscending;
	}
}
