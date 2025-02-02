using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001B3 RID: 435
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AudioOutputDeviceInfoInternal : IGettable<AudioOutputDeviceInfo>, ISettable<AudioOutputDeviceInfo>, IDisposable
	{
		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000C6E RID: 3182 RVA: 0x00012964 File Offset: 0x00010B64
		// (set) Token: 0x06000C6F RID: 3183 RVA: 0x00012985 File Offset: 0x00010B85
		public bool DefaultDevice
		{
			get
			{
				bool result;
				Helper.Get(this.m_DefaultDevice, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_DefaultDevice);
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000C70 RID: 3184 RVA: 0x00012998 File Offset: 0x00010B98
		// (set) Token: 0x06000C71 RID: 3185 RVA: 0x000129B9 File Offset: 0x00010BB9
		public Utf8String DeviceId
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_DeviceId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_DeviceId);
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000C72 RID: 3186 RVA: 0x000129CC File Offset: 0x00010BCC
		// (set) Token: 0x06000C73 RID: 3187 RVA: 0x000129ED File Offset: 0x00010BED
		public Utf8String DeviceName
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_DeviceName, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_DeviceName);
			}
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x000129FD File Offset: 0x00010BFD
		public void Set(ref AudioOutputDeviceInfo other)
		{
			this.m_ApiVersion = 1;
			this.DefaultDevice = other.DefaultDevice;
			this.DeviceId = other.DeviceId;
			this.DeviceName = other.DeviceName;
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x00012A30 File Offset: 0x00010C30
		public void Set(ref AudioOutputDeviceInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.DefaultDevice = other.Value.DefaultDevice;
				this.DeviceId = other.Value.DeviceId;
				this.DeviceName = other.Value.DeviceName;
			}
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x00012A90 File Offset: 0x00010C90
		public void Dispose()
		{
			Helper.Dispose(ref this.m_DeviceId);
			Helper.Dispose(ref this.m_DeviceName);
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x00012AAB File Offset: 0x00010CAB
		public void Get(out AudioOutputDeviceInfo output)
		{
			output = default(AudioOutputDeviceInfo);
			output.Set(ref this);
		}

		// Token: 0x040005AA RID: 1450
		private int m_ApiVersion;

		// Token: 0x040005AB RID: 1451
		private int m_DefaultDevice;

		// Token: 0x040005AC RID: 1452
		private IntPtr m_DeviceId;

		// Token: 0x040005AD RID: 1453
		private IntPtr m_DeviceName;
	}
}
