using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200036F RID: 879
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyDetailsCopyMemberAttributeByKeyOptionsInternal : ISettable<LobbyDetailsCopyMemberAttributeByKeyOptions>, IDisposable
	{
		// Token: 0x1700069B RID: 1691
		// (set) Token: 0x06001729 RID: 5929 RVA: 0x000227D4 File Offset: 0x000209D4
		public ProductUserId TargetUserId
		{
			set
			{
				Helper.Set(value, ref this.m_TargetUserId);
			}
		}

		// Token: 0x1700069C RID: 1692
		// (set) Token: 0x0600172A RID: 5930 RVA: 0x000227E4 File Offset: 0x000209E4
		public Utf8String AttrKey
		{
			set
			{
				Helper.Set(value, ref this.m_AttrKey);
			}
		}

		// Token: 0x0600172B RID: 5931 RVA: 0x000227F4 File Offset: 0x000209F4
		public void Set(ref LobbyDetailsCopyMemberAttributeByKeyOptions other)
		{
			this.m_ApiVersion = 1;
			this.TargetUserId = other.TargetUserId;
			this.AttrKey = other.AttrKey;
		}

		// Token: 0x0600172C RID: 5932 RVA: 0x00022818 File Offset: 0x00020A18
		public void Set(ref LobbyDetailsCopyMemberAttributeByKeyOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.TargetUserId = other.Value.TargetUserId;
				this.AttrKey = other.Value.AttrKey;
			}
		}

		// Token: 0x0600172D RID: 5933 RVA: 0x00022863 File Offset: 0x00020A63
		public void Dispose()
		{
			Helper.Dispose(ref this.m_TargetUserId);
			Helper.Dispose(ref this.m_AttrKey);
		}

		// Token: 0x04000A88 RID: 2696
		private int m_ApiVersion;

		// Token: 0x04000A89 RID: 2697
		private IntPtr m_TargetUserId;

		// Token: 0x04000A8A RID: 2698
		private IntPtr m_AttrKey;
	}
}
