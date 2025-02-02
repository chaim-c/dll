using System;
using System.Collections.Generic;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu.TroopSelection
{
	// Token: 0x02000090 RID: 144
	public class TroopItemComparer : IComparer<TroopSelectionItemVM>
	{
		// Token: 0x06000E42 RID: 3650 RVA: 0x00039924 File Offset: 0x00037B24
		public int Compare(TroopSelectionItemVM x, TroopSelectionItemVM y)
		{
			int result;
			if (y.Troop.Character.IsPlayerCharacter)
			{
				result = 1;
			}
			else if (y.Troop.Character.IsHero)
			{
				if (x.Troop.Character.IsPlayerCharacter)
				{
					result = -1;
				}
				else if (x.Troop.Character.IsHero)
				{
					result = y.Troop.Character.Level - x.Troop.Character.Level;
				}
				else
				{
					result = 1;
				}
			}
			else if (x.Troop.Character.IsPlayerCharacter || x.Troop.Character.IsHero)
			{
				result = -1;
			}
			else
			{
				result = y.Troop.Character.Level - x.Troop.Character.Level;
			}
			return result;
		}
	}
}
