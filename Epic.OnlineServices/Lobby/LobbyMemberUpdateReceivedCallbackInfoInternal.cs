using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000387 RID: 903
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyMemberUpdateReceivedCallbackInfoInternal : ICallbackInfoInternal, IGettable<LobbyMemberUpdateReceivedCallbackInfo>, ISettable<LobbyMemberUpdateReceivedCallbackInfo>, IDisposable
	{
		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x0600181F RID: 6175 RVA: 0x000249AC File Offset: 0x00022BAC
		// (set) Token: 0x06001820 RID: 6176 RVA: 0x000249CD File Offset: 0x00022BCD
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

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x06001821 RID: 6177 RVA: 0x000249E0 File Offset: 0x00022BE0
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x06001822 RID: 6178 RVA: 0x000249F8 File Offset: 0x00022BF8
		// (set) Token: 0x06001823 RID: 6179 RVA: 0x00024A19 File Offset: 0x00022C19
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

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x06001824 RID: 6180 RVA: 0x00024A2C File Offset: 0x00022C2C
		// (set) Token: 0x06001825 RID: 6181 RVA: 0x00024A4D File Offset: 0x00022C4D
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

		// Token: 0x06001826 RID: 6182 RVA: 0x00024A5D File Offset: 0x00022C5D
		public void Set(ref LobbyMemberUpdateReceivedCallbackInfo other)
		{
			this.ClientData = other.ClientData;
			this.LobbyId = other.LobbyId;
			this.TargetUserId = other.TargetUserId;
		}

		// Token: 0x06001827 RID: 6183 RVA: 0x00024A88 File Offset: 0x00022C88
		public void Set(ref LobbyMemberUpdateReceivedCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ClientData = other.Value.ClientData;
				this.LobbyId = other.Value.LobbyId;
				this.TargetUserId = other.Value.TargetUserId;
			}
		}

		// Token: 0x06001828 RID: 6184 RVA: 0x00024AE1 File Offset: 0x00022CE1
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LobbyId);
			Helper.Dispose(ref this.m_TargetUserId);
		}

		// Token: 0x06001829 RID: 6185 RVA: 0x00024B08 File Offset: 0x00022D08
		public void Get(out LobbyMemberUpdateReceivedCallbackInfo output)
		{
			output = default(LobbyMemberUpdateReceivedCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000B03 RID: 2819
		private IntPtr m_ClientData;

		// Token: 0x04000B04 RID: 2820
		private IntPtr m_LobbyId;

		// Token: 0x04000B05 RID: 2821
		private IntPtr m_TargetUserId;
	}
}
