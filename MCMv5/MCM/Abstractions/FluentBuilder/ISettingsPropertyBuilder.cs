using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MCM.Common;

namespace MCM.Abstractions.FluentBuilder
{
	// Token: 0x02000079 RID: 121
	[NullableContext(1)]
	public interface ISettingsPropertyBuilder
	{
		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060002C3 RID: 707
		string Name { get; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060002C4 RID: 708
		IRef PropertyReference { get; }

		// Token: 0x060002C5 RID: 709
		IEnumerable<IPropertyDefinitionBase> GetDefinitions();
	}
}
