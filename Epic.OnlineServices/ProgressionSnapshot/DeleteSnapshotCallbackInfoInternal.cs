using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.ProgressionSnapshot
{
	// Token: 0x0200021E RID: 542
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DeleteSnapshotCallbackInfoInternal : ICallbackInfoInternal, IGettable<DeleteSnapshotCallbackInfo>, ISettable<DeleteSnapshotCallbackInfo>, IDisposable
	{
		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06000F2C RID: 3884 RVA: 0x000167BC File Offset: 0x000149BC
		// (set) Token: 0x06000F2D RID: 3885 RVA: 0x000167D4 File Offset: 0x000149D4
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

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06000F2E RID: 3886 RVA: 0x000167E0 File Offset: 0x000149E0
		// (set) Token: 0x06000F2F RID: 3887 RVA: 0x00016801 File Offset: 0x00014A01
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.Get<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06000F30 RID: 3888 RVA: 0x00016814 File Offset: 0x00014A14
		// (set) Token: 0x06000F31 RID: 3889 RVA: 0x00016835 File Offset: 0x00014A35
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

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06000F32 RID: 3890 RVA: 0x00016848 File Offset: 0x00014A48
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x06000F33 RID: 3891 RVA: 0x00016860 File Offset: 0x00014A60
		public void Set(ref DeleteSnapshotCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.LocalUserId = other.LocalUserId;
			this.ClientData = other.ClientData;
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x0001688C File Offset: 0x00014A8C
		public void Set(ref DeleteSnapshotCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.LocalUserId = other.Value.LocalUserId;
				this.ClientData = other.Value.ClientData;
			}
		}

		// Token: 0x06000F35 RID: 3893 RVA: 0x000168E5 File Offset: 0x00014AE5
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_ClientData);
		}

		// Token: 0x06000F36 RID: 3894 RVA: 0x00016900 File Offset: 0x00014B00
		public void Get(out DeleteSnapshotCallbackInfo output)
		{
			output = default(DeleteSnapshotCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x040006D2 RID: 1746
		private Result m_ResultCode;

		// Token: 0x040006D3 RID: 1747
		private IntPtr m_LocalUserId;

		// Token: 0x040006D4 RID: 1748
		private IntPtr m_ClientData;
	}
}
