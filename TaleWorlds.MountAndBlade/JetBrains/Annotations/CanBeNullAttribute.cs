﻿using System;

namespace JetBrains.Annotations
{
	// Token: 0x020000E2 RID: 226
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Delegate, AllowMultiple = false, Inherited = true)]
	public sealed class CanBeNullAttribute : Attribute
	{
	}
}
