using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x0200007F RID: 127
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DeleteCacheCallbackInfoInternal : ICallbackInfoInternal, IGettable<DeleteCacheCallbackInfo>, ISettable<DeleteCacheCallbackInfo>, IDisposable
	{
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x00007B7C File Offset: 0x00005D7C
		// (set) Token: 0x0600051B RID: 1307 RVA: 0x00007B94 File Offset: 0x00005D94
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

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x00007BA0 File Offset: 0x00005DA0
		// (set) Token: 0x0600051D RID: 1309 RVA: 0x00007BC1 File Offset: 0x00005DC1
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

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600051E RID: 1310 RVA: 0x00007BD4 File Offset: 0x00005DD4
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600051F RID: 1311 RVA: 0x00007BEC File Offset: 0x00005DEC
		// (set) Token: 0x06000520 RID: 1312 RVA: 0x00007C0D File Offset: 0x00005E0D
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

		// Token: 0x06000521 RID: 1313 RVA: 0x00007C1D File Offset: 0x00005E1D
		public void Set(ref DeleteCacheCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x00007C48 File Offset: 0x00005E48
		public void Set(ref DeleteCacheCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x00007CA1 File Offset: 0x00005EA1
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00007CBC File Offset: 0x00005EBC
		public void Get(out DeleteCacheCallbackInfo output)
		{
			output = default(DeleteCacheCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x0400028E RID: 654
		private Result m_ResultCode;

		// Token: 0x0400028F RID: 655
		private IntPtr m_ClientData;

		// Token: 0x04000290 RID: 656
		private IntPtr m_LocalUserId;
	}
}
