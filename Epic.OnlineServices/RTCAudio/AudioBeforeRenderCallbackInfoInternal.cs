using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001A7 RID: 423
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AudioBeforeRenderCallbackInfoInternal : ICallbackInfoInternal, IGettable<AudioBeforeRenderCallbackInfo>, ISettable<AudioBeforeRenderCallbackInfo>, IDisposable
	{
		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000BFD RID: 3069 RVA: 0x00011DD0 File Offset: 0x0000FFD0
		// (set) Token: 0x06000BFE RID: 3070 RVA: 0x00011DF1 File Offset: 0x0000FFF1
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

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000BFF RID: 3071 RVA: 0x00011E04 File Offset: 0x00010004
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000C00 RID: 3072 RVA: 0x00011E1C File Offset: 0x0001001C
		// (set) Token: 0x06000C01 RID: 3073 RVA: 0x00011E3D File Offset: 0x0001003D
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

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000C02 RID: 3074 RVA: 0x00011E50 File Offset: 0x00010050
		// (set) Token: 0x06000C03 RID: 3075 RVA: 0x00011E71 File Offset: 0x00010071
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

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000C04 RID: 3076 RVA: 0x00011E84 File Offset: 0x00010084
		// (set) Token: 0x06000C05 RID: 3077 RVA: 0x00011EA5 File Offset: 0x000100A5
		public AudioBuffer? Buffer
		{
			get
			{
				AudioBuffer? result;
				Helper.Get<AudioBufferInternal, AudioBuffer>(this.m_Buffer, out result);
				return result;
			}
			set
			{
				Helper.Set<AudioBuffer, AudioBufferInternal>(ref value, ref this.m_Buffer);
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000C06 RID: 3078 RVA: 0x00011EB8 File Offset: 0x000100B8
		// (set) Token: 0x06000C07 RID: 3079 RVA: 0x00011ED9 File Offset: 0x000100D9
		public ProductUserId ParticipantId
		{
			get
			{
				ProductUserId result;
				Helper.Get<ProductUserId>(this.m_ParticipantId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_ParticipantId);
			}
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x00011EEC File Offset: 0x000100EC
		public void Set(ref AudioBeforeRenderCallbackInfo other)
		{
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.RoomName = other.RoomName;
			this.Buffer = other.Buffer;
			this.ParticipantId = other.ParticipantId;
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x00011F3C File Offset: 0x0001013C
		public void Set(ref AudioBeforeRenderCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.RoomName = other.Value.RoomName;
				this.Buffer = other.Value.Buffer;
				this.ParticipantId = other.Value.ParticipantId;
			}
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x00011FBF File Offset: 0x000101BF
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_RoomName);
			Helper.Dispose(ref this.m_Buffer);
			Helper.Dispose(ref this.m_ParticipantId);
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x00011FFE File Offset: 0x000101FE
		public void Get(out AudioBeforeRenderCallbackInfo output)
		{
			output = default(AudioBeforeRenderCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000581 RID: 1409
		private IntPtr m_ClientData;

		// Token: 0x04000582 RID: 1410
		private IntPtr m_LocalUserId;

		// Token: 0x04000583 RID: 1411
		private IntPtr m_RoomName;

		// Token: 0x04000584 RID: 1412
		private IntPtr m_Buffer;

		// Token: 0x04000585 RID: 1413
		private IntPtr m_ParticipantId;
	}
}
