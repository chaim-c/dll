using System;

namespace Epic.OnlineServices.Stats
{
	// Token: 0x020000BB RID: 187
	public sealed class StatsInterface : Handle
	{
		// Token: 0x060006C0 RID: 1728 RVA: 0x0000A166 File Offset: 0x00008366
		public StatsInterface()
		{
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x0000A170 File Offset: 0x00008370
		public StatsInterface(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x0000A17C File Offset: 0x0000837C
		public Result CopyStatByIndex(ref CopyStatByIndexOptions options, out Stat? outStat)
		{
			CopyStatByIndexOptionsInternal copyStatByIndexOptionsInternal = default(CopyStatByIndexOptionsInternal);
			copyStatByIndexOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_Stats_CopyStatByIndex(base.InnerHandle, ref copyStatByIndexOptionsInternal, ref zero);
			Helper.Dispose<CopyStatByIndexOptionsInternal>(ref copyStatByIndexOptionsInternal);
			Helper.Get<StatInternal, Stat>(zero, out outStat);
			bool flag = outStat != null;
			if (flag)
			{
				Bindings.EOS_Stats_Stat_Release(zero);
			}
			return result;
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x0000A1DC File Offset: 0x000083DC
		public Result CopyStatByName(ref CopyStatByNameOptions options, out Stat? outStat)
		{
			CopyStatByNameOptionsInternal copyStatByNameOptionsInternal = default(CopyStatByNameOptionsInternal);
			copyStatByNameOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_Stats_CopyStatByName(base.InnerHandle, ref copyStatByNameOptionsInternal, ref zero);
			Helper.Dispose<CopyStatByNameOptionsInternal>(ref copyStatByNameOptionsInternal);
			Helper.Get<StatInternal, Stat>(zero, out outStat);
			bool flag = outStat != null;
			if (flag)
			{
				Bindings.EOS_Stats_Stat_Release(zero);
			}
			return result;
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x0000A23C File Offset: 0x0000843C
		public uint GetStatsCount(ref GetStatCountOptions options)
		{
			GetStatCountOptionsInternal getStatCountOptionsInternal = default(GetStatCountOptionsInternal);
			getStatCountOptionsInternal.Set(ref options);
			uint result = Bindings.EOS_Stats_GetStatsCount(base.InnerHandle, ref getStatCountOptionsInternal);
			Helper.Dispose<GetStatCountOptionsInternal>(ref getStatCountOptionsInternal);
			return result;
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x0000A278 File Offset: 0x00008478
		public void IngestStat(ref IngestStatOptions options, object clientData, OnIngestStatCompleteCallback completionDelegate)
		{
			IngestStatOptionsInternal ingestStatOptionsInternal = default(IngestStatOptionsInternal);
			ingestStatOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnIngestStatCompleteCallbackInternal onIngestStatCompleteCallbackInternal = new OnIngestStatCompleteCallbackInternal(StatsInterface.OnIngestStatCompleteCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onIngestStatCompleteCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Stats_IngestStat(base.InnerHandle, ref ingestStatOptionsInternal, zero, onIngestStatCompleteCallbackInternal);
			Helper.Dispose<IngestStatOptionsInternal>(ref ingestStatOptionsInternal);
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x0000A2D4 File Offset: 0x000084D4
		public void QueryStats(ref QueryStatsOptions options, object clientData, OnQueryStatsCompleteCallback completionDelegate)
		{
			QueryStatsOptionsInternal queryStatsOptionsInternal = default(QueryStatsOptionsInternal);
			queryStatsOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnQueryStatsCompleteCallbackInternal onQueryStatsCompleteCallbackInternal = new OnQueryStatsCompleteCallbackInternal(StatsInterface.OnQueryStatsCompleteCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onQueryStatsCompleteCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Stats_QueryStats(base.InnerHandle, ref queryStatsOptionsInternal, zero, onQueryStatsCompleteCallbackInternal);
			Helper.Dispose<QueryStatsOptionsInternal>(ref queryStatsOptionsInternal);
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x0000A330 File Offset: 0x00008530
		[MonoPInvokeCallback(typeof(OnIngestStatCompleteCallbackInternal))]
		internal static void OnIngestStatCompleteCallbackInternalImplementation(ref IngestStatCompleteCallbackInfoInternal data)
		{
			OnIngestStatCompleteCallback onIngestStatCompleteCallback;
			IngestStatCompleteCallbackInfo ingestStatCompleteCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<IngestStatCompleteCallbackInfoInternal, OnIngestStatCompleteCallback, IngestStatCompleteCallbackInfo>(ref data, out onIngestStatCompleteCallback, out ingestStatCompleteCallbackInfo);
			if (flag)
			{
				onIngestStatCompleteCallback(ref ingestStatCompleteCallbackInfo);
			}
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x0000A358 File Offset: 0x00008558
		[MonoPInvokeCallback(typeof(OnQueryStatsCompleteCallbackInternal))]
		internal static void OnQueryStatsCompleteCallbackInternalImplementation(ref OnQueryStatsCompleteCallbackInfoInternal data)
		{
			OnQueryStatsCompleteCallback onQueryStatsCompleteCallback;
			OnQueryStatsCompleteCallbackInfo onQueryStatsCompleteCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<OnQueryStatsCompleteCallbackInfoInternal, OnQueryStatsCompleteCallback, OnQueryStatsCompleteCallbackInfo>(ref data, out onQueryStatsCompleteCallback, out onQueryStatsCompleteCallbackInfo);
			if (flag)
			{
				onQueryStatsCompleteCallback(ref onQueryStatsCompleteCallbackInfo);
			}
		}

		// Token: 0x04000338 RID: 824
		public const int CopystatbyindexApiLatest = 1;

		// Token: 0x04000339 RID: 825
		public const int CopystatbynameApiLatest = 1;

		// Token: 0x0400033A RID: 826
		public const int GetstatcountApiLatest = 1;

		// Token: 0x0400033B RID: 827
		public const int GetstatscountApiLatest = 1;

		// Token: 0x0400033C RID: 828
		public const int IngestdataApiLatest = 1;

		// Token: 0x0400033D RID: 829
		public const int IngeststatApiLatest = 3;

		// Token: 0x0400033E RID: 830
		public const int MaxIngestStats = 3000;

		// Token: 0x0400033F RID: 831
		public const int MaxQueryStats = 1000;

		// Token: 0x04000340 RID: 832
		public const int QuerystatsApiLatest = 3;

		// Token: 0x04000341 RID: 833
		public const int StatApiLatest = 1;

		// Token: 0x04000342 RID: 834
		public const int TimeUndefined = -1;
	}
}
