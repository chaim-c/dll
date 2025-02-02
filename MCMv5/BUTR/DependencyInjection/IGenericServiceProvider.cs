using System;
using System.Runtime.CompilerServices;

namespace BUTR.DependencyInjection
{
	// Token: 0x02000147 RID: 327
	[NullableContext(1)]
	public interface IGenericServiceProvider : IDisposable
	{
		// Token: 0x060008C1 RID: 2241
		IGenericServiceProviderScope CreateScope();

		// Token: 0x060008C2 RID: 2242
		[return: Nullable(2)]
		TService GetService<TService>() where TService : class;
	}
}
