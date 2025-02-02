using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x020005AE RID: 1454
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct VerifyUserAuthCallbackInfoInternal : ICallbackInfoInternal, IGettable<VerifyUserAuthCallbackInfo>, ISettable<VerifyUserAuthCallbackInfo>, IDisposable
	{
		// Token: 0x17000B07 RID: 2823
		// (get) Token: 0x0600257C RID: 9596 RVA: 0x00037A34 File Offset: 0x00035C34
		// (set) Token: 0x0600257D RID: 9597 RVA: 0x00037A4C File Offset: 0x00035C4C
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

		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x0600257E RID: 9598 RVA: 0x00037A58 File Offset: 0x00035C58
		// (set) Token: 0x0600257F RID: 9599 RVA: 0x00037A79 File Offset: 0x00035C79
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

		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x06002580 RID: 9600 RVA: 0x00037A8C File Offset: 0x00035C8C
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x06002581 RID: 9601 RVA: 0x00037AA4 File Offset: 0x00035CA4
		public void Set(ref VerifyUserAuthCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
		}

		// Token: 0x06002582 RID: 9602 RVA: 0x00037AC4 File Offset: 0x00035CC4
		public void Set(ref VerifyUserAuthCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
			}
		}

		// Token: 0x06002583 RID: 9603 RVA: 0x00037B08 File Offset: 0x00035D08
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
		}

		// Token: 0x06002584 RID: 9604 RVA: 0x00037B17 File Offset: 0x00035D17
		public void Get(out VerifyUserAuthCallbackInfo output)
		{
			output = default(VerifyUserAuthCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x0400106E RID: 4206
		private Result m_ResultCode;

		// Token: 0x0400106F RID: 4207
		private IntPtr m_ClientData;
	}
}
