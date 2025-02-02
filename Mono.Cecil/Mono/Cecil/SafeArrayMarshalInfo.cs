using System;

namespace Mono.Cecil
{
	// Token: 0x020000B4 RID: 180
	public sealed class SafeArrayMarshalInfo : MarshalInfo
	{
		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000687 RID: 1671 RVA: 0x000187E6 File Offset: 0x000169E6
		// (set) Token: 0x06000688 RID: 1672 RVA: 0x000187EE File Offset: 0x000169EE
		public VariantType ElementType
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

		// Token: 0x06000689 RID: 1673 RVA: 0x000187F7 File Offset: 0x000169F7
		public SafeArrayMarshalInfo() : base(NativeType.SafeArray)
		{
			this.element_type = VariantType.None;
		}

		// Token: 0x04000450 RID: 1104
		internal VariantType element_type;
	}
}
