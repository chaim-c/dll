using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace HarmonyLib.BUTR.Extensions
{
	// Token: 0x02000156 RID: 342
	[NullableContext(1)]
	[Nullable(0)]
	internal class Traverse2
	{
		// Token: 0x0600099B RID: 2459 RVA: 0x00021044 File Offset: 0x0001F244
		[MethodImpl(MethodImplOptions.Synchronized)]
		static Traverse2()
		{
			if (Traverse2.Cache == null)
			{
				Traverse2.Cache = AccessCacheHandle.Create();
			}
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x00021082 File Offset: 0x0001F282
		public static Traverse2 Create([Nullable(2)] Type type)
		{
			return new Traverse2(type);
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x0002108A File Offset: 0x0001F28A
		public static Traverse2 Create<[Nullable(2)] T>()
		{
			return Traverse2.Create(typeof(T));
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x0002109B File Offset: 0x0001F29B
		public static Traverse2 Create([Nullable(2)] object root)
		{
			return new Traverse2(root);
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x000210A3 File Offset: 0x0001F2A3
		public static Traverse2 CreateWithType(string name)
		{
			return new Traverse2(AccessTools2.TypeByName(name, true));
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x000210B1 File Offset: 0x0001F2B1
		private Traverse2()
		{
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x000210BB File Offset: 0x0001F2BB
		[NullableContext(2)]
		public Traverse2(Type type)
		{
			this._type = type;
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x000210CC File Offset: 0x0001F2CC
		[NullableContext(2)]
		public Traverse2(object root)
		{
			this._root = root;
			this._type = ((root != null) ? root.GetType() : null);
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x000210EF File Offset: 0x0001F2EF
		private Traverse2([Nullable(2)] object root, MemberInfo info, [Nullable(new byte[]
		{
			2,
			1
		})] object[] index)
		{
			this._root = root;
			this._type = (((root != null) ? root.GetType() : null) ?? info.GetUnderlyingType());
			this._info = info;
			this._params = index;
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x0002112A File Offset: 0x0001F32A
		private Traverse2([Nullable(2)] object root, MethodInfo method, [Nullable(new byte[]
		{
			2,
			1
		})] object[] parameter)
		{
			this._root = root;
			this._type = method.ReturnType;
			this._method = method;
			this._params = parameter;
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x00021158 File Offset: 0x0001F358
		[NullableContext(2)]
		public object GetValue()
		{
			FieldInfo fieldInfo = this._info as FieldInfo;
			bool flag = fieldInfo != null;
			object result;
			if (flag)
			{
				result = fieldInfo.GetValue(this._root);
			}
			else
			{
				PropertyInfo propertyInfo = this._info as PropertyInfo;
				bool flag2 = propertyInfo != null;
				if (flag2)
				{
					result = propertyInfo.GetValue(this._root, AccessTools.all, null, this._params, CultureInfo.CurrentCulture);
				}
				else
				{
					MethodBase methodBase = this._method;
					bool flag3 = methodBase != null;
					if (flag3)
					{
						result = methodBase.Invoke(this._root, this._params);
					}
					else
					{
						bool flag4 = this._root == null && this._type != null;
						if (flag4)
						{
							result = this._type;
						}
						else
						{
							result = this._root;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x00021220 File Offset: 0x0001F420
		[NullableContext(2)]
		public T GetValue<T>()
		{
			object value2 = this.GetValue();
			T value;
			bool flag;
			if (value2 is T)
			{
				value = (T)((object)value2);
				flag = true;
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			T result;
			if (flag2)
			{
				result = value;
			}
			else
			{
				result = default(T);
			}
			return result;
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x0002125E File Offset: 0x0001F45E
		[return: Nullable(2)]
		public object GetValue(params object[] arguments)
		{
			MethodBase method = this._method;
			return (method != null) ? method.Invoke(this._root, arguments) : null;
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x0002127C File Offset: 0x0001F47C
		[NullableContext(2)]
		public T GetValue<T>([Nullable(1)] params object[] arguments)
		{
			MethodBase method = this._method;
			object obj = (method != null) ? method.Invoke(this._root, arguments) : null;
			T value;
			bool flag;
			if (obj is T)
			{
				value = (T)((object)obj);
				flag = true;
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			T result;
			if (flag2)
			{
				result = value;
			}
			else
			{
				result = default(T);
			}
			return result;
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x000212D0 File Offset: 0x0001F4D0
		public Traverse2 SetValue(object value)
		{
			FieldInfo fieldInfo = this._info as FieldInfo;
			bool flag = fieldInfo != null && ((this._root == null && fieldInfo.IsStatic) || this._root != null);
			if (flag)
			{
				fieldInfo.SetValue(this._root, value, AccessTools.all, null, CultureInfo.CurrentCulture);
			}
			PropertyInfo propertyInfo = this._info as PropertyInfo;
			bool flag2 = propertyInfo != null && propertyInfo.SetMethod != null && ((this._root == null && propertyInfo.SetMethod.IsStatic) || this._root != null);
			if (flag2)
			{
				propertyInfo.SetValue(this._root, value, AccessTools.all, null, this._params, CultureInfo.CurrentCulture);
			}
			return this;
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x00021398 File Offset: 0x0001F598
		[NullableContext(2)]
		public Type GetValueType()
		{
			FieldInfo fieldInfo = this._info as FieldInfo;
			bool flag = fieldInfo != null;
			Type result;
			if (flag)
			{
				result = fieldInfo.FieldType;
			}
			else
			{
				PropertyInfo propertyInfo = this._info as PropertyInfo;
				bool flag2 = propertyInfo != null;
				if (flag2)
				{
					result = propertyInfo.PropertyType;
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x000213E8 File Offset: 0x0001F5E8
		private Traverse2 Resolve()
		{
			bool flag = this._root == null;
			if (flag)
			{
				FieldInfo fieldInfo = this._info as FieldInfo;
				bool flag2 = fieldInfo != null && fieldInfo.IsStatic;
				if (flag2)
				{
					return new Traverse2(this.GetValue());
				}
				PropertyInfo propertyInfo = this._info as PropertyInfo;
				bool flag3 = propertyInfo != null && propertyInfo.GetGetMethod().IsStatic;
				if (flag3)
				{
					return new Traverse2(this.GetValue());
				}
				MethodBase method = this._method;
				bool flag4 = method != null && method.IsStatic;
				if (flag4)
				{
					return new Traverse2(this.GetValue());
				}
				bool flag5 = this._type != null;
				if (flag5)
				{
					return this;
				}
			}
			return new Traverse2(this.GetValue());
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x000214BC File Offset: 0x0001F6BC
		public Traverse2 Type(string name)
		{
			bool flag = string.IsNullOrEmpty(name);
			Traverse2 result;
			if (flag)
			{
				result = new Traverse2();
			}
			else
			{
				bool flag2 = this._type == null;
				if (flag2)
				{
					result = new Traverse2();
				}
				else
				{
					Type type = AccessTools.Inner(this._type, name);
					bool flag3 = type == null;
					if (flag3)
					{
						result = new Traverse2();
					}
					else
					{
						result = new Traverse2(type);
					}
				}
			}
			return result;
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x0002151C File Offset: 0x0001F71C
		public Traverse2 Field(string name)
		{
			bool flag = string.IsNullOrEmpty(name);
			Traverse2 result;
			if (flag)
			{
				result = new Traverse2();
			}
			else
			{
				Traverse2 resolved = this.Resolve();
				bool flag2 = resolved._type == null;
				if (flag2)
				{
					result = new Traverse2();
				}
				else
				{
					FieldInfo fieldInfo = (Traverse2.Cache != null) ? Traverse2.Cache.GetValueOrDefault().GetFieldInfo(resolved._type, name, AccessCacheHandle.MemberType.Any, false) : null;
					bool flag3 = fieldInfo == null;
					if (flag3)
					{
						result = new Traverse2();
					}
					else
					{
						bool flag4 = !fieldInfo.IsStatic && resolved._root == null;
						if (flag4)
						{
							result = new Traverse2();
						}
						else
						{
							result = new Traverse2(resolved._root, fieldInfo, null);
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x000215CB File Offset: 0x0001F7CB
		public Traverse2<T> Field<[Nullable(2)] T>(string name)
		{
			return new Traverse2<T>(this.Field(name));
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x000215DC File Offset: 0x0001F7DC
		public List<string> Fields()
		{
			Traverse2 resolved = this.Resolve();
			return AccessTools.GetFieldNames(resolved._type);
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x00021600 File Offset: 0x0001F800
		public Traverse2 Property(string name, [Nullable(new byte[]
		{
			2,
			1
		})] object[] index = null)
		{
			bool flag = string.IsNullOrEmpty(name);
			Traverse2 result;
			if (flag)
			{
				result = new Traverse2();
			}
			else
			{
				Traverse2 resolved = this.Resolve();
				bool flag2 = resolved._type == null;
				if (flag2)
				{
					result = new Traverse2();
				}
				else
				{
					PropertyInfo info = (Traverse2.Cache != null) ? Traverse2.Cache.GetValueOrDefault().GetPropertyInfo(resolved._type, name, AccessCacheHandle.MemberType.Any, false) : null;
					bool flag3 = info == null;
					if (flag3)
					{
						result = new Traverse2();
					}
					else
					{
						result = new Traverse2(resolved._root, info, index);
					}
				}
			}
			return result;
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x0002168A File Offset: 0x0001F88A
		public Traverse2<T> Property<[Nullable(2)] T>(string name, [Nullable(new byte[]
		{
			2,
			1
		})] object[] index = null)
		{
			return new Traverse2<T>(this.Property(name, index));
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x0002169C File Offset: 0x0001F89C
		public List<string> Properties()
		{
			Traverse2 resolved = this.Resolve();
			return AccessTools.GetPropertyNames(resolved._type);
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x000216C0 File Offset: 0x0001F8C0
		public Traverse2 Method(string name, params object[] arguments)
		{
			bool flag = string.IsNullOrEmpty(name);
			Traverse2 result;
			if (flag)
			{
				result = new Traverse2();
			}
			else
			{
				Traverse2 resolved = this.Resolve();
				bool flag2 = resolved._type == null;
				if (flag2)
				{
					result = new Traverse2();
				}
				else
				{
					Type[] types = AccessTools.GetTypes(arguments);
					MethodBase method = (Traverse2.Cache != null) ? Traverse2.Cache.GetValueOrDefault().GetMethodInfo(resolved._type, name, types, AccessCacheHandle.MemberType.Any, false) : null;
					MethodInfo methodInfo = method as MethodInfo;
					bool flag3 = methodInfo == null;
					if (flag3)
					{
						result = new Traverse2();
					}
					else
					{
						result = new Traverse2(resolved._root, methodInfo, arguments);
					}
				}
			}
			return result;
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x00021764 File Offset: 0x0001F964
		public Traverse2 Method(string name, Type[] paramTypes, [Nullable(new byte[]
		{
			2,
			1
		})] object[] arguments = null)
		{
			bool flag = string.IsNullOrEmpty(name);
			Traverse2 result;
			if (flag)
			{
				result = new Traverse2();
			}
			else
			{
				Traverse2 resolved = this.Resolve();
				bool flag2 = resolved._type == null;
				if (flag2)
				{
					result = new Traverse2();
				}
				else
				{
					MethodBase method = (Traverse2.Cache != null) ? Traverse2.Cache.GetValueOrDefault().GetMethodInfo(resolved._type, name, paramTypes, AccessCacheHandle.MemberType.Any, false) : null;
					MethodInfo methodInfo = method as MethodInfo;
					bool flag3 = methodInfo == null;
					if (flag3)
					{
						result = new Traverse2();
					}
					else
					{
						result = new Traverse2(resolved._root, methodInfo, arguments);
					}
				}
			}
			return result;
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x00021800 File Offset: 0x0001FA00
		public List<string> Methods()
		{
			Traverse2 resolved = this.Resolve();
			return AccessTools.GetMethodNames(resolved._type);
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x00021824 File Offset: 0x0001FA24
		public bool FieldExists()
		{
			return this._info is FieldInfo;
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x00021834 File Offset: 0x0001FA34
		public bool PropertyExists()
		{
			return this._info is PropertyInfo;
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x00021844 File Offset: 0x0001FA44
		public bool MethodExists()
		{
			return this._method != null;
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x00021852 File Offset: 0x0001FA52
		public bool TypeExists()
		{
			return this._type != null;
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x00021860 File Offset: 0x0001FA60
		public static void IterateFields(object source, Action<Traverse2> action)
		{
			bool flag = action == null;
			if (!flag)
			{
				Traverse2 sourceTrv = Traverse2.Create(source);
				AccessTools.GetFieldNames(source).ForEach(delegate(string f)
				{
					action(sourceTrv.Field(f));
				});
			}
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x000218B0 File Offset: 0x0001FAB0
		public static void IterateFields(object source, object target, Action<Traverse2, Traverse2> action)
		{
			bool flag = action == null;
			if (!flag)
			{
				Traverse2 sourceTrv = Traverse2.Create(source);
				Traverse2 targetTrv = Traverse2.Create(target);
				AccessTools.GetFieldNames(source).ForEach(delegate(string f)
				{
					action(sourceTrv.Field(f), targetTrv.Field(f));
				});
			}
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x0002190C File Offset: 0x0001FB0C
		public static void IterateFields(object source, object target, Action<string, Traverse2, Traverse2> action)
		{
			bool flag = action == null;
			if (!flag)
			{
				Traverse2 sourceTrv = Traverse2.Create(source);
				Traverse2 targetTrv = Traverse2.Create(target);
				AccessTools.GetFieldNames(source).ForEach(delegate(string f)
				{
					action(f, sourceTrv.Field(f), targetTrv.Field(f));
				});
			}
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x00021968 File Offset: 0x0001FB68
		public static void IterateProperties(object source, Action<Traverse2> action)
		{
			bool flag = action == null;
			if (!flag)
			{
				Traverse2 sourceTrv = Traverse2.Create(source);
				AccessTools.GetPropertyNames(source).ForEach(delegate(string f)
				{
					action(sourceTrv.Property(f, null));
				});
			}
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x000219B8 File Offset: 0x0001FBB8
		public static void IterateProperties(object source, object target, Action<Traverse2, Traverse2> action)
		{
			bool flag = action == null;
			if (!flag)
			{
				Traverse2 sourceTrv = Traverse2.Create(source);
				Traverse2 targetTrv = Traverse2.Create(target);
				AccessTools.GetPropertyNames(source).ForEach(delegate(string f)
				{
					action(sourceTrv.Property(f, null), targetTrv.Property(f, null));
				});
			}
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x00021A14 File Offset: 0x0001FC14
		public static void IterateProperties(object source, object target, Action<string, Traverse2, Traverse2> action)
		{
			bool flag = action == null;
			if (!flag)
			{
				Traverse2 sourceTrv = Traverse2.Create(source);
				Traverse2 targetTrv = Traverse2.Create(target);
				AccessTools.GetPropertyNames(source).ForEach(delegate(string f)
				{
					action(f, sourceTrv.Property(f, null), targetTrv.Property(f, null));
				});
			}
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x00021A6E File Offset: 0x0001FC6E
		[NullableContext(2)]
		public override string ToString()
		{
			MethodBase methodBase = this._method ?? this.GetValue();
			return (methodBase != null) ? methodBase.ToString() : null;
		}

		// Token: 0x0400029F RID: 671
		private static readonly AccessCacheHandle? Cache;

		// Token: 0x040002A0 RID: 672
		[Nullable(2)]
		private readonly Type _type;

		// Token: 0x040002A1 RID: 673
		[Nullable(2)]
		private readonly object _root;

		// Token: 0x040002A2 RID: 674
		[Nullable(2)]
		private readonly MemberInfo _info;

		// Token: 0x040002A3 RID: 675
		[Nullable(2)]
		private readonly MethodBase _method;

		// Token: 0x040002A4 RID: 676
		[Nullable(new byte[]
		{
			2,
			1
		})]
		private readonly object[] _params;

		// Token: 0x040002A5 RID: 677
		public static Action<Traverse2, Traverse2> CopyFields = delegate(Traverse2 from, Traverse2 to)
		{
			bool flag = from == null || to == null;
			if (!flag)
			{
				to.SetValue(from.GetValue());
			}
		};
	}
}
