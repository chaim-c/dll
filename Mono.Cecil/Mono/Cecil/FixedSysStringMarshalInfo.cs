using System;

namespace Mono.Cecil
{
	// Token: 0x020000B6 RID: 182
	public sealed class FixedSysStringMarshalInfo : MarshalInfo
	{
		// Token: 0x17000179 RID: 377
		// (get) Token: 0x0600068F RID: 1679 RVA: 0x0001883C File Offset: 0x00016A3C
		// (set) Token: 0x06000690 RID: 1680 RVA: 0x00018844 File Offset: 0x00016A44
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

		// Token: 0x06000691 RID: 1681 RVA: 0x0001884D File Offset: 0x00016A4D
		public FixedSysStringMarshalInfo() : base(NativeType.FixedSysString)
		{
			this.size = -1;
		}

		// Token: 0x04000453 RID: 1107
		internal int size;
	}
}
