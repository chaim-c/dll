using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200039A RID: 922
	public sealed class LobbySearch : Handle
	{
		// Token: 0x0600186B RID: 6251 RVA: 0x00025161 File Offset: 0x00023361
		public LobbySearch()
		{
		}

		// Token: 0x0600186C RID: 6252 RVA: 0x0002516B File Offset: 0x0002336B
		public LobbySearch(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x0600186D RID: 6253 RVA: 0x00025178 File Offset: 0x00023378
		public Result CopySearchResultByIndex(ref LobbySearchCopySearchResultByIndexOptions options, out LobbyDetails outLobbyDetailsHandle)
		{
			LobbySearchCopySearchResultByIndexOptionsInternal lobbySearchCopySearchResultByIndexOptionsInternal = default(LobbySearchCopySearchResultByIndexOptionsInternal);
			lobbySearchCopySearchResultByIndexOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_LobbySearch_CopySearchResultByIndex(base.InnerHandle, ref lobbySearchCopySearchResultByIndexOptionsInternal, ref zero);
			Helper.Dispose<LobbySearchCopySearchResultByIndexOptionsInternal>(ref lobbySearchCopySearchResultByIndexOptionsInternal);
			Helper.Get<LobbyDetails>(zero, out outLobbyDetailsHandle);
			return result;
		}

		// Token: 0x0600186E RID: 6254 RVA: 0x000251C4 File Offset: 0x000233C4
		public void Find(ref LobbySearchFindOptions options, object clientData, LobbySearchOnFindCallback completionDelegate)
		{
			LobbySearchFindOptionsInternal lobbySearchFindOptionsInternal = default(LobbySearchFindOptionsInternal);
			lobbySearchFindOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			LobbySearchOnFindCallbackInternal lobbySearchOnFindCallbackInternal = new LobbySearchOnFindCallbackInternal(LobbySearch.OnFindCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, lobbySearchOnFindCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_LobbySearch_Find(base.InnerHandle, ref lobbySearchFindOptionsInternal, zero, lobbySearchOnFindCallbackInternal);
			Helper.Dispose<LobbySearchFindOptionsInternal>(ref lobbySearchFindOptionsInternal);
		}

		// Token: 0x0600186F RID: 6255 RVA: 0x00025220 File Offset: 0x00023420
		public uint GetSearchResultCount(ref LobbySearchGetSearchResultCountOptions options)
		{
			LobbySearchGetSearchResultCountOptionsInternal lobbySearchGetSearchResultCountOptionsInternal = default(LobbySearchGetSearchResultCountOptionsInternal);
			lobbySearchGetSearchResultCountOptionsInternal.Set(ref options);
			uint result = Bindings.EOS_LobbySearch_GetSearchResultCount(base.InnerHandle, ref lobbySearchGetSearchResultCountOptionsInternal);
			Helper.Dispose<LobbySearchGetSearchResultCountOptionsInternal>(ref lobbySearchGetSearchResultCountOptionsInternal);
			return result;
		}

		// Token: 0x06001870 RID: 6256 RVA: 0x0002525A File Offset: 0x0002345A
		public void Release()
		{
			Bindings.EOS_LobbySearch_Release(base.InnerHandle);
		}

		// Token: 0x06001871 RID: 6257 RVA: 0x0002526C File Offset: 0x0002346C
		public Result RemoveParameter(ref LobbySearchRemoveParameterOptions options)
		{
			LobbySearchRemoveParameterOptionsInternal lobbySearchRemoveParameterOptionsInternal = default(LobbySearchRemoveParameterOptionsInternal);
			lobbySearchRemoveParameterOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_LobbySearch_RemoveParameter(base.InnerHandle, ref lobbySearchRemoveParameterOptionsInternal);
			Helper.Dispose<LobbySearchRemoveParameterOptionsInternal>(ref lobbySearchRemoveParameterOptionsInternal);
			return result;
		}

		// Token: 0x06001872 RID: 6258 RVA: 0x000252A8 File Offset: 0x000234A8
		public Result SetLobbyId(ref LobbySearchSetLobbyIdOptions options)
		{
			LobbySearchSetLobbyIdOptionsInternal lobbySearchSetLobbyIdOptionsInternal = default(LobbySearchSetLobbyIdOptionsInternal);
			lobbySearchSetLobbyIdOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_LobbySearch_SetLobbyId(base.InnerHandle, ref lobbySearchSetLobbyIdOptionsInternal);
			Helper.Dispose<LobbySearchSetLobbyIdOptionsInternal>(ref lobbySearchSetLobbyIdOptionsInternal);
			return result;
		}

		// Token: 0x06001873 RID: 6259 RVA: 0x000252E4 File Offset: 0x000234E4
		public Result SetMaxResults(ref LobbySearchSetMaxResultsOptions options)
		{
			LobbySearchSetMaxResultsOptionsInternal lobbySearchSetMaxResultsOptionsInternal = default(LobbySearchSetMaxResultsOptionsInternal);
			lobbySearchSetMaxResultsOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_LobbySearch_SetMaxResults(base.InnerHandle, ref lobbySearchSetMaxResultsOptionsInternal);
			Helper.Dispose<LobbySearchSetMaxResultsOptionsInternal>(ref lobbySearchSetMaxResultsOptionsInternal);
			return result;
		}

		// Token: 0x06001874 RID: 6260 RVA: 0x00025320 File Offset: 0x00023520
		public Result SetParameter(ref LobbySearchSetParameterOptions options)
		{
			LobbySearchSetParameterOptionsInternal lobbySearchSetParameterOptionsInternal = default(LobbySearchSetParameterOptionsInternal);
			lobbySearchSetParameterOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_LobbySearch_SetParameter(base.InnerHandle, ref lobbySearchSetParameterOptionsInternal);
			Helper.Dispose<LobbySearchSetParameterOptionsInternal>(ref lobbySearchSetParameterOptionsInternal);
			return result;
		}

		// Token: 0x06001875 RID: 6261 RVA: 0x0002535C File Offset: 0x0002355C
		public Result SetTargetUserId(ref LobbySearchSetTargetUserIdOptions options)
		{
			LobbySearchSetTargetUserIdOptionsInternal lobbySearchSetTargetUserIdOptionsInternal = default(LobbySearchSetTargetUserIdOptionsInternal);
			lobbySearchSetTargetUserIdOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_LobbySearch_SetTargetUserId(base.InnerHandle, ref lobbySearchSetTargetUserIdOptionsInternal);
			Helper.Dispose<LobbySearchSetTargetUserIdOptionsInternal>(ref lobbySearchSetTargetUserIdOptionsInternal);
			return result;
		}

		// Token: 0x06001876 RID: 6262 RVA: 0x00025398 File Offset: 0x00023598
		[MonoPInvokeCallback(typeof(LobbySearchOnFindCallbackInternal))]
		internal static void OnFindCallbackInternalImplementation(ref LobbySearchFindCallbackInfoInternal data)
		{
			LobbySearchOnFindCallback lobbySearchOnFindCallback;
			LobbySearchFindCallbackInfo lobbySearchFindCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<LobbySearchFindCallbackInfoInternal, LobbySearchOnFindCallback, LobbySearchFindCallbackInfo>(ref data, out lobbySearchOnFindCallback, out lobbySearchFindCallbackInfo);
			if (flag)
			{
				lobbySearchOnFindCallback(ref lobbySearchFindCallbackInfo);
			}
		}

		// Token: 0x04000B30 RID: 2864
		public const int LobbysearchCopysearchresultbyindexApiLatest = 1;

		// Token: 0x04000B31 RID: 2865
		public const int LobbysearchFindApiLatest = 1;

		// Token: 0x04000B32 RID: 2866
		public const int LobbysearchGetsearchresultcountApiLatest = 1;

		// Token: 0x04000B33 RID: 2867
		public const int LobbysearchRemoveparameterApiLatest = 1;

		// Token: 0x04000B34 RID: 2868
		public const int LobbysearchSetlobbyidApiLatest = 1;

		// Token: 0x04000B35 RID: 2869
		public const int LobbysearchSetmaxresultsApiLatest = 1;

		// Token: 0x04000B36 RID: 2870
		public const int LobbysearchSetparameterApiLatest = 1;

		// Token: 0x04000B37 RID: 2871
		public const int LobbysearchSettargetuseridApiLatest = 1;
	}
}
