using System;

namespace JetBrains.Annotations
{
	// Token: 0x020000F0 RID: 240
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Parameter)]
	public sealed class AspMvcControllerAttribute : Attribute
	{
		// Token: 0x17000205 RID: 517
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x0000F314 File Offset: 0x0000D514
		// (set) Token: 0x0600091B RID: 2331 RVA: 0x0000F31C File Offset: 0x0000D51C
		[UsedImplicitly]
		public string AnonymousProperty { get; private set; }

		// Token: 0x0600091C RID: 2332 RVA: 0x0000F325 File Offset: 0x0000D525
		public AspMvcControllerAttribute()
		{
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x0000F32D File Offset: 0x0000D52D
		public AspMvcControllerAttribute(string anonymousProperty)
		{
			this.AnonymousProperty = anonymousProperty;
		}
	}
}
