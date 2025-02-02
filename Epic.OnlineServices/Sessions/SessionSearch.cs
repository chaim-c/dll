using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000142 RID: 322
	public sealed class SessionSearch : Handle
	{
		// Token: 0x0600096D RID: 2413 RVA: 0x0000DB49 File Offset: 0x0000BD49
		public SessionSearch()
		{
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x0000DB53 File Offset: 0x0000BD53
		public SessionSearch(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x0000DB60 File Offset: 0x0000BD60
		public Result CopySearchResultByIndex(ref SessionSearchCopySearchResultByIndexOptions options, out SessionDetails outSessionHandle)
		{
			SessionSearchCopySearchResultByIndexOptionsInternal sessionSearchCopySearchResultByIndexOptionsInternal = default(SessionSearchCopySearchResultByIndexOptionsInternal);
			sessionSearchCopySearchResultByIndexOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_SessionSearch_CopySearchResultByIndex(base.InnerHandle, ref sessionSearchCopySearchResultByIndexOptionsInternal, ref zero);
			Helper.Dispose<SessionSearchCopySearchResultByIndexOptionsInternal>(ref sessionSearchCopySearchResultByIndexOptionsInternal);
			Helper.Get<SessionDetails>(zero, out outSessionHandle);
			return result;
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x0000DBAC File Offset: 0x0000BDAC
		public void Find(ref SessionSearchFindOptions options, object clientData, SessionSearchOnFindCallback completionDelegate)
		{
			SessionSearchFindOptionsInternal sessionSearchFindOptionsInternal = default(SessionSearchFindOptionsInternal);
			sessionSearchFindOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			SessionSearchOnFindCallbackInternal sessionSearchOnFindCallbackInternal = new SessionSearchOnFindCallbackInternal(SessionSearch.OnFindCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, sessionSearchOnFindCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_SessionSearch_Find(base.InnerHandle, ref sessionSearchFindOptionsInternal, zero, sessionSearchOnFindCallbackInternal);
			Helper.Dispose<SessionSearchFindOptionsInternal>(ref sessionSearchFindOptionsInternal);
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x0000DC08 File Offset: 0x0000BE08
		public uint GetSearchResultCount(ref SessionSearchGetSearchResultCountOptions options)
		{
			SessionSearchGetSearchResultCountOptionsInternal sessionSearchGetSearchResultCountOptionsInternal = default(SessionSearchGetSearchResultCountOptionsInternal);
			sessionSearchGetSearchResultCountOptionsInternal.Set(ref options);
			uint result = Bindings.EOS_SessionSearch_GetSearchResultCount(base.InnerHandle, ref sessionSearchGetSearchResultCountOptionsInternal);
			Helper.Dispose<SessionSearchGetSearchResultCountOptionsInternal>(ref sessionSearchGetSearchResultCountOptionsInternal);
			return result;
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x0000DC42 File Offset: 0x0000BE42
		public void Release()
		{
			Bindings.EOS_SessionSearch_Release(base.InnerHandle);
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x0000DC54 File Offset: 0x0000BE54
		public Result RemoveParameter(ref SessionSearchRemoveParameterOptions options)
		{
			SessionSearchRemoveParameterOptionsInternal sessionSearchRemoveParameterOptionsInternal = default(SessionSearchRemoveParameterOptionsInternal);
			sessionSearchRemoveParameterOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_SessionSearch_RemoveParameter(base.InnerHandle, ref sessionSearchRemoveParameterOptionsInternal);
			Helper.Dispose<SessionSearchRemoveParameterOptionsInternal>(ref sessionSearchRemoveParameterOptionsInternal);
			return result;
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x0000DC90 File Offset: 0x0000BE90
		public Result SetMaxResults(ref SessionSearchSetMaxResultsOptions options)
		{
			SessionSearchSetMaxResultsOptionsInternal sessionSearchSetMaxResultsOptionsInternal = default(SessionSearchSetMaxResultsOptionsInternal);
			sessionSearchSetMaxResultsOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_SessionSearch_SetMaxResults(base.InnerHandle, ref sessionSearchSetMaxResultsOptionsInternal);
			Helper.Dispose<SessionSearchSetMaxResultsOptionsInternal>(ref sessionSearchSetMaxResultsOptionsInternal);
			return result;
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x0000DCCC File Offset: 0x0000BECC
		public Result SetParameter(ref SessionSearchSetParameterOptions options)
		{
			SessionSearchSetParameterOptionsInternal sessionSearchSetParameterOptionsInternal = default(SessionSearchSetParameterOptionsInternal);
			sessionSearchSetParameterOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_SessionSearch_SetParameter(base.InnerHandle, ref sessionSearchSetParameterOptionsInternal);
			Helper.Dispose<SessionSearchSetParameterOptionsInternal>(ref sessionSearchSetParameterOptionsInternal);
			return result;
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x0000DD08 File Offset: 0x0000BF08
		public Result SetSessionId(ref SessionSearchSetSessionIdOptions options)
		{
			SessionSearchSetSessionIdOptionsInternal sessionSearchSetSessionIdOptionsInternal = default(SessionSearchSetSessionIdOptionsInternal);
			sessionSearchSetSessionIdOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_SessionSearch_SetSessionId(base.InnerHandle, ref sessionSearchSetSessionIdOptionsInternal);
			Helper.Dispose<SessionSearchSetSessionIdOptionsInternal>(ref sessionSearchSetSessionIdOptionsInternal);
			return result;
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x0000DD44 File Offset: 0x0000BF44
		public Result SetTargetUserId(ref SessionSearchSetTargetUserIdOptions options)
		{
			SessionSearchSetTargetUserIdOptionsInternal sessionSearchSetTargetUserIdOptionsInternal = default(SessionSearchSetTargetUserIdOptionsInternal);
			sessionSearchSetTargetUserIdOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_SessionSearch_SetTargetUserId(base.InnerHandle, ref sessionSearchSetTargetUserIdOptionsInternal);
			Helper.Dispose<SessionSearchSetTargetUserIdOptionsInternal>(ref sessionSearchSetTargetUserIdOptionsInternal);
			return result;
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x0000DD80 File Offset: 0x0000BF80
		[MonoPInvokeCallback(typeof(SessionSearchOnFindCallbackInternal))]
		internal static void OnFindCallbackInternalImplementation(ref SessionSearchFindCallbackInfoInternal data)
		{
			SessionSearchOnFindCallback sessionSearchOnFindCallback;
			SessionSearchFindCallbackInfo sessionSearchFindCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<SessionSearchFindCallbackInfoInternal, SessionSearchOnFindCallback, SessionSearchFindCallbackInfo>(ref data, out sessionSearchOnFindCallback, out sessionSearchFindCallbackInfo);
			if (flag)
			{
				sessionSearchOnFindCallback(ref sessionSearchFindCallbackInfo);
			}
		}

		// Token: 0x04000455 RID: 1109
		public const int SessionsearchCopysearchresultbyindexApiLatest = 1;

		// Token: 0x04000456 RID: 1110
		public const int SessionsearchFindApiLatest = 2;

		// Token: 0x04000457 RID: 1111
		public const int SessionsearchGetsearchresultcountApiLatest = 1;

		// Token: 0x04000458 RID: 1112
		public const int SessionsearchRemoveparameterApiLatest = 1;

		// Token: 0x04000459 RID: 1113
		public const int SessionsearchSetmaxsearchresultsApiLatest = 1;

		// Token: 0x0400045A RID: 1114
		public const int SessionsearchSetparameterApiLatest = 1;

		// Token: 0x0400045B RID: 1115
		public const int SessionsearchSetsessionidApiLatest = 1;

		// Token: 0x0400045C RID: 1116
		public const int SessionsearchSettargetuseridApiLatest = 1;
	}
}
