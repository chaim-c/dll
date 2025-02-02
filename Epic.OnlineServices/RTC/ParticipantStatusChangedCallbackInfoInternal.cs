using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTC
{
	// Token: 0x02000193 RID: 403
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ParticipantStatusChangedCallbackInfoInternal : ICallbackInfoInternal, IGettable<ParticipantStatusChangedCallbackInfo>, ISettable<ParticipantStatusChangedCallbackInfo>, IDisposable
	{
		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000B82 RID: 2946 RVA: 0x00010FDC File Offset: 0x0000F1DC
		// (set) Token: 0x06000B83 RID: 2947 RVA: 0x00010FFD File Offset: 0x0000F1FD
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

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000B84 RID: 2948 RVA: 0x00011010 File Offset: 0x0000F210
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000B85 RID: 2949 RVA: 0x00011028 File Offset: 0x0000F228
		// (set) Token: 0x06000B86 RID: 2950 RVA: 0x00011049 File Offset: 0x0000F249
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

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000B87 RID: 2951 RVA: 0x0001105C File Offset: 0x0000F25C
		// (set) Token: 0x06000B88 RID: 2952 RVA: 0x0001107D File Offset: 0x0000F27D
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

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000B89 RID: 2953 RVA: 0x00011090 File Offset: 0x0000F290
		// (set) Token: 0x06000B8A RID: 2954 RVA: 0x000110B1 File Offset: 0x0000F2B1
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

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000B8B RID: 2955 RVA: 0x000110C4 File Offset: 0x0000F2C4
		// (set) Token: 0x06000B8C RID: 2956 RVA: 0x000110DC File Offset: 0x0000F2DC
		public RTCParticipantStatus ParticipantStatus
		{
			get
			{
				return this.m_ParticipantStatus;
			}
			set
			{
				this.m_ParticipantStatus = value;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000B8D RID: 2957 RVA: 0x000110E8 File Offset: 0x0000F2E8
		// (set) Token: 0x06000B8E RID: 2958 RVA: 0x0001110F File Offset: 0x0000F30F
		public ParticipantMetadata[] ParticipantMetadata
		{
			get
			{
				ParticipantMetadata[] result;
				Helper.Get<ParticipantMetadataInternal, ParticipantMetadata>(this.m_ParticipantMetadata, out result, this.m_ParticipantMetadataCount);
				return result;
			}
			set
			{
				Helper.Set<ParticipantMetadata, ParticipantMetadataInternal>(ref value, ref this.m_ParticipantMetadata, out this.m_ParticipantMetadataCount);
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000B8F RID: 2959 RVA: 0x00011128 File Offset: 0x0000F328
		// (set) Token: 0x06000B90 RID: 2960 RVA: 0x00011149 File Offset: 0x0000F349
		public bool ParticipantInBlocklist
		{
			get
			{
				bool result;
				Helper.Get(this.m_ParticipantInBlocklist, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_ParticipantInBlocklist);
			}
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x0001115C File Offset: 0x0000F35C
		public void Set(ref ParticipantStatusChangedCallbackInfo other)
		{
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.RoomName = other.RoomName;
			this.ParticipantId = other.ParticipantId;
			this.ParticipantStatus = other.ParticipantStatus;
			this.ParticipantMetadata = other.ParticipantMetadata;
			this.ParticipantInBlocklist = other.ParticipantInBlocklist;
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x000111C8 File Offset: 0x0000F3C8
		public void Set(ref ParticipantStatusChangedCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.RoomName = other.Value.RoomName;
				this.ParticipantId = other.Value.ParticipantId;
				this.ParticipantStatus = other.Value.ParticipantStatus;
				this.ParticipantMetadata = other.Value.ParticipantMetadata;
				this.ParticipantInBlocklist = other.Value.ParticipantInBlocklist;
			}
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x00011278 File Offset: 0x0000F478
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_RoomName);
			Helper.Dispose(ref this.m_ParticipantId);
			Helper.Dispose(ref this.m_ParticipantMetadata);
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x000112B7 File Offset: 0x0000F4B7
		public void Get(out ParticipantStatusChangedCallbackInfo output)
		{
			output = default(ParticipantStatusChangedCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x0400053D RID: 1341
		private IntPtr m_ClientData;

		// Token: 0x0400053E RID: 1342
		private IntPtr m_LocalUserId;

		// Token: 0x0400053F RID: 1343
		private IntPtr m_RoomName;

		// Token: 0x04000540 RID: 1344
		private IntPtr m_ParticipantId;

		// Token: 0x04000541 RID: 1345
		private RTCParticipantStatus m_ParticipantStatus;

		// Token: 0x04000542 RID: 1346
		private uint m_ParticipantMetadataCount;

		// Token: 0x04000543 RID: 1347
		private IntPtr m_ParticipantMetadata;

		// Token: 0x04000544 RID: 1348
		private int m_ParticipantInBlocklist;
	}
}
