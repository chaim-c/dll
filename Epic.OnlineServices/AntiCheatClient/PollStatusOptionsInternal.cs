using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatClient
{
	// Token: 0x02000634 RID: 1588
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct PollStatusOptionsInternal : ISettable<PollStatusOptions>, IDisposable
	{
		// Token: 0x17000C0D RID: 3085
		// (set) Token: 0x0600286F RID: 10351 RVA: 0x0003C34E File Offset: 0x0003A54E
		public uint OutMessageLength
		{
			set
			{
				this.m_OutMessageLength = value;
			}
		}

		// Token: 0x06002870 RID: 10352 RVA: 0x0003C358 File Offset: 0x0003A558
		public void Set(ref PollStatusOptions other)
		{
			this.m_ApiVersion = 1;
			this.OutMessageLength = other.OutMessageLength;
		}

		// Token: 0x06002871 RID: 10353 RVA: 0x0003C370 File Offset: 0x0003A570
		public void Set(ref PollStatusOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.OutMessageLength = other.Value.OutMessageLength;
			}
		}

		// Token: 0x06002872 RID: 10354 RVA: 0x0003C3A6 File Offset: 0x0003A5A6
		public void Dispose()
		{
		}

		// Token: 0x04001231 RID: 4657
		private int m_ApiVersion;

		// Token: 0x04001232 RID: 4658
		private uint m_OutMessageLength;
	}
}
