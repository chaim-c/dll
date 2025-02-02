using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.ProgressionSnapshot
{
	// Token: 0x0200021A RID: 538
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddProgressionOptionsInternal : ISettable<AddProgressionOptions>, IDisposable
	{
		// Token: 0x170003ED RID: 1005
		// (set) Token: 0x06000F18 RID: 3864 RVA: 0x000165EC File Offset: 0x000147EC
		public uint SnapshotId
		{
			set
			{
				this.m_SnapshotId = value;
			}
		}

		// Token: 0x170003EE RID: 1006
		// (set) Token: 0x06000F19 RID: 3865 RVA: 0x000165F6 File Offset: 0x000147F6
		public Utf8String Key
		{
			set
			{
				Helper.Set(value, ref this.m_Key);
			}
		}

		// Token: 0x170003EF RID: 1007
		// (set) Token: 0x06000F1A RID: 3866 RVA: 0x00016606 File Offset: 0x00014806
		public Utf8String Value
		{
			set
			{
				Helper.Set(value, ref this.m_Value);
			}
		}

		// Token: 0x06000F1B RID: 3867 RVA: 0x00016616 File Offset: 0x00014816
		public void Set(ref AddProgressionOptions other)
		{
			this.m_ApiVersion = 1;
			this.SnapshotId = other.SnapshotId;
			this.Key = other.Key;
			this.Value = other.Value;
		}

		// Token: 0x06000F1C RID: 3868 RVA: 0x00016648 File Offset: 0x00014848
		public void Set(ref AddProgressionOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.SnapshotId = other.Value.SnapshotId;
				this.Key = other.Value.Key;
				this.Value = other.Value.Value;
			}
		}

		// Token: 0x06000F1D RID: 3869 RVA: 0x000166A8 File Offset: 0x000148A8
		public void Dispose()
		{
			Helper.Dispose(ref this.m_Key);
			Helper.Dispose(ref this.m_Value);
		}

		// Token: 0x040006C8 RID: 1736
		private int m_ApiVersion;

		// Token: 0x040006C9 RID: 1737
		private uint m_SnapshotId;

		// Token: 0x040006CA RID: 1738
		private IntPtr m_Key;

		// Token: 0x040006CB RID: 1739
		private IntPtr m_Value;
	}
}
