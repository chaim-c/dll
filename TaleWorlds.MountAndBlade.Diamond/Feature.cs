using System;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000119 RID: 281
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	public class Feature : Attribute
	{
		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000628 RID: 1576 RVA: 0x0000832C File Offset: 0x0000652C
		// (set) Token: 0x06000629 RID: 1577 RVA: 0x00008334 File Offset: 0x00006534
		public Features FeatureFlag { get; private set; }

		// Token: 0x0600062A RID: 1578 RVA: 0x0000833D File Offset: 0x0000653D
		public Feature(Features flag)
		{
			this.FeatureFlag = flag;
		}
	}
}
