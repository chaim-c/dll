using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.CustomInvites
{
	// Token: 0x020004FE RID: 1278
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OnCustomInviteAcceptedCallbackInfoInternal : ICallbackInfoInternal, IGettable<OnCustomInviteAcceptedCallbackInfo>, ISettable<OnCustomInviteAcceptedCallbackInfo>, IDisposable
	{
		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x060020D6 RID: 8406 RVA: 0x00030D08 File Offset: 0x0002EF08
		// (set) Token: 0x060020D7 RID: 8407 RVA: 0x00030D29 File Offset: 0x0002EF29
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

		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x060020D8 RID: 8408 RVA: 0x00030D3C File Offset: 0x0002EF3C
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x060020D9 RID: 8409 RVA: 0x00030D54 File Offset: 0x0002EF54
		// (set) Token: 0x060020DA RID: 8410 RVA: 0x00030D75 File Offset: 0x0002EF75
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

		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x060020DB RID: 8411 RVA: 0x00030D88 File Offset: 0x0002EF88
		// (set) Token: 0x060020DC RID: 8412 RVA: 0x00030DA9 File Offset: 0x0002EFA9
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

		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x060020DD RID: 8413 RVA: 0x00030DBC File Offset: 0x0002EFBC
		// (set) Token: 0x060020DE RID: 8414 RVA: 0x00030DDD File Offset: 0x0002EFDD
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

		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x060020DF RID: 8415 RVA: 0x00030DF0 File Offset: 0x0002EFF0
		// (set) Token: 0x060020E0 RID: 8416 RVA: 0x00030E11 File Offset: 0x0002F011
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

		// Token: 0x060020E1 RID: 8417 RVA: 0x00030E24 File Offset: 0x0002F024
		public void Set(ref OnCustomInviteAcceptedCallbackInfo other)
		{
			this.ClientData = other.ClientData;
			this.TargetUserId = other.TargetUserId;
			this.LocalUserId = other.LocalUserId;
			this.CustomInviteId = other.CustomInviteId;
			this.Payload = other.Payload;
		}

		// Token: 0x060020E2 RID: 8418 RVA: 0x00030E74 File Offset: 0x0002F074
		public void Set(ref OnCustomInviteAcceptedCallbackInfo? other)
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

		// Token: 0x060020E3 RID: 8419 RVA: 0x00030EF7 File Offset: 0x0002F0F7
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_TargetUserId);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_CustomInviteId);
			Helper.Dispose(ref this.m_Payload);
		}

		// Token: 0x060020E4 RID: 8420 RVA: 0x00030F36 File Offset: 0x0002F136
		public void Get(out OnCustomInviteAcceptedCallbackInfo output)
		{
			output = default(OnCustomInviteAcceptedCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000EA1 RID: 3745
		private IntPtr m_ClientData;

		// Token: 0x04000EA2 RID: 3746
		private IntPtr m_TargetUserId;

		// Token: 0x04000EA3 RID: 3747
		private IntPtr m_LocalUserId;

		// Token: 0x04000EA4 RID: 3748
		private IntPtr m_CustomInviteId;

		// Token: 0x04000EA5 RID: 3749
		private IntPtr m_Payload;
	}
}
