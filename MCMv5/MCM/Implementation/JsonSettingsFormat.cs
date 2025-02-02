using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using BUTR.DependencyInjection.Logger;

namespace MCM.Implementation
{
	// Token: 0x02000020 RID: 32
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class JsonSettingsFormat : BaseJsonSettingsFormat
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x000043D8 File Offset: 0x000025D8
		public override IEnumerable<string> FormatTypes
		{
			get
			{
				return new string[]
				{
					"json",
					"json2"
				};
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000043F0 File Offset: 0x000025F0
		public JsonSettingsFormat(IBUTRLogger<JsonSettingsFormat> logger) : base(logger)
		{
		}
	}
}
