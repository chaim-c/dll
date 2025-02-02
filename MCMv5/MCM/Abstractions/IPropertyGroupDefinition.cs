using System;
using System.Runtime.CompilerServices;

namespace MCM.Abstractions
{
	// Token: 0x02000051 RID: 81
	[NullableContext(1)]
	public interface IPropertyGroupDefinition
	{
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001B0 RID: 432
		string GroupName { get; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001B1 RID: 433
		int GroupOrder { get; }
	}
}
