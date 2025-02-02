using System;
using System.Runtime.CompilerServices;

namespace MCM.Abstractions.FluentBuilder
{
	// Token: 0x02000077 RID: 119
	[NullableContext(1)]
	public interface ISettingsBuilderFactory
	{
		// Token: 0x060002C0 RID: 704
		ISettingsBuilder Create(string id, string displayName);
	}
}
