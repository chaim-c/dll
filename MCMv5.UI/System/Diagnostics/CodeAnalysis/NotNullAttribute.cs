﻿using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000074 RID: 116
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, Inherited = false)]
	[ExcludeFromCodeCoverage]
	[DebuggerNonUserCode]
	internal sealed class NotNullAttribute : Attribute
	{
	}
}
