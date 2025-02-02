using System;
using System.Runtime.CompilerServices;

namespace MCM.Abstractions.Base.Global
{
	// Token: 0x020000BB RID: 187
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1
	})]
	public abstract class AttributeGlobalSettings<[Nullable(0)] T> : GlobalSettings<T> where T : GlobalSettings, new()
	{
		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060003E6 RID: 998 RVA: 0x0000BD20 File Offset: 0x00009F20
		public sealed override string DiscoveryType
		{
			get
			{
				return "attributes";
			}
		}
	}
}
