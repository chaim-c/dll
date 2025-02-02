using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UI
{
	// Token: 0x02000054 RID: 84
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct IsSocialOverlayPausedOptionsInternal : ISettable<IsSocialOverlayPausedOptions>, IDisposable
	{
		// Token: 0x0600042E RID: 1070 RVA: 0x00006729 File Offset: 0x00004929
		public void Set(ref IsSocialOverlayPausedOptions other)
		{
			this.m_ApiVersion = 1;
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x00006734 File Offset: 0x00004934
		public void Set(ref IsSocialOverlayPausedOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x00006755 File Offset: 0x00004955
		public void Dispose()
		{
		}

		// Token: 0x040001C2 RID: 450
		private int m_ApiVersion;
	}
}
