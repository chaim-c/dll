using System;

namespace Epic.OnlineServices.Platform
{
	// Token: 0x0200064B RID: 1611
	public struct GetDesktopCrossplayStatusInfo
	{
		// Token: 0x17000C46 RID: 3142
		// (get) Token: 0x0600291E RID: 10526 RVA: 0x0003D669 File Offset: 0x0003B869
		// (set) Token: 0x0600291F RID: 10527 RVA: 0x0003D671 File Offset: 0x0003B871
		public DesktopCrossplayStatus Status { get; set; }

		// Token: 0x17000C47 RID: 3143
		// (get) Token: 0x06002920 RID: 10528 RVA: 0x0003D67A File Offset: 0x0003B87A
		// (set) Token: 0x06002921 RID: 10529 RVA: 0x0003D682 File Offset: 0x0003B882
		public int ServiceInitResult { get; set; }

		// Token: 0x06002922 RID: 10530 RVA: 0x0003D68B File Offset: 0x0003B88B
		internal void Set(ref GetDesktopCrossplayStatusInfoInternal other)
		{
			this.Status = other.Status;
			this.ServiceInitResult = other.ServiceInitResult;
		}
	}
}
