using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x0200001B RID: 27
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	[ExcludeFromCodeCoverage]
	[DebuggerNonUserCode]
	internal sealed class DoesNotReturnAttribute : Attribute
	{
	}
}
