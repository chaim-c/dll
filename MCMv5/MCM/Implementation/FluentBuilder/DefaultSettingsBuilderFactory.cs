using System;
using System.Runtime.CompilerServices;
using MCM.Abstractions.FluentBuilder;

namespace MCM.Implementation.FluentBuilder
{
	// Token: 0x0200002A RID: 42
	internal sealed class DefaultSettingsBuilderFactory : ISettingsBuilderFactory
	{
		// Token: 0x0600010B RID: 267 RVA: 0x00005DF7 File Offset: 0x00003FF7
		[NullableContext(1)]
		public ISettingsBuilder Create(string id, string displayName)
		{
			return new DefaultSettingsBuilder(id, displayName);
		}
	}
}
