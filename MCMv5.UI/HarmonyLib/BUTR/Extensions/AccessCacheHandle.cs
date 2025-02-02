using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace HarmonyLib.BUTR.Extensions
{
	// Token: 0x02000065 RID: 101
	[ExcludeFromCodeCoverage]
	internal readonly struct AccessCacheHandle
	{
		// Token: 0x060003C9 RID: 969 RVA: 0x0000FC04 File Offset: 0x0000DE04
		public static AccessCacheHandle? Create()
		{
			AccessCacheHandle.AccessCacheCtorDelegate accessCacheCtorMethod = AccessCacheHandle.AccessCacheCtorMethod;
			object accessCache = (accessCacheCtorMethod != null) ? accessCacheCtorMethod() : null;
			return (accessCache != null) ? new AccessCacheHandle?(new AccessCacheHandle(accessCache)) : null;
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0000FC41 File Offset: 0x0000DE41
		[NullableContext(1)]
		private AccessCacheHandle(object accessCache)
		{
			this._accessCache = accessCache;
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0000FC4A File Offset: 0x0000DE4A
		[NullableContext(1)]
		[return: Nullable(2)]
		public FieldInfo GetFieldInfo(Type type, string name, AccessCacheHandle.MemberType memberType = AccessCacheHandle.MemberType.Any, bool declaredOnly = false)
		{
			AccessCacheHandle.GetFieldInfoDelegate getFieldInfoMethod = AccessCacheHandle.GetFieldInfoMethod;
			return (getFieldInfoMethod != null) ? getFieldInfoMethod(this._accessCache, type, name, memberType, declaredOnly) : null;
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0000FC68 File Offset: 0x0000DE68
		[NullableContext(1)]
		[return: Nullable(2)]
		public PropertyInfo GetPropertyInfo(Type type, string name, AccessCacheHandle.MemberType memberType = AccessCacheHandle.MemberType.Any, bool declaredOnly = false)
		{
			AccessCacheHandle.GetPropertyInfoDelegate getPropertyInfoMethod = AccessCacheHandle.GetPropertyInfoMethod;
			return (getPropertyInfoMethod != null) ? getPropertyInfoMethod(this._accessCache, type, name, memberType, declaredOnly) : null;
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000FC86 File Offset: 0x0000DE86
		[NullableContext(1)]
		[return: Nullable(2)]
		public MethodBase GetMethodInfo(Type type, string name, Type[] arguments, AccessCacheHandle.MemberType memberType = AccessCacheHandle.MemberType.Any, bool declaredOnly = false)
		{
			AccessCacheHandle.GetMethodInfoDelegate getMethodInfoMethod = AccessCacheHandle.GetMethodInfoMethod;
			return (getMethodInfoMethod != null) ? getMethodInfoMethod(this._accessCache, type, name, arguments, memberType, declaredOnly) : null;
		}

		// Token: 0x04000155 RID: 341
		[Nullable(1)]
		private static readonly Type Blank = typeof(Harmony);

		// Token: 0x04000156 RID: 342
		[Nullable(2)]
		private static readonly AccessCacheHandle.AccessCacheCtorDelegate AccessCacheCtorMethod = AccessTools2.GetDeclaredConstructorDelegate<AccessCacheHandle.AccessCacheCtorDelegate>("HarmonyLib.AccessCache", null, true);

		// Token: 0x04000157 RID: 343
		[Nullable(2)]
		private static readonly AccessCacheHandle.GetFieldInfoDelegate GetFieldInfoMethod = AccessTools2.GetDelegateObjectInstance<AccessCacheHandle.GetFieldInfoDelegate>("HarmonyLib.AccessCache:GetFieldInfo", null, null, true);

		// Token: 0x04000158 RID: 344
		[Nullable(2)]
		private static readonly AccessCacheHandle.GetPropertyInfoDelegate GetPropertyInfoMethod = AccessTools2.GetDelegateObjectInstance<AccessCacheHandle.GetPropertyInfoDelegate>("HarmonyLib.AccessCache:GetPropertyInfo", null, null, true);

		// Token: 0x04000159 RID: 345
		[Nullable(2)]
		private static readonly AccessCacheHandle.GetMethodInfoDelegate GetMethodInfoMethod = AccessTools2.GetDelegateObjectInstance<AccessCacheHandle.GetMethodInfoDelegate>("HarmonyLib.AccessCache:GetMethodInfo", null, null, true);

		// Token: 0x0400015A RID: 346
		[Nullable(1)]
		private readonly object _accessCache;

		// Token: 0x020000F5 RID: 245
		internal enum MemberType
		{
			// Token: 0x04000317 RID: 791
			Any,
			// Token: 0x04000318 RID: 792
			Static,
			// Token: 0x04000319 RID: 793
			Instance
		}

		// Token: 0x020000F6 RID: 246
		// (Invoke) Token: 0x060006B3 RID: 1715
		private delegate object AccessCacheCtorDelegate();

		// Token: 0x020000F7 RID: 247
		// (Invoke) Token: 0x060006B7 RID: 1719
		private delegate FieldInfo GetFieldInfoDelegate(object instance, Type type, string name, AccessCacheHandle.MemberType memberType = AccessCacheHandle.MemberType.Any, bool declaredOnly = false);

		// Token: 0x020000F8 RID: 248
		// (Invoke) Token: 0x060006BB RID: 1723
		private delegate PropertyInfo GetPropertyInfoDelegate(object instance, Type type, string name, AccessCacheHandle.MemberType memberType = AccessCacheHandle.MemberType.Any, bool declaredOnly = false);

		// Token: 0x020000F9 RID: 249
		// (Invoke) Token: 0x060006BF RID: 1727
		private delegate MethodBase GetMethodInfoDelegate(object instance, Type type, string name, Type[] arguments, AccessCacheHandle.MemberType memberType = AccessCacheHandle.MemberType.Any, bool declaredOnly = false);
	}
}
