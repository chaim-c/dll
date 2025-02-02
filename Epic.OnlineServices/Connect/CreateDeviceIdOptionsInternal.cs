using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000521 RID: 1313
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CreateDeviceIdOptionsInternal : ISettable<CreateDeviceIdOptions>, IDisposable
	{
		// Token: 0x170009D3 RID: 2515
		// (set) Token: 0x060021B5 RID: 8629 RVA: 0x0003267A File Offset: 0x0003087A
		public Utf8String DeviceModel
		{
			set
			{
				Helper.Set(value, ref this.m_DeviceModel);
			}
		}

		// Token: 0x060021B6 RID: 8630 RVA: 0x0003268A File Offset: 0x0003088A
		public void Set(ref CreateDeviceIdOptions other)
		{
			this.m_ApiVersion = 1;
			this.DeviceModel = other.DeviceModel;
		}

		// Token: 0x060021B7 RID: 8631 RVA: 0x000326A4 File Offset: 0x000308A4
		public void Set(ref CreateDeviceIdOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.DeviceModel = other.Value.DeviceModel;
			}
		}

		// Token: 0x060021B8 RID: 8632 RVA: 0x000326DA File Offset: 0x000308DA
		public void Dispose()
		{
			Helper.Dispose(ref this.m_DeviceModel);
		}

		// Token: 0x04000F03 RID: 3843
		private int m_ApiVersion;

		// Token: 0x04000F04 RID: 3844
		private IntPtr m_DeviceModel;
	}
}
