using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200038E RID: 910
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyModificationRemoveAttributeOptionsInternal : ISettable<LobbyModificationRemoveAttributeOptions>, IDisposable
	{
		// Token: 0x170006EF RID: 1775
		// (set) Token: 0x06001849 RID: 6217 RVA: 0x00024EA7 File Offset: 0x000230A7
		public Utf8String Key
		{
			set
			{
				Helper.Set(value, ref this.m_Key);
			}
		}

		// Token: 0x0600184A RID: 6218 RVA: 0x00024EB7 File Offset: 0x000230B7
		public void Set(ref LobbyModificationRemoveAttributeOptions other)
		{
			this.m_ApiVersion = 1;
			this.Key = other.Key;
		}

		// Token: 0x0600184B RID: 6219 RVA: 0x00024ED0 File Offset: 0x000230D0
		public void Set(ref LobbyModificationRemoveAttributeOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.Key = other.Value.Key;
			}
		}

		// Token: 0x0600184C RID: 6220 RVA: 0x00024F06 File Offset: 0x00023106
		public void Dispose()
		{
			Helper.Dispose(ref this.m_Key);
		}

		// Token: 0x04000B1B RID: 2843
		private int m_ApiVersion;

		// Token: 0x04000B1C RID: 2844
		private IntPtr m_Key;
	}
}
