using System;
using Mono.Cecil.Metadata;

namespace Mono.Cecil
{
	// Token: 0x020000BE RID: 190
	public sealed class PinnedType : TypeSpecification
	{
		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060006C1 RID: 1729 RVA: 0x00018FCF File Offset: 0x000171CF
		// (set) Token: 0x060006C2 RID: 1730 RVA: 0x00018FD2 File Offset: 0x000171D2
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

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060006C3 RID: 1731 RVA: 0x00018FD9 File Offset: 0x000171D9
		public override bool IsPinned
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x00018FDC File Offset: 0x000171DC
		public PinnedType(TypeReference type) : base(type)
		{
			Mixin.CheckType(type);
			this.etype = Mono.Cecil.Metadata.ElementType.Pinned;
		}
	}
}
