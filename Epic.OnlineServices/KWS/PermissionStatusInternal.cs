using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x02000440 RID: 1088
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct PermissionStatusInternal : IGettable<PermissionStatus>, ISettable<PermissionStatus>, IDisposable
	{
		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x06001BE0 RID: 7136 RVA: 0x000292A0 File Offset: 0x000274A0
		// (set) Token: 0x06001BE1 RID: 7137 RVA: 0x000292C1 File Offset: 0x000274C1
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

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x06001BE2 RID: 7138 RVA: 0x000292D4 File Offset: 0x000274D4
		// (set) Token: 0x06001BE3 RID: 7139 RVA: 0x000292EC File Offset: 0x000274EC
		public KWSPermissionStatus Status
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

		// Token: 0x06001BE4 RID: 7140 RVA: 0x000292F6 File Offset: 0x000274F6
		public void Set(ref PermissionStatus other)
		{
			this.m_ApiVersion = 1;
			this.Name = other.Name;
			this.Status = other.Status;
		}

		// Token: 0x06001BE5 RID: 7141 RVA: 0x0002931C File Offset: 0x0002751C
		public void Set(ref PermissionStatus? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.Name = other.Value.Name;
				this.Status = other.Value.Status;
			}
		}

		// Token: 0x06001BE6 RID: 7142 RVA: 0x00029367 File Offset: 0x00027567
		public void Dispose()
		{
			Helper.Dispose(ref this.m_Name);
		}

		// Token: 0x06001BE7 RID: 7143 RVA: 0x00029376 File Offset: 0x00027576
		public void Get(out PermissionStatus output)
		{
			output = default(PermissionStatus);
			output.Set(ref this);
		}

		// Token: 0x04000C5A RID: 3162
		private int m_ApiVersion;

		// Token: 0x04000C5B RID: 3163
		private IntPtr m_Name;

		// Token: 0x04000C5C RID: 3164
		private KWSPermissionStatus m_Status;
	}
}
