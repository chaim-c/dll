﻿using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization.Formatters;
using System.Security;
using System.Text;

namespace System.Runtime.Serialization
{
	// Token: 0x0200072F RID: 1839
	[ComVisible(true)]
	public static class FormatterServices
	{
		// Token: 0x0600518B RID: 20875 RVA: 0x0011F45C File Offset: 0x0011D65C
		private static MemberInfo[] GetSerializableMembers(RuntimeType type)
		{
			FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			int num = 0;
			for (int i = 0; i < fields.Length; i++)
			{
				if ((fields[i].Attributes & FieldAttributes.NotSerialized) != FieldAttributes.NotSerialized)
				{
					num++;
				}
			}
			if (num != fields.Length)
			{
				FieldInfo[] array = new FieldInfo[num];
				num = 0;
				for (int j = 0; j < fields.Length; j++)
				{
					if ((fields[j].Attributes & FieldAttributes.NotSerialized) != FieldAttributes.NotSerialized)
					{
						array[num] = fields[j];
						num++;
					}
				}
				return array;
			}
			return fields;
		}

		// Token: 0x0600518C RID: 20876 RVA: 0x0011F4E8 File Offset: 0x0011D6E8
		private static bool CheckSerializable(RuntimeType type)
		{
			return type.IsSerializable;
		}

		// Token: 0x0600518D RID: 20877 RVA: 0x0011F4F8 File Offset: 0x0011D6F8
		private static MemberInfo[] InternalGetSerializableMembers(RuntimeType type)
		{
			if (type.IsInterface)
			{
				return new MemberInfo[0];
			}
			if (!FormatterServices.CheckSerializable(type))
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_NonSerType", new object[]
				{
					type.FullName,
					type.Module.Assembly.FullName
				}));
			}
			MemberInfo[] array = FormatterServices.GetSerializableMembers(type);
			RuntimeType runtimeType = (RuntimeType)type.BaseType;
			if (runtimeType != null && runtimeType != (RuntimeType)typeof(object))
			{
				RuntimeType[] array2 = null;
				int num = 0;
				bool parentTypes = FormatterServices.GetParentTypes(runtimeType, out array2, out num);
				if (num > 0)
				{
					List<SerializationFieldInfo> list = new List<SerializationFieldInfo>();
					for (int i = 0; i < num; i++)
					{
						runtimeType = array2[i];
						if (!FormatterServices.CheckSerializable(runtimeType))
						{
							throw new SerializationException(Environment.GetResourceString("Serialization_NonSerType", new object[]
							{
								runtimeType.FullName,
								runtimeType.Module.Assembly.FullName
							}));
						}
						FieldInfo[] fields = runtimeType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
						string namePrefix = parentTypes ? runtimeType.Name : runtimeType.FullName;
						foreach (FieldInfo fieldInfo in fields)
						{
							if (!fieldInfo.IsNotSerialized)
							{
								list.Add(new SerializationFieldInfo((RuntimeFieldInfo)fieldInfo, namePrefix));
							}
						}
					}
					if (list != null && list.Count > 0)
					{
						MemberInfo[] array4 = new MemberInfo[list.Count + array.Length];
						Array.Copy(array, array4, array.Length);
						((ICollection)list).CopyTo(array4, array.Length);
						array = array4;
					}
				}
			}
			return array;
		}

		// Token: 0x0600518E RID: 20878 RVA: 0x0011F690 File Offset: 0x0011D890
		private static bool GetParentTypes(RuntimeType parentType, out RuntimeType[] parentTypes, out int parentTypeCount)
		{
			parentTypes = null;
			parentTypeCount = 0;
			bool flag = true;
			RuntimeType right = (RuntimeType)typeof(object);
			RuntimeType runtimeType = parentType;
			while (runtimeType != right)
			{
				if (!runtimeType.IsInterface)
				{
					string name = runtimeType.Name;
					int num = 0;
					while (flag && num < parentTypeCount)
					{
						string name2 = parentTypes[num].Name;
						if (name2.Length == name.Length && name2[0] == name[0] && name == name2)
						{
							flag = false;
							break;
						}
						num++;
					}
					if (parentTypes == null || parentTypeCount == parentTypes.Length)
					{
						RuntimeType[] array = new RuntimeType[Math.Max(parentTypeCount * 2, 12)];
						if (parentTypes != null)
						{
							Array.Copy(parentTypes, 0, array, 0, parentTypeCount);
						}
						parentTypes = array;
					}
					RuntimeType[] array2 = parentTypes;
					int num2 = parentTypeCount;
					parentTypeCount = num2 + 1;
					array2[num2] = runtimeType;
				}
				runtimeType = (RuntimeType)runtimeType.BaseType;
			}
			return flag;
		}

		// Token: 0x0600518F RID: 20879 RVA: 0x0011F778 File Offset: 0x0011D978
		[SecurityCritical]
		public static MemberInfo[] GetSerializableMembers(Type type)
		{
			return FormatterServices.GetSerializableMembers(type, new StreamingContext(StreamingContextStates.All));
		}

		// Token: 0x06005190 RID: 20880 RVA: 0x0011F78C File Offset: 0x0011D98C
		[SecurityCritical]
		public static MemberInfo[] GetSerializableMembers(Type type, StreamingContext context)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (!(type is RuntimeType))
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_InvalidType", new object[]
				{
					type.ToString()
				}));
			}
			MemberHolder key = new MemberHolder(type, context);
			return FormatterServices.m_MemberInfoTable.GetOrAdd(key, (MemberHolder _) => FormatterServices.InternalGetSerializableMembers((RuntimeType)type));
		}

		// Token: 0x06005191 RID: 20881 RVA: 0x0011F810 File Offset: 0x0011DA10
		public static void CheckTypeSecurity(Type t, TypeFilterLevel securityLevel)
		{
			if (securityLevel == TypeFilterLevel.Low)
			{
				for (int i = 0; i < FormatterServices.advancedTypes.Length; i++)
				{
					if (FormatterServices.advancedTypes[i].IsAssignableFrom(t))
					{
						throw new SecurityException(Environment.GetResourceString("Serialization_TypeSecurity", new object[]
						{
							FormatterServices.advancedTypes[i].FullName,
							t.FullName
						}));
					}
				}
			}
		}

		// Token: 0x06005192 RID: 20882 RVA: 0x0011F870 File Offset: 0x0011DA70
		[SecurityCritical]
		public static object GetUninitializedObject(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (!(type is RuntimeType))
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_InvalidType", new object[]
				{
					type.ToString()
				}));
			}
			return FormatterServices.nativeGetUninitializedObject((RuntimeType)type);
		}

		// Token: 0x06005193 RID: 20883 RVA: 0x0011F8C0 File Offset: 0x0011DAC0
		[SecurityCritical]
		public static object GetSafeUninitializedObject(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (!(type is RuntimeType))
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_InvalidType", new object[]
				{
					type.ToString()
				}));
			}
			if (type == typeof(ConstructionCall) || type == typeof(LogicalCallContext) || type == typeof(SynchronizationAttribute))
			{
				return FormatterServices.nativeGetUninitializedObject((RuntimeType)type);
			}
			object result;
			try
			{
				result = FormatterServices.nativeGetSafeUninitializedObject((RuntimeType)type);
			}
			catch (SecurityException innerException)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_Security", new object[]
				{
					type.FullName
				}), innerException);
			}
			return result;
		}

		// Token: 0x06005194 RID: 20884
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern object nativeGetSafeUninitializedObject(RuntimeType type);

		// Token: 0x06005195 RID: 20885
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern object nativeGetUninitializedObject(RuntimeType type);

		// Token: 0x06005196 RID: 20886
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetEnableUnsafeTypeForwarders();

		// Token: 0x06005197 RID: 20887 RVA: 0x0011F978 File Offset: 0x0011DB78
		[SecuritySafeCritical]
		internal static bool UnsafeTypeForwardersIsEnabled()
		{
			if (!FormatterServices.unsafeTypeForwardersIsEnabledInitialized)
			{
				FormatterServices.unsafeTypeForwardersIsEnabled = FormatterServices.GetEnableUnsafeTypeForwarders();
				FormatterServices.unsafeTypeForwardersIsEnabledInitialized = true;
			}
			return FormatterServices.unsafeTypeForwardersIsEnabled;
		}

		// Token: 0x06005198 RID: 20888 RVA: 0x0011F99C File Offset: 0x0011DB9C
		[SecurityCritical]
		internal static void SerializationSetValue(MemberInfo fi, object target, object value)
		{
			RtFieldInfo rtFieldInfo = fi as RtFieldInfo;
			if (rtFieldInfo != null)
			{
				rtFieldInfo.CheckConsistency(target);
				rtFieldInfo.UnsafeSetValue(target, value, BindingFlags.Default, FormatterServices.s_binder, null);
				return;
			}
			SerializationFieldInfo serializationFieldInfo = fi as SerializationFieldInfo;
			if (serializationFieldInfo != null)
			{
				serializationFieldInfo.InternalSetValue(target, value, BindingFlags.Default, FormatterServices.s_binder, null);
				return;
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFieldInfo"));
		}

		// Token: 0x06005199 RID: 20889 RVA: 0x0011FA00 File Offset: 0x0011DC00
		[SecurityCritical]
		public static object PopulateObjectMembers(object obj, MemberInfo[] members, object[] data)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (members == null)
			{
				throw new ArgumentNullException("members");
			}
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (members.Length != data.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_DataLengthDifferent"));
			}
			for (int i = 0; i < members.Length; i++)
			{
				MemberInfo memberInfo = members[i];
				if (memberInfo == null)
				{
					throw new ArgumentNullException("members", Environment.GetResourceString("ArgumentNull_NullMember", new object[]
					{
						i
					}));
				}
				if (data[i] != null)
				{
					if (memberInfo.MemberType != MemberTypes.Field)
					{
						throw new SerializationException(Environment.GetResourceString("Serialization_UnknownMemberInfo"));
					}
					FormatterServices.SerializationSetValue(memberInfo, obj, data[i]);
				}
			}
			return obj;
		}

		// Token: 0x0600519A RID: 20890 RVA: 0x0011FABC File Offset: 0x0011DCBC
		[SecurityCritical]
		public static object[] GetObjectData(object obj, MemberInfo[] members)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (members == null)
			{
				throw new ArgumentNullException("members");
			}
			int num = members.Length;
			object[] array = new object[num];
			for (int i = 0; i < num; i++)
			{
				MemberInfo memberInfo = members[i];
				if (memberInfo == null)
				{
					throw new ArgumentNullException("members", Environment.GetResourceString("ArgumentNull_NullMember", new object[]
					{
						i
					}));
				}
				if (memberInfo.MemberType != MemberTypes.Field)
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_UnknownMemberInfo"));
				}
				RtFieldInfo rtFieldInfo = memberInfo as RtFieldInfo;
				if (rtFieldInfo != null)
				{
					rtFieldInfo.CheckConsistency(obj);
					array[i] = rtFieldInfo.UnsafeGetValue(obj);
				}
				else
				{
					array[i] = ((SerializationFieldInfo)memberInfo).InternalGetValue(obj);
				}
			}
			return array;
		}

		// Token: 0x0600519B RID: 20891 RVA: 0x0011FB85 File Offset: 0x0011DD85
		[SecurityCritical]
		[ComVisible(false)]
		public static ISerializationSurrogate GetSurrogateForCyclicalReference(ISerializationSurrogate innerSurrogate)
		{
			if (innerSurrogate == null)
			{
				throw new ArgumentNullException("innerSurrogate");
			}
			return new SurrogateForCyclicalReference(innerSurrogate);
		}

		// Token: 0x0600519C RID: 20892 RVA: 0x0011FB9B File Offset: 0x0011DD9B
		[SecurityCritical]
		public static Type GetTypeFromAssembly(Assembly assem, string name)
		{
			if (assem == null)
			{
				throw new ArgumentNullException("assem");
			}
			return assem.GetType(name, false, false);
		}

		// Token: 0x0600519D RID: 20893 RVA: 0x0011FBBC File Offset: 0x0011DDBC
		internal static Assembly LoadAssemblyFromString(string assemblyName)
		{
			return Assembly.Load(assemblyName);
		}

		// Token: 0x0600519E RID: 20894 RVA: 0x0011FBD4 File Offset: 0x0011DDD4
		internal static Assembly LoadAssemblyFromStringNoThrow(string assemblyName)
		{
			try
			{
				return FormatterServices.LoadAssemblyFromString(assemblyName);
			}
			catch (Exception ex)
			{
			}
			return null;
		}

		// Token: 0x0600519F RID: 20895 RVA: 0x0011FC00 File Offset: 0x0011DE00
		internal static string GetClrAssemblyName(Type type, out bool hasTypeForwardedFrom)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			object[] customAttributes = type.GetCustomAttributes(typeof(TypeForwardedFromAttribute), false);
			if (customAttributes != null && customAttributes.Length != 0)
			{
				hasTypeForwardedFrom = true;
				TypeForwardedFromAttribute typeForwardedFromAttribute = (TypeForwardedFromAttribute)customAttributes[0];
				return typeForwardedFromAttribute.AssemblyFullName;
			}
			hasTypeForwardedFrom = false;
			return type.Assembly.FullName;
		}

		// Token: 0x060051A0 RID: 20896 RVA: 0x0011FC55 File Offset: 0x0011DE55
		internal static string GetClrTypeFullName(Type type)
		{
			if (type.IsArray)
			{
				return FormatterServices.GetClrTypeFullNameForArray(type);
			}
			return FormatterServices.GetClrTypeFullNameForNonArrayTypes(type);
		}

		// Token: 0x060051A1 RID: 20897 RVA: 0x0011FC6C File Offset: 0x0011DE6C
		private static string GetClrTypeFullNameForArray(Type type)
		{
			int arrayRank = type.GetArrayRank();
			if (arrayRank == 1)
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}{1}", FormatterServices.GetClrTypeFullName(type.GetElementType()), "[]");
			}
			StringBuilder stringBuilder = new StringBuilder(FormatterServices.GetClrTypeFullName(type.GetElementType())).Append("[");
			for (int i = 1; i < arrayRank; i++)
			{
				stringBuilder.Append(",");
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x060051A2 RID: 20898 RVA: 0x0011FCEC File Offset: 0x0011DEEC
		private static string GetClrTypeFullNameForNonArrayTypes(Type type)
		{
			if (!type.IsGenericType)
			{
				return type.FullName;
			}
			Type[] genericArguments = type.GetGenericArguments();
			StringBuilder stringBuilder = new StringBuilder(type.GetGenericTypeDefinition().FullName).Append("[");
			foreach (Type type2 in genericArguments)
			{
				stringBuilder.Append("[").Append(FormatterServices.GetClrTypeFullName(type2)).Append(", ");
				bool flag;
				stringBuilder.Append(FormatterServices.GetClrAssemblyName(type2, out flag)).Append("],");
			}
			return stringBuilder.Remove(stringBuilder.Length - 1, 1).Append("]").ToString();
		}

		// Token: 0x0400243B RID: 9275
		internal static ConcurrentDictionary<MemberHolder, MemberInfo[]> m_MemberInfoTable = new ConcurrentDictionary<MemberHolder, MemberInfo[]>();

		// Token: 0x0400243C RID: 9276
		[SecurityCritical]
		private static bool unsafeTypeForwardersIsEnabled = false;

		// Token: 0x0400243D RID: 9277
		[SecurityCritical]
		private static volatile bool unsafeTypeForwardersIsEnabledInitialized = false;

		// Token: 0x0400243E RID: 9278
		private static readonly Type[] advancedTypes = new Type[]
		{
			typeof(DelegateSerializationHolder),
			typeof(ObjRef),
			typeof(IEnvoyInfo),
			typeof(ISponsor)
		};

		// Token: 0x0400243F RID: 9279
		private static Binder s_binder = Type.DefaultBinder;
	}
}
