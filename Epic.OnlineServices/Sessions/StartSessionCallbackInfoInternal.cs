using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000159 RID: 345
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct StartSessionCallbackInfoInternal : ICallbackInfoInternal, IGettable<StartSessionCallbackInfo>, ISettable<StartSessionCallbackInfo>, IDisposable
	{
		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060009F4 RID: 2548 RVA: 0x0000EDF8 File Offset: 0x0000CFF8
		// (set) Token: 0x060009F5 RID: 2549 RVA: 0x0000EE10 File Offset: 0x0000D010
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

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060009F6 RID: 2550 RVA: 0x0000EE1C File Offset: 0x0000D01C
		// (set) Token: 0x060009F7 RID: 2551 RVA: 0x0000EE3D File Offset: 0x0000D03D
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

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060009F8 RID: 2552 RVA: 0x0000EE50 File Offset: 0x0000D050
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x0000EE68 File Offset: 0x0000D068
		public void Set(ref StartSessionCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x0000EE88 File Offset: 0x0000D088
		public void Set(ref StartSessionCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
			}
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x0000EECC File Offset: 0x0000D0CC
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x0000EEDB File Offset: 0x0000D0DB
		public void Get(out StartSessionCallbackInfo output)
		{
			output = default(StartSessionCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x0400049F RID: 1183
		private Result m_ResultCode;

		// Token: 0x040004A0 RID: 1184
		private IntPtr m_ClientData;
	}
}
