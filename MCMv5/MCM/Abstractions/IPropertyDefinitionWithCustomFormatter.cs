using System;
using System.Runtime.CompilerServices;

namespace MCM.Abstractions
{
	// Token: 0x0200004C RID: 76
	[NullableContext(2)]
	public interface IPropertyDefinitionWithCustomFormatter
	{
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001A9 RID: 425
		Type CustomFormatter { get; }
	}
}
