using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001F1 RID: 497
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UpdateSendingCallbackInfoInternal : ICallbackInfoInternal, IGettable<UpdateSendingCallbackInfo>, ISettable<UpdateSendingCallbackInfo>, IDisposable
	{
		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000E01 RID: 3585 RVA: 0x00014CF0 File Offset: 0x00012EF0
		// (set) Token: 0x06000E02 RID: 3586 RVA: 0x00014D08 File Offset: 0x00012F08
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
			set
			{
				this.m_ResultCode = value;
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000E03 RID: 3587 RVA: 0x00014D14 File Offset: 0x00012F14
		// (set) Token: 0x06000E04 RID: 3588 RVA: 0x00014D35 File Offset: 0x00012F35
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

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000E05 RID: 3589 RVA: 0x00014D48 File Offset: 0x00012F48
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000E06 RID: 3590 RVA: 0x00014D60 File Offset: 0x00012F60
		// (set) Token: 0x06000E07 RID: 3591 RVA: 0x00014D81 File Offset: 0x00012F81
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

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000E08 RID: 3592 RVA: 0x00014D94 File Offset: 0x00012F94
		// (set) Token: 0x06000E09 RID: 3593 RVA: 0x00014DB5 File Offset: 0x00012FB5
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

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000E0A RID: 3594 RVA: 0x00014DC8 File Offset: 0x00012FC8
		// (set) Token: 0x06000E0B RID: 3595 RVA: 0x00014DE0 File Offset: 0x00012FE0
		public RTCAudioStatus AudioStatus
		{
			get
			{
				return this.m_AudioStatus;
			}
			set
			{
				this.m_AudioStatus = value;
			}
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x00014DEC File Offset: 0x00012FEC
		public void Set(ref UpdateSendingCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.RoomName = other.RoomName;
			this.AudioStatus = other.AudioStatus;
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x00014E3C File Offset: 0x0001303C
		public void Set(ref UpdateSendingCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.RoomName = other.Value.RoomName;
				this.AudioStatus = other.Value.AudioStatus;
			}
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x00014EBF File Offset: 0x000130BF
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_RoomName);
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x00014EE6 File Offset: 0x000130E6
		public void Get(out UpdateSendingCallbackInfo output)
		{
			output = default(UpdateSendingCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x0400064E RID: 1614
		private Result m_ResultCode;

		// Token: 0x0400064F RID: 1615
		private IntPtr m_ClientData;

		// Token: 0x04000650 RID: 1616
		private IntPtr m_LocalUserId;

		// Token: 0x04000651 RID: 1617
		private IntPtr m_RoomName;

		// Token: 0x04000652 RID: 1618
		private RTCAudioStatus m_AudioStatus;
	}
}
