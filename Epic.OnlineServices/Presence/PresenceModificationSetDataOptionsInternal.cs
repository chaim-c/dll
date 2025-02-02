using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x0200024D RID: 589
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct PresenceModificationSetDataOptionsInternal : ISettable<PresenceModificationSetDataOptions>, IDisposable
	{
		// Token: 0x17000441 RID: 1089
		// (set) Token: 0x06001042 RID: 4162 RVA: 0x000182AA File Offset: 0x000164AA
		public DataRecord[] Records
		{
			set
			{
				Helper.Set<DataRecord, DataRecordInternal>(ref value, ref this.m_Records, out this.m_RecordsCount);
			}
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x000182C1 File Offset: 0x000164C1
		public void Set(ref PresenceModificationSetDataOptions other)
		{
			this.m_ApiVersion = 1;
			this.Records = other.Records;
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x000182D8 File Offset: 0x000164D8
		public void Set(ref PresenceModificationSetDataOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.Records = other.Value.Records;
			}
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x0001830E File Offset: 0x0001650E
		public void Dispose()
		{
			Helper.Dispose(ref this.m_Records);
		}

		// Token: 0x04000749 RID: 1865
		private int m_ApiVersion;

		// Token: 0x0400074A RID: 1866
		private int m_RecordsCount;

		// Token: 0x0400074B RID: 1867
		private IntPtr m_Records;
	}
}
