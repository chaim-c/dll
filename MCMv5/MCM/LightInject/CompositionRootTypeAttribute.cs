using System;
using System.Diagnostics.CodeAnalysis;

namespace MCM.LightInject
{
	// Token: 0x0200010C RID: 268
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	[ExcludeFromCodeCoverage]
	internal class CompositionRootTypeAttribute : Attribute
	{
		// Token: 0x06000677 RID: 1655 RVA: 0x00014165 File Offset: 0x00012365
		public CompositionRootTypeAttribute(Type compositionRootType)
		{
			this.CompositionRootType = compositionRootType;
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000678 RID: 1656 RVA: 0x00014177 File Offset: 0x00012377
		// (set) Token: 0x06000679 RID: 1657 RVA: 0x0001417F File Offset: 0x0001237F
		public Type CompositionRootType { get; private set; }
	}
}
