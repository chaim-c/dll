﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace HarmonyLib.BUTR.Extensions
{
	// Token: 0x02000017 RID: 23
	[NullableContext(1)]
	[Nullable(0)]
	internal class Traverse2
	{
		// Token: 0x060000D5 RID: 213 RVA: 0x00005E30 File Offset: 0x00004030
		[MethodImpl(MethodImplOptions.Synchronized)]
		static Traverse2()
		{
			if (Traverse2.Cache == null)
			{
				Traverse2.Cache = AccessCacheHandle.Create();
			}
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00005E6E File Offset: 0x0000406E
		public static Traverse2 Create([Nullable(2)] Type type)
		{
			return new Traverse2(type);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00005E76 File Offset: 0x00004076
		public static Traverse2 Create<[Nullable(2)] T>()
		{
			return Traverse2.Create(typeof(T));
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00005E87 File Offset: 0x00004087
		public static Traverse2 Create([Nullable(2)] object root)
		{
			return new Traverse2(root);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00005E8F File Offset: 0x0000408F
		public static Traverse2 CreateWithType(string name)
		{
			return new Traverse2(AccessTools2.TypeByName(name, true));
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00005E9D File Offset: 0x0000409D
		private Traverse2()
		{
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00005EA7 File Offset: 0x000040A7
		[NullableContext(2)]
		public Traverse2(Type type)
		{
			this._type = type;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00005EB8 File Offset: 0x000040B8
		[NullableContext(2)]
		public Traverse2(object root)
		{
			this._root = root;
			this._type = ((root != null) ? root.GetType() : null);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00005EDB File Offset: 0x000040DB
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

		// Token: 0x060000DE RID: 222 RVA: 0x00005F16 File Offset: 0x00004116
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

		// Token: 0x060000DF RID: 223 RVA: 0x00005F44 File Offset: 0x00004144
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

		// Token: 0x060000E0 RID: 224 RVA: 0x0000600C File Offset: 0x0000420C
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

		// Token: 0x060000E1 RID: 225 RVA: 0x0000604A File Offset: 0x0000424A
		[return: Nullable(2)]
		public object GetValue(params object[] arguments)
		{
			MethodBase method = this._method;
			return (method != null) ? method.Invoke(this._root, arguments) : null;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00006068 File Offset: 0x00004268
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

		// Token: 0x060000E3 RID: 227 RVA: 0x000060BC File Offset: 0x000042BC
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

		// Token: 0x060000E4 RID: 228 RVA: 0x00006184 File Offset: 0x00004384
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

		// Token: 0x060000E5 RID: 229 RVA: 0x000061D4 File Offset: 0x000043D4
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

		// Token: 0x060000E6 RID: 230 RVA: 0x000062A8 File Offset: 0x000044A8
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

		// Token: 0x060000E7 RID: 231 RVA: 0x00006308 File Offset: 0x00004508
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

		// Token: 0x060000E8 RID: 232 RVA: 0x000063B7 File Offset: 0x000045B7
		public Traverse2<T> Field<[Nullable(2)] T>(string name)
		{
			return new Traverse2<T>(this.Field(name));
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000063C8 File Offset: 0x000045C8
		public List<string> Fields()
		{
			Traverse2 resolved = this.Resolve();
			return AccessTools.GetFieldNames(resolved._type);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000063EC File Offset: 0x000045EC
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

		// Token: 0x060000EB RID: 235 RVA: 0x00006476 File Offset: 0x00004676
		public Traverse2<T> Property<[Nullable(2)] T>(string name, [Nullable(new byte[]
		{
			2,
			1
		})] object[] index = null)
		{
			return new Traverse2<T>(this.Property(name, index));
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00006488 File Offset: 0x00004688
		public List<string> Properties()
		{
			Traverse2 resolved = this.Resolve();
			return AccessTools.GetPropertyNames(resolved._type);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000064AC File Offset: 0x000046AC
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

		// Token: 0x060000EE RID: 238 RVA: 0x00006550 File Offset: 0x00004750
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

		// Token: 0x060000EF RID: 239 RVA: 0x000065EC File Offset: 0x000047EC
		public List<string> Methods()
		{
			Traverse2 resolved = this.Resolve();
			return AccessTools.GetMethodNames(resolved._type);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00006610 File Offset: 0x00004810
		public bool FieldExists()
		{
			return this._info is FieldInfo;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00006620 File Offset: 0x00004820
		public bool PropertyExists()
		{
			return this._info is PropertyInfo;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00006630 File Offset: 0x00004830
		public bool MethodExists()
		{
			return this._method != null;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x0000663E File Offset: 0x0000483E
		public bool TypeExists()
		{
			return this._type != null;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x0000664C File Offset: 0x0000484C
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

		// Token: 0x060000F5 RID: 245 RVA: 0x0000669C File Offset: 0x0000489C
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

		// Token: 0x060000F6 RID: 246 RVA: 0x000066F8 File Offset: 0x000048F8
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

		// Token: 0x060000F7 RID: 247 RVA: 0x00006754 File Offset: 0x00004954
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

		// Token: 0x060000F8 RID: 248 RVA: 0x000067A4 File Offset: 0x000049A4
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

		// Token: 0x060000F9 RID: 249 RVA: 0x00006800 File Offset: 0x00004A00
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

		// Token: 0x060000FA RID: 250 RVA: 0x0000685A File Offset: 0x00004A5A
		[NullableContext(2)]
		public override string ToString()
		{
			MethodBase methodBase = this._method ?? this.GetValue();
			return (methodBase != null) ? methodBase.ToString() : null;
		}

		// Token: 0x04000014 RID: 20
		private static readonly AccessCacheHandle? Cache;

		// Token: 0x04000015 RID: 21
		[Nullable(2)]
		private readonly Type _type;

		// Token: 0x04000016 RID: 22
		[Nullable(2)]
		private readonly object _root;

		// Token: 0x04000017 RID: 23
		[Nullable(2)]
		private readonly MemberInfo _info;

		// Token: 0x04000018 RID: 24
		[Nullable(2)]
		private readonly MethodBase _method;

		// Token: 0x04000019 RID: 25
		[Nullable(new byte[]
		{
			2,
			1
		})]
		private readonly object[] _params;

		// Token: 0x0400001A RID: 26
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
