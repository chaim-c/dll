using System;

namespace TaleWorlds.Library
{
	// Token: 0x02000096 RID: 150
	public struct TWSharedMutexReadLock : IDisposable
	{
		// Token: 0x06000528 RID: 1320 RVA: 0x00010FF8 File Offset: 0x0000F1F8
		public TWSharedMutexReadLock(TWSharedMutex mtx)
		{
			mtx.EnterReadLock();
			this._mtx = mtx;
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00011007 File Offset: 0x0000F207
		public void Dispose()
		{
			this._mtx.ExitReadLock();
		}

		// Token: 0x04000183 RID: 387
		private readonly TWSharedMutex _mtx;
	}
}
