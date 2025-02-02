using System;
using System.Collections.Generic;
using System.IO;
using Mono.Cecil.Cil;
using Mono.Cecil.Metadata;
using Mono.Cecil.PE;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x0200008C RID: 140
	internal sealed class MetadataBuilder
	{
		// Token: 0x06000464 RID: 1124 RVA: 0x00012128 File Offset: 0x00010328
		public MetadataBuilder(ModuleDefinition module, string fq_name, ISymbolWriterProvider symbol_writer_provider, ISymbolWriter symbol_writer)
		{
			this.module = module;
			this.text_map = this.CreateTextMap();
			this.fq_name = fq_name;
			this.symbol_writer_provider = symbol_writer_provider;
			this.symbol_writer = symbol_writer;
			this.write_symbols = (symbol_writer != null);
			this.code = new CodeWriter(this);
			this.data = new DataBuffer();
			this.resources = new ResourceBuffer();
			this.string_heap = new StringHeapBuffer();
			this.user_string_heap = new UserStringHeapBuffer();
			this.blob_heap = new BlobHeapBuffer();
			this.table_heap = new TableHeapBuffer(module, this);
			this.type_ref_table = this.GetTable<TypeRefTable>(Table.TypeRef);
			this.type_def_table = this.GetTable<TypeDefTable>(Table.TypeDef);
			this.field_table = this.GetTable<FieldTable>(Table.Field);
			this.method_table = this.GetTable<MethodTable>(Table.Method);
			this.param_table = this.GetTable<ParamTable>(Table.Param);
			this.iface_impl_table = this.GetTable<InterfaceImplTable>(Table.InterfaceImpl);
			this.member_ref_table = this.GetTable<MemberRefTable>(Table.MemberRef);
			this.constant_table = this.GetTable<ConstantTable>(Table.Constant);
			this.custom_attribute_table = this.GetTable<CustomAttributeTable>(Table.CustomAttribute);
			this.declsec_table = this.GetTable<DeclSecurityTable>(Table.DeclSecurity);
			this.standalone_sig_table = this.GetTable<StandAloneSigTable>(Table.StandAloneSig);
			this.event_map_table = this.GetTable<EventMapTable>(Table.EventMap);
			this.event_table = this.GetTable<EventTable>(Table.Event);
			this.property_map_table = this.GetTable<PropertyMapTable>(Table.PropertyMap);
			this.property_table = this.GetTable<PropertyTable>(Table.Property);
			this.typespec_table = this.GetTable<TypeSpecTable>(Table.TypeSpec);
			this.method_spec_table = this.GetTable<MethodSpecTable>(Table.MethodSpec);
			RowEqualityComparer comparer = new RowEqualityComparer();
			this.type_ref_map = new Dictionary<Row<uint, uint, uint>, MetadataToken>(comparer);
			this.type_spec_map = new Dictionary<uint, MetadataToken>();
			this.member_ref_map = new Dictionary<Row<uint, uint, uint>, MetadataToken>(comparer);
			this.method_spec_map = new Dictionary<Row<uint, uint>, MetadataToken>(comparer);
			this.generic_parameters = new Collection<GenericParameter>();
			if (this.write_symbols)
			{
				this.method_def_map = new Dictionary<MetadataToken, MetadataToken>();
			}
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x00012328 File Offset: 0x00010528
		private TextMap CreateTextMap()
		{
			TextMap textMap = new TextMap();
			textMap.AddMap(TextSegment.ImportAddressTable, (this.module.Architecture == TargetArchitecture.I386) ? 8 : 0);
			textMap.AddMap(TextSegment.CLIHeader, 72, 8);
			return textMap;
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0001235E File Offset: 0x0001055E
		private TTable GetTable<TTable>(Table table) where TTable : MetadataTable, new()
		{
			return this.table_heap.GetTable<TTable>(table);
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0001236C File Offset: 0x0001056C
		private uint GetStringIndex(string @string)
		{
			if (string.IsNullOrEmpty(@string))
			{
				return 0U;
			}
			return this.string_heap.GetStringIndex(@string);
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00012384 File Offset: 0x00010584
		private uint GetBlobIndex(ByteBuffer blob)
		{
			if (blob.length == 0)
			{
				return 0U;
			}
			return this.blob_heap.GetBlobIndex(blob);
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x0001239C File Offset: 0x0001059C
		private uint GetBlobIndex(byte[] blob)
		{
			if (blob.IsNullOrEmpty<byte>())
			{
				return 0U;
			}
			return this.GetBlobIndex(new ByteBuffer(blob));
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x000123B4 File Offset: 0x000105B4
		public void BuildMetadata()
		{
			this.BuildModule();
			this.table_heap.WriteTableHeap();
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x000123C8 File Offset: 0x000105C8
		private void BuildModule()
		{
			ModuleTable table = this.GetTable<ModuleTable>(Table.Module);
			table.row = this.GetStringIndex(this.module.Name);
			AssemblyDefinition assembly = this.module.Assembly;
			if (assembly != null)
			{
				this.BuildAssembly();
			}
			if (this.module.HasAssemblyReferences)
			{
				this.AddAssemblyReferences();
			}
			if (this.module.HasModuleReferences)
			{
				this.AddModuleReferences();
			}
			if (this.module.HasResources)
			{
				this.AddResources();
			}
			if (this.module.HasExportedTypes)
			{
				this.AddExportedTypes();
			}
			this.BuildTypes();
			if (assembly != null)
			{
				if (assembly.HasCustomAttributes)
				{
					this.AddCustomAttributes(assembly);
				}
				if (assembly.HasSecurityDeclarations)
				{
					this.AddSecurityDeclarations(assembly);
				}
			}
			if (this.module.HasCustomAttributes)
			{
				this.AddCustomAttributes(this.module);
			}
			if (this.module.EntryPoint != null)
			{
				this.entry_point = this.LookupToken(this.module.EntryPoint);
			}
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x000124BC File Offset: 0x000106BC
		private void BuildAssembly()
		{
			AssemblyDefinition assembly = this.module.Assembly;
			AssemblyNameDefinition name = assembly.Name;
			AssemblyTable table = this.GetTable<AssemblyTable>(Table.Assembly);
			table.row = new Row<AssemblyHashAlgorithm, ushort, ushort, ushort, ushort, AssemblyAttributes, uint, uint, uint>(name.HashAlgorithm, (ushort)name.Version.Major, (ushort)name.Version.Minor, (ushort)name.Version.Build, (ushort)name.Version.Revision, name.Attributes, this.GetBlobIndex(name.PublicKey), this.GetStringIndex(name.Name), this.GetStringIndex(name.Culture));
			if (assembly.Modules.Count > 1)
			{
				this.BuildModules();
			}
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00012564 File Offset: 0x00010764
		private void BuildModules()
		{
			Collection<ModuleDefinition> modules = this.module.Assembly.Modules;
			FileTable table = this.GetTable<FileTable>(Table.File);
			for (int i = 0; i < modules.Count; i++)
			{
				ModuleDefinition moduleDefinition = modules[i];
				if (!moduleDefinition.IsMain)
				{
					WriterParameters parameters = new WriterParameters
					{
						SymbolWriterProvider = this.symbol_writer_provider
					};
					string moduleFileName = this.GetModuleFileName(moduleDefinition.Name);
					moduleDefinition.Write(moduleFileName, parameters);
					byte[] blob = CryptoService.ComputeHash(moduleFileName);
					table.AddRow(new Row<FileAttributes, uint, uint>(FileAttributes.ContainsMetaData, this.GetStringIndex(moduleDefinition.Name), this.GetBlobIndex(blob)));
				}
			}
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00012608 File Offset: 0x00010808
		private string GetModuleFileName(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new NotSupportedException();
			}
			string directoryName = Path.GetDirectoryName(this.fq_name);
			return Path.Combine(directoryName, name);
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00012638 File Offset: 0x00010838
		private void AddAssemblyReferences()
		{
			Collection<AssemblyNameReference> assemblyReferences = this.module.AssemblyReferences;
			AssemblyRefTable table = this.GetTable<AssemblyRefTable>(Table.AssemblyRef);
			for (int i = 0; i < assemblyReferences.Count; i++)
			{
				AssemblyNameReference assemblyNameReference = assemblyReferences[i];
				byte[] blob = assemblyNameReference.PublicKey.IsNullOrEmpty<byte>() ? assemblyNameReference.PublicKeyToken : assemblyNameReference.PublicKey;
				Version version = assemblyNameReference.Version;
				int rid = table.AddRow(new Row<ushort, ushort, ushort, ushort, AssemblyAttributes, uint, uint, uint, uint>((ushort)version.Major, (ushort)version.Minor, (ushort)version.Build, (ushort)version.Revision, assemblyNameReference.Attributes, this.GetBlobIndex(blob), this.GetStringIndex(assemblyNameReference.Name), this.GetStringIndex(assemblyNameReference.Culture), this.GetBlobIndex(assemblyNameReference.Hash)));
				assemblyNameReference.token = new MetadataToken(TokenType.AssemblyRef, rid);
			}
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x00012710 File Offset: 0x00010910
		private void AddModuleReferences()
		{
			Collection<ModuleReference> moduleReferences = this.module.ModuleReferences;
			ModuleRefTable table = this.GetTable<ModuleRefTable>(Table.ModuleRef);
			for (int i = 0; i < moduleReferences.Count; i++)
			{
				ModuleReference moduleReference = moduleReferences[i];
				moduleReference.token = new MetadataToken(TokenType.ModuleRef, table.AddRow(this.GetStringIndex(moduleReference.Name)));
			}
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00012770 File Offset: 0x00010970
		private void AddResources()
		{
			Collection<Resource> collection = this.module.Resources;
			ManifestResourceTable table = this.GetTable<ManifestResourceTable>(Table.ManifestResource);
			for (int i = 0; i < collection.Count; i++)
			{
				Resource resource = collection[i];
				Row<uint, ManifestResourceAttributes, uint, uint> row = new Row<uint, ManifestResourceAttributes, uint, uint>(0U, resource.Attributes, this.GetStringIndex(resource.Name), 0U);
				switch (resource.ResourceType)
				{
				case ResourceType.Linked:
					row.Col4 = CodedIndex.Implementation.CompressMetadataToken(new MetadataToken(TokenType.File, this.AddLinkedResource((LinkedResource)resource)));
					break;
				case ResourceType.Embedded:
					row.Col1 = this.AddEmbeddedResource((EmbeddedResource)resource);
					break;
				case ResourceType.AssemblyLinked:
					row.Col4 = CodedIndex.Implementation.CompressMetadataToken(((AssemblyLinkedResource)resource).Assembly.MetadataToken);
					break;
				default:
					throw new NotSupportedException();
				}
				table.AddRow(row);
			}
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00012854 File Offset: 0x00010A54
		private uint AddLinkedResource(LinkedResource resource)
		{
			FileTable table = this.GetTable<FileTable>(Table.File);
			byte[] blob = resource.Hash.IsNullOrEmpty<byte>() ? CryptoService.ComputeHash(resource.File) : resource.Hash;
			return (uint)table.AddRow(new Row<FileAttributes, uint, uint>(FileAttributes.ContainsNoMetaData, this.GetStringIndex(resource.File), this.GetBlobIndex(blob)));
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x000128AA File Offset: 0x00010AAA
		private uint AddEmbeddedResource(EmbeddedResource resource)
		{
			return this.resources.AddResource(resource.GetResourceData());
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x000128C0 File Offset: 0x00010AC0
		private void AddExportedTypes()
		{
			Collection<ExportedType> exportedTypes = this.module.ExportedTypes;
			ExportedTypeTable table = this.GetTable<ExportedTypeTable>(Table.ExportedType);
			for (int i = 0; i < exportedTypes.Count; i++)
			{
				ExportedType exportedType = exportedTypes[i];
				int rid = table.AddRow(new Row<TypeAttributes, uint, uint, uint, uint>(exportedType.Attributes, (uint)exportedType.Identifier, this.GetStringIndex(exportedType.Name), this.GetStringIndex(exportedType.Namespace), MetadataBuilder.MakeCodedRID(this.GetExportedTypeScope(exportedType), CodedIndex.Implementation)));
				exportedType.token = new MetadataToken(TokenType.ExportedType, rid);
			}
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0001294C File Offset: 0x00010B4C
		private MetadataToken GetExportedTypeScope(ExportedType exported_type)
		{
			if (exported_type.DeclaringType != null)
			{
				return exported_type.DeclaringType.MetadataToken;
			}
			IMetadataScope scope = exported_type.Scope;
			TokenType tokenType = scope.MetadataToken.TokenType;
			if (tokenType != TokenType.ModuleRef)
			{
				if (tokenType == TokenType.AssemblyRef)
				{
					return scope.MetadataToken;
				}
			}
			else
			{
				FileTable table = this.GetTable<FileTable>(Table.File);
				for (int i = 0; i < table.length; i++)
				{
					if (table.rows[i].Col2 == this.GetStringIndex(scope.Name))
					{
						return new MetadataToken(TokenType.File, i + 1);
					}
				}
			}
			throw new NotSupportedException();
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x000129E9 File Offset: 0x00010BE9
		private void BuildTypes()
		{
			if (!this.module.HasTypes)
			{
				return;
			}
			this.AttachTokens();
			this.AddTypeDefs();
			this.AddGenericParameters();
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00012A0C File Offset: 0x00010C0C
		private void AttachTokens()
		{
			Collection<TypeDefinition> types = this.module.Types;
			for (int i = 0; i < types.Count; i++)
			{
				this.AttachTypeDefToken(types[i]);
			}
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x00012A44 File Offset: 0x00010C44
		private void AttachTypeDefToken(TypeDefinition type)
		{
			type.token = new MetadataToken(TokenType.TypeDef, this.type_rid++);
			type.fields_range.Start = this.field_rid;
			type.methods_range.Start = this.method_rid;
			if (type.HasFields)
			{
				this.AttachFieldsDefToken(type);
			}
			if (type.HasMethods)
			{
				this.AttachMethodsDefToken(type);
			}
			if (type.HasNestedTypes)
			{
				this.AttachNestedTypesDefToken(type);
			}
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00012AC4 File Offset: 0x00010CC4
		private void AttachNestedTypesDefToken(TypeDefinition type)
		{
			Collection<TypeDefinition> nestedTypes = type.NestedTypes;
			for (int i = 0; i < nestedTypes.Count; i++)
			{
				this.AttachTypeDefToken(nestedTypes[i]);
			}
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x00012AF8 File Offset: 0x00010CF8
		private void AttachFieldsDefToken(TypeDefinition type)
		{
			Collection<FieldDefinition> fields = type.Fields;
			type.fields_range.Length = (uint)fields.Count;
			for (int i = 0; i < fields.Count; i++)
			{
				fields[i].token = new MetadataToken(TokenType.Field, this.field_rid++);
			}
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00012B58 File Offset: 0x00010D58
		private void AttachMethodsDefToken(TypeDefinition type)
		{
			Collection<MethodDefinition> methods = type.Methods;
			type.methods_range.Length = (uint)methods.Count;
			for (int i = 0; i < methods.Count; i++)
			{
				MethodDefinition methodDefinition = methods[i];
				MetadataToken metadataToken = new MetadataToken(TokenType.Method, this.method_rid++);
				if (this.write_symbols && methodDefinition.token != MetadataToken.Zero)
				{
					this.method_def_map.Add(metadataToken, methodDefinition.token);
				}
				methodDefinition.token = metadataToken;
			}
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00012BE8 File Offset: 0x00010DE8
		public bool TryGetOriginalMethodToken(MetadataToken new_token, out MetadataToken original)
		{
			return this.method_def_map.TryGetValue(new_token, out original);
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00012BF7 File Offset: 0x00010DF7
		private MetadataToken GetTypeToken(TypeReference type)
		{
			if (type == null)
			{
				return MetadataToken.Zero;
			}
			if (type.IsDefinition)
			{
				return type.token;
			}
			if (type.IsTypeSpecification())
			{
				return this.GetTypeSpecToken(type);
			}
			return this.GetTypeRefToken(type);
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x00012C28 File Offset: 0x00010E28
		private MetadataToken GetTypeSpecToken(TypeReference type)
		{
			uint blobIndex = this.GetBlobIndex(this.GetTypeSpecSignature(type));
			MetadataToken result;
			if (this.type_spec_map.TryGetValue(blobIndex, out result))
			{
				return result;
			}
			return this.AddTypeSpecification(type, blobIndex);
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00012C60 File Offset: 0x00010E60
		private MetadataToken AddTypeSpecification(TypeReference type, uint row)
		{
			type.token = new MetadataToken(TokenType.TypeSpec, this.typespec_table.AddRow(row));
			MetadataToken token = type.token;
			this.type_spec_map.Add(row, token);
			return token;
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00012CA0 File Offset: 0x00010EA0
		private MetadataToken GetTypeRefToken(TypeReference type)
		{
			Row<uint, uint, uint> row = this.CreateTypeRefRow(type);
			MetadataToken result;
			if (this.type_ref_map.TryGetValue(row, out result))
			{
				return result;
			}
			return this.AddTypeReference(type, row);
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00012CD0 File Offset: 0x00010ED0
		private Row<uint, uint, uint> CreateTypeRefRow(TypeReference type)
		{
			MetadataToken scopeToken = this.GetScopeToken(type);
			return new Row<uint, uint, uint>(MetadataBuilder.MakeCodedRID(scopeToken, CodedIndex.ResolutionScope), this.GetStringIndex(type.Name), this.GetStringIndex(type.Namespace));
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x00012D0C File Offset: 0x00010F0C
		private MetadataToken GetScopeToken(TypeReference type)
		{
			if (type.IsNested)
			{
				return this.GetTypeRefToken(type.DeclaringType);
			}
			IMetadataScope scope = type.Scope;
			if (scope == null)
			{
				return MetadataToken.Zero;
			}
			return scope.MetadataToken;
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00012D44 File Offset: 0x00010F44
		private static uint MakeCodedRID(IMetadataTokenProvider provider, CodedIndex index)
		{
			return MetadataBuilder.MakeCodedRID(provider.MetadataToken, index);
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00012D52 File Offset: 0x00010F52
		private static uint MakeCodedRID(MetadataToken token, CodedIndex index)
		{
			return index.CompressMetadataToken(token);
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00012D5C File Offset: 0x00010F5C
		private MetadataToken AddTypeReference(TypeReference type, Row<uint, uint, uint> row)
		{
			type.token = new MetadataToken(TokenType.TypeRef, this.type_ref_table.AddRow(row));
			MetadataToken token = type.token;
			this.type_ref_map.Add(row, token);
			return token;
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00012D9C File Offset: 0x00010F9C
		private void AddTypeDefs()
		{
			Collection<TypeDefinition> types = this.module.Types;
			for (int i = 0; i < types.Count; i++)
			{
				this.AddType(types[i]);
			}
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00012DD4 File Offset: 0x00010FD4
		private void AddType(TypeDefinition type)
		{
			this.type_def_table.AddRow(new Row<TypeAttributes, uint, uint, uint, uint, uint>(type.Attributes, this.GetStringIndex(type.Name), this.GetStringIndex(type.Namespace), MetadataBuilder.MakeCodedRID(this.GetTypeToken(type.BaseType), CodedIndex.TypeDefOrRef), type.fields_range.Start, type.methods_range.Start));
			if (type.HasGenericParameters)
			{
				this.AddGenericParameters(type);
			}
			if (type.HasInterfaces)
			{
				this.AddInterfaces(type);
			}
			if (type.HasLayoutInfo)
			{
				this.AddLayoutInfo(type);
			}
			if (type.HasFields)
			{
				this.AddFields(type);
			}
			if (type.HasMethods)
			{
				this.AddMethods(type);
			}
			if (type.HasProperties)
			{
				this.AddProperties(type);
			}
			if (type.HasEvents)
			{
				this.AddEvents(type);
			}
			if (type.HasCustomAttributes)
			{
				this.AddCustomAttributes(type);
			}
			if (type.HasSecurityDeclarations)
			{
				this.AddSecurityDeclarations(type);
			}
			if (type.HasNestedTypes)
			{
				this.AddNestedTypes(type);
			}
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00012ED0 File Offset: 0x000110D0
		private void AddGenericParameters(IGenericParameterProvider owner)
		{
			Collection<GenericParameter> genericParameters = owner.GenericParameters;
			for (int i = 0; i < genericParameters.Count; i++)
			{
				this.generic_parameters.Add(genericParameters[i]);
			}
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x00012F08 File Offset: 0x00011108
		private void AddGenericParameters()
		{
			GenericParameter[] items = this.generic_parameters.items;
			int size = this.generic_parameters.size;
			Array.Sort<GenericParameter>(items, 0, size, new MetadataBuilder.GenericParameterComparer());
			GenericParamTable table = this.GetTable<GenericParamTable>(Table.GenericParam);
			GenericParamConstraintTable table2 = this.GetTable<GenericParamConstraintTable>(Table.GenericParamConstraint);
			for (int i = 0; i < size; i++)
			{
				GenericParameter genericParameter = items[i];
				int rid = table.AddRow(new Row<ushort, GenericParameterAttributes, uint, uint>((ushort)genericParameter.Position, genericParameter.Attributes, MetadataBuilder.MakeCodedRID(genericParameter.Owner, CodedIndex.TypeOrMethodDef), this.GetStringIndex(genericParameter.Name)));
				genericParameter.token = new MetadataToken(TokenType.GenericParam, rid);
				if (genericParameter.HasConstraints)
				{
					this.AddConstraints(genericParameter, table2);
				}
				if (genericParameter.HasCustomAttributes)
				{
					this.AddCustomAttributes(genericParameter);
				}
			}
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00012FD0 File Offset: 0x000111D0
		private void AddConstraints(GenericParameter generic_parameter, GenericParamConstraintTable table)
		{
			Collection<TypeReference> constraints = generic_parameter.Constraints;
			uint rid = generic_parameter.token.RID;
			for (int i = 0; i < constraints.Count; i++)
			{
				table.AddRow(new Row<uint, uint>(rid, MetadataBuilder.MakeCodedRID(this.GetTypeToken(constraints[i]), CodedIndex.TypeDefOrRef)));
			}
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00013024 File Offset: 0x00011224
		private void AddInterfaces(TypeDefinition type)
		{
			Collection<TypeReference> interfaces = type.Interfaces;
			uint rid = type.token.RID;
			for (int i = 0; i < interfaces.Count; i++)
			{
				this.iface_impl_table.AddRow(new Row<uint, uint>(rid, MetadataBuilder.MakeCodedRID(this.GetTypeToken(interfaces[i]), CodedIndex.TypeDefOrRef)));
			}
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0001307C File Offset: 0x0001127C
		private void AddLayoutInfo(TypeDefinition type)
		{
			ClassLayoutTable table = this.GetTable<ClassLayoutTable>(Table.ClassLayout);
			table.AddRow(new Row<ushort, uint, uint>((ushort)type.PackingSize, (uint)type.ClassSize, type.token.RID));
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x000130B8 File Offset: 0x000112B8
		private void AddNestedTypes(TypeDefinition type)
		{
			Collection<TypeDefinition> nestedTypes = type.NestedTypes;
			NestedClassTable table = this.GetTable<NestedClassTable>(Table.NestedClass);
			for (int i = 0; i < nestedTypes.Count; i++)
			{
				TypeDefinition typeDefinition = nestedTypes[i];
				this.AddType(typeDefinition);
				table.AddRow(new Row<uint, uint>(typeDefinition.token.RID, type.token.RID));
			}
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00013118 File Offset: 0x00011318
		private void AddFields(TypeDefinition type)
		{
			Collection<FieldDefinition> fields = type.Fields;
			for (int i = 0; i < fields.Count; i++)
			{
				this.AddField(fields[i]);
			}
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x0001314C File Offset: 0x0001134C
		private void AddField(FieldDefinition field)
		{
			this.field_table.AddRow(new Row<FieldAttributes, uint, uint>(field.Attributes, this.GetStringIndex(field.Name), this.GetBlobIndex(this.GetFieldSignature(field))));
			if (!field.InitialValue.IsNullOrEmpty<byte>())
			{
				this.AddFieldRVA(field);
			}
			if (field.HasLayoutInfo)
			{
				this.AddFieldLayout(field);
			}
			if (field.HasCustomAttributes)
			{
				this.AddCustomAttributes(field);
			}
			if (field.HasConstant)
			{
				this.AddConstant(field, field.FieldType);
			}
			if (field.HasMarshalInfo)
			{
				this.AddMarshalInfo(field);
			}
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x000131E0 File Offset: 0x000113E0
		private void AddFieldRVA(FieldDefinition field)
		{
			FieldRVATable table = this.GetTable<FieldRVATable>(Table.FieldRVA);
			table.AddRow(new Row<uint, uint>(this.data.AddData(field.InitialValue), field.token.RID));
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00013220 File Offset: 0x00011420
		private void AddFieldLayout(FieldDefinition field)
		{
			FieldLayoutTable table = this.GetTable<FieldLayoutTable>(Table.FieldLayout);
			table.AddRow(new Row<uint, uint>((uint)field.Offset, field.token.RID));
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00013254 File Offset: 0x00011454
		private void AddMethods(TypeDefinition type)
		{
			Collection<MethodDefinition> methods = type.Methods;
			for (int i = 0; i < methods.Count; i++)
			{
				this.AddMethod(methods[i]);
			}
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00013288 File Offset: 0x00011488
		private void AddMethod(MethodDefinition method)
		{
			this.method_table.AddRow(new Row<uint, MethodImplAttributes, MethodAttributes, uint, uint, uint>(method.HasBody ? this.code.WriteMethodBody(method) : 0U, method.ImplAttributes, method.Attributes, this.GetStringIndex(method.Name), this.GetBlobIndex(this.GetMethodSignature(method)), this.param_rid));
			this.AddParameters(method);
			if (method.HasGenericParameters)
			{
				this.AddGenericParameters(method);
			}
			if (method.IsPInvokeImpl)
			{
				this.AddPInvokeInfo(method);
			}
			if (method.HasCustomAttributes)
			{
				this.AddCustomAttributes(method);
			}
			if (method.HasSecurityDeclarations)
			{
				this.AddSecurityDeclarations(method);
			}
			if (method.HasOverrides)
			{
				this.AddOverrides(method);
			}
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0001333C File Offset: 0x0001153C
		private void AddParameters(MethodDefinition method)
		{
			ParameterDefinition parameter = method.MethodReturnType.parameter;
			if (parameter != null && MetadataBuilder.RequiresParameterRow(parameter))
			{
				this.AddParameter(0, parameter, this.param_table);
			}
			if (!method.HasParameters)
			{
				return;
			}
			Collection<ParameterDefinition> parameters = method.Parameters;
			for (int i = 0; i < parameters.Count; i++)
			{
				ParameterDefinition parameter2 = parameters[i];
				if (MetadataBuilder.RequiresParameterRow(parameter2))
				{
					this.AddParameter((ushort)(i + 1), parameter2, this.param_table);
				}
			}
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x000133B0 File Offset: 0x000115B0
		private void AddPInvokeInfo(MethodDefinition method)
		{
			PInvokeInfo pinvokeInfo = method.PInvokeInfo;
			if (pinvokeInfo == null)
			{
				return;
			}
			ImplMapTable table = this.GetTable<ImplMapTable>(Table.ImplMap);
			table.AddRow(new Row<PInvokeAttributes, uint, uint, uint>(pinvokeInfo.Attributes, MetadataBuilder.MakeCodedRID(method, CodedIndex.MemberForwarded), this.GetStringIndex(pinvokeInfo.EntryPoint), pinvokeInfo.Module.MetadataToken.RID));
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0001340C File Offset: 0x0001160C
		private void AddOverrides(MethodDefinition method)
		{
			Collection<MethodReference> overrides = method.Overrides;
			MethodImplTable table = this.GetTable<MethodImplTable>(Table.MethodImpl);
			for (int i = 0; i < overrides.Count; i++)
			{
				table.AddRow(new Row<uint, uint, uint>(method.DeclaringType.token.RID, MetadataBuilder.MakeCodedRID(method, CodedIndex.MethodDefOrRef), MetadataBuilder.MakeCodedRID(this.LookupToken(overrides[i]), CodedIndex.MethodDefOrRef)));
			}
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00013470 File Offset: 0x00011670
		private static bool RequiresParameterRow(ParameterDefinition parameter)
		{
			return !string.IsNullOrEmpty(parameter.Name) || parameter.Attributes != ParameterAttributes.None || parameter.HasMarshalInfo || parameter.HasConstant || parameter.HasCustomAttributes;
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x000134A0 File Offset: 0x000116A0
		private void AddParameter(ushort sequence, ParameterDefinition parameter, ParamTable table)
		{
			table.AddRow(new Row<ParameterAttributes, ushort, uint>(parameter.Attributes, sequence, this.GetStringIndex(parameter.Name)));
			parameter.token = new MetadataToken(TokenType.Param, this.param_rid++);
			if (parameter.HasCustomAttributes)
			{
				this.AddCustomAttributes(parameter);
			}
			if (parameter.HasConstant)
			{
				this.AddConstant(parameter, parameter.ParameterType);
			}
			if (parameter.HasMarshalInfo)
			{
				this.AddMarshalInfo(parameter);
			}
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00013520 File Offset: 0x00011720
		private void AddMarshalInfo(IMarshalInfoProvider owner)
		{
			FieldMarshalTable table = this.GetTable<FieldMarshalTable>(Table.FieldMarshal);
			table.AddRow(new Row<uint, uint>(MetadataBuilder.MakeCodedRID(owner, CodedIndex.HasFieldMarshal), this.GetBlobIndex(this.GetMarshalInfoSignature(owner))));
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00013558 File Offset: 0x00011758
		private void AddProperties(TypeDefinition type)
		{
			Collection<PropertyDefinition> properties = type.Properties;
			this.property_map_table.AddRow(new Row<uint, uint>(type.token.RID, this.property_rid));
			for (int i = 0; i < properties.Count; i++)
			{
				this.AddProperty(properties[i]);
			}
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x000135AC File Offset: 0x000117AC
		private void AddProperty(PropertyDefinition property)
		{
			this.property_table.AddRow(new Row<PropertyAttributes, uint, uint>(property.Attributes, this.GetStringIndex(property.Name), this.GetBlobIndex(this.GetPropertySignature(property))));
			property.token = new MetadataToken(TokenType.Property, this.property_rid++);
			MethodDefinition methodDefinition = property.GetMethod;
			if (methodDefinition != null)
			{
				this.AddSemantic(MethodSemanticsAttributes.Getter, property, methodDefinition);
			}
			methodDefinition = property.SetMethod;
			if (methodDefinition != null)
			{
				this.AddSemantic(MethodSemanticsAttributes.Setter, property, methodDefinition);
			}
			if (property.HasOtherMethods)
			{
				this.AddOtherSemantic(property, property.OtherMethods);
			}
			if (property.HasCustomAttributes)
			{
				this.AddCustomAttributes(property);
			}
			if (property.HasConstant)
			{
				this.AddConstant(property, property.PropertyType);
			}
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x0001366C File Offset: 0x0001186C
		private void AddOtherSemantic(IMetadataTokenProvider owner, Collection<MethodDefinition> others)
		{
			for (int i = 0; i < others.Count; i++)
			{
				this.AddSemantic(MethodSemanticsAttributes.Other, owner, others[i]);
			}
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0001369C File Offset: 0x0001189C
		private void AddEvents(TypeDefinition type)
		{
			Collection<EventDefinition> events = type.Events;
			this.event_map_table.AddRow(new Row<uint, uint>(type.token.RID, this.event_rid));
			for (int i = 0; i < events.Count; i++)
			{
				this.AddEvent(events[i]);
			}
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x000136F0 File Offset: 0x000118F0
		private void AddEvent(EventDefinition @event)
		{
			this.event_table.AddRow(new Row<EventAttributes, uint, uint>(@event.Attributes, this.GetStringIndex(@event.Name), MetadataBuilder.MakeCodedRID(this.GetTypeToken(@event.EventType), CodedIndex.TypeDefOrRef)));
			@event.token = new MetadataToken(TokenType.Event, this.event_rid++);
			MethodDefinition methodDefinition = @event.AddMethod;
			if (methodDefinition != null)
			{
				this.AddSemantic(MethodSemanticsAttributes.AddOn, @event, methodDefinition);
			}
			methodDefinition = @event.InvokeMethod;
			if (methodDefinition != null)
			{
				this.AddSemantic(MethodSemanticsAttributes.Fire, @event, methodDefinition);
			}
			methodDefinition = @event.RemoveMethod;
			if (methodDefinition != null)
			{
				this.AddSemantic(MethodSemanticsAttributes.RemoveOn, @event, methodDefinition);
			}
			if (@event.HasOtherMethods)
			{
				this.AddOtherSemantic(@event, @event.OtherMethods);
			}
			if (@event.HasCustomAttributes)
			{
				this.AddCustomAttributes(@event);
			}
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x000137B4 File Offset: 0x000119B4
		private void AddSemantic(MethodSemanticsAttributes semantics, IMetadataTokenProvider provider, MethodDefinition method)
		{
			method.SemanticsAttributes = semantics;
			MethodSemanticsTable table = this.GetTable<MethodSemanticsTable>(Table.MethodSemantics);
			table.AddRow(new Row<MethodSemanticsAttributes, uint, uint>(semantics, method.token.RID, MetadataBuilder.MakeCodedRID(provider, CodedIndex.HasSemantics)));
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x000137F0 File Offset: 0x000119F0
		private void AddConstant(IConstantProvider owner, TypeReference type)
		{
			object constant = owner.Constant;
			ElementType constantType = MetadataBuilder.GetConstantType(type, constant);
			this.constant_table.AddRow(new Row<ElementType, uint, uint>(constantType, MetadataBuilder.MakeCodedRID(owner.MetadataToken, CodedIndex.HasConstant), this.GetBlobIndex(this.GetConstantSignature(constantType, constant))));
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00013838 File Offset: 0x00011A38
		private static ElementType GetConstantType(TypeReference constant_type, object constant)
		{
			if (constant == null)
			{
				return ElementType.Class;
			}
			ElementType etype = constant_type.etype;
			ElementType elementType = etype;
			switch (elementType)
			{
			case ElementType.None:
			{
				TypeDefinition typeDefinition = constant_type.CheckedResolve();
				if (typeDefinition.IsEnum)
				{
					return MetadataBuilder.GetConstantType(typeDefinition.GetEnumUnderlyingType(), constant);
				}
				return ElementType.Class;
			}
			case ElementType.Void:
			case ElementType.Ptr:
			case ElementType.ValueType:
			case ElementType.Class:
			case ElementType.TypedByRef:
			case (ElementType)23:
			case (ElementType)26:
			case ElementType.FnPtr:
				return etype;
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
				return MetadataBuilder.GetConstantType(constant.GetType());
			case ElementType.String:
				return ElementType.String;
			case ElementType.ByRef:
			case ElementType.CModReqD:
			case ElementType.CModOpt:
				break;
			case ElementType.Var:
			case ElementType.Array:
			case ElementType.SzArray:
			case ElementType.MVar:
				return ElementType.Class;
			case ElementType.GenericInst:
			{
				GenericInstanceType genericInstanceType = (GenericInstanceType)constant_type;
				if (genericInstanceType.ElementType.IsTypeOf("System", "Nullable`1"))
				{
					return MetadataBuilder.GetConstantType(genericInstanceType.GenericArguments[0], constant);
				}
				return MetadataBuilder.GetConstantType(((TypeSpecification)constant_type).ElementType, constant);
			}
			case ElementType.Object:
				return MetadataBuilder.GetConstantType(constant.GetType());
			default:
				if (elementType != ElementType.Sentinel)
				{
					return etype;
				}
				break;
			}
			return MetadataBuilder.GetConstantType(((TypeSpecification)constant_type).ElementType, constant);
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x0001397C File Offset: 0x00011B7C
		private static ElementType GetConstantType(Type type)
		{
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.Boolean:
				return ElementType.Boolean;
			case TypeCode.Char:
				return ElementType.Char;
			case TypeCode.SByte:
				return ElementType.I1;
			case TypeCode.Byte:
				return ElementType.U1;
			case TypeCode.Int16:
				return ElementType.I2;
			case TypeCode.UInt16:
				return ElementType.U2;
			case TypeCode.Int32:
				return ElementType.I4;
			case TypeCode.UInt32:
				return ElementType.U4;
			case TypeCode.Int64:
				return ElementType.I8;
			case TypeCode.UInt64:
				return ElementType.U8;
			case TypeCode.Single:
				return ElementType.R4;
			case TypeCode.Double:
				return ElementType.R8;
			case TypeCode.String:
				return ElementType.String;
			}
			throw new NotSupportedException(type.FullName);
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00013A08 File Offset: 0x00011C08
		private void AddCustomAttributes(ICustomAttributeProvider owner)
		{
			Collection<CustomAttribute> customAttributes = owner.CustomAttributes;
			for (int i = 0; i < customAttributes.Count; i++)
			{
				CustomAttribute customAttribute = customAttributes[i];
				this.custom_attribute_table.AddRow(new Row<uint, uint, uint>(MetadataBuilder.MakeCodedRID(owner, CodedIndex.HasCustomAttribute), MetadataBuilder.MakeCodedRID(this.LookupToken(customAttribute.Constructor), CodedIndex.CustomAttributeType), this.GetBlobIndex(this.GetCustomAttributeSignature(customAttribute))));
			}
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x00013A70 File Offset: 0x00011C70
		private void AddSecurityDeclarations(ISecurityDeclarationProvider owner)
		{
			Collection<SecurityDeclaration> securityDeclarations = owner.SecurityDeclarations;
			for (int i = 0; i < securityDeclarations.Count; i++)
			{
				SecurityDeclaration securityDeclaration = securityDeclarations[i];
				this.declsec_table.AddRow(new Row<SecurityAction, uint, uint>(securityDeclaration.Action, MetadataBuilder.MakeCodedRID(owner, CodedIndex.HasDeclSecurity), this.GetBlobIndex(this.GetSecurityDeclarationSignature(securityDeclaration))));
			}
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00013AC8 File Offset: 0x00011CC8
		private MetadataToken GetMemberRefToken(MemberReference member)
		{
			Row<uint, uint, uint> row = this.CreateMemberRefRow(member);
			MetadataToken result;
			if (this.member_ref_map.TryGetValue(row, out result))
			{
				return result;
			}
			this.AddMemberReference(member, row);
			return member.token;
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00013AFD File Offset: 0x00011CFD
		private Row<uint, uint, uint> CreateMemberRefRow(MemberReference member)
		{
			return new Row<uint, uint, uint>(MetadataBuilder.MakeCodedRID(this.GetTypeToken(member.DeclaringType), CodedIndex.MemberRefParent), this.GetStringIndex(member.Name), this.GetBlobIndex(this.GetMemberRefSignature(member)));
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00013B2F File Offset: 0x00011D2F
		private void AddMemberReference(MemberReference member, Row<uint, uint, uint> row)
		{
			member.token = new MetadataToken(TokenType.MemberRef, this.member_ref_table.AddRow(row));
			this.member_ref_map.Add(row, member.token);
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00013B60 File Offset: 0x00011D60
		private MetadataToken GetMethodSpecToken(MethodSpecification method_spec)
		{
			Row<uint, uint> row = this.CreateMethodSpecRow(method_spec);
			MetadataToken result;
			if (this.method_spec_map.TryGetValue(row, out result))
			{
				return result;
			}
			this.AddMethodSpecification(method_spec, row);
			return method_spec.token;
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x00013B95 File Offset: 0x00011D95
		private void AddMethodSpecification(MethodSpecification method_spec, Row<uint, uint> row)
		{
			method_spec.token = new MetadataToken(TokenType.MethodSpec, this.method_spec_table.AddRow(row));
			this.method_spec_map.Add(row, method_spec.token);
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x00013BC5 File Offset: 0x00011DC5
		private Row<uint, uint> CreateMethodSpecRow(MethodSpecification method_spec)
		{
			return new Row<uint, uint>(MetadataBuilder.MakeCodedRID(this.LookupToken(method_spec.ElementMethod), CodedIndex.MethodDefOrRef), this.GetBlobIndex(this.GetMethodSpecSignature(method_spec)));
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00013BEB File Offset: 0x00011DEB
		private SignatureWriter CreateSignatureWriter()
		{
			return new SignatureWriter(this);
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00013BF4 File Offset: 0x00011DF4
		private SignatureWriter GetMethodSpecSignature(MethodSpecification method_spec)
		{
			if (!method_spec.IsGenericInstance)
			{
				throw new NotSupportedException();
			}
			GenericInstanceMethod instance = (GenericInstanceMethod)method_spec;
			SignatureWriter signatureWriter = this.CreateSignatureWriter();
			signatureWriter.WriteByte(10);
			signatureWriter.WriteGenericInstanceSignature(instance);
			return signatureWriter;
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00013C2D File Offset: 0x00011E2D
		public uint AddStandAloneSignature(uint signature)
		{
			return (uint)this.standalone_sig_table.AddRow(signature);
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x00013C3B File Offset: 0x00011E3B
		public uint GetLocalVariableBlobIndex(Collection<VariableDefinition> variables)
		{
			return this.GetBlobIndex(this.GetVariablesSignature(variables));
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x00013C4A File Offset: 0x00011E4A
		public uint GetCallSiteBlobIndex(CallSite call_site)
		{
			return this.GetBlobIndex(this.GetMethodSignature(call_site));
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x00013C5C File Offset: 0x00011E5C
		private SignatureWriter GetVariablesSignature(Collection<VariableDefinition> variables)
		{
			SignatureWriter signatureWriter = this.CreateSignatureWriter();
			signatureWriter.WriteByte(7);
			signatureWriter.WriteCompressedUInt32((uint)variables.Count);
			for (int i = 0; i < variables.Count; i++)
			{
				signatureWriter.WriteTypeSignature(variables[i].VariableType);
			}
			return signatureWriter;
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00013CA8 File Offset: 0x00011EA8
		private SignatureWriter GetFieldSignature(FieldReference field)
		{
			SignatureWriter signatureWriter = this.CreateSignatureWriter();
			signatureWriter.WriteByte(6);
			signatureWriter.WriteTypeSignature(field.FieldType);
			return signatureWriter;
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x00013CD0 File Offset: 0x00011ED0
		private SignatureWriter GetMethodSignature(IMethodSignature method)
		{
			SignatureWriter signatureWriter = this.CreateSignatureWriter();
			signatureWriter.WriteMethodSignature(method);
			return signatureWriter;
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x00013CEC File Offset: 0x00011EEC
		private SignatureWriter GetMemberRefSignature(MemberReference member)
		{
			FieldReference fieldReference = member as FieldReference;
			if (fieldReference != null)
			{
				return this.GetFieldSignature(fieldReference);
			}
			MethodReference methodReference = member as MethodReference;
			if (methodReference != null)
			{
				return this.GetMethodSignature(methodReference);
			}
			throw new NotSupportedException();
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x00013D24 File Offset: 0x00011F24
		private SignatureWriter GetPropertySignature(PropertyDefinition property)
		{
			SignatureWriter signatureWriter = this.CreateSignatureWriter();
			byte b = 8;
			if (property.HasThis)
			{
				b |= 32;
			}
			uint num = 0U;
			Collection<ParameterDefinition> collection = null;
			if (property.HasParameters)
			{
				collection = property.Parameters;
				num = (uint)collection.Count;
			}
			signatureWriter.WriteByte(b);
			signatureWriter.WriteCompressedUInt32(num);
			signatureWriter.WriteTypeSignature(property.PropertyType);
			if (num == 0U)
			{
				return signatureWriter;
			}
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				signatureWriter.WriteTypeSignature(collection[num2].ParameterType);
				num2++;
			}
			return signatureWriter;
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00013DA8 File Offset: 0x00011FA8
		private SignatureWriter GetTypeSpecSignature(TypeReference type)
		{
			SignatureWriter signatureWriter = this.CreateSignatureWriter();
			signatureWriter.WriteTypeSignature(type);
			return signatureWriter;
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x00013DC4 File Offset: 0x00011FC4
		private SignatureWriter GetConstantSignature(ElementType type, object value)
		{
			SignatureWriter signatureWriter = this.CreateSignatureWriter();
			switch (type)
			{
			case ElementType.String:
				signatureWriter.WriteConstantString((string)value);
				return signatureWriter;
			case ElementType.Ptr:
			case ElementType.ByRef:
			case ElementType.ValueType:
				goto IL_5C;
			case ElementType.Class:
			case ElementType.Var:
			case ElementType.Array:
				break;
			default:
				switch (type)
				{
				case ElementType.Object:
				case ElementType.SzArray:
				case ElementType.MVar:
					break;
				default:
					goto IL_5C;
				}
				break;
			}
			signatureWriter.WriteInt32(0);
			return signatureWriter;
			IL_5C:
			signatureWriter.WriteConstantPrimitive(value);
			return signatureWriter;
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x00013E38 File Offset: 0x00012038
		private SignatureWriter GetCustomAttributeSignature(CustomAttribute attribute)
		{
			SignatureWriter signatureWriter = this.CreateSignatureWriter();
			if (!attribute.resolved)
			{
				signatureWriter.WriteBytes(attribute.GetBlob());
				return signatureWriter;
			}
			signatureWriter.WriteUInt16(1);
			signatureWriter.WriteCustomAttributeConstructorArguments(attribute);
			signatureWriter.WriteCustomAttributeNamedArguments(attribute);
			return signatureWriter;
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x00013E78 File Offset: 0x00012078
		private SignatureWriter GetSecurityDeclarationSignature(SecurityDeclaration declaration)
		{
			SignatureWriter signatureWriter = this.CreateSignatureWriter();
			if (!declaration.resolved)
			{
				signatureWriter.WriteBytes(declaration.GetBlob());
			}
			else if (this.module.Runtime < TargetRuntime.Net_2_0)
			{
				signatureWriter.WriteXmlSecurityDeclaration(declaration);
			}
			else
			{
				signatureWriter.WriteSecurityDeclaration(declaration);
			}
			return signatureWriter;
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x00013EC4 File Offset: 0x000120C4
		private SignatureWriter GetMarshalInfoSignature(IMarshalInfoProvider owner)
		{
			SignatureWriter signatureWriter = this.CreateSignatureWriter();
			signatureWriter.WriteMarshalInfo(owner.MarshalInfo);
			return signatureWriter;
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x00013EE5 File Offset: 0x000120E5
		private static Exception CreateForeignMemberException(MemberReference member)
		{
			return new ArgumentException(string.Format("Member '{0}' is declared in another module and needs to be imported", member));
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x00013EF8 File Offset: 0x000120F8
		public MetadataToken LookupToken(IMetadataTokenProvider provider)
		{
			if (provider == null)
			{
				throw new ArgumentNullException();
			}
			MemberReference memberReference = provider as MemberReference;
			if (memberReference == null || memberReference.Module != this.module)
			{
				throw MetadataBuilder.CreateForeignMemberException(memberReference);
			}
			MetadataToken metadataToken = provider.MetadataToken;
			TokenType tokenType = metadataToken.TokenType;
			if (tokenType <= TokenType.MemberRef)
			{
				if (tokenType <= TokenType.TypeDef)
				{
					if (tokenType == TokenType.TypeRef)
					{
						goto IL_A9;
					}
					if (tokenType != TokenType.TypeDef)
					{
						goto IL_CB;
					}
				}
				else if (tokenType != TokenType.Field && tokenType != TokenType.Method)
				{
					if (tokenType != TokenType.MemberRef)
					{
						goto IL_CB;
					}
					return this.GetMemberRefToken(memberReference);
				}
			}
			else if (tokenType <= TokenType.Property)
			{
				if (tokenType != TokenType.Event && tokenType != TokenType.Property)
				{
					goto IL_CB;
				}
			}
			else
			{
				if (tokenType == TokenType.TypeSpec || tokenType == TokenType.GenericParam)
				{
					goto IL_A9;
				}
				if (tokenType != TokenType.MethodSpec)
				{
					goto IL_CB;
				}
				return this.GetMethodSpecToken((MethodSpecification)provider);
			}
			return metadataToken;
			IL_A9:
			return this.GetTypeToken((TypeReference)provider);
			IL_CB:
			throw new NotSupportedException();
		}

		// Token: 0x040003B1 RID: 945
		internal readonly ModuleDefinition module;

		// Token: 0x040003B2 RID: 946
		internal readonly ISymbolWriterProvider symbol_writer_provider;

		// Token: 0x040003B3 RID: 947
		internal readonly ISymbolWriter symbol_writer;

		// Token: 0x040003B4 RID: 948
		internal readonly TextMap text_map;

		// Token: 0x040003B5 RID: 949
		internal readonly string fq_name;

		// Token: 0x040003B6 RID: 950
		private readonly Dictionary<Row<uint, uint, uint>, MetadataToken> type_ref_map;

		// Token: 0x040003B7 RID: 951
		private readonly Dictionary<uint, MetadataToken> type_spec_map;

		// Token: 0x040003B8 RID: 952
		private readonly Dictionary<Row<uint, uint, uint>, MetadataToken> member_ref_map;

		// Token: 0x040003B9 RID: 953
		private readonly Dictionary<Row<uint, uint>, MetadataToken> method_spec_map;

		// Token: 0x040003BA RID: 954
		private readonly Collection<GenericParameter> generic_parameters;

		// Token: 0x040003BB RID: 955
		private readonly Dictionary<MetadataToken, MetadataToken> method_def_map;

		// Token: 0x040003BC RID: 956
		internal readonly CodeWriter code;

		// Token: 0x040003BD RID: 957
		internal readonly DataBuffer data;

		// Token: 0x040003BE RID: 958
		internal readonly ResourceBuffer resources;

		// Token: 0x040003BF RID: 959
		internal readonly StringHeapBuffer string_heap;

		// Token: 0x040003C0 RID: 960
		internal readonly UserStringHeapBuffer user_string_heap;

		// Token: 0x040003C1 RID: 961
		internal readonly BlobHeapBuffer blob_heap;

		// Token: 0x040003C2 RID: 962
		internal readonly TableHeapBuffer table_heap;

		// Token: 0x040003C3 RID: 963
		internal MetadataToken entry_point;

		// Token: 0x040003C4 RID: 964
		private uint type_rid = 1U;

		// Token: 0x040003C5 RID: 965
		private uint field_rid = 1U;

		// Token: 0x040003C6 RID: 966
		private uint method_rid = 1U;

		// Token: 0x040003C7 RID: 967
		private uint param_rid = 1U;

		// Token: 0x040003C8 RID: 968
		private uint property_rid = 1U;

		// Token: 0x040003C9 RID: 969
		private uint event_rid = 1U;

		// Token: 0x040003CA RID: 970
		private readonly TypeRefTable type_ref_table;

		// Token: 0x040003CB RID: 971
		private readonly TypeDefTable type_def_table;

		// Token: 0x040003CC RID: 972
		private readonly FieldTable field_table;

		// Token: 0x040003CD RID: 973
		private readonly MethodTable method_table;

		// Token: 0x040003CE RID: 974
		private readonly ParamTable param_table;

		// Token: 0x040003CF RID: 975
		private readonly InterfaceImplTable iface_impl_table;

		// Token: 0x040003D0 RID: 976
		private readonly MemberRefTable member_ref_table;

		// Token: 0x040003D1 RID: 977
		private readonly ConstantTable constant_table;

		// Token: 0x040003D2 RID: 978
		private readonly CustomAttributeTable custom_attribute_table;

		// Token: 0x040003D3 RID: 979
		private readonly DeclSecurityTable declsec_table;

		// Token: 0x040003D4 RID: 980
		private readonly StandAloneSigTable standalone_sig_table;

		// Token: 0x040003D5 RID: 981
		private readonly EventMapTable event_map_table;

		// Token: 0x040003D6 RID: 982
		private readonly EventTable event_table;

		// Token: 0x040003D7 RID: 983
		private readonly PropertyMapTable property_map_table;

		// Token: 0x040003D8 RID: 984
		private readonly PropertyTable property_table;

		// Token: 0x040003D9 RID: 985
		private readonly TypeSpecTable typespec_table;

		// Token: 0x040003DA RID: 986
		private readonly MethodSpecTable method_spec_table;

		// Token: 0x040003DB RID: 987
		internal readonly bool write_symbols;

		// Token: 0x0200008D RID: 141
		private sealed class GenericParameterComparer : IComparer<GenericParameter>
		{
			// Token: 0x060004BC RID: 1212 RVA: 0x00013FD8 File Offset: 0x000121D8
			public int Compare(GenericParameter a, GenericParameter b)
			{
				uint num = MetadataBuilder.MakeCodedRID(a.Owner, CodedIndex.TypeOrMethodDef);
				uint num2 = MetadataBuilder.MakeCodedRID(b.Owner, CodedIndex.TypeOrMethodDef);
				if (num == num2)
				{
					int position = a.Position;
					int position2 = b.Position;
					if (position == position2)
					{
						return 0;
					}
					if (position <= position2)
					{
						return -1;
					}
					return 1;
				}
				else
				{
					if (num <= num2)
					{
						return -1;
					}
					return 1;
				}
			}
		}
	}
}
