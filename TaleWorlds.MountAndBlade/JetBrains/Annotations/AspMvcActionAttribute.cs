using System;

namespace JetBrains.Annotations
{
	// Token: 0x020000EE RID: 238
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Parameter)]
	public sealed class AspMvcActionAttribute : Attribute
	{
		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000912 RID: 2322 RVA: 0x0000F2C4 File Offset: 0x0000D4C4
		// (set) Token: 0x06000913 RID: 2323 RVA: 0x0000F2CC File Offset: 0x0000D4CC
		[UsedImplicitly]
		public string AnonymousProperty { get; private set; }

		// Token: 0x06000914 RID: 2324 RVA: 0x0000F2D5 File Offset: 0x0000D4D5
		public AspMvcActionAttribute()
		{
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x0000F2DD File Offset: 0x0000D4DD
		public AspMvcActionAttribute(string anonymousProperty)
		{
			this.AnonymousProperty = anonymousProperty;
		}
	}
}
