using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x02000097 RID: 151
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryFileListCallbackInfoInternal : ICallbackInfoInternal, IGettable<QueryFileListCallbackInfo>, ISettable<QueryFileListCallbackInfo>, IDisposable
	{
		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060005AE RID: 1454 RVA: 0x00008504 File Offset: 0x00006704
		// (set) Token: 0x060005AF RID: 1455 RVA: 0x0000851C File Offset: 0x0000671C
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

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x00008528 File Offset: 0x00006728
		// (set) Token: 0x060005B1 RID: 1457 RVA: 0x00008549 File Offset: 0x00006749
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

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x0000855C File Offset: 0x0000675C
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x00008574 File Offset: 0x00006774
		// (set) Token: 0x060005B4 RID: 1460 RVA: 0x00008595 File Offset: 0x00006795
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

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060005B5 RID: 1461 RVA: 0x000085A8 File Offset: 0x000067A8
		// (set) Token: 0x060005B6 RID: 1462 RVA: 0x000085C0 File Offset: 0x000067C0
		public uint FileCount
		{
			get
			{
				return this.m_FileCount;
			}
			set
			{
				this.m_FileCount = value;
			}
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x000085CA File Offset: 0x000067CA
		public void Set(ref QueryFileListCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.FileCount = other.FileCount;
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x00008604 File Offset: 0x00006804
		public void Set(ref QueryFileListCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.FileCount = other.Value.FileCount;
			}
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x00008672 File Offset: 0x00006872
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x0000868D File Offset: 0x0000688D
		public void Get(out QueryFileListCallbackInfo output)
		{
			output = default(QueryFileListCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x040002B4 RID: 692
		private Result m_ResultCode;

		// Token: 0x040002B5 RID: 693
		private IntPtr m_ClientData;

		// Token: 0x040002B6 RID: 694
		private IntPtr m_LocalUserId;

		// Token: 0x040002B7 RID: 695
		private uint m_FileCount;
	}
}
