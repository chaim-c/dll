using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x0200001E RID: 30
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	[ExcludeFromCodeCoverage]
	[DebuggerNonUserCode]
	internal sealed class MaybeNullWhenAttribute : Attribute
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000101 RID: 257 RVA: 0x000068B9 File Offset: 0x00004AB9
		public bool ReturnValue { get; }

		// Token: 0x06000102 RID: 258 RVA: 0x000068C1 File Offset: 0x00004AC1
		public MaybeNullWhenAttribute(bool returnValue)
		{
			this.ReturnValue = returnValue;
		}
	}
}
