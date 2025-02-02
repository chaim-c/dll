using System;

namespace Epic.OnlineServices
{
	// Token: 0x0200000A RID: 10
	internal class DynamicBindingException : Exception
	{
		// Token: 0x0600007D RID: 125 RVA: 0x00003F05 File Offset: 0x00002105
		public DynamicBindingException(string bindingName) : base(string.Format("Failed to hook dynamic binding for '{0}'", bindingName))
		{
		}
	}
}
