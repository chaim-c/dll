using System;

namespace TaleWorlds.MountAndBlade.View
{
	// Token: 0x0200001C RID: 28
	public class OverrideView : Attribute
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00007630 File Offset: 0x00005830
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x00007638 File Offset: 0x00005838
		public Type BaseType { get; private set; }

		// Token: 0x060000CA RID: 202 RVA: 0x00007641 File Offset: 0x00005841
		public OverrideView(Type baseType)
		{
			this.BaseType = baseType;
		}
	}
}
