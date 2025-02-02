using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.CustomInvites
{
	// Token: 0x020004FA RID: 1274
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct FinalizeInviteOptionsInternal : ISettable<FinalizeInviteOptions>, IDisposable
	{
		// Token: 0x1700098D RID: 2445
		// (set) Token: 0x060020BB RID: 8379 RVA: 0x00030B2F File Offset: 0x0002ED2F
		public ProductUserId TargetUserId
		{
			set
			{
				Helper.Set(value, ref this.m_TargetUserId);
			}
		}

		// Token: 0x1700098E RID: 2446
		// (set) Token: 0x060020BC RID: 8380 RVA: 0x00030B3F File Offset: 0x0002ED3F
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x1700098F RID: 2447
		// (set) Token: 0x060020BD RID: 8381 RVA: 0x00030B4F File Offset: 0x0002ED4F
		public Utf8String CustomInviteId
		{
			set
			{
				Helper.Set(value, ref this.m_CustomInviteId);
			}
		}

		// Token: 0x17000990 RID: 2448
		// (set) Token: 0x060020BE RID: 8382 RVA: 0x00030B5F File Offset: 0x0002ED5F
		public Result ProcessingResult
		{
			set
			{
				this.m_ProcessingResult = value;
			}
		}

		// Token: 0x060020BF RID: 8383 RVA: 0x00030B69 File Offset: 0x0002ED69
		public void Set(ref FinalizeInviteOptions other)
		{
			this.m_ApiVersion = 1;
			this.TargetUserId = other.TargetUserId;
			this.LocalUserId = other.LocalUserId;
			this.CustomInviteId = other.CustomInviteId;
			this.ProcessingResult = other.ProcessingResult;
		}

		// Token: 0x060020C0 RID: 8384 RVA: 0x00030BA8 File Offset: 0x0002EDA8
		public void Set(ref FinalizeInviteOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.TargetUserId = other.Value.TargetUserId;
				this.LocalUserId = other.Value.LocalUserId;
				this.CustomInviteId = other.Value.CustomInviteId;
				this.ProcessingResult = other.Value.ProcessingResult;
			}
		}

		// Token: 0x060020C1 RID: 8385 RVA: 0x00030C1D File Offset: 0x0002EE1D
		public void Dispose()
		{
			Helper.Dispose(ref this.m_TargetUserId);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_CustomInviteId);
		}

		// Token: 0x04000E97 RID: 3735
		private int m_ApiVersion;

		// Token: 0x04000E98 RID: 3736
		private IntPtr m_TargetUserId;

		// Token: 0x04000E99 RID: 3737
		private IntPtr m_LocalUserId;

		// Token: 0x04000E9A RID: 3738
		private IntPtr m_CustomInviteId;

		// Token: 0x04000E9B RID: 3739
		private Result m_ProcessingResult;
	}
}
