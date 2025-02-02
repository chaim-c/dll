using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x0200006E RID: 110
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	[ExcludeFromCodeCoverage]
	[DebuggerNonUserCode]
	internal sealed class DoesNotReturnAttribute : Attribute
	{
	}
}
