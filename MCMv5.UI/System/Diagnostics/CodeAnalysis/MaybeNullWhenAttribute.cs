using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000071 RID: 113
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	[ExcludeFromCodeCoverage]
	[DebuggerNonUserCode]
	internal sealed class MaybeNullWhenAttribute : Attribute
	{
		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x000138F5 File Offset: 0x00011AF5
		public bool ReturnValue { get; }

		// Token: 0x06000494 RID: 1172 RVA: 0x000138FD File Offset: 0x00011AFD
		public MaybeNullWhenAttribute(bool returnValue)
		{
			this.ReturnValue = returnValue;
		}
	}
}
