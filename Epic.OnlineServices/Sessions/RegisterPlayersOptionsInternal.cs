using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000114 RID: 276
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RegisterPlayersOptionsInternal : ISettable<RegisterPlayersOptions>, IDisposable
	{
		// Token: 0x170001BB RID: 443
		// (set) Token: 0x0600086D RID: 2157 RVA: 0x0000C189 File Offset: 0x0000A389
		public Utf8String SessionName
		{
			set
			{
				Helper.Set(value, ref this.m_SessionName);
			}
		}

		// Token: 0x170001BC RID: 444
		// (set) Token: 0x0600086E RID: 2158 RVA: 0x0000C199 File Offset: 0x0000A399
		public ProductUserId[] PlayersToRegister
		{
			set
			{
				Helper.Set<ProductUserId>(value, ref this.m_PlayersToRegister, out this.m_PlayersToRegisterCount);
			}
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x0000C1AF File Offset: 0x0000A3AF
		public void Set(ref RegisterPlayersOptions other)
		{
			this.m_ApiVersion = 2;
			this.SessionName = other.SessionName;
			this.PlayersToRegister = other.PlayersToRegister;
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x0000C1D4 File Offset: 0x0000A3D4
		public void Set(ref RegisterPlayersOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 2;
				this.SessionName = other.Value.SessionName;
				this.PlayersToRegister = other.Value.PlayersToRegister;
			}
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x0000C21F File Offset: 0x0000A41F
		public void Dispose()
		{
			Helper.Dispose(ref this.m_SessionName);
			Helper.Dispose(ref this.m_PlayersToRegister);
		}

		// Token: 0x040003D8 RID: 984
		private int m_ApiVersion;

		// Token: 0x040003D9 RID: 985
		private IntPtr m_SessionName;

		// Token: 0x040003DA RID: 986
		private IntPtr m_PlayersToRegister;

		// Token: 0x040003DB RID: 987
		private uint m_PlayersToRegisterCount;
	}
}
