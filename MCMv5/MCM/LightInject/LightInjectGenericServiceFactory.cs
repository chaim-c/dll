using System;
using System.Runtime.CompilerServices;
using BUTR.DependencyInjection;

namespace MCM.LightInject
{
	// Token: 0x020000C4 RID: 196
	[NullableContext(1)]
	[Nullable(0)]
	internal class LightInjectGenericServiceFactory : IGenericServiceFactory
	{
		// Token: 0x0600041E RID: 1054 RVA: 0x0000C596 File Offset: 0x0000A796
		public LightInjectGenericServiceFactory(IServiceFactory factory)
		{
			this._serviceFactory = factory;
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0000C5A6 File Offset: 0x0000A7A6
		public TService GetService<TService>() where TService : class
		{
			return this._serviceFactory.GetInstance<TService>();
		}

		// Token: 0x04000168 RID: 360
		private readonly IServiceFactory _serviceFactory;
	}
}
