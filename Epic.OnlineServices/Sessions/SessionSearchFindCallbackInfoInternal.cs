using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000146 RID: 326
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionSearchFindCallbackInfoInternal : ICallbackInfoInternal, IGettable<SessionSearchFindCallbackInfo>, ISettable<SessionSearchFindCallbackInfo>, IDisposable
	{
		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000985 RID: 2437 RVA: 0x0000DE74 File Offset: 0x0000C074
		// (set) Token: 0x06000986 RID: 2438 RVA: 0x0000DE8C File Offset: 0x0000C08C
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
			set
			{
				this.m_ResultCode = value;
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000987 RID: 2439 RVA: 0x0000DE98 File Offset: 0x0000C098
		// (set) Token: 0x06000988 RID: 2440 RVA: 0x0000DEB9 File Offset: 0x0000C0B9
		public object ClientData
		{
			get
			{
				object result;
				Helper.Get(this.m_ClientData, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_ClientData);
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000989 RID: 2441 RVA: 0x0000DECC File Offset: 0x0000C0CC
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x0000DEE4 File Offset: 0x0000C0E4
		public void Set(ref SessionSearchFindCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x0000DF04 File Offset: 0x0000C104
		public void Set(ref SessionSearchFindCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
			}
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x0000DF48 File Offset: 0x0000C148
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x0000DF57 File Offset: 0x0000C157
		public void Get(out SessionSearchFindCallbackInfo output)
		{
			output = default(SessionSearchFindCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000462 RID: 1122
		private Result m_ResultCode;

		// Token: 0x04000463 RID: 1123
		private IntPtr m_ClientData;
	}
}
