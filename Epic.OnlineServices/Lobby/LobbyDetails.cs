using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000365 RID: 869
	public sealed class LobbyDetails : Handle
	{
		// Token: 0x06001700 RID: 5888 RVA: 0x0002229E File Offset: 0x0002049E
		public LobbyDetails()
		{
		}

		// Token: 0x06001701 RID: 5889 RVA: 0x000222A8 File Offset: 0x000204A8
		public LobbyDetails(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06001702 RID: 5890 RVA: 0x000222B4 File Offset: 0x000204B4
		public Result CopyAttributeByIndex(ref LobbyDetailsCopyAttributeByIndexOptions options, out Attribute? outAttribute)
		{
			LobbyDetailsCopyAttributeByIndexOptionsInternal lobbyDetailsCopyAttributeByIndexOptionsInternal = default(LobbyDetailsCopyAttributeByIndexOptionsInternal);
			lobbyDetailsCopyAttributeByIndexOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_LobbyDetails_CopyAttributeByIndex(base.InnerHandle, ref lobbyDetailsCopyAttributeByIndexOptionsInternal, ref zero);
			Helper.Dispose<LobbyDetailsCopyAttributeByIndexOptionsInternal>(ref lobbyDetailsCopyAttributeByIndexOptionsInternal);
			Helper.Get<AttributeInternal, Attribute>(zero, out outAttribute);
			bool flag = outAttribute != null;
			if (flag)
			{
				Bindings.EOS_Lobby_Attribute_Release(zero);
			}
			return result;
		}

		// Token: 0x06001703 RID: 5891 RVA: 0x00022314 File Offset: 0x00020514
		public Result CopyAttributeByKey(ref LobbyDetailsCopyAttributeByKeyOptions options, out Attribute? outAttribute)
		{
			LobbyDetailsCopyAttributeByKeyOptionsInternal lobbyDetailsCopyAttributeByKeyOptionsInternal = default(LobbyDetailsCopyAttributeByKeyOptionsInternal);
			lobbyDetailsCopyAttributeByKeyOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_LobbyDetails_CopyAttributeByKey(base.InnerHandle, ref lobbyDetailsCopyAttributeByKeyOptionsInternal, ref zero);
			Helper.Dispose<LobbyDetailsCopyAttributeByKeyOptionsInternal>(ref lobbyDetailsCopyAttributeByKeyOptionsInternal);
			Helper.Get<AttributeInternal, Attribute>(zero, out outAttribute);
			bool flag = outAttribute != null;
			if (flag)
			{
				Bindings.EOS_Lobby_Attribute_Release(zero);
			}
			return result;
		}

		// Token: 0x06001704 RID: 5892 RVA: 0x00022374 File Offset: 0x00020574
		public Result CopyInfo(ref LobbyDetailsCopyInfoOptions options, out LobbyDetailsInfo? outLobbyDetailsInfo)
		{
			LobbyDetailsCopyInfoOptionsInternal lobbyDetailsCopyInfoOptionsInternal = default(LobbyDetailsCopyInfoOptionsInternal);
			lobbyDetailsCopyInfoOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_LobbyDetails_CopyInfo(base.InnerHandle, ref lobbyDetailsCopyInfoOptionsInternal, ref zero);
			Helper.Dispose<LobbyDetailsCopyInfoOptionsInternal>(ref lobbyDetailsCopyInfoOptionsInternal);
			Helper.Get<LobbyDetailsInfoInternal, LobbyDetailsInfo>(zero, out outLobbyDetailsInfo);
			bool flag = outLobbyDetailsInfo != null;
			if (flag)
			{
				Bindings.EOS_LobbyDetails_Info_Release(zero);
			}
			return result;
		}

		// Token: 0x06001705 RID: 5893 RVA: 0x000223D4 File Offset: 0x000205D4
		public Result CopyMemberAttributeByIndex(ref LobbyDetailsCopyMemberAttributeByIndexOptions options, out Attribute? outAttribute)
		{
			LobbyDetailsCopyMemberAttributeByIndexOptionsInternal lobbyDetailsCopyMemberAttributeByIndexOptionsInternal = default(LobbyDetailsCopyMemberAttributeByIndexOptionsInternal);
			lobbyDetailsCopyMemberAttributeByIndexOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_LobbyDetails_CopyMemberAttributeByIndex(base.InnerHandle, ref lobbyDetailsCopyMemberAttributeByIndexOptionsInternal, ref zero);
			Helper.Dispose<LobbyDetailsCopyMemberAttributeByIndexOptionsInternal>(ref lobbyDetailsCopyMemberAttributeByIndexOptionsInternal);
			Helper.Get<AttributeInternal, Attribute>(zero, out outAttribute);
			bool flag = outAttribute != null;
			if (flag)
			{
				Bindings.EOS_Lobby_Attribute_Release(zero);
			}
			return result;
		}

		// Token: 0x06001706 RID: 5894 RVA: 0x00022434 File Offset: 0x00020634
		public Result CopyMemberAttributeByKey(ref LobbyDetailsCopyMemberAttributeByKeyOptions options, out Attribute? outAttribute)
		{
			LobbyDetailsCopyMemberAttributeByKeyOptionsInternal lobbyDetailsCopyMemberAttributeByKeyOptionsInternal = default(LobbyDetailsCopyMemberAttributeByKeyOptionsInternal);
			lobbyDetailsCopyMemberAttributeByKeyOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_LobbyDetails_CopyMemberAttributeByKey(base.InnerHandle, ref lobbyDetailsCopyMemberAttributeByKeyOptionsInternal, ref zero);
			Helper.Dispose<LobbyDetailsCopyMemberAttributeByKeyOptionsInternal>(ref lobbyDetailsCopyMemberAttributeByKeyOptionsInternal);
			Helper.Get<AttributeInternal, Attribute>(zero, out outAttribute);
			bool flag = outAttribute != null;
			if (flag)
			{
				Bindings.EOS_Lobby_Attribute_Release(zero);
			}
			return result;
		}

		// Token: 0x06001707 RID: 5895 RVA: 0x00022494 File Offset: 0x00020694
		public uint GetAttributeCount(ref LobbyDetailsGetAttributeCountOptions options)
		{
			LobbyDetailsGetAttributeCountOptionsInternal lobbyDetailsGetAttributeCountOptionsInternal = default(LobbyDetailsGetAttributeCountOptionsInternal);
			lobbyDetailsGetAttributeCountOptionsInternal.Set(ref options);
			uint result = Bindings.EOS_LobbyDetails_GetAttributeCount(base.InnerHandle, ref lobbyDetailsGetAttributeCountOptionsInternal);
			Helper.Dispose<LobbyDetailsGetAttributeCountOptionsInternal>(ref lobbyDetailsGetAttributeCountOptionsInternal);
			return result;
		}

		// Token: 0x06001708 RID: 5896 RVA: 0x000224D0 File Offset: 0x000206D0
		public ProductUserId GetLobbyOwner(ref LobbyDetailsGetLobbyOwnerOptions options)
		{
			LobbyDetailsGetLobbyOwnerOptionsInternal lobbyDetailsGetLobbyOwnerOptionsInternal = default(LobbyDetailsGetLobbyOwnerOptionsInternal);
			lobbyDetailsGetLobbyOwnerOptionsInternal.Set(ref options);
			IntPtr from = Bindings.EOS_LobbyDetails_GetLobbyOwner(base.InnerHandle, ref lobbyDetailsGetLobbyOwnerOptionsInternal);
			Helper.Dispose<LobbyDetailsGetLobbyOwnerOptionsInternal>(ref lobbyDetailsGetLobbyOwnerOptionsInternal);
			ProductUserId result;
			Helper.Get<ProductUserId>(from, out result);
			return result;
		}

		// Token: 0x06001709 RID: 5897 RVA: 0x00022514 File Offset: 0x00020714
		public uint GetMemberAttributeCount(ref LobbyDetailsGetMemberAttributeCountOptions options)
		{
			LobbyDetailsGetMemberAttributeCountOptionsInternal lobbyDetailsGetMemberAttributeCountOptionsInternal = default(LobbyDetailsGetMemberAttributeCountOptionsInternal);
			lobbyDetailsGetMemberAttributeCountOptionsInternal.Set(ref options);
			uint result = Bindings.EOS_LobbyDetails_GetMemberAttributeCount(base.InnerHandle, ref lobbyDetailsGetMemberAttributeCountOptionsInternal);
			Helper.Dispose<LobbyDetailsGetMemberAttributeCountOptionsInternal>(ref lobbyDetailsGetMemberAttributeCountOptionsInternal);
			return result;
		}

		// Token: 0x0600170A RID: 5898 RVA: 0x00022550 File Offset: 0x00020750
		public ProductUserId GetMemberByIndex(ref LobbyDetailsGetMemberByIndexOptions options)
		{
			LobbyDetailsGetMemberByIndexOptionsInternal lobbyDetailsGetMemberByIndexOptionsInternal = default(LobbyDetailsGetMemberByIndexOptionsInternal);
			lobbyDetailsGetMemberByIndexOptionsInternal.Set(ref options);
			IntPtr from = Bindings.EOS_LobbyDetails_GetMemberByIndex(base.InnerHandle, ref lobbyDetailsGetMemberByIndexOptionsInternal);
			Helper.Dispose<LobbyDetailsGetMemberByIndexOptionsInternal>(ref lobbyDetailsGetMemberByIndexOptionsInternal);
			ProductUserId result;
			Helper.Get<ProductUserId>(from, out result);
			return result;
		}

		// Token: 0x0600170B RID: 5899 RVA: 0x00022594 File Offset: 0x00020794
		public uint GetMemberCount(ref LobbyDetailsGetMemberCountOptions options)
		{
			LobbyDetailsGetMemberCountOptionsInternal lobbyDetailsGetMemberCountOptionsInternal = default(LobbyDetailsGetMemberCountOptionsInternal);
			lobbyDetailsGetMemberCountOptionsInternal.Set(ref options);
			uint result = Bindings.EOS_LobbyDetails_GetMemberCount(base.InnerHandle, ref lobbyDetailsGetMemberCountOptionsInternal);
			Helper.Dispose<LobbyDetailsGetMemberCountOptionsInternal>(ref lobbyDetailsGetMemberCountOptionsInternal);
			return result;
		}

		// Token: 0x0600170C RID: 5900 RVA: 0x000225CE File Offset: 0x000207CE
		public void Release()
		{
			Bindings.EOS_LobbyDetails_Release(base.InnerHandle);
		}

		// Token: 0x04000A6F RID: 2671
		public const int LobbydetailsCopyattributebyindexApiLatest = 1;

		// Token: 0x04000A70 RID: 2672
		public const int LobbydetailsCopyattributebykeyApiLatest = 1;

		// Token: 0x04000A71 RID: 2673
		public const int LobbydetailsCopyinfoApiLatest = 1;

		// Token: 0x04000A72 RID: 2674
		public const int LobbydetailsCopymemberattributebyindexApiLatest = 1;

		// Token: 0x04000A73 RID: 2675
		public const int LobbydetailsCopymemberattributebykeyApiLatest = 1;

		// Token: 0x04000A74 RID: 2676
		public const int LobbydetailsGetattributecountApiLatest = 1;

		// Token: 0x04000A75 RID: 2677
		public const int LobbydetailsGetlobbyownerApiLatest = 1;

		// Token: 0x04000A76 RID: 2678
		public const int LobbydetailsGetmemberattributecountApiLatest = 1;

		// Token: 0x04000A77 RID: 2679
		public const int LobbydetailsGetmemberbyindexApiLatest = 1;

		// Token: 0x04000A78 RID: 2680
		public const int LobbydetailsGetmembercountApiLatest = 1;

		// Token: 0x04000A79 RID: 2681
		public const int LobbydetailsInfoApiLatest = 2;
	}
}
