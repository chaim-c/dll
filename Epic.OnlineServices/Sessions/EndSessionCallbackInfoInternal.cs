using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000E2 RID: 226
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct EndSessionCallbackInfoInternal : ICallbackInfoInternal, IGettable<EndSessionCallbackInfo>, ISettable<EndSessionCallbackInfo>, IDisposable
	{
		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000779 RID: 1913 RVA: 0x0000B4B4 File Offset: 0x000096B4
		// (set) Token: 0x0600077A RID: 1914 RVA: 0x0000B4CC File Offset: 0x000096CC
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

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x0600077B RID: 1915 RVA: 0x0000B4D8 File Offset: 0x000096D8
		// (set) Token: 0x0600077C RID: 1916 RVA: 0x0000B4F9 File Offset: 0x000096F9
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

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x0600077D RID: 1917 RVA: 0x0000B50C File Offset: 0x0000970C
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x0000B524 File Offset: 0x00009724
		public void Set(ref EndSessionCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x0000B544 File Offset: 0x00009744
		public void Set(ref EndSessionCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
			}
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x0000B588 File Offset: 0x00009788
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x0000B597 File Offset: 0x00009797
		public void Get(out EndSessionCallbackInfo output)
		{
			output = default(EndSessionCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000391 RID: 913
		private Result m_ResultCode;

		// Token: 0x04000392 RID: 914
		private IntPtr m_ClientData;
	}
}
