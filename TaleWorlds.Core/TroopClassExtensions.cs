using System;

namespace TaleWorlds.Core
{
	// Token: 0x0200005F RID: 95
	public static class TroopClassExtensions
	{
		// Token: 0x0600067C RID: 1660 RVA: 0x00017268 File Offset: 0x00015468
		public static bool IsRanged(this FormationClass troopClass)
		{
			FormationClass formationClass = troopClass.DefaultClass();
			return formationClass == FormationClass.Ranged || formationClass == FormationClass.HorseArcher;
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x00017286 File Offset: 0x00015486
		public static bool IsMounted(this FormationClass troopClass)
		{
			troopClass.DefaultClass();
			return troopClass == FormationClass.Cavalry || troopClass == FormationClass.HorseArcher;
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x00017299 File Offset: 0x00015499
		public static bool IsMeleeInfantry(this FormationClass troopClass)
		{
			troopClass.DefaultClass();
			return troopClass == FormationClass.Infantry;
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x000172A6 File Offset: 0x000154A6
		public static bool IsMeleeCavalry(this FormationClass troopClass)
		{
			return troopClass.DefaultClass() == FormationClass.Cavalry;
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x000172B4 File Offset: 0x000154B4
		public static FormationClass DefaultClass(this FormationClass troopClass)
		{
			if (troopClass.IsRegularFormationClass())
			{
				FormationClass result = troopClass;
				switch (troopClass)
				{
				case FormationClass.NumberOfDefaultFormations:
					result = FormationClass.Ranged;
					break;
				case FormationClass.HeavyInfantry:
					result = FormationClass.Infantry;
					break;
				case FormationClass.LightCavalry:
					result = FormationClass.HorseArcher;
					break;
				case FormationClass.HeavyCavalry:
					result = FormationClass.Cavalry;
					break;
				}
				return result;
			}
			return FormationClass.Infantry;
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x000172F6 File Offset: 0x000154F6
		public static FormationClass AlternativeClass(this FormationClass troopClass)
		{
			switch (troopClass)
			{
			case FormationClass.Infantry:
				return FormationClass.Ranged;
			case FormationClass.Ranged:
				return FormationClass.Infantry;
			case FormationClass.Cavalry:
				return FormationClass.HorseArcher;
			case FormationClass.HorseArcher:
				return FormationClass.Cavalry;
			case FormationClass.NumberOfDefaultFormations:
				return FormationClass.HeavyInfantry;
			case FormationClass.HeavyInfantry:
				return FormationClass.NumberOfDefaultFormations;
			case FormationClass.LightCavalry:
				return FormationClass.HeavyCavalry;
			case FormationClass.HeavyCavalry:
				return FormationClass.LightCavalry;
			default:
				return troopClass;
			}
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x00017334 File Offset: 0x00015534
		public static FormationClass DismountedClass(this FormationClass troopClass)
		{
			FormationClass result = troopClass;
			switch (troopClass)
			{
			case FormationClass.Cavalry:
				result = FormationClass.Infantry;
				break;
			case FormationClass.HorseArcher:
				result = FormationClass.Ranged;
				break;
			case FormationClass.LightCavalry:
				result = FormationClass.NumberOfDefaultFormations;
				break;
			case FormationClass.HeavyCavalry:
				result = FormationClass.HeavyInfantry;
				break;
			}
			return result;
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x00017374 File Offset: 0x00015574
		public static bool IsDefaultTroopClass(this FormationClass troopClass)
		{
			return troopClass >= FormationClass.Infantry && troopClass < FormationClass.NumberOfDefaultFormations;
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x00017390 File Offset: 0x00015590
		public static bool IsRegularTroopClass(this FormationClass troopClass)
		{
			return troopClass >= FormationClass.Infantry && troopClass < FormationClass.NumberOfRegularFormations;
		}
	}
}
