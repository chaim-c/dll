using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Stats
{
	// Token: 0x020000AC RID: 172
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct IngestDataInternal : IGettable<IngestData>, ISettable<IngestData>, IDisposable
	{
		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000647 RID: 1607 RVA: 0x00009700 File Offset: 0x00007900
		// (set) Token: 0x06000648 RID: 1608 RVA: 0x00009721 File Offset: 0x00007921
		public Utf8String StatName
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_StatName, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_StatName);
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000649 RID: 1609 RVA: 0x00009734 File Offset: 0x00007934
		// (set) Token: 0x0600064A RID: 1610 RVA: 0x0000974C File Offset: 0x0000794C
		public int IngestAmount
		{
			get
			{
				return this.m_IngestAmount;
			}
			set
			{
				this.m_IngestAmount = value;
			}
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x00009756 File Offset: 0x00007956
		public void Set(ref IngestData other)
		{
			this.m_ApiVersion = 1;
			this.StatName = other.StatName;
			this.IngestAmount = other.IngestAmount;
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x0000977C File Offset: 0x0000797C
		public void Set(ref IngestData? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.StatName = other.Value.StatName;
				this.IngestAmount = other.Value.IngestAmount;
			}
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x000097C7 File Offset: 0x000079C7
		public void Dispose()
		{
			Helper.Dispose(ref this.m_StatName);
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x000097D6 File Offset: 0x000079D6
		public void Get(out IngestData output)
		{
			output = default(IngestData);
			output.Set(ref this);
		}

		// Token: 0x04000308 RID: 776
		private int m_ApiVersion;

		// Token: 0x04000309 RID: 777
		private IntPtr m_StatName;

		// Token: 0x0400030A RID: 778
		private int m_IngestAmount;
	}
}
