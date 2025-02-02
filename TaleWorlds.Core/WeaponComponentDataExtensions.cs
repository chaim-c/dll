using System;

namespace TaleWorlds.Core
{
	// Token: 0x020000C9 RID: 201
	public static class WeaponComponentDataExtensions
	{
		// Token: 0x060009D8 RID: 2520 RVA: 0x000205EB File Offset: 0x0001E7EB
		public static int GetModifiedThrustDamage(this WeaponComponentData componentData, ItemModifier itemModifier)
		{
			if (itemModifier != null && componentData.ThrustDamage > 0)
			{
				return itemModifier.ModifyDamage(componentData.ThrustDamage);
			}
			return componentData.ThrustDamage;
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x0002060C File Offset: 0x0001E80C
		public static int GetModifiedSwingDamage(this WeaponComponentData componentData, ItemModifier itemModifier)
		{
			if (itemModifier != null && componentData.SwingDamage > 0)
			{
				return itemModifier.ModifyDamage(componentData.SwingDamage);
			}
			return componentData.SwingDamage;
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x0002062D File Offset: 0x0001E82D
		public static int GetModifiedMissileDamage(this WeaponComponentData componentData, ItemModifier itemModifier)
		{
			if (itemModifier != null && componentData.MissileDamage > 0)
			{
				return itemModifier.ModifyDamage(componentData.MissileDamage);
			}
			return componentData.MissileDamage;
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x0002064E File Offset: 0x0001E84E
		public static int GetModifiedThrustSpeed(this WeaponComponentData componentData, ItemModifier itemModifier)
		{
			if (itemModifier != null && componentData.ThrustSpeed > 0)
			{
				return itemModifier.ModifySpeed(componentData.ThrustSpeed);
			}
			return componentData.ThrustSpeed;
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x0002066F File Offset: 0x0001E86F
		public static int GetModifiedSwingSpeed(this WeaponComponentData componentData, ItemModifier itemModifier)
		{
			if (itemModifier != null && componentData.SwingSpeed > 0)
			{
				return itemModifier.ModifySpeed(componentData.SwingSpeed);
			}
			return componentData.SwingSpeed;
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x00020690 File Offset: 0x0001E890
		public static int GetModifiedMissileSpeed(this WeaponComponentData componentData, ItemModifier itemModifier)
		{
			if (itemModifier != null && componentData.MissileSpeed > 0)
			{
				return itemModifier.ModifyMissileSpeed(componentData.MissileSpeed);
			}
			return componentData.MissileSpeed;
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x000206B1 File Offset: 0x0001E8B1
		public static int GetModifiedHandling(this WeaponComponentData componentData, ItemModifier itemModifier)
		{
			if (itemModifier != null && componentData.Handling > 0)
			{
				return itemModifier.ModifySpeed(componentData.Handling);
			}
			return componentData.Handling;
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x000206D2 File Offset: 0x0001E8D2
		public static short GetModifiedStackCount(this WeaponComponentData componentData, ItemModifier itemModifier)
		{
			if (itemModifier != null && componentData.MaxDataValue > 0)
			{
				return itemModifier.ModifyStackCount(componentData.MaxDataValue);
			}
			return componentData.MaxDataValue;
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x000206F3 File Offset: 0x0001E8F3
		public static short GetModifiedMaximumHitPoints(this WeaponComponentData componentData, ItemModifier itemModifier)
		{
			if (itemModifier != null && componentData.MaxDataValue > 0)
			{
				return itemModifier.ModifyHitPoints(componentData.MaxDataValue);
			}
			return componentData.MaxDataValue;
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x00020714 File Offset: 0x0001E914
		public static int GetModifiedArmor(this WeaponComponentData componentData, ItemModifier itemModifier)
		{
			if (itemModifier != null && componentData.BodyArmor > 0)
			{
				return itemModifier.ModifyArmor(componentData.BodyArmor);
			}
			return componentData.BodyArmor;
		}
	}
}
