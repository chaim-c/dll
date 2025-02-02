using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002D4 RID: 724
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OnQueryNATTypeCompleteInfoInternal : ICallbackInfoInternal, IGettable<OnQueryNATTypeCompleteInfo>, ISettable<OnQueryNATTypeCompleteInfo>, IDisposable
	{
		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x0600136E RID: 4974 RVA: 0x0001C924 File Offset: 0x0001AB24
		// (set) Token: 0x0600136F RID: 4975 RVA: 0x0001C93C File Offset: 0x0001AB3C
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

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06001370 RID: 4976 RVA: 0x0001C948 File Offset: 0x0001AB48
		// (set) Token: 0x06001371 RID: 4977 RVA: 0x0001C969 File Offset: 0x0001AB69
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

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06001372 RID: 4978 RVA: 0x0001C97C File Offset: 0x0001AB7C
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06001373 RID: 4979 RVA: 0x0001C994 File Offset: 0x0001AB94
		// (set) Token: 0x06001374 RID: 4980 RVA: 0x0001C9AC File Offset: 0x0001ABAC
		public NATType NATType
		{
			get
			{
				return this.m_NATType;
			}
			set
			{
				this.m_NATType = value;
			}
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x0001C9B6 File Offset: 0x0001ABB6
		public void Set(ref OnQueryNATTypeCompleteInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.NATType = other.NATType;
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x0001C9E0 File Offset: 0x0001ABE0
		public void Set(ref OnQueryNATTypeCompleteInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.NATType = other.Value.NATType;
			}
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x0001CA39 File Offset: 0x0001AC39
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x0001CA48 File Offset: 0x0001AC48
		public void Get(out OnQueryNATTypeCompleteInfo output)
		{
			output = default(OnQueryNATTypeCompleteInfo);
			output.Set(ref this);
		}

		// Token: 0x040008A6 RID: 2214
		private Result m_ResultCode;

		// Token: 0x040008A7 RID: 2215
		private IntPtr m_ClientData;

		// Token: 0x040008A8 RID: 2216
		private NATType m_NATType;
	}
}
