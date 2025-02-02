using System;

namespace Epic.OnlineServices
{
	// Token: 0x02000009 RID: 9
	internal class CachedArrayAllocationException : AllocationException
	{
		// Token: 0x0600007C RID: 124 RVA: 0x00003ED9 File Offset: 0x000020D9
		public CachedArrayAllocationException(IntPtr address, int foundLength, int expectedLength) : base(string.Format("Cached array allocation has length {0} but expected {1} at {2}", foundLength, expectedLength, address.ToString("X")))
		{
		}
	}
}
