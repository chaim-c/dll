using System;
using System.Collections.Generic;
using Helpers;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Workshops;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.GameComponents
{
	// Token: 0x0200014B RID: 331
	public class DefaultWorkshopModel : WorkshopModel
	{
		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06001851 RID: 6225 RVA: 0x0007C393 File Offset: 0x0007A593
		public override int WarehouseCapacity
		{
			get
			{
				return 6000;
			}
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x06001852 RID: 6226 RVA: 0x0007C39A File Offset: 0x0007A59A
		public override int DaysForPlayerSaveWorkshopFromBankruptcy
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x06001853 RID: 6227 RVA: 0x0007C39D File Offset: 0x0007A59D
		public override int CapitalLowLimit
		{
			get
			{
				return 5000;
			}
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06001854 RID: 6228 RVA: 0x0007C3A4 File Offset: 0x0007A5A4
		public override int InitialCapital
		{
			get
			{
				return 10000;
			}
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x06001855 RID: 6229 RVA: 0x0007C3AB File Offset: 0x0007A5AB
		public override int DailyExpense
		{
			get
			{
				return 100;
			}
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x06001856 RID: 6230 RVA: 0x0007C3AF File Offset: 0x0007A5AF
		public override int DefaultWorkshopCountInSettlement
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x06001857 RID: 6231 RVA: 0x0007C3B2 File Offset: 0x0007A5B2
		public override int MaximumWorkshopsPlayerCanHave
		{
			get
			{
				return this.GetMaxWorkshopCountForClanTier(Campaign.Current.Models.ClanTierModel.MaxClanTier);
			}
		}

		// Token: 0x06001858 RID: 6232 RVA: 0x0007C3D0 File Offset: 0x0007A5D0
		public override ExplainedNumber GetEffectiveConversionSpeedOfProduction(Workshop workshop, float speed, bool includeDescription)
		{
			ExplainedNumber result = new ExplainedNumber(speed, includeDescription, new TextObject("{=basevalue}Base", null));
			Settlement settlement = workshop.Settlement;
			if (settlement.OwnerClan.Kingdom != null)
			{
				if (settlement.OwnerClan.Kingdom.ActivePolicies.Contains(DefaultPolicies.ForgivenessOfDebts))
				{
					result.AddFactor(-0.05f, DefaultPolicies.ForgivenessOfDebts.Name);
				}
				if (settlement.OwnerClan.Kingdom.ActivePolicies.Contains(DefaultPolicies.StateMonopolies))
				{
					result.AddFactor(-0.1f, DefaultPolicies.StateMonopolies.Name);
				}
			}
			PerkHelper.AddPerkBonusForTown(DefaultPerks.Trade.MercenaryConnections, settlement.Town, ref result);
			PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Steward.Sweatshops, workshop.Owner.CharacterObject, true, ref result);
			return result;
		}

		// Token: 0x06001859 RID: 6233 RVA: 0x0007C494 File Offset: 0x0007A694
		public override int GetMaxWorkshopCountForClanTier(int tier)
		{
			return tier + 1;
		}

		// Token: 0x0600185A RID: 6234 RVA: 0x0007C499 File Offset: 0x0007A699
		public override int GetCostForPlayer(Workshop workshop)
		{
			return workshop.WorkshopType.EquipmentCost + (int)workshop.Settlement.Town.Prosperity * 3 + this.InitialCapital;
		}

		// Token: 0x0600185B RID: 6235 RVA: 0x0007C4C1 File Offset: 0x0007A6C1
		public override int GetCostForNotable(Workshop workshop)
		{
			return (workshop.WorkshopType.EquipmentCost + (int)workshop.Settlement.Town.Prosperity / 2 + workshop.Capital) / 2;
		}

		// Token: 0x0600185C RID: 6236 RVA: 0x0007C4EC File Offset: 0x0007A6EC
		public override Hero GetNotableOwnerForWorkshop(Workshop workshop)
		{
			List<ValueTuple<Hero, float>> list = new List<ValueTuple<Hero, float>>();
			foreach (Hero hero in workshop.Settlement.Notables)
			{
				if (hero.IsAlive && hero != workshop.Owner)
				{
					int count = hero.OwnedWorkshops.Count;
					float item = Math.Max(hero.Power, 0f) / MathF.Pow(10f, (float)count);
					list.Add(new ValueTuple<Hero, float>(hero, item));
				}
			}
			return MBRandom.ChooseWeighted<Hero>(list);
		}

		// Token: 0x0600185D RID: 6237 RVA: 0x0007C594 File Offset: 0x0007A794
		public override int GetConvertProductionCost(WorkshopType workshopType)
		{
			return workshopType.EquipmentCost;
		}

		// Token: 0x0600185E RID: 6238 RVA: 0x0007C59C File Offset: 0x0007A79C
		public override bool CanPlayerSellWorkshop(Workshop workshop, out TextObject explanation)
		{
			Campaign.Current.Models.WorkshopModel.GetCostForNotable(workshop);
			Hero notableOwnerForWorkshop = Campaign.Current.Models.WorkshopModel.GetNotableOwnerForWorkshop(workshop);
			explanation = ((notableOwnerForWorkshop == null) ? new TextObject("{=oqPf2Gdp}There isn't any prospective buyer in the town.", null) : TextObject.Empty);
			return notableOwnerForWorkshop != null;
		}

		// Token: 0x0600185F RID: 6239 RVA: 0x0007C5F0 File Offset: 0x0007A7F0
		public override float GetTradeXpPerWarehouseProduction(EquipmentElement production)
		{
			return (float)production.GetBaseValue() * 0.1f;
		}
	}
}
