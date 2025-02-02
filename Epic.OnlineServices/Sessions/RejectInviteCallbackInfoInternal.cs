using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000116 RID: 278
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RejectInviteCallbackInfoInternal : ICallbackInfoInternal, IGettable<RejectInviteCallbackInfo>, ISettable<RejectInviteCallbackInfo>, IDisposable
	{
		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000878 RID: 2168 RVA: 0x0000C298 File Offset: 0x0000A498
		// (set) Token: 0x06000879 RID: 2169 RVA: 0x0000C2B0 File Offset: 0x0000A4B0
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

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x0600087A RID: 2170 RVA: 0x0000C2BC File Offset: 0x0000A4BC
		// (set) Token: 0x0600087B RID: 2171 RVA: 0x0000C2DD File Offset: 0x0000A4DD
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

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x0600087C RID: 2172 RVA: 0x0000C2F0 File Offset: 0x0000A4F0
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x0000C308 File Offset: 0x0000A508
		public void Set(ref RejectInviteCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x0000C328 File Offset: 0x0000A528
		public void Set(ref RejectInviteCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
			}
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x0000C36C File Offset: 0x0000A56C
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x0000C37B File Offset: 0x0000A57B
		public void Get(out RejectInviteCallbackInfo output)
		{
			output = default(RejectInviteCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x040003DE RID: 990
		private Result m_ResultCode;

		// Token: 0x040003DF RID: 991
		private IntPtr m_ClientData;
	}
}
