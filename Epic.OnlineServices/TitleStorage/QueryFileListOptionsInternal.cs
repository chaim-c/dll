using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x02000099 RID: 153
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryFileListOptionsInternal : ISettable<QueryFileListOptions>, IDisposable
	{
		// Token: 0x170000EC RID: 236
		// (set) Token: 0x060005BF RID: 1471 RVA: 0x000086C1 File Offset: 0x000068C1
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x170000ED RID: 237
		// (set) Token: 0x060005C0 RID: 1472 RVA: 0x000086D1 File Offset: 0x000068D1
		public Utf8String[] ListOfTags
		{
			set
			{
				Helper.Set<Utf8String>(value, ref this.m_ListOfTags, out this.m_ListOfTagsCount);
			}
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x000086E7 File Offset: 0x000068E7
		public void Set(ref QueryFileListOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.ListOfTags = other.ListOfTags;
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x0000870C File Offset: 0x0000690C
		public void Set(ref QueryFileListOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.ListOfTags = other.Value.ListOfTags;
			}
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x00008757 File Offset: 0x00006957
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_ListOfTags);
		}

		// Token: 0x040002BA RID: 698
		private int m_ApiVersion;

		// Token: 0x040002BB RID: 699
		private IntPtr m_LocalUserId;

		// Token: 0x040002BC RID: 700
		private IntPtr m_ListOfTags;

		// Token: 0x040002BD RID: 701
		private uint m_ListOfTagsCount;
	}
}
