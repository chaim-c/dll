using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x0200024B RID: 587
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct PresenceModificationDeleteDataOptionsInternal : ISettable<PresenceModificationDeleteDataOptions>, IDisposable
	{
		// Token: 0x1700043F RID: 1087
		// (set) Token: 0x0600103C RID: 4156 RVA: 0x00018224 File Offset: 0x00016424
		public PresenceModificationDataRecordId[] Records
		{
			set
			{
				Helper.Set<PresenceModificationDataRecordId, PresenceModificationDataRecordIdInternal>(ref value, ref this.m_Records, out this.m_RecordsCount);
			}
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x0001823B File Offset: 0x0001643B
		public void Set(ref PresenceModificationDeleteDataOptions other)
		{
			this.m_ApiVersion = 1;
			this.Records = other.Records;
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x00018254 File Offset: 0x00016454
		public void Set(ref PresenceModificationDeleteDataOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.Records = other.Value.Records;
			}
		}

		// Token: 0x0600103F RID: 4159 RVA: 0x0001828A File Offset: 0x0001648A
		public void Dispose()
		{
			Helper.Dispose(ref this.m_Records);
		}

		// Token: 0x04000745 RID: 1861
		private int m_ApiVersion;

		// Token: 0x04000746 RID: 1862
		private int m_RecordsCount;

		// Token: 0x04000747 RID: 1863
		private IntPtr m_Records;
	}
}
