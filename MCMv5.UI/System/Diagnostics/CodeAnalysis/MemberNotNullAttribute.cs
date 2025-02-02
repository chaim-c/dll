using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000072 RID: 114
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
	[ExcludeFromCodeCoverage]
	[DebuggerNonUserCode]
	internal sealed class MemberNotNullAttribute : Attribute
	{
		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x0001390E File Offset: 0x00011B0E
		public string[] Members { get; }

		// Token: 0x06000496 RID: 1174 RVA: 0x00013916 File Offset: 0x00011B16
		public MemberNotNullAttribute(string member)
		{
			this.Members = new string[]
			{
				member
			};
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00013930 File Offset: 0x00011B30
		public MemberNotNullAttribute(params string[] members)
		{
			this.Members = members;
		}
	}
}
