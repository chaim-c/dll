using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Reports
{
	// Token: 0x02000218 RID: 536
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SendPlayerBehaviorReportOptionsInternal : ISettable<SendPlayerBehaviorReportOptions>, IDisposable
	{
		// Token: 0x170003E5 RID: 997
		// (set) Token: 0x06000F0A RID: 3850 RVA: 0x0001645A File Offset: 0x0001465A
		public ProductUserId ReporterUserId
		{
			set
			{
				Helper.Set(value, ref this.m_ReporterUserId);
			}
		}

		// Token: 0x170003E6 RID: 998
		// (set) Token: 0x06000F0B RID: 3851 RVA: 0x0001646A File Offset: 0x0001466A
		public ProductUserId ReportedUserId
		{
			set
			{
				Helper.Set(value, ref this.m_ReportedUserId);
			}
		}

		// Token: 0x170003E7 RID: 999
		// (set) Token: 0x06000F0C RID: 3852 RVA: 0x0001647A File Offset: 0x0001467A
		public PlayerReportsCategory Category
		{
			set
			{
				this.m_Category = value;
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (set) Token: 0x06000F0D RID: 3853 RVA: 0x00016484 File Offset: 0x00014684
		public Utf8String Message
		{
			set
			{
				Helper.Set(value, ref this.m_Message);
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (set) Token: 0x06000F0E RID: 3854 RVA: 0x00016494 File Offset: 0x00014694
		public Utf8String Context
		{
			set
			{
				Helper.Set(value, ref this.m_Context);
			}
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x000164A4 File Offset: 0x000146A4
		public void Set(ref SendPlayerBehaviorReportOptions other)
		{
			this.m_ApiVersion = 2;
			this.ReporterUserId = other.ReporterUserId;
			this.ReportedUserId = other.ReportedUserId;
			this.Category = other.Category;
			this.Message = other.Message;
			this.Context = other.Context;
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x000164FC File Offset: 0x000146FC
		public void Set(ref SendPlayerBehaviorReportOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 2;
				this.ReporterUserId = other.Value.ReporterUserId;
				this.ReportedUserId = other.Value.ReportedUserId;
				this.Category = other.Value.Category;
				this.Message = other.Value.Message;
				this.Context = other.Value.Context;
			}
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x00016586 File Offset: 0x00014786
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ReporterUserId);
			Helper.Dispose(ref this.m_ReportedUserId);
			Helper.Dispose(ref this.m_Message);
			Helper.Dispose(ref this.m_Context);
		}

		// Token: 0x040006BF RID: 1727
		private int m_ApiVersion;

		// Token: 0x040006C0 RID: 1728
		private IntPtr m_ReporterUserId;

		// Token: 0x040006C1 RID: 1729
		private IntPtr m_ReportedUserId;

		// Token: 0x040006C2 RID: 1730
		private PlayerReportsCategory m_Category;

		// Token: 0x040006C3 RID: 1731
		private IntPtr m_Message;

		// Token: 0x040006C4 RID: 1732
		private IntPtr m_Context;
	}
}
