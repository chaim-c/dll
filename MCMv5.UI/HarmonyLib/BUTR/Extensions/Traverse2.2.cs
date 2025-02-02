using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace HarmonyLib.BUTR.Extensions
{
	// Token: 0x0200006A RID: 106
	[NullableContext(1)]
	[Nullable(0)]
	internal class Traverse2
	{
		// Token: 0x06000467 RID: 1127 RVA: 0x00012E6C File Offset: 0x0001106C
		[MethodImpl(MethodImplOptions.Synchronized)]
		static Traverse2()
		{
			if (Traverse2.Cache == null)
			{
				Traverse2.Cache = AccessCacheHandle.Create();
			}
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00012EAA File Offset: 0x000110AA
		public static Traverse2 Create([Nullable(2)] Type type)
		{
			return new Traverse2(type);
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00012EB2 File Offset: 0x000110B2
		public static Traverse2 Create<[Nullable(2)] T>()
		{
			return Traverse2.Create(typeof(T));
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00012EC3 File Offset: 0x000110C3
		public static Traverse2 Create([Nullable(2)] object root)
		{
			return new Traverse2(root);
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00012ECB File Offset: 0x000110CB
		public static Traverse2 CreateWithType(string name)
		{
			return new Traverse2(AccessTools2.TypeByName(name, true));
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00012ED9 File Offset: 0x000110D9
		private Traverse2()
		{
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00012EE3 File Offset: 0x000110E3
		[NullableContext(2)]
		public Traverse2(Type type)
		{
			this._type = type;
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00012EF4 File Offset: 0x000110F4
		[NullableContext(2)]
		public Traverse2(object root)
		{
			this._root = root;
			this._type = ((root != null) ? root.GetType() : null);
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00012F17 File Offset: 0x00011117
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

		// Token: 0x06000470 RID: 1136 RVA: 0x00012F52 File Offset: 0x00011152
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

		// Token: 0x06000471 RID: 1137 RVA: 0x00012F80 File Offset: 0x00011180
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

		// Token: 0x06000472 RID: 1138 RVA: 0x00013048 File Offset: 0x00011248
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

		// Token: 0x06000473 RID: 1139 RVA: 0x00013086 File Offset: 0x00011286
		[return: Nullable(2)]
		public object GetValue(params object[] arguments)
		{
			MethodBase method = this._method;
			return (method != null) ? method.Invoke(this._root, arguments) : null;
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x000130A4 File Offset: 0x000112A4
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

		// Token: 0x06000475 RID: 1141 RVA: 0x000130F8 File Offset: 0x000112F8
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

		// Token: 0x06000476 RID: 1142 RVA: 0x000131C0 File Offset: 0x000113C0
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

		// Token: 0x06000477 RID: 1143 RVA: 0x00013210 File Offset: 0x00011410
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

		// Token: 0x06000478 RID: 1144 RVA: 0x000132E4 File Offset: 0x000114E4
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

		// Token: 0x06000479 RID: 1145 RVA: 0x00013344 File Offset: 0x00011544
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

		// Token: 0x0600047A RID: 1146 RVA: 0x000133F3 File Offset: 0x000115F3
		public Traverse2<T> Field<[Nullable(2)] T>(string name)
		{
			return new Traverse2<T>(this.Field(name));
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00013404 File Offset: 0x00011604
		public List<string> Fields()
		{
			Traverse2 resolved = this.Resolve();
			return AccessTools.GetFieldNames(resolved._type);
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00013428 File Offset: 0x00011628
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

		// Token: 0x0600047D RID: 1149 RVA: 0x000134B2 File Offset: 0x000116B2
		public Traverse2<T> Property<[Nullable(2)] T>(string name, [Nullable(new byte[]
		{
			2,
			1
		})] object[] index = null)
		{
			return new Traverse2<T>(this.Property(name, index));
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x000134C4 File Offset: 0x000116C4
		public List<string> Properties()
		{
			Traverse2 resolved = this.Resolve();
			return AccessTools.GetPropertyNames(resolved._type);
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x000134E8 File Offset: 0x000116E8
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

		// Token: 0x06000480 RID: 1152 RVA: 0x0001358C File Offset: 0x0001178C
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

		// Token: 0x06000481 RID: 1153 RVA: 0x00013628 File Offset: 0x00011828
		public List<string> Methods()
		{
			Traverse2 resolved = this.Resolve();
			return AccessTools.GetMethodNames(resolved._type);
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x0001364C File Offset: 0x0001184C
		public bool FieldExists()
		{
			return this._info is FieldInfo;
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x0001365C File Offset: 0x0001185C
		public bool PropertyExists()
		{
			return this._info is PropertyInfo;
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0001366C File Offset: 0x0001186C
		public bool MethodExists()
		{
			return this._method != null;
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0001367A File Offset: 0x0001187A
		public bool TypeExists()
		{
			return this._type != null;
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00013688 File Offset: 0x00011888
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

		// Token: 0x06000487 RID: 1159 RVA: 0x000136D8 File Offset: 0x000118D8
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

		// Token: 0x06000488 RID: 1160 RVA: 0x00013734 File Offset: 0x00011934
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

		// Token: 0x06000489 RID: 1161 RVA: 0x00013790 File Offset: 0x00011990
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

		// Token: 0x0600048A RID: 1162 RVA: 0x000137E0 File Offset: 0x000119E0
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

		// Token: 0x0600048B RID: 1163 RVA: 0x0001383C File Offset: 0x00011A3C
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

		// Token: 0x0600048C RID: 1164 RVA: 0x00013896 File Offset: 0x00011A96
		[NullableContext(2)]
		public override string ToString()
		{
			MethodBase methodBase = this._method ?? this.GetValue();
			return (methodBase != null) ? methodBase.ToString() : null;
		}

		// Token: 0x0400015D RID: 349
		private static readonly AccessCacheHandle? Cache;

		// Token: 0x0400015E RID: 350
		[Nullable(2)]
		private readonly Type _type;

		// Token: 0x0400015F RID: 351
		[Nullable(2)]
		private readonly object _root;

		// Token: 0x04000160 RID: 352
		[Nullable(2)]
		private readonly MemberInfo _info;

		// Token: 0x04000161 RID: 353
		[Nullable(2)]
		private readonly MethodBase _method;

		// Token: 0x04000162 RID: 354
		[Nullable(new byte[]
		{
			2,
			1
		})]
		private readonly object[] _params;

		// Token: 0x04000163 RID: 355
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
