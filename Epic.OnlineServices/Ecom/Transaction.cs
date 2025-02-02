using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004EB RID: 1259
	public sealed class Transaction : Handle
	{
		// Token: 0x06002071 RID: 8305 RVA: 0x000302AA File Offset: 0x0002E4AA
		public Transaction()
		{
		}

		// Token: 0x06002072 RID: 8306 RVA: 0x000302B4 File Offset: 0x0002E4B4
		public Transaction(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06002073 RID: 8307 RVA: 0x000302C0 File Offset: 0x0002E4C0
		public Result CopyEntitlementByIndex(ref TransactionCopyEntitlementByIndexOptions options, out Entitlement? outEntitlement)
		{
			TransactionCopyEntitlementByIndexOptionsInternal transactionCopyEntitlementByIndexOptionsInternal = default(TransactionCopyEntitlementByIndexOptionsInternal);
			transactionCopyEntitlementByIndexOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_Ecom_Transaction_CopyEntitlementByIndex(base.InnerHandle, ref transactionCopyEntitlementByIndexOptionsInternal, ref zero);
			Helper.Dispose<TransactionCopyEntitlementByIndexOptionsInternal>(ref transactionCopyEntitlementByIndexOptionsInternal);
			Helper.Get<EntitlementInternal, Entitlement>(zero, out outEntitlement);
			bool flag = outEntitlement != null;
			if (flag)
			{
				Bindings.EOS_Ecom_Entitlement_Release(zero);
			}
			return result;
		}

		// Token: 0x06002074 RID: 8308 RVA: 0x00030320 File Offset: 0x0002E520
		public uint GetEntitlementsCount(ref TransactionGetEntitlementsCountOptions options)
		{
			TransactionGetEntitlementsCountOptionsInternal transactionGetEntitlementsCountOptionsInternal = default(TransactionGetEntitlementsCountOptionsInternal);
			transactionGetEntitlementsCountOptionsInternal.Set(ref options);
			uint result = Bindings.EOS_Ecom_Transaction_GetEntitlementsCount(base.InnerHandle, ref transactionGetEntitlementsCountOptionsInternal);
			Helper.Dispose<TransactionGetEntitlementsCountOptionsInternal>(ref transactionGetEntitlementsCountOptionsInternal);
			return result;
		}

		// Token: 0x06002075 RID: 8309 RVA: 0x0003035C File Offset: 0x0002E55C
		public Result GetTransactionId(out Utf8String outBuffer)
		{
			int size = 65;
			IntPtr intPtr = Helper.AddAllocation(size);
			Result result = Bindings.EOS_Ecom_Transaction_GetTransactionId(base.InnerHandle, intPtr, ref size);
			Helper.Get(intPtr, out outBuffer);
			Helper.Dispose(ref intPtr);
			return result;
		}

		// Token: 0x06002076 RID: 8310 RVA: 0x00030398 File Offset: 0x0002E598
		public void Release()
		{
			Bindings.EOS_Ecom_Transaction_Release(base.InnerHandle);
		}

		// Token: 0x04000E79 RID: 3705
		public const int TransactionCopyentitlementbyindexApiLatest = 1;

		// Token: 0x04000E7A RID: 3706
		public const int TransactionGetentitlementscountApiLatest = 1;
	}
}
