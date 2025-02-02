using System;
using System.Runtime.CompilerServices;

namespace BUTR.DependencyInjection
{
	// Token: 0x02000148 RID: 328
	public interface IGenericServiceProviderScope : IDisposable
	{
		// Token: 0x060008C3 RID: 2243
		[NullableContext(1)]
		[return: Nullable(2)]
		TService GetService<TService>() where TService : class;
	}
}
