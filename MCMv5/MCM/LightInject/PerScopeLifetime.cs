using System;
using System.Diagnostics.CodeAnalysis;

namespace MCM.LightInject
{
	// Token: 0x02000107 RID: 263
	[LifeSpan(20)]
	[ExcludeFromCodeCoverage]
	internal class PerScopeLifetime : ILifetime, ICloneableLifeTime
	{
		// Token: 0x06000657 RID: 1623 RVA: 0x00013CA4 File Offset: 0x00011EA4
		public object GetInstance(Func<object> createInstance, Scope scope)
		{
			throw new NotImplementedException("Optimized");
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x00013CB4 File Offset: 0x00011EB4
		public ILifetime Clone()
		{
			return new PerScopeLifetime();
		}
	}
}
