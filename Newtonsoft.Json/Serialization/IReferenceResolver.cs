﻿using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200007C RID: 124
	[NullableContext(1)]
	public interface IReferenceResolver
	{
		// Token: 0x06000661 RID: 1633
		object ResolveReference(object context, string reference);

		// Token: 0x06000662 RID: 1634
		string GetReference(object context, object value);

		// Token: 0x06000663 RID: 1635
		bool IsReferenced(object context, object value);

		// Token: 0x06000664 RID: 1636
		void AddReference(object context, string reference, object value);
	}
}
