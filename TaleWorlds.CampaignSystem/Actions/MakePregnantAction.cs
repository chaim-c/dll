using System;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x0200044F RID: 1103
	public static class MakePregnantAction
	{
		// Token: 0x060040F7 RID: 16631 RVA: 0x00140A5E File Offset: 0x0013EC5E
		private static void ApplyInternal(Hero mother)
		{
			mother.IsPregnant = true;
			CampaignEventDispatcher.Instance.OnChildConceived(mother);
		}

		// Token: 0x060040F8 RID: 16632 RVA: 0x00140A72 File Offset: 0x0013EC72
		public static void Apply(Hero mother)
		{
			MakePregnantAction.ApplyInternal(mother);
		}
	}
}
