using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001AF RID: 431
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AudioInputDeviceInfoInternal : IGettable<AudioInputDeviceInfo>, ISettable<AudioInputDeviceInfo>, IDisposable
	{
		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000C46 RID: 3142 RVA: 0x0001255C File Offset: 0x0001075C
		// (set) Token: 0x06000C47 RID: 3143 RVA: 0x0001257D File Offset: 0x0001077D
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

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000C48 RID: 3144 RVA: 0x00012590 File Offset: 0x00010790
		// (set) Token: 0x06000C49 RID: 3145 RVA: 0x000125B1 File Offset: 0x000107B1
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

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000C4A RID: 3146 RVA: 0x000125C4 File Offset: 0x000107C4
		// (set) Token: 0x06000C4B RID: 3147 RVA: 0x000125E5 File Offset: 0x000107E5
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

		// Token: 0x06000C4C RID: 3148 RVA: 0x000125F5 File Offset: 0x000107F5
		public void Set(ref AudioInputDeviceInfo other)
		{
			this.m_ApiVersion = 1;
			this.DefaultDevice = other.DefaultDevice;
			this.DeviceId = other.DeviceId;
			this.DeviceName = other.DeviceName;
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x00012628 File Offset: 0x00010828
		public void Set(ref AudioInputDeviceInfo? other)
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

		// Token: 0x06000C4E RID: 3150 RVA: 0x00012688 File Offset: 0x00010888
		public void Dispose()
		{
			Helper.Dispose(ref this.m_DeviceId);
			Helper.Dispose(ref this.m_DeviceName);
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x000126A3 File Offset: 0x000108A3
		public void Get(out AudioInputDeviceInfo output)
		{
			output = default(AudioInputDeviceInfo);
			output.Set(ref this);
		}

		// Token: 0x0400059B RID: 1435
		private int m_ApiVersion;

		// Token: 0x0400059C RID: 1436
		private int m_DefaultDevice;

		// Token: 0x0400059D RID: 1437
		private IntPtr m_DeviceId;

		// Token: 0x0400059E RID: 1438
		private IntPtr m_DeviceName;
	}
}
