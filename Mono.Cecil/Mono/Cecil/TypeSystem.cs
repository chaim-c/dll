using System;
using Mono.Cecil.Metadata;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x020000EF RID: 239
	public abstract class TypeSystem
	{
		// Token: 0x06000981 RID: 2433 RVA: 0x0001D9D6 File Offset: 0x0001BBD6
		private TypeSystem(ModuleDefinition module)
		{
			this.module = module;
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x0001D9E5 File Offset: 0x0001BBE5
		internal static TypeSystem CreateTypeSystem(ModuleDefinition module)
		{
			if (module.IsCorlib())
			{
				return new TypeSystem.CoreTypeSystem(module);
			}
			return new TypeSystem.CommonTypeSystem(module);
		}

		// Token: 0x06000983 RID: 2435
		internal abstract TypeReference LookupType(string @namespace, string name);

		// Token: 0x06000984 RID: 2436 RVA: 0x0001D9FC File Offset: 0x0001BBFC
		private TypeReference LookupSystemType(ref TypeReference reference, string name, ElementType element_type)
		{
			TypeReference result;
			lock (this.module.SyncRoot)
			{
				if (reference != null)
				{
					result = reference;
				}
				else
				{
					TypeReference typeReference = this.LookupType("System", name);
					typeReference.etype = element_type;
					TypeReference typeReference2;
					reference = (typeReference2 = typeReference);
					result = typeReference2;
				}
			}
			return result;
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x0001DA5C File Offset: 0x0001BC5C
		private TypeReference LookupSystemValueType(ref TypeReference typeRef, string name, ElementType element_type)
		{
			TypeReference result;
			lock (this.module.SyncRoot)
			{
				if (typeRef != null)
				{
					result = typeRef;
				}
				else
				{
					TypeReference typeReference = this.LookupType("System", name);
					typeReference.etype = element_type;
					typeReference.IsValueType = true;
					TypeReference typeReference2;
					typeRef = (typeReference2 = typeReference);
					result = typeReference2;
				}
			}
			return result;
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000986 RID: 2438 RVA: 0x0001DAC0 File Offset: 0x0001BCC0
		public IMetadataScope Corlib
		{
			get
			{
				TypeSystem.CommonTypeSystem commonTypeSystem = this as TypeSystem.CommonTypeSystem;
				if (commonTypeSystem == null)
				{
					return this.module;
				}
				return commonTypeSystem.GetCorlibReference();
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000987 RID: 2439 RVA: 0x0001DAE4 File Offset: 0x0001BCE4
		public TypeReference Object
		{
			get
			{
				return this.type_object ?? this.LookupSystemType(ref this.type_object, "Object", ElementType.Object);
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000988 RID: 2440 RVA: 0x0001DB03 File Offset: 0x0001BD03
		public TypeReference Void
		{
			get
			{
				return this.type_void ?? this.LookupSystemType(ref this.type_void, "Void", ElementType.Void);
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000989 RID: 2441 RVA: 0x0001DB21 File Offset: 0x0001BD21
		public TypeReference Boolean
		{
			get
			{
				return this.type_bool ?? this.LookupSystemValueType(ref this.type_bool, "Boolean", ElementType.Boolean);
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x0600098A RID: 2442 RVA: 0x0001DB3F File Offset: 0x0001BD3F
		public TypeReference Char
		{
			get
			{
				return this.type_char ?? this.LookupSystemValueType(ref this.type_char, "Char", ElementType.Char);
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x0600098B RID: 2443 RVA: 0x0001DB5D File Offset: 0x0001BD5D
		public TypeReference SByte
		{
			get
			{
				return this.type_sbyte ?? this.LookupSystemValueType(ref this.type_sbyte, "SByte", ElementType.I1);
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x0600098C RID: 2444 RVA: 0x0001DB7B File Offset: 0x0001BD7B
		public TypeReference Byte
		{
			get
			{
				return this.type_byte ?? this.LookupSystemValueType(ref this.type_byte, "Byte", ElementType.U1);
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x0600098D RID: 2445 RVA: 0x0001DB99 File Offset: 0x0001BD99
		public TypeReference Int16
		{
			get
			{
				return this.type_int16 ?? this.LookupSystemValueType(ref this.type_int16, "Int16", ElementType.I2);
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x0600098E RID: 2446 RVA: 0x0001DBB7 File Offset: 0x0001BDB7
		public TypeReference UInt16
		{
			get
			{
				return this.type_uint16 ?? this.LookupSystemValueType(ref this.type_uint16, "UInt16", ElementType.U2);
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x0600098F RID: 2447 RVA: 0x0001DBD5 File Offset: 0x0001BDD5
		public TypeReference Int32
		{
			get
			{
				return this.type_int32 ?? this.LookupSystemValueType(ref this.type_int32, "Int32", ElementType.I4);
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000990 RID: 2448 RVA: 0x0001DBF3 File Offset: 0x0001BDF3
		public TypeReference UInt32
		{
			get
			{
				return this.type_uint32 ?? this.LookupSystemValueType(ref this.type_uint32, "UInt32", ElementType.U4);
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000991 RID: 2449 RVA: 0x0001DC12 File Offset: 0x0001BE12
		public TypeReference Int64
		{
			get
			{
				return this.type_int64 ?? this.LookupSystemValueType(ref this.type_int64, "Int64", ElementType.I8);
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000992 RID: 2450 RVA: 0x0001DC31 File Offset: 0x0001BE31
		public TypeReference UInt64
		{
			get
			{
				return this.type_uint64 ?? this.LookupSystemValueType(ref this.type_uint64, "UInt64", ElementType.U8);
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000993 RID: 2451 RVA: 0x0001DC50 File Offset: 0x0001BE50
		public TypeReference Single
		{
			get
			{
				return this.type_single ?? this.LookupSystemValueType(ref this.type_single, "Single", ElementType.R4);
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000994 RID: 2452 RVA: 0x0001DC6F File Offset: 0x0001BE6F
		public TypeReference Double
		{
			get
			{
				return this.type_double ?? this.LookupSystemValueType(ref this.type_double, "Double", ElementType.R8);
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000995 RID: 2453 RVA: 0x0001DC8E File Offset: 0x0001BE8E
		public TypeReference IntPtr
		{
			get
			{
				return this.type_intptr ?? this.LookupSystemValueType(ref this.type_intptr, "IntPtr", ElementType.I);
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000996 RID: 2454 RVA: 0x0001DCAD File Offset: 0x0001BEAD
		public TypeReference UIntPtr
		{
			get
			{
				return this.type_uintptr ?? this.LookupSystemValueType(ref this.type_uintptr, "UIntPtr", ElementType.U);
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000997 RID: 2455 RVA: 0x0001DCCC File Offset: 0x0001BECC
		public TypeReference String
		{
			get
			{
				return this.type_string ?? this.LookupSystemType(ref this.type_string, "String", ElementType.String);
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000998 RID: 2456 RVA: 0x0001DCEB File Offset: 0x0001BEEB
		public TypeReference TypedReference
		{
			get
			{
				return this.type_typedref ?? this.LookupSystemValueType(ref this.type_typedref, "TypedReference", ElementType.TypedByRef);
			}
		}

		// Token: 0x04000607 RID: 1543
		private readonly ModuleDefinition module;

		// Token: 0x04000608 RID: 1544
		private TypeReference type_object;

		// Token: 0x04000609 RID: 1545
		private TypeReference type_void;

		// Token: 0x0400060A RID: 1546
		private TypeReference type_bool;

		// Token: 0x0400060B RID: 1547
		private TypeReference type_char;

		// Token: 0x0400060C RID: 1548
		private TypeReference type_sbyte;

		// Token: 0x0400060D RID: 1549
		private TypeReference type_byte;

		// Token: 0x0400060E RID: 1550
		private TypeReference type_int16;

		// Token: 0x0400060F RID: 1551
		private TypeReference type_uint16;

		// Token: 0x04000610 RID: 1552
		private TypeReference type_int32;

		// Token: 0x04000611 RID: 1553
		private TypeReference type_uint32;

		// Token: 0x04000612 RID: 1554
		private TypeReference type_int64;

		// Token: 0x04000613 RID: 1555
		private TypeReference type_uint64;

		// Token: 0x04000614 RID: 1556
		private TypeReference type_single;

		// Token: 0x04000615 RID: 1557
		private TypeReference type_double;

		// Token: 0x04000616 RID: 1558
		private TypeReference type_intptr;

		// Token: 0x04000617 RID: 1559
		private TypeReference type_uintptr;

		// Token: 0x04000618 RID: 1560
		private TypeReference type_string;

		// Token: 0x04000619 RID: 1561
		private TypeReference type_typedref;

		// Token: 0x020000F0 RID: 240
		private sealed class CoreTypeSystem : TypeSystem
		{
			// Token: 0x06000999 RID: 2457 RVA: 0x0001DD0A File Offset: 0x0001BF0A
			public CoreTypeSystem(ModuleDefinition module) : base(module)
			{
			}

			// Token: 0x0600099A RID: 2458 RVA: 0x0001DD14 File Offset: 0x0001BF14
			internal override TypeReference LookupType(string @namespace, string name)
			{
				TypeReference typeReference = this.LookupTypeDefinition(@namespace, name) ?? this.LookupTypeForwarded(@namespace, name);
				if (typeReference != null)
				{
					return typeReference;
				}
				throw new NotSupportedException();
			}

			// Token: 0x0600099B RID: 2459 RVA: 0x0001DDA8 File Offset: 0x0001BFA8
			private TypeReference LookupTypeDefinition(string @namespace, string name)
			{
				MetadataSystem metadataSystem = this.module.MetadataSystem;
				if (metadataSystem.Types == null)
				{
					TypeSystem.CoreTypeSystem.Initialize(this.module.Types);
				}
				return this.module.Read<Row<string, string>, TypeDefinition>(new Row<string, string>(@namespace, name), delegate(Row<string, string> row, MetadataReader reader)
				{
					TypeDefinition[] types = reader.metadata.Types;
					for (int i = 0; i < types.Length; i++)
					{
						if (types[i] == null)
						{
							types[i] = reader.GetTypeDefinition((uint)(i + 1));
						}
						TypeDefinition typeDefinition = types[i];
						if (typeDefinition.Name == row.Col2 && typeDefinition.Namespace == row.Col1)
						{
							return typeDefinition;
						}
					}
					return null;
				});
			}

			// Token: 0x0600099C RID: 2460 RVA: 0x0001DE08 File Offset: 0x0001C008
			private TypeReference LookupTypeForwarded(string @namespace, string name)
			{
				if (!this.module.HasExportedTypes)
				{
					return null;
				}
				Collection<ExportedType> exportedTypes = this.module.ExportedTypes;
				for (int i = 0; i < exportedTypes.Count; i++)
				{
					ExportedType exportedType = exportedTypes[i];
					if (exportedType.Name == name && exportedType.Namespace == @namespace)
					{
						return exportedType.CreateReference();
					}
				}
				return null;
			}

			// Token: 0x0600099D RID: 2461 RVA: 0x0001DE6D File Offset: 0x0001C06D
			private static void Initialize(object obj)
			{
			}
		}

		// Token: 0x020000F1 RID: 241
		private sealed class CommonTypeSystem : TypeSystem
		{
			// Token: 0x0600099F RID: 2463 RVA: 0x0001DE6F File Offset: 0x0001C06F
			public CommonTypeSystem(ModuleDefinition module) : base(module)
			{
			}

			// Token: 0x060009A0 RID: 2464 RVA: 0x0001DE78 File Offset: 0x0001C078
			internal override TypeReference LookupType(string @namespace, string name)
			{
				return this.CreateTypeReference(@namespace, name);
			}

			// Token: 0x060009A1 RID: 2465 RVA: 0x0001DE90 File Offset: 0x0001C090
			public AssemblyNameReference GetCorlibReference()
			{
				if (this.corlib != null)
				{
					return this.corlib;
				}
				Collection<AssemblyNameReference> assemblyReferences = this.module.AssemblyReferences;
				for (int i = 0; i < assemblyReferences.Count; i++)
				{
					AssemblyNameReference assemblyNameReference = assemblyReferences[i];
					if (assemblyNameReference.Name == "mscorlib")
					{
						return this.corlib = assemblyNameReference;
					}
				}
				this.corlib = new AssemblyNameReference
				{
					Name = "mscorlib",
					Version = this.GetCorlibVersion(),
					PublicKeyToken = new byte[]
					{
						183,
						122,
						92,
						86,
						25,
						52,
						224,
						137
					}
				};
				assemblyReferences.Add(this.corlib);
				return this.corlib;
			}

			// Token: 0x060009A2 RID: 2466 RVA: 0x0001DF40 File Offset: 0x0001C140
			private Version GetCorlibVersion()
			{
				switch (this.module.Runtime)
				{
				case TargetRuntime.Net_1_0:
				case TargetRuntime.Net_1_1:
					return new Version(1, 0, 0, 0);
				case TargetRuntime.Net_2_0:
					return new Version(2, 0, 0, 0);
				case TargetRuntime.Net_4_0:
					return new Version(4, 0, 0, 0);
				default:
					throw new NotSupportedException();
				}
			}

			// Token: 0x060009A3 RID: 2467 RVA: 0x0001DF94 File Offset: 0x0001C194
			private TypeReference CreateTypeReference(string @namespace, string name)
			{
				return new TypeReference(@namespace, name, this.module, this.GetCorlibReference());
			}

			// Token: 0x0400061B RID: 1563
			private AssemblyNameReference corlib;
		}
	}
}
