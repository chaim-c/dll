﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Serialization;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000051 RID: 81
	[NullableContext(1)]
	[Nullable(0)]
	internal static class DynamicUtils
	{
		// Token: 0x060004F0 RID: 1264 RVA: 0x00014837 File Offset: 0x00012A37
		public static IEnumerable<string> GetDynamicMemberNames(this IDynamicMetaObjectProvider dynamicProvider)
		{
			return dynamicProvider.GetMetaObject(Expression.Constant(dynamicProvider)).GetDynamicMemberNames();
		}

		// Token: 0x02000175 RID: 373
		[Nullable(0)]
		internal static class BinderWrapper
		{
			// Token: 0x06000E9D RID: 3741 RVA: 0x00040C5C File Offset: 0x0003EE5C
			private static void Init()
			{
				if (!DynamicUtils.BinderWrapper._init)
				{
					if (Type.GetType("Microsoft.CSharp.RuntimeBinder.Binder, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", false) == null)
					{
						throw new InvalidOperationException("Could not resolve type '{0}'. You may need to add a reference to Microsoft.CSharp.dll to work with dynamic types.".FormatWith(CultureInfo.InvariantCulture, "Microsoft.CSharp.RuntimeBinder.Binder, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"));
					}
					DynamicUtils.BinderWrapper._getCSharpArgumentInfoArray = DynamicUtils.BinderWrapper.CreateSharpArgumentInfoArray(new int[1]);
					DynamicUtils.BinderWrapper._setCSharpArgumentInfoArray = DynamicUtils.BinderWrapper.CreateSharpArgumentInfoArray(new int[]
					{
						0,
						3
					});
					DynamicUtils.BinderWrapper.CreateMemberCalls();
					DynamicUtils.BinderWrapper._init = true;
				}
			}

			// Token: 0x06000E9E RID: 3742 RVA: 0x00040CCC File Offset: 0x0003EECC
			private static object CreateSharpArgumentInfoArray(params int[] values)
			{
				Type type = Type.GetType("Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
				Type type2 = Type.GetType("Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfoFlags, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
				Array array = Array.CreateInstance(type, values.Length);
				for (int i = 0; i < values.Length; i++)
				{
					MethodBase method = type.GetMethod("Create", new Type[]
					{
						type2,
						typeof(string)
					});
					object obj = null;
					object[] array2 = new object[2];
					array2[0] = 0;
					object value = method.Invoke(obj, array2);
					array.SetValue(value, i);
				}
				return array;
			}

			// Token: 0x06000E9F RID: 3743 RVA: 0x00040D4C File Offset: 0x0003EF4C
			private static void CreateMemberCalls()
			{
				Type type = Type.GetType("Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", true);
				Type type2 = Type.GetType("Microsoft.CSharp.RuntimeBinder.CSharpBinderFlags, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", true);
				Type type3 = Type.GetType("Microsoft.CSharp.RuntimeBinder.Binder, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", true);
				Type type4 = typeof(IEnumerable<>).MakeGenericType(new Type[]
				{
					type
				});
				MethodInfo method = type3.GetMethod("GetMember", new Type[]
				{
					type2,
					typeof(string),
					typeof(Type),
					type4
				});
				DynamicUtils.BinderWrapper._getMemberCall = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(method);
				MethodInfo method2 = type3.GetMethod("SetMember", new Type[]
				{
					type2,
					typeof(string),
					typeof(Type),
					type4
				});
				DynamicUtils.BinderWrapper._setMemberCall = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(method2);
			}

			// Token: 0x06000EA0 RID: 3744 RVA: 0x00040E23 File Offset: 0x0003F023
			public static CallSiteBinder GetMember(string name, Type context)
			{
				DynamicUtils.BinderWrapper.Init();
				return (CallSiteBinder)DynamicUtils.BinderWrapper._getMemberCall(null, new object[]
				{
					0,
					name,
					context,
					DynamicUtils.BinderWrapper._getCSharpArgumentInfoArray
				});
			}

			// Token: 0x06000EA1 RID: 3745 RVA: 0x00040E59 File Offset: 0x0003F059
			public static CallSiteBinder SetMember(string name, Type context)
			{
				DynamicUtils.BinderWrapper.Init();
				return (CallSiteBinder)DynamicUtils.BinderWrapper._setMemberCall(null, new object[]
				{
					0,
					name,
					context,
					DynamicUtils.BinderWrapper._setCSharpArgumentInfoArray
				});
			}

			// Token: 0x0400069F RID: 1695
			public const string CSharpAssemblyName = "Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

			// Token: 0x040006A0 RID: 1696
			private const string BinderTypeName = "Microsoft.CSharp.RuntimeBinder.Binder, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

			// Token: 0x040006A1 RID: 1697
			private const string CSharpArgumentInfoTypeName = "Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

			// Token: 0x040006A2 RID: 1698
			private const string CSharpArgumentInfoFlagsTypeName = "Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfoFlags, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

			// Token: 0x040006A3 RID: 1699
			private const string CSharpBinderFlagsTypeName = "Microsoft.CSharp.RuntimeBinder.CSharpBinderFlags, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

			// Token: 0x040006A4 RID: 1700
			[Nullable(2)]
			private static object _getCSharpArgumentInfoArray;

			// Token: 0x040006A5 RID: 1701
			[Nullable(2)]
			private static object _setCSharpArgumentInfoArray;

			// Token: 0x040006A6 RID: 1702
			[Nullable(2)]
			private static MethodCall<object, object> _getMemberCall;

			// Token: 0x040006A7 RID: 1703
			[Nullable(2)]
			private static MethodCall<object, object> _setMemberCall;

			// Token: 0x040006A8 RID: 1704
			private static bool _init;
		}
	}
}
