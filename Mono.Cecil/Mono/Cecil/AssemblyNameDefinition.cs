using System;

namespace Mono.Cecil
{
	// Token: 0x0200005F RID: 95
	public sealed class AssemblyNameDefinition : AssemblyNameReference
	{
		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000324 RID: 804 RVA: 0x0000C9F0 File Offset: 0x0000ABF0
		public override byte[] Hash
		{
			get
			{
				return Empty<byte>.Array;
			}
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000C9F7 File Offset: 0x0000ABF7
		internal AssemblyNameDefinition()
		{
			this.token = new MetadataToken(TokenType.Assembly, 1);
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000CA10 File Offset: 0x0000AC10
		public AssemblyNameDefinition(string name, Version version) : base(name, version)
		{
			this.token = new MetadataToken(TokenType.Assembly, 1);
		}
	}
}
