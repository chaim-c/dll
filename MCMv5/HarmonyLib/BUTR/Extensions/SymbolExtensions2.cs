using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace HarmonyLib.BUTR.Extensions
{
	// Token: 0x02000154 RID: 340
	[NullableContext(2)]
	[Nullable(0)]
	internal static class SymbolExtensions2
	{
		// Token: 0x06000963 RID: 2403 RVA: 0x00020660 File Offset: 0x0001E860
		public static ConstructorInfo GetConstructorInfo<TResult>([Nullable(1)] Expression<Func<TResult>> expression)
		{
			bool flag = expression != null;
			ConstructorInfo result;
			if (flag)
			{
				result = SymbolExtensions2.GetConstructorInfo(expression);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x00020688 File Offset: 0x0001E888
		public static ConstructorInfo GetConstructorInfo<T1, TResult>([Nullable(1)] Expression<Func<T1, TResult>> expression)
		{
			bool flag = expression != null;
			ConstructorInfo result;
			if (flag)
			{
				result = SymbolExtensions2.GetConstructorInfo(expression);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x000206B0 File Offset: 0x0001E8B0
		public static ConstructorInfo GetConstructorInfo<T1, T2, TResult>([Nullable(1)] Expression<Func<T1, T2, TResult>> expression)
		{
			bool flag = expression != null;
			ConstructorInfo result;
			if (flag)
			{
				result = SymbolExtensions2.GetConstructorInfo(expression);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x000206D8 File Offset: 0x0001E8D8
		public static ConstructorInfo GetConstructorInfo<T1, T2, T3, TResult>([Nullable(1)] Expression<Func<T1, T2, T3, TResult>> expression)
		{
			bool flag = expression != null;
			ConstructorInfo result;
			if (flag)
			{
				result = SymbolExtensions2.GetConstructorInfo(expression);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x00020700 File Offset: 0x0001E900
		public static ConstructorInfo GetConstructorInfo<T1, T2, T3, T4, TResult>([Nullable(1)] Expression<Func<T1, T2, T3, T4, TResult>> expression)
		{
			bool flag = expression != null;
			ConstructorInfo result;
			if (flag)
			{
				result = SymbolExtensions2.GetConstructorInfo(expression);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x00020728 File Offset: 0x0001E928
		public static ConstructorInfo GetConstructorInfo<T1, T2, T3, T4, T5, TResult>([Nullable(1)] Expression<Func<T1, T2, T3, T4, T5, TResult>> expression)
		{
			bool flag = expression != null;
			ConstructorInfo result;
			if (flag)
			{
				result = SymbolExtensions2.GetConstructorInfo(expression);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x00020750 File Offset: 0x0001E950
		public static ConstructorInfo GetConstructorInfo<T1, T2, T3, T4, T5, T6, TResult>([Nullable(1)] Expression<Func<T1, T2, T3, T4, T5, T6, TResult>> expression)
		{
			bool flag = expression != null;
			ConstructorInfo result;
			if (flag)
			{
				result = SymbolExtensions2.GetConstructorInfo(expression);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x00020778 File Offset: 0x0001E978
		public static ConstructorInfo GetConstructorInfo<T1, T2, T3, T4, T5, T6, T7, TResult>([Nullable(1)] Expression<Func<T1, T2, T3, T4, T5, T6, T7, TResult>> expression)
		{
			bool flag = expression != null;
			ConstructorInfo result;
			if (flag)
			{
				result = SymbolExtensions2.GetConstructorInfo(expression);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x000207A0 File Offset: 0x0001E9A0
		public static ConstructorInfo GetConstructorInfo<T1, T2, T3, T4, T5, T6, T7, T8, TResult>([Nullable(1)] Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult>> expression)
		{
			bool flag = expression != null;
			ConstructorInfo result;
			if (flag)
			{
				result = SymbolExtensions2.GetConstructorInfo(expression);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x000207C8 File Offset: 0x0001E9C8
		public static ConstructorInfo GetConstructorInfo<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>([Nullable(1)] Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>> expression)
		{
			bool flag = expression != null;
			ConstructorInfo result;
			if (flag)
			{
				result = SymbolExtensions2.GetConstructorInfo(expression);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x000207F0 File Offset: 0x0001E9F0
		[NullableContext(1)]
		[return: Nullable(2)]
		public static ConstructorInfo GetConstructorInfo(LambdaExpression expression)
		{
			NewExpression body = ((expression != null) ? expression.Body : null) as NewExpression;
			bool flag = body != null && body.Constructor != null;
			ConstructorInfo result;
			if (flag)
			{
				result = body.Constructor;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x00020834 File Offset: 0x0001EA34
		public static FieldInfo GetFieldInfo<T>([Nullable(1)] Expression<Func<T>> expression)
		{
			bool flag = expression != null;
			FieldInfo result;
			if (flag)
			{
				result = SymbolExtensions2.GetFieldInfo(expression);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x0002085C File Offset: 0x0001EA5C
		public static FieldInfo GetFieldInfo<T, TResult>([Nullable(1)] Expression<Func<T, TResult>> expression)
		{
			bool flag = expression != null;
			FieldInfo result;
			if (flag)
			{
				result = SymbolExtensions2.GetFieldInfo(expression);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x00020884 File Offset: 0x0001EA84
		[NullableContext(1)]
		[return: Nullable(2)]
		public static FieldInfo GetFieldInfo(LambdaExpression expression)
		{
			MemberExpression memberExpression = ((expression != null) ? expression.Body : null) as MemberExpression;
			FieldInfo fieldInfo;
			bool flag;
			if (memberExpression != null)
			{
				fieldInfo = (memberExpression.Member as FieldInfo);
				flag = (fieldInfo != null);
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			FieldInfo result;
			if (flag2)
			{
				result = fieldInfo;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x000208C8 File Offset: 0x0001EAC8
		[NullableContext(1)]
		[return: Nullable(new byte[]
		{
			2,
			1,
			1
		})]
		public static AccessTools.FieldRef<object, TField> GetFieldRefAccess<[Nullable(2)] TField>(Expression<Func<TField>> expression)
		{
			bool flag = expression != null;
			AccessTools.FieldRef<object, TField> result;
			if (flag)
			{
				result = SymbolExtensions2.GetFieldRefAccess<TField>(expression);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x000208F0 File Offset: 0x0001EAF0
		[NullableContext(1)]
		[return: Nullable(new byte[]
		{
			2,
			1,
			1
		})]
		public static AccessTools.FieldRef<object, TField> GetFieldRefAccess<[Nullable(2)] TField>(LambdaExpression expression)
		{
			MemberExpression memberExpression = ((expression != null) ? expression.Body : null) as MemberExpression;
			FieldInfo fieldInfo;
			bool flag;
			if (memberExpression != null)
			{
				fieldInfo = (memberExpression.Member as FieldInfo);
				flag = (fieldInfo != null);
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			AccessTools.FieldRef<object, TField> result;
			if (flag2)
			{
				result = ((fieldInfo == null) ? null : AccessTools2.FieldRefAccess<object, TField>(fieldInfo, true));
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x00020948 File Offset: 0x0001EB48
		[NullableContext(1)]
		[return: Nullable(new byte[]
		{
			2,
			1,
			1
		})]
		public static AccessTools.FieldRef<TObject, TField> GetFieldRefAccess<TObject, [Nullable(2)] TField>(Expression<Func<TObject, TField>> expression) where TObject : class
		{
			bool flag = expression != null;
			AccessTools.FieldRef<TObject, TField> result;
			if (flag)
			{
				result = SymbolExtensions2.GetFieldRefAccess<TObject, TField>(expression);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x00020970 File Offset: 0x0001EB70
		[NullableContext(1)]
		[return: Nullable(new byte[]
		{
			2,
			1,
			1
		})]
		public static AccessTools.FieldRef<TObject, TField> GetFieldRefAccess<TObject, [Nullable(2)] TField>(LambdaExpression expression) where TObject : class
		{
			MemberExpression memberExpression = ((expression != null) ? expression.Body : null) as MemberExpression;
			FieldInfo fieldInfo;
			bool flag;
			if (memberExpression != null)
			{
				fieldInfo = (memberExpression.Member as FieldInfo);
				flag = (fieldInfo != null);
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			AccessTools.FieldRef<TObject, TField> result;
			if (flag2)
			{
				result = ((fieldInfo == null) ? null : AccessTools2.FieldRefAccess<TObject, TField>(fieldInfo, true));
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x000209C8 File Offset: 0x0001EBC8
		[NullableContext(1)]
		[return: Nullable(2)]
		public static MethodInfo GetMethodInfo(Expression<Action> expression)
		{
			bool flag = expression != null;
			MethodInfo result;
			if (flag)
			{
				result = SymbolExtensions2.GetMethodInfo(expression);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x000209F0 File Offset: 0x0001EBF0
		public static MethodInfo GetMethodInfo<T1>([Nullable(1)] Expression<Action<T1>> expression)
		{
			bool flag = expression != null;
			MethodInfo result;
			if (flag)
			{
				result = SymbolExtensions2.GetMethodInfo(expression);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x00020A18 File Offset: 0x0001EC18
		public static MethodInfo GetMethodInfo<T1, T2>([Nullable(1)] Expression<Action<T1, T2>> expression)
		{
			bool flag = expression != null;
			MethodInfo result;
			if (flag)
			{
				result = SymbolExtensions2.GetMethodInfo(expression);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x00020A40 File Offset: 0x0001EC40
		public static MethodInfo GetMethodInfo<T1, T2, T3>([Nullable(1)] Expression<Action<T1, T2, T3>> expression)
		{
			bool flag = expression != null;
			MethodInfo result;
			if (flag)
			{
				result = SymbolExtensions2.GetMethodInfo(expression);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x00020A68 File Offset: 0x0001EC68
		public static MethodInfo GetMethodInfo<T1, T2, T3, T4>([Nullable(1)] Expression<Action<T1, T2, T3, T4>> expression)
		{
			bool flag = expression != null;
			MethodInfo result;
			if (flag)
			{
				result = SymbolExtensions2.GetMethodInfo(expression);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x00020A90 File Offset: 0x0001EC90
		public static MethodInfo GetMethodInfo<T1, T2, T3, T4, T5>([Nullable(1)] Expression<Action<T1, T2, T3, T4, T5>> expression)
		{
			bool flag = expression != null;
			MethodInfo result;
			if (flag)
			{
				result = SymbolExtensions2.GetMethodInfo(expression);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x00020AB8 File Offset: 0x0001ECB8
		public static MethodInfo GetMethodInfo<T1, T2, T3, T4, T5, T6>([Nullable(1)] Expression<Action<T1, T2, T3, T4, T5, T6>> expression)
		{
			bool flag = expression != null;
			MethodInfo result;
			if (flag)
			{
				result = SymbolExtensions2.GetMethodInfo(expression);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x00020AE0 File Offset: 0x0001ECE0
		public static MethodInfo GetMethodInfo<T1, T2, T3, T4, T5, T6, T7>([Nullable(1)] Expression<Action<T1, T2, T3, T4, T5, T6, T7>> expression)
		{
			bool flag = expression != null;
			MethodInfo result;
			if (flag)
			{
				result = SymbolExtensions2.GetMethodInfo(expression);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x00020B08 File Offset: 0x0001ED08
		public static MethodInfo GetMethodInfo<T1, T2, T3, T4, T5, T6, T7, T8>([Nullable(1)] Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8>> expression)
		{
			bool flag = expression != null;
			MethodInfo result;
			if (flag)
			{
				result = SymbolExtensions2.GetMethodInfo(expression);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x00020B30 File Offset: 0x0001ED30
		public static MethodInfo GetMethodInfo<T1, T2, T3, T4, T5, T6, T7, T8, T9>([Nullable(1)] Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9>> expression)
		{
			bool flag = expression != null;
			MethodInfo result;
			if (flag)
			{
				result = SymbolExtensions2.GetMethodInfo(expression);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x00020B58 File Offset: 0x0001ED58
		public static MethodInfo GetMethodInfo<TResult>([Nullable(1)] Expression<Func<TResult>> expression)
		{
			bool flag = expression != null;
			MethodInfo result;
			if (flag)
			{
				result = SymbolExtensions2.GetMethodInfo(expression);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x00020B80 File Offset: 0x0001ED80
		public static MethodInfo GetMethodInfo<T1, TResult>([Nullable(1)] Expression<Func<T1, TResult>> expression)
		{
			bool flag = expression != null;
			MethodInfo result;
			if (flag)
			{
				result = SymbolExtensions2.GetMethodInfo(expression);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x00020BA8 File Offset: 0x0001EDA8
		public static MethodInfo GetMethodInfo<T1, T2, TResult>([Nullable(1)] Expression<Func<T1, T2, TResult>> expression)
		{
			bool flag = expression != null;
			MethodInfo result;
			if (flag)
			{
				result = SymbolExtensions2.GetMethodInfo(expression);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x00020BD0 File Offset: 0x0001EDD0
		public static MethodInfo GetMethodInfo<T1, T2, T3, TResult>([Nullable(1)] Expression<Func<T1, T2, T3, TResult>> expression)
		{
			bool flag = expression != null;
			MethodInfo result;
			if (flag)
			{
				result = SymbolExtensions2.GetMethodInfo(expression);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x00020BF8 File Offset: 0x0001EDF8
		public static MethodInfo GetMethodInfo<T1, T2, T3, T4, TResult>([Nullable(1)] Expression<Func<T1, T2, T3, T4, TResult>> expression)
		{
			bool flag = expression != null;
			MethodInfo result;
			if (flag)
			{
				result = SymbolExtensions2.GetMethodInfo(expression);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x00020C20 File Offset: 0x0001EE20
		public static MethodInfo GetMethodInfo<T1, T2, T3, T4, T5, TResult>([Nullable(1)] Expression<Func<T1, T2, T3, T4, T5, TResult>> expression)
		{
			bool flag = expression != null;
			MethodInfo result;
			if (flag)
			{
				result = SymbolExtensions2.GetMethodInfo(expression);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x00020C48 File Offset: 0x0001EE48
		public static MethodInfo GetMethodInfo<T1, T2, T3, T4, T5, T6, TResult>([Nullable(1)] Expression<Func<T1, T2, T3, T4, T5, T6, TResult>> expression)
		{
			bool flag = expression != null;
			MethodInfo result;
			if (flag)
			{
				result = SymbolExtensions2.GetMethodInfo(expression);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x00020C70 File Offset: 0x0001EE70
		public static MethodInfo GetMethodInfo<T1, T2, T3, T4, T5, T6, T7, TResult>([Nullable(1)] Expression<Func<T1, T2, T3, T4, T5, T6, T7, TResult>> expression)
		{
			bool flag = expression != null;
			MethodInfo result;
			if (flag)
			{
				result = SymbolExtensions2.GetMethodInfo(expression);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x00020C98 File Offset: 0x0001EE98
		public static MethodInfo GetMethodInfo<T1, T2, T3, T4, T5, T6, T7, T8, TResult>([Nullable(1)] Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult>> expression)
		{
			bool flag = expression != null;
			MethodInfo result;
			if (flag)
			{
				result = SymbolExtensions2.GetMethodInfo(expression);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x00020CC0 File Offset: 0x0001EEC0
		public static MethodInfo GetMethodInfo<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>([Nullable(1)] Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>> expression)
		{
			bool flag = expression != null;
			MethodInfo result;
			if (flag)
			{
				result = SymbolExtensions2.GetMethodInfo(expression);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x00020CE8 File Offset: 0x0001EEE8
		[NullableContext(1)]
		[return: Nullable(2)]
		public static MethodInfo GetMethodInfo(LambdaExpression expression)
		{
			MethodCallExpression methodCallExpression = ((expression != null) ? expression.Body : null) as MethodCallExpression;
			MethodInfo methodInfo;
			bool flag;
			if (methodCallExpression != null)
			{
				methodInfo = methodCallExpression.Method;
				flag = (methodInfo != null);
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			MethodInfo result;
			if (flag2)
			{
				result = methodInfo;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x00020D28 File Offset: 0x0001EF28
		public static PropertyInfo GetPropertyInfo<T>([Nullable(1)] Expression<Func<T>> expression)
		{
			bool flag = expression != null;
			PropertyInfo result;
			if (flag)
			{
				result = SymbolExtensions2.GetPropertyInfo(expression);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x00020D50 File Offset: 0x0001EF50
		public static PropertyInfo GetPropertyInfo<T, TResult>([Nullable(1)] Expression<Func<T, TResult>> expression)
		{
			bool flag = expression != null;
			PropertyInfo result;
			if (flag)
			{
				result = SymbolExtensions2.GetPropertyInfo(expression);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x00020D78 File Offset: 0x0001EF78
		[NullableContext(1)]
		[return: Nullable(2)]
		public static PropertyInfo GetPropertyInfo(LambdaExpression expression)
		{
			MemberExpression memberExpression = ((expression != null) ? expression.Body : null) as MemberExpression;
			PropertyInfo propertyInfo;
			bool flag;
			if (memberExpression != null)
			{
				propertyInfo = (memberExpression.Member as PropertyInfo);
				flag = (propertyInfo != null);
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			PropertyInfo result;
			if (flag2)
			{
				result = propertyInfo;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x00020DBC File Offset: 0x0001EFBC
		public static MethodInfo GetPropertyGetter<T>([Nullable(1)] Expression<Func<T>> expression)
		{
			bool flag = expression != null;
			MethodInfo result;
			if (flag)
			{
				result = SymbolExtensions2.GetPropertyGetter(expression);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x00020DE4 File Offset: 0x0001EFE4
		public static MethodInfo GetPropertyGetter<T, TResult>([Nullable(1)] Expression<Func<T, TResult>> expression)
		{
			bool flag = expression != null;
			MethodInfo result;
			if (flag)
			{
				result = SymbolExtensions2.GetPropertyGetter(expression);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x00020E0C File Offset: 0x0001F00C
		[NullableContext(1)]
		[return: Nullable(2)]
		public static MethodInfo GetPropertyGetter(LambdaExpression expression)
		{
			MemberExpression memberExpression = ((expression != null) ? expression.Body : null) as MemberExpression;
			PropertyInfo propertyInfo;
			bool flag;
			if (memberExpression != null)
			{
				propertyInfo = (memberExpression.Member as PropertyInfo);
				flag = (propertyInfo != null);
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			MethodInfo result;
			if (flag2)
			{
				result = ((propertyInfo != null) ? propertyInfo.GetGetMethod(true) : null);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x00020E5C File Offset: 0x0001F05C
		public static MethodInfo GetPropertySetter<T>([Nullable(1)] Expression<Func<T>> expression)
		{
			bool flag = expression != null;
			MethodInfo result;
			if (flag)
			{
				result = SymbolExtensions2.GetPropertySetter(expression);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x00020E84 File Offset: 0x0001F084
		public static MethodInfo GetPropertySetter<T, TResult>([Nullable(1)] Expression<Func<T, TResult>> expression)
		{
			bool flag = expression != null;
			MethodInfo result;
			if (flag)
			{
				result = SymbolExtensions2.GetPropertySetter(expression);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x00020EAC File Offset: 0x0001F0AC
		[NullableContext(1)]
		[return: Nullable(2)]
		public static MethodInfo GetPropertySetter(LambdaExpression expression)
		{
			MemberExpression memberExpression = ((expression != null) ? expression.Body : null) as MemberExpression;
			PropertyInfo propertyInfo;
			bool flag;
			if (memberExpression != null)
			{
				propertyInfo = (memberExpression.Member as PropertyInfo);
				flag = (propertyInfo != null);
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			MethodInfo result;
			if (flag2)
			{
				result = ((propertyInfo != null) ? propertyInfo.GetSetMethod(true) : null);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x00020EFC File Offset: 0x0001F0FC
		[NullableContext(1)]
		[return: Nullable(new byte[]
		{
			2,
			1
		})]
		public static AccessTools.FieldRef<TField> GetStaticFieldRefAccess<[Nullable(2)] TField>(Expression<Func<TField>> expression)
		{
			bool flag = expression != null;
			AccessTools.FieldRef<TField> result;
			if (flag)
			{
				result = SymbolExtensions2.GetStaticFieldRefAccess<TField>(expression);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x00020F24 File Offset: 0x0001F124
		[NullableContext(1)]
		[return: Nullable(new byte[]
		{
			2,
			1
		})]
		public static AccessTools.FieldRef<TField> GetStaticFieldRefAccess<[Nullable(2)] TField>(LambdaExpression expression)
		{
			MemberExpression memberExpression = ((expression != null) ? expression.Body : null) as MemberExpression;
			FieldInfo fieldInfo;
			bool flag;
			if (memberExpression != null)
			{
				fieldInfo = (memberExpression.Member as FieldInfo);
				flag = (fieldInfo != null);
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			AccessTools.FieldRef<TField> result;
			if (flag2)
			{
				result = ((fieldInfo == null) ? null : AccessTools2.StaticFieldRefAccess<TField>(fieldInfo, true));
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x00020F7C File Offset: 0x0001F17C
		[NullableContext(0)]
		[return: Nullable(new byte[]
		{
			2,
			0,
			1
		})]
		public static AccessTools.StructFieldRef<TObject, TField> GetStructFieldRefAccess<TObject, [Nullable(2)] TField>([Nullable(1)] Expression<Func<TField>> expression) where TObject : struct
		{
			bool flag = expression != null;
			AccessTools.StructFieldRef<TObject, TField> result;
			if (flag)
			{
				result = SymbolExtensions2.GetStructFieldRefAccess<TObject, TField>(expression);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x00020FA4 File Offset: 0x0001F1A4
		[NullableContext(0)]
		[return: Nullable(new byte[]
		{
			2,
			0,
			1
		})]
		public static AccessTools.StructFieldRef<TObject, TField> GetStructFieldRefAccess<TObject, [Nullable(2)] TField>([Nullable(1)] LambdaExpression expression) where TObject : struct
		{
			MemberExpression memberExpression = ((expression != null) ? expression.Body : null) as MemberExpression;
			FieldInfo fieldInfo;
			bool flag;
			if (memberExpression != null)
			{
				fieldInfo = (memberExpression.Member as FieldInfo);
				flag = (fieldInfo != null);
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			AccessTools.StructFieldRef<TObject, TField> result;
			if (flag2)
			{
				result = ((fieldInfo == null) ? null : AccessTools2.StructFieldRefAccess<TObject, TField>(fieldInfo, true));
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
