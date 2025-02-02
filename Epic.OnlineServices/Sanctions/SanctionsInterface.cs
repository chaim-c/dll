using System;

namespace Epic.OnlineServices.Sanctions
{
	// Token: 0x02000172 RID: 370
	public sealed class SanctionsInterface : Handle
	{
		// Token: 0x06000A8E RID: 2702 RVA: 0x0000FBC6 File Offset: 0x0000DDC6
		public SanctionsInterface()
		{
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x0000FBD0 File Offset: 0x0000DDD0
		public SanctionsInterface(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x0000FBDC File Offset: 0x0000DDDC
		public Result CopyPlayerSanctionByIndex(ref CopyPlayerSanctionByIndexOptions options, out PlayerSanction? outSanction)
		{
			CopyPlayerSanctionByIndexOptionsInternal copyPlayerSanctionByIndexOptionsInternal = default(CopyPlayerSanctionByIndexOptionsInternal);
			copyPlayerSanctionByIndexOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_Sanctions_CopyPlayerSanctionByIndex(base.InnerHandle, ref copyPlayerSanctionByIndexOptionsInternal, ref zero);
			Helper.Dispose<CopyPlayerSanctionByIndexOptionsInternal>(ref copyPlayerSanctionByIndexOptionsInternal);
			Helper.Get<PlayerSanctionInternal, PlayerSanction>(zero, out outSanction);
			bool flag = outSanction != null;
			if (flag)
			{
				Bindings.EOS_Sanctions_PlayerSanction_Release(zero);
			}
			return result;
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x0000FC3C File Offset: 0x0000DE3C
		public uint GetPlayerSanctionCount(ref GetPlayerSanctionCountOptions options)
		{
			GetPlayerSanctionCountOptionsInternal getPlayerSanctionCountOptionsInternal = default(GetPlayerSanctionCountOptionsInternal);
			getPlayerSanctionCountOptionsInternal.Set(ref options);
			uint result = Bindings.EOS_Sanctions_GetPlayerSanctionCount(base.InnerHandle, ref getPlayerSanctionCountOptionsInternal);
			Helper.Dispose<GetPlayerSanctionCountOptionsInternal>(ref getPlayerSanctionCountOptionsInternal);
			return result;
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x0000FC78 File Offset: 0x0000DE78
		public void QueryActivePlayerSanctions(ref QueryActivePlayerSanctionsOptions options, object clientData, OnQueryActivePlayerSanctionsCallback completionDelegate)
		{
			QueryActivePlayerSanctionsOptionsInternal queryActivePlayerSanctionsOptionsInternal = default(QueryActivePlayerSanctionsOptionsInternal);
			queryActivePlayerSanctionsOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnQueryActivePlayerSanctionsCallbackInternal onQueryActivePlayerSanctionsCallbackInternal = new OnQueryActivePlayerSanctionsCallbackInternal(SanctionsInterface.OnQueryActivePlayerSanctionsCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onQueryActivePlayerSanctionsCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Sanctions_QueryActivePlayerSanctions(base.InnerHandle, ref queryActivePlayerSanctionsOptionsInternal, zero, onQueryActivePlayerSanctionsCallbackInternal);
			Helper.Dispose<QueryActivePlayerSanctionsOptionsInternal>(ref queryActivePlayerSanctionsOptionsInternal);
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x0000FCD4 File Offset: 0x0000DED4
		[MonoPInvokeCallback(typeof(OnQueryActivePlayerSanctionsCallbackInternal))]
		internal static void OnQueryActivePlayerSanctionsCallbackInternalImplementation(ref QueryActivePlayerSanctionsCallbackInfoInternal data)
		{
			OnQueryActivePlayerSanctionsCallback onQueryActivePlayerSanctionsCallback;
			QueryActivePlayerSanctionsCallbackInfo queryActivePlayerSanctionsCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<QueryActivePlayerSanctionsCallbackInfoInternal, OnQueryActivePlayerSanctionsCallback, QueryActivePlayerSanctionsCallbackInfo>(ref data, out onQueryActivePlayerSanctionsCallback, out queryActivePlayerSanctionsCallbackInfo);
			if (flag)
			{
				onQueryActivePlayerSanctionsCallback(ref queryActivePlayerSanctionsCallbackInfo);
			}
		}

		// Token: 0x040004DD RID: 1245
		public const int CopyplayersanctionbyindexApiLatest = 1;

		// Token: 0x040004DE RID: 1246
		public const int GetplayersanctioncountApiLatest = 1;

		// Token: 0x040004DF RID: 1247
		public const int PlayersanctionApiLatest = 2;

		// Token: 0x040004E0 RID: 1248
		public const int QueryactiveplayersanctionsApiLatest = 2;
	}
}
