﻿using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200007F RID: 127
	[NullableContext(1)]
	public interface IValueProvider
	{
		// Token: 0x06000669 RID: 1641
		void SetValue(object target, [Nullable(2)] object value);

		// Token: 0x0600066A RID: 1642
		[return: Nullable(2)]
		object GetValue(object target);
	}
}
