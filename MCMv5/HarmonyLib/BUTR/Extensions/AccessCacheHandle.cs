using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace HarmonyLib.BUTR.Extensions
{
	// Token: 0x02000151 RID: 337
	[ExcludeFromCodeCoverage]
	internal readonly struct AccessCacheHandle
	{
		// Token: 0x060008FD RID: 2301 RVA: 0x0001DDDC File Offset: 0x0001BFDC
		public static AccessCacheHandle? Create()
		{
			AccessCacheHandle.AccessCacheCtorDelegate accessCacheCtorMethod = AccessCacheHandle.AccessCacheCtorMethod;
			object accessCache = (accessCacheCtorMethod != null) ? accessCacheCtorMethod() : null;
			return (accessCache != null) ? new AccessCacheHandle?(new AccessCacheHandle(accessCache)) : null;
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x0001DE19 File Offset: 0x0001C019
		[NullableContext(1)]
		private AccessCacheHandle(object accessCache)
		{
			this._accessCache = accessCache;
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x0001DE22 File Offset: 0x0001C022
		[NullableContext(1)]
		[return: Nullable(2)]
		public FieldInfo GetFieldInfo(Type type, string name, AccessCacheHandle.MemberType memberType = AccessCacheHandle.MemberType.Any, bool declaredOnly = false)
		{
			AccessCacheHandle.GetFieldInfoDelegate getFieldInfoMethod = AccessCacheHandle.GetFieldInfoMethod;
			return (getFieldInfoMethod != null) ? getFieldInfoMethod(this._accessCache, type, name, memberType, declaredOnly) : null;
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x0001DE40 File Offset: 0x0001C040
		[NullableContext(1)]
		[return: Nullable(2)]
		public PropertyInfo GetPropertyInfo(Type type, string name, AccessCacheHandle.MemberType memberType = AccessCacheHandle.MemberType.Any, bool declaredOnly = false)
		{
			AccessCacheHandle.GetPropertyInfoDelegate getPropertyInfoMethod = AccessCacheHandle.GetPropertyInfoMethod;
			return (getPropertyInfoMethod != null) ? getPropertyInfoMethod(this._accessCache, type, name, memberType, declaredOnly) : null;
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x0001DE5E File Offset: 0x0001C05E
		[NullableContext(1)]
		[return: Nullable(2)]
		public MethodBase GetMethodInfo(Type type, string name, Type[] arguments, AccessCacheHandle.MemberType memberType = AccessCacheHandle.MemberType.Any, bool declaredOnly = false)
		{
			AccessCacheHandle.GetMethodInfoDelegate getMethodInfoMethod = AccessCacheHandle.GetMethodInfoMethod;
			return (getMethodInfoMethod != null) ? getMethodInfoMethod(this._accessCache, type, name, arguments, memberType, declaredOnly) : null;
		}

		// Token: 0x04000297 RID: 663
		[Nullable(1)]
		private static readonly Type Blank = typeof(Harmony);

		// Token: 0x04000298 RID: 664
		[Nullable(2)]
		private static readonly AccessCacheHandle.AccessCacheCtorDelegate AccessCacheCtorMethod = AccessTools2.GetDeclaredConstructorDelegate<AccessCacheHandle.AccessCacheCtorDelegate>("HarmonyLib.AccessCache", null, true);

		// Token: 0x04000299 RID: 665
		[Nullable(2)]
		private static readonly AccessCacheHandle.GetFieldInfoDelegate GetFieldInfoMethod = AccessTools2.GetDelegateObjectInstance<AccessCacheHandle.GetFieldInfoDelegate>("HarmonyLib.AccessCache:GetFieldInfo", null, null, true);

		// Token: 0x0400029A RID: 666
		[Nullable(2)]
		private static readonly AccessCacheHandle.GetPropertyInfoDelegate GetPropertyInfoMethod = AccessTools2.GetDelegateObjectInstance<AccessCacheHandle.GetPropertyInfoDelegate>("HarmonyLib.AccessCache:GetPropertyInfo", null, null, true);

		// Token: 0x0400029B RID: 667
		[Nullable(2)]
		private static readonly AccessCacheHandle.GetMethodInfoDelegate GetMethodInfoMethod = AccessTools2.GetDelegateObjectInstance<AccessCacheHandle.GetMethodInfoDelegate>("HarmonyLib.AccessCache:GetMethodInfo", null, null, true);

		// Token: 0x0400029C RID: 668
		[Nullable(1)]
		private readonly object _accessCache;

		// Token: 0x02000296 RID: 662
		internal enum MemberType
		{
			// Token: 0x04000664 RID: 1636
			Any,
			// Token: 0x04000665 RID: 1637
			Static,
			// Token: 0x04000666 RID: 1638
			Instance
		}

		// Token: 0x02000297 RID: 663
		// (Invoke) Token: 0x06000EAC RID: 3756
		private delegate object AccessCacheCtorDelegate();

		// Token: 0x02000298 RID: 664
		// (Invoke) Token: 0x06000EB0 RID: 3760
		private delegate FieldInfo GetFieldInfoDelegate(object instance, Type type, string name, AccessCacheHandle.MemberType memberType = AccessCacheHandle.MemberType.Any, bool declaredOnly = false);

		// Token: 0x02000299 RID: 665
		// (Invoke) Token: 0x06000EB4 RID: 3764
		private delegate PropertyInfo GetPropertyInfoDelegate(object instance, Type type, string name, AccessCacheHandle.MemberType memberType = AccessCacheHandle.MemberType.Any, bool declaredOnly = false);

		// Token: 0x0200029A RID: 666
		// (Invoke) Token: 0x06000EB8 RID: 3768
		private delegate MethodBase GetMethodInfoDelegate(object instance, Type type, string name, Type[] arguments, AccessCacheHandle.MemberType memberType = AccessCacheHandle.MemberType.Any, bool declaredOnly = false);
	}
}
