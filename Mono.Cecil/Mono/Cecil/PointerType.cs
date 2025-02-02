using System;
using Mono.Cecil.Metadata;

namespace Mono.Cecil
{
	// Token: 0x020000D6 RID: 214
	public sealed class PointerType : TypeSpecification
	{
		// Token: 0x17000230 RID: 560
		// (get) Token: 0x0600083C RID: 2108 RVA: 0x0001B38A File Offset: 0x0001958A
		public override string Name
		{
			get
			{
				return base.Name + "*";
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x0600083D RID: 2109 RVA: 0x0001B39C File Offset: 0x0001959C
		public override string FullName
		{
			get
			{
				return base.FullName + "*";
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x0600083E RID: 2110 RVA: 0x0001B3AE File Offset: 0x000195AE
		// (set) Token: 0x0600083F RID: 2111 RVA: 0x0001B3B1 File Offset: 0x000195B1
		public override bool IsValueType
		{
			get
			{
				return false;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000840 RID: 2112 RVA: 0x0001B3B8 File Offset: 0x000195B8
		public override bool IsPointer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x0001B3BB File Offset: 0x000195BB
		public PointerType(TypeReference type) : base(type)
		{
			Mixin.CheckType(type);
			this.etype = Mono.Cecil.Metadata.ElementType.Ptr;
		}
	}
}
