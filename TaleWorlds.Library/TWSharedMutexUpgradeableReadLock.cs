using System;

namespace TaleWorlds.Library
{
	// Token: 0x02000098 RID: 152
	public struct TWSharedMutexUpgradeableReadLock : IDisposable
	{
		// Token: 0x0600052C RID: 1324 RVA: 0x00011030 File Offset: 0x0000F230
		public TWSharedMutexUpgradeableReadLock(TWSharedMutex mtx)
		{
			mtx.EnterUpgradeableReadLock();
			this._mtx = mtx;
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x0001103F File Offset: 0x0000F23F
		public void Dispose()
		{
			this._mtx.ExitUpgradeableReadLock();
		}

		// Token: 0x04000185 RID: 389
		private readonly TWSharedMutex _mtx;
	}
}
