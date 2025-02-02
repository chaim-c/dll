using System;

namespace Mono.Cecil
{
	// Token: 0x020000B1 RID: 177
	public class MarshalInfo
	{
		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000672 RID: 1650 RVA: 0x0001870D File Offset: 0x0001690D
		// (set) Token: 0x06000673 RID: 1651 RVA: 0x00018715 File Offset: 0x00016915
		public NativeType NativeType
		{
			get
			{
				return this.native;
			}
			set
			{
				this.native = value;
			}
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x0001871E File Offset: 0x0001691E
		public MarshalInfo(NativeType native)
		{
			this.native = native;
		}

		// Token: 0x04000447 RID: 1095
		internal NativeType native;
	}
}
