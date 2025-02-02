using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001A5 RID: 421
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyParticipantUpdatedOptionsInternal : ISettable<AddNotifyParticipantUpdatedOptions>, IDisposable
	{
		// Token: 0x170002E3 RID: 739
		// (set) Token: 0x06000BEC RID: 3052 RVA: 0x00011C64 File Offset: 0x0000FE64
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x170002E4 RID: 740
		// (set) Token: 0x06000BED RID: 3053 RVA: 0x00011C74 File Offset: 0x0000FE74
		public Utf8String RoomName
		{
			set
			{
				Helper.Set(value, ref this.m_RoomName);
			}
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x00011C84 File Offset: 0x0000FE84
		public void Set(ref AddNotifyParticipantUpdatedOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.RoomName = other.RoomName;
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x00011CA8 File Offset: 0x0000FEA8
		public void Set(ref AddNotifyParticipantUpdatedOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.RoomName = other.Value.RoomName;
			}
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x00011CF3 File Offset: 0x0000FEF3
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_RoomName);
		}

		// Token: 0x04000579 RID: 1401
		private int m_ApiVersion;

		// Token: 0x0400057A RID: 1402
		private IntPtr m_LocalUserId;

		// Token: 0x0400057B RID: 1403
		private IntPtr m_RoomName;
	}
}
