using System;
using System.Diagnostics.CodeAnalysis;

namespace MCM.LightInject
{
	// Token: 0x020000FD RID: 253
	[ExcludeFromCodeCoverage]
	internal abstract class Registration
	{
		// Token: 0x17000176 RID: 374
		// (get) Token: 0x0600060F RID: 1551 RVA: 0x00013655 File Offset: 0x00011855
		// (set) Token: 0x06000610 RID: 1552 RVA: 0x0001365D File Offset: 0x0001185D
		public Type ServiceType { get; set; }

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x00013666 File Offset: 0x00011866
		// (set) Token: 0x06000612 RID: 1554 RVA: 0x0001366E File Offset: 0x0001186E
		public virtual Type ImplementingType { get; set; }

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000613 RID: 1555 RVA: 0x00013677 File Offset: 0x00011877
		// (set) Token: 0x06000614 RID: 1556 RVA: 0x0001367F File Offset: 0x0001187F
		public Delegate FactoryExpression { get; set; }
	}
}
