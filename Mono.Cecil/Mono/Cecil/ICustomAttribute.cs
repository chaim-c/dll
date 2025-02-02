using System;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x0200009D RID: 157
	public interface ICustomAttribute
	{
		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060005B0 RID: 1456
		TypeReference AttributeType { get; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060005B1 RID: 1457
		bool HasFields { get; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060005B2 RID: 1458
		bool HasProperties { get; }

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060005B3 RID: 1459
		Collection<CustomAttributeNamedArgument> Fields { get; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060005B4 RID: 1460
		Collection<CustomAttributeNamedArgument> Properties { get; }
	}
}
