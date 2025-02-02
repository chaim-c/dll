using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000C6 RID: 198
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyJoinSessionAcceptedOptionsInternal : ISettable<AddNotifyJoinSessionAcceptedOptions>, IDisposable
	{
		// Token: 0x060006F0 RID: 1776 RVA: 0x0000A772 File Offset: 0x00008972
		public void Set(ref AddNotifyJoinSessionAcceptedOptions other)
		{
			this.m_ApiVersion = 1;
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x0000A77C File Offset: 0x0000897C
		public void Set(ref AddNotifyJoinSessionAcceptedOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x0000A79D File Offset: 0x0000899D
		public void Dispose()
		{
		}

		// Token: 0x04000355 RID: 853
		private int m_ApiVersion;
	}
}
