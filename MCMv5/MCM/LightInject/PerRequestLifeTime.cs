using System;
using System.Diagnostics.CodeAnalysis;

namespace MCM.LightInject
{
	// Token: 0x02000106 RID: 262
	[LifeSpan(10)]
	[ExcludeFromCodeCoverage]
	internal class PerRequestLifeTime : ILifetime, ICloneableLifeTime
	{
		// Token: 0x06000654 RID: 1620 RVA: 0x00013C77 File Offset: 0x00011E77
		public object GetInstance(Func<object> createInstance, Scope scope)
		{
			throw new NotImplementedException("Optimized");
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x00013C84 File Offset: 0x00011E84
		public ILifetime Clone()
		{
			return new PerRequestLifeTime();
		}
	}
}
