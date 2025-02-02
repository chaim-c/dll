using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200037E RID: 894
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyInviteAcceptedCallbackInfoInternal : ICallbackInfoInternal, IGettable<LobbyInviteAcceptedCallbackInfo>, ISettable<LobbyInviteAcceptedCallbackInfo>, IDisposable
	{
		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x060017BF RID: 6079 RVA: 0x00023F38 File Offset: 0x00022138
		// (set) Token: 0x060017C0 RID: 6080 RVA: 0x00023F59 File Offset: 0x00022159
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

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x060017C1 RID: 6081 RVA: 0x00023F6C File Offset: 0x0002216C
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x060017C2 RID: 6082 RVA: 0x00023F84 File Offset: 0x00022184
		// (set) Token: 0x060017C3 RID: 6083 RVA: 0x00023FA5 File Offset: 0x000221A5
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

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x060017C4 RID: 6084 RVA: 0x00023FB8 File Offset: 0x000221B8
		// (set) Token: 0x060017C5 RID: 6085 RVA: 0x00023FD9 File Offset: 0x000221D9
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

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x060017C6 RID: 6086 RVA: 0x00023FEC File Offset: 0x000221EC
		// (set) Token: 0x060017C7 RID: 6087 RVA: 0x0002400D File Offset: 0x0002220D
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

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x060017C8 RID: 6088 RVA: 0x00024020 File Offset: 0x00022220
		// (set) Token: 0x060017C9 RID: 6089 RVA: 0x00024041 File Offset: 0x00022241
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

		// Token: 0x060017CA RID: 6090 RVA: 0x00024054 File Offset: 0x00022254
		public void Set(ref LobbyInviteAcceptedCallbackInfo other)
		{
			this.ClientData = other.ClientData;
			this.InviteId = other.InviteId;
			this.LocalUserId = other.LocalUserId;
			this.TargetUserId = other.TargetUserId;
			this.LobbyId = other.LobbyId;
		}

		// Token: 0x060017CB RID: 6091 RVA: 0x000240A4 File Offset: 0x000222A4
		public void Set(ref LobbyInviteAcceptedCallbackInfo? other)
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

		// Token: 0x060017CC RID: 6092 RVA: 0x00024127 File Offset: 0x00022327
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_InviteId);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_TargetUserId);
			Helper.Dispose(ref this.m_LobbyId);
		}

		// Token: 0x060017CD RID: 6093 RVA: 0x00024166 File Offset: 0x00022366
		public void Get(out LobbyInviteAcceptedCallbackInfo output)
		{
			output = default(LobbyInviteAcceptedCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000ADA RID: 2778
		private IntPtr m_ClientData;

		// Token: 0x04000ADB RID: 2779
		private IntPtr m_InviteId;

		// Token: 0x04000ADC RID: 2780
		private IntPtr m_LocalUserId;

		// Token: 0x04000ADD RID: 2781
		private IntPtr m_TargetUserId;

		// Token: 0x04000ADE RID: 2782
		private IntPtr m_LobbyId;
	}
}
