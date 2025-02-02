using System;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x020000A8 RID: 168
	public interface IGenericInstance : IMetadataTokenProvider
	{
		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000628 RID: 1576
		bool HasGenericArguments { get; }

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000629 RID: 1577
		Collection<TypeReference> GenericArguments { get; }
	}
}
