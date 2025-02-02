using System;
using System.Runtime.CompilerServices;

namespace BUTR.DependencyInjection
{
	// Token: 0x02000146 RID: 326
	[NullableContext(1)]
	public interface IGenericServiceFactory
	{
		// Token: 0x060008C0 RID: 2240
		TService GetService<TService>() where TService : class;
	}
}
