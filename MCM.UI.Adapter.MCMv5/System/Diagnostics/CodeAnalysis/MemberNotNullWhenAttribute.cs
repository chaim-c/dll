using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000020 RID: 32
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
	[ExcludeFromCodeCoverage]
	[DebuggerNonUserCode]
	internal sealed class MemberNotNullWhenAttribute : Attribute
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00006905 File Offset: 0x00004B05
		public bool ReturnValue { get; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000107 RID: 263 RVA: 0x0000690D File Offset: 0x00004B0D
		public string[] Members { get; }

		// Token: 0x06000108 RID: 264 RVA: 0x00006915 File Offset: 0x00004B15
		public MemberNotNullWhenAttribute(bool returnValue, string member)
		{
			this.ReturnValue = returnValue;
			this.Members = new string[]
			{
				member
			};
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00006936 File Offset: 0x00004B36
		public MemberNotNullWhenAttribute(bool returnValue, params string[] members)
		{
			this.ReturnValue = returnValue;
			this.Members = members;
		}
	}
}
