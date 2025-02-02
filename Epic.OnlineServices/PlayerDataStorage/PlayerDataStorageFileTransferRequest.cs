using System;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000289 RID: 649
	public sealed class PlayerDataStorageFileTransferRequest : Handle
	{
		// Token: 0x0600118C RID: 4492 RVA: 0x0001997D File Offset: 0x00017B7D
		public PlayerDataStorageFileTransferRequest()
		{
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x00019987 File Offset: 0x00017B87
		public PlayerDataStorageFileTransferRequest(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x00019994 File Offset: 0x00017B94
		public Result CancelRequest()
		{
			return Bindings.EOS_PlayerDataStorageFileTransferRequest_CancelRequest(base.InnerHandle);
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x000199B4 File Offset: 0x00017BB4
		public Result GetFileRequestState()
		{
			return Bindings.EOS_PlayerDataStorageFileTransferRequest_GetFileRequestState(base.InnerHandle);
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x000199D4 File Offset: 0x00017BD4
		public Result GetFilename(out Utf8String outStringBuffer)
		{
			int num = 64;
			IntPtr intPtr = Helper.AddAllocation(num);
			Result result = Bindings.EOS_PlayerDataStorageFileTransferRequest_GetFilename(base.InnerHandle, (uint)num, intPtr, ref num);
			Helper.Get(intPtr, out outStringBuffer);
			Helper.Dispose(ref intPtr);
			return result;
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x00019A11 File Offset: 0x00017C11
		public void Release()
		{
			Bindings.EOS_PlayerDataStorageFileTransferRequest_Release(base.InnerHandle);
		}
	}
}
