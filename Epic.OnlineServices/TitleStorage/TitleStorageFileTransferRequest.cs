using System;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x020000A3 RID: 163
	public sealed class TitleStorageFileTransferRequest : Handle
	{
		// Token: 0x06000615 RID: 1557 RVA: 0x00009044 File Offset: 0x00007244
		public TitleStorageFileTransferRequest()
		{
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x0000904E File Offset: 0x0000724E
		public TitleStorageFileTransferRequest(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x0000905C File Offset: 0x0000725C
		public Result CancelRequest()
		{
			return Bindings.EOS_TitleStorageFileTransferRequest_CancelRequest(base.InnerHandle);
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x0000907C File Offset: 0x0000727C
		public Result GetFileRequestState()
		{
			return Bindings.EOS_TitleStorageFileTransferRequest_GetFileRequestState(base.InnerHandle);
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x0000909C File Offset: 0x0000729C
		public Result GetFilename(out Utf8String outStringBuffer)
		{
			int num = 64;
			IntPtr intPtr = Helper.AddAllocation(num);
			Result result = Bindings.EOS_TitleStorageFileTransferRequest_GetFilename(base.InnerHandle, (uint)num, intPtr, ref num);
			Helper.Get(intPtr, out outStringBuffer);
			Helper.Dispose(ref intPtr);
			return result;
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x000090D9 File Offset: 0x000072D9
		public void Release()
		{
			Bindings.EOS_TitleStorageFileTransferRequest_Release(base.InnerHandle);
		}
	}
}
