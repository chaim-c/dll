using System;
using System.Collections.Generic;

namespace TaleWorlds.Library
{
	// Token: 0x02000033 RID: 51
	public class GenericComparer<T> : Comparer<T> where T : IComparable<T>
	{
		// Token: 0x060001AA RID: 426 RVA: 0x00006C80 File Offset: 0x00004E80
		public override int Compare(T x, T y)
		{
			if (x != null)
			{
				if (y != null)
				{
					return x.CompareTo(y);
				}
				return 1;
			}
			else
			{
				if (y != null)
				{
					return -1;
				}
				return 0;
			}
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00006CAE File Offset: 0x00004EAE
		public override bool Equals(object obj)
		{
			return obj is GenericComparer<T>;
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00006CB9 File Offset: 0x00004EB9
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}
	}
}
