using System;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001AC RID: 428
	public struct AudioDevicesChangedCallbackInfo : ICallbackInfo
	{
		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000C34 RID: 3124 RVA: 0x00012411 File Offset: 0x00010611
		// (set) Token: 0x06000C35 RID: 3125 RVA: 0x00012419 File Offset: 0x00010619
		public object ClientData { get; set; }

		// Token: 0x06000C36 RID: 3126 RVA: 0x00012424 File Offset: 0x00010624
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x0001243F File Offset: 0x0001063F
		internal void Set(ref AudioDevicesChangedCallbackInfoInternal other)
		{
			this.ClientData = other.ClientData;
		}
	}
}
