﻿using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000158 RID: 344
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
	[ExcludeFromCodeCoverage]
	[DebuggerNonUserCode]
	internal sealed class AllowNullAttribute : Attribute
	{
	}
}
