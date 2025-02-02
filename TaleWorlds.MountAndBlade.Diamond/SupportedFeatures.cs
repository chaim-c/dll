using System;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000164 RID: 356
	[Serializable]
	public class SupportedFeatures
	{
		// Token: 0x060009DE RID: 2526 RVA: 0x0000F580 File Offset: 0x0000D780
		public SupportedFeatures()
		{
			this.Features = -1;
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x0000F58F File Offset: 0x0000D78F
		public SupportedFeatures(int features)
		{
			this.Features = features;
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x0000F5A0 File Offset: 0x0000D7A0
		public bool SupportsFeatures(Features feature)
		{
			return (this.Features & (int)feature) == (int)feature;
		}

		// Token: 0x040004A9 RID: 1193
		public int Features;
	}
}
