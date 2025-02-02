using System;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000247 RID: 583
	public sealed class PresenceModification : Handle
	{
		// Token: 0x06001029 RID: 4137 RVA: 0x00018000 File Offset: 0x00016200
		public PresenceModification()
		{
		}

		// Token: 0x0600102A RID: 4138 RVA: 0x0001800A File Offset: 0x0001620A
		public PresenceModification(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x00018018 File Offset: 0x00016218
		public Result DeleteData(ref PresenceModificationDeleteDataOptions options)
		{
			PresenceModificationDeleteDataOptionsInternal presenceModificationDeleteDataOptionsInternal = default(PresenceModificationDeleteDataOptionsInternal);
			presenceModificationDeleteDataOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_PresenceModification_DeleteData(base.InnerHandle, ref presenceModificationDeleteDataOptionsInternal);
			Helper.Dispose<PresenceModificationDeleteDataOptionsInternal>(ref presenceModificationDeleteDataOptionsInternal);
			return result;
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x00018052 File Offset: 0x00016252
		public void Release()
		{
			Bindings.EOS_PresenceModification_Release(base.InnerHandle);
		}

		// Token: 0x0600102D RID: 4141 RVA: 0x00018064 File Offset: 0x00016264
		public Result SetData(ref PresenceModificationSetDataOptions options)
		{
			PresenceModificationSetDataOptionsInternal presenceModificationSetDataOptionsInternal = default(PresenceModificationSetDataOptionsInternal);
			presenceModificationSetDataOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_PresenceModification_SetData(base.InnerHandle, ref presenceModificationSetDataOptionsInternal);
			Helper.Dispose<PresenceModificationSetDataOptionsInternal>(ref presenceModificationSetDataOptionsInternal);
			return result;
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x000180A0 File Offset: 0x000162A0
		public Result SetJoinInfo(ref PresenceModificationSetJoinInfoOptions options)
		{
			PresenceModificationSetJoinInfoOptionsInternal presenceModificationSetJoinInfoOptionsInternal = default(PresenceModificationSetJoinInfoOptionsInternal);
			presenceModificationSetJoinInfoOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_PresenceModification_SetJoinInfo(base.InnerHandle, ref presenceModificationSetJoinInfoOptionsInternal);
			Helper.Dispose<PresenceModificationSetJoinInfoOptionsInternal>(ref presenceModificationSetJoinInfoOptionsInternal);
			return result;
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x000180DC File Offset: 0x000162DC
		public Result SetRawRichText(ref PresenceModificationSetRawRichTextOptions options)
		{
			PresenceModificationSetRawRichTextOptionsInternal presenceModificationSetRawRichTextOptionsInternal = default(PresenceModificationSetRawRichTextOptionsInternal);
			presenceModificationSetRawRichTextOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_PresenceModification_SetRawRichText(base.InnerHandle, ref presenceModificationSetRawRichTextOptionsInternal);
			Helper.Dispose<PresenceModificationSetRawRichTextOptionsInternal>(ref presenceModificationSetRawRichTextOptionsInternal);
			return result;
		}

		// Token: 0x06001030 RID: 4144 RVA: 0x00018118 File Offset: 0x00016318
		public Result SetStatus(ref PresenceModificationSetStatusOptions options)
		{
			PresenceModificationSetStatusOptionsInternal presenceModificationSetStatusOptionsInternal = default(PresenceModificationSetStatusOptionsInternal);
			presenceModificationSetStatusOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_PresenceModification_SetStatus(base.InnerHandle, ref presenceModificationSetStatusOptionsInternal);
			Helper.Dispose<PresenceModificationSetStatusOptionsInternal>(ref presenceModificationSetStatusOptionsInternal);
			return result;
		}

		// Token: 0x0400073A RID: 1850
		public const int PresencemodificationDatarecordidApiLatest = 1;

		// Token: 0x0400073B RID: 1851
		public const int PresencemodificationDeletedataApiLatest = 1;

		// Token: 0x0400073C RID: 1852
		public const int PresencemodificationJoininfoMaxLength = 255;

		// Token: 0x0400073D RID: 1853
		public const int PresencemodificationSetdataApiLatest = 1;

		// Token: 0x0400073E RID: 1854
		public const int PresencemodificationSetjoininfoApiLatest = 1;

		// Token: 0x0400073F RID: 1855
		public const int PresencemodificationSetrawrichtextApiLatest = 1;

		// Token: 0x04000740 RID: 1856
		public const int PresencemodificationSetstatusApiLatest = 1;
	}
}
