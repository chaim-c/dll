using System;
using System.Diagnostics.CodeAnalysis;

namespace MCM.LightInject
{
	// Token: 0x02000105 RID: 261
	[LifeSpan(30)]
	[ExcludeFromCodeCoverage]
	internal class PerContainerLifetime : ILifetime, ICloneableLifeTime
	{
		// Token: 0x06000650 RID: 1616 RVA: 0x00013BB3 File Offset: 0x00011DB3
		public object GetInstance(Func<object> createInstance, Scope scope)
		{
			throw new NotImplementedException("Optimized");
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x00013BC0 File Offset: 0x00011DC0
		public object GetInstance(GetInstanceDelegate createInstance, Scope scope, object[] arguments)
		{
			bool flag = this.singleton != null;
			object result;
			if (flag)
			{
				result = this.singleton;
			}
			else
			{
				object obj = this.syncRoot;
				lock (obj)
				{
					bool flag3 = this.singleton == null;
					if (flag3)
					{
						this.singleton = createInstance(arguments, scope);
					}
				}
				result = this.singleton;
			}
			return result;
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x00013C4C File Offset: 0x00011E4C
		public ILifetime Clone()
		{
			return new PerContainerLifetime();
		}

		// Token: 0x040001D4 RID: 468
		private readonly object syncRoot = new object();

		// Token: 0x040001D5 RID: 469
		private volatile object singleton;
	}
}
