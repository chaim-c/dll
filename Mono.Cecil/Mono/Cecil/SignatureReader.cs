using System;
using System.Text;
using Mono.Cecil.Metadata;
using Mono.Cecil.PE;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x02000064 RID: 100
	internal sealed class SignatureReader : ByteBuffer
	{
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x00010274 File Offset: 0x0000E474
		private TypeSystem TypeSystem
		{
			get
			{
				return this.reader.module.TypeSystem;
			}
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00010286 File Offset: 0x0000E486
		public SignatureReader(uint blob, MetadataReader reader) : base(reader.buffer)
		{
			this.reader = reader;
			this.MoveToBlob(blob);
			this.sig_length = base.ReadCompressedUInt32();
			this.start = (uint)this.position;
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x000102BA File Offset: 0x0000E4BA
		private void MoveToBlob(uint blob)
		{
			this.position = (int)(this.reader.image.BlobHeap.Offset + blob);
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x000102D9 File Offset: 0x0000E4D9
		private MetadataToken ReadTypeTokenSignature()
		{
			return CodedIndex.TypeDefOrRef.GetMetadataToken(base.ReadCompressedUInt32());
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x000102E8 File Offset: 0x0000E4E8
		private GenericParameter GetGenericParameter(GenericParameterType type, uint var)
		{
			IGenericContext context = this.reader.context;
			if (context == null)
			{
				return this.GetUnboundGenericParameter(type, (int)var);
			}
			IGenericParameterProvider genericParameterProvider;
			switch (type)
			{
			case GenericParameterType.Type:
				genericParameterProvider = context.Type;
				break;
			case GenericParameterType.Method:
				genericParameterProvider = context.Method;
				break;
			default:
				throw new NotSupportedException();
			}
			if (!context.IsDefinition)
			{
				SignatureReader.CheckGenericContext(genericParameterProvider, (int)var);
			}
			if (var >= (uint)genericParameterProvider.GenericParameters.Count)
			{
				return this.GetUnboundGenericParameter(type, (int)var);
			}
			return genericParameterProvider.GenericParameters[(int)var];
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0001036B File Offset: 0x0000E56B
		private GenericParameter GetUnboundGenericParameter(GenericParameterType type, int index)
		{
			return new GenericParameter(index, type, this.reader.module);
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x00010380 File Offset: 0x0000E580
		private static void CheckGenericContext(IGenericParameterProvider owner, int index)
		{
			Collection<GenericParameter> genericParameters = owner.GenericParameters;
			for (int i = genericParameters.Count; i <= index; i++)
			{
				genericParameters.Add(new GenericParameter(owner));
			}
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x000103B4 File Offset: 0x0000E5B4
		public void ReadGenericInstanceSignature(IGenericParameterProvider provider, IGenericInstance instance)
		{
			uint num = base.ReadCompressedUInt32();
			if (!provider.IsDefinition)
			{
				SignatureReader.CheckGenericContext(provider, (int)(num - 1U));
			}
			Collection<TypeReference> genericArguments = instance.GenericArguments;
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				genericArguments.Add(this.ReadTypeSignature());
				num2++;
			}
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x000103FC File Offset: 0x0000E5FC
		private ArrayType ReadArrayTypeSignature()
		{
			ArrayType arrayType = new ArrayType(this.ReadTypeSignature());
			uint num = base.ReadCompressedUInt32();
			uint[] array = new uint[base.ReadCompressedUInt32()];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = base.ReadCompressedUInt32();
			}
			int[] array2 = new int[base.ReadCompressedUInt32()];
			for (int j = 0; j < array2.Length; j++)
			{
				array2[j] = base.ReadCompressedInt32();
			}
			arrayType.Dimensions.Clear();
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				int? num3 = null;
				int? upperBound = null;
				if (num2 < array2.Length)
				{
					num3 = new int?(array2[num2]);
				}
				if (num2 < array.Length)
				{
					upperBound = num3 + (int)array[num2] - 1;
				}
				arrayType.Dimensions.Add(new ArrayDimension(num3, upperBound));
				num2++;
			}
			return arrayType;
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00010524 File Offset: 0x0000E724
		private TypeReference GetTypeDefOrRef(MetadataToken token)
		{
			return this.reader.GetTypeDefOrRef(token);
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00010532 File Offset: 0x0000E732
		public TypeReference ReadTypeSignature()
		{
			return this.ReadTypeSignature((ElementType)base.ReadByte());
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00010540 File Offset: 0x0000E740
		private TypeReference ReadTypeSignature(ElementType etype)
		{
			switch (etype)
			{
			case ElementType.Void:
				return this.TypeSystem.Void;
			case ElementType.Boolean:
			case ElementType.Char:
			case ElementType.I1:
			case ElementType.U1:
			case ElementType.I2:
			case ElementType.U2:
			case ElementType.I4:
			case ElementType.U4:
			case ElementType.I8:
			case ElementType.U8:
			case ElementType.R4:
			case ElementType.R8:
			case ElementType.String:
			case (ElementType)23:
			case (ElementType)26:
				break;
			case ElementType.Ptr:
				return new PointerType(this.ReadTypeSignature());
			case ElementType.ByRef:
				return new ByReferenceType(this.ReadTypeSignature());
			case ElementType.ValueType:
			{
				TypeReference typeDefOrRef = this.GetTypeDefOrRef(this.ReadTypeTokenSignature());
				typeDefOrRef.IsValueType = true;
				return typeDefOrRef;
			}
			case ElementType.Class:
				return this.GetTypeDefOrRef(this.ReadTypeTokenSignature());
			case ElementType.Var:
				return this.GetGenericParameter(GenericParameterType.Type, base.ReadCompressedUInt32());
			case ElementType.Array:
				return this.ReadArrayTypeSignature();
			case ElementType.GenericInst:
			{
				bool flag = base.ReadByte() == 17;
				TypeReference typeDefOrRef2 = this.GetTypeDefOrRef(this.ReadTypeTokenSignature());
				GenericInstanceType genericInstanceType = new GenericInstanceType(typeDefOrRef2);
				this.ReadGenericInstanceSignature(typeDefOrRef2, genericInstanceType);
				if (flag)
				{
					genericInstanceType.IsValueType = true;
					typeDefOrRef2.GetElementType().IsValueType = true;
				}
				return genericInstanceType;
			}
			case ElementType.TypedByRef:
				return this.TypeSystem.TypedReference;
			case ElementType.I:
				return this.TypeSystem.IntPtr;
			case ElementType.U:
				return this.TypeSystem.UIntPtr;
			case ElementType.FnPtr:
			{
				FunctionPointerType functionPointerType = new FunctionPointerType();
				this.ReadMethodSignature(functionPointerType);
				return functionPointerType;
			}
			case ElementType.Object:
				return this.TypeSystem.Object;
			case ElementType.SzArray:
				return new ArrayType(this.ReadTypeSignature());
			case ElementType.MVar:
				return this.GetGenericParameter(GenericParameterType.Method, base.ReadCompressedUInt32());
			case ElementType.CModReqD:
				return new RequiredModifierType(this.GetTypeDefOrRef(this.ReadTypeTokenSignature()), this.ReadTypeSignature());
			case ElementType.CModOpt:
				return new OptionalModifierType(this.GetTypeDefOrRef(this.ReadTypeTokenSignature()), this.ReadTypeSignature());
			default:
				if (etype == ElementType.Sentinel)
				{
					return new SentinelType(this.ReadTypeSignature());
				}
				if (etype == ElementType.Pinned)
				{
					return new PinnedType(this.ReadTypeSignature());
				}
				break;
			}
			return this.GetPrimitiveType(etype);
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00010734 File Offset: 0x0000E934
		public void ReadMethodSignature(IMethodSignature method)
		{
			byte b = base.ReadByte();
			if ((b & 32) != 0)
			{
				method.HasThis = true;
				b = (byte)((int)b & -33);
			}
			if ((b & 64) != 0)
			{
				method.ExplicitThis = true;
				b = (byte)((int)b & -65);
			}
			method.CallingConvention = (MethodCallingConvention)b;
			MethodReference methodReference = method as MethodReference;
			if (methodReference != null && !methodReference.DeclaringType.IsArray)
			{
				this.reader.context = methodReference;
			}
			if ((b & 16) != 0)
			{
				uint num = base.ReadCompressedUInt32();
				if (methodReference != null && !methodReference.IsDefinition)
				{
					SignatureReader.CheckGenericContext(methodReference, (int)(num - 1U));
				}
			}
			uint num2 = base.ReadCompressedUInt32();
			method.MethodReturnType.ReturnType = this.ReadTypeSignature();
			if (num2 == 0U)
			{
				return;
			}
			MethodReference methodReference2 = method as MethodReference;
			Collection<ParameterDefinition> collection;
			if (methodReference2 != null)
			{
				collection = (methodReference2.parameters = new ParameterDefinitionCollection(method, (int)num2));
			}
			else
			{
				collection = method.Parameters;
			}
			int num3 = 0;
			while ((long)num3 < (long)((ulong)num2))
			{
				collection.Add(new ParameterDefinition(this.ReadTypeSignature()));
				num3++;
			}
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00010824 File Offset: 0x0000EA24
		public object ReadConstantSignature(ElementType type)
		{
			return this.ReadPrimitiveValue(type);
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00010830 File Offset: 0x0000EA30
		public void ReadCustomAttributeConstructorArguments(CustomAttribute attribute, Collection<ParameterDefinition> parameters)
		{
			int count = parameters.Count;
			if (count == 0)
			{
				return;
			}
			attribute.arguments = new Collection<CustomAttributeArgument>(count);
			for (int i = 0; i < count; i++)
			{
				attribute.arguments.Add(this.ReadCustomAttributeFixedArgument(parameters[i].ParameterType));
			}
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0001087D File Offset: 0x0000EA7D
		private CustomAttributeArgument ReadCustomAttributeFixedArgument(TypeReference type)
		{
			if (type.IsArray)
			{
				return this.ReadCustomAttributeFixedArrayArgument((ArrayType)type);
			}
			return this.ReadCustomAttributeElement(type);
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0001089C File Offset: 0x0000EA9C
		public void ReadCustomAttributeNamedArguments(ushort count, ref Collection<CustomAttributeNamedArgument> fields, ref Collection<CustomAttributeNamedArgument> properties)
		{
			for (int i = 0; i < (int)count; i++)
			{
				this.ReadCustomAttributeNamedArgument(ref fields, ref properties);
			}
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x000108C0 File Offset: 0x0000EAC0
		private void ReadCustomAttributeNamedArgument(ref Collection<CustomAttributeNamedArgument> fields, ref Collection<CustomAttributeNamedArgument> properties)
		{
			byte b = base.ReadByte();
			TypeReference type = this.ReadCustomAttributeFieldOrPropType();
			string name = this.ReadUTF8String();
			Collection<CustomAttributeNamedArgument> customAttributeNamedArgumentCollection;
			switch (b)
			{
			case 83:
				customAttributeNamedArgumentCollection = SignatureReader.GetCustomAttributeNamedArgumentCollection(ref fields);
				break;
			case 84:
				customAttributeNamedArgumentCollection = SignatureReader.GetCustomAttributeNamedArgumentCollection(ref properties);
				break;
			default:
				throw new NotSupportedException();
			}
			customAttributeNamedArgumentCollection.Add(new CustomAttributeNamedArgument(name, this.ReadCustomAttributeFixedArgument(type)));
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00010924 File Offset: 0x0000EB24
		private static Collection<CustomAttributeNamedArgument> GetCustomAttributeNamedArgumentCollection(ref Collection<CustomAttributeNamedArgument> collection)
		{
			if (collection != null)
			{
				return collection;
			}
			Collection<CustomAttributeNamedArgument> result;
			collection = (result = new Collection<CustomAttributeNamedArgument>());
			return result;
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00010944 File Offset: 0x0000EB44
		private CustomAttributeArgument ReadCustomAttributeFixedArrayArgument(ArrayType type)
		{
			uint num = base.ReadUInt32();
			if (num == 4294967295U)
			{
				return new CustomAttributeArgument(type, null);
			}
			if (num == 0U)
			{
				return new CustomAttributeArgument(type, Empty<CustomAttributeArgument>.Array);
			}
			CustomAttributeArgument[] array = new CustomAttributeArgument[num];
			TypeReference elementType = type.ElementType;
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				array[num2] = this.ReadCustomAttributeElement(elementType);
				num2++;
			}
			return new CustomAttributeArgument(type, array);
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x000109AC File Offset: 0x0000EBAC
		private CustomAttributeArgument ReadCustomAttributeElement(TypeReference type)
		{
			if (type.IsArray)
			{
				return this.ReadCustomAttributeFixedArrayArgument((ArrayType)type);
			}
			return new CustomAttributeArgument(type, (type.etype == ElementType.Object) ? this.ReadCustomAttributeElement(this.ReadCustomAttributeFieldOrPropType()) : this.ReadCustomAttributeElementValue(type));
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x000109F8 File Offset: 0x0000EBF8
		private object ReadCustomAttributeElementValue(TypeReference type)
		{
			ElementType etype = type.etype;
			ElementType elementType = etype;
			if (elementType != ElementType.None)
			{
				if (elementType == ElementType.String)
				{
					return this.ReadUTF8String();
				}
				return this.ReadPrimitiveValue(etype);
			}
			else
			{
				if (type.IsTypeOf("System", "Type"))
				{
					return this.ReadTypeReference();
				}
				return this.ReadCustomAttributeEnum(type);
			}
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00010A48 File Offset: 0x0000EC48
		private object ReadPrimitiveValue(ElementType type)
		{
			switch (type)
			{
			case ElementType.Boolean:
				return base.ReadByte() == 1;
			case ElementType.Char:
				return (char)base.ReadUInt16();
			case ElementType.I1:
				return (sbyte)base.ReadByte();
			case ElementType.U1:
				return base.ReadByte();
			case ElementType.I2:
				return base.ReadInt16();
			case ElementType.U2:
				return base.ReadUInt16();
			case ElementType.I4:
				return base.ReadInt32();
			case ElementType.U4:
				return base.ReadUInt32();
			case ElementType.I8:
				return base.ReadInt64();
			case ElementType.U8:
				return base.ReadUInt64();
			case ElementType.R4:
				return base.ReadSingle();
			case ElementType.R8:
				return base.ReadDouble();
			default:
				throw new NotImplementedException(type.ToString());
			}
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00010B38 File Offset: 0x0000ED38
		private TypeReference GetPrimitiveType(ElementType etype)
		{
			switch (etype)
			{
			case ElementType.Boolean:
				return this.TypeSystem.Boolean;
			case ElementType.Char:
				return this.TypeSystem.Char;
			case ElementType.I1:
				return this.TypeSystem.SByte;
			case ElementType.U1:
				return this.TypeSystem.Byte;
			case ElementType.I2:
				return this.TypeSystem.Int16;
			case ElementType.U2:
				return this.TypeSystem.UInt16;
			case ElementType.I4:
				return this.TypeSystem.Int32;
			case ElementType.U4:
				return this.TypeSystem.UInt32;
			case ElementType.I8:
				return this.TypeSystem.Int64;
			case ElementType.U8:
				return this.TypeSystem.UInt64;
			case ElementType.R4:
				return this.TypeSystem.Single;
			case ElementType.R8:
				return this.TypeSystem.Double;
			case ElementType.String:
				return this.TypeSystem.String;
			default:
				throw new NotImplementedException(etype.ToString());
			}
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00010C34 File Offset: 0x0000EE34
		private TypeReference ReadCustomAttributeFieldOrPropType()
		{
			ElementType elementType = (ElementType)base.ReadByte();
			ElementType elementType2 = elementType;
			if (elementType2 == ElementType.SzArray)
			{
				return new ArrayType(this.ReadCustomAttributeFieldOrPropType());
			}
			switch (elementType2)
			{
			case ElementType.Type:
				return this.TypeSystem.LookupType("System", "Type");
			case ElementType.Boxed:
				return this.TypeSystem.Object;
			default:
				if (elementType2 != ElementType.Enum)
				{
					return this.GetPrimitiveType(elementType);
				}
				return this.ReadTypeReference();
			}
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00010CA3 File Offset: 0x0000EEA3
		public TypeReference ReadTypeReference()
		{
			return TypeParser.ParseType(this.reader.module, this.ReadUTF8String());
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00010CBC File Offset: 0x0000EEBC
		private object ReadCustomAttributeEnum(TypeReference enum_type)
		{
			TypeDefinition typeDefinition = enum_type.CheckedResolve();
			if (!typeDefinition.IsEnum)
			{
				throw new ArgumentException();
			}
			return this.ReadCustomAttributeElementValue(typeDefinition.GetEnumUnderlyingType());
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00010CEC File Offset: 0x0000EEEC
		public SecurityAttribute ReadSecurityAttribute()
		{
			SecurityAttribute securityAttribute = new SecurityAttribute(this.ReadTypeReference());
			base.ReadCompressedUInt32();
			this.ReadCustomAttributeNamedArguments((ushort)base.ReadCompressedUInt32(), ref securityAttribute.fields, ref securityAttribute.properties);
			return securityAttribute;
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00010D28 File Offset: 0x0000EF28
		public MarshalInfo ReadMarshalInfo()
		{
			NativeType nativeType = this.ReadNativeType();
			NativeType nativeType2 = nativeType;
			if (nativeType2 == NativeType.FixedSysString)
			{
				FixedSysStringMarshalInfo fixedSysStringMarshalInfo = new FixedSysStringMarshalInfo();
				if (this.CanReadMore())
				{
					fixedSysStringMarshalInfo.size = (int)base.ReadCompressedUInt32();
				}
				return fixedSysStringMarshalInfo;
			}
			switch (nativeType2)
			{
			case NativeType.SafeArray:
			{
				SafeArrayMarshalInfo safeArrayMarshalInfo = new SafeArrayMarshalInfo();
				if (this.CanReadMore())
				{
					safeArrayMarshalInfo.element_type = this.ReadVariantType();
				}
				return safeArrayMarshalInfo;
			}
			case NativeType.FixedArray:
			{
				FixedArrayMarshalInfo fixedArrayMarshalInfo = new FixedArrayMarshalInfo();
				if (this.CanReadMore())
				{
					fixedArrayMarshalInfo.size = (int)base.ReadCompressedUInt32();
				}
				if (this.CanReadMore())
				{
					fixedArrayMarshalInfo.element_type = this.ReadNativeType();
				}
				return fixedArrayMarshalInfo;
			}
			default:
				switch (nativeType2)
				{
				case NativeType.Array:
				{
					ArrayMarshalInfo arrayMarshalInfo = new ArrayMarshalInfo();
					if (this.CanReadMore())
					{
						arrayMarshalInfo.element_type = this.ReadNativeType();
					}
					if (this.CanReadMore())
					{
						arrayMarshalInfo.size_parameter_index = (int)base.ReadCompressedUInt32();
					}
					if (this.CanReadMore())
					{
						arrayMarshalInfo.size = (int)base.ReadCompressedUInt32();
					}
					if (this.CanReadMore())
					{
						arrayMarshalInfo.size_parameter_multiplier = (int)base.ReadCompressedUInt32();
					}
					return arrayMarshalInfo;
				}
				case NativeType.CustomMarshaler:
				{
					CustomMarshalInfo customMarshalInfo = new CustomMarshalInfo();
					string text = this.ReadUTF8String();
					customMarshalInfo.guid = ((!string.IsNullOrEmpty(text)) ? new Guid(text) : Guid.Empty);
					customMarshalInfo.unmanaged_type = this.ReadUTF8String();
					customMarshalInfo.managed_type = this.ReadTypeReference();
					customMarshalInfo.cookie = this.ReadUTF8String();
					return customMarshalInfo;
				}
				}
				return new MarshalInfo(nativeType);
			}
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00010E95 File Offset: 0x0000F095
		private NativeType ReadNativeType()
		{
			return (NativeType)base.ReadByte();
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00010E9D File Offset: 0x0000F09D
		private VariantType ReadVariantType()
		{
			return (VariantType)base.ReadByte();
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00010EA8 File Offset: 0x0000F0A8
		private string ReadUTF8String()
		{
			if (this.buffer[this.position] == 255)
			{
				this.position++;
				return null;
			}
			int num = (int)base.ReadCompressedUInt32();
			if (num == 0)
			{
				return string.Empty;
			}
			string @string = Encoding.UTF8.GetString(this.buffer, this.position, (this.buffer[this.position + num - 1] == 0) ? (num - 1) : num);
			this.position += num;
			return @string;
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00010F28 File Offset: 0x0000F128
		public bool CanReadMore()
		{
			return (long)this.position - (long)((ulong)this.start) < (long)((ulong)this.sig_length);
		}

		// Token: 0x040003A9 RID: 937
		private readonly MetadataReader reader;

		// Token: 0x040003AA RID: 938
		private readonly uint start;

		// Token: 0x040003AB RID: 939
		private readonly uint sig_length;
	}
}
