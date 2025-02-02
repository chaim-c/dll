using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x0200053B RID: 1339
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LoginCallbackInfoInternal : ICallbackInfoInternal, IGettable<LoginCallbackInfo>, ISettable<LoginCallbackInfo>, IDisposable
	{
		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x0600225B RID: 8795 RVA: 0x000335A4 File Offset: 0x000317A4
		// (set) Token: 0x0600225C RID: 8796 RVA: 0x000335BC File Offset: 0x000317BC
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

		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x0600225D RID: 8797 RVA: 0x000335C8 File Offset: 0x000317C8
		// (set) Token: 0x0600225E RID: 8798 RVA: 0x000335E9 File Offset: 0x000317E9
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

		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x0600225F RID: 8799 RVA: 0x000335FC File Offset: 0x000317FC
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x06002260 RID: 8800 RVA: 0x00033614 File Offset: 0x00031814
		// (set) Token: 0x06002261 RID: 8801 RVA: 0x00033635 File Offset: 0x00031835
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

		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x06002262 RID: 8802 RVA: 0x00033648 File Offset: 0x00031848
		// (set) Token: 0x06002263 RID: 8803 RVA: 0x00033669 File Offset: 0x00031869
		public ContinuanceToken ContinuanceToken
		{
			get
			{
				ContinuanceToken result;
				Helper.Get<ContinuanceToken>(this.m_ContinuanceToken, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_ContinuanceToken);
			}
		}

		// Token: 0x06002264 RID: 8804 RVA: 0x00033679 File Offset: 0x00031879
		public void Set(ref LoginCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.ContinuanceToken = other.ContinuanceToken;
		}

		// Token: 0x06002265 RID: 8805 RVA: 0x000336B0 File Offset: 0x000318B0
		public void Set(ref LoginCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.ContinuanceToken = other.Value.ContinuanceToken;
			}
		}

		// Token: 0x06002266 RID: 8806 RVA: 0x0003371E File Offset: 0x0003191E
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_ContinuanceToken);
		}

		// Token: 0x06002267 RID: 8807 RVA: 0x00033745 File Offset: 0x00031945
		public void Get(out LoginCallbackInfo output)
		{
			output = default(LoginCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000F48 RID: 3912
		private Result m_ResultCode;

		// Token: 0x04000F49 RID: 3913
		private IntPtr m_ClientData;

		// Token: 0x04000F4A RID: 3914
		private IntPtr m_LocalUserId;

		// Token: 0x04000F4B RID: 3915
		private IntPtr m_ContinuanceToken;
	}
}
