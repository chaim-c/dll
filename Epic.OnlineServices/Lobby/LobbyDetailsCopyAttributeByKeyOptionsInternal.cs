using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000369 RID: 873
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyDetailsCopyAttributeByKeyOptionsInternal : ISettable<LobbyDetailsCopyAttributeByKeyOptions>, IDisposable
	{
		// Token: 0x17000694 RID: 1684
		// (set) Token: 0x06001715 RID: 5909 RVA: 0x0002265A File Offset: 0x0002085A
		public Utf8String AttrKey
		{
			set
			{
				Helper.Set(value, ref this.m_AttrKey);
			}
		}

		// Token: 0x06001716 RID: 5910 RVA: 0x0002266A File Offset: 0x0002086A
		public void Set(ref LobbyDetailsCopyAttributeByKeyOptions other)
		{
			this.m_ApiVersion = 1;
			this.AttrKey = other.AttrKey;
		}

		// Token: 0x06001717 RID: 5911 RVA: 0x00022684 File Offset: 0x00020884
		public void Set(ref LobbyDetailsCopyAttributeByKeyOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.AttrKey = other.Value.AttrKey;
			}
		}

		// Token: 0x06001718 RID: 5912 RVA: 0x000226BA File Offset: 0x000208BA
		public void Dispose()
		{
			Helper.Dispose(ref this.m_AttrKey);
		}

		// Token: 0x04000A7E RID: 2686
		private int m_ApiVersion;

		// Token: 0x04000A7F RID: 2687
		private IntPtr m_AttrKey;
	}
}
