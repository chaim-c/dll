using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UI
{
	// Token: 0x02000070 RID: 112
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SetToggleFriendsKeyOptionsInternal : ISettable<SetToggleFriendsKeyOptions>, IDisposable
	{
		// Token: 0x170000A2 RID: 162
		// (set) Token: 0x060004BA RID: 1210 RVA: 0x0000701E File Offset: 0x0000521E
		public KeyCombination KeyCombination
		{
			set
			{
				this.m_KeyCombination = value;
			}
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x00007028 File Offset: 0x00005228
		public void Set(ref SetToggleFriendsKeyOptions other)
		{
			this.m_ApiVersion = 1;
			this.KeyCombination = other.KeyCombination;
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x00007040 File Offset: 0x00005240
		public void Set(ref SetToggleFriendsKeyOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.KeyCombination = other.Value.KeyCombination;
			}
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x00007076 File Offset: 0x00005276
		public void Dispose()
		{
		}

		// Token: 0x0400025B RID: 603
		private int m_ApiVersion;

		// Token: 0x0400025C RID: 604
		private KeyCombination m_KeyCombination;
	}
}
