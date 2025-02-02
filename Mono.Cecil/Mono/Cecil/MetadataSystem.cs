using System;
using System.Collections.Generic;
using Mono.Cecil.Metadata;

namespace Mono.Cecil
{
	// Token: 0x020000C0 RID: 192
	internal sealed class MetadataSystem
	{
		// Token: 0x060006C6 RID: 1734 RVA: 0x00019004 File Offset: 0x00017204
		private static void InitializePrimitives()
		{
			MetadataSystem.primitive_value_types = new Dictionary<string, Row<ElementType, bool>>(18, StringComparer.Ordinal)
			{
				{
					"Void",
					new Row<ElementType, bool>(ElementType.Void, false)
				},
				{
					"Boolean",
					new Row<ElementType, bool>(ElementType.Boolean, true)
				},
				{
					"Char",
					new Row<ElementType, bool>(ElementType.Char, true)
				},
				{
					"SByte",
					new Row<ElementType, bool>(ElementType.I1, true)
				},
				{
					"Byte",
					new Row<ElementType, bool>(ElementType.U1, true)
				},
				{
					"Int16",
					new Row<ElementType, bool>(ElementType.I2, true)
				},
				{
					"UInt16",
					new Row<ElementType, bool>(ElementType.U2, true)
				},
				{
					"Int32",
					new Row<ElementType, bool>(ElementType.I4, true)
				},
				{
					"UInt32",
					new Row<ElementType, bool>(ElementType.U4, true)
				},
				{
					"Int64",
					new Row<ElementType, bool>(ElementType.I8, true)
				},
				{
					"UInt64",
					new Row<ElementType, bool>(ElementType.U8, true)
				},
				{
					"Single",
					new Row<ElementType, bool>(ElementType.R4, true)
				},
				{
					"Double",
					new Row<ElementType, bool>(ElementType.R8, true)
				},
				{
					"String",
					new Row<ElementType, bool>(ElementType.String, false)
				},
				{
					"TypedReference",
					new Row<ElementType, bool>(ElementType.TypedByRef, false)
				},
				{
					"IntPtr",
					new Row<ElementType, bool>(ElementType.I, true)
				},
				{
					"UIntPtr",
					new Row<ElementType, bool>(ElementType.U, true)
				},
				{
					"Object",
					new Row<ElementType, bool>(ElementType.Object, false)
				}
			};
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x00019174 File Offset: 0x00017374
		public static void TryProcessPrimitiveTypeReference(TypeReference type)
		{
			if (type.Namespace != "System")
			{
				return;
			}
			IMetadataScope scope = type.scope;
			if (scope == null || scope.MetadataScopeType != MetadataScopeType.AssemblyNameReference)
			{
				return;
			}
			Row<ElementType, bool> row;
			if (!MetadataSystem.TryGetPrimitiveData(type, out row))
			{
				return;
			}
			type.etype = row.Col1;
			type.IsValueType = row.Col2;
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x000191CC File Offset: 0x000173CC
		public static bool TryGetPrimitiveElementType(TypeDefinition type, out ElementType etype)
		{
			etype = ElementType.None;
			if (type.Namespace != "System")
			{
				return false;
			}
			Row<ElementType, bool> row;
			if (MetadataSystem.TryGetPrimitiveData(type, out row) && row.Col1.IsPrimitive())
			{
				etype = row.Col1;
				return true;
			}
			return false;
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x00019214 File Offset: 0x00017414
		private static bool TryGetPrimitiveData(TypeReference type, out Row<ElementType, bool> primitive_data)
		{
			if (MetadataSystem.primitive_value_types == null)
			{
				MetadataSystem.InitializePrimitives();
			}
			return MetadataSystem.primitive_value_types.TryGetValue(type.Name, out primitive_data);
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x00019234 File Offset: 0x00017434
		public void Clear()
		{
			if (this.NestedTypes != null)
			{
				this.NestedTypes.Clear();
			}
			if (this.ReverseNestedTypes != null)
			{
				this.ReverseNestedTypes.Clear();
			}
			if (this.Interfaces != null)
			{
				this.Interfaces.Clear();
			}
			if (this.ClassLayouts != null)
			{
				this.ClassLayouts.Clear();
			}
			if (this.FieldLayouts != null)
			{
				this.FieldLayouts.Clear();
			}
			if (this.FieldRVAs != null)
			{
				this.FieldRVAs.Clear();
			}
			if (this.FieldMarshals != null)
			{
				this.FieldMarshals.Clear();
			}
			if (this.Constants != null)
			{
				this.Constants.Clear();
			}
			if (this.Overrides != null)
			{
				this.Overrides.Clear();
			}
			if (this.CustomAttributes != null)
			{
				this.CustomAttributes.Clear();
			}
			if (this.SecurityDeclarations != null)
			{
				this.SecurityDeclarations.Clear();
			}
			if (this.Events != null)
			{
				this.Events.Clear();
			}
			if (this.Properties != null)
			{
				this.Properties.Clear();
			}
			if (this.Semantics != null)
			{
				this.Semantics.Clear();
			}
			if (this.PInvokes != null)
			{
				this.PInvokes.Clear();
			}
			if (this.GenericParameters != null)
			{
				this.GenericParameters.Clear();
			}
			if (this.GenericConstraints != null)
			{
				this.GenericConstraints.Clear();
			}
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x00019384 File Offset: 0x00017584
		public TypeDefinition GetTypeDefinition(uint rid)
		{
			if (rid < 1U || (ulong)rid > (ulong)((long)this.Types.Length))
			{
				return null;
			}
			return this.Types[(int)((UIntPtr)(rid - 1U))];
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x000193A4 File Offset: 0x000175A4
		public void AddTypeDefinition(TypeDefinition type)
		{
			this.Types[(int)((UIntPtr)(type.token.RID - 1U))] = type;
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x000193BC File Offset: 0x000175BC
		public TypeReference GetTypeReference(uint rid)
		{
			if (rid < 1U || (ulong)rid > (ulong)((long)this.TypeReferences.Length))
			{
				return null;
			}
			return this.TypeReferences[(int)((UIntPtr)(rid - 1U))];
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x000193DC File Offset: 0x000175DC
		public void AddTypeReference(TypeReference type)
		{
			this.TypeReferences[(int)((UIntPtr)(type.token.RID - 1U))] = type;
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x000193F4 File Offset: 0x000175F4
		public FieldDefinition GetFieldDefinition(uint rid)
		{
			if (rid < 1U || (ulong)rid > (ulong)((long)this.Fields.Length))
			{
				return null;
			}
			return this.Fields[(int)((UIntPtr)(rid - 1U))];
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x00019414 File Offset: 0x00017614
		public void AddFieldDefinition(FieldDefinition field)
		{
			this.Fields[(int)((UIntPtr)(field.token.RID - 1U))] = field;
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x0001942C File Offset: 0x0001762C
		public MethodDefinition GetMethodDefinition(uint rid)
		{
			if (rid < 1U || (ulong)rid > (ulong)((long)this.Methods.Length))
			{
				return null;
			}
			return this.Methods[(int)((UIntPtr)(rid - 1U))];
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x0001944C File Offset: 0x0001764C
		public void AddMethodDefinition(MethodDefinition method)
		{
			this.Methods[(int)((UIntPtr)(method.token.RID - 1U))] = method;
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x00019464 File Offset: 0x00017664
		public MemberReference GetMemberReference(uint rid)
		{
			if (rid < 1U || (ulong)rid > (ulong)((long)this.MemberReferences.Length))
			{
				return null;
			}
			return this.MemberReferences[(int)((UIntPtr)(rid - 1U))];
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x00019484 File Offset: 0x00017684
		public void AddMemberReference(MemberReference member)
		{
			this.MemberReferences[(int)((UIntPtr)(member.token.RID - 1U))] = member;
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x0001949C File Offset: 0x0001769C
		public bool TryGetNestedTypeMapping(TypeDefinition type, out uint[] mapping)
		{
			return this.NestedTypes.TryGetValue(type.token.RID, out mapping);
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x000194B5 File Offset: 0x000176B5
		public void SetNestedTypeMapping(uint type_rid, uint[] mapping)
		{
			this.NestedTypes[type_rid] = mapping;
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x000194C4 File Offset: 0x000176C4
		public void RemoveNestedTypeMapping(TypeDefinition type)
		{
			this.NestedTypes.Remove(type.token.RID);
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x000194DD File Offset: 0x000176DD
		public bool TryGetReverseNestedTypeMapping(TypeDefinition type, out uint declaring)
		{
			return this.ReverseNestedTypes.TryGetValue(type.token.RID, out declaring);
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x000194F6 File Offset: 0x000176F6
		public void SetReverseNestedTypeMapping(uint nested, uint declaring)
		{
			this.ReverseNestedTypes.Add(nested, declaring);
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x00019505 File Offset: 0x00017705
		public void RemoveReverseNestedTypeMapping(TypeDefinition type)
		{
			this.ReverseNestedTypes.Remove(type.token.RID);
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x0001951E File Offset: 0x0001771E
		public bool TryGetInterfaceMapping(TypeDefinition type, out MetadataToken[] mapping)
		{
			return this.Interfaces.TryGetValue(type.token.RID, out mapping);
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x00019537 File Offset: 0x00017737
		public void SetInterfaceMapping(uint type_rid, MetadataToken[] mapping)
		{
			this.Interfaces[type_rid] = mapping;
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x00019546 File Offset: 0x00017746
		public void RemoveInterfaceMapping(TypeDefinition type)
		{
			this.Interfaces.Remove(type.token.RID);
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x0001955F File Offset: 0x0001775F
		public void AddPropertiesRange(uint type_rid, Range range)
		{
			this.Properties.Add(type_rid, range);
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x0001956E File Offset: 0x0001776E
		public bool TryGetPropertiesRange(TypeDefinition type, out Range range)
		{
			return this.Properties.TryGetValue(type.token.RID, out range);
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x00019587 File Offset: 0x00017787
		public void RemovePropertiesRange(TypeDefinition type)
		{
			this.Properties.Remove(type.token.RID);
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x000195A0 File Offset: 0x000177A0
		public void AddEventsRange(uint type_rid, Range range)
		{
			this.Events.Add(type_rid, range);
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x000195AF File Offset: 0x000177AF
		public bool TryGetEventsRange(TypeDefinition type, out Range range)
		{
			return this.Events.TryGetValue(type.token.RID, out range);
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x000195C8 File Offset: 0x000177C8
		public void RemoveEventsRange(TypeDefinition type)
		{
			this.Events.Remove(type.token.RID);
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x000195E1 File Offset: 0x000177E1
		public bool TryGetGenericParameterRanges(IGenericParameterProvider owner, out Range[] ranges)
		{
			return this.GenericParameters.TryGetValue(owner.MetadataToken, out ranges);
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x000195F5 File Offset: 0x000177F5
		public void RemoveGenericParameterRange(IGenericParameterProvider owner)
		{
			this.GenericParameters.Remove(owner.MetadataToken);
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x00019609 File Offset: 0x00017809
		public bool TryGetCustomAttributeRanges(ICustomAttributeProvider owner, out Range[] ranges)
		{
			return this.CustomAttributes.TryGetValue(owner.MetadataToken, out ranges);
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x0001961D File Offset: 0x0001781D
		public void RemoveCustomAttributeRange(ICustomAttributeProvider owner)
		{
			this.CustomAttributes.Remove(owner.MetadataToken);
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00019631 File Offset: 0x00017831
		public bool TryGetSecurityDeclarationRanges(ISecurityDeclarationProvider owner, out Range[] ranges)
		{
			return this.SecurityDeclarations.TryGetValue(owner.MetadataToken, out ranges);
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x00019645 File Offset: 0x00017845
		public void RemoveSecurityDeclarationRange(ISecurityDeclarationProvider owner)
		{
			this.SecurityDeclarations.Remove(owner.MetadataToken);
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x00019659 File Offset: 0x00017859
		public bool TryGetGenericConstraintMapping(GenericParameter generic_parameter, out MetadataToken[] mapping)
		{
			return this.GenericConstraints.TryGetValue(generic_parameter.token.RID, out mapping);
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x00019672 File Offset: 0x00017872
		public void SetGenericConstraintMapping(uint gp_rid, MetadataToken[] mapping)
		{
			this.GenericConstraints[gp_rid] = mapping;
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x00019681 File Offset: 0x00017881
		public void RemoveGenericConstraintMapping(GenericParameter generic_parameter)
		{
			this.GenericConstraints.Remove(generic_parameter.token.RID);
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x0001969A File Offset: 0x0001789A
		public bool TryGetOverrideMapping(MethodDefinition method, out MetadataToken[] mapping)
		{
			return this.Overrides.TryGetValue(method.token.RID, out mapping);
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x000196B3 File Offset: 0x000178B3
		public void SetOverrideMapping(uint rid, MetadataToken[] mapping)
		{
			this.Overrides[rid] = mapping;
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x000196C2 File Offset: 0x000178C2
		public void RemoveOverrideMapping(MethodDefinition method)
		{
			this.Overrides.Remove(method.token.RID);
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x000196DB File Offset: 0x000178DB
		public TypeDefinition GetFieldDeclaringType(uint field_rid)
		{
			return MetadataSystem.BinaryRangeSearch(this.Types, field_rid, true);
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x000196EA File Offset: 0x000178EA
		public TypeDefinition GetMethodDeclaringType(uint method_rid)
		{
			return MetadataSystem.BinaryRangeSearch(this.Types, method_rid, false);
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x000196FC File Offset: 0x000178FC
		private static TypeDefinition BinaryRangeSearch(TypeDefinition[] types, uint rid, bool field)
		{
			int i = 0;
			int num = types.Length - 1;
			while (i <= num)
			{
				int num2 = i + (num - i) / 2;
				TypeDefinition typeDefinition = types[num2];
				Range range = field ? typeDefinition.fields_range : typeDefinition.methods_range;
				if (rid < range.Start)
				{
					num = num2 - 1;
				}
				else
				{
					if (rid < range.Start + range.Length)
					{
						return typeDefinition;
					}
					i = num2 + 1;
				}
			}
			return null;
		}

		// Token: 0x04000480 RID: 1152
		internal AssemblyNameReference[] AssemblyReferences;

		// Token: 0x04000481 RID: 1153
		internal ModuleReference[] ModuleReferences;

		// Token: 0x04000482 RID: 1154
		internal TypeDefinition[] Types;

		// Token: 0x04000483 RID: 1155
		internal TypeReference[] TypeReferences;

		// Token: 0x04000484 RID: 1156
		internal FieldDefinition[] Fields;

		// Token: 0x04000485 RID: 1157
		internal MethodDefinition[] Methods;

		// Token: 0x04000486 RID: 1158
		internal MemberReference[] MemberReferences;

		// Token: 0x04000487 RID: 1159
		internal Dictionary<uint, uint[]> NestedTypes;

		// Token: 0x04000488 RID: 1160
		internal Dictionary<uint, uint> ReverseNestedTypes;

		// Token: 0x04000489 RID: 1161
		internal Dictionary<uint, MetadataToken[]> Interfaces;

		// Token: 0x0400048A RID: 1162
		internal Dictionary<uint, Row<ushort, uint>> ClassLayouts;

		// Token: 0x0400048B RID: 1163
		internal Dictionary<uint, uint> FieldLayouts;

		// Token: 0x0400048C RID: 1164
		internal Dictionary<uint, uint> FieldRVAs;

		// Token: 0x0400048D RID: 1165
		internal Dictionary<MetadataToken, uint> FieldMarshals;

		// Token: 0x0400048E RID: 1166
		internal Dictionary<MetadataToken, Row<ElementType, uint>> Constants;

		// Token: 0x0400048F RID: 1167
		internal Dictionary<uint, MetadataToken[]> Overrides;

		// Token: 0x04000490 RID: 1168
		internal Dictionary<MetadataToken, Range[]> CustomAttributes;

		// Token: 0x04000491 RID: 1169
		internal Dictionary<MetadataToken, Range[]> SecurityDeclarations;

		// Token: 0x04000492 RID: 1170
		internal Dictionary<uint, Range> Events;

		// Token: 0x04000493 RID: 1171
		internal Dictionary<uint, Range> Properties;

		// Token: 0x04000494 RID: 1172
		internal Dictionary<uint, Row<MethodSemanticsAttributes, MetadataToken>> Semantics;

		// Token: 0x04000495 RID: 1173
		internal Dictionary<uint, Row<PInvokeAttributes, uint, uint>> PInvokes;

		// Token: 0x04000496 RID: 1174
		internal Dictionary<MetadataToken, Range[]> GenericParameters;

		// Token: 0x04000497 RID: 1175
		internal Dictionary<uint, MetadataToken[]> GenericConstraints;

		// Token: 0x04000498 RID: 1176
		private static Dictionary<string, Row<ElementType, bool>> primitive_value_types;
	}
}
