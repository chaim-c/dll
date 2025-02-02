﻿using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace HarmonyLib.BUTR.Extensions
{
	// Token: 0x02000015 RID: 21
	[NullableContext(2)]
	[Nullable(0)]
	internal static class SymbolExtensions2
	{
		// Token: 0x0600009D RID: 157 RVA: 0x0000544C File Offset: 0x0000364C
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

		// Token: 0x0600009E RID: 158 RVA: 0x00005474 File Offset: 0x00003674
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

		// Token: 0x0600009F RID: 159 RVA: 0x0000549C File Offset: 0x0000369C
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

		// Token: 0x060000A0 RID: 160 RVA: 0x000054C4 File Offset: 0x000036C4
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

		// Token: 0x060000A1 RID: 161 RVA: 0x000054EC File Offset: 0x000036EC
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

		// Token: 0x060000A2 RID: 162 RVA: 0x00005514 File Offset: 0x00003714
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

		// Token: 0x060000A3 RID: 163 RVA: 0x0000553C File Offset: 0x0000373C
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

		// Token: 0x060000A4 RID: 164 RVA: 0x00005564 File Offset: 0x00003764
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

		// Token: 0x060000A5 RID: 165 RVA: 0x0000558C File Offset: 0x0000378C
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

		// Token: 0x060000A6 RID: 166 RVA: 0x000055B4 File Offset: 0x000037B4
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

		// Token: 0x060000A7 RID: 167 RVA: 0x000055DC File Offset: 0x000037DC
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

		// Token: 0x060000A8 RID: 168 RVA: 0x00005620 File Offset: 0x00003820
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

		// Token: 0x060000A9 RID: 169 RVA: 0x00005648 File Offset: 0x00003848
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

		// Token: 0x060000AA RID: 170 RVA: 0x00005670 File Offset: 0x00003870
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

		// Token: 0x060000AB RID: 171 RVA: 0x000056B4 File Offset: 0x000038B4
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

		// Token: 0x060000AC RID: 172 RVA: 0x000056DC File Offset: 0x000038DC
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

		// Token: 0x060000AD RID: 173 RVA: 0x00005734 File Offset: 0x00003934
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

		// Token: 0x060000AE RID: 174 RVA: 0x0000575C File Offset: 0x0000395C
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

		// Token: 0x060000AF RID: 175 RVA: 0x000057B4 File Offset: 0x000039B4
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

		// Token: 0x060000B0 RID: 176 RVA: 0x000057DC File Offset: 0x000039DC
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

		// Token: 0x060000B1 RID: 177 RVA: 0x00005804 File Offset: 0x00003A04
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

		// Token: 0x060000B2 RID: 178 RVA: 0x0000582C File Offset: 0x00003A2C
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

		// Token: 0x060000B3 RID: 179 RVA: 0x00005854 File Offset: 0x00003A54
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

		// Token: 0x060000B4 RID: 180 RVA: 0x0000587C File Offset: 0x00003A7C
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

		// Token: 0x060000B5 RID: 181 RVA: 0x000058A4 File Offset: 0x00003AA4
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

		// Token: 0x060000B6 RID: 182 RVA: 0x000058CC File Offset: 0x00003ACC
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

		// Token: 0x060000B7 RID: 183 RVA: 0x000058F4 File Offset: 0x00003AF4
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

		// Token: 0x060000B8 RID: 184 RVA: 0x0000591C File Offset: 0x00003B1C
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

		// Token: 0x060000B9 RID: 185 RVA: 0x00005944 File Offset: 0x00003B44
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

		// Token: 0x060000BA RID: 186 RVA: 0x0000596C File Offset: 0x00003B6C
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

		// Token: 0x060000BB RID: 187 RVA: 0x00005994 File Offset: 0x00003B94
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

		// Token: 0x060000BC RID: 188 RVA: 0x000059BC File Offset: 0x00003BBC
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

		// Token: 0x060000BD RID: 189 RVA: 0x000059E4 File Offset: 0x00003BE4
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

		// Token: 0x060000BE RID: 190 RVA: 0x00005A0C File Offset: 0x00003C0C
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

		// Token: 0x060000BF RID: 191 RVA: 0x00005A34 File Offset: 0x00003C34
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

		// Token: 0x060000C0 RID: 192 RVA: 0x00005A5C File Offset: 0x00003C5C
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

		// Token: 0x060000C1 RID: 193 RVA: 0x00005A84 File Offset: 0x00003C84
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

		// Token: 0x060000C2 RID: 194 RVA: 0x00005AAC File Offset: 0x00003CAC
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

		// Token: 0x060000C3 RID: 195 RVA: 0x00005AD4 File Offset: 0x00003CD4
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

		// Token: 0x060000C4 RID: 196 RVA: 0x00005B14 File Offset: 0x00003D14
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

		// Token: 0x060000C5 RID: 197 RVA: 0x00005B3C File Offset: 0x00003D3C
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

		// Token: 0x060000C6 RID: 198 RVA: 0x00005B64 File Offset: 0x00003D64
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

		// Token: 0x060000C7 RID: 199 RVA: 0x00005BA8 File Offset: 0x00003DA8
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

		// Token: 0x060000C8 RID: 200 RVA: 0x00005BD0 File Offset: 0x00003DD0
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

		// Token: 0x060000C9 RID: 201 RVA: 0x00005BF8 File Offset: 0x00003DF8
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

		// Token: 0x060000CA RID: 202 RVA: 0x00005C48 File Offset: 0x00003E48
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

		// Token: 0x060000CB RID: 203 RVA: 0x00005C70 File Offset: 0x00003E70
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

		// Token: 0x060000CC RID: 204 RVA: 0x00005C98 File Offset: 0x00003E98
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

		// Token: 0x060000CD RID: 205 RVA: 0x00005CE8 File Offset: 0x00003EE8
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

		// Token: 0x060000CE RID: 206 RVA: 0x00005D10 File Offset: 0x00003F10
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

		// Token: 0x060000CF RID: 207 RVA: 0x00005D68 File Offset: 0x00003F68
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

		// Token: 0x060000D0 RID: 208 RVA: 0x00005D90 File Offset: 0x00003F90
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
