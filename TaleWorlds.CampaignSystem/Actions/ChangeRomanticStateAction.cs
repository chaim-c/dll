using System;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x02000434 RID: 1076
	public static class ChangeRomanticStateAction
	{
		// Token: 0x06004081 RID: 16513 RVA: 0x0013E334 File Offset: 0x0013C534
		private static void ApplyInternal(Hero hero1, Hero hero2, Romance.RomanceLevelEnum toWhat)
		{
			Romance.SetRomanticState(hero1, hero2, toWhat);
			CampaignEventDispatcher.Instance.OnRomanticStateChanged(hero1, hero2, toWhat);
		}

		// Token: 0x06004082 RID: 16514 RVA: 0x0013E34B File Offset: 0x0013C54B
		public static void Apply(Hero person1, Hero person2, Romance.RomanceLevelEnum toWhat)
		{
			ChangeRomanticStateAction.ApplyInternal(person1, person2, toWhat);
		}
	}
}
