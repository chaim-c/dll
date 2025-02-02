using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAdmin
{
	// Token: 0x020001FD RID: 509
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct KickCompleteCallbackInfoInternal : ICallbackInfoInternal, IGettable<KickCompleteCallbackInfo>, ISettable<KickCompleteCallbackInfo>, IDisposable
	{
		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000E5B RID: 3675 RVA: 0x000155A0 File Offset: 0x000137A0
		// (set) Token: 0x06000E5C RID: 3676 RVA: 0x000155B8 File Offset: 0x000137B8
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

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000E5D RID: 3677 RVA: 0x000155C4 File Offset: 0x000137C4
		// (set) Token: 0x06000E5E RID: 3678 RVA: 0x000155E5 File Offset: 0x000137E5
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

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000E5F RID: 3679 RVA: 0x000155F8 File Offset: 0x000137F8
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x00015610 File Offset: 0x00013810
		public void Set(ref KickCompleteCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
		}

		// Token: 0x06000E61 RID: 3681 RVA: 0x00015630 File Offset: 0x00013830
		public void Set(ref KickCompleteCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
			}
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x00015674 File Offset: 0x00013874
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x00015683 File Offset: 0x00013883
		public void Get(out KickCompleteCallbackInfo output)
		{
			output = default(KickCompleteCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000677 RID: 1655
		private Result m_ResultCode;

		// Token: 0x04000678 RID: 1656
		private IntPtr m_ClientData;
	}
}
