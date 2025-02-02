using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x0200006D RID: 109
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
	[ExcludeFromCodeCoverage]
	[DebuggerNonUserCode]
	internal sealed class DisallowNullAttribute : Attribute
	{
	}
}
