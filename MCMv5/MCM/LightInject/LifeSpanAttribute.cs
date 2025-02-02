using System;
using System.Diagnostics.CodeAnalysis;

namespace MCM.LightInject
{
	// Token: 0x0200010B RID: 267
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	[ExcludeFromCodeCoverage]
	internal class LifeSpanAttribute : Attribute
	{
		// Token: 0x06000675 RID: 1653 RVA: 0x0001414C File Offset: 0x0001234C
		public LifeSpanAttribute(int value)
		{
			this.Value = value;
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000676 RID: 1654 RVA: 0x0001415D File Offset: 0x0001235D
		public int Value { get; }
	}
}
