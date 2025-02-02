﻿using System;
using System.Collections.Generic;
using System.Reflection;

namespace HarmonyLib
{
	// Token: 0x0200000C RID: 12
	internal class AccessCache
	{
		// Token: 0x06000028 RID: 40 RVA: 0x00002860 File Offset: 0x00000A60
		private static T Get<T>(Dictionary<Type, Dictionary<string, T>> dict, Type type, string name, Func<T> fetcher)
		{
			T result;
			lock (dict)
			{
				Dictionary<string, T> dictionary;
				if (!dict.TryGetValue(type, out dictionary))
				{
					dictionary = new Dictionary<string, T>();
					dict[type] = dictionary;
				}
				T t;
				if (!dictionary.TryGetValue(name, out t))
				{
					t = fetcher();
					dictionary[name] = t;
				}
				result = t;
			}
			return result;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000028D0 File Offset: 0x00000AD0
		private static T Get<T>(Dictionary<Type, Dictionary<string, Dictionary<int, T>>> dict, Type type, string name, Type[] arguments, Func<T> fetcher)
		{
			T result;
			lock (dict)
			{
				Dictionary<string, Dictionary<int, T>> dictionary;
				if (!dict.TryGetValue(type, out dictionary))
				{
					dictionary = new Dictionary<string, Dictionary<int, T>>();
					dict[type] = dictionary;
				}
				Dictionary<int, T> dictionary2;
				if (!dictionary.TryGetValue(name, out dictionary2))
				{
					dictionary2 = new Dictionary<int, T>();
					dictionary[name] = dictionary2;
				}
				int key = AccessTools.CombinedHashCode(arguments);
				T t;
				if (!dictionary2.TryGetValue(key, out t))
				{
					t = fetcher();
					dictionary2[key] = t;
				}
				result = t;
			}
			return result;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002964 File Offset: 0x00000B64
		internal FieldInfo GetFieldInfo(Type type, string name, AccessCache.MemberType memberType = AccessCache.MemberType.Any, bool declaredOnly = false)
		{
			FieldInfo fieldInfo = AccessCache.Get<FieldInfo>(this.declaredFields, type, name, () => type.GetField(name, AccessCache.declaredOnlyBindingFlags[memberType]));
			if (fieldInfo == null && !declaredOnly)
			{
				Func<Type, FieldInfo> <>9__2;
				fieldInfo = AccessCache.Get<FieldInfo>(this.inheritedFields, type, name, delegate()
				{
					Type type2 = type;
					Func<Type, FieldInfo> func;
					if ((func = <>9__2) == null)
					{
						func = (<>9__2 = ((Type t) => t.GetField(name, AccessTools.all)));
					}
					return AccessTools.FindIncludingBaseTypes<FieldInfo>(type2, func);
				});
			}
			return fieldInfo;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000029DC File Offset: 0x00000BDC
		internal PropertyInfo GetPropertyInfo(Type type, string name, AccessCache.MemberType memberType = AccessCache.MemberType.Any, bool declaredOnly = false)
		{
			PropertyInfo propertyInfo = AccessCache.Get<PropertyInfo>(this.declaredProperties, type, name, () => type.GetProperty(name, AccessCache.declaredOnlyBindingFlags[memberType]));
			if (propertyInfo == null && !declaredOnly)
			{
				Func<Type, PropertyInfo> <>9__2;
				propertyInfo = AccessCache.Get<PropertyInfo>(this.inheritedProperties, type, name, delegate()
				{
					Type type2 = type;
					Func<Type, PropertyInfo> func;
					if ((func = <>9__2) == null)
					{
						func = (<>9__2 = ((Type t) => t.GetProperty(name, AccessTools.all)));
					}
					return AccessTools.FindIncludingBaseTypes<PropertyInfo>(type2, func);
				});
			}
			return propertyInfo;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002A54 File Offset: 0x00000C54
		internal MethodBase GetMethodInfo(Type type, string name, Type[] arguments, AccessCache.MemberType memberType = AccessCache.MemberType.Any, bool declaredOnly = false)
		{
			MethodBase methodBase = AccessCache.Get<MethodBase>(this.declaredMethods, type, name, arguments, () => type.GetMethod(name, AccessCache.declaredOnlyBindingFlags[memberType], null, arguments, null));
			if (methodBase == null && !declaredOnly)
			{
				methodBase = AccessCache.Get<MethodBase>(this.inheritedMethods, type, name, arguments, () => AccessTools.Method(type, name, arguments, null));
			}
			return methodBase;
		}

		// Token: 0x04000004 RID: 4
		private const BindingFlags BasicFlags = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.SetField | BindingFlags.GetProperty | BindingFlags.SetProperty;

		// Token: 0x04000005 RID: 5
		private static readonly Dictionary<AccessCache.MemberType, BindingFlags> declaredOnlyBindingFlags = new Dictionary<AccessCache.MemberType, BindingFlags>
		{
			{
				AccessCache.MemberType.Any,
				BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.SetField | BindingFlags.GetProperty | BindingFlags.SetProperty
			},
			{
				AccessCache.MemberType.Instance,
				BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.SetField | BindingFlags.GetProperty | BindingFlags.SetProperty
			},
			{
				AccessCache.MemberType.Static,
				BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.SetField | BindingFlags.GetProperty | BindingFlags.SetProperty
			}
		};

		// Token: 0x04000006 RID: 6
		private readonly Dictionary<Type, Dictionary<string, FieldInfo>> declaredFields = new Dictionary<Type, Dictionary<string, FieldInfo>>();

		// Token: 0x04000007 RID: 7
		private readonly Dictionary<Type, Dictionary<string, PropertyInfo>> declaredProperties = new Dictionary<Type, Dictionary<string, PropertyInfo>>();

		// Token: 0x04000008 RID: 8
		private readonly Dictionary<Type, Dictionary<string, Dictionary<int, MethodBase>>> declaredMethods = new Dictionary<Type, Dictionary<string, Dictionary<int, MethodBase>>>();

		// Token: 0x04000009 RID: 9
		private readonly Dictionary<Type, Dictionary<string, FieldInfo>> inheritedFields = new Dictionary<Type, Dictionary<string, FieldInfo>>();

		// Token: 0x0400000A RID: 10
		private readonly Dictionary<Type, Dictionary<string, PropertyInfo>> inheritedProperties = new Dictionary<Type, Dictionary<string, PropertyInfo>>();

		// Token: 0x0400000B RID: 11
		private readonly Dictionary<Type, Dictionary<string, Dictionary<int, MethodBase>>> inheritedMethods = new Dictionary<Type, Dictionary<string, Dictionary<int, MethodBase>>>();

		// Token: 0x02000056 RID: 86
		internal enum MemberType
		{
			// Token: 0x040000F6 RID: 246
			Any,
			// Token: 0x040000F7 RID: 247
			Static,
			// Token: 0x040000F8 RID: 248
			Instance
		}
	}
}
