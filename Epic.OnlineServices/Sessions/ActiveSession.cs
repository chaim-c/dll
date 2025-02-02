using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000BC RID: 188
	public sealed class ActiveSession : Handle
	{
		// Token: 0x060006C9 RID: 1737 RVA: 0x0000A37F File Offset: 0x0000857F
		public ActiveSession()
		{
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x0000A389 File Offset: 0x00008589
		public ActiveSession(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x0000A394 File Offset: 0x00008594
		public Result CopyInfo(ref ActiveSessionCopyInfoOptions options, out ActiveSessionInfo? outActiveSessionInfo)
		{
			ActiveSessionCopyInfoOptionsInternal activeSessionCopyInfoOptionsInternal = default(ActiveSessionCopyInfoOptionsInternal);
			activeSessionCopyInfoOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_ActiveSession_CopyInfo(base.InnerHandle, ref activeSessionCopyInfoOptionsInternal, ref zero);
			Helper.Dispose<ActiveSessionCopyInfoOptionsInternal>(ref activeSessionCopyInfoOptionsInternal);
			Helper.Get<ActiveSessionInfoInternal, ActiveSessionInfo>(zero, out outActiveSessionInfo);
			bool flag = outActiveSessionInfo != null;
			if (flag)
			{
				Bindings.EOS_ActiveSession_Info_Release(zero);
			}
			return result;
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x0000A3F4 File Offset: 0x000085F4
		public ProductUserId GetRegisteredPlayerByIndex(ref ActiveSessionGetRegisteredPlayerByIndexOptions options)
		{
			ActiveSessionGetRegisteredPlayerByIndexOptionsInternal activeSessionGetRegisteredPlayerByIndexOptionsInternal = default(ActiveSessionGetRegisteredPlayerByIndexOptionsInternal);
			activeSessionGetRegisteredPlayerByIndexOptionsInternal.Set(ref options);
			IntPtr from = Bindings.EOS_ActiveSession_GetRegisteredPlayerByIndex(base.InnerHandle, ref activeSessionGetRegisteredPlayerByIndexOptionsInternal);
			Helper.Dispose<ActiveSessionGetRegisteredPlayerByIndexOptionsInternal>(ref activeSessionGetRegisteredPlayerByIndexOptionsInternal);
			ProductUserId result;
			Helper.Get<ProductUserId>(from, out result);
			return result;
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x0000A438 File Offset: 0x00008638
		public uint GetRegisteredPlayerCount(ref ActiveSessionGetRegisteredPlayerCountOptions options)
		{
			ActiveSessionGetRegisteredPlayerCountOptionsInternal activeSessionGetRegisteredPlayerCountOptionsInternal = default(ActiveSessionGetRegisteredPlayerCountOptionsInternal);
			activeSessionGetRegisteredPlayerCountOptionsInternal.Set(ref options);
			uint result = Bindings.EOS_ActiveSession_GetRegisteredPlayerCount(base.InnerHandle, ref activeSessionGetRegisteredPlayerCountOptionsInternal);
			Helper.Dispose<ActiveSessionGetRegisteredPlayerCountOptionsInternal>(ref activeSessionGetRegisteredPlayerCountOptionsInternal);
			return result;
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x0000A472 File Offset: 0x00008672
		public void Release()
		{
			Bindings.EOS_ActiveSession_Release(base.InnerHandle);
		}

		// Token: 0x04000343 RID: 835
		public const int ActivesessionCopyinfoApiLatest = 1;

		// Token: 0x04000344 RID: 836
		public const int ActivesessionGetregisteredplayerbyindexApiLatest = 1;

		// Token: 0x04000345 RID: 837
		public const int ActivesessionGetregisteredplayercountApiLatest = 1;

		// Token: 0x04000346 RID: 838
		public const int ActivesessionInfoApiLatest = 1;
	}
}
