using System;

namespace TaleWorlds.Library
{
	// Token: 0x02000097 RID: 151
	public struct TWSharedMutexWriteLock : IDisposable
	{
		// Token: 0x0600052A RID: 1322 RVA: 0x00011014 File Offset: 0x0000F214
		public TWSharedMutexWriteLock(TWSharedMutex mtx)
		{
			mtx.EnterWriteLock();
			this._mtx = mtx;
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00011023 File Offset: 0x0000F223
		public void Dispose()
		{
			this._mtx.ExitWriteLock();
		}

		// Token: 0x04000184 RID: 388
		private readonly TWSharedMutex _mtx;
	}
}
