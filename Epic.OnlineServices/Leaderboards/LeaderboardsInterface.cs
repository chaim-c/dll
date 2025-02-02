using System;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x0200040E RID: 1038
	public sealed class LeaderboardsInterface : Handle
	{
		// Token: 0x06001AC0 RID: 6848 RVA: 0x00027986 File Offset: 0x00025B86
		public LeaderboardsInterface()
		{
		}

		// Token: 0x06001AC1 RID: 6849 RVA: 0x00027990 File Offset: 0x00025B90
		public LeaderboardsInterface(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06001AC2 RID: 6850 RVA: 0x0002799C File Offset: 0x00025B9C
		public Result CopyLeaderboardDefinitionByIndex(ref CopyLeaderboardDefinitionByIndexOptions options, out Definition? outLeaderboardDefinition)
		{
			CopyLeaderboardDefinitionByIndexOptionsInternal copyLeaderboardDefinitionByIndexOptionsInternal = default(CopyLeaderboardDefinitionByIndexOptionsInternal);
			copyLeaderboardDefinitionByIndexOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_Leaderboards_CopyLeaderboardDefinitionByIndex(base.InnerHandle, ref copyLeaderboardDefinitionByIndexOptionsInternal, ref zero);
			Helper.Dispose<CopyLeaderboardDefinitionByIndexOptionsInternal>(ref copyLeaderboardDefinitionByIndexOptionsInternal);
			Helper.Get<DefinitionInternal, Definition>(zero, out outLeaderboardDefinition);
			bool flag = outLeaderboardDefinition != null;
			if (flag)
			{
				Bindings.EOS_Leaderboards_Definition_Release(zero);
			}
			return result;
		}

		// Token: 0x06001AC3 RID: 6851 RVA: 0x000279FC File Offset: 0x00025BFC
		public Result CopyLeaderboardDefinitionByLeaderboardId(ref CopyLeaderboardDefinitionByLeaderboardIdOptions options, out Definition? outLeaderboardDefinition)
		{
			CopyLeaderboardDefinitionByLeaderboardIdOptionsInternal copyLeaderboardDefinitionByLeaderboardIdOptionsInternal = default(CopyLeaderboardDefinitionByLeaderboardIdOptionsInternal);
			copyLeaderboardDefinitionByLeaderboardIdOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_Leaderboards_CopyLeaderboardDefinitionByLeaderboardId(base.InnerHandle, ref copyLeaderboardDefinitionByLeaderboardIdOptionsInternal, ref zero);
			Helper.Dispose<CopyLeaderboardDefinitionByLeaderboardIdOptionsInternal>(ref copyLeaderboardDefinitionByLeaderboardIdOptionsInternal);
			Helper.Get<DefinitionInternal, Definition>(zero, out outLeaderboardDefinition);
			bool flag = outLeaderboardDefinition != null;
			if (flag)
			{
				Bindings.EOS_Leaderboards_Definition_Release(zero);
			}
			return result;
		}

		// Token: 0x06001AC4 RID: 6852 RVA: 0x00027A5C File Offset: 0x00025C5C
		public Result CopyLeaderboardRecordByIndex(ref CopyLeaderboardRecordByIndexOptions options, out LeaderboardRecord? outLeaderboardRecord)
		{
			CopyLeaderboardRecordByIndexOptionsInternal copyLeaderboardRecordByIndexOptionsInternal = default(CopyLeaderboardRecordByIndexOptionsInternal);
			copyLeaderboardRecordByIndexOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_Leaderboards_CopyLeaderboardRecordByIndex(base.InnerHandle, ref copyLeaderboardRecordByIndexOptionsInternal, ref zero);
			Helper.Dispose<CopyLeaderboardRecordByIndexOptionsInternal>(ref copyLeaderboardRecordByIndexOptionsInternal);
			Helper.Get<LeaderboardRecordInternal, LeaderboardRecord>(zero, out outLeaderboardRecord);
			bool flag = outLeaderboardRecord != null;
			if (flag)
			{
				Bindings.EOS_Leaderboards_LeaderboardRecord_Release(zero);
			}
			return result;
		}

		// Token: 0x06001AC5 RID: 6853 RVA: 0x00027ABC File Offset: 0x00025CBC
		public Result CopyLeaderboardRecordByUserId(ref CopyLeaderboardRecordByUserIdOptions options, out LeaderboardRecord? outLeaderboardRecord)
		{
			CopyLeaderboardRecordByUserIdOptionsInternal copyLeaderboardRecordByUserIdOptionsInternal = default(CopyLeaderboardRecordByUserIdOptionsInternal);
			copyLeaderboardRecordByUserIdOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_Leaderboards_CopyLeaderboardRecordByUserId(base.InnerHandle, ref copyLeaderboardRecordByUserIdOptionsInternal, ref zero);
			Helper.Dispose<CopyLeaderboardRecordByUserIdOptionsInternal>(ref copyLeaderboardRecordByUserIdOptionsInternal);
			Helper.Get<LeaderboardRecordInternal, LeaderboardRecord>(zero, out outLeaderboardRecord);
			bool flag = outLeaderboardRecord != null;
			if (flag)
			{
				Bindings.EOS_Leaderboards_LeaderboardRecord_Release(zero);
			}
			return result;
		}

		// Token: 0x06001AC6 RID: 6854 RVA: 0x00027B1C File Offset: 0x00025D1C
		public Result CopyLeaderboardUserScoreByIndex(ref CopyLeaderboardUserScoreByIndexOptions options, out LeaderboardUserScore? outLeaderboardUserScore)
		{
			CopyLeaderboardUserScoreByIndexOptionsInternal copyLeaderboardUserScoreByIndexOptionsInternal = default(CopyLeaderboardUserScoreByIndexOptionsInternal);
			copyLeaderboardUserScoreByIndexOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_Leaderboards_CopyLeaderboardUserScoreByIndex(base.InnerHandle, ref copyLeaderboardUserScoreByIndexOptionsInternal, ref zero);
			Helper.Dispose<CopyLeaderboardUserScoreByIndexOptionsInternal>(ref copyLeaderboardUserScoreByIndexOptionsInternal);
			Helper.Get<LeaderboardUserScoreInternal, LeaderboardUserScore>(zero, out outLeaderboardUserScore);
			bool flag = outLeaderboardUserScore != null;
			if (flag)
			{
				Bindings.EOS_Leaderboards_LeaderboardUserScore_Release(zero);
			}
			return result;
		}

		// Token: 0x06001AC7 RID: 6855 RVA: 0x00027B7C File Offset: 0x00025D7C
		public Result CopyLeaderboardUserScoreByUserId(ref CopyLeaderboardUserScoreByUserIdOptions options, out LeaderboardUserScore? outLeaderboardUserScore)
		{
			CopyLeaderboardUserScoreByUserIdOptionsInternal copyLeaderboardUserScoreByUserIdOptionsInternal = default(CopyLeaderboardUserScoreByUserIdOptionsInternal);
			copyLeaderboardUserScoreByUserIdOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_Leaderboards_CopyLeaderboardUserScoreByUserId(base.InnerHandle, ref copyLeaderboardUserScoreByUserIdOptionsInternal, ref zero);
			Helper.Dispose<CopyLeaderboardUserScoreByUserIdOptionsInternal>(ref copyLeaderboardUserScoreByUserIdOptionsInternal);
			Helper.Get<LeaderboardUserScoreInternal, LeaderboardUserScore>(zero, out outLeaderboardUserScore);
			bool flag = outLeaderboardUserScore != null;
			if (flag)
			{
				Bindings.EOS_Leaderboards_LeaderboardUserScore_Release(zero);
			}
			return result;
		}

		// Token: 0x06001AC8 RID: 6856 RVA: 0x00027BDC File Offset: 0x00025DDC
		public uint GetLeaderboardDefinitionCount(ref GetLeaderboardDefinitionCountOptions options)
		{
			GetLeaderboardDefinitionCountOptionsInternal getLeaderboardDefinitionCountOptionsInternal = default(GetLeaderboardDefinitionCountOptionsInternal);
			getLeaderboardDefinitionCountOptionsInternal.Set(ref options);
			uint result = Bindings.EOS_Leaderboards_GetLeaderboardDefinitionCount(base.InnerHandle, ref getLeaderboardDefinitionCountOptionsInternal);
			Helper.Dispose<GetLeaderboardDefinitionCountOptionsInternal>(ref getLeaderboardDefinitionCountOptionsInternal);
			return result;
		}

		// Token: 0x06001AC9 RID: 6857 RVA: 0x00027C18 File Offset: 0x00025E18
		public uint GetLeaderboardRecordCount(ref GetLeaderboardRecordCountOptions options)
		{
			GetLeaderboardRecordCountOptionsInternal getLeaderboardRecordCountOptionsInternal = default(GetLeaderboardRecordCountOptionsInternal);
			getLeaderboardRecordCountOptionsInternal.Set(ref options);
			uint result = Bindings.EOS_Leaderboards_GetLeaderboardRecordCount(base.InnerHandle, ref getLeaderboardRecordCountOptionsInternal);
			Helper.Dispose<GetLeaderboardRecordCountOptionsInternal>(ref getLeaderboardRecordCountOptionsInternal);
			return result;
		}

		// Token: 0x06001ACA RID: 6858 RVA: 0x00027C54 File Offset: 0x00025E54
		public uint GetLeaderboardUserScoreCount(ref GetLeaderboardUserScoreCountOptions options)
		{
			GetLeaderboardUserScoreCountOptionsInternal getLeaderboardUserScoreCountOptionsInternal = default(GetLeaderboardUserScoreCountOptionsInternal);
			getLeaderboardUserScoreCountOptionsInternal.Set(ref options);
			uint result = Bindings.EOS_Leaderboards_GetLeaderboardUserScoreCount(base.InnerHandle, ref getLeaderboardUserScoreCountOptionsInternal);
			Helper.Dispose<GetLeaderboardUserScoreCountOptionsInternal>(ref getLeaderboardUserScoreCountOptionsInternal);
			return result;
		}

		// Token: 0x06001ACB RID: 6859 RVA: 0x00027C90 File Offset: 0x00025E90
		public void QueryLeaderboardDefinitions(ref QueryLeaderboardDefinitionsOptions options, object clientData, OnQueryLeaderboardDefinitionsCompleteCallback completionDelegate)
		{
			QueryLeaderboardDefinitionsOptionsInternal queryLeaderboardDefinitionsOptionsInternal = default(QueryLeaderboardDefinitionsOptionsInternal);
			queryLeaderboardDefinitionsOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnQueryLeaderboardDefinitionsCompleteCallbackInternal onQueryLeaderboardDefinitionsCompleteCallbackInternal = new OnQueryLeaderboardDefinitionsCompleteCallbackInternal(LeaderboardsInterface.OnQueryLeaderboardDefinitionsCompleteCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onQueryLeaderboardDefinitionsCompleteCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Leaderboards_QueryLeaderboardDefinitions(base.InnerHandle, ref queryLeaderboardDefinitionsOptionsInternal, zero, onQueryLeaderboardDefinitionsCompleteCallbackInternal);
			Helper.Dispose<QueryLeaderboardDefinitionsOptionsInternal>(ref queryLeaderboardDefinitionsOptionsInternal);
		}

		// Token: 0x06001ACC RID: 6860 RVA: 0x00027CEC File Offset: 0x00025EEC
		public void QueryLeaderboardRanks(ref QueryLeaderboardRanksOptions options, object clientData, OnQueryLeaderboardRanksCompleteCallback completionDelegate)
		{
			QueryLeaderboardRanksOptionsInternal queryLeaderboardRanksOptionsInternal = default(QueryLeaderboardRanksOptionsInternal);
			queryLeaderboardRanksOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnQueryLeaderboardRanksCompleteCallbackInternal onQueryLeaderboardRanksCompleteCallbackInternal = new OnQueryLeaderboardRanksCompleteCallbackInternal(LeaderboardsInterface.OnQueryLeaderboardRanksCompleteCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onQueryLeaderboardRanksCompleteCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Leaderboards_QueryLeaderboardRanks(base.InnerHandle, ref queryLeaderboardRanksOptionsInternal, zero, onQueryLeaderboardRanksCompleteCallbackInternal);
			Helper.Dispose<QueryLeaderboardRanksOptionsInternal>(ref queryLeaderboardRanksOptionsInternal);
		}

		// Token: 0x06001ACD RID: 6861 RVA: 0x00027D48 File Offset: 0x00025F48
		public void QueryLeaderboardUserScores(ref QueryLeaderboardUserScoresOptions options, object clientData, OnQueryLeaderboardUserScoresCompleteCallback completionDelegate)
		{
			QueryLeaderboardUserScoresOptionsInternal queryLeaderboardUserScoresOptionsInternal = default(QueryLeaderboardUserScoresOptionsInternal);
			queryLeaderboardUserScoresOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnQueryLeaderboardUserScoresCompleteCallbackInternal onQueryLeaderboardUserScoresCompleteCallbackInternal = new OnQueryLeaderboardUserScoresCompleteCallbackInternal(LeaderboardsInterface.OnQueryLeaderboardUserScoresCompleteCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onQueryLeaderboardUserScoresCompleteCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Leaderboards_QueryLeaderboardUserScores(base.InnerHandle, ref queryLeaderboardUserScoresOptionsInternal, zero, onQueryLeaderboardUserScoresCompleteCallbackInternal);
			Helper.Dispose<QueryLeaderboardUserScoresOptionsInternal>(ref queryLeaderboardUserScoresOptionsInternal);
		}

		// Token: 0x06001ACE RID: 6862 RVA: 0x00027DA4 File Offset: 0x00025FA4
		[MonoPInvokeCallback(typeof(OnQueryLeaderboardDefinitionsCompleteCallbackInternal))]
		internal static void OnQueryLeaderboardDefinitionsCompleteCallbackInternalImplementation(ref OnQueryLeaderboardDefinitionsCompleteCallbackInfoInternal data)
		{
			OnQueryLeaderboardDefinitionsCompleteCallback onQueryLeaderboardDefinitionsCompleteCallback;
			OnQueryLeaderboardDefinitionsCompleteCallbackInfo onQueryLeaderboardDefinitionsCompleteCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<OnQueryLeaderboardDefinitionsCompleteCallbackInfoInternal, OnQueryLeaderboardDefinitionsCompleteCallback, OnQueryLeaderboardDefinitionsCompleteCallbackInfo>(ref data, out onQueryLeaderboardDefinitionsCompleteCallback, out onQueryLeaderboardDefinitionsCompleteCallbackInfo);
			if (flag)
			{
				onQueryLeaderboardDefinitionsCompleteCallback(ref onQueryLeaderboardDefinitionsCompleteCallbackInfo);
			}
		}

		// Token: 0x06001ACF RID: 6863 RVA: 0x00027DCC File Offset: 0x00025FCC
		[MonoPInvokeCallback(typeof(OnQueryLeaderboardRanksCompleteCallbackInternal))]
		internal static void OnQueryLeaderboardRanksCompleteCallbackInternalImplementation(ref OnQueryLeaderboardRanksCompleteCallbackInfoInternal data)
		{
			OnQueryLeaderboardRanksCompleteCallback onQueryLeaderboardRanksCompleteCallback;
			OnQueryLeaderboardRanksCompleteCallbackInfo onQueryLeaderboardRanksCompleteCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<OnQueryLeaderboardRanksCompleteCallbackInfoInternal, OnQueryLeaderboardRanksCompleteCallback, OnQueryLeaderboardRanksCompleteCallbackInfo>(ref data, out onQueryLeaderboardRanksCompleteCallback, out onQueryLeaderboardRanksCompleteCallbackInfo);
			if (flag)
			{
				onQueryLeaderboardRanksCompleteCallback(ref onQueryLeaderboardRanksCompleteCallbackInfo);
			}
		}

		// Token: 0x06001AD0 RID: 6864 RVA: 0x00027DF4 File Offset: 0x00025FF4
		[MonoPInvokeCallback(typeof(OnQueryLeaderboardUserScoresCompleteCallbackInternal))]
		internal static void OnQueryLeaderboardUserScoresCompleteCallbackInternalImplementation(ref OnQueryLeaderboardUserScoresCompleteCallbackInfoInternal data)
		{
			OnQueryLeaderboardUserScoresCompleteCallback onQueryLeaderboardUserScoresCompleteCallback;
			OnQueryLeaderboardUserScoresCompleteCallbackInfo onQueryLeaderboardUserScoresCompleteCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<OnQueryLeaderboardUserScoresCompleteCallbackInfoInternal, OnQueryLeaderboardUserScoresCompleteCallback, OnQueryLeaderboardUserScoresCompleteCallbackInfo>(ref data, out onQueryLeaderboardUserScoresCompleteCallback, out onQueryLeaderboardUserScoresCompleteCallbackInfo);
			if (flag)
			{
				onQueryLeaderboardUserScoresCompleteCallback(ref onQueryLeaderboardUserScoresCompleteCallbackInfo);
			}
		}

		// Token: 0x04000BE9 RID: 3049
		public const int CopyleaderboarddefinitionbyindexApiLatest = 1;

		// Token: 0x04000BEA RID: 3050
		public const int CopyleaderboarddefinitionbyleaderboardidApiLatest = 1;

		// Token: 0x04000BEB RID: 3051
		public const int CopyleaderboardrecordbyindexApiLatest = 2;

		// Token: 0x04000BEC RID: 3052
		public const int CopyleaderboardrecordbyuseridApiLatest = 2;

		// Token: 0x04000BED RID: 3053
		public const int CopyleaderboarduserscorebyindexApiLatest = 1;

		// Token: 0x04000BEE RID: 3054
		public const int CopyleaderboarduserscorebyuseridApiLatest = 1;

		// Token: 0x04000BEF RID: 3055
		public const int DefinitionApiLatest = 1;

		// Token: 0x04000BF0 RID: 3056
		public const int GetleaderboarddefinitioncountApiLatest = 1;

		// Token: 0x04000BF1 RID: 3057
		public const int GetleaderboardrecordcountApiLatest = 1;

		// Token: 0x04000BF2 RID: 3058
		public const int GetleaderboarduserscorecountApiLatest = 1;

		// Token: 0x04000BF3 RID: 3059
		public const int LeaderboardrecordApiLatest = 2;

		// Token: 0x04000BF4 RID: 3060
		public const int LeaderboarduserscoreApiLatest = 1;

		// Token: 0x04000BF5 RID: 3061
		public const int QueryleaderboarddefinitionsApiLatest = 2;

		// Token: 0x04000BF6 RID: 3062
		public const int QueryleaderboardranksApiLatest = 2;

		// Token: 0x04000BF7 RID: 3063
		public const int QueryleaderboarduserscoresApiLatest = 2;

		// Token: 0x04000BF8 RID: 3064
		public const int TimeUndefined = -1;

		// Token: 0x04000BF9 RID: 3065
		public const int UserscoresquerystatinfoApiLatest = 1;
	}
}
