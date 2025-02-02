using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004AA RID: 1194
	public sealed class EcomInterface : Handle
	{
		// Token: 0x06001ECE RID: 7886 RVA: 0x0002D946 File Offset: 0x0002BB46
		public EcomInterface()
		{
		}

		// Token: 0x06001ECF RID: 7887 RVA: 0x0002D950 File Offset: 0x0002BB50
		public EcomInterface(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06001ED0 RID: 7888 RVA: 0x0002D95C File Offset: 0x0002BB5C
		public void Checkout(ref CheckoutOptions options, object clientData, OnCheckoutCallback completionDelegate)
		{
			CheckoutOptionsInternal checkoutOptionsInternal = default(CheckoutOptionsInternal);
			checkoutOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnCheckoutCallbackInternal onCheckoutCallbackInternal = new OnCheckoutCallbackInternal(EcomInterface.OnCheckoutCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onCheckoutCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Ecom_Checkout(base.InnerHandle, ref checkoutOptionsInternal, zero, onCheckoutCallbackInternal);
			Helper.Dispose<CheckoutOptionsInternal>(ref checkoutOptionsInternal);
		}

		// Token: 0x06001ED1 RID: 7889 RVA: 0x0002D9B8 File Offset: 0x0002BBB8
		public Result CopyEntitlementById(ref CopyEntitlementByIdOptions options, out Entitlement? outEntitlement)
		{
			CopyEntitlementByIdOptionsInternal copyEntitlementByIdOptionsInternal = default(CopyEntitlementByIdOptionsInternal);
			copyEntitlementByIdOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_Ecom_CopyEntitlementById(base.InnerHandle, ref copyEntitlementByIdOptionsInternal, ref zero);
			Helper.Dispose<CopyEntitlementByIdOptionsInternal>(ref copyEntitlementByIdOptionsInternal);
			Helper.Get<EntitlementInternal, Entitlement>(zero, out outEntitlement);
			bool flag = outEntitlement != null;
			if (flag)
			{
				Bindings.EOS_Ecom_Entitlement_Release(zero);
			}
			return result;
		}

		// Token: 0x06001ED2 RID: 7890 RVA: 0x0002DA18 File Offset: 0x0002BC18
		public Result CopyEntitlementByIndex(ref CopyEntitlementByIndexOptions options, out Entitlement? outEntitlement)
		{
			CopyEntitlementByIndexOptionsInternal copyEntitlementByIndexOptionsInternal = default(CopyEntitlementByIndexOptionsInternal);
			copyEntitlementByIndexOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_Ecom_CopyEntitlementByIndex(base.InnerHandle, ref copyEntitlementByIndexOptionsInternal, ref zero);
			Helper.Dispose<CopyEntitlementByIndexOptionsInternal>(ref copyEntitlementByIndexOptionsInternal);
			Helper.Get<EntitlementInternal, Entitlement>(zero, out outEntitlement);
			bool flag = outEntitlement != null;
			if (flag)
			{
				Bindings.EOS_Ecom_Entitlement_Release(zero);
			}
			return result;
		}

		// Token: 0x06001ED3 RID: 7891 RVA: 0x0002DA78 File Offset: 0x0002BC78
		public Result CopyEntitlementByNameAndIndex(ref CopyEntitlementByNameAndIndexOptions options, out Entitlement? outEntitlement)
		{
			CopyEntitlementByNameAndIndexOptionsInternal copyEntitlementByNameAndIndexOptionsInternal = default(CopyEntitlementByNameAndIndexOptionsInternal);
			copyEntitlementByNameAndIndexOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_Ecom_CopyEntitlementByNameAndIndex(base.InnerHandle, ref copyEntitlementByNameAndIndexOptionsInternal, ref zero);
			Helper.Dispose<CopyEntitlementByNameAndIndexOptionsInternal>(ref copyEntitlementByNameAndIndexOptionsInternal);
			Helper.Get<EntitlementInternal, Entitlement>(zero, out outEntitlement);
			bool flag = outEntitlement != null;
			if (flag)
			{
				Bindings.EOS_Ecom_Entitlement_Release(zero);
			}
			return result;
		}

		// Token: 0x06001ED4 RID: 7892 RVA: 0x0002DAD8 File Offset: 0x0002BCD8
		public Result CopyItemById(ref CopyItemByIdOptions options, out CatalogItem? outItem)
		{
			CopyItemByIdOptionsInternal copyItemByIdOptionsInternal = default(CopyItemByIdOptionsInternal);
			copyItemByIdOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_Ecom_CopyItemById(base.InnerHandle, ref copyItemByIdOptionsInternal, ref zero);
			Helper.Dispose<CopyItemByIdOptionsInternal>(ref copyItemByIdOptionsInternal);
			Helper.Get<CatalogItemInternal, CatalogItem>(zero, out outItem);
			bool flag = outItem != null;
			if (flag)
			{
				Bindings.EOS_Ecom_CatalogItem_Release(zero);
			}
			return result;
		}

		// Token: 0x06001ED5 RID: 7893 RVA: 0x0002DB38 File Offset: 0x0002BD38
		public Result CopyItemImageInfoByIndex(ref CopyItemImageInfoByIndexOptions options, out KeyImageInfo? outImageInfo)
		{
			CopyItemImageInfoByIndexOptionsInternal copyItemImageInfoByIndexOptionsInternal = default(CopyItemImageInfoByIndexOptionsInternal);
			copyItemImageInfoByIndexOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_Ecom_CopyItemImageInfoByIndex(base.InnerHandle, ref copyItemImageInfoByIndexOptionsInternal, ref zero);
			Helper.Dispose<CopyItemImageInfoByIndexOptionsInternal>(ref copyItemImageInfoByIndexOptionsInternal);
			Helper.Get<KeyImageInfoInternal, KeyImageInfo>(zero, out outImageInfo);
			bool flag = outImageInfo != null;
			if (flag)
			{
				Bindings.EOS_Ecom_KeyImageInfo_Release(zero);
			}
			return result;
		}

		// Token: 0x06001ED6 RID: 7894 RVA: 0x0002DB98 File Offset: 0x0002BD98
		public Result CopyItemReleaseByIndex(ref CopyItemReleaseByIndexOptions options, out CatalogRelease? outRelease)
		{
			CopyItemReleaseByIndexOptionsInternal copyItemReleaseByIndexOptionsInternal = default(CopyItemReleaseByIndexOptionsInternal);
			copyItemReleaseByIndexOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_Ecom_CopyItemReleaseByIndex(base.InnerHandle, ref copyItemReleaseByIndexOptionsInternal, ref zero);
			Helper.Dispose<CopyItemReleaseByIndexOptionsInternal>(ref copyItemReleaseByIndexOptionsInternal);
			Helper.Get<CatalogReleaseInternal, CatalogRelease>(zero, out outRelease);
			bool flag = outRelease != null;
			if (flag)
			{
				Bindings.EOS_Ecom_CatalogRelease_Release(zero);
			}
			return result;
		}

		// Token: 0x06001ED7 RID: 7895 RVA: 0x0002DBF8 File Offset: 0x0002BDF8
		public Result CopyLastRedeemedEntitlementByIndex(ref CopyLastRedeemedEntitlementByIndexOptions options, out Utf8String outRedeemedEntitlementId)
		{
			CopyLastRedeemedEntitlementByIndexOptionsInternal copyLastRedeemedEntitlementByIndexOptionsInternal = default(CopyLastRedeemedEntitlementByIndexOptionsInternal);
			copyLastRedeemedEntitlementByIndexOptionsInternal.Set(ref options);
			int size = 33;
			IntPtr intPtr = Helper.AddAllocation(size);
			Result result = Bindings.EOS_Ecom_CopyLastRedeemedEntitlementByIndex(base.InnerHandle, ref copyLastRedeemedEntitlementByIndexOptionsInternal, intPtr, ref size);
			Helper.Dispose<CopyLastRedeemedEntitlementByIndexOptionsInternal>(ref copyLastRedeemedEntitlementByIndexOptionsInternal);
			Helper.Get(intPtr, out outRedeemedEntitlementId);
			Helper.Dispose(ref intPtr);
			return result;
		}

		// Token: 0x06001ED8 RID: 7896 RVA: 0x0002DC54 File Offset: 0x0002BE54
		public Result CopyOfferById(ref CopyOfferByIdOptions options, out CatalogOffer? outOffer)
		{
			CopyOfferByIdOptionsInternal copyOfferByIdOptionsInternal = default(CopyOfferByIdOptionsInternal);
			copyOfferByIdOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_Ecom_CopyOfferById(base.InnerHandle, ref copyOfferByIdOptionsInternal, ref zero);
			Helper.Dispose<CopyOfferByIdOptionsInternal>(ref copyOfferByIdOptionsInternal);
			Helper.Get<CatalogOfferInternal, CatalogOffer>(zero, out outOffer);
			bool flag = outOffer != null;
			if (flag)
			{
				Bindings.EOS_Ecom_CatalogOffer_Release(zero);
			}
			return result;
		}

		// Token: 0x06001ED9 RID: 7897 RVA: 0x0002DCB4 File Offset: 0x0002BEB4
		public Result CopyOfferByIndex(ref CopyOfferByIndexOptions options, out CatalogOffer? outOffer)
		{
			CopyOfferByIndexOptionsInternal copyOfferByIndexOptionsInternal = default(CopyOfferByIndexOptionsInternal);
			copyOfferByIndexOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_Ecom_CopyOfferByIndex(base.InnerHandle, ref copyOfferByIndexOptionsInternal, ref zero);
			Helper.Dispose<CopyOfferByIndexOptionsInternal>(ref copyOfferByIndexOptionsInternal);
			Helper.Get<CatalogOfferInternal, CatalogOffer>(zero, out outOffer);
			bool flag = outOffer != null;
			if (flag)
			{
				Bindings.EOS_Ecom_CatalogOffer_Release(zero);
			}
			return result;
		}

		// Token: 0x06001EDA RID: 7898 RVA: 0x0002DD14 File Offset: 0x0002BF14
		public Result CopyOfferImageInfoByIndex(ref CopyOfferImageInfoByIndexOptions options, out KeyImageInfo? outImageInfo)
		{
			CopyOfferImageInfoByIndexOptionsInternal copyOfferImageInfoByIndexOptionsInternal = default(CopyOfferImageInfoByIndexOptionsInternal);
			copyOfferImageInfoByIndexOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_Ecom_CopyOfferImageInfoByIndex(base.InnerHandle, ref copyOfferImageInfoByIndexOptionsInternal, ref zero);
			Helper.Dispose<CopyOfferImageInfoByIndexOptionsInternal>(ref copyOfferImageInfoByIndexOptionsInternal);
			Helper.Get<KeyImageInfoInternal, KeyImageInfo>(zero, out outImageInfo);
			bool flag = outImageInfo != null;
			if (flag)
			{
				Bindings.EOS_Ecom_KeyImageInfo_Release(zero);
			}
			return result;
		}

		// Token: 0x06001EDB RID: 7899 RVA: 0x0002DD74 File Offset: 0x0002BF74
		public Result CopyOfferItemByIndex(ref CopyOfferItemByIndexOptions options, out CatalogItem? outItem)
		{
			CopyOfferItemByIndexOptionsInternal copyOfferItemByIndexOptionsInternal = default(CopyOfferItemByIndexOptionsInternal);
			copyOfferItemByIndexOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_Ecom_CopyOfferItemByIndex(base.InnerHandle, ref copyOfferItemByIndexOptionsInternal, ref zero);
			Helper.Dispose<CopyOfferItemByIndexOptionsInternal>(ref copyOfferItemByIndexOptionsInternal);
			Helper.Get<CatalogItemInternal, CatalogItem>(zero, out outItem);
			bool flag = outItem != null;
			if (flag)
			{
				Bindings.EOS_Ecom_CatalogItem_Release(zero);
			}
			return result;
		}

		// Token: 0x06001EDC RID: 7900 RVA: 0x0002DDD4 File Offset: 0x0002BFD4
		public Result CopyTransactionById(ref CopyTransactionByIdOptions options, out Transaction outTransaction)
		{
			CopyTransactionByIdOptionsInternal copyTransactionByIdOptionsInternal = default(CopyTransactionByIdOptionsInternal);
			copyTransactionByIdOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_Ecom_CopyTransactionById(base.InnerHandle, ref copyTransactionByIdOptionsInternal, ref zero);
			Helper.Dispose<CopyTransactionByIdOptionsInternal>(ref copyTransactionByIdOptionsInternal);
			Helper.Get<Transaction>(zero, out outTransaction);
			return result;
		}

		// Token: 0x06001EDD RID: 7901 RVA: 0x0002DE20 File Offset: 0x0002C020
		public Result CopyTransactionByIndex(ref CopyTransactionByIndexOptions options, out Transaction outTransaction)
		{
			CopyTransactionByIndexOptionsInternal copyTransactionByIndexOptionsInternal = default(CopyTransactionByIndexOptionsInternal);
			copyTransactionByIndexOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_Ecom_CopyTransactionByIndex(base.InnerHandle, ref copyTransactionByIndexOptionsInternal, ref zero);
			Helper.Dispose<CopyTransactionByIndexOptionsInternal>(ref copyTransactionByIndexOptionsInternal);
			Helper.Get<Transaction>(zero, out outTransaction);
			return result;
		}

		// Token: 0x06001EDE RID: 7902 RVA: 0x0002DE6C File Offset: 0x0002C06C
		public uint GetEntitlementsByNameCount(ref GetEntitlementsByNameCountOptions options)
		{
			GetEntitlementsByNameCountOptionsInternal getEntitlementsByNameCountOptionsInternal = default(GetEntitlementsByNameCountOptionsInternal);
			getEntitlementsByNameCountOptionsInternal.Set(ref options);
			uint result = Bindings.EOS_Ecom_GetEntitlementsByNameCount(base.InnerHandle, ref getEntitlementsByNameCountOptionsInternal);
			Helper.Dispose<GetEntitlementsByNameCountOptionsInternal>(ref getEntitlementsByNameCountOptionsInternal);
			return result;
		}

		// Token: 0x06001EDF RID: 7903 RVA: 0x0002DEA8 File Offset: 0x0002C0A8
		public uint GetEntitlementsCount(ref GetEntitlementsCountOptions options)
		{
			GetEntitlementsCountOptionsInternal getEntitlementsCountOptionsInternal = default(GetEntitlementsCountOptionsInternal);
			getEntitlementsCountOptionsInternal.Set(ref options);
			uint result = Bindings.EOS_Ecom_GetEntitlementsCount(base.InnerHandle, ref getEntitlementsCountOptionsInternal);
			Helper.Dispose<GetEntitlementsCountOptionsInternal>(ref getEntitlementsCountOptionsInternal);
			return result;
		}

		// Token: 0x06001EE0 RID: 7904 RVA: 0x0002DEE4 File Offset: 0x0002C0E4
		public uint GetItemImageInfoCount(ref GetItemImageInfoCountOptions options)
		{
			GetItemImageInfoCountOptionsInternal getItemImageInfoCountOptionsInternal = default(GetItemImageInfoCountOptionsInternal);
			getItemImageInfoCountOptionsInternal.Set(ref options);
			uint result = Bindings.EOS_Ecom_GetItemImageInfoCount(base.InnerHandle, ref getItemImageInfoCountOptionsInternal);
			Helper.Dispose<GetItemImageInfoCountOptionsInternal>(ref getItemImageInfoCountOptionsInternal);
			return result;
		}

		// Token: 0x06001EE1 RID: 7905 RVA: 0x0002DF20 File Offset: 0x0002C120
		public uint GetItemReleaseCount(ref GetItemReleaseCountOptions options)
		{
			GetItemReleaseCountOptionsInternal getItemReleaseCountOptionsInternal = default(GetItemReleaseCountOptionsInternal);
			getItemReleaseCountOptionsInternal.Set(ref options);
			uint result = Bindings.EOS_Ecom_GetItemReleaseCount(base.InnerHandle, ref getItemReleaseCountOptionsInternal);
			Helper.Dispose<GetItemReleaseCountOptionsInternal>(ref getItemReleaseCountOptionsInternal);
			return result;
		}

		// Token: 0x06001EE2 RID: 7906 RVA: 0x0002DF5C File Offset: 0x0002C15C
		public uint GetLastRedeemedEntitlementsCount(ref GetLastRedeemedEntitlementsCountOptions options)
		{
			GetLastRedeemedEntitlementsCountOptionsInternal getLastRedeemedEntitlementsCountOptionsInternal = default(GetLastRedeemedEntitlementsCountOptionsInternal);
			getLastRedeemedEntitlementsCountOptionsInternal.Set(ref options);
			uint result = Bindings.EOS_Ecom_GetLastRedeemedEntitlementsCount(base.InnerHandle, ref getLastRedeemedEntitlementsCountOptionsInternal);
			Helper.Dispose<GetLastRedeemedEntitlementsCountOptionsInternal>(ref getLastRedeemedEntitlementsCountOptionsInternal);
			return result;
		}

		// Token: 0x06001EE3 RID: 7907 RVA: 0x0002DF98 File Offset: 0x0002C198
		public uint GetOfferCount(ref GetOfferCountOptions options)
		{
			GetOfferCountOptionsInternal getOfferCountOptionsInternal = default(GetOfferCountOptionsInternal);
			getOfferCountOptionsInternal.Set(ref options);
			uint result = Bindings.EOS_Ecom_GetOfferCount(base.InnerHandle, ref getOfferCountOptionsInternal);
			Helper.Dispose<GetOfferCountOptionsInternal>(ref getOfferCountOptionsInternal);
			return result;
		}

		// Token: 0x06001EE4 RID: 7908 RVA: 0x0002DFD4 File Offset: 0x0002C1D4
		public uint GetOfferImageInfoCount(ref GetOfferImageInfoCountOptions options)
		{
			GetOfferImageInfoCountOptionsInternal getOfferImageInfoCountOptionsInternal = default(GetOfferImageInfoCountOptionsInternal);
			getOfferImageInfoCountOptionsInternal.Set(ref options);
			uint result = Bindings.EOS_Ecom_GetOfferImageInfoCount(base.InnerHandle, ref getOfferImageInfoCountOptionsInternal);
			Helper.Dispose<GetOfferImageInfoCountOptionsInternal>(ref getOfferImageInfoCountOptionsInternal);
			return result;
		}

		// Token: 0x06001EE5 RID: 7909 RVA: 0x0002E010 File Offset: 0x0002C210
		public uint GetOfferItemCount(ref GetOfferItemCountOptions options)
		{
			GetOfferItemCountOptionsInternal getOfferItemCountOptionsInternal = default(GetOfferItemCountOptionsInternal);
			getOfferItemCountOptionsInternal.Set(ref options);
			uint result = Bindings.EOS_Ecom_GetOfferItemCount(base.InnerHandle, ref getOfferItemCountOptionsInternal);
			Helper.Dispose<GetOfferItemCountOptionsInternal>(ref getOfferItemCountOptionsInternal);
			return result;
		}

		// Token: 0x06001EE6 RID: 7910 RVA: 0x0002E04C File Offset: 0x0002C24C
		public uint GetTransactionCount(ref GetTransactionCountOptions options)
		{
			GetTransactionCountOptionsInternal getTransactionCountOptionsInternal = default(GetTransactionCountOptionsInternal);
			getTransactionCountOptionsInternal.Set(ref options);
			uint result = Bindings.EOS_Ecom_GetTransactionCount(base.InnerHandle, ref getTransactionCountOptionsInternal);
			Helper.Dispose<GetTransactionCountOptionsInternal>(ref getTransactionCountOptionsInternal);
			return result;
		}

		// Token: 0x06001EE7 RID: 7911 RVA: 0x0002E088 File Offset: 0x0002C288
		public void QueryEntitlementToken(ref QueryEntitlementTokenOptions options, object clientData, OnQueryEntitlementTokenCallback completionDelegate)
		{
			QueryEntitlementTokenOptionsInternal queryEntitlementTokenOptionsInternal = default(QueryEntitlementTokenOptionsInternal);
			queryEntitlementTokenOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnQueryEntitlementTokenCallbackInternal onQueryEntitlementTokenCallbackInternal = new OnQueryEntitlementTokenCallbackInternal(EcomInterface.OnQueryEntitlementTokenCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onQueryEntitlementTokenCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Ecom_QueryEntitlementToken(base.InnerHandle, ref queryEntitlementTokenOptionsInternal, zero, onQueryEntitlementTokenCallbackInternal);
			Helper.Dispose<QueryEntitlementTokenOptionsInternal>(ref queryEntitlementTokenOptionsInternal);
		}

		// Token: 0x06001EE8 RID: 7912 RVA: 0x0002E0E4 File Offset: 0x0002C2E4
		public void QueryEntitlements(ref QueryEntitlementsOptions options, object clientData, OnQueryEntitlementsCallback completionDelegate)
		{
			QueryEntitlementsOptionsInternal queryEntitlementsOptionsInternal = default(QueryEntitlementsOptionsInternal);
			queryEntitlementsOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnQueryEntitlementsCallbackInternal onQueryEntitlementsCallbackInternal = new OnQueryEntitlementsCallbackInternal(EcomInterface.OnQueryEntitlementsCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onQueryEntitlementsCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Ecom_QueryEntitlements(base.InnerHandle, ref queryEntitlementsOptionsInternal, zero, onQueryEntitlementsCallbackInternal);
			Helper.Dispose<QueryEntitlementsOptionsInternal>(ref queryEntitlementsOptionsInternal);
		}

		// Token: 0x06001EE9 RID: 7913 RVA: 0x0002E140 File Offset: 0x0002C340
		public void QueryOffers(ref QueryOffersOptions options, object clientData, OnQueryOffersCallback completionDelegate)
		{
			QueryOffersOptionsInternal queryOffersOptionsInternal = default(QueryOffersOptionsInternal);
			queryOffersOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnQueryOffersCallbackInternal onQueryOffersCallbackInternal = new OnQueryOffersCallbackInternal(EcomInterface.OnQueryOffersCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onQueryOffersCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Ecom_QueryOffers(base.InnerHandle, ref queryOffersOptionsInternal, zero, onQueryOffersCallbackInternal);
			Helper.Dispose<QueryOffersOptionsInternal>(ref queryOffersOptionsInternal);
		}

		// Token: 0x06001EEA RID: 7914 RVA: 0x0002E19C File Offset: 0x0002C39C
		public void QueryOwnership(ref QueryOwnershipOptions options, object clientData, OnQueryOwnershipCallback completionDelegate)
		{
			QueryOwnershipOptionsInternal queryOwnershipOptionsInternal = default(QueryOwnershipOptionsInternal);
			queryOwnershipOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnQueryOwnershipCallbackInternal onQueryOwnershipCallbackInternal = new OnQueryOwnershipCallbackInternal(EcomInterface.OnQueryOwnershipCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onQueryOwnershipCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Ecom_QueryOwnership(base.InnerHandle, ref queryOwnershipOptionsInternal, zero, onQueryOwnershipCallbackInternal);
			Helper.Dispose<QueryOwnershipOptionsInternal>(ref queryOwnershipOptionsInternal);
		}

		// Token: 0x06001EEB RID: 7915 RVA: 0x0002E1F8 File Offset: 0x0002C3F8
		public void QueryOwnershipToken(ref QueryOwnershipTokenOptions options, object clientData, OnQueryOwnershipTokenCallback completionDelegate)
		{
			QueryOwnershipTokenOptionsInternal queryOwnershipTokenOptionsInternal = default(QueryOwnershipTokenOptionsInternal);
			queryOwnershipTokenOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnQueryOwnershipTokenCallbackInternal onQueryOwnershipTokenCallbackInternal = new OnQueryOwnershipTokenCallbackInternal(EcomInterface.OnQueryOwnershipTokenCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onQueryOwnershipTokenCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Ecom_QueryOwnershipToken(base.InnerHandle, ref queryOwnershipTokenOptionsInternal, zero, onQueryOwnershipTokenCallbackInternal);
			Helper.Dispose<QueryOwnershipTokenOptionsInternal>(ref queryOwnershipTokenOptionsInternal);
		}

		// Token: 0x06001EEC RID: 7916 RVA: 0x0002E254 File Offset: 0x0002C454
		public void RedeemEntitlements(ref RedeemEntitlementsOptions options, object clientData, OnRedeemEntitlementsCallback completionDelegate)
		{
			RedeemEntitlementsOptionsInternal redeemEntitlementsOptionsInternal = default(RedeemEntitlementsOptionsInternal);
			redeemEntitlementsOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnRedeemEntitlementsCallbackInternal onRedeemEntitlementsCallbackInternal = new OnRedeemEntitlementsCallbackInternal(EcomInterface.OnRedeemEntitlementsCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onRedeemEntitlementsCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Ecom_RedeemEntitlements(base.InnerHandle, ref redeemEntitlementsOptionsInternal, zero, onRedeemEntitlementsCallbackInternal);
			Helper.Dispose<RedeemEntitlementsOptionsInternal>(ref redeemEntitlementsOptionsInternal);
		}

		// Token: 0x06001EED RID: 7917 RVA: 0x0002E2B0 File Offset: 0x0002C4B0
		[MonoPInvokeCallback(typeof(OnCheckoutCallbackInternal))]
		internal static void OnCheckoutCallbackInternalImplementation(ref CheckoutCallbackInfoInternal data)
		{
			OnCheckoutCallback onCheckoutCallback;
			CheckoutCallbackInfo checkoutCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<CheckoutCallbackInfoInternal, OnCheckoutCallback, CheckoutCallbackInfo>(ref data, out onCheckoutCallback, out checkoutCallbackInfo);
			if (flag)
			{
				onCheckoutCallback(ref checkoutCallbackInfo);
			}
		}

		// Token: 0x06001EEE RID: 7918 RVA: 0x0002E2D8 File Offset: 0x0002C4D8
		[MonoPInvokeCallback(typeof(OnQueryEntitlementTokenCallbackInternal))]
		internal static void OnQueryEntitlementTokenCallbackInternalImplementation(ref QueryEntitlementTokenCallbackInfoInternal data)
		{
			OnQueryEntitlementTokenCallback onQueryEntitlementTokenCallback;
			QueryEntitlementTokenCallbackInfo queryEntitlementTokenCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<QueryEntitlementTokenCallbackInfoInternal, OnQueryEntitlementTokenCallback, QueryEntitlementTokenCallbackInfo>(ref data, out onQueryEntitlementTokenCallback, out queryEntitlementTokenCallbackInfo);
			if (flag)
			{
				onQueryEntitlementTokenCallback(ref queryEntitlementTokenCallbackInfo);
			}
		}

		// Token: 0x06001EEF RID: 7919 RVA: 0x0002E300 File Offset: 0x0002C500
		[MonoPInvokeCallback(typeof(OnQueryEntitlementsCallbackInternal))]
		internal static void OnQueryEntitlementsCallbackInternalImplementation(ref QueryEntitlementsCallbackInfoInternal data)
		{
			OnQueryEntitlementsCallback onQueryEntitlementsCallback;
			QueryEntitlementsCallbackInfo queryEntitlementsCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<QueryEntitlementsCallbackInfoInternal, OnQueryEntitlementsCallback, QueryEntitlementsCallbackInfo>(ref data, out onQueryEntitlementsCallback, out queryEntitlementsCallbackInfo);
			if (flag)
			{
				onQueryEntitlementsCallback(ref queryEntitlementsCallbackInfo);
			}
		}

		// Token: 0x06001EF0 RID: 7920 RVA: 0x0002E328 File Offset: 0x0002C528
		[MonoPInvokeCallback(typeof(OnQueryOffersCallbackInternal))]
		internal static void OnQueryOffersCallbackInternalImplementation(ref QueryOffersCallbackInfoInternal data)
		{
			OnQueryOffersCallback onQueryOffersCallback;
			QueryOffersCallbackInfo queryOffersCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<QueryOffersCallbackInfoInternal, OnQueryOffersCallback, QueryOffersCallbackInfo>(ref data, out onQueryOffersCallback, out queryOffersCallbackInfo);
			if (flag)
			{
				onQueryOffersCallback(ref queryOffersCallbackInfo);
			}
		}

		// Token: 0x06001EF1 RID: 7921 RVA: 0x0002E350 File Offset: 0x0002C550
		[MonoPInvokeCallback(typeof(OnQueryOwnershipCallbackInternal))]
		internal static void OnQueryOwnershipCallbackInternalImplementation(ref QueryOwnershipCallbackInfoInternal data)
		{
			OnQueryOwnershipCallback onQueryOwnershipCallback;
			QueryOwnershipCallbackInfo queryOwnershipCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<QueryOwnershipCallbackInfoInternal, OnQueryOwnershipCallback, QueryOwnershipCallbackInfo>(ref data, out onQueryOwnershipCallback, out queryOwnershipCallbackInfo);
			if (flag)
			{
				onQueryOwnershipCallback(ref queryOwnershipCallbackInfo);
			}
		}

		// Token: 0x06001EF2 RID: 7922 RVA: 0x0002E378 File Offset: 0x0002C578
		[MonoPInvokeCallback(typeof(OnQueryOwnershipTokenCallbackInternal))]
		internal static void OnQueryOwnershipTokenCallbackInternalImplementation(ref QueryOwnershipTokenCallbackInfoInternal data)
		{
			OnQueryOwnershipTokenCallback onQueryOwnershipTokenCallback;
			QueryOwnershipTokenCallbackInfo queryOwnershipTokenCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<QueryOwnershipTokenCallbackInfoInternal, OnQueryOwnershipTokenCallback, QueryOwnershipTokenCallbackInfo>(ref data, out onQueryOwnershipTokenCallback, out queryOwnershipTokenCallbackInfo);
			if (flag)
			{
				onQueryOwnershipTokenCallback(ref queryOwnershipTokenCallbackInfo);
			}
		}

		// Token: 0x06001EF3 RID: 7923 RVA: 0x0002E3A0 File Offset: 0x0002C5A0
		[MonoPInvokeCallback(typeof(OnRedeemEntitlementsCallbackInternal))]
		internal static void OnRedeemEntitlementsCallbackInternalImplementation(ref RedeemEntitlementsCallbackInfoInternal data)
		{
			OnRedeemEntitlementsCallback onRedeemEntitlementsCallback;
			RedeemEntitlementsCallbackInfo redeemEntitlementsCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<RedeemEntitlementsCallbackInfoInternal, OnRedeemEntitlementsCallback, RedeemEntitlementsCallbackInfo>(ref data, out onRedeemEntitlementsCallback, out redeemEntitlementsCallbackInfo);
			if (flag)
			{
				onRedeemEntitlementsCallback(ref redeemEntitlementsCallbackInfo);
			}
		}

		// Token: 0x04000DAB RID: 3499
		public const int CatalogitemApiLatest = 1;

		// Token: 0x04000DAC RID: 3500
		public const int CatalogitemEntitlementendtimestampUndefined = -1;

		// Token: 0x04000DAD RID: 3501
		public const int CatalogofferApiLatest = 5;

		// Token: 0x04000DAE RID: 3502
		public const int CatalogofferEffectivedatetimestampUndefined = -1;

		// Token: 0x04000DAF RID: 3503
		public const int CatalogofferExpirationtimestampUndefined = -1;

		// Token: 0x04000DB0 RID: 3504
		public const int CatalogofferReleasedatetimestampUndefined = -1;

		// Token: 0x04000DB1 RID: 3505
		public const int CatalogreleaseApiLatest = 1;

		// Token: 0x04000DB2 RID: 3506
		public const int CheckoutApiLatest = 1;

		// Token: 0x04000DB3 RID: 3507
		public const int CheckoutMaxEntries = 10;

		// Token: 0x04000DB4 RID: 3508
		public const int CheckoutentryApiLatest = 1;

		// Token: 0x04000DB5 RID: 3509
		public const int CopyentitlementbyidApiLatest = 2;

		// Token: 0x04000DB6 RID: 3510
		public const int CopyentitlementbyindexApiLatest = 1;

		// Token: 0x04000DB7 RID: 3511
		public const int CopyentitlementbynameandindexApiLatest = 1;

		// Token: 0x04000DB8 RID: 3512
		public const int CopyitembyidApiLatest = 1;

		// Token: 0x04000DB9 RID: 3513
		public const int CopyitemimageinfobyindexApiLatest = 1;

		// Token: 0x04000DBA RID: 3514
		public const int CopyitemreleasebyindexApiLatest = 1;

		// Token: 0x04000DBB RID: 3515
		public const int CopylastredeemedentitlementbyindexApiLatest = 1;

		// Token: 0x04000DBC RID: 3516
		public const int CopyofferbyidApiLatest = 3;

		// Token: 0x04000DBD RID: 3517
		public const int CopyofferbyindexApiLatest = 3;

		// Token: 0x04000DBE RID: 3518
		public const int CopyofferimageinfobyindexApiLatest = 1;

		// Token: 0x04000DBF RID: 3519
		public const int CopyofferitembyindexApiLatest = 1;

		// Token: 0x04000DC0 RID: 3520
		public const int CopytransactionbyidApiLatest = 1;

		// Token: 0x04000DC1 RID: 3521
		public const int CopytransactionbyindexApiLatest = 1;

		// Token: 0x04000DC2 RID: 3522
		public const int EntitlementApiLatest = 2;

		// Token: 0x04000DC3 RID: 3523
		public const int EntitlementEndtimestampUndefined = -1;

		// Token: 0x04000DC4 RID: 3524
		public const int EntitlementidMaxLength = 32;

		// Token: 0x04000DC5 RID: 3525
		public const int GetentitlementsbynamecountApiLatest = 1;

		// Token: 0x04000DC6 RID: 3526
		public const int GetentitlementscountApiLatest = 1;

		// Token: 0x04000DC7 RID: 3527
		public const int GetitemimageinfocountApiLatest = 1;

		// Token: 0x04000DC8 RID: 3528
		public const int GetitemreleasecountApiLatest = 1;

		// Token: 0x04000DC9 RID: 3529
		public const int GetlastredeemedentitlementscountApiLatest = 1;

		// Token: 0x04000DCA RID: 3530
		public const int GetoffercountApiLatest = 1;

		// Token: 0x04000DCB RID: 3531
		public const int GetofferimageinfocountApiLatest = 1;

		// Token: 0x04000DCC RID: 3532
		public const int GetofferitemcountApiLatest = 1;

		// Token: 0x04000DCD RID: 3533
		public const int GettransactioncountApiLatest = 1;

		// Token: 0x04000DCE RID: 3534
		public const int ItemownershipApiLatest = 1;

		// Token: 0x04000DCF RID: 3535
		public const int KeyimageinfoApiLatest = 1;

		// Token: 0x04000DD0 RID: 3536
		public const int QueryentitlementsApiLatest = 2;

		// Token: 0x04000DD1 RID: 3537
		public const int QueryentitlementsMaxEntitlementIds = 32;

		// Token: 0x04000DD2 RID: 3538
		public const int QueryentitlementtokenApiLatest = 1;

		// Token: 0x04000DD3 RID: 3539
		public const int QueryentitlementtokenMaxEntitlementIds = 32;

		// Token: 0x04000DD4 RID: 3540
		public const int QueryoffersApiLatest = 1;

		// Token: 0x04000DD5 RID: 3541
		public const int QueryownershipApiLatest = 2;

		// Token: 0x04000DD6 RID: 3542
		public const int QueryownershipMaxCatalogIds = 50;

		// Token: 0x04000DD7 RID: 3543
		public const int QueryownershiptokenApiLatest = 2;

		// Token: 0x04000DD8 RID: 3544
		public const int QueryownershiptokenMaxCatalogitemIds = 32;

		// Token: 0x04000DD9 RID: 3545
		public const int RedeementitlementsApiLatest = 2;

		// Token: 0x04000DDA RID: 3546
		public const int RedeementitlementsMaxIds = 32;

		// Token: 0x04000DDB RID: 3547
		public const int TransactionidMaximumLength = 64;
	}
}
