using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x0200015E RID: 350
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
	[ExcludeFromCodeCoverage]
	[DebuggerNonUserCode]
	internal sealed class MemberNotNullAttribute : Attribute
	{
		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060009C9 RID: 2505 RVA: 0x00021AE6 File Offset: 0x0001FCE6
		public string[] Members { get; }

		// Token: 0x060009CA RID: 2506 RVA: 0x00021AEE File Offset: 0x0001FCEE
		public MemberNotNullAttribute(string member)
		{
			this.Members = new string[]
			{
				member
			};
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x00021B08 File Offset: 0x0001FD08
		public MemberNotNullAttribute(params string[] members)
		{
			this.Members = members;
		}
	}
}
