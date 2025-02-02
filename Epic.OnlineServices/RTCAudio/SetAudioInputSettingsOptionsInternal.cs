using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001DF RID: 479
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SetAudioInputSettingsOptionsInternal : ISettable<SetAudioInputSettingsOptions>, IDisposable
	{
		// Token: 0x1700033D RID: 829
		// (set) Token: 0x06000D59 RID: 3417 RVA: 0x00013C57 File Offset: 0x00011E57
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x1700033E RID: 830
		// (set) Token: 0x06000D5A RID: 3418 RVA: 0x00013C67 File Offset: 0x00011E67
		public Utf8String DeviceId
		{
			set
			{
				Helper.Set(value, ref this.m_DeviceId);
			}
		}

		// Token: 0x1700033F RID: 831
		// (set) Token: 0x06000D5B RID: 3419 RVA: 0x00013C77 File Offset: 0x00011E77
		public float Volume
		{
			set
			{
				this.m_Volume = value;
			}
		}

		// Token: 0x17000340 RID: 832
		// (set) Token: 0x06000D5C RID: 3420 RVA: 0x00013C81 File Offset: 0x00011E81
		public bool PlatformAEC
		{
			set
			{
				Helper.Set(value, ref this.m_PlatformAEC);
			}
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x00013C91 File Offset: 0x00011E91
		public void Set(ref SetAudioInputSettingsOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.DeviceId = other.DeviceId;
			this.Volume = other.Volume;
			this.PlatformAEC = other.PlatformAEC;
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x00013CD0 File Offset: 0x00011ED0
		public void Set(ref SetAudioInputSettingsOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.DeviceId = other.Value.DeviceId;
				this.Volume = other.Value.Volume;
				this.PlatformAEC = other.Value.PlatformAEC;
			}
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x00013D45 File Offset: 0x00011F45
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_DeviceId);
		}

		// Token: 0x040005FF RID: 1535
		private int m_ApiVersion;

		// Token: 0x04000600 RID: 1536
		private IntPtr m_LocalUserId;

		// Token: 0x04000601 RID: 1537
		private IntPtr m_DeviceId;

		// Token: 0x04000602 RID: 1538
		private float m_Volume;

		// Token: 0x04000603 RID: 1539
		private int m_PlatformAEC;
	}
}
