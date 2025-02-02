using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000380 RID: 896
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyInviteReceivedCallbackInfoInternal : ICallbackInfoInternal, IGettable<LobbyInviteReceivedCallbackInfo>, ISettable<LobbyInviteReceivedCallbackInfo>, IDisposable
	{
		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x060017D8 RID: 6104 RVA: 0x00024210 File Offset: 0x00022410
		// (set) Token: 0x060017D9 RID: 6105 RVA: 0x00024231 File Offset: 0x00022431
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

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x060017DA RID: 6106 RVA: 0x00024244 File Offset: 0x00022444
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x060017DB RID: 6107 RVA: 0x0002425C File Offset: 0x0002245C
		// (set) Token: 0x060017DC RID: 6108 RVA: 0x0002427D File Offset: 0x0002247D
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

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x060017DD RID: 6109 RVA: 0x00024290 File Offset: 0x00022490
		// (set) Token: 0x060017DE RID: 6110 RVA: 0x000242B1 File Offset: 0x000224B1
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

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x060017DF RID: 6111 RVA: 0x000242C4 File Offset: 0x000224C4
		// (set) Token: 0x060017E0 RID: 6112 RVA: 0x000242E5 File Offset: 0x000224E5
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

		// Token: 0x060017E1 RID: 6113 RVA: 0x000242F5 File Offset: 0x000224F5
		public void Set(ref LobbyInviteReceivedCallbackInfo other)
		{
			this.ClientData = other.ClientData;
			this.InviteId = other.InviteId;
			this.LocalUserId = other.LocalUserId;
			this.TargetUserId = other.TargetUserId;
		}

		// Token: 0x060017E2 RID: 6114 RVA: 0x0002432C File Offset: 0x0002252C
		public void Set(ref LobbyInviteReceivedCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ClientData = other.Value.ClientData;
				this.InviteId = other.Value.InviteId;
				this.LocalUserId = other.Value.LocalUserId;
				this.TargetUserId = other.Value.TargetUserId;
			}
		}

		// Token: 0x060017E3 RID: 6115 RVA: 0x0002439A File Offset: 0x0002259A
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_InviteId);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_TargetUserId);
		}

		// Token: 0x060017E4 RID: 6116 RVA: 0x000243CD File Offset: 0x000225CD
		public void Get(out LobbyInviteReceivedCallbackInfo output)
		{
			output = default(LobbyInviteReceivedCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000AE3 RID: 2787
		private IntPtr m_ClientData;

		// Token: 0x04000AE4 RID: 2788
		private IntPtr m_InviteId;

		// Token: 0x04000AE5 RID: 2789
		private IntPtr m_LocalUserId;

		// Token: 0x04000AE6 RID: 2790
		private IntPtr m_TargetUserId;
	}
}
