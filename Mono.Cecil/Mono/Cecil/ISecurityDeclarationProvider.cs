using System;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x02000057 RID: 87
	public interface ISecurityDeclarationProvider : IMetadataTokenProvider
	{
		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060002D4 RID: 724
		bool HasSecurityDeclarations { get; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060002D5 RID: 725
		Collection<SecurityDeclaration> SecurityDeclarations { get; }
	}
}
