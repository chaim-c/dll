using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using Mono.Cecil.Cil;
using Mono.Cecil.Metadata;
using Mono.Collections.Generic;
using Mono.Security.Cryptography;

namespace Mono.Cecil
{
	// Token: 0x02000043 RID: 67
	internal static class Mixin
	{
		// Token: 0x060001C8 RID: 456 RVA: 0x000081B4 File Offset: 0x000063B4
		public static uint ReadCompressedUInt32(this byte[] data, ref int position)
		{
			uint num;
			if ((data[position] & 128) == 0)
			{
				num = (uint)data[position];
				position++;
			}
			else if ((data[position] & 64) == 0)
			{
				num = ((uint)data[position] & 4294967167U) << 8;
				num |= (uint)data[position + 1];
				position += 2;
			}
			else
			{
				num = ((uint)data[position] & 4294967103U) << 24;
				num |= (uint)((uint)data[position + 1] << 16);
				num |= (uint)((uint)data[position + 2] << 8);
				num |= (uint)data[position + 3];
				position += 4;
			}
			return num;
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00008238 File Offset: 0x00006438
		public static MetadataToken GetMetadataToken(this CodedIndex self, uint data)
		{
			uint rid;
			TokenType type;
			switch (self)
			{
			case CodedIndex.TypeDefOrRef:
				rid = data >> 2;
				switch (data & 3U)
				{
				case 0U:
					type = TokenType.TypeDef;
					break;
				case 1U:
					type = TokenType.TypeRef;
					break;
				case 2U:
					type = TokenType.TypeSpec;
					break;
				default:
					goto IL_44B;
				}
				break;
			case CodedIndex.HasConstant:
				rid = data >> 2;
				switch (data & 3U)
				{
				case 0U:
					type = TokenType.Field;
					break;
				case 1U:
					type = TokenType.Param;
					break;
				case 2U:
					type = TokenType.Property;
					break;
				default:
					goto IL_44B;
				}
				break;
			case CodedIndex.HasCustomAttribute:
				rid = data >> 5;
				switch (data & 31U)
				{
				case 0U:
					type = TokenType.Method;
					break;
				case 1U:
					type = TokenType.Field;
					break;
				case 2U:
					type = TokenType.TypeRef;
					break;
				case 3U:
					type = TokenType.TypeDef;
					break;
				case 4U:
					type = TokenType.Param;
					break;
				case 5U:
					type = TokenType.InterfaceImpl;
					break;
				case 6U:
					type = TokenType.MemberRef;
					break;
				case 7U:
					type = TokenType.Module;
					break;
				case 8U:
					type = TokenType.Permission;
					break;
				case 9U:
					type = TokenType.Property;
					break;
				case 10U:
					type = TokenType.Event;
					break;
				case 11U:
					type = TokenType.Signature;
					break;
				case 12U:
					type = TokenType.ModuleRef;
					break;
				case 13U:
					type = TokenType.TypeSpec;
					break;
				case 14U:
					type = TokenType.Assembly;
					break;
				case 15U:
					type = TokenType.AssemblyRef;
					break;
				case 16U:
					type = TokenType.File;
					break;
				case 17U:
					type = TokenType.ExportedType;
					break;
				case 18U:
					type = TokenType.ManifestResource;
					break;
				case 19U:
					type = TokenType.GenericParam;
					break;
				default:
					goto IL_44B;
				}
				break;
			case CodedIndex.HasFieldMarshal:
				rid = data >> 1;
				switch (data & 1U)
				{
				case 0U:
					type = TokenType.Field;
					break;
				case 1U:
					type = TokenType.Param;
					break;
				default:
					goto IL_44B;
				}
				break;
			case CodedIndex.HasDeclSecurity:
				rid = data >> 2;
				switch (data & 3U)
				{
				case 0U:
					type = TokenType.TypeDef;
					break;
				case 1U:
					type = TokenType.Method;
					break;
				case 2U:
					type = TokenType.Assembly;
					break;
				default:
					goto IL_44B;
				}
				break;
			case CodedIndex.MemberRefParent:
				rid = data >> 3;
				switch (data & 7U)
				{
				case 0U:
					type = TokenType.TypeDef;
					break;
				case 1U:
					type = TokenType.TypeRef;
					break;
				case 2U:
					type = TokenType.ModuleRef;
					break;
				case 3U:
					type = TokenType.Method;
					break;
				case 4U:
					type = TokenType.TypeSpec;
					break;
				default:
					goto IL_44B;
				}
				break;
			case CodedIndex.HasSemantics:
				rid = data >> 1;
				switch (data & 1U)
				{
				case 0U:
					type = TokenType.Event;
					break;
				case 1U:
					type = TokenType.Property;
					break;
				default:
					goto IL_44B;
				}
				break;
			case CodedIndex.MethodDefOrRef:
				rid = data >> 1;
				switch (data & 1U)
				{
				case 0U:
					type = TokenType.Method;
					break;
				case 1U:
					type = TokenType.MemberRef;
					break;
				default:
					goto IL_44B;
				}
				break;
			case CodedIndex.MemberForwarded:
				rid = data >> 1;
				switch (data & 1U)
				{
				case 0U:
					type = TokenType.Field;
					break;
				case 1U:
					type = TokenType.Method;
					break;
				default:
					goto IL_44B;
				}
				break;
			case CodedIndex.Implementation:
				rid = data >> 2;
				switch (data & 3U)
				{
				case 0U:
					type = TokenType.File;
					break;
				case 1U:
					type = TokenType.AssemblyRef;
					break;
				case 2U:
					type = TokenType.ExportedType;
					break;
				default:
					goto IL_44B;
				}
				break;
			case CodedIndex.CustomAttributeType:
				rid = data >> 3;
				switch (data & 7U)
				{
				case 2U:
					type = TokenType.Method;
					break;
				case 3U:
					type = TokenType.MemberRef;
					break;
				default:
					goto IL_44B;
				}
				break;
			case CodedIndex.ResolutionScope:
				rid = data >> 2;
				switch (data & 3U)
				{
				case 0U:
					type = TokenType.Module;
					break;
				case 1U:
					type = TokenType.ModuleRef;
					break;
				case 2U:
					type = TokenType.AssemblyRef;
					break;
				case 3U:
					type = TokenType.TypeRef;
					break;
				default:
					goto IL_44B;
				}
				break;
			case CodedIndex.TypeOrMethodDef:
				rid = data >> 1;
				switch (data & 1U)
				{
				case 0U:
					type = TokenType.TypeDef;
					break;
				case 1U:
					type = TokenType.Method;
					break;
				default:
					goto IL_44B;
				}
				break;
			default:
				goto IL_44B;
			}
			return new MetadataToken(type, rid);
			IL_44B:
			return MetadataToken.Zero;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00008698 File Offset: 0x00006898
		public static uint CompressMetadataToken(this CodedIndex self, MetadataToken token)
		{
			uint num = 0U;
			if (token.RID == 0U)
			{
				return num;
			}
			switch (self)
			{
			case CodedIndex.TypeDefOrRef:
			{
				num = token.RID << 2;
				TokenType tokenType = token.TokenType;
				if (tokenType == TokenType.TypeRef)
				{
					return num | 1U;
				}
				if (tokenType == TokenType.TypeDef)
				{
					return num;
				}
				if (tokenType == TokenType.TypeSpec)
				{
					return num | 2U;
				}
				break;
			}
			case CodedIndex.HasConstant:
			{
				num = token.RID << 2;
				TokenType tokenType2 = token.TokenType;
				if (tokenType2 == TokenType.Field)
				{
					return num;
				}
				if (tokenType2 == TokenType.Param)
				{
					return num | 1U;
				}
				if (tokenType2 == TokenType.Property)
				{
					return num | 2U;
				}
				break;
			}
			case CodedIndex.HasCustomAttribute:
			{
				num = token.RID << 5;
				TokenType tokenType3 = token.TokenType;
				if (tokenType3 <= TokenType.Signature)
				{
					if (tokenType3 <= TokenType.Method)
					{
						if (tokenType3 <= TokenType.TypeRef)
						{
							if (tokenType3 == TokenType.Module)
							{
								return num | 7U;
							}
							if (tokenType3 == TokenType.TypeRef)
							{
								return num | 2U;
							}
						}
						else
						{
							if (tokenType3 == TokenType.TypeDef)
							{
								return num | 3U;
							}
							if (tokenType3 == TokenType.Field)
							{
								return num | 1U;
							}
							if (tokenType3 == TokenType.Method)
							{
								return num;
							}
						}
					}
					else if (tokenType3 <= TokenType.InterfaceImpl)
					{
						if (tokenType3 == TokenType.Param)
						{
							return num | 4U;
						}
						if (tokenType3 == TokenType.InterfaceImpl)
						{
							return num | 5U;
						}
					}
					else
					{
						if (tokenType3 == TokenType.MemberRef)
						{
							return num | 6U;
						}
						if (tokenType3 == TokenType.Permission)
						{
							return num | 8U;
						}
						if (tokenType3 == TokenType.Signature)
						{
							return num | 11U;
						}
					}
				}
				else if (tokenType3 <= TokenType.Assembly)
				{
					if (tokenType3 <= TokenType.Property)
					{
						if (tokenType3 == TokenType.Event)
						{
							return num | 10U;
						}
						if (tokenType3 == TokenType.Property)
						{
							return num | 9U;
						}
					}
					else
					{
						if (tokenType3 == TokenType.ModuleRef)
						{
							return num | 12U;
						}
						if (tokenType3 == TokenType.TypeSpec)
						{
							return num | 13U;
						}
						if (tokenType3 == TokenType.Assembly)
						{
							return num | 14U;
						}
					}
				}
				else if (tokenType3 <= TokenType.File)
				{
					if (tokenType3 == TokenType.AssemblyRef)
					{
						return num | 15U;
					}
					if (tokenType3 == TokenType.File)
					{
						return num | 16U;
					}
				}
				else
				{
					if (tokenType3 == TokenType.ExportedType)
					{
						return num | 17U;
					}
					if (tokenType3 == TokenType.ManifestResource)
					{
						return num | 18U;
					}
					if (tokenType3 == TokenType.GenericParam)
					{
						return num | 19U;
					}
				}
				break;
			}
			case CodedIndex.HasFieldMarshal:
			{
				num = token.RID << 1;
				TokenType tokenType4 = token.TokenType;
				if (tokenType4 == TokenType.Field)
				{
					return num;
				}
				if (tokenType4 == TokenType.Param)
				{
					return num | 1U;
				}
				break;
			}
			case CodedIndex.HasDeclSecurity:
			{
				num = token.RID << 2;
				TokenType tokenType5 = token.TokenType;
				if (tokenType5 == TokenType.TypeDef)
				{
					return num;
				}
				if (tokenType5 == TokenType.Method)
				{
					return num | 1U;
				}
				if (tokenType5 == TokenType.Assembly)
				{
					return num | 2U;
				}
				break;
			}
			case CodedIndex.MemberRefParent:
			{
				num = token.RID << 3;
				TokenType tokenType6 = token.TokenType;
				if (tokenType6 <= TokenType.TypeDef)
				{
					if (tokenType6 == TokenType.TypeRef)
					{
						return num | 1U;
					}
					if (tokenType6 == TokenType.TypeDef)
					{
						return num;
					}
				}
				else
				{
					if (tokenType6 == TokenType.Method)
					{
						return num | 3U;
					}
					if (tokenType6 == TokenType.ModuleRef)
					{
						return num | 2U;
					}
					if (tokenType6 == TokenType.TypeSpec)
					{
						return num | 4U;
					}
				}
				break;
			}
			case CodedIndex.HasSemantics:
			{
				num = token.RID << 1;
				TokenType tokenType7 = token.TokenType;
				if (tokenType7 == TokenType.Event)
				{
					return num;
				}
				if (tokenType7 == TokenType.Property)
				{
					return num | 1U;
				}
				break;
			}
			case CodedIndex.MethodDefOrRef:
			{
				num = token.RID << 1;
				TokenType tokenType8 = token.TokenType;
				if (tokenType8 == TokenType.Method)
				{
					return num;
				}
				if (tokenType8 == TokenType.MemberRef)
				{
					return num | 1U;
				}
				break;
			}
			case CodedIndex.MemberForwarded:
			{
				num = token.RID << 1;
				TokenType tokenType9 = token.TokenType;
				if (tokenType9 == TokenType.Field)
				{
					return num;
				}
				if (tokenType9 == TokenType.Method)
				{
					return num | 1U;
				}
				break;
			}
			case CodedIndex.Implementation:
			{
				num = token.RID << 2;
				TokenType tokenType10 = token.TokenType;
				if (tokenType10 == TokenType.AssemblyRef)
				{
					return num | 1U;
				}
				if (tokenType10 == TokenType.File)
				{
					return num;
				}
				if (tokenType10 == TokenType.ExportedType)
				{
					return num | 2U;
				}
				break;
			}
			case CodedIndex.CustomAttributeType:
			{
				num = token.RID << 3;
				TokenType tokenType11 = token.TokenType;
				if (tokenType11 == TokenType.Method)
				{
					return num | 2U;
				}
				if (tokenType11 == TokenType.MemberRef)
				{
					return num | 3U;
				}
				break;
			}
			case CodedIndex.ResolutionScope:
			{
				num = token.RID << 2;
				TokenType tokenType12 = token.TokenType;
				if (tokenType12 <= TokenType.TypeRef)
				{
					if (tokenType12 == TokenType.Module)
					{
						return num;
					}
					if (tokenType12 == TokenType.TypeRef)
					{
						return num | 3U;
					}
				}
				else
				{
					if (tokenType12 == TokenType.ModuleRef)
					{
						return num | 1U;
					}
					if (tokenType12 == TokenType.AssemblyRef)
					{
						return num | 2U;
					}
				}
				break;
			}
			case CodedIndex.TypeOrMethodDef:
			{
				num = token.RID << 1;
				TokenType tokenType13 = token.TokenType;
				if (tokenType13 == TokenType.TypeDef)
				{
					return num;
				}
				if (tokenType13 == TokenType.Method)
				{
					return num | 1U;
				}
				break;
			}
			}
			throw new ArgumentException();
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00008B6C File Offset: 0x00006D6C
		public static int GetSize(this CodedIndex self, Func<Table, int> counter)
		{
			int num;
			Table[] array;
			switch (self)
			{
			case CodedIndex.TypeDefOrRef:
				num = 2;
				array = new Table[]
				{
					Table.TypeDef,
					Table.TypeRef,
					Table.TypeSpec
				};
				break;
			case CodedIndex.HasConstant:
				num = 2;
				array = new Table[]
				{
					Table.Field,
					Table.Param,
					Table.Property
				};
				break;
			case CodedIndex.HasCustomAttribute:
				num = 5;
				array = new Table[]
				{
					Table.Method,
					Table.Field,
					Table.TypeRef,
					Table.TypeDef,
					Table.Param,
					Table.InterfaceImpl,
					Table.MemberRef,
					Table.Module,
					Table.DeclSecurity,
					Table.Property,
					Table.Event,
					Table.StandAloneSig,
					Table.ModuleRef,
					Table.TypeSpec,
					Table.Assembly,
					Table.AssemblyRef,
					Table.File,
					Table.ExportedType,
					Table.ManifestResource,
					Table.GenericParam
				};
				break;
			case CodedIndex.HasFieldMarshal:
				num = 1;
				array = new Table[]
				{
					Table.Field,
					Table.Param
				};
				break;
			case CodedIndex.HasDeclSecurity:
				num = 2;
				array = new Table[]
				{
					Table.TypeDef,
					Table.Method,
					Table.Assembly
				};
				break;
			case CodedIndex.MemberRefParent:
				num = 3;
				array = new Table[]
				{
					Table.TypeDef,
					Table.TypeRef,
					Table.ModuleRef,
					Table.Method,
					Table.TypeSpec
				};
				break;
			case CodedIndex.HasSemantics:
				num = 1;
				array = new Table[]
				{
					Table.Event,
					Table.Property
				};
				break;
			case CodedIndex.MethodDefOrRef:
				num = 1;
				array = new Table[]
				{
					Table.Method,
					Table.MemberRef
				};
				break;
			case CodedIndex.MemberForwarded:
				num = 1;
				array = new Table[]
				{
					Table.Field,
					Table.Method
				};
				break;
			case CodedIndex.Implementation:
				num = 2;
				array = new Table[]
				{
					Table.File,
					Table.AssemblyRef,
					Table.ExportedType
				};
				break;
			case CodedIndex.CustomAttributeType:
				num = 3;
				array = new Table[]
				{
					Table.Method,
					Table.MemberRef
				};
				break;
			case CodedIndex.ResolutionScope:
				num = 2;
				array = new Table[]
				{
					Table.Module,
					Table.ModuleRef,
					Table.AssemblyRef,
					Table.TypeRef
				};
				break;
			case CodedIndex.TypeOrMethodDef:
				num = 1;
				array = new Table[]
				{
					Table.TypeDef,
					Table.Method
				};
				break;
			default:
				throw new ArgumentException();
			}
			int num2 = 0;
			for (int i = 0; i < array.Length; i++)
			{
				num2 = Math.Max(counter(array[i]), num2);
			}
			if (num2 >= 1 << 16 - num)
			{
				return 4;
			}
			return 2;
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00008DFC File Offset: 0x00006FFC
		public static bool GetHasSecurityDeclarations(this ISecurityDeclarationProvider self, ModuleDefinition module)
		{
			if (module.HasImage())
			{
				return module.Read<ISecurityDeclarationProvider, bool>(self, (ISecurityDeclarationProvider provider, MetadataReader reader) => reader.HasSecurityDeclarations(provider));
			}
			return false;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00008E38 File Offset: 0x00007038
		public static Collection<SecurityDeclaration> GetSecurityDeclarations(this ISecurityDeclarationProvider self, ref Collection<SecurityDeclaration> variable, ModuleDefinition module)
		{
			if (!module.HasImage())
			{
				Collection<SecurityDeclaration> result;
				variable = (result = new Collection<SecurityDeclaration>());
				return result;
			}
			return module.Read<ISecurityDeclarationProvider, Collection<SecurityDeclaration>>(ref variable, self, (ISecurityDeclarationProvider provider, MetadataReader reader) => reader.ReadSecurityDeclarations(provider));
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00008E7D File Offset: 0x0000707D
		public static void CheckName(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("Empty name");
			}
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00008EAC File Offset: 0x000070AC
		public static void ResolveConstant(this IConstantProvider self, ref object constant, ModuleDefinition module)
		{
			if (module == null)
			{
				constant = Mixin.NoValue;
				return;
			}
			lock (module.SyncRoot)
			{
				if (constant == Mixin.NotResolved)
				{
					if (module.HasImage())
					{
						constant = module.Read<IConstantProvider, object>(self, (IConstantProvider provider, MetadataReader reader) => reader.ReadConstant(provider));
					}
					else
					{
						constant = Mixin.NoValue;
					}
				}
			}
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00008F35 File Offset: 0x00007135
		public static bool GetHasCustomAttributes(this ICustomAttributeProvider self, ModuleDefinition module)
		{
			if (module.HasImage())
			{
				return module.Read<ICustomAttributeProvider, bool>(self, (ICustomAttributeProvider provider, MetadataReader reader) => reader.HasCustomAttributes(provider));
			}
			return false;
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00008F70 File Offset: 0x00007170
		public static Collection<CustomAttribute> GetCustomAttributes(this ICustomAttributeProvider self, ref Collection<CustomAttribute> variable, ModuleDefinition module)
		{
			if (!module.HasImage())
			{
				Collection<CustomAttribute> result;
				variable = (result = new Collection<CustomAttribute>());
				return result;
			}
			return module.Read<ICustomAttributeProvider, Collection<CustomAttribute>>(ref variable, self, (ICustomAttributeProvider provider, MetadataReader reader) => reader.ReadCustomAttributes(provider));
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00008FB8 File Offset: 0x000071B8
		public static bool ContainsGenericParameter(this IGenericInstance self)
		{
			Collection<TypeReference> genericArguments = self.GenericArguments;
			for (int i = 0; i < genericArguments.Count; i++)
			{
				if (genericArguments[i].ContainsGenericParameter)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00008FF0 File Offset: 0x000071F0
		public static void GenericInstanceFullName(this IGenericInstance self, StringBuilder builder)
		{
			builder.Append("<");
			Collection<TypeReference> genericArguments = self.GenericArguments;
			for (int i = 0; i < genericArguments.Count; i++)
			{
				if (i > 0)
				{
					builder.Append(",");
				}
				builder.Append(genericArguments[i].FullName);
			}
			builder.Append(">");
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00009059 File Offset: 0x00007259
		public static bool GetHasGenericParameters(this IGenericParameterProvider self, ModuleDefinition module)
		{
			if (module.HasImage())
			{
				return module.Read<IGenericParameterProvider, bool>(self, (IGenericParameterProvider provider, MetadataReader reader) => reader.HasGenericParameters(provider));
			}
			return false;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00009094 File Offset: 0x00007294
		public static Collection<GenericParameter> GetGenericParameters(this IGenericParameterProvider self, ref Collection<GenericParameter> collection, ModuleDefinition module)
		{
			if (!module.HasImage())
			{
				Collection<GenericParameter> result;
				collection = (result = new GenericParameterCollection(self));
				return result;
			}
			return module.Read<IGenericParameterProvider, Collection<GenericParameter>>(ref collection, self, (IGenericParameterProvider provider, MetadataReader reader) => reader.ReadGenericParameters(provider));
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x000090E3 File Offset: 0x000072E3
		public static bool GetHasMarshalInfo(this IMarshalInfoProvider self, ModuleDefinition module)
		{
			if (module.HasImage())
			{
				return module.Read<IMarshalInfoProvider, bool>(self, (IMarshalInfoProvider provider, MetadataReader reader) => reader.HasMarshalInfo(provider));
			}
			return false;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000911C File Offset: 0x0000731C
		public static MarshalInfo GetMarshalInfo(this IMarshalInfoProvider self, ref MarshalInfo variable, ModuleDefinition module)
		{
			if (!module.HasImage())
			{
				return null;
			}
			return module.Read<IMarshalInfoProvider, MarshalInfo>(ref variable, self, (IMarshalInfoProvider provider, MetadataReader reader) => reader.ReadMarshalInfo(provider));
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000914D File Offset: 0x0000734D
		public static void CheckModifier(TypeReference modifierType, TypeReference type)
		{
			if (modifierType == null)
			{
				throw new ArgumentNullException("modifierType");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000916B File Offset: 0x0000736B
		public static bool HasImplicitThis(this IMethodSignature self)
		{
			return self.HasThis && !self.ExplicitThis;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00009180 File Offset: 0x00007380
		public static void MethodSignatureFullName(this IMethodSignature self, StringBuilder builder)
		{
			builder.Append("(");
			if (self.HasParameters)
			{
				Collection<ParameterDefinition> parameters = self.Parameters;
				for (int i = 0; i < parameters.Count; i++)
				{
					ParameterDefinition parameterDefinition = parameters[i];
					if (i > 0)
					{
						builder.Append(",");
					}
					if (parameterDefinition.ParameterType.IsSentinel)
					{
						builder.Append("...,");
					}
					builder.Append(parameterDefinition.ParameterType.FullName);
				}
			}
			builder.Append(")");
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00009208 File Offset: 0x00007408
		public static bool GetAttributes(this uint self, uint attributes)
		{
			return (self & attributes) != 0U;
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00009213 File Offset: 0x00007413
		public static uint SetAttributes(this uint self, uint attributes, bool value)
		{
			if (value)
			{
				return self | attributes;
			}
			return self & ~attributes;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00009220 File Offset: 0x00007420
		public static bool GetMaskedAttributes(this uint self, uint mask, uint attributes)
		{
			return (self & mask) == attributes;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00009228 File Offset: 0x00007428
		public static uint SetMaskedAttributes(this uint self, uint mask, uint attributes, bool value)
		{
			if (value)
			{
				self &= ~mask;
				return self | attributes;
			}
			return self & ~(mask & attributes);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000923D File Offset: 0x0000743D
		public static bool GetAttributes(this ushort self, ushort attributes)
		{
			return (self & attributes) != 0;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00009248 File Offset: 0x00007448
		public static ushort SetAttributes(this ushort self, ushort attributes, bool value)
		{
			if (value)
			{
				return self | attributes;
			}
			return self & ~attributes;
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00009257 File Offset: 0x00007457
		public static bool GetMaskedAttributes(this ushort self, ushort mask, uint attributes)
		{
			return (long)(self & mask) == (long)((ulong)attributes);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00009261 File Offset: 0x00007461
		public static ushort SetMaskedAttributes(this ushort self, ushort mask, uint attributes, bool value)
		{
			if (value)
			{
				self &= ~mask;
				return (ushort)((uint)self | attributes);
			}
			return (ushort)((uint)self & ~((uint)mask & attributes));
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000927C File Offset: 0x0000747C
		public static ParameterDefinition GetParameter(this Mono.Cecil.Cil.MethodBody self, int index)
		{
			MethodDefinition method = self.method;
			if (method.HasThis)
			{
				if (index == 0)
				{
					return self.ThisParameter;
				}
				index--;
			}
			Collection<ParameterDefinition> parameters = method.Parameters;
			if (index < 0 || index >= parameters.size)
			{
				return null;
			}
			return parameters[index];
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x000092C4 File Offset: 0x000074C4
		public static VariableDefinition GetVariable(this Mono.Cecil.Cil.MethodBody self, int index)
		{
			Collection<VariableDefinition> variables = self.Variables;
			if (index < 0 || index >= variables.size)
			{
				return null;
			}
			return variables[index];
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x000092EE File Offset: 0x000074EE
		public static bool GetSemantics(this MethodDefinition self, MethodSemanticsAttributes semantics)
		{
			return (ushort)(self.SemanticsAttributes & semantics) != 0;
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x000092FF File Offset: 0x000074FF
		public static void SetSemantics(this MethodDefinition self, MethodSemanticsAttributes semantics, bool value)
		{
			if (value)
			{
				self.SemanticsAttributes |= semantics;
				return;
			}
			self.SemanticsAttributes &= ~semantics;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00009325 File Offset: 0x00007525
		public static bool IsVarArg(this IMethodSignature self)
		{
			return (byte)(self.CallingConvention & MethodCallingConvention.VarArg) != 0;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00009338 File Offset: 0x00007538
		public static int GetSentinelPosition(this IMethodSignature self)
		{
			if (!self.HasParameters)
			{
				return -1;
			}
			Collection<ParameterDefinition> parameters = self.Parameters;
			for (int i = 0; i < parameters.Count; i++)
			{
				if (parameters[i].ParameterType.IsSentinel)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000937D File Offset: 0x0000757D
		public static void CheckParameters(object parameters)
		{
			if (parameters == null)
			{
				throw new ArgumentNullException("parameters");
			}
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000938D File Offset: 0x0000758D
		public static bool HasImage(this ModuleDefinition self)
		{
			return self != null && self.HasImage;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000939A File Offset: 0x0000759A
		public static bool IsCorlib(this ModuleDefinition module)
		{
			return module.Assembly != null && module.Assembly.Name.Name == "mscorlib";
		}

		// Token: 0x060001EC RID: 492 RVA: 0x000093C0 File Offset: 0x000075C0
		public static string GetFullyQualifiedName(this Stream self)
		{
			FileStream fileStream = self as FileStream;
			if (fileStream == null)
			{
				return string.Empty;
			}
			return Path.GetFullPath(fileStream.Name);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x000093E8 File Offset: 0x000075E8
		public static TargetRuntime ParseRuntime(this string self)
		{
			switch (self[1])
			{
			case '1':
				if (self[3] != '0')
				{
					return TargetRuntime.Net_1_1;
				}
				return TargetRuntime.Net_1_0;
			case '2':
				return TargetRuntime.Net_2_0;
			}
			return TargetRuntime.Net_4_0;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000942C File Offset: 0x0000762C
		public static string RuntimeVersionString(this TargetRuntime runtime)
		{
			switch (runtime)
			{
			case TargetRuntime.Net_1_0:
				return "v1.0.3705";
			case TargetRuntime.Net_1_1:
				return "v1.1.4322";
			case TargetRuntime.Net_2_0:
				return "v2.0.50727";
			}
			return "v4.0.30319";
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000946C File Offset: 0x0000766C
		public static TypeReference GetEnumUnderlyingType(this TypeDefinition self)
		{
			Collection<FieldDefinition> fields = self.Fields;
			for (int i = 0; i < fields.Count; i++)
			{
				FieldDefinition fieldDefinition = fields[i];
				if (!fieldDefinition.IsStatic)
				{
					return fieldDefinition.FieldType;
				}
			}
			throw new ArgumentException();
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x000094B0 File Offset: 0x000076B0
		public static TypeDefinition GetNestedType(this TypeDefinition self, string fullname)
		{
			if (!self.HasNestedTypes)
			{
				return null;
			}
			Collection<TypeDefinition> nestedTypes = self.NestedTypes;
			for (int i = 0; i < nestedTypes.Count; i++)
			{
				TypeDefinition typeDefinition = nestedTypes[i];
				if (typeDefinition.TypeFullName() == fullname)
				{
					return typeDefinition;
				}
			}
			return null;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x000094F8 File Offset: 0x000076F8
		public static bool IsPrimitive(this ElementType self)
		{
			switch (self)
			{
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
			case ElementType.I:
			case ElementType.U:
				return true;
			}
			return false;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00009574 File Offset: 0x00007774
		public static string TypeFullName(this TypeReference self)
		{
			if (!string.IsNullOrEmpty(self.Namespace))
			{
				return self.Namespace + '.' + self.Name;
			}
			return self.Name;
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x000095A2 File Offset: 0x000077A2
		public static bool IsTypeOf(this TypeReference self, string @namespace, string name)
		{
			return self.Name == name && self.Namespace == @namespace;
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x000095C0 File Offset: 0x000077C0
		public static bool IsTypeSpecification(this TypeReference type)
		{
			ElementType etype = type.etype;
			switch (etype)
			{
			case ElementType.Ptr:
			case ElementType.ByRef:
			case ElementType.Var:
			case ElementType.Array:
			case ElementType.GenericInst:
			case ElementType.FnPtr:
			case ElementType.SzArray:
			case ElementType.MVar:
			case ElementType.CModReqD:
			case ElementType.CModOpt:
				break;
			case ElementType.ValueType:
			case ElementType.Class:
			case ElementType.TypedByRef:
			case (ElementType)23:
			case ElementType.I:
			case ElementType.U:
			case (ElementType)26:
			case ElementType.Object:
				return false;
			default:
				if (etype != ElementType.Sentinel && etype != ElementType.Pinned)
				{
					return false;
				}
				break;
			}
			return true;
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00009634 File Offset: 0x00007834
		public static TypeDefinition CheckedResolve(this TypeReference self)
		{
			TypeDefinition typeDefinition = self.Resolve();
			if (typeDefinition == null)
			{
				throw new ResolutionException(self);
			}
			return typeDefinition;
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00009653 File Offset: 0x00007853
		public static void CheckType(TypeReference type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00009664 File Offset: 0x00007864
		public static RSA CreateRSA(this StrongNameKeyPair key_pair)
		{
			byte[] blob;
			string keyContainerName;
			if (!Mixin.TryGetKeyContainer(key_pair, out blob, out keyContainerName))
			{
				return CryptoConvert.FromCapiKeyBlob(blob);
			}
			CspParameters parameters = new CspParameters
			{
				Flags = CspProviderFlags.UseMachineKeyStore,
				KeyContainerName = keyContainerName,
				KeyNumber = 2
			};
			return new RSACryptoServiceProvider(parameters);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x000096A8 File Offset: 0x000078A8
		private static bool TryGetKeyContainer(ISerializable key_pair, out byte[] key, out string key_container)
		{
			SerializationInfo serializationInfo = new SerializationInfo(typeof(StrongNameKeyPair), new FormatterConverter());
			key_pair.GetObjectData(serializationInfo, default(StreamingContext));
			key = (byte[])serializationInfo.GetValue("_keyPairArray", typeof(byte[]));
			key_container = serializationInfo.GetString("_keyPairContainer");
			return key_container != null;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000970B File Offset: 0x0000790B
		public static bool IsNullOrEmpty<T>(this T[] self)
		{
			return self == null || self.Length == 0;
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00009718 File Offset: 0x00007918
		public static bool IsNullOrEmpty<T>(this Collection<T> self)
		{
			return self == null || self.size == 0;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00009728 File Offset: 0x00007928
		public static T[] Resize<T>(this T[] self, int length)
		{
			Array.Resize<T>(ref self, length);
			return self;
		}

		// Token: 0x04000326 RID: 806
		public const int NotResolvedMarker = -2;

		// Token: 0x04000327 RID: 807
		public const int NoDataMarker = -1;

		// Token: 0x04000328 RID: 808
		internal static object NoValue = new object();

		// Token: 0x04000329 RID: 809
		internal static object NotResolved = new object();
	}
}
