using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace MCM.LightInject
{
	// Token: 0x02000127 RID: 295
	[ExcludeFromCodeCoverage]
	internal static class TypeHelper
	{
		// Token: 0x060006F5 RID: 1781 RVA: 0x00016E78 File Offset: 0x00015078
		public static Type TryMakeGenericType(Type openGenericType, Type[] closedGenericArguments)
		{
			Type result;
			try
			{
				result = openGenericType.MakeGenericType(closedGenericArguments);
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x00016EA8 File Offset: 0x000150A8
		public static bool IsEnumerableOfT(this Type type)
		{
			TypeInfo typeInfo = type.GetTypeInfo();
			return typeInfo.IsGenericType && typeInfo.GetGenericTypeDefinition() == typeof(IEnumerable<>);
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x00016EE4 File Offset: 0x000150E4
		public static bool IsListOfT(this Type type)
		{
			TypeInfo typeInfo = type.GetTypeInfo();
			return typeInfo.IsGenericType && typeInfo.GetGenericTypeDefinition() == typeof(IList<>);
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x00016F20 File Offset: 0x00015120
		public static bool IsCollectionOfT(this Type type)
		{
			TypeInfo typeInfo = type.GetTypeInfo();
			return typeInfo.IsGenericType && typeInfo.GetGenericTypeDefinition() == typeof(ICollection<>);
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x00016F5C File Offset: 0x0001515C
		public static bool IsReadOnlyCollectionOfT(this Type type)
		{
			TypeInfo typeInfo = type.GetTypeInfo();
			return typeInfo.IsGenericType && typeInfo.GetGenericTypeDefinition() == typeof(IReadOnlyCollection<>);
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x00016F98 File Offset: 0x00015198
		public static bool IsReadOnlyListOfT(this Type type)
		{
			TypeInfo typeInfo = type.GetTypeInfo();
			return typeInfo.IsGenericType && typeInfo.GetGenericTypeDefinition() == typeof(IReadOnlyList<>);
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x00016FD4 File Offset: 0x000151D4
		public static bool IsLazy(this Type type)
		{
			TypeInfo typeInfo = type.GetTypeInfo();
			return typeInfo.IsGenericType && typeInfo.GetGenericTypeDefinition() == typeof(Lazy<>);
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x00017010 File Offset: 0x00015210
		public static bool IsFuncRepresentingService(this Type type)
		{
			TypeInfo typeInfo = type.GetTypeInfo();
			return typeInfo.IsGenericType && typeInfo.GetGenericTypeDefinition() == typeof(Func<>);
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x0001704C File Offset: 0x0001524C
		public static bool IsFuncWithParameters(this Type type)
		{
			TypeInfo typeInfo = type.GetTypeInfo();
			bool flag = !typeInfo.IsGenericType;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = type.IsFuncRepresentingNamedService();
				if (flag2)
				{
					result = false;
				}
				else
				{
					Type genericTypeDefinition = typeInfo.GetGenericTypeDefinition();
					result = (genericTypeDefinition == typeof(Func<, >) || genericTypeDefinition == typeof(Func<, , >) || genericTypeDefinition == typeof(Func<, , , >) || genericTypeDefinition == typeof(Func<, , , , >));
				}
			}
			return result;
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x000170D8 File Offset: 0x000152D8
		public static bool IsFuncRepresentingNamedService(this Type type)
		{
			TypeInfo typeInfo = type.GetTypeInfo();
			bool flag = !typeInfo.IsGenericType;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				Type genericTypeDefinition = typeInfo.GetGenericTypeDefinition();
				result = (genericTypeDefinition == typeof(Func<, >) && typeInfo.GenericTypeArguments[0] == typeof(string));
			}
			return result;
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x00017138 File Offset: 0x00015338
		public static bool IsClosedGeneric(this Type type)
		{
			TypeInfo typeInfo = type.GetTypeInfo();
			return typeInfo.IsGenericType && !typeInfo.IsGenericTypeDefinition;
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x00017168 File Offset: 0x00015368
		public static Type GetElementType(Type type)
		{
			TypeInfo typeInfo = type.GetTypeInfo();
			Type[] genericTypeArguments = typeInfo.GenericTypeArguments;
			bool flag = typeInfo.IsGenericType && genericTypeArguments.Length == 1;
			Type result;
			if (flag)
			{
				result = genericTypeArguments[0];
			}
			else
			{
				result = type.GetElementType();
			}
			return result;
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x000171AC File Offset: 0x000153AC
		public static object GetDefaultValue(Type type)
		{
			MethodInfo openGenericGetDefaultValueInternalMethod = typeof(TypeHelper).GetTypeInfo().GetDeclaredMethod("GetDefaultValueInternal");
			MethodInfo closedGenericDefaultValueInternalMethod = openGenericGetDefaultValueInternalMethod.MakeGenericMethod(new Type[]
			{
				type
			});
			return closedGenericDefaultValueInternalMethod.Invoke(null, new object[0]);
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x000171F8 File Offset: 0x000153F8
		private static T GetDefaultValueInternal<T>()
		{
			return default(T);
		}
	}
}
