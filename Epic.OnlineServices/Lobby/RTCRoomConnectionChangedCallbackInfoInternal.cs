using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003EA RID: 1002
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RTCRoomConnectionChangedCallbackInfoInternal : ICallbackInfoInternal, IGettable<RTCRoomConnectionChangedCallbackInfo>, ISettable<RTCRoomConnectionChangedCallbackInfo>, IDisposable
	{
		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x060019ED RID: 6637 RVA: 0x00026510 File Offset: 0x00024710
		// (set) Token: 0x060019EE RID: 6638 RVA: 0x00026531 File Offset: 0x00024731
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

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x060019EF RID: 6639 RVA: 0x00026544 File Offset: 0x00024744
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x060019F0 RID: 6640 RVA: 0x0002655C File Offset: 0x0002475C
		// (set) Token: 0x060019F1 RID: 6641 RVA: 0x0002657D File Offset: 0x0002477D
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

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x060019F2 RID: 6642 RVA: 0x00026590 File Offset: 0x00024790
		// (set) Token: 0x060019F3 RID: 6643 RVA: 0x000265B1 File Offset: 0x000247B1
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

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x060019F4 RID: 6644 RVA: 0x000265C4 File Offset: 0x000247C4
		// (set) Token: 0x060019F5 RID: 6645 RVA: 0x000265E5 File Offset: 0x000247E5
		public bool IsConnected
		{
			get
			{
				bool result;
				Helper.Get(this.m_IsConnected, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_IsConnected);
			}
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x060019F6 RID: 6646 RVA: 0x000265F8 File Offset: 0x000247F8
		// (set) Token: 0x060019F7 RID: 6647 RVA: 0x00026610 File Offset: 0x00024810
		public Result DisconnectReason
		{
			get
			{
				return this.m_DisconnectReason;
			}
			set
			{
				this.m_DisconnectReason = value;
			}
		}

		// Token: 0x060019F8 RID: 6648 RVA: 0x0002661C File Offset: 0x0002481C
		public void Set(ref RTCRoomConnectionChangedCallbackInfo other)
		{
			this.ClientData = other.ClientData;
			this.LobbyId = other.LobbyId;
			this.LocalUserId = other.LocalUserId;
			this.IsConnected = other.IsConnected;
			this.DisconnectReason = other.DisconnectReason;
		}

		// Token: 0x060019F9 RID: 6649 RVA: 0x0002666C File Offset: 0x0002486C
		public void Set(ref RTCRoomConnectionChangedCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ClientData = other.Value.ClientData;
				this.LobbyId = other.Value.LobbyId;
				this.LocalUserId = other.Value.LocalUserId;
				this.IsConnected = other.Value.IsConnected;
				this.DisconnectReason = other.Value.DisconnectReason;
			}
		}

		// Token: 0x060019FA RID: 6650 RVA: 0x000266EF File Offset: 0x000248EF
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LobbyId);
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x060019FB RID: 6651 RVA: 0x00026716 File Offset: 0x00024916
		public void Get(out RTCRoomConnectionChangedCallbackInfo output)
		{
			output = default(RTCRoomConnectionChangedCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000B89 RID: 2953
		private IntPtr m_ClientData;

		// Token: 0x04000B8A RID: 2954
		private IntPtr m_LobbyId;

		// Token: 0x04000B8B RID: 2955
		private IntPtr m_LocalUserId;

		// Token: 0x04000B8C RID: 2956
		private int m_IsConnected;

		// Token: 0x04000B8D RID: 2957
		private Result m_DisconnectReason;
	}
}
