using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x0200057C RID: 1404
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DeletePersistentAuthCallbackInfoInternal : ICallbackInfoInternal, IGettable<DeletePersistentAuthCallbackInfo>, ISettable<DeletePersistentAuthCallbackInfo>, IDisposable
	{
		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x060023F4 RID: 9204 RVA: 0x0003583C File Offset: 0x00033A3C
		// (set) Token: 0x060023F5 RID: 9205 RVA: 0x00035854 File Offset: 0x00033A54
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

		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x060023F6 RID: 9206 RVA: 0x00035860 File Offset: 0x00033A60
		// (set) Token: 0x060023F7 RID: 9207 RVA: 0x00035881 File Offset: 0x00033A81
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

		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x060023F8 RID: 9208 RVA: 0x00035894 File Offset: 0x00033A94
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x060023F9 RID: 9209 RVA: 0x000358AC File Offset: 0x00033AAC
		public void Set(ref DeletePersistentAuthCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
		}

		// Token: 0x060023FA RID: 9210 RVA: 0x000358CC File Offset: 0x00033ACC
		public void Set(ref DeletePersistentAuthCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
			}
		}

		// Token: 0x060023FB RID: 9211 RVA: 0x00035910 File Offset: 0x00033B10
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
		}

		// Token: 0x060023FC RID: 9212 RVA: 0x0003591F File Offset: 0x00033B1F
		public void Get(out DeletePersistentAuthCallbackInfo output)
		{
			output = default(DeletePersistentAuthCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000FD9 RID: 4057
		private Result m_ResultCode;

		// Token: 0x04000FDA RID: 4058
		private IntPtr m_ClientData;
	}
}
