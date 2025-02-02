using System;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x02000056 RID: 86
	public interface ICustomAttributeProvider : IMetadataTokenProvider
	{
		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060002D2 RID: 722
		Collection<CustomAttribute> CustomAttributes { get; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060002D3 RID: 723
		bool HasCustomAttributes { get; }
	}
}
