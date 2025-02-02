using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x0200015F RID: 351
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
	[ExcludeFromCodeCoverage]
	[DebuggerNonUserCode]
	internal sealed class MemberNotNullWhenAttribute : Attribute
	{
		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060009CC RID: 2508 RVA: 0x00021B19 File Offset: 0x0001FD19
		public bool ReturnValue { get; }

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060009CD RID: 2509 RVA: 0x00021B21 File Offset: 0x0001FD21
		public string[] Members { get; }

		// Token: 0x060009CE RID: 2510 RVA: 0x00021B29 File Offset: 0x0001FD29
		public MemberNotNullWhenAttribute(bool returnValue, string member)
		{
			this.ReturnValue = returnValue;
			this.Members = new string[]
			{
				member
			};
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x00021B4A File Offset: 0x0001FD4A
		public MemberNotNullWhenAttribute(bool returnValue, params string[] members)
		{
			this.ReturnValue = returnValue;
			this.Members = members;
		}
	}
}
