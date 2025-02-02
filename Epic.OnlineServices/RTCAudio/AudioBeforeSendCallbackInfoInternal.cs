using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001A9 RID: 425
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AudioBeforeSendCallbackInfoInternal : ICallbackInfoInternal, IGettable<AudioBeforeSendCallbackInfo>, ISettable<AudioBeforeSendCallbackInfo>, IDisposable
	{
		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000C16 RID: 3094 RVA: 0x000120A8 File Offset: 0x000102A8
		// (set) Token: 0x06000C17 RID: 3095 RVA: 0x000120C9 File Offset: 0x000102C9
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

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000C18 RID: 3096 RVA: 0x000120DC File Offset: 0x000102DC
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000C19 RID: 3097 RVA: 0x000120F4 File Offset: 0x000102F4
		// (set) Token: 0x06000C1A RID: 3098 RVA: 0x00012115 File Offset: 0x00010315
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

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000C1B RID: 3099 RVA: 0x00012128 File Offset: 0x00010328
		// (set) Token: 0x06000C1C RID: 3100 RVA: 0x00012149 File Offset: 0x00010349
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

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000C1D RID: 3101 RVA: 0x0001215C File Offset: 0x0001035C
		// (set) Token: 0x06000C1E RID: 3102 RVA: 0x0001217D File Offset: 0x0001037D
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

		// Token: 0x06000C1F RID: 3103 RVA: 0x0001218E File Offset: 0x0001038E
		public void Set(ref AudioBeforeSendCallbackInfo other)
		{
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.RoomName = other.RoomName;
			this.Buffer = other.Buffer;
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x000121C8 File Offset: 0x000103C8
		public void Set(ref AudioBeforeSendCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.RoomName = other.Value.RoomName;
				this.Buffer = other.Value.Buffer;
			}
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x00012236 File Offset: 0x00010436
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_RoomName);
			Helper.Dispose(ref this.m_Buffer);
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x00012269 File Offset: 0x00010469
		public void Get(out AudioBeforeSendCallbackInfo output)
		{
			output = default(AudioBeforeSendCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x0400058A RID: 1418
		private IntPtr m_ClientData;

		// Token: 0x0400058B RID: 1419
		private IntPtr m_LocalUserId;

		// Token: 0x0400058C RID: 1420
		private IntPtr m_RoomName;

		// Token: 0x0400058D RID: 1421
		private IntPtr m_Buffer;
	}
}
