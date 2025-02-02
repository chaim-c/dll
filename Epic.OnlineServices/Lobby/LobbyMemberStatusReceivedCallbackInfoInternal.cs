using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000385 RID: 901
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyMemberStatusReceivedCallbackInfoInternal : ICallbackInfoInternal, IGettable<LobbyMemberStatusReceivedCallbackInfo>, ISettable<LobbyMemberStatusReceivedCallbackInfo>, IDisposable
	{
		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x0600180A RID: 6154 RVA: 0x00024778 File Offset: 0x00022978
		// (set) Token: 0x0600180B RID: 6155 RVA: 0x00024799 File Offset: 0x00022999
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

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x0600180C RID: 6156 RVA: 0x000247AC File Offset: 0x000229AC
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x0600180D RID: 6157 RVA: 0x000247C4 File Offset: 0x000229C4
		// (set) Token: 0x0600180E RID: 6158 RVA: 0x000247E5 File Offset: 0x000229E5
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

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x0600180F RID: 6159 RVA: 0x000247F8 File Offset: 0x000229F8
		// (set) Token: 0x06001810 RID: 6160 RVA: 0x00024819 File Offset: 0x00022A19
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

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x06001811 RID: 6161 RVA: 0x0002482C File Offset: 0x00022A2C
		// (set) Token: 0x06001812 RID: 6162 RVA: 0x00024844 File Offset: 0x00022A44
		public LobbyMemberStatus CurrentStatus
		{
			get
			{
				return this.m_CurrentStatus;
			}
			set
			{
				this.m_CurrentStatus = value;
			}
		}

		// Token: 0x06001813 RID: 6163 RVA: 0x0002484E File Offset: 0x00022A4E
		public void Set(ref LobbyMemberStatusReceivedCallbackInfo other)
		{
			this.ClientData = other.ClientData;
			this.LobbyId = other.LobbyId;
			this.TargetUserId = other.TargetUserId;
			this.CurrentStatus = other.CurrentStatus;
		}

		// Token: 0x06001814 RID: 6164 RVA: 0x00024888 File Offset: 0x00022A88
		public void Set(ref LobbyMemberStatusReceivedCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ClientData = other.Value.ClientData;
				this.LobbyId = other.Value.LobbyId;
				this.TargetUserId = other.Value.TargetUserId;
				this.CurrentStatus = other.Value.CurrentStatus;
			}
		}

		// Token: 0x06001815 RID: 6165 RVA: 0x000248F6 File Offset: 0x00022AF6
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LobbyId);
			Helper.Dispose(ref this.m_TargetUserId);
		}

		// Token: 0x06001816 RID: 6166 RVA: 0x0002491D File Offset: 0x00022B1D
		public void Get(out LobbyMemberStatusReceivedCallbackInfo output)
		{
			output = default(LobbyMemberStatusReceivedCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000AFC RID: 2812
		private IntPtr m_ClientData;

		// Token: 0x04000AFD RID: 2813
		private IntPtr m_LobbyId;

		// Token: 0x04000AFE RID: 2814
		private IntPtr m_TargetUserId;

		// Token: 0x04000AFF RID: 2815
		private LobbyMemberStatus m_CurrentStatus;
	}
}
