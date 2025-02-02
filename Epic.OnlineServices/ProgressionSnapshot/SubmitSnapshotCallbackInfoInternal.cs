using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.ProgressionSnapshot
{
	// Token: 0x02000229 RID: 553
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SubmitSnapshotCallbackInfoInternal : ICallbackInfoInternal, IGettable<SubmitSnapshotCallbackInfo>, ISettable<SubmitSnapshotCallbackInfo>, IDisposable
	{
		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06000F64 RID: 3940 RVA: 0x00016C54 File Offset: 0x00014E54
		// (set) Token: 0x06000F65 RID: 3941 RVA: 0x00016C6C File Offset: 0x00014E6C
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
			set
			{
				this.m_ResultCode = value;
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06000F66 RID: 3942 RVA: 0x00016C78 File Offset: 0x00014E78
		// (set) Token: 0x06000F67 RID: 3943 RVA: 0x00016C90 File Offset: 0x00014E90
		public uint SnapshotId
		{
			get
			{
				return this.m_SnapshotId;
			}
			set
			{
				this.m_SnapshotId = value;
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06000F68 RID: 3944 RVA: 0x00016C9C File Offset: 0x00014E9C
		// (set) Token: 0x06000F69 RID: 3945 RVA: 0x00016CBD File Offset: 0x00014EBD
		public object ClientData
		{
			get
			{
				object result;
				Helper.Get(this.m_ClientData, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_ClientData);
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06000F6A RID: 3946 RVA: 0x00016CD0 File Offset: 0x00014ED0
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x00016CE8 File Offset: 0x00014EE8
		public void Set(ref SubmitSnapshotCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.SnapshotId = other.SnapshotId;
			this.ClientData = other.ClientData;
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x00016D14 File Offset: 0x00014F14
		public void Set(ref SubmitSnapshotCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.SnapshotId = other.Value.SnapshotId;
				this.ClientData = other.Value.ClientData;
			}
		}

		// Token: 0x06000F6D RID: 3949 RVA: 0x00016D6D File Offset: 0x00014F6D
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
		}

		// Token: 0x06000F6E RID: 3950 RVA: 0x00016D7C File Offset: 0x00014F7C
		public void Get(out SubmitSnapshotCallbackInfo output)
		{
			output = default(SubmitSnapshotCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x040006E4 RID: 1764
		private Result m_ResultCode;

		// Token: 0x040006E5 RID: 1765
		private uint m_SnapshotId;

		// Token: 0x040006E6 RID: 1766
		private IntPtr m_ClientData;
	}
}
