using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001E1 RID: 481
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SetAudioOutputSettingsOptionsInternal : ISettable<SetAudioOutputSettingsOptions>, IDisposable
	{
		// Token: 0x17000344 RID: 836
		// (set) Token: 0x06000D66 RID: 3430 RVA: 0x00013D93 File Offset: 0x00011F93
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x17000345 RID: 837
		// (set) Token: 0x06000D67 RID: 3431 RVA: 0x00013DA3 File Offset: 0x00011FA3
		public Utf8String DeviceId
		{
			set
			{
				Helper.Set(value, ref this.m_DeviceId);
			}
		}

		// Token: 0x17000346 RID: 838
		// (set) Token: 0x06000D68 RID: 3432 RVA: 0x00013DB3 File Offset: 0x00011FB3
		public float Volume
		{
			set
			{
				this.m_Volume = value;
			}
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x00013DBD File Offset: 0x00011FBD
		public void Set(ref SetAudioOutputSettingsOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.DeviceId = other.DeviceId;
			this.Volume = other.Volume;
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x00013DF0 File Offset: 0x00011FF0
		public void Set(ref SetAudioOutputSettingsOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.DeviceId = other.Value.DeviceId;
				this.Volume = other.Value.Volume;
			}
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x00013E50 File Offset: 0x00012050
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_DeviceId);
		}

		// Token: 0x04000607 RID: 1543
		private int m_ApiVersion;

		// Token: 0x04000608 RID: 1544
		private IntPtr m_LocalUserId;

		// Token: 0x04000609 RID: 1545
		private IntPtr m_DeviceId;

		// Token: 0x0400060A RID: 1546
		private float m_Volume;
	}
}
