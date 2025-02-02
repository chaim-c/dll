using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000251 RID: 593
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct PresenceModificationSetRawRichTextOptionsInternal : ISettable<PresenceModificationSetRawRichTextOptions>, IDisposable
	{
		// Token: 0x17000445 RID: 1093
		// (set) Token: 0x0600104E RID: 4174 RVA: 0x000183AE File Offset: 0x000165AE
		public Utf8String RichText
		{
			set
			{
				Helper.Set(value, ref this.m_RichText);
			}
		}

		// Token: 0x0600104F RID: 4175 RVA: 0x000183BE File Offset: 0x000165BE
		public void Set(ref PresenceModificationSetRawRichTextOptions other)
		{
			this.m_ApiVersion = 1;
			this.RichText = other.RichText;
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x000183D8 File Offset: 0x000165D8
		public void Set(ref PresenceModificationSetRawRichTextOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.RichText = other.Value.RichText;
			}
		}

		// Token: 0x06001051 RID: 4177 RVA: 0x0001840E File Offset: 0x0001660E
		public void Dispose()
		{
			Helper.Dispose(ref this.m_RichText);
		}

		// Token: 0x04000750 RID: 1872
		private int m_ApiVersion;

		// Token: 0x04000751 RID: 1873
		private IntPtr m_RichText;
	}
}
