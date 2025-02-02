using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x0200028E RID: 654
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryFileListCallbackInfoInternal : ICallbackInfoInternal, IGettable<QueryFileListCallbackInfo>, ISettable<QueryFileListCallbackInfo>, IDisposable
	{
		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x060011C5 RID: 4549 RVA: 0x0001A278 File Offset: 0x00018478
		// (set) Token: 0x060011C6 RID: 4550 RVA: 0x0001A290 File Offset: 0x00018490
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

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x060011C7 RID: 4551 RVA: 0x0001A29C File Offset: 0x0001849C
		// (set) Token: 0x060011C8 RID: 4552 RVA: 0x0001A2BD File Offset: 0x000184BD
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

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x060011C9 RID: 4553 RVA: 0x0001A2D0 File Offset: 0x000184D0
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x060011CA RID: 4554 RVA: 0x0001A2E8 File Offset: 0x000184E8
		// (set) Token: 0x060011CB RID: 4555 RVA: 0x0001A309 File Offset: 0x00018509
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

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x060011CC RID: 4556 RVA: 0x0001A31C File Offset: 0x0001851C
		// (set) Token: 0x060011CD RID: 4557 RVA: 0x0001A334 File Offset: 0x00018534
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

		// Token: 0x060011CE RID: 4558 RVA: 0x0001A33E File Offset: 0x0001853E
		public void Set(ref QueryFileListCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.FileCount = other.FileCount;
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x0001A378 File Offset: 0x00018578
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

		// Token: 0x060011D0 RID: 4560 RVA: 0x0001A3E6 File Offset: 0x000185E6
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x0001A401 File Offset: 0x00018601
		public void Get(out QueryFileListCallbackInfo output)
		{
			output = default(QueryFileListCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x040007D7 RID: 2007
		private Result m_ResultCode;

		// Token: 0x040007D8 RID: 2008
		private IntPtr m_ClientData;

		// Token: 0x040007D9 RID: 2009
		private IntPtr m_LocalUserId;

		// Token: 0x040007DA RID: 2010
		private uint m_FileCount;
	}
}
