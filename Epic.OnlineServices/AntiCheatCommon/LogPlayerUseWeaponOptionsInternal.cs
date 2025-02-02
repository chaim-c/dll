using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x020005FD RID: 1533
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LogPlayerUseWeaponOptionsInternal : ISettable<LogPlayerUseWeaponOptions>, IDisposable
	{
		// Token: 0x17000BBC RID: 3004
		// (set) Token: 0x06002747 RID: 10055 RVA: 0x0003A8F5 File Offset: 0x00038AF5
		public LogPlayerUseWeaponData? UseWeaponData
		{
			set
			{
				Helper.Set<LogPlayerUseWeaponData, LogPlayerUseWeaponDataInternal>(ref value, ref this.m_UseWeaponData);
			}
		}

		// Token: 0x06002748 RID: 10056 RVA: 0x0003A906 File Offset: 0x00038B06
		public void Set(ref LogPlayerUseWeaponOptions other)
		{
			this.m_ApiVersion = 2;
			this.UseWeaponData = other.UseWeaponData;
		}

		// Token: 0x06002749 RID: 10057 RVA: 0x0003A920 File Offset: 0x00038B20
		public void Set(ref LogPlayerUseWeaponOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 2;
				this.UseWeaponData = other.Value.UseWeaponData;
			}
		}

		// Token: 0x0600274A RID: 10058 RVA: 0x0003A956 File Offset: 0x00038B56
		public void Dispose()
		{
			Helper.Dispose(ref this.m_UseWeaponData);
		}

		// Token: 0x040011AD RID: 4525
		private int m_ApiVersion;

		// Token: 0x040011AE RID: 4526
		private IntPtr m_UseWeaponData;
	}
}
