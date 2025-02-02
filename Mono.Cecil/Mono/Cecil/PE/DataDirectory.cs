using System;

namespace Mono.Cecil.PE
{
	// Token: 0x02000047 RID: 71
	internal struct DataDirectory
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000217 RID: 535 RVA: 0x00009881 File Offset: 0x00007A81
		public bool IsZero
		{
			get
			{
				return this.VirtualAddress == 0U && this.Size == 0U;
			}
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00009896 File Offset: 0x00007A96
		public DataDirectory(uint rva, uint size)
		{
			this.VirtualAddress = rva;
			this.Size = size;
		}

		// Token: 0x04000333 RID: 819
		public readonly uint VirtualAddress;

		// Token: 0x04000334 RID: 820
		public readonly uint Size;
	}
}
