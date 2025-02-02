using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatClient
{
	// Token: 0x02000611 RID: 1553
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddExternalIntegrityCatalogOptionsInternal : ISettable<AddExternalIntegrityCatalogOptions>, IDisposable
	{
		// Token: 0x17000BF9 RID: 3065
		// (set) Token: 0x060027E2 RID: 10210 RVA: 0x0003B6D7 File Offset: 0x000398D7
		public Utf8String PathToBinFile
		{
			set
			{
				Helper.Set(value, ref this.m_PathToBinFile);
			}
		}

		// Token: 0x060027E3 RID: 10211 RVA: 0x0003B6E7 File Offset: 0x000398E7
		public void Set(ref AddExternalIntegrityCatalogOptions other)
		{
			this.m_ApiVersion = 1;
			this.PathToBinFile = other.PathToBinFile;
		}

		// Token: 0x060027E4 RID: 10212 RVA: 0x0003B700 File Offset: 0x00039900
		public void Set(ref AddExternalIntegrityCatalogOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.PathToBinFile = other.Value.PathToBinFile;
			}
		}

		// Token: 0x060027E5 RID: 10213 RVA: 0x0003B736 File Offset: 0x00039936
		public void Dispose()
		{
			Helper.Dispose(ref this.m_PathToBinFile);
		}

		// Token: 0x040011ED RID: 4589
		private int m_ApiVersion;

		// Token: 0x040011EE RID: 4590
		private IntPtr m_PathToBinFile;
	}
}
