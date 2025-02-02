using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Platform
{
	// Token: 0x0200064C RID: 1612
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetDesktopCrossplayStatusInfoInternal : IGettable<GetDesktopCrossplayStatusInfo>, ISettable<GetDesktopCrossplayStatusInfo>, IDisposable
	{
		// Token: 0x17000C48 RID: 3144
		// (get) Token: 0x06002923 RID: 10531 RVA: 0x0003D6A8 File Offset: 0x0003B8A8
		// (set) Token: 0x06002924 RID: 10532 RVA: 0x0003D6C0 File Offset: 0x0003B8C0
		public DesktopCrossplayStatus Status
		{
			get
			{
				return this.m_Status;
			}
			set
			{
				this.m_Status = value;
			}
		}

		// Token: 0x17000C49 RID: 3145
		// (get) Token: 0x06002925 RID: 10533 RVA: 0x0003D6CC File Offset: 0x0003B8CC
		// (set) Token: 0x06002926 RID: 10534 RVA: 0x0003D6E4 File Offset: 0x0003B8E4
		public int ServiceInitResult
		{
			get
			{
				return this.m_ServiceInitResult;
			}
			set
			{
				this.m_ServiceInitResult = value;
			}
		}

		// Token: 0x06002927 RID: 10535 RVA: 0x0003D6EE File Offset: 0x0003B8EE
		public void Set(ref GetDesktopCrossplayStatusInfo other)
		{
			this.Status = other.Status;
			this.ServiceInitResult = other.ServiceInitResult;
		}

		// Token: 0x06002928 RID: 10536 RVA: 0x0003D70C File Offset: 0x0003B90C
		public void Set(ref GetDesktopCrossplayStatusInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.Status = other.Value.Status;
				this.ServiceInitResult = other.Value.ServiceInitResult;
			}
		}

		// Token: 0x06002929 RID: 10537 RVA: 0x0003D750 File Offset: 0x0003B950
		public void Dispose()
		{
		}

		// Token: 0x0600292A RID: 10538 RVA: 0x0003D753 File Offset: 0x0003B953
		public void Get(out GetDesktopCrossplayStatusInfo output)
		{
			output = default(GetDesktopCrossplayStatusInfo);
			output.Set(ref this);
		}

		// Token: 0x04001293 RID: 4755
		private DesktopCrossplayStatus m_Status;

		// Token: 0x04001294 RID: 4756
		private int m_ServiceInitResult;
	}
}
