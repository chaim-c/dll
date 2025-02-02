using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000071 RID: 113
	[NullableContext(1)]
	[Nullable(0)]
	public class CamelCasePropertyNamesContractResolver : DefaultContractResolver
	{
		// Token: 0x060005F6 RID: 1526 RVA: 0x00018C2C File Offset: 0x00016E2C
		public CamelCasePropertyNamesContractResolver()
		{
			base.NamingStrategy = new CamelCaseNamingStrategy
			{
				ProcessDictionaryKeys = true,
				OverrideSpecifiedNames = true
			};
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x00018C50 File Offset: 0x00016E50
		public override JsonContract ResolveContract(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			StructMultiKey<Type, Type> key = new StructMultiKey<Type, Type>(base.GetType(), type);
			Dictionary<StructMultiKey<Type, Type>, JsonContract> contractCache = CamelCasePropertyNamesContractResolver._contractCache;
			JsonContract jsonContract;
			if (contractCache == null || !contractCache.TryGetValue(key, out jsonContract))
			{
				jsonContract = this.CreateContract(type);
				object typeContractCacheLock = CamelCasePropertyNamesContractResolver.TypeContractCacheLock;
				lock (typeContractCacheLock)
				{
					contractCache = CamelCasePropertyNamesContractResolver._contractCache;
					Dictionary<StructMultiKey<Type, Type>, JsonContract> dictionary = (contractCache != null) ? new Dictionary<StructMultiKey<Type, Type>, JsonContract>(contractCache) : new Dictionary<StructMultiKey<Type, Type>, JsonContract>();
					dictionary[key] = jsonContract;
					CamelCasePropertyNamesContractResolver._contractCache = dictionary;
				}
			}
			return jsonContract;
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x00018CF0 File Offset: 0x00016EF0
		internal override DefaultJsonNameTable GetNameTable()
		{
			return CamelCasePropertyNamesContractResolver.NameTable;
		}

		// Token: 0x0400020F RID: 527
		private static readonly object TypeContractCacheLock = new object();

		// Token: 0x04000210 RID: 528
		private static readonly DefaultJsonNameTable NameTable = new DefaultJsonNameTable();

		// Token: 0x04000211 RID: 529
		[Nullable(new byte[]
		{
			2,
			0,
			1,
			1,
			1
		})]
		private static Dictionary<StructMultiKey<Type, Type>, JsonContract> _contractCache;
	}
}
