﻿using System;
using System.Dynamic;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000089 RID: 137
	[NullableContext(1)]
	[Nullable(0)]
	public class JsonDynamicContract : JsonContainerContract
	{
		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060006C1 RID: 1729 RVA: 0x0001C172 File Offset: 0x0001A372
		public JsonPropertyCollection Properties { get; }

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060006C2 RID: 1730 RVA: 0x0001C17A File Offset: 0x0001A37A
		// (set) Token: 0x060006C3 RID: 1731 RVA: 0x0001C182 File Offset: 0x0001A382
		[Nullable(new byte[]
		{
			2,
			1,
			1
		})]
		public Func<string, string> PropertyNameResolver { [return: Nullable(new byte[]
		{
			2,
			1,
			1
		})] get; [param: Nullable(new byte[]
		{
			2,
			1,
			1
		})] set; }

		// Token: 0x060006C4 RID: 1732 RVA: 0x0001C18B File Offset: 0x0001A38B
		private static CallSite<Func<CallSite, object, object>> CreateCallSiteGetter(string name)
		{
			return CallSite<Func<CallSite, object, object>>.Create(new NoThrowGetBinderMember((GetMemberBinder)DynamicUtils.BinderWrapper.GetMember(name, typeof(DynamicUtils))));
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x0001C1AC File Offset: 0x0001A3AC
		[return: Nullable(new byte[]
		{
			1,
			1,
			1,
			1,
			2,
			1
		})]
		private static CallSite<Func<CallSite, object, object, object>> CreateCallSiteSetter(string name)
		{
			return CallSite<Func<CallSite, object, object, object>>.Create(new NoThrowSetBinderMember((SetMemberBinder)DynamicUtils.BinderWrapper.SetMember(name, typeof(DynamicUtils))));
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x0001C1D0 File Offset: 0x0001A3D0
		public JsonDynamicContract(Type underlyingType) : base(underlyingType)
		{
			this.ContractType = JsonContractType.Dynamic;
			this.Properties = new JsonPropertyCollection(base.UnderlyingType);
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x0001C22C File Offset: 0x0001A42C
		internal bool TryGetMember(IDynamicMetaObjectProvider dynamicProvider, string name, [Nullable(2)] out object value)
		{
			ValidationUtils.ArgumentNotNull(dynamicProvider, "dynamicProvider");
			CallSite<Func<CallSite, object, object>> callSite = this._callSiteGetters.Get(name);
			object obj = callSite.Target(callSite, dynamicProvider);
			if (obj != NoThrowExpressionVisitor.ErrorResult)
			{
				value = obj;
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x0001C270 File Offset: 0x0001A470
		internal bool TrySetMember(IDynamicMetaObjectProvider dynamicProvider, string name, [Nullable(2)] object value)
		{
			ValidationUtils.ArgumentNotNull(dynamicProvider, "dynamicProvider");
			CallSite<Func<CallSite, object, object, object>> callSite = this._callSiteSetters.Get(name);
			return callSite.Target(callSite, dynamicProvider, value) != NoThrowExpressionVisitor.ErrorResult;
		}

		// Token: 0x0400026E RID: 622
		private readonly ThreadSafeStore<string, CallSite<Func<CallSite, object, object>>> _callSiteGetters = new ThreadSafeStore<string, CallSite<Func<CallSite, object, object>>>(new Func<string, CallSite<Func<CallSite, object, object>>>(JsonDynamicContract.CreateCallSiteGetter));

		// Token: 0x0400026F RID: 623
		[Nullable(new byte[]
		{
			1,
			1,
			1,
			1,
			1,
			1,
			2,
			1
		})]
		private readonly ThreadSafeStore<string, CallSite<Func<CallSite, object, object, object>>> _callSiteSetters = new ThreadSafeStore<string, CallSite<Func<CallSite, object, object, object>>>(new Func<string, CallSite<Func<CallSite, object, object, object>>>(JsonDynamicContract.CreateCallSiteSetter));
	}
}
