using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MCM.Abstractions.Base;
using MCM.Abstractions.GameFeatures;

namespace MCM.Abstractions
{
	// Token: 0x02000055 RID: 85
	[NullableContext(1)]
	public interface ISettingsFormat
	{
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001C4 RID: 452
		IEnumerable<string> FormatTypes { get; }

		// Token: 0x060001C5 RID: 453
		bool Save(BaseSettings settings, GameDirectory directory, string filename);

		// Token: 0x060001C6 RID: 454
		BaseSettings Load(BaseSettings settings, GameDirectory directory, string filename);
	}
}
