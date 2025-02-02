using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices
{
	// Token: 0x0200001D RID: 29
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct PageQueryInternal : IGettable<PageQuery>, ISettable<PageQuery>, IDisposable
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x00004604 File Offset: 0x00002804
		// (set) Token: 0x060002D9 RID: 729 RVA: 0x0000461C File Offset: 0x0000281C
		public int StartIndex
		{
			get
			{
				return this.m_StartIndex;
			}
			set
			{
				this.m_StartIndex = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060002DA RID: 730 RVA: 0x00004628 File Offset: 0x00002828
		// (set) Token: 0x060002DB RID: 731 RVA: 0x00004640 File Offset: 0x00002840
		public int MaxCount
		{
			get
			{
				return this.m_MaxCount;
			}
			set
			{
				this.m_MaxCount = value;
			}
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000464A File Offset: 0x0000284A
		public void Set(ref PageQuery other)
		{
			this.m_ApiVersion = 1;
			this.StartIndex = other.StartIndex;
			this.MaxCount = other.MaxCount;
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00004670 File Offset: 0x00002870
		public void Set(ref PageQuery? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.StartIndex = other.Value.StartIndex;
				this.MaxCount = other.Value.MaxCount;
			}
		}

		// Token: 0x060002DE RID: 734 RVA: 0x000046BB File Offset: 0x000028BB
		public void Dispose()
		{
		}

		// Token: 0x060002DF RID: 735 RVA: 0x000046BE File Offset: 0x000028BE
		public void Get(out PageQuery output)
		{
			output = default(PageQuery);
			output.Set(ref this);
		}

		// Token: 0x0400004E RID: 78
		private int m_ApiVersion;

		// Token: 0x0400004F RID: 79
		private int m_StartIndex;

		// Token: 0x04000050 RID: 80
		private int m_MaxCount;
	}
}
