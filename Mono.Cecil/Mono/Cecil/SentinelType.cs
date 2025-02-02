using System;
using Mono.Cecil.Metadata;

namespace Mono.Cecil
{
	// Token: 0x020000E9 RID: 233
	public sealed class SentinelType : TypeSpecification
	{
		// Token: 0x1700027F RID: 639
		// (get) Token: 0x0600090F RID: 2319 RVA: 0x0001CC8C File Offset: 0x0001AE8C
		// (set) Token: 0x06000910 RID: 2320 RVA: 0x0001CC8F File Offset: 0x0001AE8F
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

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000911 RID: 2321 RVA: 0x0001CC96 File Offset: 0x0001AE96
		public override bool IsSentinel
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x0001CC99 File Offset: 0x0001AE99
		public SentinelType(TypeReference type) : base(type)
		{
			Mixin.CheckType(type);
			this.etype = Mono.Cecil.Metadata.ElementType.Sentinel;
		}
	}
}
