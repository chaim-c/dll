using System;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x02000051 RID: 81
	public interface IGenericParameterProvider : IMetadataTokenProvider
	{
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600028F RID: 655
		bool HasGenericParameters { get; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000290 RID: 656
		bool IsDefinition { get; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000291 RID: 657
		ModuleDefinition Module { get; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000292 RID: 658
		Collection<GenericParameter> GenericParameters { get; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000293 RID: 659
		GenericParameterType GenericParameterType { get; }
	}
}
