using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001AD RID: 429
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AudioDevicesChangedCallbackInfoInternal : ICallbackInfoInternal, IGettable<AudioDevicesChangedCallbackInfo>, ISettable<AudioDevicesChangedCallbackInfo>, IDisposable
	{
		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000C38 RID: 3128 RVA: 0x00012450 File Offset: 0x00010650
		// (set) Token: 0x06000C39 RID: 3129 RVA: 0x00012471 File Offset: 0x00010671
		public object ClientData
		{
			get
			{
				object result;
				Helper.Get(this.m_ClientData, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_ClientData);
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000C3A RID: 3130 RVA: 0x00012484 File Offset: 0x00010684
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x0001249C File Offset: 0x0001069C
		public void Set(ref AudioDevicesChangedCallbackInfo other)
		{
			this.ClientData = other.ClientData;
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x000124AC File Offset: 0x000106AC
		public void Set(ref AudioDevicesChangedCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ClientData = other.Value.ClientData;
			}
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x000124DB File Offset: 0x000106DB
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x000124EA File Offset: 0x000106EA
		public void Get(out AudioDevicesChangedCallbackInfo output)
		{
			output = default(AudioDevicesChangedCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000597 RID: 1431
		private IntPtr m_ClientData;
	}
}
