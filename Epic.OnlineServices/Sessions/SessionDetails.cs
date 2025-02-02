using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200011E RID: 286
	public sealed class SessionDetails : Handle
	{
		// Token: 0x060008A5 RID: 2213 RVA: 0x0000C6CB File Offset: 0x0000A8CB
		public SessionDetails()
		{
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x0000C6D5 File Offset: 0x0000A8D5
		public SessionDetails(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x0000C6E0 File Offset: 0x0000A8E0
		public Result CopyInfo(ref SessionDetailsCopyInfoOptions options, out SessionDetailsInfo? outSessionInfo)
		{
			SessionDetailsCopyInfoOptionsInternal sessionDetailsCopyInfoOptionsInternal = default(SessionDetailsCopyInfoOptionsInternal);
			sessionDetailsCopyInfoOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_SessionDetails_CopyInfo(base.InnerHandle, ref sessionDetailsCopyInfoOptionsInternal, ref zero);
			Helper.Dispose<SessionDetailsCopyInfoOptionsInternal>(ref sessionDetailsCopyInfoOptionsInternal);
			Helper.Get<SessionDetailsInfoInternal, SessionDetailsInfo>(zero, out outSessionInfo);
			bool flag = outSessionInfo != null;
			if (flag)
			{
				Bindings.EOS_SessionDetails_Info_Release(zero);
			}
			return result;
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x0000C740 File Offset: 0x0000A940
		public Result CopySessionAttributeByIndex(ref SessionDetailsCopySessionAttributeByIndexOptions options, out SessionDetailsAttribute? outSessionAttribute)
		{
			SessionDetailsCopySessionAttributeByIndexOptionsInternal sessionDetailsCopySessionAttributeByIndexOptionsInternal = default(SessionDetailsCopySessionAttributeByIndexOptionsInternal);
			sessionDetailsCopySessionAttributeByIndexOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_SessionDetails_CopySessionAttributeByIndex(base.InnerHandle, ref sessionDetailsCopySessionAttributeByIndexOptionsInternal, ref zero);
			Helper.Dispose<SessionDetailsCopySessionAttributeByIndexOptionsInternal>(ref sessionDetailsCopySessionAttributeByIndexOptionsInternal);
			Helper.Get<SessionDetailsAttributeInternal, SessionDetailsAttribute>(zero, out outSessionAttribute);
			bool flag = outSessionAttribute != null;
			if (flag)
			{
				Bindings.EOS_SessionDetails_Attribute_Release(zero);
			}
			return result;
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x0000C7A0 File Offset: 0x0000A9A0
		public Result CopySessionAttributeByKey(ref SessionDetailsCopySessionAttributeByKeyOptions options, out SessionDetailsAttribute? outSessionAttribute)
		{
			SessionDetailsCopySessionAttributeByKeyOptionsInternal sessionDetailsCopySessionAttributeByKeyOptionsInternal = default(SessionDetailsCopySessionAttributeByKeyOptionsInternal);
			sessionDetailsCopySessionAttributeByKeyOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_SessionDetails_CopySessionAttributeByKey(base.InnerHandle, ref sessionDetailsCopySessionAttributeByKeyOptionsInternal, ref zero);
			Helper.Dispose<SessionDetailsCopySessionAttributeByKeyOptionsInternal>(ref sessionDetailsCopySessionAttributeByKeyOptionsInternal);
			Helper.Get<SessionDetailsAttributeInternal, SessionDetailsAttribute>(zero, out outSessionAttribute);
			bool flag = outSessionAttribute != null;
			if (flag)
			{
				Bindings.EOS_SessionDetails_Attribute_Release(zero);
			}
			return result;
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x0000C800 File Offset: 0x0000AA00
		public uint GetSessionAttributeCount(ref SessionDetailsGetSessionAttributeCountOptions options)
		{
			SessionDetailsGetSessionAttributeCountOptionsInternal sessionDetailsGetSessionAttributeCountOptionsInternal = default(SessionDetailsGetSessionAttributeCountOptionsInternal);
			sessionDetailsGetSessionAttributeCountOptionsInternal.Set(ref options);
			uint result = Bindings.EOS_SessionDetails_GetSessionAttributeCount(base.InnerHandle, ref sessionDetailsGetSessionAttributeCountOptionsInternal);
			Helper.Dispose<SessionDetailsGetSessionAttributeCountOptionsInternal>(ref sessionDetailsGetSessionAttributeCountOptionsInternal);
			return result;
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x0000C83A File Offset: 0x0000AA3A
		public void Release()
		{
			Bindings.EOS_SessionDetails_Release(base.InnerHandle);
		}

		// Token: 0x040003F3 RID: 1011
		public const int SessiondetailsAttributeApiLatest = 1;

		// Token: 0x040003F4 RID: 1012
		public const int SessiondetailsCopyinfoApiLatest = 1;

		// Token: 0x040003F5 RID: 1013
		public const int SessiondetailsCopysessionattributebyindexApiLatest = 1;

		// Token: 0x040003F6 RID: 1014
		public const int SessiondetailsCopysessionattributebykeyApiLatest = 1;

		// Token: 0x040003F7 RID: 1015
		public const int SessiondetailsGetsessionattributecountApiLatest = 1;

		// Token: 0x040003F8 RID: 1016
		public const int SessiondetailsInfoApiLatest = 1;

		// Token: 0x040003F9 RID: 1017
		public const int SessiondetailsSettingsApiLatest = 3;
	}
}
