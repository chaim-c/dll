using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000372 RID: 882
	public static class OrderOfBattleFormationExtensions
	{
		// Token: 0x0600308F RID: 12431 RVA: 0x000C986C File Offset: 0x000C7A6C
		public unsafe static void Refresh(this Formation formation)
		{
			if (formation != null)
			{
				formation.SetMovementOrder(*formation.GetReadonlyMovementOrderReference());
			}
		}

		// Token: 0x06003090 RID: 12432 RVA: 0x000C9882 File Offset: 0x000C7A82
		public static DeploymentFormationClass GetOrderOfBattleFormationClass(this FormationClass formationClass)
		{
			switch (formationClass)
			{
			case FormationClass.Infantry:
			case FormationClass.NumberOfDefaultFormations:
			case FormationClass.HeavyInfantry:
				return DeploymentFormationClass.Infantry;
			case FormationClass.Ranged:
				return DeploymentFormationClass.Ranged;
			case FormationClass.Cavalry:
			case FormationClass.LightCavalry:
			case FormationClass.HeavyCavalry:
				return DeploymentFormationClass.Cavalry;
			case FormationClass.HorseArcher:
				return DeploymentFormationClass.HorseArcher;
			default:
				return DeploymentFormationClass.Unset;
			}
		}

		// Token: 0x06003091 RID: 12433 RVA: 0x000C98B8 File Offset: 0x000C7AB8
		public static List<FormationClass> GetFormationClasses(this DeploymentFormationClass orderOfBattleFormationClass)
		{
			List<FormationClass> list = new List<FormationClass>();
			switch (orderOfBattleFormationClass)
			{
			case DeploymentFormationClass.Infantry:
				list.Add(FormationClass.Infantry);
				break;
			case DeploymentFormationClass.Ranged:
				list.Add(FormationClass.Ranged);
				break;
			case DeploymentFormationClass.Cavalry:
				list.Add(FormationClass.Cavalry);
				break;
			case DeploymentFormationClass.HorseArcher:
				list.Add(FormationClass.HorseArcher);
				break;
			case DeploymentFormationClass.InfantryAndRanged:
				list.Add(FormationClass.Infantry);
				list.Add(FormationClass.Ranged);
				break;
			case DeploymentFormationClass.CavalryAndHorseArcher:
				list.Add(FormationClass.Cavalry);
				list.Add(FormationClass.HorseArcher);
				break;
			}
			return list;
		}

		// Token: 0x06003092 RID: 12434 RVA: 0x000C9930 File Offset: 0x000C7B30
		public static TextObject GetFilterName(this FormationFilterType filterType)
		{
			switch (filterType)
			{
			case FormationFilterType.Shield:
				return new TextObject("{=PSN8IaIg}Shields", null);
			case FormationFilterType.Spear:
				return new TextObject("{=f83FU4X6}Polearms", null);
			case FormationFilterType.Thrown:
				return new TextObject("{=Ea3K1PVR}Thrown Weapons", null);
			case FormationFilterType.Heavy:
				return new TextObject("{=Jw0GMgzv}Heavy Armors", null);
			case FormationFilterType.HighTier:
				return new TextObject("{=DzAkCzwd}High Tier", null);
			case FormationFilterType.LowTier:
				return new TextObject("{=qaPgbwZv}Low Tier", null);
			default:
				return new TextObject("{=w7Yrbi5t}Unset", null);
			}
		}

		// Token: 0x06003093 RID: 12435 RVA: 0x000C99B4 File Offset: 0x000C7BB4
		public static TextObject GetFilterDescription(this FormationFilterType filterType)
		{
			switch (filterType)
			{
			case FormationFilterType.Unset:
				return new TextObject("{=Q1Ga032B}Don't give preference to any type of troop.", null);
			case FormationFilterType.Shield:
				return new TextObject("{=MVOPbhNj}Give preference to troops with Shields", null);
			case FormationFilterType.Spear:
				return new TextObject("{=K3Cr70PY}Give preference to troops with Polearms", null);
			case FormationFilterType.Thrown:
				return new TextObject("{=DWWa3aIb}Give preference to troops with Thrown Weapons", null);
			case FormationFilterType.Heavy:
				return new TextObject("{=ush8OHIw}Give preference to troops with Heavy Armors", null);
			case FormationFilterType.HighTier:
				return new TextObject("{=DRNDtkP2}Give preference to troops at higher tiers", null);
			case FormationFilterType.LowTier:
				return new TextObject("{=zbpCRmuJ}Give preference to troops at lower tiers", null);
			default:
				return new TextObject("{=w7Yrbi5t}Unset", null);
			}
		}

		// Token: 0x06003094 RID: 12436 RVA: 0x000C9A44 File Offset: 0x000C7C44
		public static TextObject GetClassName(this DeploymentFormationClass formationClass)
		{
			switch (formationClass)
			{
			case DeploymentFormationClass.Infantry:
				return GameTexts.FindText("str_troop_type_name", "Infantry");
			case DeploymentFormationClass.Ranged:
				return GameTexts.FindText("str_troop_type_name", "Ranged");
			case DeploymentFormationClass.Cavalry:
				return GameTexts.FindText("str_troop_type_name", "Cavalry");
			case DeploymentFormationClass.HorseArcher:
				return GameTexts.FindText("str_troop_type_name", "HorseArcher");
			case DeploymentFormationClass.InfantryAndRanged:
				return new TextObject("{=mBDj5uG5}Infantry and Ranged", null);
			case DeploymentFormationClass.CavalryAndHorseArcher:
				return new TextObject("{=FNLfNWH3}Cavalry and Horse Archer", null);
			default:
				return new TextObject("{=w7Yrbi5t}Unset", null);
			}
		}

		// Token: 0x06003095 RID: 12437 RVA: 0x000C9AD6 File Offset: 0x000C7CD6
		public static List<Agent> GetHeroAgents(this Team team)
		{
			return (from a in team.ActiveAgents
			where a.IsHero
			select a).ToList<Agent>();
		}
	}
}
