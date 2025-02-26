﻿using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.OrderOfBattle
{
	// Token: 0x02000031 RID: 49
	internal static class OrderOfBattleUIHelper
	{
		// Token: 0x060003E4 RID: 996 RVA: 0x00010868 File Offset: 0x0000EA68
		internal static Team.TroopFilter GetTroopFilterForClass(params FormationClass[] formationClasses)
		{
			Team.TroopFilter result = Team.TroopFilter.Melee;
			if (formationClasses.Length == 1)
			{
				FormationClass formationClass = formationClasses[0];
				if (formationClass == FormationClass.Infantry)
				{
					result = Team.TroopFilter.Melee;
				}
				if (formationClass == FormationClass.Ranged)
				{
					result = Team.TroopFilter.Ranged;
				}
				if (formationClass == FormationClass.Cavalry)
				{
					result = (Team.TroopFilter.Mount | Team.TroopFilter.Melee);
				}
				if (formationClass == FormationClass.HorseArcher)
				{
					result = (Team.TroopFilter.Mount | Team.TroopFilter.Ranged);
				}
			}
			else if (formationClasses.Length == 2)
			{
				if (formationClasses[0] == FormationClass.Infantry && formationClasses[1] == FormationClass.Ranged)
				{
					result = (Team.TroopFilter.Ranged | Team.TroopFilter.Melee);
				}
				if (formationClasses[0] == FormationClass.Cavalry && formationClasses[1] == FormationClass.HorseArcher)
				{
					result = (Team.TroopFilter.Mount | Team.TroopFilter.Ranged | Team.TroopFilter.Melee);
				}
			}
			return result;
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x000108C4 File Offset: 0x0000EAC4
		internal static Team.TroopFilter GetTroopFilterForFormationFilter(params FormationFilterType[] filterTypes)
		{
			if (filterTypes.Length == 0)
			{
				return (Team.TroopFilter)0;
			}
			Team.TroopFilter troopFilter = (Team.TroopFilter)0;
			if (filterTypes.Any((FormationFilterType f) => f == FormationFilterType.Heavy))
			{
				troopFilter |= Team.TroopFilter.Armor;
			}
			if (filterTypes.Any((FormationFilterType f) => f == FormationFilterType.Shield))
			{
				troopFilter |= Team.TroopFilter.Shield;
			}
			if (filterTypes.Any((FormationFilterType f) => f == FormationFilterType.Thrown))
			{
				troopFilter |= Team.TroopFilter.Thrown;
			}
			if (filterTypes.Any((FormationFilterType f) => f == FormationFilterType.Spear))
			{
				troopFilter |= Team.TroopFilter.Spear;
			}
			if (filterTypes.Any((FormationFilterType f) => f == FormationFilterType.HighTier))
			{
				troopFilter |= Team.TroopFilter.HighTier;
			}
			if (filterTypes.Any((FormationFilterType f) => f == FormationFilterType.LowTier))
			{
				troopFilter |= Team.TroopFilter.LowTier;
			}
			return troopFilter;
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x000109E4 File Offset: 0x0000EBE4
		internal static List<Agent> GetExcludedAgentsForTransfer(OrderOfBattleFormationItemVM formationVM, FormationClass formationClass)
		{
			List<Agent> list = new List<Agent>();
			if (formationVM.HasCommander)
			{
				list.Add(formationVM.Commander.Agent);
			}
			if (formationVM.HeroTroops.Count > 0)
			{
				list.AddRange(from t in formationVM.HeroTroops
				select t.Agent);
			}
			foreach (IFormationUnit formationUnit in formationVM.Formation.Arrangement.GetAllUnits())
			{
				Agent agent = (Agent)formationUnit;
				if (agent.Banner != null || !OrderOfBattleUIHelper.IsAgentInFormationClass(agent, formationClass))
				{
					list.Add(agent);
				}
			}
			return list.Distinct<Agent>().ToList<Agent>();
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x00010AC0 File Offset: 0x0000ECC0
		internal static Tuple<Formation, int, Team.TroopFilter, List<Agent>> CreateMassTransferData(OrderOfBattleFormationClassVM affectedClass, FormationClass formationClass, Team.TroopFilter filter, int unitCount)
		{
			List<Agent> excludedAgentsForTransfer = OrderOfBattleUIHelper.GetExcludedAgentsForTransfer(affectedClass.BelongedFormationItem, formationClass);
			return new Tuple<Formation, int, Team.TroopFilter, List<Agent>>(affectedClass.BelongedFormationItem.Formation, unitCount, filter, excludedAgentsForTransfer);
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00010AF0 File Offset: 0x0000ECF0
		internal static Tuple<Formation, int, Team.TroopFilter, List<Agent>> CreateMassTransferData(OrderOfBattleFormationItemVM affectedFormation, FormationClass formationClass, Team.TroopFilter filter, int unitCount)
		{
			List<Agent> excludedAgentsForTransfer = OrderOfBattleUIHelper.GetExcludedAgentsForTransfer(affectedFormation, formationClass);
			return new Tuple<Formation, int, Team.TroopFilter, List<Agent>>(affectedFormation.Formation, unitCount, filter, excludedAgentsForTransfer);
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00010B14 File Offset: 0x0000ED14
		internal static ValueTuple<int, bool, bool> GetRelevantTroopTransferParameters(OrderOfBattleFormationClassVM classVM)
		{
			if (classVM == null)
			{
				return new ValueTuple<int, bool, bool>(0, false, false);
			}
			DeploymentFormationClass orderOfBattleFormationClass = classVM.Class.GetOrderOfBattleFormationClass();
			bool item = orderOfBattleFormationClass == DeploymentFormationClass.Ranged || orderOfBattleFormationClass == DeploymentFormationClass.HorseArcher;
			bool item2 = orderOfBattleFormationClass == DeploymentFormationClass.Cavalry || orderOfBattleFormationClass == DeploymentFormationClass.HorseArcher;
			return new ValueTuple<int, bool, bool>(OrderOfBattleUIHelper.GetTotalCountOfUnitsInClass(classVM.BelongedFormationItem.Formation, classVM.Class), item, item2);
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00010B70 File Offset: 0x0000ED70
		internal static OrderOfBattleFormationClassVM GetFormationClassWithExtremumWeight(List<OrderOfBattleFormationClassVM> classes, bool isMinimum)
		{
			if (classes.Count == 0)
			{
				return null;
			}
			if (classes.Count == 1)
			{
				return classes[0];
			}
			OrderOfBattleFormationClassVM orderOfBattleFormationClassVM = classes[0];
			for (int i = 1; i < classes.Count; i++)
			{
				if ((isMinimum && classes[i].Weight < orderOfBattleFormationClassVM.Weight) || (!isMinimum && classes[i].Weight > orderOfBattleFormationClassVM.Weight))
				{
					orderOfBattleFormationClassVM = classes[i];
				}
			}
			return orderOfBattleFormationClassVM;
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00010BE8 File Offset: 0x0000EDE8
		internal static List<OrderOfBattleFormationClassVM> GetMatchingClasses(List<OrderOfBattleFormationItemVM> formationList, OrderOfBattleFormationClassVM formationClass, Func<OrderOfBattleFormationClassVM, bool> predicate = null)
		{
			List<OrderOfBattleFormationClassVM> list = new List<OrderOfBattleFormationClassVM>();
			for (int i = 0; i < formationList.Count; i++)
			{
				OrderOfBattleFormationItemVM orderOfBattleFormationItemVM = formationList[i];
				for (int j = 0; j < orderOfBattleFormationItemVM.Classes.Count; j++)
				{
					OrderOfBattleFormationClassVM orderOfBattleFormationClassVM = orderOfBattleFormationItemVM.Classes[j];
					if (orderOfBattleFormationClassVM.Class == formationClass.Class && (predicate == null || predicate(orderOfBattleFormationClassVM)))
					{
						list.Add(orderOfBattleFormationClassVM);
					}
				}
			}
			return list;
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00010C5D File Offset: 0x0000EE5D
		internal static bool IsAgentInFormationClass(Agent agent, FormationClass fc)
		{
			switch (fc)
			{
			case FormationClass.Infantry:
				return QueryLibrary.IsInfantry(agent);
			case FormationClass.Ranged:
				return QueryLibrary.IsRanged(agent);
			case FormationClass.Cavalry:
				return QueryLibrary.IsCavalry(agent);
			case FormationClass.HorseArcher:
				return QueryLibrary.IsRangedCavalry(agent);
			default:
				return false;
			}
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00010C94 File Offset: 0x0000EE94
		private static List<Agent> GetBannerBearersOfFormation(Formation formation)
		{
			Mission mission = Mission.Current;
			List<Agent> list;
			if (mission == null)
			{
				list = null;
			}
			else
			{
				BannerBearerLogic missionBehavior = mission.GetMissionBehavior<BannerBearerLogic>();
				list = ((missionBehavior != null) ? missionBehavior.GetFormationBannerBearers(formation) : null);
			}
			List<Agent> list2 = list;
			if (list2 != null)
			{
				return list2;
			}
			return new List<Agent>();
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00010CCC File Offset: 0x0000EECC
		private static int GetCountOfUnitsInClass(OrderOfBattleFormationClassVM classVM, bool includeHeroes, bool includeBannerBearers)
		{
			Formation formation = classVM.BelongedFormationItem.Formation;
			FormationClass fc = classVM.Class;
			return formation.GetCountOfUnitsWithCondition((Agent agent) => (includeHeroes || !agent.IsHero) && (includeBannerBearers || agent.Banner == null) && OrderOfBattleUIHelper.IsAgentInFormationClass(agent, fc));
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00010D18 File Offset: 0x0000EF18
		internal static int GetTotalCountOfUnitsInClass(Formation formation, FormationClass fc)
		{
			switch (fc)
			{
			case FormationClass.Infantry:
				return MathF.Round(formation.QuerySystem.InfantryUnitRatio * (float)formation.CountOfUnits);
			case FormationClass.Ranged:
				return MathF.Round(formation.QuerySystem.RangedUnitRatio * (float)formation.CountOfUnits);
			case FormationClass.Cavalry:
				return MathF.Round(formation.QuerySystem.CavalryUnitRatio * (float)formation.CountOfUnits);
			case FormationClass.HorseArcher:
				return MathF.Round(formation.QuerySystem.RangedCavalryUnitRatio * (float)formation.CountOfUnits);
			default:
				return 0;
			}
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00010DA2 File Offset: 0x0000EFA2
		internal static int GetCountOfRealUnitsInClass(OrderOfBattleFormationClassVM classVM)
		{
			return OrderOfBattleUIHelper.GetTotalCountOfUnitsInClass(classVM.BelongedFormationItem.Formation, classVM.Class);
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00010DBC File Offset: 0x0000EFBC
		internal static int GetVisibleCountOfUnitsInClass(OrderOfBattleFormationClassVM classVM)
		{
			OrderOfBattleFormationItemVM belongedFormationItem = classVM.BelongedFormationItem;
			Formation formation = classVM.BelongedFormationItem.Formation;
			if ((from c in belongedFormationItem.Classes
			where !c.IsUnset
			select c).ToList<OrderOfBattleFormationClassVM>().Count == 1)
			{
				return classVM.BelongedFormationItem.Formation.CountOfUnits;
			}
			int num = OrderOfBattleUIHelper.GetCountOfUnitsInClass(classVM, true, false);
			if (classVM.Class == FormationClass.Infantry || classVM.Class == FormationClass.Cavalry)
			{
				num += OrderOfBattleUIHelper.GetBannerBearersOfFormation(formation).Count;
			}
			return num;
		}
	}
}
