using System;
using System.Runtime.CompilerServices;

namespace MCM.Abstractions
{
	// Token: 0x02000045 RID: 69
	[NullableContext(1)]
	public interface IPropertyDefinitionBase
	{
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001A1 RID: 417
		string DisplayName { get; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001A2 RID: 418
		int Order { get; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001A3 RID: 419
		bool RequireRestart { get; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001A4 RID: 420
		string HintText { get; }
	}
}
