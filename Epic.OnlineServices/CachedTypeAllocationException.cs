using System;

namespace Epic.OnlineServices
{
	// Token: 0x02000008 RID: 8
	internal class CachedTypeAllocationException : AllocationException
	{
		// Token: 0x0600007B RID: 123 RVA: 0x00003EB7 File Offset: 0x000020B7
		public CachedTypeAllocationException(IntPtr address, Type foundType, Type expectedType) : base(string.Format("Cached allocation is '{0}' but expected '{1}' at {2}", foundType, expectedType, address.ToString("X")))
		{
		}
	}
}
