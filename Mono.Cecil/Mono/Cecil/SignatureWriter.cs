using System;
using System.Text;
using Mono.Cecil.Metadata;
using Mono.Cecil.PE;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x0200008E RID: 142
	internal sealed class SignatureWriter : ByteBuffer
	{
		// Token: 0x060004BE RID: 1214 RVA: 0x00014030 File Offset: 0x00012230
		public SignatureWriter(MetadataBuilder metadata) : base(6)
		{
			this.metadata = metadata;
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x00014040 File Offset: 0x00012240
		public void WriteElementType(ElementType element_type)
		{
			base.WriteByte((byte)element_type);
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0001404C File Offset: 0x0001224C
		public void WriteUTF8String(string @string)
		{
			if (@string == null)
			{
				base.WriteByte(byte.MaxValue);
				return;
			}
			byte[] bytes = Encoding.UTF8.GetBytes(@string);
			base.WriteCompressedUInt32((uint)bytes.Length);
			base.WriteBytes(bytes);
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00014084 File Offset: 0x00012284
		public void WriteMethodSignature(IMethodSignature method)
		{
			byte b = (byte)method.CallingConvention;
			if (method.HasThis)
			{
				b |= 32;
			}
			if (method.ExplicitThis)
			{
				b |= 64;
			}
			IGenericParameterProvider genericParameterProvider = method as IGenericParameterProvider;
			int num = (genericParameterProvider != null && genericParameterProvider.HasGenericParameters) ? genericParameterProvider.GenericParameters.Count : 0;
			if (num > 0)
			{
				b |= 16;
			}
			int num2 = method.HasParameters ? method.Parameters.Count : 0;
			base.WriteByte(b);
			if (num > 0)
			{
				base.WriteCompressedUInt32((uint)num);
			}
			base.WriteCompressedUInt32((uint)num2);
			this.WriteTypeSignature(method.ReturnType);
			if (num2 == 0)
			{
				return;
			}
			Collection<ParameterDefinition> parameters = method.Parameters;
			for (int i = 0; i < num2; i++)
			{
				this.WriteTypeSignature(parameters[i].ParameterType);
			}
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x0001414B File Offset: 0x0001234B
		private uint MakeTypeDefOrRefCodedRID(TypeReference type)
		{
			return CodedIndex.TypeDefOrRef.CompressMetadataToken(this.metadata.LookupToken(type));
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x00014160 File Offset: 0x00012360
		public void WriteTypeSignature(TypeReference type)
		{
			if (type == null)
			{
				throw new ArgumentNullException();
			}
			ElementType etype = type.etype;
			ElementType elementType = etype;
			if (elementType <= ElementType.GenericInst)
			{
				if (elementType == ElementType.None)
				{
					this.WriteElementType(type.IsValueType ? ElementType.ValueType : ElementType.Class);
					base.WriteCompressedUInt32(this.MakeTypeDefOrRefCodedRID(type));
					return;
				}
				switch (elementType)
				{
				case ElementType.Ptr:
				case ElementType.ByRef:
					goto IL_E3;
				case ElementType.ValueType:
				case ElementType.Class:
					goto IL_17D;
				case ElementType.Var:
					break;
				case ElementType.Array:
				{
					ArrayType arrayType = (ArrayType)type;
					if (!arrayType.IsVector)
					{
						this.WriteArrayTypeSignature(arrayType);
						return;
					}
					this.WriteElementType(ElementType.SzArray);
					this.WriteTypeSignature(arrayType.ElementType);
					return;
				}
				case ElementType.GenericInst:
				{
					GenericInstanceType genericInstanceType = (GenericInstanceType)type;
					this.WriteElementType(ElementType.GenericInst);
					this.WriteElementType(genericInstanceType.IsValueType ? ElementType.ValueType : ElementType.Class);
					base.WriteCompressedUInt32(this.MakeTypeDefOrRefCodedRID(genericInstanceType.ElementType));
					this.WriteGenericInstanceSignature(genericInstanceType);
					return;
				}
				default:
					goto IL_17D;
				}
			}
			else
			{
				switch (elementType)
				{
				case ElementType.FnPtr:
				{
					FunctionPointerType method = (FunctionPointerType)type;
					this.WriteElementType(ElementType.FnPtr);
					this.WriteMethodSignature(method);
					return;
				}
				case ElementType.Object:
				case ElementType.SzArray:
					goto IL_17D;
				case ElementType.MVar:
					break;
				case ElementType.CModReqD:
				case ElementType.CModOpt:
				{
					IModifierType type2 = (IModifierType)type;
					this.WriteModifierSignature(etype, type2);
					return;
				}
				default:
					if (elementType != ElementType.Sentinel && elementType != ElementType.Pinned)
					{
						goto IL_17D;
					}
					goto IL_E3;
				}
			}
			GenericParameter genericParameter = (GenericParameter)type;
			this.WriteElementType(etype);
			int position = genericParameter.Position;
			if (position == -1)
			{
				throw new NotSupportedException();
			}
			base.WriteCompressedUInt32((uint)position);
			return;
			IL_E3:
			TypeSpecification typeSpecification = (TypeSpecification)type;
			this.WriteElementType(etype);
			this.WriteTypeSignature(typeSpecification.ElementType);
			return;
			IL_17D:
			if (!this.TryWriteElementType(type))
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x000142FC File Offset: 0x000124FC
		private void WriteArrayTypeSignature(ArrayType array)
		{
			this.WriteElementType(ElementType.Array);
			this.WriteTypeSignature(array.ElementType);
			Collection<ArrayDimension> dimensions = array.Dimensions;
			int count = dimensions.Count;
			base.WriteCompressedUInt32((uint)count);
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < count; i++)
			{
				ArrayDimension arrayDimension = dimensions[i];
				if (arrayDimension.UpperBound != null)
				{
					num++;
					num2++;
				}
				else if (arrayDimension.LowerBound != null)
				{
					num2++;
				}
			}
			int[] array2 = new int[num];
			int[] array3 = new int[num2];
			for (int j = 0; j < num2; j++)
			{
				ArrayDimension arrayDimension2 = dimensions[j];
				array3[j] = arrayDimension2.LowerBound.GetValueOrDefault();
				if (arrayDimension2.UpperBound != null)
				{
					array2[j] = arrayDimension2.UpperBound.Value - array3[j] + 1;
				}
			}
			base.WriteCompressedUInt32((uint)num);
			for (int k = 0; k < num; k++)
			{
				base.WriteCompressedUInt32((uint)array2[k]);
			}
			base.WriteCompressedUInt32((uint)num2);
			for (int l = 0; l < num2; l++)
			{
				base.WriteCompressedInt32(array3[l]);
			}
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x00014434 File Offset: 0x00012634
		public void WriteGenericInstanceSignature(IGenericInstance instance)
		{
			Collection<TypeReference> genericArguments = instance.GenericArguments;
			int count = genericArguments.Count;
			base.WriteCompressedUInt32((uint)count);
			for (int i = 0; i < count; i++)
			{
				this.WriteTypeSignature(genericArguments[i]);
			}
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0001446F File Offset: 0x0001266F
		private void WriteModifierSignature(ElementType element_type, IModifierType type)
		{
			this.WriteElementType(element_type);
			base.WriteCompressedUInt32(this.MakeTypeDefOrRefCodedRID(type.ModifierType));
			this.WriteTypeSignature(type.ElementType);
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x00014498 File Offset: 0x00012698
		private bool TryWriteElementType(TypeReference type)
		{
			ElementType etype = type.etype;
			if (etype == ElementType.None)
			{
				return false;
			}
			this.WriteElementType(etype);
			return true;
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x000144B9 File Offset: 0x000126B9
		public void WriteConstantString(string value)
		{
			base.WriteBytes(Encoding.Unicode.GetBytes(value));
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x000144CC File Offset: 0x000126CC
		public void WriteConstantPrimitive(object value)
		{
			this.WritePrimitiveValue(value);
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x000144D8 File Offset: 0x000126D8
		public void WriteCustomAttributeConstructorArguments(CustomAttribute attribute)
		{
			if (!attribute.HasConstructorArguments)
			{
				return;
			}
			Collection<CustomAttributeArgument> constructorArguments = attribute.ConstructorArguments;
			Collection<ParameterDefinition> parameters = attribute.Constructor.Parameters;
			if (parameters.Count != constructorArguments.Count)
			{
				throw new InvalidOperationException();
			}
			for (int i = 0; i < constructorArguments.Count; i++)
			{
				this.WriteCustomAttributeFixedArgument(parameters[i].ParameterType, constructorArguments[i]);
			}
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x0001453F File Offset: 0x0001273F
		private void WriteCustomAttributeFixedArgument(TypeReference type, CustomAttributeArgument argument)
		{
			if (type.IsArray)
			{
				this.WriteCustomAttributeFixedArrayArgument((ArrayType)type, argument);
				return;
			}
			this.WriteCustomAttributeElement(type, argument);
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x00014560 File Offset: 0x00012760
		private void WriteCustomAttributeFixedArrayArgument(ArrayType type, CustomAttributeArgument argument)
		{
			CustomAttributeArgument[] array = argument.Value as CustomAttributeArgument[];
			if (array == null)
			{
				base.WriteUInt32(uint.MaxValue);
				return;
			}
			base.WriteInt32(array.Length);
			if (array.Length == 0)
			{
				return;
			}
			TypeReference elementType = type.ElementType;
			for (int i = 0; i < array.Length; i++)
			{
				this.WriteCustomAttributeElement(elementType, array[i]);
			}
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x000145BC File Offset: 0x000127BC
		private void WriteCustomAttributeElement(TypeReference type, CustomAttributeArgument argument)
		{
			if (type.IsArray)
			{
				this.WriteCustomAttributeFixedArrayArgument((ArrayType)type, argument);
				return;
			}
			if (type.etype == ElementType.Object)
			{
				argument = (CustomAttributeArgument)argument.Value;
				type = argument.Type;
				this.WriteCustomAttributeFieldOrPropType(type);
				this.WriteCustomAttributeElement(type, argument);
				return;
			}
			this.WriteCustomAttributeValue(type, argument.Value);
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x00014620 File Offset: 0x00012820
		private void WriteCustomAttributeValue(TypeReference type, object value)
		{
			ElementType etype = type.etype;
			ElementType elementType = etype;
			if (elementType != ElementType.None)
			{
				if (elementType != ElementType.String)
				{
					this.WritePrimitiveValue(value);
					return;
				}
				string text = (string)value;
				if (text == null)
				{
					base.WriteByte(byte.MaxValue);
					return;
				}
				this.WriteUTF8String(text);
				return;
			}
			else
			{
				if (type.IsTypeOf("System", "Type"))
				{
					this.WriteTypeReference((TypeReference)value);
					return;
				}
				this.WriteCustomAttributeEnumValue(type, value);
				return;
			}
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x0001468C File Offset: 0x0001288C
		private void WritePrimitiveValue(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException();
			}
			switch (Type.GetTypeCode(value.GetType()))
			{
			case TypeCode.Boolean:
				base.WriteByte(((bool)value) ? 1 : 0);
				return;
			case TypeCode.Char:
				base.WriteInt16((short)((char)value));
				return;
			case TypeCode.SByte:
				base.WriteSByte((sbyte)value);
				return;
			case TypeCode.Byte:
				base.WriteByte((byte)value);
				return;
			case TypeCode.Int16:
				base.WriteInt16((short)value);
				return;
			case TypeCode.UInt16:
				base.WriteUInt16((ushort)value);
				return;
			case TypeCode.Int32:
				base.WriteInt32((int)value);
				return;
			case TypeCode.UInt32:
				base.WriteUInt32((uint)value);
				return;
			case TypeCode.Int64:
				base.WriteInt64((long)value);
				return;
			case TypeCode.UInt64:
				base.WriteUInt64((ulong)value);
				return;
			case TypeCode.Single:
				base.WriteSingle((float)value);
				return;
			case TypeCode.Double:
				base.WriteDouble((double)value);
				return;
			default:
				throw new NotSupportedException(value.GetType().FullName);
			}
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x000147A0 File Offset: 0x000129A0
		private void WriteCustomAttributeEnumValue(TypeReference enum_type, object value)
		{
			TypeDefinition typeDefinition = enum_type.CheckedResolve();
			if (!typeDefinition.IsEnum)
			{
				throw new ArgumentException();
			}
			this.WriteCustomAttributeValue(typeDefinition.GetEnumUnderlyingType(), value);
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x000147D0 File Offset: 0x000129D0
		private void WriteCustomAttributeFieldOrPropType(TypeReference type)
		{
			if (type.IsArray)
			{
				ArrayType arrayType = (ArrayType)type;
				this.WriteElementType(ElementType.SzArray);
				this.WriteCustomAttributeFieldOrPropType(arrayType.ElementType);
				return;
			}
			ElementType etype = type.etype;
			ElementType elementType = etype;
			if (elementType != ElementType.None)
			{
				if (elementType == ElementType.Object)
				{
					this.WriteElementType(ElementType.Boxed);
					return;
				}
				this.WriteElementType(etype);
				return;
			}
			else
			{
				if (type.IsTypeOf("System", "Type"))
				{
					this.WriteElementType(ElementType.Type);
					return;
				}
				this.WriteElementType(ElementType.Enum);
				this.WriteTypeReference(type);
				return;
			}
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x00014850 File Offset: 0x00012A50
		public void WriteCustomAttributeNamedArguments(CustomAttribute attribute)
		{
			int namedArgumentCount = SignatureWriter.GetNamedArgumentCount(attribute);
			base.WriteUInt16((ushort)namedArgumentCount);
			if (namedArgumentCount == 0)
			{
				return;
			}
			this.WriteICustomAttributeNamedArguments(attribute);
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x00014878 File Offset: 0x00012A78
		private static int GetNamedArgumentCount(ICustomAttribute attribute)
		{
			int num = 0;
			if (attribute.HasFields)
			{
				num += attribute.Fields.Count;
			}
			if (attribute.HasProperties)
			{
				num += attribute.Properties.Count;
			}
			return num;
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x000148B4 File Offset: 0x00012AB4
		private void WriteICustomAttributeNamedArguments(ICustomAttribute attribute)
		{
			if (attribute.HasFields)
			{
				this.WriteCustomAttributeNamedArguments(83, attribute.Fields);
			}
			if (attribute.HasProperties)
			{
				this.WriteCustomAttributeNamedArguments(84, attribute.Properties);
			}
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x000148E4 File Offset: 0x00012AE4
		private void WriteCustomAttributeNamedArguments(byte kind, Collection<CustomAttributeNamedArgument> named_arguments)
		{
			for (int i = 0; i < named_arguments.Count; i++)
			{
				this.WriteCustomAttributeNamedArgument(kind, named_arguments[i]);
			}
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x00014910 File Offset: 0x00012B10
		private void WriteCustomAttributeNamedArgument(byte kind, CustomAttributeNamedArgument named_argument)
		{
			CustomAttributeArgument argument = named_argument.Argument;
			base.WriteByte(kind);
			this.WriteCustomAttributeFieldOrPropType(argument.Type);
			this.WriteUTF8String(named_argument.Name);
			this.WriteCustomAttributeFixedArgument(argument.Type, argument);
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x00014954 File Offset: 0x00012B54
		private void WriteSecurityAttribute(SecurityAttribute attribute)
		{
			this.WriteTypeReference(attribute.AttributeType);
			int namedArgumentCount = SignatureWriter.GetNamedArgumentCount(attribute);
			if (namedArgumentCount == 0)
			{
				base.WriteCompressedUInt32(1U);
				base.WriteCompressedUInt32(0U);
				return;
			}
			SignatureWriter signatureWriter = new SignatureWriter(this.metadata);
			signatureWriter.WriteCompressedUInt32((uint)namedArgumentCount);
			signatureWriter.WriteICustomAttributeNamedArguments(attribute);
			base.WriteCompressedUInt32((uint)signatureWriter.length);
			base.WriteBytes(signatureWriter);
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x000149B4 File Offset: 0x00012BB4
		public void WriteSecurityDeclaration(SecurityDeclaration declaration)
		{
			base.WriteByte(46);
			Collection<SecurityAttribute> security_attributes = declaration.security_attributes;
			if (security_attributes == null)
			{
				throw new NotSupportedException();
			}
			base.WriteCompressedUInt32((uint)security_attributes.Count);
			for (int i = 0; i < security_attributes.Count; i++)
			{
				this.WriteSecurityAttribute(security_attributes[i]);
			}
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00014A04 File Offset: 0x00012C04
		public void WriteXmlSecurityDeclaration(SecurityDeclaration declaration)
		{
			string xmlSecurityDeclaration = SignatureWriter.GetXmlSecurityDeclaration(declaration);
			if (xmlSecurityDeclaration == null)
			{
				throw new NotSupportedException();
			}
			base.WriteBytes(Encoding.Unicode.GetBytes(xmlSecurityDeclaration));
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x00014A34 File Offset: 0x00012C34
		private static string GetXmlSecurityDeclaration(SecurityDeclaration declaration)
		{
			if (declaration.security_attributes == null || declaration.security_attributes.Count != 1)
			{
				return null;
			}
			SecurityAttribute securityAttribute = declaration.security_attributes[0];
			if (!securityAttribute.AttributeType.IsTypeOf("System.Security.Permissions", "PermissionSetAttribute"))
			{
				return null;
			}
			if (securityAttribute.properties == null || securityAttribute.properties.Count != 1)
			{
				return null;
			}
			CustomAttributeNamedArgument customAttributeNamedArgument = securityAttribute.properties[0];
			if (customAttributeNamedArgument.Name != "XML")
			{
				return null;
			}
			return (string)customAttributeNamedArgument.Argument.Value;
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x00014ACD File Offset: 0x00012CCD
		private void WriteTypeReference(TypeReference type)
		{
			this.WriteUTF8String(TypeParser.ToParseable(type));
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x00014ADC File Offset: 0x00012CDC
		public void WriteMarshalInfo(MarshalInfo marshal_info)
		{
			this.WriteNativeType(marshal_info.native);
			NativeType native = marshal_info.native;
			if (native == NativeType.FixedSysString)
			{
				FixedSysStringMarshalInfo fixedSysStringMarshalInfo = (FixedSysStringMarshalInfo)marshal_info;
				if (fixedSysStringMarshalInfo.size > -1)
				{
					base.WriteCompressedUInt32((uint)fixedSysStringMarshalInfo.size);
				}
				return;
			}
			switch (native)
			{
			case NativeType.SafeArray:
			{
				SafeArrayMarshalInfo safeArrayMarshalInfo = (SafeArrayMarshalInfo)marshal_info;
				if (safeArrayMarshalInfo.element_type != VariantType.None)
				{
					this.WriteVariantType(safeArrayMarshalInfo.element_type);
				}
				return;
			}
			case NativeType.FixedArray:
			{
				FixedArrayMarshalInfo fixedArrayMarshalInfo = (FixedArrayMarshalInfo)marshal_info;
				if (fixedArrayMarshalInfo.size > -1)
				{
					base.WriteCompressedUInt32((uint)fixedArrayMarshalInfo.size);
				}
				if (fixedArrayMarshalInfo.element_type != NativeType.None)
				{
					this.WriteNativeType(fixedArrayMarshalInfo.element_type);
				}
				return;
			}
			default:
				switch (native)
				{
				case NativeType.Array:
				{
					ArrayMarshalInfo arrayMarshalInfo = (ArrayMarshalInfo)marshal_info;
					if (arrayMarshalInfo.element_type != NativeType.None)
					{
						this.WriteNativeType(arrayMarshalInfo.element_type);
					}
					if (arrayMarshalInfo.size_parameter_index > -1)
					{
						base.WriteCompressedUInt32((uint)arrayMarshalInfo.size_parameter_index);
					}
					if (arrayMarshalInfo.size > -1)
					{
						base.WriteCompressedUInt32((uint)arrayMarshalInfo.size);
					}
					if (arrayMarshalInfo.size_parameter_multiplier > -1)
					{
						base.WriteCompressedUInt32((uint)arrayMarshalInfo.size_parameter_multiplier);
					}
					return;
				}
				case NativeType.LPStruct:
					break;
				case NativeType.CustomMarshaler:
				{
					CustomMarshalInfo customMarshalInfo = (CustomMarshalInfo)marshal_info;
					this.WriteUTF8String((customMarshalInfo.guid != Guid.Empty) ? customMarshalInfo.guid.ToString() : string.Empty);
					this.WriteUTF8String(customMarshalInfo.unmanaged_type);
					this.WriteTypeReference(customMarshalInfo.managed_type);
					this.WriteUTF8String(customMarshalInfo.cookie);
					break;
				}
				default:
					return;
				}
				return;
			}
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00014C59 File Offset: 0x00012E59
		private void WriteNativeType(NativeType native)
		{
			base.WriteByte((byte)native);
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00014C63 File Offset: 0x00012E63
		private void WriteVariantType(VariantType variant)
		{
			base.WriteByte((byte)variant);
		}

		// Token: 0x040003DC RID: 988
		private readonly MetadataBuilder metadata;
	}
}
