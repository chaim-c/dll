using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000073 RID: 115
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
	[ExcludeFromCodeCoverage]
	[DebuggerNonUserCode]
	internal sealed class MemberNotNullWhenAttribute : Attribute
	{
		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x00013941 File Offset: 0x00011B41
		public bool ReturnValue { get; }

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x00013949 File Offset: 0x00011B49
		public string[] Members { get; }

		// Token: 0x0600049A RID: 1178 RVA: 0x00013951 File Offset: 0x00011B51
		public MemberNotNullWhenAttribute(bool returnValue, string member)
		{
			this.ReturnValue = returnValue;
			this.Members = new string[]
			{
				member
			};
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00013972 File Offset: 0x00011B72
		public MemberNotNullWhenAttribute(bool returnValue, params string[] members)
		{
			this.ReturnValue = returnValue;
			this.Members = members;
		}
	}
}
