using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003E6 RID: 998
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RejectInviteCallbackInfoInternal : ICallbackInfoInternal, IGettable<RejectInviteCallbackInfo>, ISettable<RejectInviteCallbackInfo>, IDisposable
	{
		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x060019CD RID: 6605 RVA: 0x00026230 File Offset: 0x00024430
		// (set) Token: 0x060019CE RID: 6606 RVA: 0x00026248 File Offset: 0x00024448
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

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x060019CF RID: 6607 RVA: 0x00026254 File Offset: 0x00024454
		// (set) Token: 0x060019D0 RID: 6608 RVA: 0x00026275 File Offset: 0x00024475
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

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x060019D1 RID: 6609 RVA: 0x00026288 File Offset: 0x00024488
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x060019D2 RID: 6610 RVA: 0x000262A0 File Offset: 0x000244A0
		// (set) Token: 0x060019D3 RID: 6611 RVA: 0x000262C1 File Offset: 0x000244C1
		public Utf8String InviteId
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_InviteId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_InviteId);
			}
		}

		// Token: 0x060019D4 RID: 6612 RVA: 0x000262D1 File Offset: 0x000244D1
		public void Set(ref RejectInviteCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.InviteId = other.InviteId;
		}

		// Token: 0x060019D5 RID: 6613 RVA: 0x000262FC File Offset: 0x000244FC
		public void Set(ref RejectInviteCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.InviteId = other.Value.InviteId;
			}
		}

		// Token: 0x060019D6 RID: 6614 RVA: 0x00026355 File Offset: 0x00024555
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_InviteId);
		}

		// Token: 0x060019D7 RID: 6615 RVA: 0x00026370 File Offset: 0x00024570
		public void Get(out RejectInviteCallbackInfo output)
		{
			output = default(RejectInviteCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000B7C RID: 2940
		private Result m_ResultCode;

		// Token: 0x04000B7D RID: 2941
		private IntPtr m_ClientData;

		// Token: 0x04000B7E RID: 2942
		private IntPtr m_InviteId;
	}
}
