using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x0200023B RID: 571
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct InfoInternal : IGettable<Info>, ISettable<Info>, IDisposable
	{
		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06000FBC RID: 4028 RVA: 0x000173A0 File Offset: 0x000155A0
		// (set) Token: 0x06000FBD RID: 4029 RVA: 0x000173B8 File Offset: 0x000155B8
		public Status Status
		{
			get
			{
				return this.m_Status;
			}
			set
			{
				this.m_Status = value;
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06000FBE RID: 4030 RVA: 0x000173C4 File Offset: 0x000155C4
		// (set) Token: 0x06000FBF RID: 4031 RVA: 0x000173E5 File Offset: 0x000155E5
		public EpicAccountId UserId
		{
			get
			{
				EpicAccountId result;
				Helper.Get<EpicAccountId>(this.m_UserId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_UserId);
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06000FC0 RID: 4032 RVA: 0x000173F8 File Offset: 0x000155F8
		// (set) Token: 0x06000FC1 RID: 4033 RVA: 0x00017419 File Offset: 0x00015619
		public Utf8String ProductId
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_ProductId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_ProductId);
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06000FC2 RID: 4034 RVA: 0x0001742C File Offset: 0x0001562C
		// (set) Token: 0x06000FC3 RID: 4035 RVA: 0x0001744D File Offset: 0x0001564D
		public Utf8String ProductVersion
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_ProductVersion, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_ProductVersion);
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06000FC4 RID: 4036 RVA: 0x00017460 File Offset: 0x00015660
		// (set) Token: 0x06000FC5 RID: 4037 RVA: 0x00017481 File Offset: 0x00015681
		public Utf8String Platform
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_Platform, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_Platform);
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06000FC6 RID: 4038 RVA: 0x00017494 File Offset: 0x00015694
		// (set) Token: 0x06000FC7 RID: 4039 RVA: 0x000174B5 File Offset: 0x000156B5
		public Utf8String RichText
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_RichText, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_RichText);
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06000FC8 RID: 4040 RVA: 0x000174C8 File Offset: 0x000156C8
		// (set) Token: 0x06000FC9 RID: 4041 RVA: 0x000174EF File Offset: 0x000156EF
		public DataRecord[] Records
		{
			get
			{
				DataRecord[] result;
				Helper.Get<DataRecordInternal, DataRecord>(this.m_Records, out result, this.m_RecordsCount);
				return result;
			}
			set
			{
				Helper.Set<DataRecord, DataRecordInternal>(ref value, ref this.m_Records, out this.m_RecordsCount);
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06000FCA RID: 4042 RVA: 0x00017508 File Offset: 0x00015708
		// (set) Token: 0x06000FCB RID: 4043 RVA: 0x00017529 File Offset: 0x00015729
		public Utf8String ProductName
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_ProductName, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_ProductName);
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06000FCC RID: 4044 RVA: 0x0001753C File Offset: 0x0001573C
		// (set) Token: 0x06000FCD RID: 4045 RVA: 0x0001755D File Offset: 0x0001575D
		public Utf8String IntegratedPlatform
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_IntegratedPlatform, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_IntegratedPlatform);
			}
		}

		// Token: 0x06000FCE RID: 4046 RVA: 0x00017570 File Offset: 0x00015770
		public void Set(ref Info other)
		{
			this.m_ApiVersion = 3;
			this.Status = other.Status;
			this.UserId = other.UserId;
			this.ProductId = other.ProductId;
			this.ProductVersion = other.ProductVersion;
			this.Platform = other.Platform;
			this.RichText = other.RichText;
			this.Records = other.Records;
			this.ProductName = other.ProductName;
			this.IntegratedPlatform = other.IntegratedPlatform;
		}

		// Token: 0x06000FCF RID: 4047 RVA: 0x000175FC File Offset: 0x000157FC
		public void Set(ref Info? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 3;
				this.Status = other.Value.Status;
				this.UserId = other.Value.UserId;
				this.ProductId = other.Value.ProductId;
				this.ProductVersion = other.Value.ProductVersion;
				this.Platform = other.Value.Platform;
				this.RichText = other.Value.RichText;
				this.Records = other.Value.Records;
				this.ProductName = other.Value.ProductName;
				this.IntegratedPlatform = other.Value.IntegratedPlatform;
			}
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x000176E0 File Offset: 0x000158E0
		public void Dispose()
		{
			Helper.Dispose(ref this.m_UserId);
			Helper.Dispose(ref this.m_ProductId);
			Helper.Dispose(ref this.m_ProductVersion);
			Helper.Dispose(ref this.m_Platform);
			Helper.Dispose(ref this.m_RichText);
			Helper.Dispose(ref this.m_Records);
			Helper.Dispose(ref this.m_ProductName);
			Helper.Dispose(ref this.m_IntegratedPlatform);
		}

		// Token: 0x06000FD1 RID: 4049 RVA: 0x0001774E File Offset: 0x0001594E
		public void Get(out Info output)
		{
			output = default(Info);
			output.Set(ref this);
		}

		// Token: 0x0400070C RID: 1804
		private int m_ApiVersion;

		// Token: 0x0400070D RID: 1805
		private Status m_Status;

		// Token: 0x0400070E RID: 1806
		private IntPtr m_UserId;

		// Token: 0x0400070F RID: 1807
		private IntPtr m_ProductId;

		// Token: 0x04000710 RID: 1808
		private IntPtr m_ProductVersion;

		// Token: 0x04000711 RID: 1809
		private IntPtr m_Platform;

		// Token: 0x04000712 RID: 1810
		private IntPtr m_RichText;

		// Token: 0x04000713 RID: 1811
		private int m_RecordsCount;

		// Token: 0x04000714 RID: 1812
		private IntPtr m_Records;

		// Token: 0x04000715 RID: 1813
		private IntPtr m_ProductName;

		// Token: 0x04000716 RID: 1814
		private IntPtr m_IntegratedPlatform;
	}
}
