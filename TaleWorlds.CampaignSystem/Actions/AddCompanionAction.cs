using System;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x02000423 RID: 1059
	public static class AddCompanionAction
	{
		// Token: 0x0600403F RID: 16447 RVA: 0x0013C5AC File Offset: 0x0013A7AC
		private static void ApplyInternal(Clan clan, Hero companion)
		{
			if (companion.CompanionOf != null)
			{
				RemoveCompanionAction.ApplyByFire(companion.CompanionOf, companion);
			}
			companion.CompanionOf = clan;
			CampaignEventDispatcher.Instance.OnNewCompanionAdded(companion);
		}

		// Token: 0x06004040 RID: 16448 RVA: 0x0013C5D4 File Offset: 0x0013A7D4
		public static void Apply(Clan clan, Hero companion)
		{
			AddCompanionAction.ApplyInternal(clan, companion);
		}
	}
}
