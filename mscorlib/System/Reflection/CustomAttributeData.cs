﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Reflection
{
	// Token: 0x020005D4 RID: 1492
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class CustomAttributeData
	{
		// Token: 0x06004528 RID: 17704 RVA: 0x000FDD5A File Offset: 0x000FBF5A
		public static IList<CustomAttributeData> GetCustomAttributes(MemberInfo target)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			return target.GetCustomAttributesData();
		}

		// Token: 0x06004529 RID: 17705 RVA: 0x000FDD76 File Offset: 0x000FBF76
		public static IList<CustomAttributeData> GetCustomAttributes(Module target)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			return target.GetCustomAttributesData();
		}

		// Token: 0x0600452A RID: 17706 RVA: 0x000FDD92 File Offset: 0x000FBF92
		public static IList<CustomAttributeData> GetCustomAttributes(Assembly target)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			return target.GetCustomAttributesData();
		}

		// Token: 0x0600452B RID: 17707 RVA: 0x000FDDAE File Offset: 0x000FBFAE
		public static IList<CustomAttributeData> GetCustomAttributes(ParameterInfo target)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			return target.GetCustomAttributesData();
		}

		// Token: 0x0600452C RID: 17708 RVA: 0x000FDDC4 File Offset: 0x000FBFC4
		[SecuritySafeCritical]
		internal static IList<CustomAttributeData> GetCustomAttributesInternal(RuntimeType target)
		{
			IList<CustomAttributeData> customAttributes = CustomAttributeData.GetCustomAttributes(target.GetRuntimeModule(), target.MetadataToken);
			int num = 0;
			Attribute[] customAttributes2 = PseudoCustomAttribute.GetCustomAttributes(target, typeof(object) as RuntimeType, true, out num);
			if (num == 0)
			{
				return customAttributes;
			}
			CustomAttributeData[] array = new CustomAttributeData[customAttributes.Count + num];
			customAttributes.CopyTo(array, num);
			for (int i = 0; i < num; i++)
			{
				array[i] = new CustomAttributeData(customAttributes2[i]);
			}
			return Array.AsReadOnly<CustomAttributeData>(array);
		}

		// Token: 0x0600452D RID: 17709 RVA: 0x000FDE3C File Offset: 0x000FC03C
		[SecuritySafeCritical]
		internal static IList<CustomAttributeData> GetCustomAttributesInternal(RuntimeFieldInfo target)
		{
			IList<CustomAttributeData> customAttributes = CustomAttributeData.GetCustomAttributes(target.GetRuntimeModule(), target.MetadataToken);
			int num = 0;
			Attribute[] customAttributes2 = PseudoCustomAttribute.GetCustomAttributes(target, typeof(object) as RuntimeType, out num);
			if (num == 0)
			{
				return customAttributes;
			}
			CustomAttributeData[] array = new CustomAttributeData[customAttributes.Count + num];
			customAttributes.CopyTo(array, num);
			for (int i = 0; i < num; i++)
			{
				array[i] = new CustomAttributeData(customAttributes2[i]);
			}
			return Array.AsReadOnly<CustomAttributeData>(array);
		}

		// Token: 0x0600452E RID: 17710 RVA: 0x000FDEB4 File Offset: 0x000FC0B4
		[SecuritySafeCritical]
		internal static IList<CustomAttributeData> GetCustomAttributesInternal(RuntimeMethodInfo target)
		{
			IList<CustomAttributeData> customAttributes = CustomAttributeData.GetCustomAttributes(target.GetRuntimeModule(), target.MetadataToken);
			int num = 0;
			Attribute[] customAttributes2 = PseudoCustomAttribute.GetCustomAttributes(target, typeof(object) as RuntimeType, true, out num);
			if (num == 0)
			{
				return customAttributes;
			}
			CustomAttributeData[] array = new CustomAttributeData[customAttributes.Count + num];
			customAttributes.CopyTo(array, num);
			for (int i = 0; i < num; i++)
			{
				array[i] = new CustomAttributeData(customAttributes2[i]);
			}
			return Array.AsReadOnly<CustomAttributeData>(array);
		}

		// Token: 0x0600452F RID: 17711 RVA: 0x000FDF2C File Offset: 0x000FC12C
		[SecuritySafeCritical]
		internal static IList<CustomAttributeData> GetCustomAttributesInternal(RuntimeConstructorInfo target)
		{
			return CustomAttributeData.GetCustomAttributes(target.GetRuntimeModule(), target.MetadataToken);
		}

		// Token: 0x06004530 RID: 17712 RVA: 0x000FDF3F File Offset: 0x000FC13F
		[SecuritySafeCritical]
		internal static IList<CustomAttributeData> GetCustomAttributesInternal(RuntimeEventInfo target)
		{
			return CustomAttributeData.GetCustomAttributes(target.GetRuntimeModule(), target.MetadataToken);
		}

		// Token: 0x06004531 RID: 17713 RVA: 0x000FDF52 File Offset: 0x000FC152
		[SecuritySafeCritical]
		internal static IList<CustomAttributeData> GetCustomAttributesInternal(RuntimePropertyInfo target)
		{
			return CustomAttributeData.GetCustomAttributes(target.GetRuntimeModule(), target.MetadataToken);
		}

		// Token: 0x06004532 RID: 17714 RVA: 0x000FDF65 File Offset: 0x000FC165
		[SecuritySafeCritical]
		internal static IList<CustomAttributeData> GetCustomAttributesInternal(RuntimeModule target)
		{
			if (target.IsResource())
			{
				return new List<CustomAttributeData>();
			}
			return CustomAttributeData.GetCustomAttributes(target, target.MetadataToken);
		}

		// Token: 0x06004533 RID: 17715 RVA: 0x000FDF84 File Offset: 0x000FC184
		[SecuritySafeCritical]
		internal static IList<CustomAttributeData> GetCustomAttributesInternal(RuntimeAssembly target)
		{
			IList<CustomAttributeData> customAttributes = CustomAttributeData.GetCustomAttributes((RuntimeModule)target.ManifestModule, RuntimeAssembly.GetToken(target.GetNativeHandle()));
			int num = 0;
			Attribute[] customAttributes2 = PseudoCustomAttribute.GetCustomAttributes(target, typeof(object) as RuntimeType, false, out num);
			if (num == 0)
			{
				return customAttributes;
			}
			CustomAttributeData[] array = new CustomAttributeData[customAttributes.Count + num];
			customAttributes.CopyTo(array, num);
			for (int i = 0; i < num; i++)
			{
				array[i] = new CustomAttributeData(customAttributes2[i]);
			}
			return Array.AsReadOnly<CustomAttributeData>(array);
		}

		// Token: 0x06004534 RID: 17716 RVA: 0x000FE008 File Offset: 0x000FC208
		[SecuritySafeCritical]
		internal static IList<CustomAttributeData> GetCustomAttributesInternal(RuntimeParameterInfo target)
		{
			IList<CustomAttributeData> customAttributes = CustomAttributeData.GetCustomAttributes(target.GetRuntimeModule(), target.MetadataToken);
			int num = 0;
			Attribute[] customAttributes2 = PseudoCustomAttribute.GetCustomAttributes(target, typeof(object) as RuntimeType, out num);
			if (num == 0)
			{
				return customAttributes;
			}
			CustomAttributeData[] array = new CustomAttributeData[customAttributes.Count + num];
			customAttributes.CopyTo(array, num);
			for (int i = 0; i < num; i++)
			{
				array[i] = new CustomAttributeData(customAttributes2[i]);
			}
			return Array.AsReadOnly<CustomAttributeData>(array);
		}

		// Token: 0x06004535 RID: 17717 RVA: 0x000FE080 File Offset: 0x000FC280
		private static CustomAttributeEncoding TypeToCustomAttributeEncoding(RuntimeType type)
		{
			if (type == (RuntimeType)typeof(int))
			{
				return CustomAttributeEncoding.Int32;
			}
			if (type.IsEnum)
			{
				return CustomAttributeEncoding.Enum;
			}
			if (type == (RuntimeType)typeof(string))
			{
				return CustomAttributeEncoding.String;
			}
			if (type == (RuntimeType)typeof(Type))
			{
				return CustomAttributeEncoding.Type;
			}
			if (type == (RuntimeType)typeof(object))
			{
				return CustomAttributeEncoding.Object;
			}
			if (type.IsArray)
			{
				return CustomAttributeEncoding.Array;
			}
			if (type == (RuntimeType)typeof(char))
			{
				return CustomAttributeEncoding.Char;
			}
			if (type == (RuntimeType)typeof(bool))
			{
				return CustomAttributeEncoding.Boolean;
			}
			if (type == (RuntimeType)typeof(byte))
			{
				return CustomAttributeEncoding.Byte;
			}
			if (type == (RuntimeType)typeof(sbyte))
			{
				return CustomAttributeEncoding.SByte;
			}
			if (type == (RuntimeType)typeof(short))
			{
				return CustomAttributeEncoding.Int16;
			}
			if (type == (RuntimeType)typeof(ushort))
			{
				return CustomAttributeEncoding.UInt16;
			}
			if (type == (RuntimeType)typeof(uint))
			{
				return CustomAttributeEncoding.UInt32;
			}
			if (type == (RuntimeType)typeof(long))
			{
				return CustomAttributeEncoding.Int64;
			}
			if (type == (RuntimeType)typeof(ulong))
			{
				return CustomAttributeEncoding.UInt64;
			}
			if (type == (RuntimeType)typeof(float))
			{
				return CustomAttributeEncoding.Float;
			}
			if (type == (RuntimeType)typeof(double))
			{
				return CustomAttributeEncoding.Double;
			}
			if (type == (RuntimeType)typeof(Enum))
			{
				return CustomAttributeEncoding.Object;
			}
			if (type.IsClass)
			{
				return CustomAttributeEncoding.Object;
			}
			if (type.IsInterface)
			{
				return CustomAttributeEncoding.Object;
			}
			if (type.IsValueType)
			{
				return CustomAttributeEncoding.Undefined;
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_InvalidKindOfTypeForCA"), "type");
		}

		// Token: 0x06004536 RID: 17718 RVA: 0x000FE270 File Offset: 0x000FC470
		private static CustomAttributeType InitCustomAttributeType(RuntimeType parameterType)
		{
			CustomAttributeEncoding customAttributeEncoding = CustomAttributeData.TypeToCustomAttributeEncoding(parameterType);
			CustomAttributeEncoding customAttributeEncoding2 = CustomAttributeEncoding.Undefined;
			CustomAttributeEncoding encodedEnumType = CustomAttributeEncoding.Undefined;
			string enumName = null;
			if (customAttributeEncoding == CustomAttributeEncoding.Array)
			{
				parameterType = (RuntimeType)parameterType.GetElementType();
				customAttributeEncoding2 = CustomAttributeData.TypeToCustomAttributeEncoding(parameterType);
			}
			if (customAttributeEncoding == CustomAttributeEncoding.Enum || customAttributeEncoding2 == CustomAttributeEncoding.Enum)
			{
				encodedEnumType = CustomAttributeData.TypeToCustomAttributeEncoding((RuntimeType)Enum.GetUnderlyingType(parameterType));
				enumName = parameterType.AssemblyQualifiedName;
			}
			return new CustomAttributeType(customAttributeEncoding, customAttributeEncoding2, encodedEnumType, enumName);
		}

		// Token: 0x06004537 RID: 17719 RVA: 0x000FE2D0 File Offset: 0x000FC4D0
		[SecurityCritical]
		private static IList<CustomAttributeData> GetCustomAttributes(RuntimeModule module, int tkTarget)
		{
			CustomAttributeRecord[] customAttributeRecords = CustomAttributeData.GetCustomAttributeRecords(module, tkTarget);
			CustomAttributeData[] array = new CustomAttributeData[customAttributeRecords.Length];
			for (int i = 0; i < customAttributeRecords.Length; i++)
			{
				array[i] = new CustomAttributeData(module, customAttributeRecords[i]);
			}
			return Array.AsReadOnly<CustomAttributeData>(array);
		}

		// Token: 0x06004538 RID: 17720 RVA: 0x000FE314 File Offset: 0x000FC514
		[SecurityCritical]
		internal static CustomAttributeRecord[] GetCustomAttributeRecords(RuntimeModule module, int targetToken)
		{
			MetadataImport metadataImport = module.MetadataImport;
			MetadataEnumResult metadataEnumResult;
			metadataImport.EnumCustomAttributes(targetToken, out metadataEnumResult);
			CustomAttributeRecord[] array = new CustomAttributeRecord[metadataEnumResult.Length];
			for (int i = 0; i < array.Length; i++)
			{
				metadataImport.GetCustomAttributeProps(metadataEnumResult[i], out array[i].tkCtor.Value, out array[i].blob);
			}
			return array;
		}

		// Token: 0x06004539 RID: 17721 RVA: 0x000FE37C File Offset: 0x000FC57C
		internal static CustomAttributeTypedArgument Filter(IList<CustomAttributeData> attrs, Type caType, int parameter)
		{
			for (int i = 0; i < attrs.Count; i++)
			{
				if (attrs[i].Constructor.DeclaringType == caType)
				{
					return attrs[i].ConstructorArguments[parameter];
				}
			}
			return default(CustomAttributeTypedArgument);
		}

		// Token: 0x0600453A RID: 17722 RVA: 0x000FE3CF File Offset: 0x000FC5CF
		protected CustomAttributeData()
		{
		}

		// Token: 0x0600453B RID: 17723 RVA: 0x000FE3D8 File Offset: 0x000FC5D8
		[SecuritySafeCritical]
		private CustomAttributeData(RuntimeModule scope, CustomAttributeRecord caRecord)
		{
			this.m_scope = scope;
			this.m_ctor = (RuntimeConstructorInfo)RuntimeType.GetMethodBase(scope, caRecord.tkCtor);
			ParameterInfo[] parametersNoCopy = this.m_ctor.GetParametersNoCopy();
			this.m_ctorParams = new CustomAttributeCtorParameter[parametersNoCopy.Length];
			for (int i = 0; i < parametersNoCopy.Length; i++)
			{
				this.m_ctorParams[i] = new CustomAttributeCtorParameter(CustomAttributeData.InitCustomAttributeType((RuntimeType)parametersNoCopy[i].ParameterType));
			}
			FieldInfo[] fields = this.m_ctor.DeclaringType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			PropertyInfo[] properties = this.m_ctor.DeclaringType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			this.m_namedParams = new CustomAttributeNamedParameter[properties.Length + fields.Length];
			for (int j = 0; j < fields.Length; j++)
			{
				this.m_namedParams[j] = new CustomAttributeNamedParameter(fields[j].Name, CustomAttributeEncoding.Field, CustomAttributeData.InitCustomAttributeType((RuntimeType)fields[j].FieldType));
			}
			for (int k = 0; k < properties.Length; k++)
			{
				this.m_namedParams[k + fields.Length] = new CustomAttributeNamedParameter(properties[k].Name, CustomAttributeEncoding.Property, CustomAttributeData.InitCustomAttributeType((RuntimeType)properties[k].PropertyType));
			}
			this.m_members = new MemberInfo[fields.Length + properties.Length];
			fields.CopyTo(this.m_members, 0);
			properties.CopyTo(this.m_members, fields.Length);
			CustomAttributeEncodedArgument.ParseAttributeArguments(caRecord.blob, ref this.m_ctorParams, ref this.m_namedParams, this.m_scope);
		}

		// Token: 0x0600453C RID: 17724 RVA: 0x000FE564 File Offset: 0x000FC764
		internal CustomAttributeData(Attribute attribute)
		{
			if (attribute is DllImportAttribute)
			{
				this.Init((DllImportAttribute)attribute);
				return;
			}
			if (attribute is FieldOffsetAttribute)
			{
				this.Init((FieldOffsetAttribute)attribute);
				return;
			}
			if (attribute is MarshalAsAttribute)
			{
				this.Init((MarshalAsAttribute)attribute);
				return;
			}
			if (attribute is TypeForwardedToAttribute)
			{
				this.Init((TypeForwardedToAttribute)attribute);
				return;
			}
			this.Init(attribute);
		}

		// Token: 0x0600453D RID: 17725 RVA: 0x000FE5D4 File Offset: 0x000FC7D4
		private void Init(DllImportAttribute dllImport)
		{
			Type typeFromHandle = typeof(DllImportAttribute);
			this.m_ctor = typeFromHandle.GetConstructors(BindingFlags.Instance | BindingFlags.Public)[0];
			this.m_typedCtorArgs = Array.AsReadOnly<CustomAttributeTypedArgument>(new CustomAttributeTypedArgument[]
			{
				new CustomAttributeTypedArgument(dllImport.Value)
			});
			this.m_namedArgs = Array.AsReadOnly<CustomAttributeNamedArgument>(new CustomAttributeNamedArgument[]
			{
				new CustomAttributeNamedArgument(typeFromHandle.GetField("EntryPoint"), dllImport.EntryPoint),
				new CustomAttributeNamedArgument(typeFromHandle.GetField("CharSet"), dllImport.CharSet),
				new CustomAttributeNamedArgument(typeFromHandle.GetField("ExactSpelling"), dllImport.ExactSpelling),
				new CustomAttributeNamedArgument(typeFromHandle.GetField("SetLastError"), dllImport.SetLastError),
				new CustomAttributeNamedArgument(typeFromHandle.GetField("PreserveSig"), dllImport.PreserveSig),
				new CustomAttributeNamedArgument(typeFromHandle.GetField("CallingConvention"), dllImport.CallingConvention),
				new CustomAttributeNamedArgument(typeFromHandle.GetField("BestFitMapping"), dllImport.BestFitMapping),
				new CustomAttributeNamedArgument(typeFromHandle.GetField("ThrowOnUnmappableChar"), dllImport.ThrowOnUnmappableChar)
			});
		}

		// Token: 0x0600453E RID: 17726 RVA: 0x000FE73C File Offset: 0x000FC93C
		private void Init(FieldOffsetAttribute fieldOffset)
		{
			this.m_ctor = typeof(FieldOffsetAttribute).GetConstructors(BindingFlags.Instance | BindingFlags.Public)[0];
			this.m_typedCtorArgs = Array.AsReadOnly<CustomAttributeTypedArgument>(new CustomAttributeTypedArgument[]
			{
				new CustomAttributeTypedArgument(fieldOffset.Value)
			});
			this.m_namedArgs = Array.AsReadOnly<CustomAttributeNamedArgument>(new CustomAttributeNamedArgument[0]);
		}

		// Token: 0x0600453F RID: 17727 RVA: 0x000FE79C File Offset: 0x000FC99C
		private void Init(MarshalAsAttribute marshalAs)
		{
			Type typeFromHandle = typeof(MarshalAsAttribute);
			this.m_ctor = typeFromHandle.GetConstructors(BindingFlags.Instance | BindingFlags.Public)[0];
			this.m_typedCtorArgs = Array.AsReadOnly<CustomAttributeTypedArgument>(new CustomAttributeTypedArgument[]
			{
				new CustomAttributeTypedArgument(marshalAs.Value)
			});
			int num = 3;
			if (marshalAs.MarshalType != null)
			{
				num++;
			}
			if (marshalAs.MarshalTypeRef != null)
			{
				num++;
			}
			if (marshalAs.MarshalCookie != null)
			{
				num++;
			}
			num++;
			num++;
			if (marshalAs.SafeArrayUserDefinedSubType != null)
			{
				num++;
			}
			CustomAttributeNamedArgument[] array = new CustomAttributeNamedArgument[num];
			num = 0;
			array[num++] = new CustomAttributeNamedArgument(typeFromHandle.GetField("ArraySubType"), marshalAs.ArraySubType);
			array[num++] = new CustomAttributeNamedArgument(typeFromHandle.GetField("SizeParamIndex"), marshalAs.SizeParamIndex);
			array[num++] = new CustomAttributeNamedArgument(typeFromHandle.GetField("SizeConst"), marshalAs.SizeConst);
			array[num++] = new CustomAttributeNamedArgument(typeFromHandle.GetField("IidParameterIndex"), marshalAs.IidParameterIndex);
			array[num++] = new CustomAttributeNamedArgument(typeFromHandle.GetField("SafeArraySubType"), marshalAs.SafeArraySubType);
			if (marshalAs.MarshalType != null)
			{
				array[num++] = new CustomAttributeNamedArgument(typeFromHandle.GetField("MarshalType"), marshalAs.MarshalType);
			}
			if (marshalAs.MarshalTypeRef != null)
			{
				array[num++] = new CustomAttributeNamedArgument(typeFromHandle.GetField("MarshalTypeRef"), marshalAs.MarshalTypeRef);
			}
			if (marshalAs.MarshalCookie != null)
			{
				array[num++] = new CustomAttributeNamedArgument(typeFromHandle.GetField("MarshalCookie"), marshalAs.MarshalCookie);
			}
			if (marshalAs.SafeArrayUserDefinedSubType != null)
			{
				array[num++] = new CustomAttributeNamedArgument(typeFromHandle.GetField("SafeArrayUserDefinedSubType"), marshalAs.SafeArrayUserDefinedSubType);
			}
			this.m_namedArgs = Array.AsReadOnly<CustomAttributeNamedArgument>(array);
		}

		// Token: 0x06004540 RID: 17728 RVA: 0x000FE9B8 File Offset: 0x000FCBB8
		private void Init(TypeForwardedToAttribute forwardedTo)
		{
			Type typeFromHandle = typeof(TypeForwardedToAttribute);
			Type[] types = new Type[]
			{
				typeof(Type)
			};
			this.m_ctor = typeFromHandle.GetConstructor(BindingFlags.Instance | BindingFlags.Public, null, types, null);
			this.m_typedCtorArgs = Array.AsReadOnly<CustomAttributeTypedArgument>(new CustomAttributeTypedArgument[]
			{
				new CustomAttributeTypedArgument(typeof(Type), forwardedTo.Destination)
			});
			CustomAttributeNamedArgument[] array = new CustomAttributeNamedArgument[0];
			this.m_namedArgs = Array.AsReadOnly<CustomAttributeNamedArgument>(array);
		}

		// Token: 0x06004541 RID: 17729 RVA: 0x000FEA37 File Offset: 0x000FCC37
		private void Init(object pca)
		{
			this.m_ctor = pca.GetType().GetConstructors(BindingFlags.Instance | BindingFlags.Public)[0];
			this.m_typedCtorArgs = Array.AsReadOnly<CustomAttributeTypedArgument>(new CustomAttributeTypedArgument[0]);
			this.m_namedArgs = Array.AsReadOnly<CustomAttributeNamedArgument>(new CustomAttributeNamedArgument[0]);
		}

		// Token: 0x06004542 RID: 17730 RVA: 0x000FEA70 File Offset: 0x000FCC70
		public override string ToString()
		{
			string text = "";
			for (int i = 0; i < this.ConstructorArguments.Count; i++)
			{
				text += string.Format(CultureInfo.CurrentCulture, (i == 0) ? "{0}" : ", {0}", this.ConstructorArguments[i]);
			}
			string text2 = "";
			for (int j = 0; j < this.NamedArguments.Count; j++)
			{
				text2 += string.Format(CultureInfo.CurrentCulture, (j == 0 && text.Length == 0) ? "{0}" : ", {0}", this.NamedArguments[j]);
			}
			return string.Format(CultureInfo.CurrentCulture, "[{0}({1}{2})]", this.Constructor.DeclaringType.FullName, text, text2);
		}

		// Token: 0x06004543 RID: 17731 RVA: 0x000FEB40 File Offset: 0x000FCD40
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06004544 RID: 17732 RVA: 0x000FEB48 File Offset: 0x000FCD48
		public override bool Equals(object obj)
		{
			return obj == this;
		}

		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x06004545 RID: 17733 RVA: 0x000FEB4E File Offset: 0x000FCD4E
		[__DynamicallyInvokable]
		public Type AttributeType
		{
			[__DynamicallyInvokable]
			get
			{
				return this.Constructor.DeclaringType;
			}
		}

		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x06004546 RID: 17734 RVA: 0x000FEB5B File Offset: 0x000FCD5B
		[ComVisible(true)]
		public virtual ConstructorInfo Constructor
		{
			get
			{
				return this.m_ctor;
			}
		}

		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x06004547 RID: 17735 RVA: 0x000FEB64 File Offset: 0x000FCD64
		[ComVisible(true)]
		[__DynamicallyInvokable]
		public virtual IList<CustomAttributeTypedArgument> ConstructorArguments
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.m_typedCtorArgs == null)
				{
					CustomAttributeTypedArgument[] array = new CustomAttributeTypedArgument[this.m_ctorParams.Length];
					for (int i = 0; i < array.Length; i++)
					{
						CustomAttributeEncodedArgument customAttributeEncodedArgument = this.m_ctorParams[i].CustomAttributeEncodedArgument;
						array[i] = new CustomAttributeTypedArgument(this.m_scope, this.m_ctorParams[i].CustomAttributeEncodedArgument);
					}
					this.m_typedCtorArgs = Array.AsReadOnly<CustomAttributeTypedArgument>(array);
				}
				return this.m_typedCtorArgs;
			}
		}

		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x06004548 RID: 17736 RVA: 0x000FEBDC File Offset: 0x000FCDDC
		[__DynamicallyInvokable]
		public virtual IList<CustomAttributeNamedArgument> NamedArguments
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.m_namedArgs == null)
				{
					if (this.m_namedParams == null)
					{
						return null;
					}
					int num = 0;
					for (int i = 0; i < this.m_namedParams.Length; i++)
					{
						if (this.m_namedParams[i].EncodedArgument.CustomAttributeType.EncodedType != CustomAttributeEncoding.Undefined)
						{
							num++;
						}
					}
					CustomAttributeNamedArgument[] array = new CustomAttributeNamedArgument[num];
					int j = 0;
					int num2 = 0;
					while (j < this.m_namedParams.Length)
					{
						if (this.m_namedParams[j].EncodedArgument.CustomAttributeType.EncodedType != CustomAttributeEncoding.Undefined)
						{
							array[num2++] = new CustomAttributeNamedArgument(this.m_members[j], new CustomAttributeTypedArgument(this.m_scope, this.m_namedParams[j].EncodedArgument));
						}
						j++;
					}
					this.m_namedArgs = Array.AsReadOnly<CustomAttributeNamedArgument>(array);
				}
				return this.m_namedArgs;
			}
		}

		// Token: 0x04001C5A RID: 7258
		private ConstructorInfo m_ctor;

		// Token: 0x04001C5B RID: 7259
		private RuntimeModule m_scope;

		// Token: 0x04001C5C RID: 7260
		private MemberInfo[] m_members;

		// Token: 0x04001C5D RID: 7261
		private CustomAttributeCtorParameter[] m_ctorParams;

		// Token: 0x04001C5E RID: 7262
		private CustomAttributeNamedParameter[] m_namedParams;

		// Token: 0x04001C5F RID: 7263
		private IList<CustomAttributeTypedArgument> m_typedCtorArgs;

		// Token: 0x04001C60 RID: 7264
		private IList<CustomAttributeNamedArgument> m_namedArgs;
	}
}
