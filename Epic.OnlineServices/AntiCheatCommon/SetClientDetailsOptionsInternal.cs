using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x0200060B RID: 1547
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SetClientDetailsOptionsInternal : ISettable<SetClientDetailsOptions>, IDisposable
	{
		// Token: 0x17000BED RID: 3053
		// (set) Token: 0x060027C3 RID: 10179 RVA: 0x0003B428 File Offset: 0x00039628
		public IntPtr ClientHandle
		{
			set
			{
				this.m_ClientHandle = value;
			}
		}

		// Token: 0x17000BEE RID: 3054
		// (set) Token: 0x060027C4 RID: 10180 RVA: 0x0003B432 File Offset: 0x00039632
		public AntiCheatCommonClientFlags ClientFlags
		{
			set
			{
				this.m_ClientFlags = value;
			}
		}

		// Token: 0x17000BEF RID: 3055
		// (set) Token: 0x060027C5 RID: 10181 RVA: 0x0003B43C File Offset: 0x0003963C
		public AntiCheatCommonClientInput ClientInputMethod
		{
			set
			{
				this.m_ClientInputMethod = value;
			}
		}

		// Token: 0x060027C6 RID: 10182 RVA: 0x0003B446 File Offset: 0x00039646
		public void Set(ref SetClientDetailsOptions other)
		{
			this.m_ApiVersion = 1;
			this.ClientHandle = other.ClientHandle;
			this.ClientFlags = other.ClientFlags;
			this.ClientInputMethod = other.ClientInputMethod;
		}

		// Token: 0x060027C7 RID: 10183 RVA: 0x0003B478 File Offset: 0x00039678
		public void Set(ref SetClientDetailsOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.ClientHandle = other.Value.ClientHandle;
				this.ClientFlags = other.Value.ClientFlags;
				this.ClientInputMethod = other.Value.ClientInputMethod;
			}
		}

		// Token: 0x060027C8 RID: 10184 RVA: 0x0003B4D8 File Offset: 0x000396D8
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientHandle);
		}

		// Token: 0x040011DF RID: 4575
		private int m_ApiVersion;

		// Token: 0x040011E0 RID: 4576
		private IntPtr m_ClientHandle;

		// Token: 0x040011E1 RID: 4577
		private AntiCheatCommonClientFlags m_ClientFlags;

		// Token: 0x040011E2 RID: 4578
		private AntiCheatCommonClientInput m_ClientInputMethod;
	}
}
