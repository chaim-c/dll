﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Newtonsoft.Json.Serialization;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000066 RID: 102
	[NullableContext(1)]
	[Nullable(0)]
	internal static class ReflectionUtils
	{
		// Token: 0x06000581 RID: 1409 RVA: 0x000171F4 File Offset: 0x000153F4
		public static bool IsVirtual(this PropertyInfo propertyInfo)
		{
			ValidationUtils.ArgumentNotNull(propertyInfo, "propertyInfo");
			MethodInfo methodInfo = propertyInfo.GetGetMethod(true);
			if (methodInfo != null && methodInfo.IsVirtual)
			{
				return true;
			}
			methodInfo = propertyInfo.GetSetMethod(true);
			return methodInfo != null && methodInfo.IsVirtual;
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00017244 File Offset: 0x00015444
		[return: Nullable(2)]
		public static MethodInfo GetBaseDefinition(this PropertyInfo propertyInfo)
		{
			ValidationUtils.ArgumentNotNull(propertyInfo, "propertyInfo");
			MethodInfo getMethod = propertyInfo.GetGetMethod(true);
			if (getMethod != null)
			{
				return getMethod.GetBaseDefinition();
			}
			MethodInfo setMethod = propertyInfo.GetSetMethod(true);
			if (setMethod == null)
			{
				return null;
			}
			return setMethod.GetBaseDefinition();
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00017288 File Offset: 0x00015488
		public static bool IsPublic(PropertyInfo property)
		{
			MethodInfo getMethod = property.GetGetMethod();
			if (getMethod != null && getMethod.IsPublic)
			{
				return true;
			}
			MethodInfo setMethod = property.GetSetMethod();
			return setMethod != null && setMethod.IsPublic;
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x000172CA File Offset: 0x000154CA
		[NullableContext(2)]
		public static Type GetObjectType(object v)
		{
			if (v == null)
			{
				return null;
			}
			return v.GetType();
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x000172D8 File Offset: 0x000154D8
		public static string GetTypeName(Type t, TypeNameAssemblyFormatHandling assemblyFormat, [Nullable(2)] ISerializationBinder binder)
		{
			string fullyQualifiedTypeName = ReflectionUtils.GetFullyQualifiedTypeName(t, binder);
			if (assemblyFormat == TypeNameAssemblyFormatHandling.Simple)
			{
				return ReflectionUtils.RemoveAssemblyDetails(fullyQualifiedTypeName);
			}
			if (assemblyFormat != TypeNameAssemblyFormatHandling.Full)
			{
				throw new ArgumentOutOfRangeException();
			}
			return fullyQualifiedTypeName;
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x00017304 File Offset: 0x00015504
		private static string GetFullyQualifiedTypeName(Type t, [Nullable(2)] ISerializationBinder binder)
		{
			if (binder != null)
			{
				string text;
				string str;
				binder.BindToName(t, out text, out str);
				return str + ((text == null) ? "" : (", " + text));
			}
			return t.AssemblyQualifiedName;
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x00017344 File Offset: 0x00015544
		private static string RemoveAssemblyDetails(string fullyQualifiedTypeName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			bool flag2 = false;
			foreach (char c in fullyQualifiedTypeName)
			{
				if (c != ',')
				{
					if (c == '[' || c == ']')
					{
						flag = false;
						flag2 = false;
						stringBuilder.Append(c);
					}
					else if (!flag2)
					{
						stringBuilder.Append(c);
					}
				}
				else if (!flag)
				{
					flag = true;
					stringBuilder.Append(c);
				}
				else
				{
					flag2 = true;
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x000173BC File Offset: 0x000155BC
		public static bool HasDefaultConstructor(Type t, bool nonPublic)
		{
			ValidationUtils.ArgumentNotNull(t, "t");
			return t.IsValueType() || ReflectionUtils.GetDefaultConstructor(t, nonPublic) != null;
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x000173E0 File Offset: 0x000155E0
		public static ConstructorInfo GetDefaultConstructor(Type t)
		{
			return ReflectionUtils.GetDefaultConstructor(t, false);
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x000173EC File Offset: 0x000155EC
		public static ConstructorInfo GetDefaultConstructor(Type t, bool nonPublic)
		{
			BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;
			if (nonPublic)
			{
				bindingFlags |= BindingFlags.NonPublic;
			}
			return t.GetConstructors(bindingFlags).SingleOrDefault((ConstructorInfo c) => !c.GetParameters().Any<ParameterInfo>());
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x0001742F File Offset: 0x0001562F
		public static bool IsNullable(Type t)
		{
			ValidationUtils.ArgumentNotNull(t, "t");
			return !t.IsValueType() || ReflectionUtils.IsNullableType(t);
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x0001744C File Offset: 0x0001564C
		public static bool IsNullableType(Type t)
		{
			ValidationUtils.ArgumentNotNull(t, "t");
			return t.IsGenericType() && t.GetGenericTypeDefinition() == typeof(Nullable<>);
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00017478 File Offset: 0x00015678
		public static Type EnsureNotNullableType(Type t)
		{
			if (!ReflectionUtils.IsNullableType(t))
			{
				return t;
			}
			return Nullable.GetUnderlyingType(t);
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x0001748A File Offset: 0x0001568A
		public static Type EnsureNotByRefType(Type t)
		{
			if (!t.IsByRef || !t.HasElementType)
			{
				return t;
			}
			return t.GetElementType();
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x000174A4 File Offset: 0x000156A4
		public static bool IsGenericDefinition(Type type, Type genericInterfaceDefinition)
		{
			return type.IsGenericType() && type.GetGenericTypeDefinition() == genericInterfaceDefinition;
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x000174BC File Offset: 0x000156BC
		public static bool ImplementsGenericDefinition(Type type, Type genericInterfaceDefinition)
		{
			Type type2;
			return ReflectionUtils.ImplementsGenericDefinition(type, genericInterfaceDefinition, out type2);
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x000174D4 File Offset: 0x000156D4
		public static bool ImplementsGenericDefinition(Type type, Type genericInterfaceDefinition, [Nullable(2)] [NotNullWhen(true)] out Type implementingType)
		{
			ValidationUtils.ArgumentNotNull(type, "type");
			ValidationUtils.ArgumentNotNull(genericInterfaceDefinition, "genericInterfaceDefinition");
			if (!genericInterfaceDefinition.IsInterface() || !genericInterfaceDefinition.IsGenericTypeDefinition())
			{
				throw new ArgumentNullException("'{0}' is not a generic interface definition.".FormatWith(CultureInfo.InvariantCulture, genericInterfaceDefinition));
			}
			if (type.IsInterface() && type.IsGenericType())
			{
				Type genericTypeDefinition = type.GetGenericTypeDefinition();
				if (genericInterfaceDefinition == genericTypeDefinition)
				{
					implementingType = type;
					return true;
				}
			}
			foreach (Type type2 in type.GetInterfaces())
			{
				if (type2.IsGenericType())
				{
					Type genericTypeDefinition2 = type2.GetGenericTypeDefinition();
					if (genericInterfaceDefinition == genericTypeDefinition2)
					{
						implementingType = type2;
						return true;
					}
				}
			}
			implementingType = null;
			return false;
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x00017580 File Offset: 0x00015780
		public static bool InheritsGenericDefinition(Type type, Type genericClassDefinition)
		{
			Type type2;
			return ReflectionUtils.InheritsGenericDefinition(type, genericClassDefinition, out type2);
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x00017598 File Offset: 0x00015798
		public static bool InheritsGenericDefinition(Type type, Type genericClassDefinition, [Nullable(2)] out Type implementingType)
		{
			ValidationUtils.ArgumentNotNull(type, "type");
			ValidationUtils.ArgumentNotNull(genericClassDefinition, "genericClassDefinition");
			if (!genericClassDefinition.IsClass() || !genericClassDefinition.IsGenericTypeDefinition())
			{
				throw new ArgumentNullException("'{0}' is not a generic class definition.".FormatWith(CultureInfo.InvariantCulture, genericClassDefinition));
			}
			return ReflectionUtils.InheritsGenericDefinitionInternal(type, genericClassDefinition, out implementingType);
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x000175E9 File Offset: 0x000157E9
		private static bool InheritsGenericDefinitionInternal(Type currentType, Type genericClassDefinition, [Nullable(2)] out Type implementingType)
		{
			while (!currentType.IsGenericType() || !(genericClassDefinition == currentType.GetGenericTypeDefinition()))
			{
				currentType = currentType.BaseType();
				if (!(currentType != null))
				{
					implementingType = null;
					return false;
				}
			}
			implementingType = currentType;
			return true;
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0001761C File Offset: 0x0001581C
		[return: Nullable(2)]
		public static Type GetCollectionItemType(Type type)
		{
			ValidationUtils.ArgumentNotNull(type, "type");
			if (type.IsArray)
			{
				return type.GetElementType();
			}
			Type type2;
			if (ReflectionUtils.ImplementsGenericDefinition(type, typeof(IEnumerable<>), out type2))
			{
				if (type2.IsGenericTypeDefinition())
				{
					throw new Exception("Type {0} is not a collection.".FormatWith(CultureInfo.InvariantCulture, type));
				}
				return type2.GetGenericArguments()[0];
			}
			else
			{
				if (typeof(IEnumerable).IsAssignableFrom(type))
				{
					return null;
				}
				throw new Exception("Type {0} is not a collection.".FormatWith(CultureInfo.InvariantCulture, type));
			}
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x000176A8 File Offset: 0x000158A8
		[NullableContext(2)]
		public static void GetDictionaryKeyValueTypes([Nullable(1)] Type dictionaryType, out Type keyType, out Type valueType)
		{
			ValidationUtils.ArgumentNotNull(dictionaryType, "dictionaryType");
			Type type;
			if (ReflectionUtils.ImplementsGenericDefinition(dictionaryType, typeof(IDictionary<, >), out type))
			{
				if (type.IsGenericTypeDefinition())
				{
					throw new Exception("Type {0} is not a dictionary.".FormatWith(CultureInfo.InvariantCulture, dictionaryType));
				}
				Type[] genericArguments = type.GetGenericArguments();
				keyType = genericArguments[0];
				valueType = genericArguments[1];
				return;
			}
			else
			{
				if (typeof(IDictionary).IsAssignableFrom(dictionaryType))
				{
					keyType = null;
					valueType = null;
					return;
				}
				throw new Exception("Type {0} is not a dictionary.".FormatWith(CultureInfo.InvariantCulture, dictionaryType));
			}
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x00017734 File Offset: 0x00015934
		public static Type GetMemberUnderlyingType(MemberInfo member)
		{
			ValidationUtils.ArgumentNotNull(member, "member");
			MemberTypes memberTypes = member.MemberType();
			if (memberTypes <= MemberTypes.Field)
			{
				if (memberTypes == MemberTypes.Event)
				{
					return ((EventInfo)member).EventHandlerType;
				}
				if (memberTypes == MemberTypes.Field)
				{
					return ((FieldInfo)member).FieldType;
				}
			}
			else
			{
				if (memberTypes == MemberTypes.Method)
				{
					return ((MethodInfo)member).ReturnType;
				}
				if (memberTypes == MemberTypes.Property)
				{
					return ((PropertyInfo)member).PropertyType;
				}
			}
			throw new ArgumentException("MemberInfo must be of type FieldInfo, PropertyInfo, EventInfo or MethodInfo", "member");
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x000177AC File Offset: 0x000159AC
		public static bool IsByRefLikeType(Type type)
		{
			if (!type.IsValueType())
			{
				return false;
			}
			Attribute[] attributes = ReflectionUtils.GetAttributes(type, null, false);
			for (int i = 0; i < attributes.Length; i++)
			{
				if (string.Equals(attributes[i].GetType().FullName, "System.Runtime.CompilerServices.IsByRefLikeAttribute", StringComparison.Ordinal))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x000177F7 File Offset: 0x000159F7
		public static bool IsIndexedProperty(PropertyInfo property)
		{
			ValidationUtils.ArgumentNotNull(property, "property");
			return property.GetIndexParameters().Length != 0;
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x00017810 File Offset: 0x00015A10
		public static object GetMemberValue(MemberInfo member, object target)
		{
			ValidationUtils.ArgumentNotNull(member, "member");
			ValidationUtils.ArgumentNotNull(target, "target");
			MemberTypes memberTypes = member.MemberType();
			if (memberTypes != MemberTypes.Field)
			{
				if (memberTypes == MemberTypes.Property)
				{
					try
					{
						return ((PropertyInfo)member).GetValue(target, null);
					}
					catch (TargetParameterCountException innerException)
					{
						throw new ArgumentException("MemberInfo '{0}' has index parameters".FormatWith(CultureInfo.InvariantCulture, member.Name), innerException);
					}
				}
				throw new ArgumentException("MemberInfo '{0}' is not of type FieldInfo or PropertyInfo".FormatWith(CultureInfo.InvariantCulture, member.Name), "member");
			}
			return ((FieldInfo)member).GetValue(target);
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x000178B4 File Offset: 0x00015AB4
		public static void SetMemberValue(MemberInfo member, object target, [Nullable(2)] object value)
		{
			ValidationUtils.ArgumentNotNull(member, "member");
			ValidationUtils.ArgumentNotNull(target, "target");
			MemberTypes memberTypes = member.MemberType();
			if (memberTypes == MemberTypes.Field)
			{
				((FieldInfo)member).SetValue(target, value);
				return;
			}
			if (memberTypes != MemberTypes.Property)
			{
				throw new ArgumentException("MemberInfo '{0}' must be of type FieldInfo or PropertyInfo".FormatWith(CultureInfo.InvariantCulture, member.Name), "member");
			}
			((PropertyInfo)member).SetValue(target, value, null);
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x00017928 File Offset: 0x00015B28
		public static bool CanReadMemberValue(MemberInfo member, bool nonPublic)
		{
			MemberTypes memberTypes = member.MemberType();
			if (memberTypes == MemberTypes.Field)
			{
				FieldInfo fieldInfo = (FieldInfo)member;
				return nonPublic || fieldInfo.IsPublic;
			}
			if (memberTypes != MemberTypes.Property)
			{
				return false;
			}
			PropertyInfo propertyInfo = (PropertyInfo)member;
			return propertyInfo.CanRead && (nonPublic || propertyInfo.GetGetMethod(nonPublic) != null);
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x00017984 File Offset: 0x00015B84
		public static bool CanSetMemberValue(MemberInfo member, bool nonPublic, bool canSetReadOnly)
		{
			MemberTypes memberTypes = member.MemberType();
			if (memberTypes == MemberTypes.Field)
			{
				FieldInfo fieldInfo = (FieldInfo)member;
				return !fieldInfo.IsLiteral && (!fieldInfo.IsInitOnly || canSetReadOnly) && (nonPublic || fieldInfo.IsPublic);
			}
			if (memberTypes != MemberTypes.Property)
			{
				return false;
			}
			PropertyInfo propertyInfo = (PropertyInfo)member;
			return propertyInfo.CanWrite && (nonPublic || propertyInfo.GetSetMethod(nonPublic) != null);
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x000179F8 File Offset: 0x00015BF8
		public static List<MemberInfo> GetFieldsAndProperties(Type type, BindingFlags bindingAttr)
		{
			List<MemberInfo> list = new List<MemberInfo>();
			list.AddRange(ReflectionUtils.GetFields(type, bindingAttr));
			list.AddRange(ReflectionUtils.GetProperties(type, bindingAttr));
			List<MemberInfo> list2 = new List<MemberInfo>(list.Count);
			foreach (IGrouping<string, MemberInfo> grouping in from m in list
			group m by m.Name)
			{
				if (grouping.Count<MemberInfo>() == 1)
				{
					list2.Add(grouping.First<MemberInfo>());
				}
				else
				{
					List<MemberInfo> list3 = new List<MemberInfo>();
					using (IEnumerator<MemberInfo> enumerator2 = grouping.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							MemberInfo memberInfo = enumerator2.Current;
							if (list3.Count == 0)
							{
								list3.Add(memberInfo);
							}
							else if ((!ReflectionUtils.IsOverridenGenericMember(memberInfo, bindingAttr) || memberInfo.Name == "Item") && !list3.Any((MemberInfo m) => m.DeclaringType == memberInfo.DeclaringType))
							{
								list3.Add(memberInfo);
							}
						}
					}
					list2.AddRange(list3);
				}
			}
			return list2;
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x00017B58 File Offset: 0x00015D58
		private static bool IsOverridenGenericMember(MemberInfo memberInfo, BindingFlags bindingAttr)
		{
			if (memberInfo.MemberType() != MemberTypes.Property)
			{
				return false;
			}
			PropertyInfo propertyInfo = (PropertyInfo)memberInfo;
			if (!propertyInfo.IsVirtual())
			{
				return false;
			}
			Type declaringType = propertyInfo.DeclaringType;
			if (!declaringType.IsGenericType())
			{
				return false;
			}
			Type genericTypeDefinition = declaringType.GetGenericTypeDefinition();
			if (genericTypeDefinition == null)
			{
				return false;
			}
			MemberInfo[] member = genericTypeDefinition.GetMember(propertyInfo.Name, bindingAttr);
			return member.Length != 0 && ReflectionUtils.GetMemberUnderlyingType(member[0]).IsGenericParameter;
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x00017BCB File Offset: 0x00015DCB
		[return: Nullable(2)]
		public static T GetAttribute<[Nullable(0)] T>(object attributeProvider) where T : Attribute
		{
			return ReflectionUtils.GetAttribute<T>(attributeProvider, true);
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x00017BD4 File Offset: 0x00015DD4
		[return: Nullable(2)]
		public static T GetAttribute<[Nullable(0)] T>(object attributeProvider, bool inherit) where T : Attribute
		{
			T[] attributes = ReflectionUtils.GetAttributes<T>(attributeProvider, inherit);
			if (attributes == null)
			{
				return default(T);
			}
			return attributes.FirstOrDefault<T>();
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x00017BFC File Offset: 0x00015DFC
		public static T[] GetAttributes<[Nullable(0)] T>(object attributeProvider, bool inherit) where T : Attribute
		{
			Attribute[] attributes = ReflectionUtils.GetAttributes(attributeProvider, typeof(T), inherit);
			T[] array = attributes as T[];
			if (array != null)
			{
				return array;
			}
			return attributes.Cast<T>().ToArray<T>();
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x00017C34 File Offset: 0x00015E34
		public static Attribute[] GetAttributes(object attributeProvider, [Nullable(2)] Type attributeType, bool inherit)
		{
			ValidationUtils.ArgumentNotNull(attributeProvider, "attributeProvider");
			Type type = attributeProvider as Type;
			if (type != null)
			{
				return ((attributeType != null) ? type.GetCustomAttributes(attributeType, inherit) : type.GetCustomAttributes(inherit)).Cast<Attribute>().ToArray<Attribute>();
			}
			Assembly assembly = attributeProvider as Assembly;
			if (assembly == null)
			{
				MemberInfo memberInfo = attributeProvider as MemberInfo;
				if (memberInfo == null)
				{
					Module module = attributeProvider as Module;
					if (module == null)
					{
						ParameterInfo parameterInfo = attributeProvider as ParameterInfo;
						if (parameterInfo == null)
						{
							ICustomAttributeProvider customAttributeProvider = (ICustomAttributeProvider)attributeProvider;
							return (Attribute[])((attributeType != null) ? customAttributeProvider.GetCustomAttributes(attributeType, inherit) : customAttributeProvider.GetCustomAttributes(inherit));
						}
						if (!(attributeType != null))
						{
							return Attribute.GetCustomAttributes(parameterInfo, inherit);
						}
						return Attribute.GetCustomAttributes(parameterInfo, attributeType, inherit);
					}
					else
					{
						if (!(attributeType != null))
						{
							return Attribute.GetCustomAttributes(module, inherit);
						}
						return Attribute.GetCustomAttributes(module, attributeType, inherit);
					}
				}
				else
				{
					if (!(attributeType != null))
					{
						return Attribute.GetCustomAttributes(memberInfo, inherit);
					}
					return Attribute.GetCustomAttributes(memberInfo, attributeType, inherit);
				}
			}
			else
			{
				if (!(attributeType != null))
				{
					return Attribute.GetCustomAttributes(assembly);
				}
				return Attribute.GetCustomAttributes(assembly, attributeType);
			}
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x00017D44 File Offset: 0x00015F44
		[return: Nullable(new byte[]
		{
			0,
			2,
			1
		})]
		public static StructMultiKey<string, string> SplitFullyQualifiedTypeName(string fullyQualifiedTypeName)
		{
			int? assemblyDelimiterIndex = ReflectionUtils.GetAssemblyDelimiterIndex(fullyQualifiedTypeName);
			string v;
			string v2;
			if (assemblyDelimiterIndex != null)
			{
				v = fullyQualifiedTypeName.Trim(0, assemblyDelimiterIndex.GetValueOrDefault());
				v2 = fullyQualifiedTypeName.Trim(assemblyDelimiterIndex.GetValueOrDefault() + 1, fullyQualifiedTypeName.Length - assemblyDelimiterIndex.GetValueOrDefault() - 1);
			}
			else
			{
				v = fullyQualifiedTypeName;
				v2 = null;
			}
			return new StructMultiKey<string, string>(v2, v);
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x00017DA0 File Offset: 0x00015FA0
		private static int? GetAssemblyDelimiterIndex(string fullyQualifiedTypeName)
		{
			int num = 0;
			for (int i = 0; i < fullyQualifiedTypeName.Length; i++)
			{
				char c = fullyQualifiedTypeName[i];
				if (c != ',')
				{
					if (c != '[')
					{
						if (c == ']')
						{
							num--;
						}
					}
					else
					{
						num++;
					}
				}
				else if (num == 0)
				{
					return new int?(i);
				}
			}
			return null;
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x00017DF8 File Offset: 0x00015FF8
		public static MemberInfo GetMemberInfoFromType(Type targetType, MemberInfo memberInfo)
		{
			if (memberInfo.MemberType() == MemberTypes.Property)
			{
				PropertyInfo propertyInfo = (PropertyInfo)memberInfo;
				Type[] types = (from p in propertyInfo.GetIndexParameters()
				select p.ParameterType).ToArray<Type>();
				return targetType.GetProperty(propertyInfo.Name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, propertyInfo.PropertyType, types, null);
			}
			return targetType.GetMember(memberInfo.Name, memberInfo.MemberType(), BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic).SingleOrDefault<MemberInfo>();
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x00017E77 File Offset: 0x00016077
		public static IEnumerable<FieldInfo> GetFields(Type targetType, BindingFlags bindingAttr)
		{
			ValidationUtils.ArgumentNotNull(targetType, "targetType");
			List<MemberInfo> list = new List<MemberInfo>(targetType.GetFields(bindingAttr));
			ReflectionUtils.GetChildPrivateFields(list, targetType, bindingAttr);
			return list.Cast<FieldInfo>();
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x00017EA0 File Offset: 0x000160A0
		private static void GetChildPrivateFields(IList<MemberInfo> initialFields, Type targetType, BindingFlags bindingAttr)
		{
			if ((bindingAttr & BindingFlags.NonPublic) != BindingFlags.Default)
			{
				BindingFlags bindingAttr2 = bindingAttr.RemoveFlag(BindingFlags.Public);
				while ((targetType = targetType.BaseType()) != null)
				{
					IEnumerable<FieldInfo> collection = from f in targetType.GetFields(bindingAttr2)
					where f.IsPrivate
					select f;
					initialFields.AddRange(collection);
				}
			}
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x00017F04 File Offset: 0x00016104
		public static IEnumerable<PropertyInfo> GetProperties(Type targetType, BindingFlags bindingAttr)
		{
			ValidationUtils.ArgumentNotNull(targetType, "targetType");
			List<PropertyInfo> list = new List<PropertyInfo>(targetType.GetProperties(bindingAttr));
			if (targetType.IsInterface())
			{
				foreach (Type type in targetType.GetInterfaces())
				{
					list.AddRange(type.GetProperties(bindingAttr));
				}
			}
			ReflectionUtils.GetChildPrivateProperties(list, targetType, bindingAttr);
			for (int j = 0; j < list.Count; j++)
			{
				PropertyInfo propertyInfo = list[j];
				if (propertyInfo.DeclaringType != targetType)
				{
					PropertyInfo value = (PropertyInfo)ReflectionUtils.GetMemberInfoFromType(propertyInfo.DeclaringType, propertyInfo);
					list[j] = value;
				}
			}
			return list;
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x00017FAD File Offset: 0x000161AD
		public static BindingFlags RemoveFlag(this BindingFlags bindingAttr, BindingFlags flag)
		{
			if ((bindingAttr & flag) != flag)
			{
				return bindingAttr;
			}
			return bindingAttr ^ flag;
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x00017FBC File Offset: 0x000161BC
		private static void GetChildPrivateProperties(IList<PropertyInfo> initialProperties, Type targetType, BindingFlags bindingAttr)
		{
			while ((targetType = targetType.BaseType()) != null)
			{
				foreach (PropertyInfo subTypeProperty in targetType.GetProperties(bindingAttr))
				{
					ReflectionUtils.<>c__DisplayClass44_0 CS$<>8__locals1 = new ReflectionUtils.<>c__DisplayClass44_0();
					CS$<>8__locals1.subTypeProperty = subTypeProperty;
					if (!CS$<>8__locals1.subTypeProperty.IsVirtual())
					{
						if (!ReflectionUtils.IsPublic(CS$<>8__locals1.subTypeProperty))
						{
							int num = initialProperties.IndexOf((PropertyInfo p) => p.Name == CS$<>8__locals1.subTypeProperty.Name);
							if (num == -1)
							{
								initialProperties.Add(CS$<>8__locals1.subTypeProperty);
							}
							else if (!ReflectionUtils.IsPublic(initialProperties[num]))
							{
								initialProperties[num] = CS$<>8__locals1.subTypeProperty;
							}
						}
						else if (initialProperties.IndexOf((PropertyInfo p) => p.Name == CS$<>8__locals1.subTypeProperty.Name && p.DeclaringType == CS$<>8__locals1.subTypeProperty.DeclaringType) == -1)
						{
							initialProperties.Add(CS$<>8__locals1.subTypeProperty);
						}
					}
					else
					{
						ReflectionUtils.<>c__DisplayClass44_1 CS$<>8__locals2 = new ReflectionUtils.<>c__DisplayClass44_1();
						CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
						ReflectionUtils.<>c__DisplayClass44_1 CS$<>8__locals3 = CS$<>8__locals2;
						MethodInfo baseDefinition = CS$<>8__locals2.CS$<>8__locals1.subTypeProperty.GetBaseDefinition();
						CS$<>8__locals3.subTypePropertyDeclaringType = (((baseDefinition != null) ? baseDefinition.DeclaringType : null) ?? CS$<>8__locals2.CS$<>8__locals1.subTypeProperty.DeclaringType);
						if (initialProperties.IndexOf(delegate(PropertyInfo p)
						{
							if (p.Name == CS$<>8__locals2.CS$<>8__locals1.subTypeProperty.Name && p.IsVirtual())
							{
								MethodInfo baseDefinition2 = p.GetBaseDefinition();
								return (((baseDefinition2 != null) ? baseDefinition2.DeclaringType : null) ?? p.DeclaringType).IsAssignableFrom(CS$<>8__locals2.subTypePropertyDeclaringType);
							}
							return false;
						}) == -1)
						{
							initialProperties.Add(CS$<>8__locals2.CS$<>8__locals1.subTypeProperty);
						}
					}
				}
			}
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x0001810C File Offset: 0x0001630C
		public static bool IsMethodOverridden(Type currentType, Type methodDeclaringType, string method)
		{
			return currentType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Any((MethodInfo info) => info.Name == method && info.DeclaringType != methodDeclaringType && info.GetBaseDefinition().DeclaringType == methodDeclaringType);
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x00018148 File Offset: 0x00016348
		[return: Nullable(2)]
		public static object GetDefaultValue(Type type)
		{
			if (!type.IsValueType())
			{
				return null;
			}
			PrimitiveTypeCode typeCode = ConvertUtils.GetTypeCode(type);
			switch (typeCode)
			{
			case PrimitiveTypeCode.Char:
			case PrimitiveTypeCode.SByte:
			case PrimitiveTypeCode.Int16:
			case PrimitiveTypeCode.UInt16:
			case PrimitiveTypeCode.Int32:
			case PrimitiveTypeCode.Byte:
			case PrimitiveTypeCode.UInt32:
				return 0;
			case PrimitiveTypeCode.CharNullable:
			case PrimitiveTypeCode.BooleanNullable:
			case PrimitiveTypeCode.SByteNullable:
			case PrimitiveTypeCode.Int16Nullable:
			case PrimitiveTypeCode.UInt16Nullable:
			case PrimitiveTypeCode.Int32Nullable:
			case PrimitiveTypeCode.ByteNullable:
			case PrimitiveTypeCode.UInt32Nullable:
			case PrimitiveTypeCode.Int64Nullable:
			case PrimitiveTypeCode.UInt64Nullable:
			case PrimitiveTypeCode.SingleNullable:
			case PrimitiveTypeCode.DoubleNullable:
			case PrimitiveTypeCode.DateTimeNullable:
			case PrimitiveTypeCode.DateTimeOffsetNullable:
			case PrimitiveTypeCode.DecimalNullable:
				break;
			case PrimitiveTypeCode.Boolean:
				return false;
			case PrimitiveTypeCode.Int64:
			case PrimitiveTypeCode.UInt64:
				return 0L;
			case PrimitiveTypeCode.Single:
				return 0f;
			case PrimitiveTypeCode.Double:
				return 0.0;
			case PrimitiveTypeCode.DateTime:
				return default(DateTime);
			case PrimitiveTypeCode.DateTimeOffset:
				return default(DateTimeOffset);
			case PrimitiveTypeCode.Decimal:
				return 0m;
			case PrimitiveTypeCode.Guid:
				return default(Guid);
			default:
				if (typeCode == PrimitiveTypeCode.BigInteger)
				{
					return default(BigInteger);
				}
				break;
			}
			if (ReflectionUtils.IsNullable(type))
			{
				return null;
			}
			return Activator.CreateInstance(type);
		}

		// Token: 0x040001FF RID: 511
		public static readonly Type[] EmptyTypes = Type.EmptyTypes;
	}
}
