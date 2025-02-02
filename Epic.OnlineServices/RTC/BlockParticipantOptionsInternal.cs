using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTC
{
	// Token: 0x0200017A RID: 378
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct BlockParticipantOptionsInternal : ISettable<BlockParticipantOptions>, IDisposable
	{
		// Token: 0x1700027D RID: 637
		// (set) Token: 0x06000ACD RID: 2765 RVA: 0x00010238 File Offset: 0x0000E438
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x1700027E RID: 638
		// (set) Token: 0x06000ACE RID: 2766 RVA: 0x00010248 File Offset: 0x0000E448
		public Utf8String RoomName
		{
			set
			{
				Helper.Set(value, ref this.m_RoomName);
			}
		}

		// Token: 0x1700027F RID: 639
		// (set) Token: 0x06000ACF RID: 2767 RVA: 0x00010258 File Offset: 0x0000E458
		public ProductUserId ParticipantId
		{
			set
			{
				Helper.Set(value, ref this.m_ParticipantId);
			}
		}

		// Token: 0x17000280 RID: 640
		// (set) Token: 0x06000AD0 RID: 2768 RVA: 0x00010268 File Offset: 0x0000E468
		public bool Blocked
		{
			set
			{
				Helper.Set(value, ref this.m_Blocked);
			}
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x00010278 File Offset: 0x0000E478
		public void Set(ref BlockParticipantOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.RoomName = other.RoomName;
			this.ParticipantId = other.ParticipantId;
			this.Blocked = other.Blocked;
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x000102B8 File Offset: 0x0000E4B8
		public void Set(ref BlockParticipantOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.RoomName = other.Value.RoomName;
				this.ParticipantId = other.Value.ParticipantId;
				this.Blocked = other.Value.Blocked;
			}
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x0001032D File Offset: 0x0000E52D
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_RoomName);
			Helper.Dispose(ref this.m_ParticipantId);
		}

		// Token: 0x040004FB RID: 1275
		private int m_ApiVersion;

		// Token: 0x040004FC RID: 1276
		private IntPtr m_LocalUserId;

		// Token: 0x040004FD RID: 1277
		private IntPtr m_RoomName;

		// Token: 0x040004FE RID: 1278
		private IntPtr m_ParticipantId;

		// Token: 0x040004FF RID: 1279
		private int m_Blocked;
	}
}
