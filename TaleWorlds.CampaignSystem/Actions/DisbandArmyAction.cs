using System;
using System.Linq;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.Party;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x0200043D RID: 1085
	public static class DisbandArmyAction
	{
		// Token: 0x060040A2 RID: 16546 RVA: 0x0013EAC4 File Offset: 0x0013CCC4
		private static void ApplyInternal(Army army, Army.ArmyDispersionReason reason)
		{
			if (reason == Army.ArmyDispersionReason.DismissalRequestedWithInfluence)
			{
				DiplomacyModel diplomacyModel = Campaign.Current.Models.DiplomacyModel;
				ChangeClanInfluenceAction.Apply(Clan.PlayerClan, (float)(-(float)diplomacyModel.GetInfluenceCostOfDisbandingArmy()));
				foreach (MobileParty mobileParty in army.Parties.ToList<MobileParty>())
				{
					if (mobileParty != MobileParty.MainParty && mobileParty.LeaderHero != null)
					{
						ChangeRelationAction.ApplyPlayerRelation(mobileParty.LeaderHero, diplomacyModel.GetRelationCostOfDisbandingArmy(mobileParty == mobileParty.Army.LeaderParty), true, true);
					}
				}
			}
			army.DisperseInternal(reason);
		}

		// Token: 0x060040A3 RID: 16547 RVA: 0x0013EB78 File Offset: 0x0013CD78
		public static void ApplyByReleasedByPlayerAfterBattle(Army army)
		{
			DisbandArmyAction.ApplyInternal(army, Army.ArmyDispersionReason.DismissalRequestedWithInfluence);
		}

		// Token: 0x060040A4 RID: 16548 RVA: 0x0013EB81 File Offset: 0x0013CD81
		public static void ApplyByArmyLeaderIsDead(Army army)
		{
			DisbandArmyAction.ApplyInternal(army, Army.ArmyDispersionReason.ArmyLeaderIsDead);
		}

		// Token: 0x060040A5 RID: 16549 RVA: 0x0013EB8B File Offset: 0x0013CD8B
		public static void ApplyByNotEnoughParty(Army army)
		{
			DisbandArmyAction.ApplyInternal(army, Army.ArmyDispersionReason.NotEnoughParty);
		}

		// Token: 0x060040A6 RID: 16550 RVA: 0x0013EB94 File Offset: 0x0013CD94
		public static void ApplyByObjectiveFinished(Army army)
		{
			DisbandArmyAction.ApplyInternal(army, Army.ArmyDispersionReason.ObjectiveFinished);
		}

		// Token: 0x060040A7 RID: 16551 RVA: 0x0013EB9D File Offset: 0x0013CD9D
		public static void ApplyByPlayerTakenPrisoner(Army army)
		{
			DisbandArmyAction.ApplyInternal(army, Army.ArmyDispersionReason.PlayerTakenPrisoner);
		}

		// Token: 0x060040A8 RID: 16552 RVA: 0x0013EBA6 File Offset: 0x0013CDA6
		public static void ApplyByFoodProblem(Army army)
		{
			DisbandArmyAction.ApplyInternal(army, Army.ArmyDispersionReason.FoodProblem);
		}

		// Token: 0x060040A9 RID: 16553 RVA: 0x0013EBB0 File Offset: 0x0013CDB0
		public static void ApplyByCohesionDepleted(Army army)
		{
			DisbandArmyAction.ApplyInternal(army, Army.ArmyDispersionReason.CohesionDepleted);
		}

		// Token: 0x060040AA RID: 16554 RVA: 0x0013EBB9 File Offset: 0x0013CDB9
		public static void ApplyByNoActiveWar(Army army)
		{
			DisbandArmyAction.ApplyInternal(army, Army.ArmyDispersionReason.NoActiveWar);
		}

		// Token: 0x060040AB RID: 16555 RVA: 0x0013EBC3 File Offset: 0x0013CDC3
		public static void ApplyByUnknownReason(Army army)
		{
			DisbandArmyAction.ApplyInternal(army, Army.ArmyDispersionReason.Unknown);
		}

		// Token: 0x060040AC RID: 16556 RVA: 0x0013EBCC File Offset: 0x0013CDCC
		public static void ApplyByLeaderPartyRemoved(Army army)
		{
			DisbandArmyAction.ApplyInternal(army, Army.ArmyDispersionReason.LeaderPartyRemoved);
		}
	}
}
