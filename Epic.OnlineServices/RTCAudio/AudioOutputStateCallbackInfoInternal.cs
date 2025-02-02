using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001B5 RID: 437
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AudioOutputStateCallbackInfoInternal : ICallbackInfoInternal, IGettable<AudioOutputStateCallbackInfo>, ISettable<AudioOutputStateCallbackInfo>, IDisposable
	{
		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000C82 RID: 3202 RVA: 0x00012B58 File Offset: 0x00010D58
		// (set) Token: 0x06000C83 RID: 3203 RVA: 0x00012B79 File Offset: 0x00010D79
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

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000C84 RID: 3204 RVA: 0x00012B8C File Offset: 0x00010D8C
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000C85 RID: 3205 RVA: 0x00012BA4 File Offset: 0x00010DA4
		// (set) Token: 0x06000C86 RID: 3206 RVA: 0x00012BC5 File Offset: 0x00010DC5
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

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000C87 RID: 3207 RVA: 0x00012BD8 File Offset: 0x00010DD8
		// (set) Token: 0x06000C88 RID: 3208 RVA: 0x00012BF9 File Offset: 0x00010DF9
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

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000C89 RID: 3209 RVA: 0x00012C0C File Offset: 0x00010E0C
		// (set) Token: 0x06000C8A RID: 3210 RVA: 0x00012C24 File Offset: 0x00010E24
		public RTCAudioOutputStatus Status
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

		// Token: 0x06000C8B RID: 3211 RVA: 0x00012C2E File Offset: 0x00010E2E
		public void Set(ref AudioOutputStateCallbackInfo other)
		{
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.RoomName = other.RoomName;
			this.Status = other.Status;
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x00012C68 File Offset: 0x00010E68
		public void Set(ref AudioOutputStateCallbackInfo? other)
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

		// Token: 0x06000C8D RID: 3213 RVA: 0x00012CD6 File Offset: 0x00010ED6
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_RoomName);
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x00012CFD File Offset: 0x00010EFD
		public void Get(out AudioOutputStateCallbackInfo output)
		{
			output = default(AudioOutputStateCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x040005B2 RID: 1458
		private IntPtr m_ClientData;

		// Token: 0x040005B3 RID: 1459
		private IntPtr m_LocalUserId;

		// Token: 0x040005B4 RID: 1460
		private IntPtr m_RoomName;

		// Token: 0x040005B5 RID: 1461
		private RTCAudioOutputStatus m_Status;
	}
}
