using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatServer
{
	// Token: 0x020005D1 RID: 1489
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SetClientNetworkStateOptionsInternal : ISettable<SetClientNetworkStateOptions>, IDisposable
	{
		// Token: 0x17000B3E RID: 2878
		// (set) Token: 0x0600263E RID: 9790 RVA: 0x00038D8F File Offset: 0x00036F8F
		public IntPtr ClientHandle
		{
			set
			{
				this.m_ClientHandle = value;
			}
		}

		// Token: 0x17000B3F RID: 2879
		// (set) Token: 0x0600263F RID: 9791 RVA: 0x00038D99 File Offset: 0x00036F99
		public bool IsNetworkActive
		{
			set
			{
				Helper.Set(value, ref this.m_IsNetworkActive);
			}
		}

		// Token: 0x06002640 RID: 9792 RVA: 0x00038DA9 File Offset: 0x00036FA9
		public void Set(ref SetClientNetworkStateOptions other)
		{
			this.m_ApiVersion = 1;
			this.ClientHandle = other.ClientHandle;
			this.IsNetworkActive = other.IsNetworkActive;
		}

		// Token: 0x06002641 RID: 9793 RVA: 0x00038DD0 File Offset: 0x00036FD0
		public void Set(ref SetClientNetworkStateOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.ClientHandle = other.Value.ClientHandle;
				this.IsNetworkActive = other.Value.IsNetworkActive;
			}
		}

		// Token: 0x06002642 RID: 9794 RVA: 0x00038E1B File Offset: 0x0003701B
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientHandle);
		}

		// Token: 0x040010C1 RID: 4289
		private int m_ApiVersion;

		// Token: 0x040010C2 RID: 4290
		private IntPtr m_ClientHandle;

		// Token: 0x040010C3 RID: 4291
		private int m_IsNetworkActive;
	}
}
