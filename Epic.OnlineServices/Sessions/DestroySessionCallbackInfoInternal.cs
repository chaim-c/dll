using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000DC RID: 220
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DestroySessionCallbackInfoInternal : ICallbackInfoInternal, IGettable<DestroySessionCallbackInfo>, ISettable<DestroySessionCallbackInfo>, IDisposable
	{
		// Token: 0x1700017B RID: 379
		// (get) Token: 0x0600075E RID: 1886 RVA: 0x0000B260 File Offset: 0x00009460
		// (set) Token: 0x0600075F RID: 1887 RVA: 0x0000B278 File Offset: 0x00009478
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

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000760 RID: 1888 RVA: 0x0000B284 File Offset: 0x00009484
		// (set) Token: 0x06000761 RID: 1889 RVA: 0x0000B2A5 File Offset: 0x000094A5
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

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000762 RID: 1890 RVA: 0x0000B2B8 File Offset: 0x000094B8
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x0000B2D0 File Offset: 0x000094D0
		public void Set(ref DestroySessionCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x0000B2F0 File Offset: 0x000094F0
		public void Set(ref DestroySessionCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
			}
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x0000B334 File Offset: 0x00009534
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x0000B343 File Offset: 0x00009543
		public void Get(out DestroySessionCallbackInfo output)
		{
			output = default(DestroySessionCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000387 RID: 903
		private Result m_ResultCode;

		// Token: 0x04000388 RID: 904
		private IntPtr m_ClientData;
	}
}
