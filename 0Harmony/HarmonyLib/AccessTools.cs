using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.Serialization;
using System.Threading;
using MonoMod.Core.Platforms;
using MonoMod.Utils;

namespace HarmonyLib
{
	// Token: 0x02000046 RID: 70
	public static class AccessTools
	{
		// Token: 0x060001B1 RID: 433 RVA: 0x0000CF19 File Offset: 0x0000B119
		public static IEnumerable<Assembly> AllAssemblies()
		{
			return from a in AppDomain.CurrentDomain.GetAssemblies()
			where !a.FullName.StartsWith("Microsoft.VisualStudio")
			select a;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000CF4C File Offset: 0x0000B14C
		public static Type TypeByName(string name)
		{
			Type type = Type.GetType(name, false);
			if (type == null)
			{
				type = AccessTools.AllTypes().FirstOrDefault((Type t) => t.FullName == name);
			}
			if (type == null)
			{
				type = AccessTools.AllTypes().FirstOrDefault((Type t) => t.Name == name);
			}
			if (type == null)
			{
				FileLog.Debug("AccessTools.TypeByName: Could not find type named " + name);
			}
			return type;
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0000CFC0 File Offset: 0x0000B1C0
		public static Type[] GetTypesFromAssembly(Assembly assembly)
		{
			Type[] result;
			try
			{
				result = assembly.GetTypes();
			}
			catch (ReflectionTypeLoadException ex)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(47, 2);
				defaultInterpolatedStringHandler.AppendLiteral("AccessTools.GetTypesFromAssembly: assembly ");
				defaultInterpolatedStringHandler.AppendFormatted<Assembly>(assembly);
				defaultInterpolatedStringHandler.AppendLiteral(" => ");
				defaultInterpolatedStringHandler.AppendFormatted<ReflectionTypeLoadException>(ex);
				FileLog.Debug(defaultInterpolatedStringHandler.ToStringAndClear());
				result = (from type in ex.Types
				where type != null
				select type).ToArray<Type>();
			}
			return result;
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000D058 File Offset: 0x0000B258
		public static IEnumerable<Type> AllTypes()
		{
			IEnumerable<Assembly> source = AccessTools.AllAssemblies();
			Func<Assembly, IEnumerable<Type>> selector;
			if ((selector = AccessTools.<>O.<0>__GetTypesFromAssembly) == null)
			{
				selector = (AccessTools.<>O.<0>__GetTypesFromAssembly = new Func<Assembly, IEnumerable<Type>>(AccessTools.GetTypesFromAssembly));
			}
			return source.SelectMany(selector);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000D07F File Offset: 0x0000B27F
		public static IEnumerable<Type> InnerTypes(Type type)
		{
			return type.GetNestedTypes(AccessTools.all);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000D08C File Offset: 0x0000B28C
		public static T FindIncludingBaseTypes<T>(Type type, Func<Type, T> func) where T : class
		{
			T t;
			for (;;)
			{
				t = func(type);
				if (t != null)
				{
					break;
				}
				type = type.BaseType;
				if (type == null)
				{
					goto Block_1;
				}
			}
			return t;
			Block_1:
			return default(T);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000D0C0 File Offset: 0x0000B2C0
		public static T FindIncludingInnerTypes<T>(Type type, Func<Type, T> func) where T : class
		{
			T t = func(type);
			if (t != null)
			{
				return t;
			}
			foreach (Type type2 in type.GetNestedTypes(AccessTools.all))
			{
				t = AccessTools.FindIncludingInnerTypes<T>(type2, func);
				if (t != null)
				{
					break;
				}
			}
			return t;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000D10E File Offset: 0x0000B30E
		public static MethodInfo Identifiable(this MethodInfo method)
		{
			return (PlatformTriple.Current.GetIdentifiable(method) as MethodInfo) ?? method;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000D128 File Offset: 0x0000B328
		public static FieldInfo DeclaredField(Type type, string name)
		{
			if (type == null)
			{
				FileLog.Debug("AccessTools.DeclaredField: type is null");
				return null;
			}
			if (name == null)
			{
				FileLog.Debug("AccessTools.DeclaredField: name is null");
				return null;
			}
			FieldInfo field = type.GetField(name, AccessTools.allDeclared);
			if (field == null)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(67, 2);
				defaultInterpolatedStringHandler.AppendLiteral("AccessTools.DeclaredField: Could not find field for type ");
				defaultInterpolatedStringHandler.AppendFormatted<Type>(type);
				defaultInterpolatedStringHandler.AppendLiteral(" and name ");
				defaultInterpolatedStringHandler.AppendFormatted(name);
				FileLog.Debug(defaultInterpolatedStringHandler.ToStringAndClear());
			}
			return field;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000D1A4 File Offset: 0x0000B3A4
		public static FieldInfo DeclaredField(string typeColonName)
		{
			Tools.TypeAndName typeAndName = Tools.TypColonName(typeColonName);
			FieldInfo field = typeAndName.type.GetField(typeAndName.name, AccessTools.allDeclared);
			if (field == null)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(67, 2);
				defaultInterpolatedStringHandler.AppendLiteral("AccessTools.DeclaredField: Could not find field for type ");
				defaultInterpolatedStringHandler.AppendFormatted<Type>(typeAndName.type);
				defaultInterpolatedStringHandler.AppendLiteral(" and name ");
				defaultInterpolatedStringHandler.AppendFormatted(typeAndName.name);
				FileLog.Debug(defaultInterpolatedStringHandler.ToStringAndClear());
			}
			return field;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000D21C File Offset: 0x0000B41C
		public static FieldInfo Field(Type type, string name)
		{
			if (type == null)
			{
				FileLog.Debug("AccessTools.Field: type is null");
				return null;
			}
			if (name == null)
			{
				FileLog.Debug("AccessTools.Field: name is null");
				return null;
			}
			FieldInfo fieldInfo = AccessTools.FindIncludingBaseTypes<FieldInfo>(type, (Type t) => t.GetField(name, AccessTools.all));
			if (fieldInfo == null)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(59, 2);
				defaultInterpolatedStringHandler.AppendLiteral("AccessTools.Field: Could not find field for type ");
				defaultInterpolatedStringHandler.AppendFormatted<Type>(type);
				defaultInterpolatedStringHandler.AppendLiteral(" and name ");
				defaultInterpolatedStringHandler.AppendFormatted(name);
				FileLog.Debug(defaultInterpolatedStringHandler.ToStringAndClear());
			}
			return fieldInfo;
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000D2B4 File Offset: 0x0000B4B4
		public static FieldInfo Field(string typeColonName)
		{
			Tools.TypeAndName info = Tools.TypColonName(typeColonName);
			FieldInfo fieldInfo = AccessTools.FindIncludingBaseTypes<FieldInfo>(info.type, (Type t) => t.GetField(info.name, AccessTools.all));
			if (fieldInfo == null)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(59, 2);
				defaultInterpolatedStringHandler.AppendLiteral("AccessTools.Field: Could not find field for type ");
				defaultInterpolatedStringHandler.AppendFormatted<Type>(info.type);
				defaultInterpolatedStringHandler.AppendLiteral(" and name ");
				defaultInterpolatedStringHandler.AppendFormatted(info.name);
				FileLog.Debug(defaultInterpolatedStringHandler.ToStringAndClear());
			}
			return fieldInfo;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000D348 File Offset: 0x0000B548
		public static FieldInfo DeclaredField(Type type, int idx)
		{
			if (type == null)
			{
				FileLog.Debug("AccessTools.DeclaredField: type is null");
				return null;
			}
			FieldInfo fieldInfo = AccessTools.GetDeclaredFields(type).ElementAtOrDefault(idx);
			if (fieldInfo == null)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(66, 2);
				defaultInterpolatedStringHandler.AppendLiteral("AccessTools.DeclaredField: Could not find field for type ");
				defaultInterpolatedStringHandler.AppendFormatted<Type>(type);
				defaultInterpolatedStringHandler.AppendLiteral(" and idx ");
				defaultInterpolatedStringHandler.AppendFormatted<int>(idx);
				FileLog.Debug(defaultInterpolatedStringHandler.ToStringAndClear());
			}
			return fieldInfo;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000D3B4 File Offset: 0x0000B5B4
		public static PropertyInfo DeclaredProperty(Type type, string name)
		{
			if (type == null)
			{
				FileLog.Debug("AccessTools.DeclaredProperty: type is null");
				return null;
			}
			if (name == null)
			{
				FileLog.Debug("AccessTools.DeclaredProperty: name is null");
				return null;
			}
			PropertyInfo property = type.GetProperty(name, AccessTools.allDeclared);
			if (property == null)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(73, 2);
				defaultInterpolatedStringHandler.AppendLiteral("AccessTools.DeclaredProperty: Could not find property for type ");
				defaultInterpolatedStringHandler.AppendFormatted<Type>(type);
				defaultInterpolatedStringHandler.AppendLiteral(" and name ");
				defaultInterpolatedStringHandler.AppendFormatted(name);
				FileLog.Debug(defaultInterpolatedStringHandler.ToStringAndClear());
			}
			return property;
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000D430 File Offset: 0x0000B630
		public static PropertyInfo DeclaredProperty(string typeColonName)
		{
			Tools.TypeAndName typeAndName = Tools.TypColonName(typeColonName);
			PropertyInfo property = typeAndName.type.GetProperty(typeAndName.name, AccessTools.allDeclared);
			if (property == null)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(73, 2);
				defaultInterpolatedStringHandler.AppendLiteral("AccessTools.DeclaredProperty: Could not find property for type ");
				defaultInterpolatedStringHandler.AppendFormatted<Type>(typeAndName.type);
				defaultInterpolatedStringHandler.AppendLiteral(" and name ");
				defaultInterpolatedStringHandler.AppendFormatted(typeAndName.name);
				FileLog.Debug(defaultInterpolatedStringHandler.ToStringAndClear());
			}
			return property;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000D4A8 File Offset: 0x0000B6A8
		public static PropertyInfo DeclaredIndexer(Type type, Type[] parameters = null)
		{
			if (type == null)
			{
				FileLog.Debug("AccessTools.DeclaredIndexer: type is null");
				return null;
			}
			PropertyInfo result;
			try
			{
				PropertyInfo propertyInfo;
				if (parameters != null)
				{
					propertyInfo = type.GetProperties(AccessTools.allDeclared).FirstOrDefault((PropertyInfo property) => (from param in property.GetIndexParameters()
					select param.ParameterType).SequenceEqual(parameters));
				}
				else
				{
					propertyInfo = type.GetProperties(AccessTools.allDeclared).SingleOrDefault((PropertyInfo property) => property.GetIndexParameters().Length != 0);
				}
				PropertyInfo propertyInfo2 = propertyInfo;
				if (propertyInfo2 == null)
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(77, 2);
					defaultInterpolatedStringHandler.AppendLiteral("AccessTools.DeclaredIndexer: Could not find indexer for type ");
					defaultInterpolatedStringHandler.AppendFormatted<Type>(type);
					defaultInterpolatedStringHandler.AppendLiteral(" and parameters ");
					Type[] parameters2 = parameters;
					defaultInterpolatedStringHandler.AppendFormatted((parameters2 != null) ? parameters2.Description() : null);
					FileLog.Debug(defaultInterpolatedStringHandler.ToStringAndClear());
				}
				result = propertyInfo2;
			}
			catch (InvalidOperationException inner)
			{
				throw new AmbiguousMatchException("Multiple possible indexers were found.", inner);
			}
			return result;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0000D5A0 File Offset: 0x0000B7A0
		public static MethodInfo DeclaredPropertyGetter(Type type, string name)
		{
			PropertyInfo propertyInfo = AccessTools.DeclaredProperty(type, name);
			if (propertyInfo == null)
			{
				return null;
			}
			return propertyInfo.GetGetMethod(true);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000D5B5 File Offset: 0x0000B7B5
		public static MethodInfo DeclaredPropertyGetter(string typeColonName)
		{
			PropertyInfo propertyInfo = AccessTools.DeclaredProperty(typeColonName);
			if (propertyInfo == null)
			{
				return null;
			}
			return propertyInfo.GetGetMethod(true);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000D5C9 File Offset: 0x0000B7C9
		public static MethodInfo DeclaredIndexerGetter(Type type, Type[] parameters = null)
		{
			PropertyInfo propertyInfo = AccessTools.DeclaredIndexer(type, parameters);
			if (propertyInfo == null)
			{
				return null;
			}
			return propertyInfo.GetGetMethod(true);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000D5DE File Offset: 0x0000B7DE
		public static MethodInfo DeclaredPropertySetter(Type type, string name)
		{
			PropertyInfo propertyInfo = AccessTools.DeclaredProperty(type, name);
			if (propertyInfo == null)
			{
				return null;
			}
			return propertyInfo.GetSetMethod(true);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000D5F3 File Offset: 0x0000B7F3
		public static MethodInfo DeclaredPropertySetter(string typeColonName)
		{
			PropertyInfo propertyInfo = AccessTools.DeclaredProperty(typeColonName);
			if (propertyInfo == null)
			{
				return null;
			}
			return propertyInfo.GetSetMethod(true);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000D607 File Offset: 0x0000B807
		public static MethodInfo DeclaredIndexerSetter(Type type, Type[] parameters)
		{
			PropertyInfo propertyInfo = AccessTools.DeclaredIndexer(type, parameters);
			if (propertyInfo == null)
			{
				return null;
			}
			return propertyInfo.GetSetMethod(true);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000D61C File Offset: 0x0000B81C
		public static PropertyInfo Property(Type type, string name)
		{
			if (type == null)
			{
				FileLog.Debug("AccessTools.Property: type is null");
				return null;
			}
			if (name == null)
			{
				FileLog.Debug("AccessTools.Property: name is null");
				return null;
			}
			PropertyInfo propertyInfo = AccessTools.FindIncludingBaseTypes<PropertyInfo>(type, (Type t) => t.GetProperty(name, AccessTools.all));
			if (propertyInfo == null)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(65, 2);
				defaultInterpolatedStringHandler.AppendLiteral("AccessTools.Property: Could not find property for type ");
				defaultInterpolatedStringHandler.AppendFormatted<Type>(type);
				defaultInterpolatedStringHandler.AppendLiteral(" and name ");
				defaultInterpolatedStringHandler.AppendFormatted(name);
				FileLog.Debug(defaultInterpolatedStringHandler.ToStringAndClear());
			}
			return propertyInfo;
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000D6B4 File Offset: 0x0000B8B4
		public static PropertyInfo Property(string typeColonName)
		{
			Tools.TypeAndName info = Tools.TypColonName(typeColonName);
			PropertyInfo propertyInfo = AccessTools.FindIncludingBaseTypes<PropertyInfo>(info.type, (Type t) => t.GetProperty(info.name, AccessTools.all));
			if (propertyInfo == null)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(65, 2);
				defaultInterpolatedStringHandler.AppendLiteral("AccessTools.Property: Could not find property for type ");
				defaultInterpolatedStringHandler.AppendFormatted<Type>(info.type);
				defaultInterpolatedStringHandler.AppendLiteral(" and name ");
				defaultInterpolatedStringHandler.AppendFormatted(info.name);
				FileLog.Debug(defaultInterpolatedStringHandler.ToStringAndClear());
			}
			return propertyInfo;
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000D748 File Offset: 0x0000B948
		public static PropertyInfo Indexer(Type type, Type[] parameters = null)
		{
			if (type == null)
			{
				FileLog.Debug("AccessTools.Indexer: type is null");
				return null;
			}
			Func<Type, PropertyInfo> func;
			if (parameters != null)
			{
				Func<PropertyInfo, bool> <>9__3;
				func = delegate(Type t)
				{
					IEnumerable<PropertyInfo> properties = t.GetProperties(AccessTools.all);
					Func<PropertyInfo, bool> predicate;
					if ((predicate = <>9__3) == null)
					{
						predicate = (<>9__3 = ((PropertyInfo property) => (from param in property.GetIndexParameters()
						select param.ParameterType).SequenceEqual(parameters)));
					}
					return properties.FirstOrDefault(predicate);
				};
			}
			else
			{
				func = ((Type t) => t.GetProperties(AccessTools.all).SingleOrDefault((PropertyInfo property) => property.GetIndexParameters().Length != 0));
			}
			Func<Type, PropertyInfo> func2 = func;
			PropertyInfo result;
			try
			{
				PropertyInfo propertyInfo = AccessTools.FindIncludingBaseTypes<PropertyInfo>(type, func2);
				if (propertyInfo == null)
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(69, 2);
					defaultInterpolatedStringHandler.AppendLiteral("AccessTools.Indexer: Could not find indexer for type ");
					defaultInterpolatedStringHandler.AppendFormatted<Type>(type);
					defaultInterpolatedStringHandler.AppendLiteral(" and parameters ");
					Type[] parameters2 = parameters;
					defaultInterpolatedStringHandler.AppendFormatted((parameters2 != null) ? parameters2.Description() : null);
					FileLog.Debug(defaultInterpolatedStringHandler.ToStringAndClear());
				}
				result = propertyInfo;
			}
			catch (InvalidOperationException inner)
			{
				throw new AmbiguousMatchException("Multiple possible indexers were found.", inner);
			}
			return result;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000D828 File Offset: 0x0000BA28
		public static MethodInfo PropertyGetter(Type type, string name)
		{
			PropertyInfo propertyInfo = AccessTools.Property(type, name);
			if (propertyInfo == null)
			{
				return null;
			}
			return propertyInfo.GetGetMethod(true);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000D83D File Offset: 0x0000BA3D
		public static MethodInfo PropertyGetter(string typeColonName)
		{
			PropertyInfo propertyInfo = AccessTools.Property(typeColonName);
			if (propertyInfo == null)
			{
				return null;
			}
			return propertyInfo.GetGetMethod(true);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000D851 File Offset: 0x0000BA51
		public static MethodInfo IndexerGetter(Type type, Type[] parameters = null)
		{
			PropertyInfo propertyInfo = AccessTools.Indexer(type, parameters);
			if (propertyInfo == null)
			{
				return null;
			}
			return propertyInfo.GetGetMethod(true);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000D866 File Offset: 0x0000BA66
		public static MethodInfo PropertySetter(Type type, string name)
		{
			PropertyInfo propertyInfo = AccessTools.Property(type, name);
			if (propertyInfo == null)
			{
				return null;
			}
			return propertyInfo.GetSetMethod(true);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000D87B File Offset: 0x0000BA7B
		public static MethodInfo PropertySetter(string typeColonName)
		{
			PropertyInfo propertyInfo = AccessTools.Property(typeColonName);
			if (propertyInfo == null)
			{
				return null;
			}
			return propertyInfo.GetSetMethod(true);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000D88F File Offset: 0x0000BA8F
		public static MethodInfo IndexerSetter(Type type, Type[] parameters = null)
		{
			PropertyInfo propertyInfo = AccessTools.Indexer(type, parameters);
			if (propertyInfo == null)
			{
				return null;
			}
			return propertyInfo.GetSetMethod(true);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000D8A4 File Offset: 0x0000BAA4
		public static MethodInfo DeclaredMethod(Type type, string name, Type[] parameters = null, Type[] generics = null)
		{
			if (type == null)
			{
				FileLog.Debug("AccessTools.DeclaredMethod: type is null");
				return null;
			}
			if (name == null)
			{
				FileLog.Debug("AccessTools.DeclaredMethod: name is null");
				return null;
			}
			ParameterModifier[] modifiers = new ParameterModifier[0];
			MethodInfo methodInfo;
			if (parameters == null)
			{
				methodInfo = type.GetMethod(name, AccessTools.allDeclared);
			}
			else
			{
				methodInfo = type.GetMethod(name, AccessTools.allDeclared, null, parameters, modifiers);
			}
			if (methodInfo == null)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(85, 3);
				defaultInterpolatedStringHandler.AppendLiteral("AccessTools.DeclaredMethod: Could not find method for type ");
				defaultInterpolatedStringHandler.AppendFormatted<Type>(type);
				defaultInterpolatedStringHandler.AppendLiteral(" and name ");
				defaultInterpolatedStringHandler.AppendFormatted(name);
				defaultInterpolatedStringHandler.AppendLiteral(" and parameters ");
				defaultInterpolatedStringHandler.AppendFormatted((parameters != null) ? parameters.Description() : null);
				FileLog.Debug(defaultInterpolatedStringHandler.ToStringAndClear());
				return null;
			}
			if (generics != null)
			{
				methodInfo = methodInfo.MakeGenericMethod(generics);
			}
			return methodInfo;
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000D968 File Offset: 0x0000BB68
		public static MethodInfo DeclaredMethod(string typeColonName, Type[] parameters = null, Type[] generics = null)
		{
			Tools.TypeAndName typeAndName = Tools.TypColonName(typeColonName);
			return AccessTools.DeclaredMethod(typeAndName.type, typeAndName.name, parameters, generics);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000D990 File Offset: 0x0000BB90
		public static MethodInfo Method(Type type, string name, Type[] parameters = null, Type[] generics = null)
		{
			if (type == null)
			{
				FileLog.Debug("AccessTools.Method: type is null");
				return null;
			}
			if (name == null)
			{
				FileLog.Debug("AccessTools.Method: name is null");
				return null;
			}
			ParameterModifier[] modifiers = new ParameterModifier[0];
			MethodInfo methodInfo;
			if (parameters == null)
			{
				try
				{
					methodInfo = AccessTools.FindIncludingBaseTypes<MethodInfo>(type, (Type t) => t.GetMethod(name, AccessTools.all));
					goto IL_D1;
				}
				catch (AmbiguousMatchException inner)
				{
					methodInfo = AccessTools.FindIncludingBaseTypes<MethodInfo>(type, (Type t) => t.GetMethod(name, AccessTools.all, null, Array.Empty<Type>(), modifiers));
					if (methodInfo == null)
					{
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(38, 2);
						defaultInterpolatedStringHandler.AppendLiteral("Ambiguous match in Harmony patch for ");
						defaultInterpolatedStringHandler.AppendFormatted<Type>(type);
						defaultInterpolatedStringHandler.AppendLiteral(":");
						defaultInterpolatedStringHandler.AppendFormatted(name);
						throw new AmbiguousMatchException(defaultInterpolatedStringHandler.ToStringAndClear(), inner);
					}
					goto IL_D1;
				}
			}
			methodInfo = AccessTools.FindIncludingBaseTypes<MethodInfo>(type, (Type t) => t.GetMethod(name, AccessTools.all, null, parameters, modifiers));
			IL_D1:
			if (methodInfo == null)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(77, 3);
				defaultInterpolatedStringHandler.AppendLiteral("AccessTools.Method: Could not find method for type ");
				defaultInterpolatedStringHandler.AppendFormatted<Type>(type);
				defaultInterpolatedStringHandler.AppendLiteral(" and name ");
				defaultInterpolatedStringHandler.AppendFormatted(name);
				defaultInterpolatedStringHandler.AppendLiteral(" and parameters ");
				Type[] parameters2 = parameters;
				defaultInterpolatedStringHandler.AppendFormatted((parameters2 != null) ? parameters2.Description() : null);
				FileLog.Debug(defaultInterpolatedStringHandler.ToStringAndClear());
				return null;
			}
			if (generics != null)
			{
				methodInfo = methodInfo.MakeGenericMethod(generics);
			}
			return methodInfo;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000DAF8 File Offset: 0x0000BCF8
		public static MethodInfo Method(string typeColonName, Type[] parameters = null, Type[] generics = null)
		{
			Tools.TypeAndName typeAndName = Tools.TypColonName(typeColonName);
			return AccessTools.Method(typeAndName.type, typeAndName.name, parameters, generics);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000DB20 File Offset: 0x0000BD20
		public static MethodInfo EnumeratorMoveNext(MethodBase method)
		{
			if (method == null)
			{
				FileLog.Debug("AccessTools.EnumeratorMoveNext: method is null");
				return null;
			}
			IEnumerable<KeyValuePair<OpCode, object>> source = from pair in PatchProcessor.ReadMethodBody(method)
			where pair.Key == OpCodes.Newobj
			select pair;
			if (source.Count<KeyValuePair<OpCode, object>>() != 1)
			{
				FileLog.Debug("AccessTools.EnumeratorMoveNext: " + method.FullDescription() + " contains no Newobj opcode");
				return null;
			}
			ConstructorInfo constructorInfo = source.First<KeyValuePair<OpCode, object>>().Value as ConstructorInfo;
			if (constructorInfo == null)
			{
				FileLog.Debug("AccessTools.EnumeratorMoveNext: " + method.FullDescription() + " contains no constructor");
				return null;
			}
			Type declaringType = constructorInfo.DeclaringType;
			if (declaringType == null)
			{
				FileLog.Debug("AccessTools.EnumeratorMoveNext: " + method.FullDescription() + " refers to a global type");
				return null;
			}
			return AccessTools.Method(declaringType, "MoveNext", null, null);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000DC00 File Offset: 0x0000BE00
		public static MethodInfo AsyncMoveNext(MethodBase method)
		{
			if (method == null)
			{
				FileLog.Debug("AccessTools.AsyncMoveNext: method is null");
				return null;
			}
			AsyncStateMachineAttribute customAttribute = method.GetCustomAttribute<AsyncStateMachineAttribute>();
			if (customAttribute == null)
			{
				FileLog.Debug("AccessTools.AsyncMoveNext: Could not find AsyncStateMachine for " + method.FullDescription());
				return null;
			}
			Type stateMachineType = customAttribute.StateMachineType;
			MethodInfo methodInfo = AccessTools.DeclaredMethod(stateMachineType, "MoveNext", null, null);
			if (methodInfo == null)
			{
				FileLog.Debug("AccessTools.AsyncMoveNext: Could not find async method body for " + method.FullDescription());
				return null;
			}
			return methodInfo;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000DC70 File Offset: 0x0000BE70
		public static List<string> GetMethodNames(Type type)
		{
			if (type == null)
			{
				FileLog.Debug("AccessTools.GetMethodNames: type is null");
				return new List<string>();
			}
			return (from m in AccessTools.GetDeclaredMethods(type)
			select m.Name).ToList<string>();
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000DCBF File Offset: 0x0000BEBF
		public static List<string> GetMethodNames(object instance)
		{
			if (instance == null)
			{
				FileLog.Debug("AccessTools.GetMethodNames: instance is null");
				return new List<string>();
			}
			return AccessTools.GetMethodNames(instance.GetType());
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000DCE0 File Offset: 0x0000BEE0
		public static List<string> GetFieldNames(Type type)
		{
			if (type == null)
			{
				FileLog.Debug("AccessTools.GetFieldNames: type is null");
				return new List<string>();
			}
			return (from f in AccessTools.GetDeclaredFields(type)
			select f.Name).ToList<string>();
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000DD2F File Offset: 0x0000BF2F
		public static List<string> GetFieldNames(object instance)
		{
			if (instance == null)
			{
				FileLog.Debug("AccessTools.GetFieldNames: instance is null");
				return new List<string>();
			}
			return AccessTools.GetFieldNames(instance.GetType());
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000DD50 File Offset: 0x0000BF50
		public static List<string> GetPropertyNames(Type type)
		{
			if (type == null)
			{
				FileLog.Debug("AccessTools.GetPropertyNames: type is null");
				return new List<string>();
			}
			return (from f in AccessTools.GetDeclaredProperties(type)
			select f.Name).ToList<string>();
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000DD9F File Offset: 0x0000BF9F
		public static List<string> GetPropertyNames(object instance)
		{
			if (instance == null)
			{
				FileLog.Debug("AccessTools.GetPropertyNames: instance is null");
				return new List<string>();
			}
			return AccessTools.GetPropertyNames(instance.GetType());
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000DDC0 File Offset: 0x0000BFC0
		public static Type GetUnderlyingType(this MemberInfo member)
		{
			MemberTypes memberType = member.MemberType;
			if (memberType <= MemberTypes.Field)
			{
				if (memberType == MemberTypes.Event)
				{
					return ((EventInfo)member).EventHandlerType;
				}
				if (memberType == MemberTypes.Field)
				{
					return ((FieldInfo)member).FieldType;
				}
			}
			else
			{
				if (memberType == MemberTypes.Method)
				{
					return ((MethodInfo)member).ReturnType;
				}
				if (memberType == MemberTypes.Property)
				{
					return ((PropertyInfo)member).PropertyType;
				}
			}
			throw new ArgumentException("Member must be of type EventInfo, FieldInfo, MethodInfo, or PropertyInfo");
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000DE31 File Offset: 0x0000C031
		public static bool IsDeclaredMember<T>(this T member) where T : MemberInfo
		{
			return member.DeclaringType == member.ReflectedType;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000DE50 File Offset: 0x0000C050
		public static T GetDeclaredMember<T>(this T member) where T : MemberInfo
		{
			if (member.DeclaringType == null || member.IsDeclaredMember<T>())
			{
				return member;
			}
			int metadataToken = member.MetadataToken;
			Type declaringType = member.DeclaringType;
			MemberInfo[] array = ((declaringType != null) ? declaringType.GetMembers(AccessTools.all) : null) ?? Array.Empty<MemberInfo>();
			foreach (MemberInfo memberInfo in array)
			{
				if (memberInfo.MetadataToken == metadataToken)
				{
					return (T)((object)memberInfo);
				}
			}
			return member;
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000DED0 File Offset: 0x0000C0D0
		public static ConstructorInfo DeclaredConstructor(Type type, Type[] parameters = null, bool searchForStatic = false)
		{
			if (type == null)
			{
				FileLog.Debug("AccessTools.DeclaredConstructor: type is null");
				return null;
			}
			if (parameters == null)
			{
				parameters = Array.Empty<Type>();
			}
			BindingFlags bindingAttr = searchForStatic ? (AccessTools.allDeclared & ~BindingFlags.Instance) : (AccessTools.allDeclared & ~BindingFlags.Static);
			return type.GetConstructor(bindingAttr, null, parameters, Array.Empty<ParameterModifier>());
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000DF1C File Offset: 0x0000C11C
		public static ConstructorInfo Constructor(Type type, Type[] parameters = null, bool searchForStatic = false)
		{
			if (type == null)
			{
				FileLog.Debug("AccessTools.ConstructorInfo: type is null");
				return null;
			}
			if (parameters == null)
			{
				parameters = Array.Empty<Type>();
			}
			BindingFlags flags = searchForStatic ? (AccessTools.all & ~BindingFlags.Instance) : (AccessTools.all & ~BindingFlags.Static);
			return AccessTools.FindIncludingBaseTypes<ConstructorInfo>(type, (Type t) => t.GetConstructor(flags, null, parameters, Array.Empty<ParameterModifier>()));
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000DF88 File Offset: 0x0000C188
		public static List<ConstructorInfo> GetDeclaredConstructors(Type type, bool? searchForStatic = null)
		{
			if (type == null)
			{
				FileLog.Debug("AccessTools.GetDeclaredConstructors: type is null");
				return new List<ConstructorInfo>();
			}
			BindingFlags bindingFlags = AccessTools.allDeclared;
			if (searchForStatic != null)
			{
				bindingFlags = (searchForStatic.Value ? (bindingFlags & ~BindingFlags.Instance) : (bindingFlags & ~BindingFlags.Static));
			}
			return (from method in type.GetConstructors(bindingFlags)
			where method.DeclaringType == type
			select method).ToList<ConstructorInfo>();
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000E000 File Offset: 0x0000C200
		public static List<MethodInfo> GetDeclaredMethods(Type type)
		{
			if (type == null)
			{
				FileLog.Debug("AccessTools.GetDeclaredMethods: type is null");
				return new List<MethodInfo>();
			}
			MethodInfo[] methods = type.GetMethods(AccessTools.allDeclared);
			List<MethodInfo> list = new List<MethodInfo>(methods.Length);
			list.AddRange(methods);
			return list;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000E03C File Offset: 0x0000C23C
		public static List<PropertyInfo> GetDeclaredProperties(Type type)
		{
			if (type == null)
			{
				FileLog.Debug("AccessTools.GetDeclaredProperties: type is null");
				return new List<PropertyInfo>();
			}
			PropertyInfo[] properties = type.GetProperties(AccessTools.allDeclared);
			List<PropertyInfo> list = new List<PropertyInfo>(properties.Length);
			list.AddRange(properties);
			return list;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000E078 File Offset: 0x0000C278
		public static List<FieldInfo> GetDeclaredFields(Type type)
		{
			if (type == null)
			{
				FileLog.Debug("AccessTools.GetDeclaredFields: type is null");
				return new List<FieldInfo>();
			}
			FieldInfo[] fields = type.GetFields(AccessTools.allDeclared);
			List<FieldInfo> list = new List<FieldInfo>(fields.Length);
			list.AddRange(fields);
			return list;
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000E0B4 File Offset: 0x0000C2B4
		public static Type GetReturnedType(MethodBase methodOrConstructor)
		{
			if (methodOrConstructor == null)
			{
				FileLog.Debug("AccessTools.GetReturnedType: methodOrConstructor is null");
				return null;
			}
			ConstructorInfo constructorInfo = methodOrConstructor as ConstructorInfo;
			if (constructorInfo != null)
			{
				return typeof(void);
			}
			return ((MethodInfo)methodOrConstructor).ReturnType;
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000E0F0 File Offset: 0x0000C2F0
		public static Type Inner(Type type, string name)
		{
			if (type == null)
			{
				FileLog.Debug("AccessTools.Inner: type is null");
				return null;
			}
			if (name == null)
			{
				FileLog.Debug("AccessTools.Inner: name is null");
				return null;
			}
			return AccessTools.FindIncludingBaseTypes<Type>(type, (Type t) => t.GetNestedType(name, AccessTools.all));
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000E140 File Offset: 0x0000C340
		public static Type FirstInner(Type type, Func<Type, bool> predicate)
		{
			if (type == null)
			{
				FileLog.Debug("AccessTools.FirstInner: type is null");
				return null;
			}
			if (predicate == null)
			{
				FileLog.Debug("AccessTools.FirstInner: predicate is null");
				return null;
			}
			return type.GetNestedTypes(AccessTools.all).FirstOrDefault((Type subType) => predicate(subType));
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000E19C File Offset: 0x0000C39C
		public static MethodInfo FirstMethod(Type type, Func<MethodInfo, bool> predicate)
		{
			if (type == null)
			{
				FileLog.Debug("AccessTools.FirstMethod: type is null");
				return null;
			}
			if (predicate == null)
			{
				FileLog.Debug("AccessTools.FirstMethod: predicate is null");
				return null;
			}
			return type.GetMethods(AccessTools.allDeclared).FirstOrDefault((MethodInfo method) => predicate(method));
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000E1F8 File Offset: 0x0000C3F8
		public static ConstructorInfo FirstConstructor(Type type, Func<ConstructorInfo, bool> predicate)
		{
			if (type == null)
			{
				FileLog.Debug("AccessTools.FirstConstructor: type is null");
				return null;
			}
			if (predicate == null)
			{
				FileLog.Debug("AccessTools.FirstConstructor: predicate is null");
				return null;
			}
			return type.GetConstructors(AccessTools.allDeclared).FirstOrDefault((ConstructorInfo constructor) => predicate(constructor));
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000E254 File Offset: 0x0000C454
		public static PropertyInfo FirstProperty(Type type, Func<PropertyInfo, bool> predicate)
		{
			if (type == null)
			{
				FileLog.Debug("AccessTools.FirstProperty: type is null");
				return null;
			}
			if (predicate == null)
			{
				FileLog.Debug("AccessTools.FirstProperty: predicate is null");
				return null;
			}
			return type.GetProperties(AccessTools.allDeclared).FirstOrDefault((PropertyInfo property) => predicate(property));
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000E2AD File Offset: 0x0000C4AD
		public static Type[] GetTypes(object[] parameters)
		{
			if (parameters == null)
			{
				return Array.Empty<Type>();
			}
			return parameters.Select(delegate(object p)
			{
				if (p != null)
				{
					return p.GetType();
				}
				return typeof(object);
			}).ToArray<Type>();
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000E2E4 File Offset: 0x0000C4E4
		public static object[] ActualParameters(MethodBase method, object[] inputs)
		{
			List<Type> inputTypes = inputs.Select(delegate(object obj)
			{
				if (obj == null)
				{
					return null;
				}
				return obj.GetType();
			}).ToList<Type>();
			return (from p in method.GetParameters()
			select p.ParameterType).Select(delegate(Type pType)
			{
				int num = inputTypes.FindIndex((Type inType) => inType != null && pType.IsAssignableFrom(inType));
				if (num >= 0)
				{
					return inputs[num];
				}
				return AccessTools.GetDefaultValue(pType);
			}).ToArray<object>();
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000E374 File Offset: 0x0000C574
		public static AccessTools.FieldRef<T, F> FieldRefAccess<T, F>(string fieldName)
		{
			if (fieldName == null)
			{
				throw new ArgumentNullException("fieldName");
			}
			AccessTools.FieldRef<T, F> result;
			try
			{
				Type typeFromHandle = typeof(T);
				if (typeFromHandle.IsValueType)
				{
					throw new ArgumentException("T (FieldRefAccess instance type) must not be a value type");
				}
				result = Tools.FieldRefAccess<T, F>(Tools.GetInstanceField(typeFromHandle, fieldName), false);
			}
			catch (Exception innerException)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(43, 3);
				defaultInterpolatedStringHandler.AppendLiteral("FieldRefAccess<");
				defaultInterpolatedStringHandler.AppendFormatted<Type>(typeof(T));
				defaultInterpolatedStringHandler.AppendLiteral(", ");
				defaultInterpolatedStringHandler.AppendFormatted<Type>(typeof(F));
				defaultInterpolatedStringHandler.AppendLiteral("> for ");
				defaultInterpolatedStringHandler.AppendFormatted(fieldName);
				defaultInterpolatedStringHandler.AppendLiteral(" caused an exception");
				throw new ArgumentException(defaultInterpolatedStringHandler.ToStringAndClear(), innerException);
			}
			return result;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000E444 File Offset: 0x0000C644
		public static ref F FieldRefAccess<T, F>(T instance, string fieldName)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			if (fieldName == null)
			{
				throw new ArgumentNullException("fieldName");
			}
			F result;
			try
			{
				Type typeFromHandle = typeof(T);
				if (typeFromHandle.IsValueType)
				{
					throw new ArgumentException("T (FieldRefAccess instance type) must not be a value type");
				}
				result = Tools.FieldRefAccess<T, F>(Tools.GetInstanceField(typeFromHandle, fieldName), false)(instance);
			}
			catch (Exception innerException)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(45, 4);
				defaultInterpolatedStringHandler.AppendLiteral("FieldRefAccess<");
				defaultInterpolatedStringHandler.AppendFormatted<Type>(typeof(T));
				defaultInterpolatedStringHandler.AppendLiteral(", ");
				defaultInterpolatedStringHandler.AppendFormatted<Type>(typeof(F));
				defaultInterpolatedStringHandler.AppendLiteral("> for ");
				defaultInterpolatedStringHandler.AppendFormatted<T>(instance);
				defaultInterpolatedStringHandler.AppendLiteral(", ");
				defaultInterpolatedStringHandler.AppendFormatted(fieldName);
				defaultInterpolatedStringHandler.AppendLiteral(" caused an exception");
				throw new ArgumentException(defaultInterpolatedStringHandler.ToStringAndClear(), innerException);
			}
			return ref result;
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000E544 File Offset: 0x0000C744
		public static AccessTools.FieldRef<object, F> FieldRefAccess<F>(Type type, string fieldName)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (fieldName == null)
			{
				throw new ArgumentNullException("fieldName");
			}
			AccessTools.FieldRef<object, F> result;
			try
			{
				FieldInfo fieldInfo = AccessTools.Field(type, fieldName);
				if (fieldInfo == null)
				{
					throw new MissingFieldException(type.Name, fieldName);
				}
				if (!fieldInfo.IsStatic)
				{
					Type declaringType = fieldInfo.DeclaringType;
					if (declaringType != null && declaringType.IsValueType)
					{
						throw new ArgumentException("Either FieldDeclaringType must be a class or field must be static");
					}
				}
				result = Tools.FieldRefAccess<object, F>(fieldInfo, true);
			}
			catch (Exception innerException)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(43, 3);
				defaultInterpolatedStringHandler.AppendLiteral("FieldRefAccess<");
				defaultInterpolatedStringHandler.AppendFormatted<Type>(typeof(F));
				defaultInterpolatedStringHandler.AppendLiteral("> for ");
				defaultInterpolatedStringHandler.AppendFormatted<Type>(type);
				defaultInterpolatedStringHandler.AppendLiteral(", ");
				defaultInterpolatedStringHandler.AppendFormatted(fieldName);
				defaultInterpolatedStringHandler.AppendLiteral(" caused an exception");
				throw new ArgumentException(defaultInterpolatedStringHandler.ToStringAndClear(), innerException);
			}
			return result;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000E630 File Offset: 0x0000C830
		public static AccessTools.FieldRef<object, F> FieldRefAccess<F>(string typeColonName)
		{
			Tools.TypeAndName typeAndName = Tools.TypColonName(typeColonName);
			return AccessTools.FieldRefAccess<F>(typeAndName.type, typeAndName.name);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000E658 File Offset: 0x0000C858
		public static AccessTools.FieldRef<T, F> FieldRefAccess<T, F>(FieldInfo fieldInfo)
		{
			if (fieldInfo == null)
			{
				throw new ArgumentNullException("fieldInfo");
			}
			AccessTools.FieldRef<T, F> result;
			try
			{
				Type typeFromHandle = typeof(T);
				if (typeFromHandle.IsValueType)
				{
					throw new ArgumentException("T (FieldRefAccess instance type) must not be a value type");
				}
				bool needCastclass = false;
				if (!fieldInfo.IsStatic)
				{
					Type declaringType = fieldInfo.DeclaringType;
					if (declaringType != null)
					{
						if (declaringType.IsValueType)
						{
							throw new ArgumentException("Either FieldDeclaringType must be a class or field must be static");
						}
						needCastclass = Tools.FieldRefNeedsClasscast(typeFromHandle, declaringType);
					}
				}
				result = Tools.FieldRefAccess<T, F>(fieldInfo, needCastclass);
			}
			catch (Exception innerException)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(43, 3);
				defaultInterpolatedStringHandler.AppendLiteral("FieldRefAccess<");
				defaultInterpolatedStringHandler.AppendFormatted<Type>(typeof(T));
				defaultInterpolatedStringHandler.AppendLiteral(", ");
				defaultInterpolatedStringHandler.AppendFormatted<Type>(typeof(F));
				defaultInterpolatedStringHandler.AppendLiteral("> for ");
				defaultInterpolatedStringHandler.AppendFormatted<FieldInfo>(fieldInfo);
				defaultInterpolatedStringHandler.AppendLiteral(" caused an exception");
				throw new ArgumentException(defaultInterpolatedStringHandler.ToStringAndClear(), innerException);
			}
			return result;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000E754 File Offset: 0x0000C954
		public static ref F FieldRefAccess<T, F>(T instance, FieldInfo fieldInfo)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			if (fieldInfo == null)
			{
				throw new ArgumentNullException("fieldInfo");
			}
			F result;
			try
			{
				Type typeFromHandle = typeof(T);
				if (typeFromHandle.IsValueType)
				{
					throw new ArgumentException("T (FieldRefAccess instance type) must not be a value type");
				}
				if (fieldInfo.IsStatic)
				{
					throw new ArgumentException("Field must not be static");
				}
				bool needCastclass = false;
				Type declaringType = fieldInfo.DeclaringType;
				if (declaringType != null)
				{
					if (declaringType.IsValueType)
					{
						throw new ArgumentException("FieldDeclaringType must be a class");
					}
					needCastclass = Tools.FieldRefNeedsClasscast(typeFromHandle, declaringType);
				}
				result = Tools.FieldRefAccess<T, F>(fieldInfo, needCastclass)(instance);
			}
			catch (Exception innerException)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(45, 4);
				defaultInterpolatedStringHandler.AppendLiteral("FieldRefAccess<");
				defaultInterpolatedStringHandler.AppendFormatted<Type>(typeof(T));
				defaultInterpolatedStringHandler.AppendLiteral(", ");
				defaultInterpolatedStringHandler.AppendFormatted<Type>(typeof(F));
				defaultInterpolatedStringHandler.AppendLiteral("> for ");
				defaultInterpolatedStringHandler.AppendFormatted<T>(instance);
				defaultInterpolatedStringHandler.AppendLiteral(", ");
				defaultInterpolatedStringHandler.AppendFormatted<FieldInfo>(fieldInfo);
				defaultInterpolatedStringHandler.AppendLiteral(" caused an exception");
				throw new ArgumentException(defaultInterpolatedStringHandler.ToStringAndClear(), innerException);
			}
			return ref result;
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000E888 File Offset: 0x0000CA88
		public static AccessTools.StructFieldRef<T, F> StructFieldRefAccess<T, F>(string fieldName) where T : struct
		{
			if (fieldName == null)
			{
				throw new ArgumentNullException("fieldName");
			}
			AccessTools.StructFieldRef<T, F> result;
			try
			{
				result = Tools.StructFieldRefAccess<T, F>(Tools.GetInstanceField(typeof(T), fieldName));
			}
			catch (Exception innerException)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(49, 3);
				defaultInterpolatedStringHandler.AppendLiteral("StructFieldRefAccess<");
				defaultInterpolatedStringHandler.AppendFormatted<Type>(typeof(T));
				defaultInterpolatedStringHandler.AppendLiteral(", ");
				defaultInterpolatedStringHandler.AppendFormatted<Type>(typeof(F));
				defaultInterpolatedStringHandler.AppendLiteral("> for ");
				defaultInterpolatedStringHandler.AppendFormatted(fieldName);
				defaultInterpolatedStringHandler.AppendLiteral(" caused an exception");
				throw new ArgumentException(defaultInterpolatedStringHandler.ToStringAndClear(), innerException);
			}
			return result;
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000E940 File Offset: 0x0000CB40
		public static ref F StructFieldRefAccess<T, F>(ref T instance, string fieldName) where T : struct
		{
			if (fieldName == null)
			{
				throw new ArgumentNullException("fieldName");
			}
			F result;
			try
			{
				result = Tools.StructFieldRefAccess<T, F>(Tools.GetInstanceField(typeof(T), fieldName))(ref instance);
			}
			catch (Exception innerException)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(51, 4);
				defaultInterpolatedStringHandler.AppendLiteral("StructFieldRefAccess<");
				defaultInterpolatedStringHandler.AppendFormatted<Type>(typeof(T));
				defaultInterpolatedStringHandler.AppendLiteral(", ");
				defaultInterpolatedStringHandler.AppendFormatted<Type>(typeof(F));
				defaultInterpolatedStringHandler.AppendLiteral("> for ");
				defaultInterpolatedStringHandler.AppendFormatted<T>(instance);
				defaultInterpolatedStringHandler.AppendLiteral(", ");
				defaultInterpolatedStringHandler.AppendFormatted(fieldName);
				defaultInterpolatedStringHandler.AppendLiteral(" caused an exception");
				throw new ArgumentException(defaultInterpolatedStringHandler.ToStringAndClear(), innerException);
			}
			return ref result;
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000EA1C File Offset: 0x0000CC1C
		public static AccessTools.StructFieldRef<T, F> StructFieldRefAccess<T, F>(FieldInfo fieldInfo) where T : struct
		{
			if (fieldInfo == null)
			{
				throw new ArgumentNullException("fieldInfo");
			}
			AccessTools.StructFieldRef<T, F> result;
			try
			{
				Tools.ValidateStructField<T, F>(fieldInfo);
				result = Tools.StructFieldRefAccess<T, F>(fieldInfo);
			}
			catch (Exception innerException)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(49, 3);
				defaultInterpolatedStringHandler.AppendLiteral("StructFieldRefAccess<");
				defaultInterpolatedStringHandler.AppendFormatted<Type>(typeof(T));
				defaultInterpolatedStringHandler.AppendLiteral(", ");
				defaultInterpolatedStringHandler.AppendFormatted<Type>(typeof(F));
				defaultInterpolatedStringHandler.AppendLiteral("> for ");
				defaultInterpolatedStringHandler.AppendFormatted<FieldInfo>(fieldInfo);
				defaultInterpolatedStringHandler.AppendLiteral(" caused an exception");
				throw new ArgumentException(defaultInterpolatedStringHandler.ToStringAndClear(), innerException);
			}
			return result;
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000EACC File Offset: 0x0000CCCC
		public static ref F StructFieldRefAccess<T, F>(ref T instance, FieldInfo fieldInfo) where T : struct
		{
			if (fieldInfo == null)
			{
				throw new ArgumentNullException("fieldInfo");
			}
			F result;
			try
			{
				Tools.ValidateStructField<T, F>(fieldInfo);
				result = Tools.StructFieldRefAccess<T, F>(fieldInfo)(ref instance);
			}
			catch (Exception innerException)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(51, 4);
				defaultInterpolatedStringHandler.AppendLiteral("StructFieldRefAccess<");
				defaultInterpolatedStringHandler.AppendFormatted<Type>(typeof(T));
				defaultInterpolatedStringHandler.AppendLiteral(", ");
				defaultInterpolatedStringHandler.AppendFormatted<Type>(typeof(F));
				defaultInterpolatedStringHandler.AppendLiteral("> for ");
				defaultInterpolatedStringHandler.AppendFormatted<T>(instance);
				defaultInterpolatedStringHandler.AppendLiteral(", ");
				defaultInterpolatedStringHandler.AppendFormatted<FieldInfo>(fieldInfo);
				defaultInterpolatedStringHandler.AppendLiteral(" caused an exception");
				throw new ArgumentException(defaultInterpolatedStringHandler.ToStringAndClear(), innerException);
			}
			return ref result;
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000EBA0 File Offset: 0x0000CDA0
		public static ref F StaticFieldRefAccess<T, F>(string fieldName)
		{
			return AccessTools.StaticFieldRefAccess<F>(typeof(T), fieldName);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000EBB4 File Offset: 0x0000CDB4
		public static ref F StaticFieldRefAccess<F>(Type type, string fieldName)
		{
			F result;
			try
			{
				FieldInfo fieldInfo = AccessTools.Field(type, fieldName);
				if (fieldInfo == null)
				{
					throw new MissingFieldException(type.Name, fieldName);
				}
				result = Tools.StaticFieldRefAccess<F>(fieldInfo)();
			}
			catch (Exception innerException)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(49, 3);
				defaultInterpolatedStringHandler.AppendLiteral("StaticFieldRefAccess<");
				defaultInterpolatedStringHandler.AppendFormatted<Type>(typeof(F));
				defaultInterpolatedStringHandler.AppendLiteral("> for ");
				defaultInterpolatedStringHandler.AppendFormatted<Type>(type);
				defaultInterpolatedStringHandler.AppendLiteral(", ");
				defaultInterpolatedStringHandler.AppendFormatted(fieldName);
				defaultInterpolatedStringHandler.AppendLiteral(" caused an exception");
				throw new ArgumentException(defaultInterpolatedStringHandler.ToStringAndClear(), innerException);
			}
			return ref result;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000EC64 File Offset: 0x0000CE64
		public static ref F StaticFieldRefAccess<F>(string typeColonName)
		{
			Tools.TypeAndName typeAndName = Tools.TypColonName(typeColonName);
			return AccessTools.StaticFieldRefAccess<F>(typeAndName.type, typeAndName.name);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000EC8C File Offset: 0x0000CE8C
		public static ref F StaticFieldRefAccess<T, F>(FieldInfo fieldInfo)
		{
			if (fieldInfo == null)
			{
				throw new ArgumentNullException("fieldInfo");
			}
			F result;
			try
			{
				result = Tools.StaticFieldRefAccess<F>(fieldInfo)();
			}
			catch (Exception innerException)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(49, 3);
				defaultInterpolatedStringHandler.AppendLiteral("StaticFieldRefAccess<");
				defaultInterpolatedStringHandler.AppendFormatted<Type>(typeof(T));
				defaultInterpolatedStringHandler.AppendLiteral(", ");
				defaultInterpolatedStringHandler.AppendFormatted<Type>(typeof(F));
				defaultInterpolatedStringHandler.AppendLiteral("> for ");
				defaultInterpolatedStringHandler.AppendFormatted<FieldInfo>(fieldInfo);
				defaultInterpolatedStringHandler.AppendLiteral(" caused an exception");
				throw new ArgumentException(defaultInterpolatedStringHandler.ToStringAndClear(), innerException);
			}
			return ref result;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000ED3C File Offset: 0x0000CF3C
		public static AccessTools.FieldRef<F> StaticFieldRefAccess<F>(FieldInfo fieldInfo)
		{
			if (fieldInfo == null)
			{
				throw new ArgumentNullException("fieldInfo");
			}
			AccessTools.FieldRef<F> result;
			try
			{
				result = Tools.StaticFieldRefAccess<F>(fieldInfo);
			}
			catch (Exception innerException)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(47, 2);
				defaultInterpolatedStringHandler.AppendLiteral("StaticFieldRefAccess<");
				defaultInterpolatedStringHandler.AppendFormatted<Type>(typeof(F));
				defaultInterpolatedStringHandler.AppendLiteral("> for ");
				defaultInterpolatedStringHandler.AppendFormatted<FieldInfo>(fieldInfo);
				defaultInterpolatedStringHandler.AppendLiteral(" caused an exception");
				throw new ArgumentException(defaultInterpolatedStringHandler.ToStringAndClear(), innerException);
			}
			return result;
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000EDC8 File Offset: 0x0000CFC8
		public static DelegateType MethodDelegate<DelegateType>(MethodInfo method, object instance = null, bool virtualCall = true) where DelegateType : Delegate
		{
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			Type typeFromHandle = typeof(DelegateType);
			if (method.IsStatic)
			{
				return (DelegateType)((object)Delegate.CreateDelegate(typeFromHandle, method));
			}
			Type type = method.DeclaringType;
			if (type != null && type.IsInterface && !virtualCall)
			{
				throw new ArgumentException("Interface methods must be called virtually");
			}
			if (instance == null)
			{
				ParameterInfo[] parameters = typeFromHandle.GetMethod("Invoke").GetParameters();
				if (parameters.Length == 0)
				{
					Delegate.CreateDelegate(typeof(DelegateType), method);
					throw new ArgumentException("Invalid delegate type");
				}
				Type parameterType = parameters[0].ParameterType;
				if (type != null && type.IsInterface && parameterType.IsValueType)
				{
					InterfaceMapping interfaceMap = parameterType.GetInterfaceMap(type);
					method = interfaceMap.TargetMethods[Array.IndexOf<MethodInfo>(interfaceMap.InterfaceMethods, method)];
					type = parameterType;
				}
				if (type != null && virtualCall)
				{
					if (type.IsInterface)
					{
						return (DelegateType)((object)Delegate.CreateDelegate(typeFromHandle, method));
					}
					if (parameterType.IsInterface)
					{
						InterfaceMapping interfaceMap2 = type.GetInterfaceMap(parameterType);
						MethodInfo method2 = interfaceMap2.InterfaceMethods[Array.IndexOf<MethodInfo>(interfaceMap2.TargetMethods, method)];
						return (DelegateType)((object)Delegate.CreateDelegate(typeFromHandle, method2));
					}
					if (!type.IsValueType)
					{
						return (DelegateType)((object)Delegate.CreateDelegate(typeFromHandle, method.GetBaseDefinition()));
					}
				}
				ParameterInfo[] parameters2 = method.GetParameters();
				int num = parameters2.Length;
				Type[] array = new Type[num + 1];
				array[0] = type;
				for (int i = 0; i < num; i++)
				{
					array[i + 1] = parameters2[i].ParameterType;
				}
				DynamicMethodDefinition dynamicMethodDefinition = new DynamicMethodDefinition("OpenInstanceDelegate_" + method.Name, method.ReturnType, array);
				ILGenerator ilgenerator = dynamicMethodDefinition.GetILGenerator();
				if (type != null && type.IsValueType)
				{
					ilgenerator.Emit(OpCodes.Ldarga_S, 0);
				}
				else
				{
					ilgenerator.Emit(OpCodes.Ldarg_0);
				}
				for (int j = 1; j < array.Length; j++)
				{
					ilgenerator.Emit(OpCodes.Ldarg, j);
				}
				ilgenerator.Emit(OpCodes.Call, method);
				ilgenerator.Emit(OpCodes.Ret);
				return (DelegateType)((object)dynamicMethodDefinition.Generate().CreateDelegate(typeFromHandle));
			}
			else
			{
				if (virtualCall)
				{
					return (DelegateType)((object)Delegate.CreateDelegate(typeFromHandle, instance, method.GetBaseDefinition()));
				}
				if (type != null && !type.IsInstanceOfType(instance))
				{
					Delegate.CreateDelegate(typeof(DelegateType), instance, method);
					throw new ArgumentException("Invalid delegate type");
				}
				if (AccessTools.IsMonoRuntime)
				{
					DynamicMethodDefinition dynamicMethodDefinition2 = new DynamicMethodDefinition("LdftnDelegate_" + method.Name, typeFromHandle, new Type[]
					{
						typeof(object)
					});
					ILGenerator ilgenerator2 = dynamicMethodDefinition2.GetILGenerator();
					ilgenerator2.Emit(OpCodes.Ldarg_0);
					ilgenerator2.Emit(OpCodes.Ldftn, method);
					ilgenerator2.Emit(OpCodes.Newobj, typeFromHandle.GetConstructor(new Type[]
					{
						typeof(object),
						typeof(IntPtr)
					}));
					ilgenerator2.Emit(OpCodes.Ret);
					return (DelegateType)((object)dynamicMethodDefinition2.Generate().Invoke(null, new object[]
					{
						instance
					}));
				}
				return (DelegateType)((object)Activator.CreateInstance(typeFromHandle, new object[]
				{
					instance,
					method.MethodHandle.GetFunctionPointer()
				}));
			}
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000F118 File Offset: 0x0000D318
		public static DelegateType MethodDelegate<DelegateType>(string typeColonName, object instance = null, bool virtualCall = true) where DelegateType : Delegate
		{
			MethodInfo method = AccessTools.DeclaredMethod(typeColonName, null, null);
			return AccessTools.MethodDelegate<DelegateType>(method, instance, virtualCall);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000F138 File Offset: 0x0000D338
		public static DelegateType HarmonyDelegate<DelegateType>(object instance = null) where DelegateType : Delegate
		{
			HarmonyMethod mergedFromType = HarmonyMethodExtensions.GetMergedFromType(typeof(DelegateType));
			HarmonyMethod harmonyMethod = mergedFromType;
			MethodType value = harmonyMethod.methodType.GetValueOrDefault();
			if (harmonyMethod.methodType == null)
			{
				value = MethodType.Normal;
				harmonyMethod.methodType = new MethodType?(value);
			}
			MethodInfo methodInfo = mergedFromType.GetOriginalMethod() as MethodInfo;
			if (methodInfo == null)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(40, 1);
				defaultInterpolatedStringHandler.AppendLiteral("Delegate ");
				defaultInterpolatedStringHandler.AppendFormatted<Type>(typeof(DelegateType));
				defaultInterpolatedStringHandler.AppendLiteral(" has no defined original method");
				throw new NullReferenceException(defaultInterpolatedStringHandler.ToStringAndClear());
			}
			return AccessTools.MethodDelegate<DelegateType>(methodInfo, instance, !mergedFromType.nonVirtualDelegate);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000F1E0 File Offset: 0x0000D3E0
		public static MethodBase GetOutsideCaller()
		{
			StackTrace stackTrace = new StackTrace(true);
			foreach (StackFrame stackFrame in stackTrace.GetFrames())
			{
				MethodBase method = stackFrame.GetMethod();
				Type declaringType = method.DeclaringType;
				if (((declaringType != null) ? declaringType.Namespace : null) != typeof(Harmony).Namespace)
				{
					return method;
				}
			}
			throw new Exception("Unexpected end of stack trace");
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000F24B File Offset: 0x0000D44B
		public static void RethrowException(Exception exception)
		{
			ExceptionDispatchInfo.Capture(exception).Throw();
			throw exception;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000201 RID: 513 RVA: 0x0000F259 File Offset: 0x0000D459
		public static bool IsMonoRuntime { get; } = Type.GetType("Mono.Runtime") != null;

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000202 RID: 514 RVA: 0x0000F260 File Offset: 0x0000D460
		public static bool IsNetFrameworkRuntime { get; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000203 RID: 515 RVA: 0x0000F267 File Offset: 0x0000D467
		public static bool IsNetCoreRuntime { get; }

		// Token: 0x06000204 RID: 516 RVA: 0x0000F270 File Offset: 0x0000D470
		public static void ThrowMissingMemberException(Type type, params string[] names)
		{
			string value = string.Join(",", AccessTools.GetFieldNames(type).ToArray());
			string value2 = string.Join(",", AccessTools.GetPropertyNames(type).ToArray());
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(44, 3);
			defaultInterpolatedStringHandler.AppendFormatted(string.Join(",", names));
			defaultInterpolatedStringHandler.AppendLiteral("; available fields: ");
			defaultInterpolatedStringHandler.AppendFormatted(value);
			defaultInterpolatedStringHandler.AppendLiteral("; available properties: ");
			defaultInterpolatedStringHandler.AppendFormatted(value2);
			throw new MissingMemberException(defaultInterpolatedStringHandler.ToStringAndClear());
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000F2F9 File Offset: 0x0000D4F9
		public static object GetDefaultValue(Type type)
		{
			if (type == null)
			{
				FileLog.Debug("AccessTools.GetDefaultValue: type is null");
				return null;
			}
			if (type == typeof(void))
			{
				return null;
			}
			if (type.IsValueType)
			{
				return Activator.CreateInstance(type);
			}
			return null;
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000F330 File Offset: 0x0000D530
		public static object CreateInstance(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			ConstructorInfo constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, CallingConventions.Any, Array.Empty<Type>(), null);
			if (constructor != null)
			{
				return constructor.Invoke(null);
			}
			return FormatterServices.GetUninitializedObject(type);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000F370 File Offset: 0x0000D570
		public static T CreateInstance<T>()
		{
			object obj = AccessTools.CreateInstance(typeof(T));
			if (obj is T)
			{
				return (T)((object)obj);
			}
			return default(T);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000F3A7 File Offset: 0x0000D5A7
		public static T MakeDeepCopy<T>(object source) where T : class
		{
			return AccessTools.MakeDeepCopy(source, typeof(T), null, "") as T;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000F3C9 File Offset: 0x0000D5C9
		public static void MakeDeepCopy<T>(object source, out T result, Func<string, Traverse, Traverse, object> processor = null, string pathRoot = "")
		{
			result = (T)((object)AccessTools.MakeDeepCopy(source, typeof(T), processor, pathRoot));
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000F3E8 File Offset: 0x0000D5E8
		public static object MakeDeepCopy(object source, Type resultType, Func<string, Traverse, Traverse, object> processor = null, string pathRoot = "")
		{
			if (source == null || resultType == null)
			{
				return null;
			}
			resultType = (Nullable.GetUnderlyingType(resultType) ?? resultType);
			Type type = source.GetType();
			if (type.IsPrimitive)
			{
				return source;
			}
			if (type.IsEnum)
			{
				return Enum.ToObject(resultType, (int)source);
			}
			if (type.IsGenericType && resultType.IsGenericType)
			{
				AccessTools.addHandlerCacheLock.EnterUpgradeableReadLock();
				try
				{
					FastInvokeHandler handler;
					if (!AccessTools.addHandlerCache.TryGetValue(resultType, out handler))
					{
						MethodInfo methodInfo = AccessTools.FirstMethod(resultType, (MethodInfo m) => m.Name == "Add" && m.GetParameters().Length == 1);
						if (methodInfo != null)
						{
							handler = MethodInvoker.GetHandler(methodInfo, false);
						}
						AccessTools.addHandlerCacheLock.EnterWriteLock();
						try
						{
							AccessTools.addHandlerCache[resultType] = handler;
						}
						finally
						{
							AccessTools.addHandlerCacheLock.ExitWriteLock();
						}
					}
					if (handler != null)
					{
						object obj = Activator.CreateInstance(resultType);
						Type resultType2 = resultType.GetGenericArguments()[0];
						int num = 0;
						foreach (object source2 in (source as IEnumerable))
						{
							string text = num++.ToString();
							string pathRoot2 = (pathRoot.Length > 0) ? (pathRoot + "." + text) : text;
							object obj2 = AccessTools.MakeDeepCopy(source2, resultType2, processor, pathRoot2);
							handler(obj, new object[]
							{
								obj2
							});
						}
						return obj;
					}
				}
				finally
				{
					AccessTools.addHandlerCacheLock.ExitUpgradeableReadLock();
				}
			}
			if (type.IsArray && resultType.IsArray)
			{
				Type elementType = resultType.GetElementType();
				int length = ((Array)source).Length;
				object[] array = Activator.CreateInstance(resultType, new object[]
				{
					length
				}) as object[];
				object[] array2 = source as object[];
				for (int i = 0; i < length; i++)
				{
					string text2 = i.ToString();
					string pathRoot3 = (pathRoot.Length > 0) ? (pathRoot + "." + text2) : text2;
					array[i] = AccessTools.MakeDeepCopy(array2[i], elementType, processor, pathRoot3);
				}
				return array;
			}
			string @namespace = type.Namespace;
			if (@namespace == "System" || (@namespace != null && @namespace.StartsWith("System.")))
			{
				return source;
			}
			object obj3 = AccessTools.CreateInstance((resultType == typeof(object)) ? type : resultType);
			Traverse.IterateFields(source, obj3, delegate(string name, Traverse src, Traverse dst)
			{
				string text3 = (pathRoot.Length > 0) ? (pathRoot + "." + name) : name;
				object source3 = (processor != null) ? processor(text3, src, dst) : src.GetValue();
				dst.SetValue(AccessTools.MakeDeepCopy(source3, dst.GetValueType(), processor, text3));
			});
			return obj3;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000F6E8 File Offset: 0x0000D8E8
		public static bool IsStruct(Type type)
		{
			return !(type == null) && (type.IsValueType && !AccessTools.IsValue(type)) && !AccessTools.IsVoid(type);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000F710 File Offset: 0x0000D910
		public static bool IsClass(Type type)
		{
			return !(type == null) && !type.IsValueType;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000F726 File Offset: 0x0000D926
		public static bool IsValue(Type type)
		{
			return !(type == null) && (type.IsPrimitive || type.IsEnum);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000F744 File Offset: 0x0000D944
		public static bool IsInteger(Type type)
		{
			if (type == null)
			{
				return false;
			}
			TypeCode typeCode = Type.GetTypeCode(type);
			return typeCode - TypeCode.SByte <= 7;
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000F770 File Offset: 0x0000D970
		public static bool IsFloatingPoint(Type type)
		{
			if (type == null)
			{
				return false;
			}
			TypeCode typeCode = Type.GetTypeCode(type);
			return typeCode - TypeCode.Single <= 2;
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000F79D File Offset: 0x0000D99D
		public static bool IsNumber(Type type)
		{
			return AccessTools.IsInteger(type) || AccessTools.IsFloatingPoint(type);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000F7AF File Offset: 0x0000D9AF
		public static bool IsVoid(Type type)
		{
			return type == typeof(void);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000F7C1 File Offset: 0x0000D9C1
		public static bool IsOfNullableType<T>(T instance)
		{
			return Nullable.GetUnderlyingType(typeof(T)) != null;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000F7D8 File Offset: 0x0000D9D8
		public static bool IsStatic(MemberInfo member)
		{
			if (member == null)
			{
				throw new ArgumentNullException("member");
			}
			MemberTypes memberType = member.MemberType;
			if (memberType <= MemberTypes.Method)
			{
				switch (memberType)
				{
				case MemberTypes.Constructor:
					break;
				case MemberTypes.Event:
					return AccessTools.IsStatic((EventInfo)member);
				case MemberTypes.Constructor | MemberTypes.Event:
					goto IL_91;
				case MemberTypes.Field:
					return ((FieldInfo)member).IsStatic;
				default:
					if (memberType != MemberTypes.Method)
					{
						goto IL_91;
					}
					break;
				}
				return ((MethodBase)member).IsStatic;
			}
			if (memberType == MemberTypes.Property)
			{
				return AccessTools.IsStatic((PropertyInfo)member);
			}
			if (memberType == MemberTypes.TypeInfo || memberType == MemberTypes.NestedType)
			{
				return AccessTools.IsStatic((Type)member);
			}
			IL_91:
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(21, 1);
			defaultInterpolatedStringHandler.AppendLiteral("Unknown member type: ");
			defaultInterpolatedStringHandler.AppendFormatted<MemberTypes>(member.MemberType);
			throw new ArgumentException(defaultInterpolatedStringHandler.ToStringAndClear());
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000F8A7 File Offset: 0x0000DAA7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static bool IsStatic(Type type)
		{
			return type != null && type.IsAbstract && type.IsSealed;
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000F8BE File Offset: 0x0000DABE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static bool IsStatic(PropertyInfo propertyInfo)
		{
			if (propertyInfo == null)
			{
				throw new ArgumentNullException("propertyInfo");
			}
			return propertyInfo.GetAccessors(true)[0].IsStatic;
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000F8DC File Offset: 0x0000DADC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static bool IsStatic(EventInfo eventInfo)
		{
			if (eventInfo == null)
			{
				throw new ArgumentNullException("eventInfo");
			}
			return eventInfo.GetAddMethod(true).IsStatic;
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000F8F8 File Offset: 0x0000DAF8
		public static int CombinedHashCode(IEnumerable<object> objects)
		{
			int num = 352654597;
			int num2 = num;
			int num3 = 0;
			foreach (object obj in objects)
			{
				if (num3 % 2 == 0)
				{
					num = ((num << 5) + num + (num >> 27) ^ obj.GetHashCode());
				}
				else
				{
					num2 = ((num2 << 5) + num2 + (num2 >> 27) ^ obj.GetHashCode());
				}
				num3++;
			}
			return num + num2 * 1566083941;
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000F980 File Offset: 0x0000DB80
		// Note: this type is marked as 'beforefieldinit'.
		static AccessTools()
		{
			Type type = Type.GetType("System.Runtime.InteropServices.RuntimeInformation", false);
			AccessTools.IsNetFrameworkRuntime = ((type != null) ? type.GetProperty("FrameworkDescription").GetValue(null, null).ToString().StartsWith(".NET Framework") : (!AccessTools.IsMonoRuntime));
			Type type2 = Type.GetType("System.Runtime.InteropServices.RuntimeInformation", false);
			AccessTools.IsNetCoreRuntime = (type2 != null && type2.GetProperty("FrameworkDescription").GetValue(null, null).ToString().StartsWith(".NET Core"));
			AccessTools.addHandlerCache = new Dictionary<Type, FastInvokeHandler>();
			AccessTools.addHandlerCacheLock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
		}

		// Token: 0x040000C8 RID: 200
		public static readonly BindingFlags all = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.SetField | BindingFlags.GetProperty | BindingFlags.SetProperty;

		// Token: 0x040000C9 RID: 201
		public static readonly BindingFlags allDeclared = AccessTools.all | BindingFlags.DeclaredOnly;

		// Token: 0x040000CD RID: 205
		private static readonly Dictionary<Type, FastInvokeHandler> addHandlerCache;

		// Token: 0x040000CE RID: 206
		private static readonly ReaderWriterLockSlim addHandlerCacheLock;

		// Token: 0x020000A0 RID: 160
		// (Invoke) Token: 0x0600051B RID: 1307
		public unsafe delegate F* FieldRef<in T, F>(T instance = default(T));

		// Token: 0x020000A1 RID: 161
		// (Invoke) Token: 0x0600051F RID: 1311
		public unsafe delegate F* StructFieldRef<T, F>(ref T instance) where T : struct;

		// Token: 0x020000A2 RID: 162
		// (Invoke) Token: 0x06000523 RID: 1315
		public unsafe delegate F* FieldRef<F>();

		// Token: 0x020000A3 RID: 163
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040001EA RID: 490
			public static Func<Assembly, IEnumerable<Type>> <0>__GetTypesFromAssembly;
		}
	}
}
