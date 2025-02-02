using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000151 RID: 337
	public struct SessionSearchSetParameterOptions
	{
		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060009AE RID: 2478 RVA: 0x0000E13D File Offset: 0x0000C33D
		// (set) Token: 0x060009AF RID: 2479 RVA: 0x0000E145 File Offset: 0x0000C345
		public AttributeData? Parameter { get; set; }

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060009B0 RID: 2480 RVA: 0x0000E14E File Offset: 0x0000C34E
		// (set) Token: 0x060009B1 RID: 2481 RVA: 0x0000E156 File Offset: 0x0000C356
		public ComparisonOp ComparisonOp { get; set; }
	}
}
