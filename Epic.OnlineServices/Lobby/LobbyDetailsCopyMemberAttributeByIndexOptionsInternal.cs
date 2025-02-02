using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200036D RID: 877
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyDetailsCopyMemberAttributeByIndexOptionsInternal : ISettable<LobbyDetailsCopyMemberAttributeByIndexOptions>, IDisposable
	{
		// Token: 0x17000697 RID: 1687
		// (set) Token: 0x06001720 RID: 5920 RVA: 0x0002271A File Offset: 0x0002091A
		public ProductUserId TargetUserId
		{
			set
			{
				Helper.Set(value, ref this.m_TargetUserId);
			}
		}

		// Token: 0x17000698 RID: 1688
		// (set) Token: 0x06001721 RID: 5921 RVA: 0x0002272A File Offset: 0x0002092A
		public uint AttrIndex
		{
			set
			{
				this.m_AttrIndex = value;
			}
		}

		// Token: 0x06001722 RID: 5922 RVA: 0x00022734 File Offset: 0x00020934
		public void Set(ref LobbyDetailsCopyMemberAttributeByIndexOptions other)
		{
			this.m_ApiVersion = 1;
			this.TargetUserId = other.TargetUserId;
			this.AttrIndex = other.AttrIndex;
		}

		// Token: 0x06001723 RID: 5923 RVA: 0x00022758 File Offset: 0x00020958
		public void Set(ref LobbyDetailsCopyMemberAttributeByIndexOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.TargetUserId = other.Value.TargetUserId;
				this.AttrIndex = other.Value.AttrIndex;
			}
		}

		// Token: 0x06001724 RID: 5924 RVA: 0x000227A3 File Offset: 0x000209A3
		public void Dispose()
		{
			Helper.Dispose(ref this.m_TargetUserId);
		}

		// Token: 0x04000A83 RID: 2691
		private int m_ApiVersion;

		// Token: 0x04000A84 RID: 2692
		private IntPtr m_TargetUserId;

		// Token: 0x04000A85 RID: 2693
		private uint m_AttrIndex;
	}
}
