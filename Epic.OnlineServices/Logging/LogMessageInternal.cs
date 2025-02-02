using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Logging
{
	// Token: 0x0200031B RID: 795
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LogMessageInternal : IGettable<LogMessage>, ISettable<LogMessage>, IDisposable
	{
		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x0600154D RID: 5453 RVA: 0x0001FA30 File Offset: 0x0001DC30
		// (set) Token: 0x0600154E RID: 5454 RVA: 0x0001FA51 File Offset: 0x0001DC51
		public Utf8String Category
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_Category, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_Category);
			}
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x0600154F RID: 5455 RVA: 0x0001FA64 File Offset: 0x0001DC64
		// (set) Token: 0x06001550 RID: 5456 RVA: 0x0001FA85 File Offset: 0x0001DC85
		public Utf8String Message
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_Message, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_Message);
			}
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06001551 RID: 5457 RVA: 0x0001FA98 File Offset: 0x0001DC98
		// (set) Token: 0x06001552 RID: 5458 RVA: 0x0001FAB0 File Offset: 0x0001DCB0
		public LogLevel Level
		{
			get
			{
				return this.m_Level;
			}
			set
			{
				this.m_Level = value;
			}
		}

		// Token: 0x06001553 RID: 5459 RVA: 0x0001FABA File Offset: 0x0001DCBA
		public void Set(ref LogMessage other)
		{
			this.Category = other.Category;
			this.Message = other.Message;
			this.Level = other.Level;
		}

		// Token: 0x06001554 RID: 5460 RVA: 0x0001FAE4 File Offset: 0x0001DCE4
		public void Set(ref LogMessage? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.Category = other.Value.Category;
				this.Message = other.Value.Message;
				this.Level = other.Value.Level;
			}
		}

		// Token: 0x06001555 RID: 5461 RVA: 0x0001FB3D File Offset: 0x0001DD3D
		public void Dispose()
		{
			Helper.Dispose(ref this.m_Category);
			Helper.Dispose(ref this.m_Message);
		}

		// Token: 0x06001556 RID: 5462 RVA: 0x0001FB58 File Offset: 0x0001DD58
		public void Get(out LogMessage output)
		{
			output = default(LogMessage);
			output.Set(ref this);
		}

		// Token: 0x040009B1 RID: 2481
		private IntPtr m_Category;

		// Token: 0x040009B2 RID: 2482
		private IntPtr m_Message;

		// Token: 0x040009B3 RID: 2483
		private LogLevel m_Level;
	}
}
