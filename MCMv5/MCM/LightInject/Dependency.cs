using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace MCM.LightInject
{
	// Token: 0x02000102 RID: 258
	[ExcludeFromCodeCoverage]
	internal abstract class Dependency
	{
		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000639 RID: 1593 RVA: 0x00013A09 File Offset: 0x00011C09
		// (set) Token: 0x0600063A RID: 1594 RVA: 0x00013A11 File Offset: 0x00011C11
		public Type ServiceType { get; set; }

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x0600063B RID: 1595 RVA: 0x00013A1A File Offset: 0x00011C1A
		// (set) Token: 0x0600063C RID: 1596 RVA: 0x00013A22 File Offset: 0x00011C22
		public string ServiceName { get; set; }

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x0600063D RID: 1597 RVA: 0x00013A2B File Offset: 0x00011C2B
		// (set) Token: 0x0600063E RID: 1598 RVA: 0x00013A33 File Offset: 0x00011C33
		public Delegate FactoryExpression { get; set; }

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x0600063F RID: 1599
		public abstract string Name { get; }

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000640 RID: 1600 RVA: 0x00013A3C File Offset: 0x00011C3C
		// (set) Token: 0x06000641 RID: 1601 RVA: 0x00013A44 File Offset: 0x00011C44
		public bool IsRequired { get; set; }

		// Token: 0x06000642 RID: 1602 RVA: 0x00013A50 File Offset: 0x00011C50
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			return sb.AppendFormat("[Requested dependency: ServiceType:{0}, ServiceName:{1}]", this.ServiceType, this.ServiceName).ToString();
		}
	}
}
