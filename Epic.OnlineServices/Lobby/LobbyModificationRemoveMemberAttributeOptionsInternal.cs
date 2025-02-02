using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000390 RID: 912
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyModificationRemoveMemberAttributeOptionsInternal : ISettable<LobbyModificationRemoveMemberAttributeOptions>, IDisposable
	{
		// Token: 0x170006F1 RID: 1777
		// (set) Token: 0x0600184F RID: 6223 RVA: 0x00024F26 File Offset: 0x00023126
		public Utf8String Key
		{
			set
			{
				Helper.Set(value, ref this.m_Key);
			}
		}

		// Token: 0x06001850 RID: 6224 RVA: 0x00024F36 File Offset: 0x00023136
		public void Set(ref LobbyModificationRemoveMemberAttributeOptions other)
		{
			this.m_ApiVersion = 1;
			this.Key = other.Key;
		}

		// Token: 0x06001851 RID: 6225 RVA: 0x00024F50 File Offset: 0x00023150
		public void Set(ref LobbyModificationRemoveMemberAttributeOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.Key = other.Value.Key;
			}
		}

		// Token: 0x06001852 RID: 6226 RVA: 0x00024F86 File Offset: 0x00023186
		public void Dispose()
		{
			Helper.Dispose(ref this.m_Key);
		}

		// Token: 0x04000B1E RID: 2846
		private int m_ApiVersion;

		// Token: 0x04000B1F RID: 2847
		private IntPtr m_Key;
	}
}
