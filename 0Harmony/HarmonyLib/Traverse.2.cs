using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace HarmonyLib
{
	// Token: 0x02000054 RID: 84
	public class Traverse
	{
		// Token: 0x060003E1 RID: 993 RVA: 0x00013BC0 File Offset: 0x00011DC0
		[MethodImpl(MethodImplOptions.Synchronized)]
		static Traverse()
		{
			if (Traverse.Cache == null)
			{
				Traverse.Cache = new AccessCache();
			}
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00013BE8 File Offset: 0x00011DE8
		public static Traverse Create(Type type)
		{
			return new Traverse(type);
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00013BF0 File Offset: 0x00011DF0
		public static Traverse Create<T>()
		{
			return Traverse.Create(typeof(T));
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00013C01 File Offset: 0x00011E01
		public static Traverse Create(object root)
		{
			return new Traverse(root);
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x00013C09 File Offset: 0x00011E09
		public static Traverse CreateWithType(string name)
		{
			return new Traverse(AccessTools.TypeByName(name));
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x00013C16 File Offset: 0x00011E16
		private Traverse()
		{
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x00013C1E File Offset: 0x00011E1E
		public Traverse(Type type)
		{
			this._type = type;
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00013C2D File Offset: 0x00011E2D
		public Traverse(object root)
		{
			this._root = root;
			this._type = ((root != null) ? root.GetType() : null);
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00013C4E File Offset: 0x00011E4E
		private Traverse(object root, MemberInfo info, object[] index)
		{
			this._root = root;
			this._type = (((root != null) ? root.GetType() : null) ?? info.GetUnderlyingType());
			this._info = info;
			this._params = index;
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00013C87 File Offset: 0x00011E87
		private Traverse(object root, MethodInfo method, object[] parameter)
		{
			this._root = root;
			this._type = method.ReturnType;
			this._method = method;
			this._params = parameter;
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00013CB0 File Offset: 0x00011EB0
		public object GetValue()
		{
			if (this._info is FieldInfo)
			{
				return ((FieldInfo)this._info).GetValue(this._root);
			}
			if (this._info is PropertyInfo)
			{
				return ((PropertyInfo)this._info).GetValue(this._root, AccessTools.all, null, this._params, CultureInfo.CurrentCulture);
			}
			if (this._method != null)
			{
				return this._method.Invoke(this._root, this._params);
			}
			if (this._root == null && this._type != null)
			{
				return this._type;
			}
			return this._root;
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00013D54 File Offset: 0x00011F54
		public T GetValue<T>()
		{
			object value = this.GetValue();
			if (value == null)
			{
				return default(T);
			}
			return (T)((object)value);
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00013D7B File Offset: 0x00011F7B
		public object GetValue(params object[] arguments)
		{
			if (this._method == null)
			{
				throw new Exception("cannot get method value without method");
			}
			return this._method.Invoke(this._root, arguments);
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00013DA2 File Offset: 0x00011FA2
		public T GetValue<T>(params object[] arguments)
		{
			if (this._method == null)
			{
				throw new Exception("cannot get method value without method");
			}
			return (T)((object)this._method.Invoke(this._root, arguments));
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00013DD0 File Offset: 0x00011FD0
		public Traverse SetValue(object value)
		{
			if (this._info is FieldInfo)
			{
				((FieldInfo)this._info).SetValue(this._root, value, AccessTools.all, null, CultureInfo.CurrentCulture);
			}
			if (this._info is PropertyInfo)
			{
				((PropertyInfo)this._info).SetValue(this._root, value, AccessTools.all, null, this._params, CultureInfo.CurrentCulture);
			}
			if (this._method != null)
			{
				throw new Exception("cannot set value of method " + this._method.FullDescription());
			}
			return this;
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00013E65 File Offset: 0x00012065
		public Type GetValueType()
		{
			if (this._info is FieldInfo)
			{
				return ((FieldInfo)this._info).FieldType;
			}
			if (this._info is PropertyInfo)
			{
				return ((PropertyInfo)this._info).PropertyType;
			}
			return null;
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00013EA4 File Offset: 0x000120A4
		private Traverse Resolve()
		{
			if (this._root == null)
			{
				FieldInfo fieldInfo = this._info as FieldInfo;
				if (fieldInfo != null && fieldInfo.IsStatic)
				{
					return new Traverse(this.GetValue());
				}
				PropertyInfo propertyInfo = this._info as PropertyInfo;
				if (propertyInfo != null && propertyInfo.GetGetMethod().IsStatic)
				{
					return new Traverse(this.GetValue());
				}
				if (this._method != null && this._method.IsStatic)
				{
					return new Traverse(this.GetValue());
				}
				if (this._type != null)
				{
					return this;
				}
			}
			return new Traverse(this.GetValue());
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00013F3C File Offset: 0x0001213C
		public Traverse Type(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (this._type == null)
			{
				return new Traverse();
			}
			Type type = AccessTools.Inner(this._type, name);
			if (type == null)
			{
				return new Traverse();
			}
			return new Traverse(type);
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00013F84 File Offset: 0x00012184
		public Traverse Field(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			Traverse traverse = this.Resolve();
			if (traverse._type == null)
			{
				return new Traverse();
			}
			FieldInfo fieldInfo = Traverse.Cache.GetFieldInfo(traverse._type, name, AccessCache.MemberType.Any, false);
			if (fieldInfo == null)
			{
				return new Traverse();
			}
			if (!fieldInfo.IsStatic && traverse._root == null)
			{
				return new Traverse();
			}
			return new Traverse(traverse._root, fieldInfo, null);
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00013FF4 File Offset: 0x000121F4
		public Traverse<T> Field<T>(string name)
		{
			return new Traverse<T>(this.Field(name));
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00014004 File Offset: 0x00012204
		public List<string> Fields()
		{
			Traverse traverse = this.Resolve();
			return AccessTools.GetFieldNames(traverse._type);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00014024 File Offset: 0x00012224
		public Traverse Property(string name, object[] index = null)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			Traverse traverse = this.Resolve();
			if (traverse._type == null)
			{
				return new Traverse();
			}
			PropertyInfo propertyInfo = Traverse.Cache.GetPropertyInfo(traverse._type, name, AccessCache.MemberType.Any, false);
			if (propertyInfo == null)
			{
				return new Traverse();
			}
			return new Traverse(traverse._root, propertyInfo, index);
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0001407E File Offset: 0x0001227E
		public Traverse<T> Property<T>(string name, object[] index = null)
		{
			return new Traverse<T>(this.Property(name, index));
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00014090 File Offset: 0x00012290
		public List<string> Properties()
		{
			Traverse traverse = this.Resolve();
			return AccessTools.GetPropertyNames(traverse._type);
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x000140B0 File Offset: 0x000122B0
		public Traverse Method(string name, params object[] arguments)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			Traverse traverse = this.Resolve();
			if (traverse._type == null)
			{
				return new Traverse();
			}
			Type[] types = AccessTools.GetTypes(arguments);
			MethodBase methodInfo = Traverse.Cache.GetMethodInfo(traverse._type, name, types, AccessCache.MemberType.Any, false);
			if (methodInfo == null)
			{
				return new Traverse();
			}
			return new Traverse(traverse._root, (MethodInfo)methodInfo, arguments);
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00014118 File Offset: 0x00012318
		public Traverse Method(string name, Type[] paramTypes, object[] arguments = null)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			Traverse traverse = this.Resolve();
			if (traverse._type == null)
			{
				return new Traverse();
			}
			MethodBase methodInfo = Traverse.Cache.GetMethodInfo(traverse._type, name, paramTypes, AccessCache.MemberType.Any, false);
			if (methodInfo == null)
			{
				return new Traverse();
			}
			return new Traverse(traverse._root, (MethodInfo)methodInfo, arguments);
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00014178 File Offset: 0x00012378
		public List<string> Methods()
		{
			Traverse traverse = this.Resolve();
			return AccessTools.GetMethodNames(traverse._type);
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00014197 File Offset: 0x00012397
		public bool FieldExists()
		{
			return this._info != null && this._info is FieldInfo;
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x000141B1 File Offset: 0x000123B1
		public bool PropertyExists()
		{
			return this._info != null && this._info is PropertyInfo;
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x000141CB File Offset: 0x000123CB
		public bool MethodExists()
		{
			return this._method != null;
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x000141D9 File Offset: 0x000123D9
		public bool TypeExists()
		{
			return this._type != null;
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x000141E8 File Offset: 0x000123E8
		public static void IterateFields(object source, Action<Traverse> action)
		{
			Traverse sourceTrv = Traverse.Create(source);
			AccessTools.GetFieldNames(source).ForEach(delegate(string f)
			{
				action(sourceTrv.Field(f));
			});
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00014228 File Offset: 0x00012428
		public static void IterateFields(object source, object target, Action<Traverse, Traverse> action)
		{
			Traverse sourceTrv = Traverse.Create(source);
			Traverse targetTrv = Traverse.Create(target);
			AccessTools.GetFieldNames(source).ForEach(delegate(string f)
			{
				action(sourceTrv.Field(f), targetTrv.Field(f));
			});
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x00014274 File Offset: 0x00012474
		public static void IterateFields(object source, object target, Action<string, Traverse, Traverse> action)
		{
			Traverse sourceTrv = Traverse.Create(source);
			Traverse targetTrv = Traverse.Create(target);
			AccessTools.GetFieldNames(source).ForEach(delegate(string f)
			{
				action(f, sourceTrv.Field(f), targetTrv.Field(f));
			});
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x000142C0 File Offset: 0x000124C0
		public static void IterateProperties(object source, Action<Traverse> action)
		{
			Traverse sourceTrv = Traverse.Create(source);
			AccessTools.GetPropertyNames(source).ForEach(delegate(string f)
			{
				action(sourceTrv.Property(f, null));
			});
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00014300 File Offset: 0x00012500
		public static void IterateProperties(object source, object target, Action<Traverse, Traverse> action)
		{
			Traverse sourceTrv = Traverse.Create(source);
			Traverse targetTrv = Traverse.Create(target);
			AccessTools.GetPropertyNames(source).ForEach(delegate(string f)
			{
				action(sourceTrv.Property(f, null), targetTrv.Property(f, null));
			});
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0001434C File Offset: 0x0001254C
		public static void IterateProperties(object source, object target, Action<string, Traverse, Traverse> action)
		{
			Traverse sourceTrv = Traverse.Create(source);
			Traverse targetTrv = Traverse.Create(target);
			AccessTools.GetPropertyNames(source).ForEach(delegate(string f)
			{
				action(f, sourceTrv.Property(f, null), targetTrv.Property(f, null));
			});
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00014398 File Offset: 0x00012598
		public override string ToString()
		{
			object obj = this._method ?? this.GetValue();
			if (obj == null)
			{
				return null;
			}
			return obj.ToString();
		}

		// Token: 0x040000ED RID: 237
		private static readonly AccessCache Cache;

		// Token: 0x040000EE RID: 238
		private readonly Type _type;

		// Token: 0x040000EF RID: 239
		private readonly object _root;

		// Token: 0x040000F0 RID: 240
		private readonly MemberInfo _info;

		// Token: 0x040000F1 RID: 241
		private readonly MethodBase _method;

		// Token: 0x040000F2 RID: 242
		private readonly object[] _params;

		// Token: 0x040000F3 RID: 243
		public static Action<Traverse, Traverse> CopyFields = delegate(Traverse from, Traverse to)
		{
			to.SetValue(from.GetValue());
		};
	}
}
