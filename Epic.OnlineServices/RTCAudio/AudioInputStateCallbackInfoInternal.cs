using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001B1 RID: 433
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AudioInputStateCallbackInfoInternal : ICallbackInfoInternal, IGettable<AudioInputStateCallbackInfo>, ISettable<AudioInputStateCallbackInfo>, IDisposable
	{
		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000C5A RID: 3162 RVA: 0x00012750 File Offset: 0x00010950
		// (set) Token: 0x06000C5B RID: 3163 RVA: 0x00012771 File Offset: 0x00010971
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

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000C5C RID: 3164 RVA: 0x00012784 File Offset: 0x00010984
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000C5D RID: 3165 RVA: 0x0001279C File Offset: 0x0001099C
		// (set) Token: 0x06000C5E RID: 3166 RVA: 0x000127BD File Offset: 0x000109BD
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.Get<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000C5F RID: 3167 RVA: 0x000127D0 File Offset: 0x000109D0
		// (set) Token: 0x06000C60 RID: 3168 RVA: 0x000127F1 File Offset: 0x000109F1
		public Utf8String RoomName
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_RoomName, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_RoomName);
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000C61 RID: 3169 RVA: 0x00012804 File Offset: 0x00010A04
		// (set) Token: 0x06000C62 RID: 3170 RVA: 0x0001281C File Offset: 0x00010A1C
		public RTCAudioInputStatus Status
		{
			get
			{
				return this.m_Status;
			}
			set
			{
				this.m_Status = value;
			}
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x00012826 File Offset: 0x00010A26
		public void Set(ref AudioInputStateCallbackInfo other)
		{
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.RoomName = other.RoomName;
			this.Status = other.Status;
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x00012860 File Offset: 0x00010A60
		public void Set(ref AudioInputStateCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.RoomName = other.Value.RoomName;
				this.Status = other.Value.Status;
			}
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x000128CE File Offset: 0x00010ACE
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_RoomName);
		}

		// Token: 0x06000C66 RID: 3174 RVA: 0x000128F5 File Offset: 0x00010AF5
		public void Get(out AudioInputStateCallbackInfo output)
		{
			output = default(AudioInputStateCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x040005A3 RID: 1443
		private IntPtr m_ClientData;

		// Token: 0x040005A4 RID: 1444
		private IntPtr m_LocalUserId;

		// Token: 0x040005A5 RID: 1445
		private IntPtr m_RoomName;

		// Token: 0x040005A6 RID: 1446
		private RTCAudioInputStatus m_Status;
	}
}
