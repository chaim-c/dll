using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTC
{
	// Token: 0x02000199 RID: 409
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SetSettingOptionsInternal : ISettable<SetSettingOptions>, IDisposable
	{
		// Token: 0x170002CD RID: 717
		// (set) Token: 0x06000BB9 RID: 3001 RVA: 0x000117EE File Offset: 0x0000F9EE
		public Utf8String SettingName
		{
			set
			{
				Helper.Set(value, ref this.m_SettingName);
			}
		}

		// Token: 0x170002CE RID: 718
		// (set) Token: 0x06000BBA RID: 3002 RVA: 0x000117FE File Offset: 0x0000F9FE
		public Utf8String SettingValue
		{
			set
			{
				Helper.Set(value, ref this.m_SettingValue);
			}
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x0001180E File Offset: 0x0000FA0E
		public void Set(ref SetSettingOptions other)
		{
			this.m_ApiVersion = 1;
			this.SettingName = other.SettingName;
			this.SettingValue = other.SettingValue;
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x00011834 File Offset: 0x0000FA34
		public void Set(ref SetSettingOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.SettingName = other.Value.SettingName;
				this.SettingValue = other.Value.SettingValue;
			}
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x0001187F File Offset: 0x0000FA7F
		public void Dispose()
		{
			Helper.Dispose(ref this.m_SettingName);
			Helper.Dispose(ref this.m_SettingValue);
		}

		// Token: 0x0400055D RID: 1373
		private int m_ApiVersion;

		// Token: 0x0400055E RID: 1374
		private IntPtr m_SettingName;

		// Token: 0x0400055F RID: 1375
		private IntPtr m_SettingValue;
	}
}
