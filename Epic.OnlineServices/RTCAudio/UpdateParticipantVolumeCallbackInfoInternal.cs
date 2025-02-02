using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001E5 RID: 485
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UpdateParticipantVolumeCallbackInfoInternal : ICallbackInfoInternal, IGettable<UpdateParticipantVolumeCallbackInfo>, ISettable<UpdateParticipantVolumeCallbackInfo>, IDisposable
	{
		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000D80 RID: 3456 RVA: 0x00013FCC File Offset: 0x000121CC
		// (set) Token: 0x06000D81 RID: 3457 RVA: 0x00013FE4 File Offset: 0x000121E4
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

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000D82 RID: 3458 RVA: 0x00013FF0 File Offset: 0x000121F0
		// (set) Token: 0x06000D83 RID: 3459 RVA: 0x00014011 File Offset: 0x00012211
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

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000D84 RID: 3460 RVA: 0x00014024 File Offset: 0x00012224
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000D85 RID: 3461 RVA: 0x0001403C File Offset: 0x0001223C
		// (set) Token: 0x06000D86 RID: 3462 RVA: 0x0001405D File Offset: 0x0001225D
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

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000D87 RID: 3463 RVA: 0x00014070 File Offset: 0x00012270
		// (set) Token: 0x06000D88 RID: 3464 RVA: 0x00014091 File Offset: 0x00012291
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

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000D89 RID: 3465 RVA: 0x000140A4 File Offset: 0x000122A4
		// (set) Token: 0x06000D8A RID: 3466 RVA: 0x000140C5 File Offset: 0x000122C5
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

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000D8B RID: 3467 RVA: 0x000140D8 File Offset: 0x000122D8
		// (set) Token: 0x06000D8C RID: 3468 RVA: 0x000140F0 File Offset: 0x000122F0
		public float Volume
		{
			get
			{
				return this.m_Volume;
			}
			set
			{
				this.m_Volume = value;
			}
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x000140FC File Offset: 0x000122FC
		public void Set(ref UpdateParticipantVolumeCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.RoomName = other.RoomName;
			this.ParticipantId = other.ParticipantId;
			this.Volume = other.Volume;
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x00014158 File Offset: 0x00012358
		public void Set(ref UpdateParticipantVolumeCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.RoomName = other.Value.RoomName;
				this.ParticipantId = other.Value.ParticipantId;
				this.Volume = other.Value.Volume;
			}
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x000141F3 File Offset: 0x000123F3
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_RoomName);
			Helper.Dispose(ref this.m_ParticipantId);
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x00014226 File Offset: 0x00012426
		public void Get(out UpdateParticipantVolumeCallbackInfo output)
		{
			output = default(UpdateParticipantVolumeCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000614 RID: 1556
		private Result m_ResultCode;

		// Token: 0x04000615 RID: 1557
		private IntPtr m_ClientData;

		// Token: 0x04000616 RID: 1558
		private IntPtr m_LocalUserId;

		// Token: 0x04000617 RID: 1559
		private IntPtr m_RoomName;

		// Token: 0x04000618 RID: 1560
		private IntPtr m_ParticipantId;

		// Token: 0x04000619 RID: 1561
		private float m_Volume;
	}
}
