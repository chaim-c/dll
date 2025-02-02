using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UI
{
	// Token: 0x02000046 RID: 70
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AcknowledgeEventIdOptionsInternal : ISettable<AcknowledgeEventIdOptions>, IDisposable
	{
		// Token: 0x17000071 RID: 113
		// (set) Token: 0x060003FE RID: 1022 RVA: 0x000062F9 File Offset: 0x000044F9
		public ulong UiEventId
		{
			set
			{
				this.m_UiEventId = value;
			}
		}

		// Token: 0x17000072 RID: 114
		// (set) Token: 0x060003FF RID: 1023 RVA: 0x00006303 File Offset: 0x00004503
		public Result Result
		{
			set
			{
				this.m_Result = value;
			}
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0000630D File Offset: 0x0000450D
		public void Set(ref AcknowledgeEventIdOptions other)
		{
			this.m_ApiVersion = 1;
			this.UiEventId = other.UiEventId;
			this.Result = other.Result;
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00006334 File Offset: 0x00004534
		public void Set(ref AcknowledgeEventIdOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.UiEventId = other.Value.UiEventId;
				this.Result = other.Value.Result;
			}
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0000637F File Offset: 0x0000457F
		public void Dispose()
		{
		}

		// Token: 0x040001AE RID: 430
		private int m_ApiVersion;

		// Token: 0x040001AF RID: 431
		private ulong m_UiEventId;

		// Token: 0x040001B0 RID: 432
		private Result m_Result;
	}
}
