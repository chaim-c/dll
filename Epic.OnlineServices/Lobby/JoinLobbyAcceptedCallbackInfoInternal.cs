using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000353 RID: 851
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct JoinLobbyAcceptedCallbackInfoInternal : ICallbackInfoInternal, IGettable<JoinLobbyAcceptedCallbackInfo>, ISettable<JoinLobbyAcceptedCallbackInfo>, IDisposable
	{
		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06001676 RID: 5750 RVA: 0x00021564 File Offset: 0x0001F764
		// (set) Token: 0x06001677 RID: 5751 RVA: 0x00021585 File Offset: 0x0001F785
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

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06001678 RID: 5752 RVA: 0x00021598 File Offset: 0x0001F798
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06001679 RID: 5753 RVA: 0x000215B0 File Offset: 0x0001F7B0
		// (set) Token: 0x0600167A RID: 5754 RVA: 0x000215D1 File Offset: 0x0001F7D1
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

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x0600167B RID: 5755 RVA: 0x000215E4 File Offset: 0x0001F7E4
		// (set) Token: 0x0600167C RID: 5756 RVA: 0x000215FC File Offset: 0x0001F7FC
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

		// Token: 0x0600167D RID: 5757 RVA: 0x00021606 File Offset: 0x0001F806
		public void Set(ref JoinLobbyAcceptedCallbackInfo other)
		{
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.UiEventId = other.UiEventId;
		}

		// Token: 0x0600167E RID: 5758 RVA: 0x00021630 File Offset: 0x0001F830
		public void Set(ref JoinLobbyAcceptedCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.UiEventId = other.Value.UiEventId;
			}
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x00021689 File Offset: 0x0001F889
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x06001680 RID: 5760 RVA: 0x000216A4 File Offset: 0x0001F8A4
		public void Get(out JoinLobbyAcceptedCallbackInfo output)
		{
			output = default(JoinLobbyAcceptedCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000A33 RID: 2611
		private IntPtr m_ClientData;

		// Token: 0x04000A34 RID: 2612
		private IntPtr m_LocalUserId;

		// Token: 0x04000A35 RID: 2613
		private ulong m_UiEventId;
	}
}
