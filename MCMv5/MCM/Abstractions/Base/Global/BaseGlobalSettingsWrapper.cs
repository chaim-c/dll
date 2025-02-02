using System;
using System.Runtime.CompilerServices;
using MCM.Common;

namespace MCM.Abstractions.Base.Global
{
	// Token: 0x020000BD RID: 189
	[NullableContext(1)]
	[Nullable(0)]
	[Obsolete("Will be removed from future API", true)]
	public abstract class BaseGlobalSettingsWrapper : GlobalSettings, IWrapper
	{
		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x0000BD73 File Offset: 0x00009F73
		// (set) Token: 0x060003EC RID: 1004 RVA: 0x0000BD7B File Offset: 0x00009F7B
		public object Object { get; protected set; }

		// Token: 0x060003ED RID: 1005 RVA: 0x0000BD84 File Offset: 0x00009F84
		protected BaseGlobalSettingsWrapper(object @object)
		{
			this.Object = @object;
		}
	}
}
