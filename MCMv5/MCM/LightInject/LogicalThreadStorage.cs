using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace MCM.LightInject
{
	// Token: 0x0200011F RID: 287
	[ExcludeFromCodeCoverage]
	internal class LogicalThreadStorage<T>
	{
		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060006D7 RID: 1751 RVA: 0x000167EC File Offset: 0x000149EC
		// (set) Token: 0x060006D8 RID: 1752 RVA: 0x00016809 File Offset: 0x00014A09
		public T Value
		{
			get
			{
				return this.asyncLocal.Value;
			}
			set
			{
				this.asyncLocal.Value = value;
			}
		}

		// Token: 0x04000210 RID: 528
		private readonly AsyncLocal<T> asyncLocal = new AsyncLocal<T>();
	}
}
