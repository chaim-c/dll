using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000156 RID: 342
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionSearchSetTargetUserIdOptionsInternal : ISettable<SessionSearchSetTargetUserIdOptions>, IDisposable
	{
		// Token: 0x17000229 RID: 553
		// (set) Token: 0x060009BF RID: 2495 RVA: 0x0000E28A File Offset: 0x0000C48A
		public ProductUserId TargetUserId
		{
			set
			{
				Helper.Set(value, ref this.m_TargetUserId);
			}
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x0000E29A File Offset: 0x0000C49A
		public void Set(ref SessionSearchSetTargetUserIdOptions other)
		{
			this.m_ApiVersion = 1;
			this.TargetUserId = other.TargetUserId;
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x0000E2B4 File Offset: 0x0000C4B4
		public void Set(ref SessionSearchSetTargetUserIdOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.TargetUserId = other.Value.TargetUserId;
			}
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x0000E2EA File Offset: 0x0000C4EA
		public void Dispose()
		{
			Helper.Dispose(ref this.m_TargetUserId);
		}

		// Token: 0x04000479 RID: 1145
		private int m_ApiVersion;

		// Token: 0x0400047A RID: 1146
		private IntPtr m_TargetUserId;
	}
}
