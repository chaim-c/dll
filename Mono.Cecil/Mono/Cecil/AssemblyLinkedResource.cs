using System;

namespace Mono.Cecil
{
	// Token: 0x0200005C RID: 92
	public sealed class AssemblyLinkedResource : Resource
	{
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060002FB RID: 763 RVA: 0x0000C4D2 File Offset: 0x0000A6D2
		// (set) Token: 0x060002FC RID: 764 RVA: 0x0000C4DA File Offset: 0x0000A6DA
		public AssemblyNameReference Assembly
		{
			get
			{
				return this.reference;
			}
			set
			{
				this.reference = value;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060002FD RID: 765 RVA: 0x0000C4E3 File Offset: 0x0000A6E3
		public override ResourceType ResourceType
		{
			get
			{
				return ResourceType.AssemblyLinked;
			}
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000C4E6 File Offset: 0x0000A6E6
		public AssemblyLinkedResource(string name, ManifestResourceAttributes flags) : base(name, flags)
		{
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000C4F0 File Offset: 0x0000A6F0
		public AssemblyLinkedResource(string name, ManifestResourceAttributes flags, AssemblyNameReference reference) : base(name, flags)
		{
			this.reference = reference;
		}

		// Token: 0x04000397 RID: 919
		private AssemblyNameReference reference;
	}
}
