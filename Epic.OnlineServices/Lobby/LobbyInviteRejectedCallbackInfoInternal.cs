using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000382 RID: 898
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyInviteRejectedCallbackInfoInternal : ICallbackInfoInternal, IGettable<LobbyInviteRejectedCallbackInfo>, ISettable<LobbyInviteRejectedCallbackInfo>, IDisposable
	{
		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x060017F1 RID: 6129 RVA: 0x000244A0 File Offset: 0x000226A0
		// (set) Token: 0x060017F2 RID: 6130 RVA: 0x000244C1 File Offset: 0x000226C1
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

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x060017F3 RID: 6131 RVA: 0x000244D4 File Offset: 0x000226D4
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x060017F4 RID: 6132 RVA: 0x000244EC File Offset: 0x000226EC
		// (set) Token: 0x060017F5 RID: 6133 RVA: 0x0002450D File Offset: 0x0002270D
		public Utf8String InviteId
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_InviteId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_InviteId);
			}
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x060017F6 RID: 6134 RVA: 0x00024520 File Offset: 0x00022720
		// (set) Token: 0x060017F7 RID: 6135 RVA: 0x00024541 File Offset: 0x00022741
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

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x060017F8 RID: 6136 RVA: 0x00024554 File Offset: 0x00022754
		// (set) Token: 0x060017F9 RID: 6137 RVA: 0x00024575 File Offset: 0x00022775
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

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x060017FA RID: 6138 RVA: 0x00024588 File Offset: 0x00022788
		// (set) Token: 0x060017FB RID: 6139 RVA: 0x000245A9 File Offset: 0x000227A9
		public Utf8String LobbyId
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_LobbyId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_LobbyId);
			}
		}

		// Token: 0x060017FC RID: 6140 RVA: 0x000245BC File Offset: 0x000227BC
		public void Set(ref LobbyInviteRejectedCallbackInfo other)
		{
			this.ClientData = other.ClientData;
			this.InviteId = other.InviteId;
			this.LocalUserId = other.LocalUserId;
			this.TargetUserId = other.TargetUserId;
			this.LobbyId = other.LobbyId;
		}

		// Token: 0x060017FD RID: 6141 RVA: 0x0002460C File Offset: 0x0002280C
		public void Set(ref LobbyInviteRejectedCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ClientData = other.Value.ClientData;
				this.InviteId = other.Value.InviteId;
				this.LocalUserId = other.Value.LocalUserId;
				this.TargetUserId = other.Value.TargetUserId;
				this.LobbyId = other.Value.LobbyId;
			}
		}

		// Token: 0x060017FE RID: 6142 RVA: 0x0002468F File Offset: 0x0002288F
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_InviteId);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_TargetUserId);
			Helper.Dispose(ref this.m_LobbyId);
		}

		// Token: 0x060017FF RID: 6143 RVA: 0x000246CE File Offset: 0x000228CE
		public void Get(out LobbyInviteRejectedCallbackInfo output)
		{
			output = default(LobbyInviteRejectedCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000AEC RID: 2796
		private IntPtr m_ClientData;

		// Token: 0x04000AED RID: 2797
		private IntPtr m_InviteId;

		// Token: 0x04000AEE RID: 2798
		private IntPtr m_LocalUserId;

		// Token: 0x04000AEF RID: 2799
		private IntPtr m_TargetUserId;

		// Token: 0x04000AF0 RID: 2800
		private IntPtr m_LobbyId;
	}
}
