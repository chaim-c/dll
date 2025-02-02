using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000150 RID: 336
	public struct FiringOrder
	{
		// Token: 0x06001102 RID: 4354 RVA: 0x000352AC File Offset: 0x000334AC
		private FiringOrder(FiringOrder.RangedWeaponUsageOrderEnum orderEnum)
		{
			this.OrderEnum = orderEnum;
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06001103 RID: 4355 RVA: 0x000352B5 File Offset: 0x000334B5
		public OrderType OrderType
		{
			get
			{
				if (this.OrderEnum != FiringOrder.RangedWeaponUsageOrderEnum.FireAtWill)
				{
					return OrderType.HoldFire;
				}
				return OrderType.FireAtWill;
			}
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x000352C4 File Offset: 0x000334C4
		public override bool Equals(object obj)
		{
			if (obj is FiringOrder)
			{
				FiringOrder f = (FiringOrder)obj;
				return f == this;
			}
			return false;
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x000352F0 File Offset: 0x000334F0
		public override int GetHashCode()
		{
			return (int)this.OrderEnum;
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x000352F8 File Offset: 0x000334F8
		public static bool operator !=(FiringOrder f1, FiringOrder f2)
		{
			return f1.OrderEnum != f2.OrderEnum;
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x0003530B File Offset: 0x0003350B
		public static bool operator ==(FiringOrder f1, FiringOrder f2)
		{
			return f1.OrderEnum == f2.OrderEnum;
		}

		// Token: 0x0400043D RID: 1085
		public readonly FiringOrder.RangedWeaponUsageOrderEnum OrderEnum;

		// Token: 0x0400043E RID: 1086
		public static readonly FiringOrder FiringOrderFireAtWill = new FiringOrder(FiringOrder.RangedWeaponUsageOrderEnum.FireAtWill);

		// Token: 0x0400043F RID: 1087
		public static readonly FiringOrder FiringOrderHoldYourFire = new FiringOrder(FiringOrder.RangedWeaponUsageOrderEnum.HoldYourFire);

		// Token: 0x02000445 RID: 1093
		public enum RangedWeaponUsageOrderEnum
		{
			// Token: 0x040018E2 RID: 6370
			FireAtWill,
			// Token: 0x040018E3 RID: 6371
			HoldYourFire
		}
	}
}
