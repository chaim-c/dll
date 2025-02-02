using System;
using TaleWorlds.CampaignSystem.Settlements.Workshops;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x02000430 RID: 1072
	public static class ChangeOwnerOfWorkshopAction
	{
		// Token: 0x06004074 RID: 16500 RVA: 0x0013E058 File Offset: 0x0013C258
		private static void ApplyInternal(Workshop workshop, Hero newOwner, WorkshopType workshopType, int capital, int cost)
		{
			Hero owner = workshop.Owner;
			workshop.ChangeOwnerOfWorkshop(newOwner, workshopType, capital);
			if (newOwner == Hero.MainHero)
			{
				GiveGoldAction.ApplyBetweenCharacters(newOwner, owner, cost, false);
			}
			if (owner == Hero.MainHero)
			{
				GiveGoldAction.ApplyBetweenCharacters(null, Hero.MainHero, cost, false);
			}
			CampaignEventDispatcher.Instance.OnWorkshopOwnerChanged(workshop, owner);
		}

		// Token: 0x06004075 RID: 16501 RVA: 0x0013E0A9 File Offset: 0x0013C2A9
		public static void ApplyByBankruptcy(Workshop workshop, Hero newOwner, WorkshopType workshopType, int cost)
		{
			ChangeOwnerOfWorkshopAction.ApplyInternal(workshop, newOwner, workshopType, Campaign.Current.Models.WorkshopModel.InitialCapital, cost);
		}

		// Token: 0x06004076 RID: 16502 RVA: 0x0013E0C8 File Offset: 0x0013C2C8
		public static void ApplyByPlayerBuying(Workshop workshop)
		{
			int costForPlayer = Campaign.Current.Models.WorkshopModel.GetCostForPlayer(workshop);
			ChangeOwnerOfWorkshopAction.ApplyInternal(workshop, Hero.MainHero, workshop.WorkshopType, Campaign.Current.Models.WorkshopModel.InitialCapital, costForPlayer);
		}

		// Token: 0x06004077 RID: 16503 RVA: 0x0013E114 File Offset: 0x0013C314
		public static void ApplyByPlayerSelling(Workshop workshop, Hero newOwner, WorkshopType workshopType)
		{
			int costForNotable = Campaign.Current.Models.WorkshopModel.GetCostForNotable(workshop);
			ChangeOwnerOfWorkshopAction.ApplyInternal(workshop, newOwner, workshopType, Campaign.Current.Models.WorkshopModel.InitialCapital, costForNotable);
		}

		// Token: 0x06004078 RID: 16504 RVA: 0x0013E154 File Offset: 0x0013C354
		public static void ApplyByDeath(Workshop workshop, Hero newOwner)
		{
			ChangeOwnerOfWorkshopAction.ApplyInternal(workshop, newOwner, workshop.WorkshopType, workshop.Capital, 0);
		}

		// Token: 0x06004079 RID: 16505 RVA: 0x0013E16A File Offset: 0x0013C36A
		public static void ApplyByWar(Workshop workshop, Hero newOwner, WorkshopType workshopType)
		{
			ChangeOwnerOfWorkshopAction.ApplyInternal(workshop, newOwner, workshopType, Campaign.Current.Models.WorkshopModel.InitialCapital, 0);
		}
	}
}
