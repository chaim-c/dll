using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200011A RID: 282
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SendInviteCallbackInfoInternal : ICallbackInfoInternal, IGettable<SendInviteCallbackInfo>, ISettable<SendInviteCallbackInfo>, IDisposable
	{
		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000890 RID: 2192 RVA: 0x0000C4B8 File Offset: 0x0000A6B8
		// (set) Token: 0x06000891 RID: 2193 RVA: 0x0000C4D0 File Offset: 0x0000A6D0
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

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000892 RID: 2194 RVA: 0x0000C4DC File Offset: 0x0000A6DC
		// (set) Token: 0x06000893 RID: 2195 RVA: 0x0000C4FD File Offset: 0x0000A6FD
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

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000894 RID: 2196 RVA: 0x0000C510 File Offset: 0x0000A710
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x0000C528 File Offset: 0x0000A728
		public void Set(ref SendInviteCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x0000C548 File Offset: 0x0000A748
		public void Set(ref SendInviteCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
			}
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x0000C58C File Offset: 0x0000A78C
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x0000C59B File Offset: 0x0000A79B
		public void Get(out SendInviteCallbackInfo output)
		{
			output = default(SendInviteCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x040003E7 RID: 999
		private Result m_ResultCode;

		// Token: 0x040003E8 RID: 1000
		private IntPtr m_ClientData;
	}
}
