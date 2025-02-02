using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x0200026A RID: 618
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DeleteFileOptionsInternal : ISettable<DeleteFileOptions>, IDisposable
	{
		// Token: 0x1700047A RID: 1146
		// (set) Token: 0x060010DE RID: 4318 RVA: 0x00019008 File Offset: 0x00017208
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x1700047B RID: 1147
		// (set) Token: 0x060010DF RID: 4319 RVA: 0x00019018 File Offset: 0x00017218
		public Utf8String Filename
		{
			set
			{
				Helper.Set(value, ref this.m_Filename);
			}
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x00019028 File Offset: 0x00017228
		public void Set(ref DeleteFileOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.Filename = other.Filename;
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x0001904C File Offset: 0x0001724C
		public void Set(ref DeleteFileOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.Filename = other.Value.Filename;
			}
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x00019097 File Offset: 0x00017297
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_Filename);
		}

		// Token: 0x0400078E RID: 1934
		private int m_ApiVersion;

		// Token: 0x0400078F RID: 1935
		private IntPtr m_LocalUserId;

		// Token: 0x04000790 RID: 1936
		private IntPtr m_Filename;
	}
}
