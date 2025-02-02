using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000235 RID: 565
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DataRecordInternal : IGettable<DataRecord>, ISettable<DataRecord>, IDisposable
	{
		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06000F8F RID: 3983 RVA: 0x00016FE4 File Offset: 0x000151E4
		// (set) Token: 0x06000F90 RID: 3984 RVA: 0x00017005 File Offset: 0x00015205
		public Utf8String Key
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_Key, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_Key);
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06000F91 RID: 3985 RVA: 0x00017018 File Offset: 0x00015218
		// (set) Token: 0x06000F92 RID: 3986 RVA: 0x00017039 File Offset: 0x00015239
		public Utf8String Value
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_Value, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_Value);
			}
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x00017049 File Offset: 0x00015249
		public void Set(ref DataRecord other)
		{
			this.m_ApiVersion = 1;
			this.Key = other.Key;
			this.Value = other.Value;
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x00017070 File Offset: 0x00015270
		public void Set(ref DataRecord? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.Key = other.Value.Key;
				this.Value = other.Value.Value;
			}
		}

		// Token: 0x06000F95 RID: 3989 RVA: 0x000170BB File Offset: 0x000152BB
		public void Dispose()
		{
			Helper.Dispose(ref this.m_Key);
			Helper.Dispose(ref this.m_Value);
		}

		// Token: 0x06000F96 RID: 3990 RVA: 0x000170D6 File Offset: 0x000152D6
		public void Get(out DataRecord output)
		{
			output = default(DataRecord);
			output.Set(ref this);
		}

		// Token: 0x040006F6 RID: 1782
		private int m_ApiVersion;

		// Token: 0x040006F7 RID: 1783
		private IntPtr m_Key;

		// Token: 0x040006F8 RID: 1784
		private IntPtr m_Value;
	}
}
