using System;
using TaleWorlds.CampaignSystem.Settlements.Workshops;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x02000448 RID: 1096
	public static class InitializeWorkshopAction
	{
		// Token: 0x060040DC RID: 16604 RVA: 0x0013F764 File Offset: 0x0013D964
		public static void ApplyByNewGame(Workshop workshop, Hero workshopOwner, WorkshopType workshopType)
		{
			workshop.InitializeWorkshop(workshopOwner, workshopType);
			TextObject firstName;
			TextObject fullName;
			NameGenerator.Current.GenerateHeroNameAndHeroFullName(workshopOwner, out firstName, out fullName, true);
			workshopOwner.SetName(fullName, firstName);
			CampaignEventDispatcher.Instance.OnWorkshopInitialized(workshop);
		}
	}
}
