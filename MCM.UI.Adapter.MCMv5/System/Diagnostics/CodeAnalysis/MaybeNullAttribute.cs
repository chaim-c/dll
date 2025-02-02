using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x0200001D RID: 29
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, Inherited = false)]
	[ExcludeFromCodeCoverage]
	[DebuggerNonUserCode]
	internal sealed class MaybeNullAttribute : Attribute
	{
	}
}
