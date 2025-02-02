using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000EE RID: 238
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct JoinSessionCallbackInfoInternal : ICallbackInfoInternal, IGettable<JoinSessionCallbackInfo>, ISettable<JoinSessionCallbackInfo>, IDisposable
	{
		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060007B9 RID: 1977 RVA: 0x0000BA60 File Offset: 0x00009C60
		// (set) Token: 0x060007BA RID: 1978 RVA: 0x0000BA78 File Offset: 0x00009C78
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

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060007BB RID: 1979 RVA: 0x0000BA84 File Offset: 0x00009C84
		// (set) Token: 0x060007BC RID: 1980 RVA: 0x0000BAA5 File Offset: 0x00009CA5
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

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060007BD RID: 1981 RVA: 0x0000BAB8 File Offset: 0x00009CB8
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x0000BAD0 File Offset: 0x00009CD0
		public void Set(ref JoinSessionCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x0000BAF0 File Offset: 0x00009CF0
		public void Set(ref JoinSessionCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
			}
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x0000BB34 File Offset: 0x00009D34
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x0000BB43 File Offset: 0x00009D43
		public void Get(out JoinSessionCallbackInfo output)
		{
			output = default(JoinSessionCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x040003AB RID: 939
		private Result m_ResultCode;

		// Token: 0x040003AC RID: 940
		private IntPtr m_ClientData;
	}
}
