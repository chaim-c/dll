using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Stats
{
	// Token: 0x020000BA RID: 186
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct StatInternal : IGettable<Stat>, ISettable<Stat>, IDisposable
	{
		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x00009FD4 File Offset: 0x000081D4
		// (set) Token: 0x060006B5 RID: 1717 RVA: 0x00009FF5 File Offset: 0x000081F5
		public Utf8String Name
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_Name, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_Name);
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060006B6 RID: 1718 RVA: 0x0000A008 File Offset: 0x00008208
		// (set) Token: 0x060006B7 RID: 1719 RVA: 0x0000A029 File Offset: 0x00008229
		public DateTimeOffset? StartTime
		{
			get
			{
				DateTimeOffset? result;
				Helper.Get(this.m_StartTime, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_StartTime);
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060006B8 RID: 1720 RVA: 0x0000A03C File Offset: 0x0000823C
		// (set) Token: 0x060006B9 RID: 1721 RVA: 0x0000A05D File Offset: 0x0000825D
		public DateTimeOffset? EndTime
		{
			get
			{
				DateTimeOffset? result;
				Helper.Get(this.m_EndTime, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_EndTime);
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060006BA RID: 1722 RVA: 0x0000A070 File Offset: 0x00008270
		// (set) Token: 0x060006BB RID: 1723 RVA: 0x0000A088 File Offset: 0x00008288
		public int Value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.m_Value = value;
			}
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x0000A092 File Offset: 0x00008292
		public void Set(ref Stat other)
		{
			this.m_ApiVersion = 1;
			this.Name = other.Name;
			this.StartTime = other.StartTime;
			this.EndTime = other.EndTime;
			this.Value = other.Value;
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x0000A0D0 File Offset: 0x000082D0
		public void Set(ref Stat? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.Name = other.Value.Name;
				this.StartTime = other.Value.StartTime;
				this.EndTime = other.Value.EndTime;
				this.Value = other.Value.Value;
			}
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x0000A145 File Offset: 0x00008345
		public void Dispose()
		{
			Helper.Dispose(ref this.m_Name);
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x0000A154 File Offset: 0x00008354
		public void Get(out Stat output)
		{
			output = default(Stat);
			output.Set(ref this);
		}

		// Token: 0x04000333 RID: 819
		private int m_ApiVersion;

		// Token: 0x04000334 RID: 820
		private IntPtr m_Name;

		// Token: 0x04000335 RID: 821
		private long m_StartTime;

		// Token: 0x04000336 RID: 822
		private long m_EndTime;

		// Token: 0x04000337 RID: 823
		private int m_Value;
	}
}
