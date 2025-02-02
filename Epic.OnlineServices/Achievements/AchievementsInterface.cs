using System;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000661 RID: 1633
	public sealed class AchievementsInterface : Handle
	{
		// Token: 0x060029E0 RID: 10720 RVA: 0x0003E80B File Offset: 0x0003CA0B
		public AchievementsInterface()
		{
		}

		// Token: 0x060029E1 RID: 10721 RVA: 0x0003E815 File Offset: 0x0003CA15
		public AchievementsInterface(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x060029E2 RID: 10722 RVA: 0x0003E820 File Offset: 0x0003CA20
		public ulong AddNotifyAchievementsUnlocked(ref AddNotifyAchievementsUnlockedOptions options, object clientData, OnAchievementsUnlockedCallback notificationFn)
		{
			AddNotifyAchievementsUnlockedOptionsInternal addNotifyAchievementsUnlockedOptionsInternal = default(AddNotifyAchievementsUnlockedOptionsInternal);
			addNotifyAchievementsUnlockedOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnAchievementsUnlockedCallbackInternal onAchievementsUnlockedCallbackInternal = new OnAchievementsUnlockedCallbackInternal(AchievementsInterface.OnAchievementsUnlockedCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, notificationFn, onAchievementsUnlockedCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_Achievements_AddNotifyAchievementsUnlocked(base.InnerHandle, ref addNotifyAchievementsUnlockedOptionsInternal, zero, onAchievementsUnlockedCallbackInternal);
			Helper.Dispose<AddNotifyAchievementsUnlockedOptionsInternal>(ref addNotifyAchievementsUnlockedOptionsInternal);
			Helper.AssignNotificationIdToCallback(zero, num);
			return num;
		}

		// Token: 0x060029E3 RID: 10723 RVA: 0x0003E88C File Offset: 0x0003CA8C
		public ulong AddNotifyAchievementsUnlockedV2(ref AddNotifyAchievementsUnlockedV2Options options, object clientData, OnAchievementsUnlockedCallbackV2 notificationFn)
		{
			AddNotifyAchievementsUnlockedV2OptionsInternal addNotifyAchievementsUnlockedV2OptionsInternal = default(AddNotifyAchievementsUnlockedV2OptionsInternal);
			addNotifyAchievementsUnlockedV2OptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnAchievementsUnlockedCallbackV2Internal onAchievementsUnlockedCallbackV2Internal = new OnAchievementsUnlockedCallbackV2Internal(AchievementsInterface.OnAchievementsUnlockedCallbackV2InternalImplementation);
			Helper.AddCallback(out zero, clientData, notificationFn, onAchievementsUnlockedCallbackV2Internal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_Achievements_AddNotifyAchievementsUnlockedV2(base.InnerHandle, ref addNotifyAchievementsUnlockedV2OptionsInternal, zero, onAchievementsUnlockedCallbackV2Internal);
			Helper.Dispose<AddNotifyAchievementsUnlockedV2OptionsInternal>(ref addNotifyAchievementsUnlockedV2OptionsInternal);
			Helper.AssignNotificationIdToCallback(zero, num);
			return num;
		}

		// Token: 0x060029E4 RID: 10724 RVA: 0x0003E8F8 File Offset: 0x0003CAF8
		public Result CopyAchievementDefinitionByAchievementId(ref CopyAchievementDefinitionByAchievementIdOptions options, out Definition? outDefinition)
		{
			CopyAchievementDefinitionByAchievementIdOptionsInternal copyAchievementDefinitionByAchievementIdOptionsInternal = default(CopyAchievementDefinitionByAchievementIdOptionsInternal);
			copyAchievementDefinitionByAchievementIdOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_Achievements_CopyAchievementDefinitionByAchievementId(base.InnerHandle, ref copyAchievementDefinitionByAchievementIdOptionsInternal, ref zero);
			Helper.Dispose<CopyAchievementDefinitionByAchievementIdOptionsInternal>(ref copyAchievementDefinitionByAchievementIdOptionsInternal);
			Helper.Get<DefinitionInternal, Definition>(zero, out outDefinition);
			bool flag = outDefinition != null;
			if (flag)
			{
				Bindings.EOS_Achievements_Definition_Release(zero);
			}
			return result;
		}

		// Token: 0x060029E5 RID: 10725 RVA: 0x0003E958 File Offset: 0x0003CB58
		public Result CopyAchievementDefinitionByIndex(ref CopyAchievementDefinitionByIndexOptions options, out Definition? outDefinition)
		{
			CopyAchievementDefinitionByIndexOptionsInternal copyAchievementDefinitionByIndexOptionsInternal = default(CopyAchievementDefinitionByIndexOptionsInternal);
			copyAchievementDefinitionByIndexOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_Achievements_CopyAchievementDefinitionByIndex(base.InnerHandle, ref copyAchievementDefinitionByIndexOptionsInternal, ref zero);
			Helper.Dispose<CopyAchievementDefinitionByIndexOptionsInternal>(ref copyAchievementDefinitionByIndexOptionsInternal);
			Helper.Get<DefinitionInternal, Definition>(zero, out outDefinition);
			bool flag = outDefinition != null;
			if (flag)
			{
				Bindings.EOS_Achievements_Definition_Release(zero);
			}
			return result;
		}

		// Token: 0x060029E6 RID: 10726 RVA: 0x0003E9B8 File Offset: 0x0003CBB8
		public Result CopyAchievementDefinitionV2ByAchievementId(ref CopyAchievementDefinitionV2ByAchievementIdOptions options, out DefinitionV2? outDefinition)
		{
			CopyAchievementDefinitionV2ByAchievementIdOptionsInternal copyAchievementDefinitionV2ByAchievementIdOptionsInternal = default(CopyAchievementDefinitionV2ByAchievementIdOptionsInternal);
			copyAchievementDefinitionV2ByAchievementIdOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_Achievements_CopyAchievementDefinitionV2ByAchievementId(base.InnerHandle, ref copyAchievementDefinitionV2ByAchievementIdOptionsInternal, ref zero);
			Helper.Dispose<CopyAchievementDefinitionV2ByAchievementIdOptionsInternal>(ref copyAchievementDefinitionV2ByAchievementIdOptionsInternal);
			Helper.Get<DefinitionV2Internal, DefinitionV2>(zero, out outDefinition);
			bool flag = outDefinition != null;
			if (flag)
			{
				Bindings.EOS_Achievements_DefinitionV2_Release(zero);
			}
			return result;
		}

		// Token: 0x060029E7 RID: 10727 RVA: 0x0003EA18 File Offset: 0x0003CC18
		public Result CopyAchievementDefinitionV2ByIndex(ref CopyAchievementDefinitionV2ByIndexOptions options, out DefinitionV2? outDefinition)
		{
			CopyAchievementDefinitionV2ByIndexOptionsInternal copyAchievementDefinitionV2ByIndexOptionsInternal = default(CopyAchievementDefinitionV2ByIndexOptionsInternal);
			copyAchievementDefinitionV2ByIndexOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_Achievements_CopyAchievementDefinitionV2ByIndex(base.InnerHandle, ref copyAchievementDefinitionV2ByIndexOptionsInternal, ref zero);
			Helper.Dispose<CopyAchievementDefinitionV2ByIndexOptionsInternal>(ref copyAchievementDefinitionV2ByIndexOptionsInternal);
			Helper.Get<DefinitionV2Internal, DefinitionV2>(zero, out outDefinition);
			bool flag = outDefinition != null;
			if (flag)
			{
				Bindings.EOS_Achievements_DefinitionV2_Release(zero);
			}
			return result;
		}

		// Token: 0x060029E8 RID: 10728 RVA: 0x0003EA78 File Offset: 0x0003CC78
		public Result CopyPlayerAchievementByAchievementId(ref CopyPlayerAchievementByAchievementIdOptions options, out PlayerAchievement? outAchievement)
		{
			CopyPlayerAchievementByAchievementIdOptionsInternal copyPlayerAchievementByAchievementIdOptionsInternal = default(CopyPlayerAchievementByAchievementIdOptionsInternal);
			copyPlayerAchievementByAchievementIdOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_Achievements_CopyPlayerAchievementByAchievementId(base.InnerHandle, ref copyPlayerAchievementByAchievementIdOptionsInternal, ref zero);
			Helper.Dispose<CopyPlayerAchievementByAchievementIdOptionsInternal>(ref copyPlayerAchievementByAchievementIdOptionsInternal);
			Helper.Get<PlayerAchievementInternal, PlayerAchievement>(zero, out outAchievement);
			bool flag = outAchievement != null;
			if (flag)
			{
				Bindings.EOS_Achievements_PlayerAchievement_Release(zero);
			}
			return result;
		}

		// Token: 0x060029E9 RID: 10729 RVA: 0x0003EAD8 File Offset: 0x0003CCD8
		public Result CopyPlayerAchievementByIndex(ref CopyPlayerAchievementByIndexOptions options, out PlayerAchievement? outAchievement)
		{
			CopyPlayerAchievementByIndexOptionsInternal copyPlayerAchievementByIndexOptionsInternal = default(CopyPlayerAchievementByIndexOptionsInternal);
			copyPlayerAchievementByIndexOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_Achievements_CopyPlayerAchievementByIndex(base.InnerHandle, ref copyPlayerAchievementByIndexOptionsInternal, ref zero);
			Helper.Dispose<CopyPlayerAchievementByIndexOptionsInternal>(ref copyPlayerAchievementByIndexOptionsInternal);
			Helper.Get<PlayerAchievementInternal, PlayerAchievement>(zero, out outAchievement);
			bool flag = outAchievement != null;
			if (flag)
			{
				Bindings.EOS_Achievements_PlayerAchievement_Release(zero);
			}
			return result;
		}

		// Token: 0x060029EA RID: 10730 RVA: 0x0003EB38 File Offset: 0x0003CD38
		public Result CopyUnlockedAchievementByAchievementId(ref CopyUnlockedAchievementByAchievementIdOptions options, out UnlockedAchievement? outAchievement)
		{
			CopyUnlockedAchievementByAchievementIdOptionsInternal copyUnlockedAchievementByAchievementIdOptionsInternal = default(CopyUnlockedAchievementByAchievementIdOptionsInternal);
			copyUnlockedAchievementByAchievementIdOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_Achievements_CopyUnlockedAchievementByAchievementId(base.InnerHandle, ref copyUnlockedAchievementByAchievementIdOptionsInternal, ref zero);
			Helper.Dispose<CopyUnlockedAchievementByAchievementIdOptionsInternal>(ref copyUnlockedAchievementByAchievementIdOptionsInternal);
			Helper.Get<UnlockedAchievementInternal, UnlockedAchievement>(zero, out outAchievement);
			bool flag = outAchievement != null;
			if (flag)
			{
				Bindings.EOS_Achievements_UnlockedAchievement_Release(zero);
			}
			return result;
		}

		// Token: 0x060029EB RID: 10731 RVA: 0x0003EB98 File Offset: 0x0003CD98
		public Result CopyUnlockedAchievementByIndex(ref CopyUnlockedAchievementByIndexOptions options, out UnlockedAchievement? outAchievement)
		{
			CopyUnlockedAchievementByIndexOptionsInternal copyUnlockedAchievementByIndexOptionsInternal = default(CopyUnlockedAchievementByIndexOptionsInternal);
			copyUnlockedAchievementByIndexOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_Achievements_CopyUnlockedAchievementByIndex(base.InnerHandle, ref copyUnlockedAchievementByIndexOptionsInternal, ref zero);
			Helper.Dispose<CopyUnlockedAchievementByIndexOptionsInternal>(ref copyUnlockedAchievementByIndexOptionsInternal);
			Helper.Get<UnlockedAchievementInternal, UnlockedAchievement>(zero, out outAchievement);
			bool flag = outAchievement != null;
			if (flag)
			{
				Bindings.EOS_Achievements_UnlockedAchievement_Release(zero);
			}
			return result;
		}

		// Token: 0x060029EC RID: 10732 RVA: 0x0003EBF8 File Offset: 0x0003CDF8
		public uint GetAchievementDefinitionCount(ref GetAchievementDefinitionCountOptions options)
		{
			GetAchievementDefinitionCountOptionsInternal getAchievementDefinitionCountOptionsInternal = default(GetAchievementDefinitionCountOptionsInternal);
			getAchievementDefinitionCountOptionsInternal.Set(ref options);
			uint result = Bindings.EOS_Achievements_GetAchievementDefinitionCount(base.InnerHandle, ref getAchievementDefinitionCountOptionsInternal);
			Helper.Dispose<GetAchievementDefinitionCountOptionsInternal>(ref getAchievementDefinitionCountOptionsInternal);
			return result;
		}

		// Token: 0x060029ED RID: 10733 RVA: 0x0003EC34 File Offset: 0x0003CE34
		public uint GetPlayerAchievementCount(ref GetPlayerAchievementCountOptions options)
		{
			GetPlayerAchievementCountOptionsInternal getPlayerAchievementCountOptionsInternal = default(GetPlayerAchievementCountOptionsInternal);
			getPlayerAchievementCountOptionsInternal.Set(ref options);
			uint result = Bindings.EOS_Achievements_GetPlayerAchievementCount(base.InnerHandle, ref getPlayerAchievementCountOptionsInternal);
			Helper.Dispose<GetPlayerAchievementCountOptionsInternal>(ref getPlayerAchievementCountOptionsInternal);
			return result;
		}

		// Token: 0x060029EE RID: 10734 RVA: 0x0003EC70 File Offset: 0x0003CE70
		public uint GetUnlockedAchievementCount(ref GetUnlockedAchievementCountOptions options)
		{
			GetUnlockedAchievementCountOptionsInternal getUnlockedAchievementCountOptionsInternal = default(GetUnlockedAchievementCountOptionsInternal);
			getUnlockedAchievementCountOptionsInternal.Set(ref options);
			uint result = Bindings.EOS_Achievements_GetUnlockedAchievementCount(base.InnerHandle, ref getUnlockedAchievementCountOptionsInternal);
			Helper.Dispose<GetUnlockedAchievementCountOptionsInternal>(ref getUnlockedAchievementCountOptionsInternal);
			return result;
		}

		// Token: 0x060029EF RID: 10735 RVA: 0x0003ECAC File Offset: 0x0003CEAC
		public void QueryDefinitions(ref QueryDefinitionsOptions options, object clientData, OnQueryDefinitionsCompleteCallback completionDelegate)
		{
			QueryDefinitionsOptionsInternal queryDefinitionsOptionsInternal = default(QueryDefinitionsOptionsInternal);
			queryDefinitionsOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnQueryDefinitionsCompleteCallbackInternal onQueryDefinitionsCompleteCallbackInternal = new OnQueryDefinitionsCompleteCallbackInternal(AchievementsInterface.OnQueryDefinitionsCompleteCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onQueryDefinitionsCompleteCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Achievements_QueryDefinitions(base.InnerHandle, ref queryDefinitionsOptionsInternal, zero, onQueryDefinitionsCompleteCallbackInternal);
			Helper.Dispose<QueryDefinitionsOptionsInternal>(ref queryDefinitionsOptionsInternal);
		}

		// Token: 0x060029F0 RID: 10736 RVA: 0x0003ED08 File Offset: 0x0003CF08
		public void QueryPlayerAchievements(ref QueryPlayerAchievementsOptions options, object clientData, OnQueryPlayerAchievementsCompleteCallback completionDelegate)
		{
			QueryPlayerAchievementsOptionsInternal queryPlayerAchievementsOptionsInternal = default(QueryPlayerAchievementsOptionsInternal);
			queryPlayerAchievementsOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnQueryPlayerAchievementsCompleteCallbackInternal onQueryPlayerAchievementsCompleteCallbackInternal = new OnQueryPlayerAchievementsCompleteCallbackInternal(AchievementsInterface.OnQueryPlayerAchievementsCompleteCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onQueryPlayerAchievementsCompleteCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Achievements_QueryPlayerAchievements(base.InnerHandle, ref queryPlayerAchievementsOptionsInternal, zero, onQueryPlayerAchievementsCompleteCallbackInternal);
			Helper.Dispose<QueryPlayerAchievementsOptionsInternal>(ref queryPlayerAchievementsOptionsInternal);
		}

		// Token: 0x060029F1 RID: 10737 RVA: 0x0003ED62 File Offset: 0x0003CF62
		public void RemoveNotifyAchievementsUnlocked(ulong inId)
		{
			Bindings.EOS_Achievements_RemoveNotifyAchievementsUnlocked(base.InnerHandle, inId);
			Helper.RemoveCallbackByNotificationId(inId);
		}

		// Token: 0x060029F2 RID: 10738 RVA: 0x0003ED7C File Offset: 0x0003CF7C
		public void UnlockAchievements(ref UnlockAchievementsOptions options, object clientData, OnUnlockAchievementsCompleteCallback completionDelegate)
		{
			UnlockAchievementsOptionsInternal unlockAchievementsOptionsInternal = default(UnlockAchievementsOptionsInternal);
			unlockAchievementsOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnUnlockAchievementsCompleteCallbackInternal onUnlockAchievementsCompleteCallbackInternal = new OnUnlockAchievementsCompleteCallbackInternal(AchievementsInterface.OnUnlockAchievementsCompleteCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onUnlockAchievementsCompleteCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Achievements_UnlockAchievements(base.InnerHandle, ref unlockAchievementsOptionsInternal, zero, onUnlockAchievementsCompleteCallbackInternal);
			Helper.Dispose<UnlockAchievementsOptionsInternal>(ref unlockAchievementsOptionsInternal);
		}

		// Token: 0x060029F3 RID: 10739 RVA: 0x0003EDD8 File Offset: 0x0003CFD8
		[MonoPInvokeCallback(typeof(OnAchievementsUnlockedCallbackInternal))]
		internal static void OnAchievementsUnlockedCallbackInternalImplementation(ref OnAchievementsUnlockedCallbackInfoInternal data)
		{
			OnAchievementsUnlockedCallback onAchievementsUnlockedCallback;
			OnAchievementsUnlockedCallbackInfo onAchievementsUnlockedCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<OnAchievementsUnlockedCallbackInfoInternal, OnAchievementsUnlockedCallback, OnAchievementsUnlockedCallbackInfo>(ref data, out onAchievementsUnlockedCallback, out onAchievementsUnlockedCallbackInfo);
			if (flag)
			{
				onAchievementsUnlockedCallback(ref onAchievementsUnlockedCallbackInfo);
			}
		}

		// Token: 0x060029F4 RID: 10740 RVA: 0x0003EE00 File Offset: 0x0003D000
		[MonoPInvokeCallback(typeof(OnAchievementsUnlockedCallbackV2Internal))]
		internal static void OnAchievementsUnlockedCallbackV2InternalImplementation(ref OnAchievementsUnlockedCallbackV2InfoInternal data)
		{
			OnAchievementsUnlockedCallbackV2 onAchievementsUnlockedCallbackV;
			OnAchievementsUnlockedCallbackV2Info onAchievementsUnlockedCallbackV2Info;
			bool flag = Helper.TryGetAndRemoveCallback<OnAchievementsUnlockedCallbackV2InfoInternal, OnAchievementsUnlockedCallbackV2, OnAchievementsUnlockedCallbackV2Info>(ref data, out onAchievementsUnlockedCallbackV, out onAchievementsUnlockedCallbackV2Info);
			if (flag)
			{
				onAchievementsUnlockedCallbackV(ref onAchievementsUnlockedCallbackV2Info);
			}
		}

		// Token: 0x060029F5 RID: 10741 RVA: 0x0003EE28 File Offset: 0x0003D028
		[MonoPInvokeCallback(typeof(OnQueryDefinitionsCompleteCallbackInternal))]
		internal static void OnQueryDefinitionsCompleteCallbackInternalImplementation(ref OnQueryDefinitionsCompleteCallbackInfoInternal data)
		{
			OnQueryDefinitionsCompleteCallback onQueryDefinitionsCompleteCallback;
			OnQueryDefinitionsCompleteCallbackInfo onQueryDefinitionsCompleteCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<OnQueryDefinitionsCompleteCallbackInfoInternal, OnQueryDefinitionsCompleteCallback, OnQueryDefinitionsCompleteCallbackInfo>(ref data, out onQueryDefinitionsCompleteCallback, out onQueryDefinitionsCompleteCallbackInfo);
			if (flag)
			{
				onQueryDefinitionsCompleteCallback(ref onQueryDefinitionsCompleteCallbackInfo);
			}
		}

		// Token: 0x060029F6 RID: 10742 RVA: 0x0003EE50 File Offset: 0x0003D050
		[MonoPInvokeCallback(typeof(OnQueryPlayerAchievementsCompleteCallbackInternal))]
		internal static void OnQueryPlayerAchievementsCompleteCallbackInternalImplementation(ref OnQueryPlayerAchievementsCompleteCallbackInfoInternal data)
		{
			OnQueryPlayerAchievementsCompleteCallback onQueryPlayerAchievementsCompleteCallback;
			OnQueryPlayerAchievementsCompleteCallbackInfo onQueryPlayerAchievementsCompleteCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<OnQueryPlayerAchievementsCompleteCallbackInfoInternal, OnQueryPlayerAchievementsCompleteCallback, OnQueryPlayerAchievementsCompleteCallbackInfo>(ref data, out onQueryPlayerAchievementsCompleteCallback, out onQueryPlayerAchievementsCompleteCallbackInfo);
			if (flag)
			{
				onQueryPlayerAchievementsCompleteCallback(ref onQueryPlayerAchievementsCompleteCallbackInfo);
			}
		}

		// Token: 0x060029F7 RID: 10743 RVA: 0x0003EE78 File Offset: 0x0003D078
		[MonoPInvokeCallback(typeof(OnUnlockAchievementsCompleteCallbackInternal))]
		internal static void OnUnlockAchievementsCompleteCallbackInternalImplementation(ref OnUnlockAchievementsCompleteCallbackInfoInternal data)
		{
			OnUnlockAchievementsCompleteCallback onUnlockAchievementsCompleteCallback;
			OnUnlockAchievementsCompleteCallbackInfo onUnlockAchievementsCompleteCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<OnUnlockAchievementsCompleteCallbackInfoInternal, OnUnlockAchievementsCompleteCallback, OnUnlockAchievementsCompleteCallbackInfo>(ref data, out onUnlockAchievementsCompleteCallback, out onUnlockAchievementsCompleteCallbackInfo);
			if (flag)
			{
				onUnlockAchievementsCompleteCallback(ref onUnlockAchievementsCompleteCallbackInfo);
			}
		}

		// Token: 0x04001303 RID: 4867
		public const int AchievementUnlocktimeUndefined = -1;

		// Token: 0x04001304 RID: 4868
		public const int AddnotifyachievementsunlockedApiLatest = 1;

		// Token: 0x04001305 RID: 4869
		public const int Addnotifyachievementsunlockedv2ApiLatest = 2;

		// Token: 0x04001306 RID: 4870
		public const int Copyachievementdefinitionv2ByachievementidApiLatest = 2;

		// Token: 0x04001307 RID: 4871
		public const int Copyachievementdefinitionv2ByindexApiLatest = 2;

		// Token: 0x04001308 RID: 4872
		public const int CopydefinitionbyachievementidApiLatest = 1;

		// Token: 0x04001309 RID: 4873
		public const int CopydefinitionbyindexApiLatest = 1;

		// Token: 0x0400130A RID: 4874
		public const int Copydefinitionv2ByachievementidApiLatest = 2;

		// Token: 0x0400130B RID: 4875
		public const int Copydefinitionv2ByindexApiLatest = 2;

		// Token: 0x0400130C RID: 4876
		public const int CopyplayerachievementbyachievementidApiLatest = 2;

		// Token: 0x0400130D RID: 4877
		public const int CopyplayerachievementbyindexApiLatest = 2;

		// Token: 0x0400130E RID: 4878
		public const int CopyunlockedachievementbyachievementidApiLatest = 1;

		// Token: 0x0400130F RID: 4879
		public const int CopyunlockedachievementbyindexApiLatest = 1;

		// Token: 0x04001310 RID: 4880
		public const int DefinitionApiLatest = 1;

		// Token: 0x04001311 RID: 4881
		public const int Definitionv2ApiLatest = 2;

		// Token: 0x04001312 RID: 4882
		public const int GetachievementdefinitioncountApiLatest = 1;

		// Token: 0x04001313 RID: 4883
		public const int GetplayerachievementcountApiLatest = 1;

		// Token: 0x04001314 RID: 4884
		public const int GetunlockedachievementcountApiLatest = 1;

		// Token: 0x04001315 RID: 4885
		public const int PlayerachievementApiLatest = 2;

		// Token: 0x04001316 RID: 4886
		public const int PlayerstatinfoApiLatest = 1;

		// Token: 0x04001317 RID: 4887
		public const int QuerydefinitionsApiLatest = 3;

		// Token: 0x04001318 RID: 4888
		public const int QueryplayerachievementsApiLatest = 2;

		// Token: 0x04001319 RID: 4889
		public const int StatthresholdApiLatest = 1;

		// Token: 0x0400131A RID: 4890
		public const int StatthresholdsApiLatest = 1;

		// Token: 0x0400131B RID: 4891
		public const int UnlockachievementsApiLatest = 1;

		// Token: 0x0400131C RID: 4892
		public const int UnlockedachievementApiLatest = 1;
	}
}
