using System;
using Microsoft.CodeAnalysis;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000008 RID: 8
	[CompilerGenerated]
	[Embedded]
	[AttributeUsage(AttributeTargets.Module, AllowMultiple = false, Inherited = false)]
	internal sealed class RefSafetyRulesAttribute : Attribute
	{
		// Token: 0x0600000E RID: 14 RVA: 0x0000222A File Offset: 0x0000042A
		public RefSafetyRulesAttribute(int A_1)
		{
			this.Version = A_1;
		}

		// Token: 0x04000006 RID: 6
		public readonly int Version;
	}
}
