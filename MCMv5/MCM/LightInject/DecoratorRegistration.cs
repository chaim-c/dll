using System;
using System.Diagnostics.CodeAnalysis;

namespace MCM.LightInject
{
	// Token: 0x020000FE RID: 254
	[ExcludeFromCodeCoverage]
	internal class DecoratorRegistration : Registration
	{
		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x00013691 File Offset: 0x00011891
		// (set) Token: 0x06000617 RID: 1559 RVA: 0x00013699 File Offset: 0x00011899
		public Func<ServiceRegistration, bool> CanDecorate { get; set; }

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000618 RID: 1560 RVA: 0x000136A2 File Offset: 0x000118A2
		// (set) Token: 0x06000619 RID: 1561 RVA: 0x000136AA File Offset: 0x000118AA
		public Func<IServiceFactory, ServiceRegistration, Type> ImplementingTypeFactory { get; set; }

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x0600061A RID: 1562 RVA: 0x000136B3 File Offset: 0x000118B3
		// (set) Token: 0x0600061B RID: 1563 RVA: 0x000136BB File Offset: 0x000118BB
		public int Index { get; set; }

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x0600061C RID: 1564 RVA: 0x000136C4 File Offset: 0x000118C4
		public bool HasDeferredImplementingType
		{
			get
			{
				return this.ImplementingType == null && base.FactoryExpression == null;
			}
		}
	}
}
