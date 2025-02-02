using System;

namespace Mono.Cecil
{
	// Token: 0x020000B5 RID: 181
	public sealed class FixedArrayMarshalInfo : MarshalInfo
	{
		// Token: 0x17000177 RID: 375
		// (get) Token: 0x0600068A RID: 1674 RVA: 0x00018808 File Offset: 0x00016A08
		// (set) Token: 0x0600068B RID: 1675 RVA: 0x00018810 File Offset: 0x00016A10
		public NativeType ElementType
		{
			get
			{
				return this.element_type;
			}
			set
			{
				this.element_type = value;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x0600068C RID: 1676 RVA: 0x00018819 File Offset: 0x00016A19
		// (set) Token: 0x0600068D RID: 1677 RVA: 0x00018821 File Offset: 0x00016A21
		public int Size
		{
			get
			{
				return this.size;
			}
			set
			{
				this.size = value;
			}
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x0001882A File Offset: 0x00016A2A
		public FixedArrayMarshalInfo() : base(NativeType.FixedArray)
		{
			this.element_type = NativeType.None;
		}

		// Token: 0x04000451 RID: 1105
		internal NativeType element_type;

		// Token: 0x04000452 RID: 1106
		internal int size;
	}
}
