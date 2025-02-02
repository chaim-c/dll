using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace MCM.LightInject
{
	// Token: 0x02000101 RID: 257
	[ExcludeFromCodeCoverage]
	internal class ConstructionInfo
	{
		// Token: 0x0600062E RID: 1582 RVA: 0x00013992 File Offset: 0x00011B92
		public ConstructionInfo()
		{
			this.PropertyDependencies = new List<PropertyDependency>();
			this.ConstructorDependencies = new List<ConstructorDependency>();
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x0600062F RID: 1583 RVA: 0x000139B4 File Offset: 0x00011BB4
		// (set) Token: 0x06000630 RID: 1584 RVA: 0x000139BC File Offset: 0x00011BBC
		public Type ImplementingType { get; set; }

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000631 RID: 1585 RVA: 0x000139C5 File Offset: 0x00011BC5
		// (set) Token: 0x06000632 RID: 1586 RVA: 0x000139CD File Offset: 0x00011BCD
		public ConstructorInfo Constructor { get; set; }

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000633 RID: 1587 RVA: 0x000139D6 File Offset: 0x00011BD6
		// (set) Token: 0x06000634 RID: 1588 RVA: 0x000139DE File Offset: 0x00011BDE
		public List<PropertyDependency> PropertyDependencies { get; private set; }

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000635 RID: 1589 RVA: 0x000139E7 File Offset: 0x00011BE7
		// (set) Token: 0x06000636 RID: 1590 RVA: 0x000139EF File Offset: 0x00011BEF
		public List<ConstructorDependency> ConstructorDependencies { get; private set; }

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000637 RID: 1591 RVA: 0x000139F8 File Offset: 0x00011BF8
		// (set) Token: 0x06000638 RID: 1592 RVA: 0x00013A00 File Offset: 0x00011C00
		public Delegate FactoryDelegate { get; set; }
	}
}
