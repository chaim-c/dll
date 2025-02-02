using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x0200015A RID: 346
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	[ExcludeFromCodeCoverage]
	[DebuggerNonUserCode]
	internal sealed class DoesNotReturnAttribute : Attribute
	{
	}
}
