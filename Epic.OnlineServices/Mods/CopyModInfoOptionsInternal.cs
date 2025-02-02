using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Mods
{
	// Token: 0x020002ED RID: 749
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyModInfoOptionsInternal : ISettable<CopyModInfoOptions>, IDisposable
	{
		// Token: 0x1700058A RID: 1418
		// (set) Token: 0x06001429 RID: 5161 RVA: 0x0001DDF9 File Offset: 0x0001BFF9
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x1700058B RID: 1419
		// (set) Token: 0x0600142A RID: 5162 RVA: 0x0001DE09 File Offset: 0x0001C009
		public ModEnumerationType Type
		{
			set
			{
				this.m_Type = value;
			}
		}

		// Token: 0x0600142B RID: 5163 RVA: 0x0001DE13 File Offset: 0x0001C013
		public void Set(ref CopyModInfoOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.Type = other.Type;
		}

		// Token: 0x0600142C RID: 5164 RVA: 0x0001DE38 File Offset: 0x0001C038
		public void Set(ref CopyModInfoOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.Type = other.Value.Type;
			}
		}

		// Token: 0x0600142D RID: 5165 RVA: 0x0001DE83 File Offset: 0x0001C083
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x0400090C RID: 2316
		private int m_ApiVersion;

		// Token: 0x0400090D RID: 2317
		private IntPtr m_LocalUserId;

		// Token: 0x0400090E RID: 2318
		private ModEnumerationType m_Type;
	}
}
