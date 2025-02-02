using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MCM.Abstractions.Base;
using MCM.Abstractions.GameFeatures;

namespace MCM.Abstractions
{
	// Token: 0x02000057 RID: 87
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class NoneSettingsFormat : ISettingsFormat
	{
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00007E50 File Offset: 0x00006050
		public IEnumerable<string> FormatTypes { get; } = new string[]
		{
			"none"
		};

		// Token: 0x060001CC RID: 460 RVA: 0x00007E58 File Offset: 0x00006058
		public BaseSettings Load(BaseSettings settings, GameDirectory directory, string filename)
		{
			return settings;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00007E5B File Offset: 0x0000605B
		public bool Save(BaseSettings settings, GameDirectory directory, string filename)
		{
			return true;
		}
	}
}
