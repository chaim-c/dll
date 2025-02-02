using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x02000095 RID: 149
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryFileCallbackInfoInternal : ICallbackInfoInternal, IGettable<QueryFileCallbackInfo>, ISettable<QueryFileCallbackInfo>, IDisposable
	{
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000599 RID: 1433 RVA: 0x00008318 File Offset: 0x00006518
		// (set) Token: 0x0600059A RID: 1434 RVA: 0x00008330 File Offset: 0x00006530
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

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600059B RID: 1435 RVA: 0x0000833C File Offset: 0x0000653C
		// (set) Token: 0x0600059C RID: 1436 RVA: 0x0000835D File Offset: 0x0000655D
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

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x00008370 File Offset: 0x00006570
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x00008388 File Offset: 0x00006588
		// (set) Token: 0x0600059F RID: 1439 RVA: 0x000083A9 File Offset: 0x000065A9
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

		// Token: 0x060005A0 RID: 1440 RVA: 0x000083B9 File Offset: 0x000065B9
		public void Set(ref QueryFileCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x000083E4 File Offset: 0x000065E4
		public void Set(ref QueryFileCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x0000843D File Offset: 0x0000663D
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x00008458 File Offset: 0x00006658
		public void Get(out QueryFileCallbackInfo output)
		{
			output = default(QueryFileCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x040002AD RID: 685
		private Result m_ResultCode;

		// Token: 0x040002AE RID: 686
		private IntPtr m_ClientData;

		// Token: 0x040002AF RID: 687
		private IntPtr m_LocalUserId;
	}
}
