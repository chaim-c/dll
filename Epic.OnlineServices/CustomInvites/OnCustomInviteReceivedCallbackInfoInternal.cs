using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.CustomInvites
{
	// Token: 0x02000502 RID: 1282
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OnCustomInviteReceivedCallbackInfoInternal : ICallbackInfoInternal, IGettable<OnCustomInviteReceivedCallbackInfo>, ISettable<OnCustomInviteReceivedCallbackInfo>, IDisposable
	{
		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x060020F9 RID: 8441 RVA: 0x0003100C File Offset: 0x0002F20C
		// (set) Token: 0x060020FA RID: 8442 RVA: 0x0003102D File Offset: 0x0002F22D
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

		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x060020FB RID: 8443 RVA: 0x00031040 File Offset: 0x0002F240
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x060020FC RID: 8444 RVA: 0x00031058 File Offset: 0x0002F258
		// (set) Token: 0x060020FD RID: 8445 RVA: 0x00031079 File Offset: 0x0002F279
		public ProductUserId TargetUserId
		{
			get
			{
				ProductUserId result;
				Helper.Get<ProductUserId>(this.m_TargetUserId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_TargetUserId);
			}
		}

		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x060020FE RID: 8446 RVA: 0x0003108C File Offset: 0x0002F28C
		// (set) Token: 0x060020FF RID: 8447 RVA: 0x000310AD File Offset: 0x0002F2AD
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

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x06002100 RID: 8448 RVA: 0x000310C0 File Offset: 0x0002F2C0
		// (set) Token: 0x06002101 RID: 8449 RVA: 0x000310E1 File Offset: 0x0002F2E1
		public Utf8String CustomInviteId
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_CustomInviteId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_CustomInviteId);
			}
		}

		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x06002102 RID: 8450 RVA: 0x000310F4 File Offset: 0x0002F2F4
		// (set) Token: 0x06002103 RID: 8451 RVA: 0x00031115 File Offset: 0x0002F315
		public Utf8String Payload
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_Payload, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_Payload);
			}
		}

		// Token: 0x06002104 RID: 8452 RVA: 0x00031128 File Offset: 0x0002F328
		public void Set(ref OnCustomInviteReceivedCallbackInfo other)
		{
			this.ClientData = other.ClientData;
			this.TargetUserId = other.TargetUserId;
			this.LocalUserId = other.LocalUserId;
			this.CustomInviteId = other.CustomInviteId;
			this.Payload = other.Payload;
		}

		// Token: 0x06002105 RID: 8453 RVA: 0x00031178 File Offset: 0x0002F378
		public void Set(ref OnCustomInviteReceivedCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ClientData = other.Value.ClientData;
				this.TargetUserId = other.Value.TargetUserId;
				this.LocalUserId = other.Value.LocalUserId;
				this.CustomInviteId = other.Value.CustomInviteId;
				this.Payload = other.Value.Payload;
			}
		}

		// Token: 0x06002106 RID: 8454 RVA: 0x000311FB File Offset: 0x0002F3FB
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_TargetUserId);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_CustomInviteId);
			Helper.Dispose(ref this.m_Payload);
		}

		// Token: 0x06002107 RID: 8455 RVA: 0x0003123A File Offset: 0x0002F43A
		public void Get(out OnCustomInviteReceivedCallbackInfo output)
		{
			output = default(OnCustomInviteReceivedCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000EAB RID: 3755
		private IntPtr m_ClientData;

		// Token: 0x04000EAC RID: 3756
		private IntPtr m_TargetUserId;

		// Token: 0x04000EAD RID: 3757
		private IntPtr m_LocalUserId;

		// Token: 0x04000EAE RID: 3758
		private IntPtr m_CustomInviteId;

		// Token: 0x04000EAF RID: 3759
		private IntPtr m_Payload;
	}
}
