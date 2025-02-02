using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x0200069D RID: 1693
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct StatThresholdsInternal : IGettable<StatThresholds>, ISettable<StatThresholds>, IDisposable
	{
		// Token: 0x17000D33 RID: 3379
		// (get) Token: 0x06002B88 RID: 11144 RVA: 0x000412D0 File Offset: 0x0003F4D0
		// (set) Token: 0x06002B89 RID: 11145 RVA: 0x000412F1 File Offset: 0x0003F4F1
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

		// Token: 0x17000D34 RID: 3380
		// (get) Token: 0x06002B8A RID: 11146 RVA: 0x00041304 File Offset: 0x0003F504
		// (set) Token: 0x06002B8B RID: 11147 RVA: 0x0004131C File Offset: 0x0003F51C
		public int Threshold
		{
			get
			{
				return this.m_Threshold;
			}
			set
			{
				this.m_Threshold = value;
			}
		}

		// Token: 0x06002B8C RID: 11148 RVA: 0x00041326 File Offset: 0x0003F526
		public void Set(ref StatThresholds other)
		{
			this.m_ApiVersion = 1;
			this.Name = other.Name;
			this.Threshold = other.Threshold;
		}

		// Token: 0x06002B8D RID: 11149 RVA: 0x0004134C File Offset: 0x0003F54C
		public void Set(ref StatThresholds? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.Name = other.Value.Name;
				this.Threshold = other.Value.Threshold;
			}
		}

		// Token: 0x06002B8E RID: 11150 RVA: 0x00041397 File Offset: 0x0003F597
		public void Dispose()
		{
			Helper.Dispose(ref this.m_Name);
		}

		// Token: 0x06002B8F RID: 11151 RVA: 0x000413A6 File Offset: 0x0003F5A6
		public void Get(out StatThresholds output)
		{
			output = default(StatThresholds);
			output.Set(ref this);
		}

		// Token: 0x040013C1 RID: 5057
		private int m_ApiVersion;

		// Token: 0x040013C2 RID: 5058
		private IntPtr m_Name;

		// Token: 0x040013C3 RID: 5059
		private int m_Threshold;
	}
}
