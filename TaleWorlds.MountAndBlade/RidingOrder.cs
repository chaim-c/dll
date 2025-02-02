using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000155 RID: 341
	public struct RidingOrder
	{
		// Token: 0x06001161 RID: 4449 RVA: 0x000375EA File Offset: 0x000357EA
		private RidingOrder(RidingOrder.RidingOrderEnum orderEnum)
		{
			this.OrderEnum = orderEnum;
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06001162 RID: 4450 RVA: 0x000375F3 File Offset: 0x000357F3
		public OrderType OrderType
		{
			get
			{
				if (this.OrderEnum == RidingOrder.RidingOrderEnum.Free)
				{
					return OrderType.RideFree;
				}
				if (this.OrderEnum != RidingOrder.RidingOrderEnum.Mount)
				{
					return OrderType.Dismount;
				}
				return OrderType.Mount;
			}
		}

		// Token: 0x06001163 RID: 4451 RVA: 0x00037610 File Offset: 0x00035810
		public override bool Equals(object obj)
		{
			if (obj is RidingOrder)
			{
				RidingOrder r = (RidingOrder)obj;
				return r == this;
			}
			return false;
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x0003763C File Offset: 0x0003583C
		public override int GetHashCode()
		{
			return (int)this.OrderEnum;
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x00037644 File Offset: 0x00035844
		public static bool operator !=(RidingOrder r1, RidingOrder r2)
		{
			return r1.OrderEnum != r2.OrderEnum;
		}

		// Token: 0x06001166 RID: 4454 RVA: 0x00037657 File Offset: 0x00035857
		public static bool operator ==(RidingOrder r1, RidingOrder r2)
		{
			return r1.OrderEnum == r2.OrderEnum;
		}

		// Token: 0x0400045D RID: 1117
		public readonly RidingOrder.RidingOrderEnum OrderEnum;

		// Token: 0x0400045E RID: 1118
		public static readonly RidingOrder RidingOrderFree = new RidingOrder(RidingOrder.RidingOrderEnum.Free);

		// Token: 0x0400045F RID: 1119
		public static readonly RidingOrder RidingOrderMount = new RidingOrder(RidingOrder.RidingOrderEnum.Mount);

		// Token: 0x04000460 RID: 1120
		public static readonly RidingOrder RidingOrderDismount = new RidingOrder(RidingOrder.RidingOrderEnum.Dismount);

		// Token: 0x02000456 RID: 1110
		public enum RidingOrderEnum
		{
			// Token: 0x04001918 RID: 6424
			Free,
			// Token: 0x04001919 RID: 6425
			Mount,
			// Token: 0x0400191A RID: 6426
			Dismount
		}
	}
}
