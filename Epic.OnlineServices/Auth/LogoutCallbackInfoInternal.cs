using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x0200058E RID: 1422
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LogoutCallbackInfoInternal : ICallbackInfoInternal, IGettable<LogoutCallbackInfo>, ISettable<LogoutCallbackInfo>, IDisposable
	{
		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x06002482 RID: 9346 RVA: 0x00036658 File Offset: 0x00034858
		// (set) Token: 0x06002483 RID: 9347 RVA: 0x00036670 File Offset: 0x00034870
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

		// Token: 0x17000ABA RID: 2746
		// (get) Token: 0x06002484 RID: 9348 RVA: 0x0003667C File Offset: 0x0003487C
		// (set) Token: 0x06002485 RID: 9349 RVA: 0x0003669D File Offset: 0x0003489D
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

		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x06002486 RID: 9350 RVA: 0x000366B0 File Offset: 0x000348B0
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000ABC RID: 2748
		// (get) Token: 0x06002487 RID: 9351 RVA: 0x000366C8 File Offset: 0x000348C8
		// (set) Token: 0x06002488 RID: 9352 RVA: 0x000366E9 File Offset: 0x000348E9
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId result;
				Helper.Get<EpicAccountId>(this.m_LocalUserId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x06002489 RID: 9353 RVA: 0x000366F9 File Offset: 0x000348F9
		public void Set(ref LogoutCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x0600248A RID: 9354 RVA: 0x00036724 File Offset: 0x00034924
		public void Set(ref LogoutCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x0600248B RID: 9355 RVA: 0x0003677D File Offset: 0x0003497D
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x0600248C RID: 9356 RVA: 0x00036798 File Offset: 0x00034998
		public void Get(out LogoutCallbackInfo output)
		{
			output = default(LogoutCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x0400101E RID: 4126
		private Result m_ResultCode;

		// Token: 0x0400101F RID: 4127
		private IntPtr m_ClientData;

		// Token: 0x04001020 RID: 4128
		private IntPtr m_LocalUserId;
	}
}
