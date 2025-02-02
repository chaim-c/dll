using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices
{
	// Token: 0x0200001F RID: 31
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct PageResultInternal : IGettable<PageResult>, ISettable<PageResult>, IDisposable
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x00004730 File Offset: 0x00002930
		// (set) Token: 0x060002E8 RID: 744 RVA: 0x00004748 File Offset: 0x00002948
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

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x00004754 File Offset: 0x00002954
		// (set) Token: 0x060002EA RID: 746 RVA: 0x0000476C File Offset: 0x0000296C
		public int Count
		{
			get
			{
				return this.m_Count;
			}
			set
			{
				this.m_Count = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060002EB RID: 747 RVA: 0x00004778 File Offset: 0x00002978
		// (set) Token: 0x060002EC RID: 748 RVA: 0x00004790 File Offset: 0x00002990
		public int TotalCount
		{
			get
			{
				return this.m_TotalCount;
			}
			set
			{
				this.m_TotalCount = value;
			}
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000479A File Offset: 0x0000299A
		public void Set(ref PageResult other)
		{
			this.StartIndex = other.StartIndex;
			this.Count = other.Count;
			this.TotalCount = other.TotalCount;
		}

		// Token: 0x060002EE RID: 750 RVA: 0x000047C4 File Offset: 0x000029C4
		public void Set(ref PageResult? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.StartIndex = other.Value.StartIndex;
				this.Count = other.Value.Count;
				this.TotalCount = other.Value.TotalCount;
			}
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000481D File Offset: 0x00002A1D
		public void Dispose()
		{
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00004820 File Offset: 0x00002A20
		public void Get(out PageResult output)
		{
			output = default(PageResult);
			output.Set(ref this);
		}

		// Token: 0x04000054 RID: 84
		private int m_StartIndex;

		// Token: 0x04000055 RID: 85
		private int m_Count;

		// Token: 0x04000056 RID: 86
		private int m_TotalCount;
	}
}
