using System;
using System.Threading;

namespace TaleWorlds.Library
{
	// Token: 0x02000088 RID: 136
	public static class SingleThreadedSynchronizationContextManager
	{
		// Token: 0x060004A7 RID: 1191 RVA: 0x0000FDE8 File Offset: 0x0000DFE8
		public static void Initialize()
		{
			if (SingleThreadedSynchronizationContextManager._synchronizationContext == null)
			{
				SingleThreadedSynchronizationContextManager._synchronizationContext = new SingleThreadedSynchronizationContext();
				SynchronizationContext.SetSynchronizationContext(SingleThreadedSynchronizationContextManager._synchronizationContext);
			}
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x0000FE05 File Offset: 0x0000E005
		public static void Tick()
		{
			SingleThreadedSynchronizationContextManager._synchronizationContext.Tick();
		}

		// Token: 0x04000167 RID: 359
		private static SingleThreadedSynchronizationContext _synchronizationContext;
	}
}
