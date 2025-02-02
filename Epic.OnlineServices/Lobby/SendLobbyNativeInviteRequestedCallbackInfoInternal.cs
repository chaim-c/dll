using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003F0 RID: 1008
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SendLobbyNativeInviteRequestedCallbackInfoInternal : ICallbackInfoInternal, IGettable<SendLobbyNativeInviteRequestedCallbackInfo>, ISettable<SendLobbyNativeInviteRequestedCallbackInfo>, IDisposable
	{
		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x06001A29 RID: 6697 RVA: 0x00026AF4 File Offset: 0x00024CF4
		// (set) Token: 0x06001A2A RID: 6698 RVA: 0x00026B15 File Offset: 0x00024D15
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

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x06001A2B RID: 6699 RVA: 0x00026B28 File Offset: 0x00024D28
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x06001A2C RID: 6700 RVA: 0x00026B40 File Offset: 0x00024D40
		// (set) Token: 0x06001A2D RID: 6701 RVA: 0x00026B58 File Offset: 0x00024D58
		public ulong UiEventId
		{
			get
			{
				return this.m_UiEventId;
			}
			set
			{
				this.m_UiEventId = value;
			}
		}

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x06001A2E RID: 6702 RVA: 0x00026B64 File Offset: 0x00024D64
		// (set) Token: 0x06001A2F RID: 6703 RVA: 0x00026B85 File Offset: 0x00024D85
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

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x06001A30 RID: 6704 RVA: 0x00026B98 File Offset: 0x00024D98
		// (set) Token: 0x06001A31 RID: 6705 RVA: 0x00026BB9 File Offset: 0x00024DB9
		public Utf8String TargetNativeAccountType
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_TargetNativeAccountType, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_TargetNativeAccountType);
			}
		}

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x06001A32 RID: 6706 RVA: 0x00026BCC File Offset: 0x00024DCC
		// (set) Token: 0x06001A33 RID: 6707 RVA: 0x00026BED File Offset: 0x00024DED
		public Utf8String TargetUserNativeAccountId
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_TargetUserNativeAccountId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_TargetUserNativeAccountId);
			}
		}

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x06001A34 RID: 6708 RVA: 0x00026C00 File Offset: 0x00024E00
		// (set) Token: 0x06001A35 RID: 6709 RVA: 0x00026C21 File Offset: 0x00024E21
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

		// Token: 0x06001A36 RID: 6710 RVA: 0x00026C34 File Offset: 0x00024E34
		public void Set(ref SendLobbyNativeInviteRequestedCallbackInfo other)
		{
			this.ClientData = other.ClientData;
			this.UiEventId = other.UiEventId;
			this.LocalUserId = other.LocalUserId;
			this.TargetNativeAccountType = other.TargetNativeAccountType;
			this.TargetUserNativeAccountId = other.TargetUserNativeAccountId;
			this.LobbyId = other.LobbyId;
		}

		// Token: 0x06001A37 RID: 6711 RVA: 0x00026C90 File Offset: 0x00024E90
		public void Set(ref SendLobbyNativeInviteRequestedCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ClientData = other.Value.ClientData;
				this.UiEventId = other.Value.UiEventId;
				this.LocalUserId = other.Value.LocalUserId;
				this.TargetNativeAccountType = other.Value.TargetNativeAccountType;
				this.TargetUserNativeAccountId = other.Value.TargetUserNativeAccountId;
				this.LobbyId = other.Value.LobbyId;
			}
		}

		// Token: 0x06001A38 RID: 6712 RVA: 0x00026D2B File Offset: 0x00024F2B
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_TargetNativeAccountType);
			Helper.Dispose(ref this.m_TargetUserNativeAccountId);
			Helper.Dispose(ref this.m_LobbyId);
		}

		// Token: 0x06001A39 RID: 6713 RVA: 0x00026D6A File Offset: 0x00024F6A
		public void Get(out SendLobbyNativeInviteRequestedCallbackInfo output)
		{
			output = default(SendLobbyNativeInviteRequestedCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000BA1 RID: 2977
		private IntPtr m_ClientData;

		// Token: 0x04000BA2 RID: 2978
		private ulong m_UiEventId;

		// Token: 0x04000BA3 RID: 2979
		private IntPtr m_LocalUserId;

		// Token: 0x04000BA4 RID: 2980
		private IntPtr m_TargetNativeAccountType;

		// Token: 0x04000BA5 RID: 2981
		private IntPtr m_TargetUserNativeAccountId;

		// Token: 0x04000BA6 RID: 2982
		private IntPtr m_LobbyId;
	}
}
