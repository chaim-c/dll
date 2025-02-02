using System;
using System.Threading;

namespace TaleWorlds.Library
{
	// Token: 0x02000095 RID: 149
	public class TWSharedMutex
	{
		// Token: 0x0600051E RID: 1310 RVA: 0x00010F6F File Offset: 0x0000F16F
		public TWSharedMutex()
		{
			this._mutex = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00010F83 File Offset: 0x0000F183
		public void EnterReadLock()
		{
			this._mutex.EnterReadLock();
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00010F90 File Offset: 0x0000F190
		public void EnterUpgradeableReadLock()
		{
			this._mutex.EnterUpgradeableReadLock();
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00010F9D File Offset: 0x0000F19D
		public void EnterWriteLock()
		{
			this._mutex.EnterWriteLock();
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x00010FAA File Offset: 0x0000F1AA
		public void ExitReadLock()
		{
			this._mutex.ExitReadLock();
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x00010FB7 File Offset: 0x0000F1B7
		public void ExitUpgradeableReadLock()
		{
			this._mutex.ExitUpgradeableReadLock();
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00010FC4 File Offset: 0x0000F1C4
		public void ExitWriteLock()
		{
			this._mutex.ExitWriteLock();
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x00010FD1 File Offset: 0x0000F1D1
		public bool IsReadLockHeld
		{
			get
			{
				return this._mutex.IsReadLockHeld;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000526 RID: 1318 RVA: 0x00010FDE File Offset: 0x0000F1DE
		public bool IsUpgradeableReadLockHeld
		{
			get
			{
				return this._mutex.IsUpgradeableReadLockHeld;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000527 RID: 1319 RVA: 0x00010FEB File Offset: 0x0000F1EB
		public bool IsWriteLockHeld
		{
			get
			{
				return this._mutex.IsWriteLockHeld;
			}
		}

		// Token: 0x04000182 RID: 386
		private ReaderWriterLockSlim _mutex;
	}
}
