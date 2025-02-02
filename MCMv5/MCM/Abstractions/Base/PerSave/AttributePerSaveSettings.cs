using System;
using System.Runtime.CompilerServices;

namespace MCM.Abstractions.Base.PerSave
{
	// Token: 0x020000AD RID: 173
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1
	})]
	public abstract class AttributePerSaveSettings<[Nullable(0)] T> : PerSaveSettings<T> where T : PerSaveSettings, new()
	{
		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000392 RID: 914 RVA: 0x0000B18A File Offset: 0x0000938A
		public sealed override string DiscoveryType
		{
			get
			{
				return "attributes";
			}
		}
	}
}
