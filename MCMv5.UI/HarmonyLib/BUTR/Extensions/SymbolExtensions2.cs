using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace HarmonyLib.BUTR.Extensions
{
	// Token: 0x02000068 RID: 104
	[NullableContext(2)]
	[Nullable(0)]
	internal static class SymbolExtensions2
	{
		// Token: 0x0600042F RID: 1071 RVA: 0x00012488 File Offset: 0x00010688
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

		// Token: 0x06000430 RID: 1072 RVA: 0x000124B0 File Offset: 0x000106B0
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

		// Token: 0x06000431 RID: 1073 RVA: 0x000124D8 File Offset: 0x000106D8
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

		// Token: 0x06000432 RID: 1074 RVA: 0x00012500 File Offset: 0x00010700
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

		// Token: 0x06000433 RID: 1075 RVA: 0x00012528 File Offset: 0x00010728
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

		// Token: 0x06000434 RID: 1076 RVA: 0x00012550 File Offset: 0x00010750
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

		// Token: 0x06000435 RID: 1077 RVA: 0x00012578 File Offset: 0x00010778
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

		// Token: 0x06000436 RID: 1078 RVA: 0x000125A0 File Offset: 0x000107A0
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

		// Token: 0x06000437 RID: 1079 RVA: 0x000125C8 File Offset: 0x000107C8
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

		// Token: 0x06000438 RID: 1080 RVA: 0x000125F0 File Offset: 0x000107F0
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

		// Token: 0x06000439 RID: 1081 RVA: 0x00012618 File Offset: 0x00010818
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

		// Token: 0x0600043A RID: 1082 RVA: 0x0001265C File Offset: 0x0001085C
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

		// Token: 0x0600043B RID: 1083 RVA: 0x00012684 File Offset: 0x00010884
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

		// Token: 0x0600043C RID: 1084 RVA: 0x000126AC File Offset: 0x000108AC
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

		// Token: 0x0600043D RID: 1085 RVA: 0x000126F0 File Offset: 0x000108F0
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

		// Token: 0x0600043E RID: 1086 RVA: 0x00012718 File Offset: 0x00010918
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

		// Token: 0x0600043F RID: 1087 RVA: 0x00012770 File Offset: 0x00010970
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

		// Token: 0x06000440 RID: 1088 RVA: 0x00012798 File Offset: 0x00010998
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

		// Token: 0x06000441 RID: 1089 RVA: 0x000127F0 File Offset: 0x000109F0
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

		// Token: 0x06000442 RID: 1090 RVA: 0x00012818 File Offset: 0x00010A18
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

		// Token: 0x06000443 RID: 1091 RVA: 0x00012840 File Offset: 0x00010A40
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

		// Token: 0x06000444 RID: 1092 RVA: 0x00012868 File Offset: 0x00010A68
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

		// Token: 0x06000445 RID: 1093 RVA: 0x00012890 File Offset: 0x00010A90
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

		// Token: 0x06000446 RID: 1094 RVA: 0x000128B8 File Offset: 0x00010AB8
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

		// Token: 0x06000447 RID: 1095 RVA: 0x000128E0 File Offset: 0x00010AE0
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

		// Token: 0x06000448 RID: 1096 RVA: 0x00012908 File Offset: 0x00010B08
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

		// Token: 0x06000449 RID: 1097 RVA: 0x00012930 File Offset: 0x00010B30
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

		// Token: 0x0600044A RID: 1098 RVA: 0x00012958 File Offset: 0x00010B58
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

		// Token: 0x0600044B RID: 1099 RVA: 0x00012980 File Offset: 0x00010B80
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

		// Token: 0x0600044C RID: 1100 RVA: 0x000129A8 File Offset: 0x00010BA8
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

		// Token: 0x0600044D RID: 1101 RVA: 0x000129D0 File Offset: 0x00010BD0
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

		// Token: 0x0600044E RID: 1102 RVA: 0x000129F8 File Offset: 0x00010BF8
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

		// Token: 0x0600044F RID: 1103 RVA: 0x00012A20 File Offset: 0x00010C20
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

		// Token: 0x06000450 RID: 1104 RVA: 0x00012A48 File Offset: 0x00010C48
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

		// Token: 0x06000451 RID: 1105 RVA: 0x00012A70 File Offset: 0x00010C70
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

		// Token: 0x06000452 RID: 1106 RVA: 0x00012A98 File Offset: 0x00010C98
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

		// Token: 0x06000453 RID: 1107 RVA: 0x00012AC0 File Offset: 0x00010CC0
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

		// Token: 0x06000454 RID: 1108 RVA: 0x00012AE8 File Offset: 0x00010CE8
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

		// Token: 0x06000455 RID: 1109 RVA: 0x00012B10 File Offset: 0x00010D10
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

		// Token: 0x06000456 RID: 1110 RVA: 0x00012B50 File Offset: 0x00010D50
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

		// Token: 0x06000457 RID: 1111 RVA: 0x00012B78 File Offset: 0x00010D78
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

		// Token: 0x06000458 RID: 1112 RVA: 0x00012BA0 File Offset: 0x00010DA0
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

		// Token: 0x06000459 RID: 1113 RVA: 0x00012BE4 File Offset: 0x00010DE4
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

		// Token: 0x0600045A RID: 1114 RVA: 0x00012C0C File Offset: 0x00010E0C
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

		// Token: 0x0600045B RID: 1115 RVA: 0x00012C34 File Offset: 0x00010E34
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

		// Token: 0x0600045C RID: 1116 RVA: 0x00012C84 File Offset: 0x00010E84
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

		// Token: 0x0600045D RID: 1117 RVA: 0x00012CAC File Offset: 0x00010EAC
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

		// Token: 0x0600045E RID: 1118 RVA: 0x00012CD4 File Offset: 0x00010ED4
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

		// Token: 0x0600045F RID: 1119 RVA: 0x00012D24 File Offset: 0x00010F24
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

		// Token: 0x06000460 RID: 1120 RVA: 0x00012D4C File Offset: 0x00010F4C
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

		// Token: 0x06000461 RID: 1121 RVA: 0x00012DA4 File Offset: 0x00010FA4
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

		// Token: 0x06000462 RID: 1122 RVA: 0x00012DCC File Offset: 0x00010FCC
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
