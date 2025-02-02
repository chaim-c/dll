using System;
using System.Collections.Generic;
using System.Reflection;

namespace TaleWorlds.Library
{
	// Token: 0x0200002B RID: 43
	public static class Extensions
	{
		// Token: 0x0600015D RID: 349 RVA: 0x00005B80 File Offset: 0x00003D80
		public static List<Type> GetTypesSafe(this Assembly assembly, Func<Type, bool> func = null)
		{
			List<Type> list = new List<Type>();
			Type[] array;
			try
			{
				array = assembly.GetTypes();
			}
			catch (ReflectionTypeLoadException ex)
			{
				array = ex.Types;
				Debug.Print(ex.Message + " " + ex.GetType(), 0, Debug.DebugColor.White, 17592186044416UL);
				foreach (object obj in ex.Data.Values)
				{
					Debug.Print(obj.ToString(), 0, Debug.DebugColor.White, 17592186044416UL);
				}
			}
			catch (Exception ex2)
			{
				array = new Type[0];
				Debug.Print(ex2.Message, 0, Debug.DebugColor.White, 17592186044416UL);
			}
			foreach (Type type in array)
			{
				if (type != null && (func == null || func(type)))
				{
					list.Add(type);
				}
			}
			return list;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00005C9C File Offset: 0x00003E9C
		public static object[] GetCustomAttributesSafe(this Type type, Type attributeType, bool inherit)
		{
			try
			{
				return type.GetCustomAttributes(attributeType, inherit);
			}
			catch (Exception ex)
			{
				Debug.FailedAssert(string.Concat(new string[]
				{
					"Failed to get custom attributes (",
					attributeType.Name,
					") for type: ",
					type.Name,
					". Exception: ",
					ex.Message
				}), "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\Base\\TaleWorlds.Library\\Extensions.cs", "GetCustomAttributesSafe", 59);
			}
			return new object[0];
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00005D20 File Offset: 0x00003F20
		public static object[] GetCustomAttributesSafe(this Type type, bool inherit)
		{
			try
			{
				return type.GetCustomAttributes(inherit);
			}
			catch (Exception ex)
			{
				Debug.FailedAssert("Failed to get custom attributes for type: " + type.Name + ". Exception: " + ex.Message, "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\Base\\TaleWorlds.Library\\Extensions.cs", "GetCustomAttributesSafe", 74);
			}
			return new object[0];
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00005D80 File Offset: 0x00003F80
		public static IEnumerable<Attribute> GetCustomAttributesSafe(this Type type, Type attributeType)
		{
			try
			{
				return type.GetCustomAttributes(attributeType);
			}
			catch (Exception ex)
			{
				Debug.FailedAssert(string.Concat(new string[]
				{
					"Failed to get custom attributes (",
					attributeType.Name,
					") for type: ",
					type.Name,
					". Exception: ",
					ex.Message
				}), "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\Base\\TaleWorlds.Library\\Extensions.cs", "GetCustomAttributesSafe", 89);
			}
			return new List<Attribute>();
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00005E00 File Offset: 0x00004000
		public static object[] GetCustomAttributesSafe(this PropertyInfo property, Type attributeType, bool inherit)
		{
			try
			{
				return property.GetCustomAttributes(attributeType, inherit);
			}
			catch (Exception ex)
			{
				Debug.FailedAssert(string.Concat(new string[]
				{
					"Failed to get custom attributes (",
					attributeType.Name,
					") for property: ",
					property.Name,
					". Exception: ",
					ex.Message
				}), "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\Base\\TaleWorlds.Library\\Extensions.cs", "GetCustomAttributesSafe", 104);
			}
			return new object[0];
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00005E84 File Offset: 0x00004084
		public static object[] GetCustomAttributesSafe(this PropertyInfo property, bool inherit)
		{
			try
			{
				return property.GetCustomAttributes(inherit);
			}
			catch (Exception ex)
			{
				Debug.FailedAssert("Failed to get custom attributes for property: " + property.Name + ". Exception: " + ex.Message, "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\Base\\TaleWorlds.Library\\Extensions.cs", "GetCustomAttributesSafe", 119);
			}
			return new object[0];
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00005EE4 File Offset: 0x000040E4
		public static IEnumerable<Attribute> GetCustomAttributesSafe(this PropertyInfo property, Type attributeType)
		{
			try
			{
				return property.GetCustomAttributes(attributeType);
			}
			catch (Exception ex)
			{
				Debug.FailedAssert("Failed to get custom attributes for property: " + property.Name + ". Exception: " + ex.Message, "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\Base\\TaleWorlds.Library\\Extensions.cs", "GetCustomAttributesSafe", 134);
			}
			return new List<Attribute>();
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00005F44 File Offset: 0x00004144
		public static object[] GetCustomAttributesSafe(this FieldInfo field, Type attributeType, bool inherit)
		{
			try
			{
				return field.GetCustomAttributes(attributeType, false);
			}
			catch (Exception ex)
			{
				Debug.FailedAssert(string.Concat(new string[]
				{
					"Failed to get custom attributes (",
					attributeType.Name,
					") for field: ",
					field.Name,
					". Exception: ",
					ex.Message
				}), "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\Base\\TaleWorlds.Library\\Extensions.cs", "GetCustomAttributesSafe", 149);
			}
			return new object[0];
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00005FCC File Offset: 0x000041CC
		public static object[] GetCustomAttributesSafe(this FieldInfo field, bool inherit)
		{
			try
			{
				return field.GetCustomAttributes(inherit);
			}
			catch (Exception ex)
			{
				Debug.FailedAssert("Failed to get custom attributes for field: " + field.Name + ". Exception: " + ex.Message, "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\Base\\TaleWorlds.Library\\Extensions.cs", "GetCustomAttributesSafe", 164);
			}
			return new object[0];
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00006030 File Offset: 0x00004230
		public static IEnumerable<Attribute> GetCustomAttributesSafe(this FieldInfo field, Type attributeType)
		{
			try
			{
				return field.GetCustomAttributes(attributeType);
			}
			catch (Exception ex)
			{
				Debug.FailedAssert("Failed to get custom attributes for field: " + field.Name + ". Exception: " + ex.Message, "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\Base\\TaleWorlds.Library\\Extensions.cs", "GetCustomAttributesSafe", 179);
			}
			return new List<Attribute>();
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00006090 File Offset: 0x00004290
		public static object[] GetCustomAttributesSafe(this MethodInfo method, Type attributeType, bool inherit)
		{
			try
			{
				return method.GetCustomAttributes(attributeType, false);
			}
			catch (Exception ex)
			{
				Debug.FailedAssert(string.Concat(new string[]
				{
					"Failed to get custom attributes (",
					attributeType.Name,
					") for method: ",
					method.Name,
					". Exception: ",
					ex.Message
				}), "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\Base\\TaleWorlds.Library\\Extensions.cs", "GetCustomAttributesSafe", 194);
			}
			return new object[0];
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00006118 File Offset: 0x00004318
		public static object[] GetCustomAttributesSafe(this MethodInfo method, bool inherit)
		{
			try
			{
				return method.GetCustomAttributes(inherit);
			}
			catch (Exception ex)
			{
				Debug.FailedAssert("Failed to get custom attributes for method: " + method.Name + ". Exception: " + ex.Message, "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\Base\\TaleWorlds.Library\\Extensions.cs", "GetCustomAttributesSafe", 209);
			}
			return new object[0];
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000617C File Offset: 0x0000437C
		public static IEnumerable<Attribute> GetCustomAttributesSafe(this MethodInfo method, Type attributeType)
		{
			try
			{
				return method.GetCustomAttributes(attributeType);
			}
			catch (Exception ex)
			{
				Debug.FailedAssert("Failed to get custom attributes for method: " + method.Name + ". Exception: " + ex.Message, "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\Base\\TaleWorlds.Library\\Extensions.cs", "GetCustomAttributesSafe", 224);
			}
			return new List<Attribute>();
		}

		// Token: 0x0600016A RID: 362 RVA: 0x000061DC File Offset: 0x000043DC
		public static object[] GetCustomAttributesSafe(this Assembly assembly, Type attributeType, bool inherit)
		{
			try
			{
				return assembly.GetCustomAttributes(attributeType, false);
			}
			catch (Exception ex)
			{
				Debug.FailedAssert(string.Concat(new string[]
				{
					"Failed to get custom attributes (",
					attributeType.Name,
					") for assembly: ",
					assembly.FullName,
					". Exception: ",
					ex.Message
				}), "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\Base\\TaleWorlds.Library\\Extensions.cs", "GetCustomAttributesSafe", 239);
			}
			return new object[0];
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00006264 File Offset: 0x00004464
		public static object[] GetCustomAttributesSafe(this Assembly assembly, bool inherit)
		{
			try
			{
				return assembly.GetCustomAttributes(inherit);
			}
			catch (Exception ex)
			{
				Debug.FailedAssert("Failed to get custom attributes for assembly: " + assembly.FullName + ". Exception: " + ex.Message, "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\Base\\TaleWorlds.Library\\Extensions.cs", "GetCustomAttributesSafe", 254);
			}
			return new object[0];
		}

		// Token: 0x0600016C RID: 364 RVA: 0x000062C8 File Offset: 0x000044C8
		public static IEnumerable<Attribute> GetCustomAttributesSafe(this Assembly assembly, Type attributeType)
		{
			try
			{
				return assembly.GetCustomAttributes(attributeType);
			}
			catch (Exception ex)
			{
				Debug.FailedAssert("Failed to get custom attributes for assembly: " + assembly.FullName + ". Exception: " + ex.Message, "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\Base\\TaleWorlds.Library\\Extensions.cs", "GetCustomAttributesSafe", 269);
			}
			return new List<Attribute>();
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00006328 File Offset: 0x00004528
		public static MBList<T> ToMBList<T>(this T[] source)
		{
			MBList<T> mblist = new MBList<T>(source.Length);
			mblist.AddRange(source);
			return mblist;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00006339 File Offset: 0x00004539
		public static MBList<T> ToMBList<T>(this List<T> source)
		{
			MBList<T> mblist = new MBList<T>(source.Count);
			mblist.AddRange(source);
			return mblist;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00006350 File Offset: 0x00004550
		public static MBList<T> ToMBList<T>(this IEnumerable<T> source)
		{
			T[] source2;
			if ((source2 = (source as T[])) != null)
			{
				return source2.ToMBList<T>();
			}
			List<T> source3;
			if ((source3 = (source as List<T>)) != null)
			{
				return source3.ToMBList<T>();
			}
			MBList<T> mblist = new MBList<T>();
			mblist.AddRange(source);
			return mblist;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000638C File Offset: 0x0000458C
		public static void AppendList<T>(this List<T> list1, List<T> list2)
		{
			if (list1.Count + list2.Count > list1.Capacity)
			{
				list1.Capacity = list1.Count + list2.Count;
			}
			for (int i = 0; i < list2.Count; i++)
			{
				list1.Add(list2[i]);
			}
		}

		// Token: 0x06000171 RID: 369 RVA: 0x000063DF File Offset: 0x000045DF
		public static MBReadOnlyDictionary<TKey, TValue> GetReadOnlyDictionary<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
		{
			return new MBReadOnlyDictionary<TKey, TValue>(dictionary);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x000063E7 File Offset: 0x000045E7
		public static bool HasAnyFlag<T>(this T p1, T p2) where T : struct
		{
			return EnumHelper<T>.HasAnyFlag(p1, p2);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x000063F5 File Offset: 0x000045F5
		public static bool HasAllFlags<T>(this T p1, T p2) where T : struct
		{
			return EnumHelper<T>.HasAllFlags(p1, p2);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00006404 File Offset: 0x00004604
		public static int GetDeterministicHashCode(this string text)
		{
			int num = 5381;
			for (int i = 0; i < text.Length; i++)
			{
				num = (num << 5) + num + (int)text[i];
			}
			return num;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00006438 File Offset: 0x00004638
		public static int IndexOfMin<TSource>(this IReadOnlyList<TSource> self, Func<TSource, int> func)
		{
			int num = int.MaxValue;
			int result = -1;
			for (int i = 0; i < self.Count; i++)
			{
				int num2 = func(self[i]);
				if (num2 < num)
				{
					num = num2;
					result = i;
				}
			}
			return result;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00006478 File Offset: 0x00004678
		public static int IndexOfMin<TSource>(this MBReadOnlyList<TSource> self, Func<TSource, int> func)
		{
			int num = int.MaxValue;
			int result = -1;
			for (int i = 0; i < self.Count; i++)
			{
				int num2 = func(self[i]);
				if (num2 < num)
				{
					num = num2;
					result = i;
				}
			}
			return result;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x000064B8 File Offset: 0x000046B8
		public static int IndexOfMax<TSource>(this IReadOnlyList<TSource> self, Func<TSource, int> func)
		{
			int num = int.MinValue;
			int result = -1;
			for (int i = 0; i < self.Count; i++)
			{
				int num2 = func(self[i]);
				if (num2 > num)
				{
					num = num2;
					result = i;
				}
			}
			return result;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x000064F8 File Offset: 0x000046F8
		public static int IndexOfMax<TSource>(this MBReadOnlyList<TSource> self, Func<TSource, int> func)
		{
			int num = int.MinValue;
			int result = -1;
			for (int i = 0; i < self.Count; i++)
			{
				int num2 = func(self[i]);
				if (num2 > num)
				{
					num = num2;
					result = i;
				}
			}
			return result;
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00006538 File Offset: 0x00004738
		public static int IndexOf<TValue>(this TValue[] source, TValue item)
		{
			for (int i = 0; i < source.Length; i++)
			{
				if (source[i].Equals(item))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00006574 File Offset: 0x00004774
		public static int FindIndex<TValue>(this IReadOnlyList<TValue> source, Func<TValue, bool> predicate)
		{
			for (int i = 0; i < source.Count; i++)
			{
				if (predicate(source[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x000065A4 File Offset: 0x000047A4
		public static int FindIndex<TValue>(this MBReadOnlyList<TValue> source, Func<TValue, bool> predicate)
		{
			for (int i = 0; i < source.Count; i++)
			{
				if (predicate(source[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x000065D4 File Offset: 0x000047D4
		public static int FindLastIndex<TValue>(this IReadOnlyList<TValue> source, Func<TValue, bool> predicate)
		{
			for (int i = source.Count - 1; i >= 0; i--)
			{
				if (predicate(source[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00006608 File Offset: 0x00004808
		public static int FindLastIndex<TValue>(this MBReadOnlyList<TValue> source, Func<TValue, bool> predicate)
		{
			for (int i = source.Count - 1; i >= 0; i--)
			{
				if (predicate(source[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000663C File Offset: 0x0000483C
		public static void Randomize<T>(this IList<T> array)
		{
			Random random = new Random();
			int i = array.Count;
			while (i > 1)
			{
				i--;
				int index = random.Next(0, i + 1);
				T value = array[index];
				array[index] = array[i];
				array[i] = value;
			}
		}
	}
}
