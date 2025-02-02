using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTC
{
	// Token: 0x02000176 RID: 374
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyParticipantStatusChangedOptionsInternal : ISettable<AddNotifyParticipantStatusChangedOptions>, IDisposable
	{
		// Token: 0x1700026A RID: 618
		// (set) Token: 0x06000AA1 RID: 2721 RVA: 0x0000FDEC File Offset: 0x0000DFEC
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x1700026B RID: 619
		// (set) Token: 0x06000AA2 RID: 2722 RVA: 0x0000FDFC File Offset: 0x0000DFFC
		public Utf8String RoomName
		{
			set
			{
				Helper.Set(value, ref this.m_RoomName);
			}
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x0000FE0C File Offset: 0x0000E00C
		public void Set(ref AddNotifyParticipantStatusChangedOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.RoomName = other.RoomName;
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x0000FE30 File Offset: 0x0000E030
		public void Set(ref AddNotifyParticipantStatusChangedOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.RoomName = other.Value.RoomName;
			}
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x0000FE7B File Offset: 0x0000E07B
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_RoomName);
		}

		// Token: 0x040004E8 RID: 1256
		private int m_ApiVersion;

		// Token: 0x040004E9 RID: 1257
		private IntPtr m_LocalUserId;

		// Token: 0x040004EA RID: 1258
		private IntPtr m_RoomName;
	}
}
