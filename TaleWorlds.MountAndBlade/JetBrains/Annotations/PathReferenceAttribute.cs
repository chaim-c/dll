using System;

namespace JetBrains.Annotations
{
	// Token: 0x020000ED RID: 237
	[AttributeUsage(AttributeTargets.Parameter)]
	public class PathReferenceAttribute : Attribute
	{
		// Token: 0x0600090E RID: 2318 RVA: 0x0000F29C File Offset: 0x0000D49C
		public PathReferenceAttribute()
		{
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x0000F2A4 File Offset: 0x0000D4A4
		[UsedImplicitly]
		public PathReferenceAttribute([PathReference] string basePath)
		{
			this.BasePath = basePath;
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000910 RID: 2320 RVA: 0x0000F2B3 File Offset: 0x0000D4B3
		// (set) Token: 0x06000911 RID: 2321 RVA: 0x0000F2BB File Offset: 0x0000D4BB
		[UsedImplicitly]
		public string BasePath { get; private set; }
	}
}
