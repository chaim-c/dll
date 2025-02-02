using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000568 RID: 1384
	public struct UserLoginInfo
	{
		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x0600235E RID: 9054 RVA: 0x00034555 File Offset: 0x00032755
		// (set) Token: 0x0600235F RID: 9055 RVA: 0x0003455D File Offset: 0x0003275D
		public Utf8String DisplayName { get; set; }

		// Token: 0x06002360 RID: 9056 RVA: 0x00034566 File Offset: 0x00032766
		internal void Set(ref UserLoginInfoInternal other)
		{
			this.DisplayName = other.DisplayName;
		}
	}
}
