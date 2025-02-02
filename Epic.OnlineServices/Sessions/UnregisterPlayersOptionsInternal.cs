using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200015F RID: 351
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UnregisterPlayersOptionsInternal : ISettable<UnregisterPlayersOptions>, IDisposable
	{
		// Token: 0x1700023A RID: 570
		// (set) Token: 0x06000A1A RID: 2586 RVA: 0x0000F168 File Offset: 0x0000D368
		public Utf8String SessionName
		{
			set
			{
				Helper.Set(value, ref this.m_SessionName);
			}
		}

		// Token: 0x1700023B RID: 571
		// (set) Token: 0x06000A1B RID: 2587 RVA: 0x0000F178 File Offset: 0x0000D378
		public ProductUserId[] PlayersToUnregister
		{
			set
			{
				Helper.Set<ProductUserId>(value, ref this.m_PlayersToUnregister, out this.m_PlayersToUnregisterCount);
			}
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x0000F18E File Offset: 0x0000D38E
		public void Set(ref UnregisterPlayersOptions other)
		{
			this.m_ApiVersion = 2;
			this.SessionName = other.SessionName;
			this.PlayersToUnregister = other.PlayersToUnregister;
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x0000F1B4 File Offset: 0x0000D3B4
		public void Set(ref UnregisterPlayersOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 2;
				this.SessionName = other.Value.SessionName;
				this.PlayersToUnregister = other.Value.PlayersToUnregister;
			}
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x0000F1FF File Offset: 0x0000D3FF
		public void Dispose()
		{
			Helper.Dispose(ref this.m_SessionName);
			Helper.Dispose(ref this.m_PlayersToUnregister);
		}

		// Token: 0x040004AD RID: 1197
		private int m_ApiVersion;

		// Token: 0x040004AE RID: 1198
		private IntPtr m_SessionName;

		// Token: 0x040004AF RID: 1199
		private IntPtr m_PlayersToUnregister;

		// Token: 0x040004B0 RID: 1200
		private uint m_PlayersToUnregisterCount;
	}
}
