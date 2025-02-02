using System;

namespace TaleWorlds.TwoDimension
{
	// Token: 0x02000035 RID: 53
	public interface ITexture
	{
		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600024F RID: 591
		int Width { get; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000250 RID: 592
		int Height { get; }

		// Token: 0x06000251 RID: 593
		void Release();

		// Token: 0x06000252 RID: 594
		bool IsLoaded();
	}
}
