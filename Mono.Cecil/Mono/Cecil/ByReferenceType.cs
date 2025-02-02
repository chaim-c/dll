using System;
using Mono.Cecil.Metadata;

namespace Mono.Cecil
{
	// Token: 0x020000DA RID: 218
	public sealed class ByReferenceType : TypeSpecification
	{
		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000868 RID: 2152 RVA: 0x0001B87F File Offset: 0x00019A7F
		public override string Name
		{
			get
			{
				return base.Name + "&";
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000869 RID: 2153 RVA: 0x0001B891 File Offset: 0x00019A91
		public override string FullName
		{
			get
			{
				return base.FullName + "&";
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x0600086A RID: 2154 RVA: 0x0001B8A3 File Offset: 0x00019AA3
		// (set) Token: 0x0600086B RID: 2155 RVA: 0x0001B8A6 File Offset: 0x00019AA6
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

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x0600086C RID: 2156 RVA: 0x0001B8AD File Offset: 0x00019AAD
		public override bool IsByReference
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x0001B8B0 File Offset: 0x00019AB0
		public ByReferenceType(TypeReference type) : base(type)
		{
			Mixin.CheckType(type);
			this.etype = Mono.Cecil.Metadata.ElementType.ByRef;
		}
	}
}
