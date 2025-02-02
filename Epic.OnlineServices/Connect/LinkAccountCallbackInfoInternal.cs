using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000537 RID: 1335
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LinkAccountCallbackInfoInternal : ICallbackInfoInternal, IGettable<LinkAccountCallbackInfo>, ISettable<LinkAccountCallbackInfo>, IDisposable
	{
		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x0600223D RID: 8765 RVA: 0x000332EC File Offset: 0x000314EC
		// (set) Token: 0x0600223E RID: 8766 RVA: 0x00033304 File Offset: 0x00031504
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

		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x0600223F RID: 8767 RVA: 0x00033310 File Offset: 0x00031510
		// (set) Token: 0x06002240 RID: 8768 RVA: 0x00033331 File Offset: 0x00031531
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

		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x06002241 RID: 8769 RVA: 0x00033344 File Offset: 0x00031544
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x06002242 RID: 8770 RVA: 0x0003335C File Offset: 0x0003155C
		// (set) Token: 0x06002243 RID: 8771 RVA: 0x0003337D File Offset: 0x0003157D
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.Get<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x06002244 RID: 8772 RVA: 0x0003338D File Offset: 0x0003158D
		public void Set(ref LinkAccountCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x06002245 RID: 8773 RVA: 0x000333B8 File Offset: 0x000315B8
		public void Set(ref LinkAccountCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x06002246 RID: 8774 RVA: 0x00033411 File Offset: 0x00031611
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x06002247 RID: 8775 RVA: 0x0003342C File Offset: 0x0003162C
		public void Get(out LinkAccountCallbackInfo output)
		{
			output = default(LinkAccountCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000F3C RID: 3900
		private Result m_ResultCode;

		// Token: 0x04000F3D RID: 3901
		private IntPtr m_ClientData;

		// Token: 0x04000F3E RID: 3902
		private IntPtr m_LocalUserId;
	}
}
