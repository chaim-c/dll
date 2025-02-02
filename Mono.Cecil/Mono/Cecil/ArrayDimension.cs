using System;

namespace Mono.Cecil
{
	// Token: 0x0200004E RID: 78
	public struct ArrayDimension
	{
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000277 RID: 631 RVA: 0x0000BBC2 File Offset: 0x00009DC2
		// (set) Token: 0x06000278 RID: 632 RVA: 0x0000BBCA File Offset: 0x00009DCA
		public int? LowerBound
		{
			get
			{
				return this.lower_bound;
			}
			set
			{
				this.lower_bound = value;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000279 RID: 633 RVA: 0x0000BBD3 File Offset: 0x00009DD3
		// (set) Token: 0x0600027A RID: 634 RVA: 0x0000BBDB File Offset: 0x00009DDB
		public int? UpperBound
		{
			get
			{
				return this.upper_bound;
			}
			set
			{
				this.upper_bound = value;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600027B RID: 635 RVA: 0x0000BBE4 File Offset: 0x00009DE4
		public bool IsSized
		{
			get
			{
				return this.lower_bound != null || this.upper_bound != null;
			}
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000BC00 File Offset: 0x00009E00
		public ArrayDimension(int? lowerBound, int? upperBound)
		{
			this.lower_bound = lowerBound;
			this.upper_bound = upperBound;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000BC10 File Offset: 0x00009E10
		public override string ToString()
		{
			if (this.IsSized)
			{
				return this.lower_bound + "..." + this.upper_bound;
			}
			return string.Empty;
		}

		// Token: 0x04000376 RID: 886
		private int? lower_bound;

		// Token: 0x04000377 RID: 887
		private int? upper_bound;
	}
}
