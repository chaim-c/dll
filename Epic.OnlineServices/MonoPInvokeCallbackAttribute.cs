using System;

namespace Epic.OnlineServices
{
	// Token: 0x0200000F RID: 15
	[AttributeUsage(AttributeTargets.Method)]
	internal sealed class MonoPInvokeCallbackAttribute : Attribute
	{
		// Token: 0x06000084 RID: 132 RVA: 0x00003F1A File Offset: 0x0000211A
		public MonoPInvokeCallbackAttribute(Type type)
		{
		}
	}
}
