using System;

namespace JetBrains.Annotations
{
	// Token: 0x020000EF RID: 239
	[AttributeUsage(AttributeTargets.Parameter)]
	public sealed class AspMvcAreaAttribute : PathReferenceAttribute
	{
		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000916 RID: 2326 RVA: 0x0000F2EC File Offset: 0x0000D4EC
		// (set) Token: 0x06000917 RID: 2327 RVA: 0x0000F2F4 File Offset: 0x0000D4F4
		[UsedImplicitly]
		public string AnonymousProperty { get; private set; }

		// Token: 0x06000918 RID: 2328 RVA: 0x0000F2FD File Offset: 0x0000D4FD
		[UsedImplicitly]
		public AspMvcAreaAttribute()
		{
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x0000F305 File Offset: 0x0000D505
		public AspMvcAreaAttribute(string anonymousProperty)
		{
			this.AnonymousProperty = anonymousProperty;
		}
	}
}
