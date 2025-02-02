using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001D5 RID: 469
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ParticipantUpdatedCallbackInfoInternal : ICallbackInfoInternal, IGettable<ParticipantUpdatedCallbackInfo>, ISettable<ParticipantUpdatedCallbackInfo>, IDisposable
	{
		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000D07 RID: 3335 RVA: 0x00012F28 File Offset: 0x00011128
		// (set) Token: 0x06000D08 RID: 3336 RVA: 0x00012F49 File Offset: 0x00011149
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

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000D09 RID: 3337 RVA: 0x00012F5C File Offset: 0x0001115C
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000D0A RID: 3338 RVA: 0x00012F74 File Offset: 0x00011174
		// (set) Token: 0x06000D0B RID: 3339 RVA: 0x00012F95 File Offset: 0x00011195
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

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000D0C RID: 3340 RVA: 0x00012FA8 File Offset: 0x000111A8
		// (set) Token: 0x06000D0D RID: 3341 RVA: 0x00012FC9 File Offset: 0x000111C9
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

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000D0E RID: 3342 RVA: 0x00012FDC File Offset: 0x000111DC
		// (set) Token: 0x06000D0F RID: 3343 RVA: 0x00012FFD File Offset: 0x000111FD
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

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000D10 RID: 3344 RVA: 0x00013010 File Offset: 0x00011210
		// (set) Token: 0x06000D11 RID: 3345 RVA: 0x00013031 File Offset: 0x00011231
		public bool Speaking
		{
			get
			{
				bool result;
				Helper.Get(this.m_Speaking, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_Speaking);
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000D12 RID: 3346 RVA: 0x00013044 File Offset: 0x00011244
		// (set) Token: 0x06000D13 RID: 3347 RVA: 0x0001305C File Offset: 0x0001125C
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

		// Token: 0x06000D14 RID: 3348 RVA: 0x00013068 File Offset: 0x00011268
		public void Set(ref ParticipantUpdatedCallbackInfo other)
		{
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.RoomName = other.RoomName;
			this.ParticipantId = other.ParticipantId;
			this.Speaking = other.Speaking;
			this.AudioStatus = other.AudioStatus;
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x000130C4 File Offset: 0x000112C4
		public void Set(ref ParticipantUpdatedCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.RoomName = other.Value.RoomName;
				this.ParticipantId = other.Value.ParticipantId;
				this.Speaking = other.Value.Speaking;
				this.AudioStatus = other.Value.AudioStatus;
			}
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x0001315F File Offset: 0x0001135F
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_RoomName);
			Helper.Dispose(ref this.m_ParticipantId);
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x00013192 File Offset: 0x00011392
		public void Get(out ParticipantUpdatedCallbackInfo output)
		{
			output = default(ParticipantUpdatedCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x040005C4 RID: 1476
		private IntPtr m_ClientData;

		// Token: 0x040005C5 RID: 1477
		private IntPtr m_LocalUserId;

		// Token: 0x040005C6 RID: 1478
		private IntPtr m_RoomName;

		// Token: 0x040005C7 RID: 1479
		private IntPtr m_ParticipantId;

		// Token: 0x040005C8 RID: 1480
		private int m_Speaking;

		// Token: 0x040005C9 RID: 1481
		private RTCAudioStatus m_AudioStatus;
	}
}
