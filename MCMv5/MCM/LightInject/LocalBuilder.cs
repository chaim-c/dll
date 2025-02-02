using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace MCM.LightInject
{
	// Token: 0x020000F6 RID: 246
	[ExcludeFromCodeCoverage]
	internal class LocalBuilder
	{
		// Token: 0x060005EF RID: 1519 RVA: 0x0001314C File Offset: 0x0001134C
		public LocalBuilder(Type type, int localIndex)
		{
			this.Variable = Expression.Parameter(type);
			this.LocalType = type;
			this.LocalIndex = localIndex;
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x00013173 File Offset: 0x00011373
		// (set) Token: 0x060005F1 RID: 1521 RVA: 0x0001317B File Offset: 0x0001137B
		public ParameterExpression Variable { get; private set; }

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x00013184 File Offset: 0x00011384
		// (set) Token: 0x060005F3 RID: 1523 RVA: 0x0001318C File Offset: 0x0001138C
		public Type LocalType { get; private set; }

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060005F4 RID: 1524 RVA: 0x00013195 File Offset: 0x00011395
		// (set) Token: 0x060005F5 RID: 1525 RVA: 0x0001319D File Offset: 0x0001139D
		public int LocalIndex { get; private set; }
	}
}
