using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MCM.LightInject
{
	// Token: 0x02000111 RID: 273
	[ExcludeFromCodeCoverage]
	internal class CompositionRootExecutor : ICompositionRootExecutor
	{
		// Token: 0x06000685 RID: 1669 RVA: 0x0001471F File Offset: 0x0001291F
		public CompositionRootExecutor(IServiceRegistry serviceRegistry, Func<Type, ICompositionRoot> activator)
		{
			this.serviceRegistry = serviceRegistry;
			this.activator = activator;
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x00014750 File Offset: 0x00012950
		public void Execute(Type compositionRootType)
		{
			bool flag = !this.executedCompositionRoots.Contains(compositionRootType);
			if (flag)
			{
				object obj = this.syncRoot;
				lock (obj)
				{
					bool flag3 = !this.executedCompositionRoots.Contains(compositionRootType);
					if (flag3)
					{
						this.executedCompositionRoots.Add(compositionRootType);
						ICompositionRoot compositionRoot = this.activator(compositionRootType);
						compositionRoot.Compose(this.serviceRegistry);
					}
				}
			}
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x000147E4 File Offset: 0x000129E4
		public void Execute<TCompositionRoot>(TCompositionRoot compositionRoot) where TCompositionRoot : ICompositionRoot
		{
			bool flag = !this.executedCompositionRoots.Contains(typeof(TCompositionRoot));
			if (flag)
			{
				object obj = this.syncRoot;
				lock (obj)
				{
					bool flag3 = !this.executedCompositionRoots.Contains(typeof(TCompositionRoot));
					if (flag3)
					{
						this.executedCompositionRoots.Add(typeof(TCompositionRoot));
						compositionRoot.Compose(this.serviceRegistry);
					}
				}
			}
		}

		// Token: 0x040001E6 RID: 486
		private readonly IServiceRegistry serviceRegistry;

		// Token: 0x040001E7 RID: 487
		private readonly Func<Type, ICompositionRoot> activator;

		// Token: 0x040001E8 RID: 488
		private readonly IList<Type> executedCompositionRoots = new List<Type>();

		// Token: 0x040001E9 RID: 489
		private readonly object syncRoot = new object();
	}
}
