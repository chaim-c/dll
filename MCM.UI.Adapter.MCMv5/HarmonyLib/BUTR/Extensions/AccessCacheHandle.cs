using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace HarmonyLib.BUTR.Extensions
{
	// Token: 0x02000012 RID: 18
	[ExcludeFromCodeCoverage]
	internal readonly struct AccessCacheHandle
	{
		// Token: 0x06000037 RID: 55 RVA: 0x00002BC8 File Offset: 0x00000DC8
		public static AccessCacheHandle? Create()
		{
			AccessCacheHandle.AccessCacheCtorDelegate accessCacheCtorMethod = AccessCacheHandle.AccessCacheCtorMethod;
			object accessCache = (accessCacheCtorMethod != null) ? accessCacheCtorMethod() : null;
			return (accessCache != null) ? new AccessCacheHandle?(new AccessCacheHandle(accessCache)) : null;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002C05 File Offset: 0x00000E05
		[NullableContext(1)]
		private AccessCacheHandle(object accessCache)
		{
			this._accessCache = accessCache;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002C0E File Offset: 0x00000E0E
		[NullableContext(1)]
		[return: Nullable(2)]
		public FieldInfo GetFieldInfo(Type type, string name, AccessCacheHandle.MemberType memberType = AccessCacheHandle.MemberType.Any, bool declaredOnly = false)
		{
			AccessCacheHandle.GetFieldInfoDelegate getFieldInfoMethod = AccessCacheHandle.GetFieldInfoMethod;
			return (getFieldInfoMethod != null) ? getFieldInfoMethod(this._accessCache, type, name, memberType, declaredOnly) : null;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002C2C File Offset: 0x00000E2C
		[NullableContext(1)]
		[return: Nullable(2)]
		public PropertyInfo GetPropertyInfo(Type type, string name, AccessCacheHandle.MemberType memberType = AccessCacheHandle.MemberType.Any, bool declaredOnly = false)
		{
			AccessCacheHandle.GetPropertyInfoDelegate getPropertyInfoMethod = AccessCacheHandle.GetPropertyInfoMethod;
			return (getPropertyInfoMethod != null) ? getPropertyInfoMethod(this._accessCache, type, name, memberType, declaredOnly) : null;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002C4A File Offset: 0x00000E4A
		[NullableContext(1)]
		[return: Nullable(2)]
		public MethodBase GetMethodInfo(Type type, string name, Type[] arguments, AccessCacheHandle.MemberType memberType = AccessCacheHandle.MemberType.Any, bool declaredOnly = false)
		{
			AccessCacheHandle.GetMethodInfoDelegate getMethodInfoMethod = AccessCacheHandle.GetMethodInfoMethod;
			return (getMethodInfoMethod != null) ? getMethodInfoMethod(this._accessCache, type, name, arguments, memberType, declaredOnly) : null;
		}

		// Token: 0x0400000C RID: 12
		[Nullable(1)]
		private static readonly Type Blank = typeof(Harmony);

		// Token: 0x0400000D RID: 13
		[Nullable(2)]
		private static readonly AccessCacheHandle.AccessCacheCtorDelegate AccessCacheCtorMethod = AccessTools2.GetDeclaredConstructorDelegate<AccessCacheHandle.AccessCacheCtorDelegate>("HarmonyLib.AccessCache", null, true);

		// Token: 0x0400000E RID: 14
		[Nullable(2)]
		private static readonly AccessCacheHandle.GetFieldInfoDelegate GetFieldInfoMethod = AccessTools2.GetDelegateObjectInstance<AccessCacheHandle.GetFieldInfoDelegate>("HarmonyLib.AccessCache:GetFieldInfo", null, null, true);

		// Token: 0x0400000F RID: 15
		[Nullable(2)]
		private static readonly AccessCacheHandle.GetPropertyInfoDelegate GetPropertyInfoMethod = AccessTools2.GetDelegateObjectInstance<AccessCacheHandle.GetPropertyInfoDelegate>("HarmonyLib.AccessCache:GetPropertyInfo", null, null, true);

		// Token: 0x04000010 RID: 16
		[Nullable(2)]
		private static readonly AccessCacheHandle.GetMethodInfoDelegate GetMethodInfoMethod = AccessTools2.GetDelegateObjectInstance<AccessCacheHandle.GetMethodInfoDelegate>("HarmonyLib.AccessCache:GetMethodInfo", null, null, true);

		// Token: 0x04000011 RID: 17
		[Nullable(1)]
		private readonly object _accessCache;

		// Token: 0x02000031 RID: 49
		internal enum MemberType
		{
			// Token: 0x0400006F RID: 111
			Any,
			// Token: 0x04000070 RID: 112
			Static,
			// Token: 0x04000071 RID: 113
			Instance
		}

		// Token: 0x02000032 RID: 50
		// (Invoke) Token: 0x06000161 RID: 353
		private delegate object AccessCacheCtorDelegate();

		// Token: 0x02000033 RID: 51
		// (Invoke) Token: 0x06000165 RID: 357
		private delegate FieldInfo GetFieldInfoDelegate(object instance, Type type, string name, AccessCacheHandle.MemberType memberType = AccessCacheHandle.MemberType.Any, bool declaredOnly = false);

		// Token: 0x02000034 RID: 52
		// (Invoke) Token: 0x06000169 RID: 361
		private delegate PropertyInfo GetPropertyInfoDelegate(object instance, Type type, string name, AccessCacheHandle.MemberType memberType = AccessCacheHandle.MemberType.Any, bool declaredOnly = false);

		// Token: 0x02000035 RID: 53
		// (Invoke) Token: 0x0600016D RID: 365
		private delegate MethodBase GetMethodInfoDelegate(object instance, Type type, string name, Type[] arguments, AccessCacheHandle.MemberType memberType = AccessCacheHandle.MemberType.Any, bool declaredOnly = false);
	}
}
