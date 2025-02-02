using System;

namespace Epic.OnlineServices
{
	// Token: 0x02000007 RID: 7
	internal class ExternalAllocationException : AllocationException
	{
		// Token: 0x0600007A RID: 122 RVA: 0x00003E96 File Offset: 0x00002096
		public ExternalAllocationException(IntPtr address, Type type) : base(string.Format("Attempting to allocate '{0}' over externally allocated memory at {1}", type, address.ToString("X")))
		{
		}
	}
}
