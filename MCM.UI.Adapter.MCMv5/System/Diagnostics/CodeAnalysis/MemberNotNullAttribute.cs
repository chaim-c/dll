using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x0200001F RID: 31
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
	[ExcludeFromCodeCoverage]
	[DebuggerNonUserCode]
	internal sealed class MemberNotNullAttribute : Attribute
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000103 RID: 259 RVA: 0x000068D2 File Offset: 0x00004AD2
		public string[] Members { get; }

		// Token: 0x06000104 RID: 260 RVA: 0x000068DA File Offset: 0x00004ADA
		public MemberNotNullAttribute(string member)
		{
			this.Members = new string[]
			{
				member
			};
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000068F4 File Offset: 0x00004AF4
		public MemberNotNullAttribute(params string[] members)
		{
			this.Members = members;
		}
	}
}
