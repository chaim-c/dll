using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000697 RID: 1687
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct PlayerStatInfoInternal : IGettable<PlayerStatInfo>, ISettable<PlayerStatInfo>, IDisposable
	{
		// Token: 0x17000D24 RID: 3364
		// (get) Token: 0x06002B64 RID: 11108 RVA: 0x00040F70 File Offset: 0x0003F170
		// (set) Token: 0x06002B65 RID: 11109 RVA: 0x00040F91 File Offset: 0x0003F191
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

		// Token: 0x17000D25 RID: 3365
		// (get) Token: 0x06002B66 RID: 11110 RVA: 0x00040FA4 File Offset: 0x0003F1A4
		// (set) Token: 0x06002B67 RID: 11111 RVA: 0x00040FBC File Offset: 0x0003F1BC
		public int CurrentValue
		{
			get
			{
				return this.m_CurrentValue;
			}
			set
			{
				this.m_CurrentValue = value;
			}
		}

		// Token: 0x17000D26 RID: 3366
		// (get) Token: 0x06002B68 RID: 11112 RVA: 0x00040FC8 File Offset: 0x0003F1C8
		// (set) Token: 0x06002B69 RID: 11113 RVA: 0x00040FE0 File Offset: 0x0003F1E0
		public int ThresholdValue
		{
			get
			{
				return this.m_ThresholdValue;
			}
			set
			{
				this.m_ThresholdValue = value;
			}
		}

		// Token: 0x06002B6A RID: 11114 RVA: 0x00040FEA File Offset: 0x0003F1EA
		public void Set(ref PlayerStatInfo other)
		{
			this.m_ApiVersion = 1;
			this.Name = other.Name;
			this.CurrentValue = other.CurrentValue;
			this.ThresholdValue = other.ThresholdValue;
		}

		// Token: 0x06002B6B RID: 11115 RVA: 0x0004101C File Offset: 0x0003F21C
		public void Set(ref PlayerStatInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.Name = other.Value.Name;
				this.CurrentValue = other.Value.CurrentValue;
				this.ThresholdValue = other.Value.ThresholdValue;
			}
		}

		// Token: 0x06002B6C RID: 11116 RVA: 0x0004107C File Offset: 0x0003F27C
		public void Dispose()
		{
			Helper.Dispose(ref this.m_Name);
		}

		// Token: 0x06002B6D RID: 11117 RVA: 0x0004108B File Offset: 0x0003F28B
		public void Get(out PlayerStatInfo output)
		{
			output = default(PlayerStatInfo);
			output.Set(ref this);
		}

		// Token: 0x040013AE RID: 5038
		private int m_ApiVersion;

		// Token: 0x040013AF RID: 5039
		private IntPtr m_Name;

		// Token: 0x040013B0 RID: 5040
		private int m_CurrentValue;

		// Token: 0x040013B1 RID: 5041
		private int m_ThresholdValue;
	}
}
