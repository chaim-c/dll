using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Mono.Cecil.Cil;
using Mono.Cecil.Metadata;
using Mono.Cecil.PE;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x02000063 RID: 99
	internal sealed class MetadataReader : ByteBuffer
	{
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000340 RID: 832 RVA: 0x0000D078 File Offset: 0x0000B278
		// (set) Token: 0x06000341 RID: 833 RVA: 0x0000D080 File Offset: 0x0000B280
		private uint Position
		{
			get
			{
				return (uint)this.position;
			}
			set
			{
				this.position = (int)value;
			}
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000D08C File Offset: 0x0000B28C
		public MetadataReader(ModuleDefinition module) : base(module.Image.MetadataSection.Data)
		{
			this.image = module.Image;
			this.module = module;
			this.metadata = module.MetadataSystem;
			this.code = new CodeReader(this.image.MetadataSection, this);
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000D0E5 File Offset: 0x0000B2E5
		private int GetCodedIndexSize(CodedIndex index)
		{
			return this.image.GetCodedIndexSize(index);
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000D0F3 File Offset: 0x0000B2F3
		private uint ReadByIndexSize(int size)
		{
			if (size == 4)
			{
				return base.ReadUInt32();
			}
			return (uint)base.ReadUInt16();
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000D108 File Offset: 0x0000B308
		private byte[] ReadBlob()
		{
			BlobHeap blobHeap = this.image.BlobHeap;
			if (blobHeap == null)
			{
				this.position += 2;
				return Empty<byte>.Array;
			}
			return blobHeap.Read(this.ReadBlobIndex());
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000D144 File Offset: 0x0000B344
		private byte[] ReadBlob(uint signature)
		{
			BlobHeap blobHeap = this.image.BlobHeap;
			if (blobHeap == null)
			{
				return Empty<byte>.Array;
			}
			return blobHeap.Read(signature);
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000D170 File Offset: 0x0000B370
		private uint ReadBlobIndex()
		{
			BlobHeap blobHeap = this.image.BlobHeap;
			return this.ReadByIndexSize((blobHeap != null) ? blobHeap.IndexSize : 2);
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000D19B File Offset: 0x0000B39B
		private string ReadString()
		{
			return this.image.StringHeap.Read(this.ReadByIndexSize(this.image.StringHeap.IndexSize));
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000D1C3 File Offset: 0x0000B3C3
		private uint ReadStringIndex()
		{
			return this.ReadByIndexSize(this.image.StringHeap.IndexSize);
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000D1DB File Offset: 0x0000B3DB
		private uint ReadTableIndex(Table table)
		{
			return this.ReadByIndexSize(this.image.GetTableIndexSize(table));
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000D1EF File Offset: 0x0000B3EF
		private MetadataToken ReadMetadataToken(CodedIndex index)
		{
			return index.GetMetadataToken(this.ReadByIndexSize(this.GetCodedIndexSize(index)));
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000D204 File Offset: 0x0000B404
		private int MoveTo(Table table)
		{
			TableInformation tableInformation = this.image.TableHeap[table];
			if (tableInformation.Length != 0U)
			{
				this.Position = tableInformation.Offset;
			}
			return (int)tableInformation.Length;
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000D240 File Offset: 0x0000B440
		private bool MoveTo(Table table, uint row)
		{
			TableInformation tableInformation = this.image.TableHeap[table];
			uint length = tableInformation.Length;
			if (length == 0U || row > length)
			{
				return false;
			}
			this.Position = tableInformation.Offset + tableInformation.RowSize * (row - 1U);
			return true;
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000D28C File Offset: 0x0000B48C
		public AssemblyNameDefinition ReadAssemblyNameDefinition()
		{
			if (this.MoveTo(Table.Assembly) == 0)
			{
				return null;
			}
			AssemblyNameDefinition assemblyNameDefinition = new AssemblyNameDefinition();
			assemblyNameDefinition.HashAlgorithm = (AssemblyHashAlgorithm)base.ReadUInt32();
			this.PopulateVersionAndFlags(assemblyNameDefinition);
			assemblyNameDefinition.PublicKey = this.ReadBlob();
			this.PopulateNameAndCulture(assemblyNameDefinition);
			return assemblyNameDefinition;
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000D2D4 File Offset: 0x0000B4D4
		public ModuleDefinition Populate(ModuleDefinition module)
		{
			if (this.MoveTo(Table.Module) == 0)
			{
				return module;
			}
			base.Advance(2);
			module.Name = this.ReadString();
			module.Mvid = this.image.GuidHeap.Read(this.ReadByIndexSize(this.image.GuidHeap.IndexSize));
			return module;
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000D32C File Offset: 0x0000B52C
		private void InitializeAssemblyReferences()
		{
			if (this.metadata.AssemblyReferences != null)
			{
				return;
			}
			int num = this.MoveTo(Table.AssemblyRef);
			AssemblyNameReference[] array = this.metadata.AssemblyReferences = new AssemblyNameReference[num];
			uint num2 = 0U;
			while ((ulong)num2 < (ulong)((long)num))
			{
				AssemblyNameReference assemblyNameReference = new AssemblyNameReference();
				assemblyNameReference.token = new MetadataToken(TokenType.AssemblyRef, num2 + 1U);
				this.PopulateVersionAndFlags(assemblyNameReference);
				byte[] array2 = this.ReadBlob();
				if (assemblyNameReference.HasPublicKey)
				{
					assemblyNameReference.PublicKey = array2;
				}
				else
				{
					assemblyNameReference.PublicKeyToken = array2;
				}
				this.PopulateNameAndCulture(assemblyNameReference);
				assemblyNameReference.Hash = this.ReadBlob();
				array[(int)((UIntPtr)num2)] = assemblyNameReference;
				num2 += 1U;
			}
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000D3CF File Offset: 0x0000B5CF
		public Collection<AssemblyNameReference> ReadAssemblyReferences()
		{
			this.InitializeAssemblyReferences();
			return new Collection<AssemblyNameReference>(this.metadata.AssemblyReferences);
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000D3E8 File Offset: 0x0000B5E8
		public MethodDefinition ReadEntryPoint()
		{
			if (this.module.Image.EntryPointToken == 0U)
			{
				return null;
			}
			MetadataToken metadataToken = new MetadataToken(this.module.Image.EntryPointToken);
			return this.GetMethodDefinition(metadataToken.RID);
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000D430 File Offset: 0x0000B630
		public Collection<ModuleDefinition> ReadModules()
		{
			Collection<ModuleDefinition> collection = new Collection<ModuleDefinition>(1);
			collection.Add(this.module);
			int num = this.MoveTo(Table.File);
			uint num2 = 1U;
			while ((ulong)num2 <= (ulong)((long)num))
			{
				FileAttributes fileAttributes = (FileAttributes)base.ReadUInt32();
				string name = this.ReadString();
				this.ReadBlobIndex();
				if (fileAttributes == FileAttributes.ContainsMetaData)
				{
					ReaderParameters parameters = new ReaderParameters
					{
						ReadingMode = this.module.ReadingMode,
						SymbolReaderProvider = this.module.SymbolReaderProvider,
						AssemblyResolver = this.module.AssemblyResolver
					};
					collection.Add(ModuleDefinition.ReadModule(this.GetModuleFileName(name), parameters));
				}
				num2 += 1U;
			}
			return collection;
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000D4D8 File Offset: 0x0000B6D8
		private string GetModuleFileName(string name)
		{
			if (this.module.FullyQualifiedName == null)
			{
				throw new NotSupportedException();
			}
			string directoryName = Path.GetDirectoryName(this.module.FullyQualifiedName);
			return Path.Combine(directoryName, name);
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000D510 File Offset: 0x0000B710
		private void InitializeModuleReferences()
		{
			if (this.metadata.ModuleReferences != null)
			{
				return;
			}
			int num = this.MoveTo(Table.ModuleRef);
			ModuleReference[] array = this.metadata.ModuleReferences = new ModuleReference[num];
			uint num2 = 0U;
			while ((ulong)num2 < (ulong)((long)num))
			{
				ModuleReference moduleReference = new ModuleReference(this.ReadString());
				moduleReference.token = new MetadataToken(TokenType.ModuleRef, num2 + 1U);
				array[(int)((UIntPtr)num2)] = moduleReference;
				num2 += 1U;
			}
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000D57D File Offset: 0x0000B77D
		public Collection<ModuleReference> ReadModuleReferences()
		{
			this.InitializeModuleReferences();
			return new Collection<ModuleReference>(this.metadata.ModuleReferences);
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000D598 File Offset: 0x0000B798
		public bool HasFileResource()
		{
			int num = this.MoveTo(Table.File);
			if (num == 0)
			{
				return false;
			}
			uint num2 = 1U;
			while ((ulong)num2 <= (ulong)((long)num))
			{
				if (this.ReadFileRecord(num2).Col1 == FileAttributes.ContainsNoMetaData)
				{
					return true;
				}
				num2 += 1U;
			}
			return false;
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000D5D4 File Offset: 0x0000B7D4
		public Collection<Resource> ReadResources()
		{
			int num = this.MoveTo(Table.ManifestResource);
			Collection<Resource> collection = new Collection<Resource>(num);
			for (int i = 1; i <= num; i++)
			{
				uint offset = base.ReadUInt32();
				ManifestResourceAttributes manifestResourceAttributes = (ManifestResourceAttributes)base.ReadUInt32();
				string name = this.ReadString();
				MetadataToken scope = this.ReadMetadataToken(CodedIndex.Implementation);
				Resource item;
				if (scope.RID == 0U)
				{
					item = new EmbeddedResource(name, manifestResourceAttributes, offset, this);
				}
				else if (scope.TokenType == TokenType.AssemblyRef)
				{
					item = new AssemblyLinkedResource(name, manifestResourceAttributes)
					{
						Assembly = (AssemblyNameReference)this.GetTypeReferenceScope(scope)
					};
				}
				else
				{
					if (scope.TokenType != TokenType.File)
					{
						throw new NotSupportedException();
					}
					Row<FileAttributes, string, uint> row = this.ReadFileRecord(scope.RID);
					item = new LinkedResource(name, manifestResourceAttributes)
					{
						File = row.Col2,
						hash = this.ReadBlob(row.Col3)
					};
				}
				collection.Add(item);
			}
			return collection;
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000D6D4 File Offset: 0x0000B8D4
		private Row<FileAttributes, string, uint> ReadFileRecord(uint rid)
		{
			int position = this.position;
			if (!this.MoveTo(Table.File, rid))
			{
				throw new ArgumentException();
			}
			Row<FileAttributes, string, uint> result = new Row<FileAttributes, string, uint>((FileAttributes)base.ReadUInt32(), this.ReadString(), this.ReadBlobIndex());
			this.position = position;
			return result;
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000D71C File Offset: 0x0000B91C
		public MemoryStream GetManagedResourceStream(uint offset)
		{
			uint virtualAddress = this.image.Resources.VirtualAddress;
			Section sectionAtVirtualAddress = this.image.GetSectionAtVirtualAddress(virtualAddress);
			uint num = virtualAddress - sectionAtVirtualAddress.VirtualAddress + offset;
			byte[] data = sectionAtVirtualAddress.Data;
			int count = (int)data[(int)((UIntPtr)num)] | (int)data[(int)((UIntPtr)(num + 1U))] << 8 | (int)data[(int)((UIntPtr)(num + 2U))] << 16 | (int)data[(int)((UIntPtr)(num + 3U))] << 24;
			return new MemoryStream(data, (int)(num + 4U), count);
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000D787 File Offset: 0x0000B987
		private void PopulateVersionAndFlags(AssemblyNameReference name)
		{
			name.Version = new Version((int)base.ReadUInt16(), (int)base.ReadUInt16(), (int)base.ReadUInt16(), (int)base.ReadUInt16());
			name.Attributes = (AssemblyAttributes)base.ReadUInt32();
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000D7B8 File Offset: 0x0000B9B8
		private void PopulateNameAndCulture(AssemblyNameReference name)
		{
			name.Name = this.ReadString();
			name.Culture = this.ReadString();
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000D7D4 File Offset: 0x0000B9D4
		public TypeDefinitionCollection ReadTypes()
		{
			this.InitializeTypeDefinitions();
			TypeDefinition[] types = this.metadata.Types;
			int capacity = types.Length - this.metadata.NestedTypes.Count;
			TypeDefinitionCollection typeDefinitionCollection = new TypeDefinitionCollection(this.module, capacity);
			foreach (TypeDefinition typeDefinition in types)
			{
				if (!MetadataReader.IsNested(typeDefinition.Attributes))
				{
					typeDefinitionCollection.Add(typeDefinition);
				}
			}
			if (this.image.HasTable(Table.MethodPtr) || this.image.HasTable(Table.FieldPtr))
			{
				this.CompleteTypes();
			}
			return typeDefinitionCollection;
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000D864 File Offset: 0x0000BA64
		private void CompleteTypes()
		{
			foreach (TypeDefinition typeDefinition in this.metadata.Types)
			{
				MetadataReader.InitializeCollection(typeDefinition.Fields);
				MetadataReader.InitializeCollection(typeDefinition.Methods);
			}
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000D8A8 File Offset: 0x0000BAA8
		private void InitializeTypeDefinitions()
		{
			if (this.metadata.Types != null)
			{
				return;
			}
			this.InitializeNestedTypes();
			this.InitializeFields();
			this.InitializeMethods();
			int num = this.MoveTo(Table.TypeDef);
			TypeDefinition[] array = this.metadata.Types = new TypeDefinition[num];
			uint num2 = 0U;
			while ((ulong)num2 < (ulong)((long)num))
			{
				if (array[(int)((UIntPtr)num2)] == null)
				{
					array[(int)((UIntPtr)num2)] = this.ReadType(num2 + 1U);
				}
				num2 += 1U;
			}
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0000D914 File Offset: 0x0000BB14
		private static bool IsNested(TypeAttributes attributes)
		{
			switch (attributes & TypeAttributes.VisibilityMask)
			{
			case TypeAttributes.NestedPublic:
			case TypeAttributes.NestedPrivate:
			case TypeAttributes.NestedFamily:
			case TypeAttributes.NestedAssembly:
			case TypeAttributes.NestedFamANDAssem:
			case TypeAttributes.VisibilityMask:
				return true;
			default:
				return false;
			}
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000D94C File Offset: 0x0000BB4C
		public bool HasNestedTypes(TypeDefinition type)
		{
			this.InitializeNestedTypes();
			uint[] array;
			return this.metadata.TryGetNestedTypeMapping(type, out array) && array.Length > 0;
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000D978 File Offset: 0x0000BB78
		public Collection<TypeDefinition> ReadNestedTypes(TypeDefinition type)
		{
			this.InitializeNestedTypes();
			uint[] array;
			if (!this.metadata.TryGetNestedTypeMapping(type, out array))
			{
				return new MemberDefinitionCollection<TypeDefinition>(type);
			}
			MemberDefinitionCollection<TypeDefinition> memberDefinitionCollection = new MemberDefinitionCollection<TypeDefinition>(type, array.Length);
			for (int i = 0; i < array.Length; i++)
			{
				TypeDefinition typeDefinition = this.GetTypeDefinition(array[i]);
				if (typeDefinition != null)
				{
					memberDefinitionCollection.Add(typeDefinition);
				}
			}
			this.metadata.RemoveNestedTypeMapping(type);
			return memberDefinitionCollection;
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000D9DC File Offset: 0x0000BBDC
		private void InitializeNestedTypes()
		{
			if (this.metadata.NestedTypes != null)
			{
				return;
			}
			int num = this.MoveTo(Table.NestedClass);
			this.metadata.NestedTypes = new Dictionary<uint, uint[]>(num);
			this.metadata.ReverseNestedTypes = new Dictionary<uint, uint>(num);
			if (num == 0)
			{
				return;
			}
			for (int i = 1; i <= num; i++)
			{
				uint nested = this.ReadTableIndex(Table.TypeDef);
				uint declaring = this.ReadTableIndex(Table.TypeDef);
				this.AddNestedMapping(declaring, nested);
			}
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000DA4A File Offset: 0x0000BC4A
		private void AddNestedMapping(uint declaring, uint nested)
		{
			this.metadata.SetNestedTypeMapping(declaring, MetadataReader.AddMapping<uint, uint>(this.metadata.NestedTypes, declaring, nested));
			this.metadata.SetReverseNestedTypeMapping(nested, declaring);
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000DA78 File Offset: 0x0000BC78
		private static TValue[] AddMapping<TKey, TValue>(Dictionary<TKey, TValue[]> cache, TKey key, TValue value)
		{
			TValue[] array;
			if (!cache.TryGetValue(key, out array))
			{
				array = new TValue[]
				{
					value
				};
				return array;
			}
			TValue[] array2 = new TValue[array.Length + 1];
			Array.Copy(array, array2, array.Length);
			array2[array.Length] = value;
			return array2;
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000DAC4 File Offset: 0x0000BCC4
		private TypeDefinition ReadType(uint rid)
		{
			if (!this.MoveTo(Table.TypeDef, rid))
			{
				return null;
			}
			TypeAttributes attributes = (TypeAttributes)base.ReadUInt32();
			string name = this.ReadString();
			string @namespace = this.ReadString();
			TypeDefinition typeDefinition = new TypeDefinition(@namespace, name, attributes);
			typeDefinition.token = new MetadataToken(TokenType.TypeDef, rid);
			typeDefinition.scope = this.module;
			typeDefinition.module = this.module;
			this.metadata.AddTypeDefinition(typeDefinition);
			this.context = typeDefinition;
			typeDefinition.BaseType = this.GetTypeDefOrRef(this.ReadMetadataToken(CodedIndex.TypeDefOrRef));
			typeDefinition.fields_range = this.ReadFieldsRange(rid);
			typeDefinition.methods_range = this.ReadMethodsRange(rid);
			if (MetadataReader.IsNested(attributes))
			{
				typeDefinition.DeclaringType = this.GetNestedTypeDeclaringType(typeDefinition);
			}
			return typeDefinition;
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000DB7C File Offset: 0x0000BD7C
		private TypeDefinition GetNestedTypeDeclaringType(TypeDefinition type)
		{
			uint rid;
			if (!this.metadata.TryGetReverseNestedTypeMapping(type, out rid))
			{
				return null;
			}
			this.metadata.RemoveReverseNestedTypeMapping(type);
			return this.GetTypeDefinition(rid);
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000DBAE File Offset: 0x0000BDAE
		private Range ReadFieldsRange(uint type_index)
		{
			return this.ReadListRange(type_index, Table.TypeDef, Table.Field);
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0000DBB9 File Offset: 0x0000BDB9
		private Range ReadMethodsRange(uint type_index)
		{
			return this.ReadListRange(type_index, Table.TypeDef, Table.Method);
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000DBC4 File Offset: 0x0000BDC4
		private Range ReadListRange(uint current_index, Table current, Table target)
		{
			Range result = default(Range);
			result.Start = this.ReadTableIndex(target);
			TableInformation tableInformation = this.image.TableHeap[current];
			uint num;
			if (current_index == tableInformation.Length)
			{
				num = this.image.TableHeap[target].Length + 1U;
			}
			else
			{
				uint position = this.Position;
				this.Position += (uint)((ulong)tableInformation.RowSize - (ulong)((long)this.image.GetTableIndexSize(target)));
				num = this.ReadTableIndex(target);
				this.Position = position;
			}
			result.Length = num - result.Start;
			return result;
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000DC6C File Offset: 0x0000BE6C
		public Row<short, int> ReadTypeLayout(TypeDefinition type)
		{
			this.InitializeTypeLayouts();
			uint rid = type.token.RID;
			Row<ushort, uint> row;
			if (!this.metadata.ClassLayouts.TryGetValue(rid, out row))
			{
				return new Row<short, int>(-1, -1);
			}
			type.PackingSize = (short)row.Col1;
			type.ClassSize = (int)row.Col2;
			this.metadata.ClassLayouts.Remove(rid);
			return new Row<short, int>((short)row.Col1, (int)row.Col2);
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000DCEC File Offset: 0x0000BEEC
		private void InitializeTypeLayouts()
		{
			if (this.metadata.ClassLayouts != null)
			{
				return;
			}
			int num = this.MoveTo(Table.ClassLayout);
			Dictionary<uint, Row<ushort, uint>> dictionary = this.metadata.ClassLayouts = new Dictionary<uint, Row<ushort, uint>>(num);
			uint num2 = 0U;
			while ((ulong)num2 < (ulong)((long)num))
			{
				ushort col = base.ReadUInt16();
				uint col2 = base.ReadUInt32();
				uint key = this.ReadTableIndex(Table.TypeDef);
				dictionary.Add(key, new Row<ushort, uint>(col, col2));
				num2 += 1U;
			}
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000DD5D File Offset: 0x0000BF5D
		public TypeReference GetTypeDefOrRef(MetadataToken token)
		{
			return (TypeReference)this.LookupToken(token);
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000DD6C File Offset: 0x0000BF6C
		public TypeDefinition GetTypeDefinition(uint rid)
		{
			this.InitializeTypeDefinitions();
			TypeDefinition typeDefinition = this.metadata.GetTypeDefinition(rid);
			if (typeDefinition != null)
			{
				return typeDefinition;
			}
			return this.ReadTypeDefinition(rid);
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0000DD98 File Offset: 0x0000BF98
		private TypeDefinition ReadTypeDefinition(uint rid)
		{
			if (!this.MoveTo(Table.TypeDef, rid))
			{
				return null;
			}
			return this.ReadType(rid);
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0000DDAD File Offset: 0x0000BFAD
		private void InitializeTypeReferences()
		{
			if (this.metadata.TypeReferences != null)
			{
				return;
			}
			this.metadata.TypeReferences = new TypeReference[this.image.GetTableLength(Table.TypeRef)];
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000DDDC File Offset: 0x0000BFDC
		public TypeReference GetTypeReference(string scope, string full_name)
		{
			this.InitializeTypeReferences();
			int num = this.metadata.TypeReferences.Length;
			uint num2 = 1U;
			while ((ulong)num2 <= (ulong)((long)num))
			{
				TypeReference typeReference = this.GetTypeReference(num2);
				if (!(typeReference.FullName != full_name))
				{
					if (string.IsNullOrEmpty(scope))
					{
						return typeReference;
					}
					if (typeReference.Scope.Name == scope)
					{
						return typeReference;
					}
				}
				num2 += 1U;
			}
			return null;
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0000DE44 File Offset: 0x0000C044
		private TypeReference GetTypeReference(uint rid)
		{
			this.InitializeTypeReferences();
			TypeReference typeReference = this.metadata.GetTypeReference(rid);
			if (typeReference != null)
			{
				return typeReference;
			}
			return this.ReadTypeReference(rid);
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000DE70 File Offset: 0x0000C070
		private TypeReference ReadTypeReference(uint rid)
		{
			if (!this.MoveTo(Table.TypeRef, rid))
			{
				return null;
			}
			TypeReference typeReference = null;
			MetadataToken metadataToken = this.ReadMetadataToken(CodedIndex.ResolutionScope);
			string name = this.ReadString();
			string @namespace = this.ReadString();
			TypeReference typeReference2 = new TypeReference(@namespace, name, this.module, null);
			typeReference2.token = new MetadataToken(TokenType.TypeRef, rid);
			this.metadata.AddTypeReference(typeReference2);
			IMetadataScope scope;
			if (metadataToken.TokenType == TokenType.TypeRef)
			{
				typeReference = this.GetTypeDefOrRef(metadataToken);
				scope = ((typeReference != null) ? typeReference.Scope : this.module);
			}
			else
			{
				scope = this.GetTypeReferenceScope(metadataToken);
			}
			typeReference2.scope = scope;
			typeReference2.DeclaringType = typeReference;
			MetadataSystem.TryProcessPrimitiveTypeReference(typeReference2);
			return typeReference2;
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0000DF20 File Offset: 0x0000C120
		private IMetadataScope GetTypeReferenceScope(MetadataToken scope)
		{
			if (scope.TokenType == TokenType.Module)
			{
				return this.module;
			}
			TokenType tokenType = scope.TokenType;
			IMetadataScope[] array;
			if (tokenType != TokenType.ModuleRef)
			{
				if (tokenType != TokenType.AssemblyRef)
				{
					throw new NotSupportedException();
				}
				this.InitializeAssemblyReferences();
				array = this.metadata.AssemblyReferences;
			}
			else
			{
				this.InitializeModuleReferences();
				array = this.metadata.ModuleReferences;
			}
			uint num = scope.RID - 1U;
			if (num < 0U || (ulong)num >= (ulong)((long)array.Length))
			{
				return null;
			}
			return array[(int)((UIntPtr)num)];
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0000DFA0 File Offset: 0x0000C1A0
		public IEnumerable<TypeReference> GetTypeReferences()
		{
			this.InitializeTypeReferences();
			int tableLength = this.image.GetTableLength(Table.TypeRef);
			TypeReference[] array = new TypeReference[tableLength];
			uint num = 1U;
			while ((ulong)num <= (ulong)((long)tableLength))
			{
				array[(int)((UIntPtr)(num - 1U))] = this.GetTypeReference(num);
				num += 1U;
			}
			return array;
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000DFE4 File Offset: 0x0000C1E4
		private TypeReference GetTypeSpecification(uint rid)
		{
			if (!this.MoveTo(Table.TypeSpec, rid))
			{
				return null;
			}
			SignatureReader signatureReader = this.ReadSignature(this.ReadBlobIndex());
			TypeReference typeReference = signatureReader.ReadTypeSignature();
			if (typeReference.token.RID == 0U)
			{
				typeReference.token = new MetadataToken(TokenType.TypeSpec, rid);
			}
			return typeReference;
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000E031 File Offset: 0x0000C231
		private SignatureReader ReadSignature(uint signature)
		{
			return new SignatureReader(signature, this);
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000E03C File Offset: 0x0000C23C
		public bool HasInterfaces(TypeDefinition type)
		{
			this.InitializeInterfaces();
			MetadataToken[] array;
			return this.metadata.TryGetInterfaceMapping(type, out array);
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000E060 File Offset: 0x0000C260
		public Collection<TypeReference> ReadInterfaces(TypeDefinition type)
		{
			this.InitializeInterfaces();
			MetadataToken[] array;
			if (!this.metadata.TryGetInterfaceMapping(type, out array))
			{
				return new Collection<TypeReference>();
			}
			Collection<TypeReference> collection = new Collection<TypeReference>(array.Length);
			this.context = type;
			for (int i = 0; i < array.Length; i++)
			{
				collection.Add(this.GetTypeDefOrRef(array[i]));
			}
			this.metadata.RemoveInterfaceMapping(type);
			return collection;
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0000E0CC File Offset: 0x0000C2CC
		private void InitializeInterfaces()
		{
			if (this.metadata.Interfaces != null)
			{
				return;
			}
			int num = this.MoveTo(Table.InterfaceImpl);
			this.metadata.Interfaces = new Dictionary<uint, MetadataToken[]>(num);
			for (int i = 0; i < num; i++)
			{
				uint type = this.ReadTableIndex(Table.TypeDef);
				MetadataToken @interface = this.ReadMetadataToken(CodedIndex.TypeDefOrRef);
				this.AddInterfaceMapping(type, @interface);
			}
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0000E125 File Offset: 0x0000C325
		private void AddInterfaceMapping(uint type, MetadataToken @interface)
		{
			this.metadata.SetInterfaceMapping(type, MetadataReader.AddMapping<uint, MetadataToken>(this.metadata.Interfaces, type, @interface));
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0000E148 File Offset: 0x0000C348
		public Collection<FieldDefinition> ReadFields(TypeDefinition type)
		{
			Range fields_range = type.fields_range;
			if (fields_range.Length == 0U)
			{
				return new MemberDefinitionCollection<FieldDefinition>(type);
			}
			MemberDefinitionCollection<FieldDefinition> memberDefinitionCollection = new MemberDefinitionCollection<FieldDefinition>(type, (int)fields_range.Length);
			this.context = type;
			if (!this.MoveTo(Table.FieldPtr, fields_range.Start))
			{
				if (!this.MoveTo(Table.Field, fields_range.Start))
				{
					return memberDefinitionCollection;
				}
				for (uint num = 0U; num < fields_range.Length; num += 1U)
				{
					this.ReadField(fields_range.Start + num, memberDefinitionCollection);
				}
			}
			else
			{
				this.ReadPointers<FieldDefinition>(Table.FieldPtr, Table.Field, fields_range, memberDefinitionCollection, new Action<uint, Collection<FieldDefinition>>(this.ReadField));
			}
			return memberDefinitionCollection;
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000E1E0 File Offset: 0x0000C3E0
		private void ReadField(uint field_rid, Collection<FieldDefinition> fields)
		{
			FieldAttributes attributes = (FieldAttributes)base.ReadUInt16();
			string name = this.ReadString();
			uint signature = this.ReadBlobIndex();
			FieldDefinition fieldDefinition = new FieldDefinition(name, attributes, this.ReadFieldType(signature));
			fieldDefinition.token = new MetadataToken(TokenType.Field, field_rid);
			this.metadata.AddFieldDefinition(fieldDefinition);
			if (MetadataReader.IsDeleted(fieldDefinition))
			{
				return;
			}
			fields.Add(fieldDefinition);
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0000E23E File Offset: 0x0000C43E
		private void InitializeFields()
		{
			if (this.metadata.Fields != null)
			{
				return;
			}
			this.metadata.Fields = new FieldDefinition[this.image.GetTableLength(Table.Field)];
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0000E26C File Offset: 0x0000C46C
		private TypeReference ReadFieldType(uint signature)
		{
			SignatureReader signatureReader = this.ReadSignature(signature);
			if (signatureReader.ReadByte() != 6)
			{
				throw new NotSupportedException();
			}
			return signatureReader.ReadTypeSignature();
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000E298 File Offset: 0x0000C498
		public int ReadFieldRVA(FieldDefinition field)
		{
			this.InitializeFieldRVAs();
			uint rid = field.token.RID;
			uint num;
			if (!this.metadata.FieldRVAs.TryGetValue(rid, out num))
			{
				return 0;
			}
			int fieldTypeSize = MetadataReader.GetFieldTypeSize(field.FieldType);
			if (fieldTypeSize == 0 || num == 0U)
			{
				return 0;
			}
			this.metadata.FieldRVAs.Remove(rid);
			field.InitialValue = this.GetFieldInitializeValue(fieldTypeSize, num);
			return (int)num;
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000E304 File Offset: 0x0000C504
		private byte[] GetFieldInitializeValue(int size, uint rva)
		{
			Section sectionAtVirtualAddress = this.image.GetSectionAtVirtualAddress(rva);
			if (sectionAtVirtualAddress == null)
			{
				return Empty<byte>.Array;
			}
			byte[] array = new byte[size];
			Buffer.BlockCopy(sectionAtVirtualAddress.Data, (int)(rva - sectionAtVirtualAddress.VirtualAddress), array, 0, size);
			return array;
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000E348 File Offset: 0x0000C548
		private static int GetFieldTypeSize(TypeReference type)
		{
			int result = 0;
			switch (type.etype)
			{
			case ElementType.Boolean:
			case ElementType.I1:
			case ElementType.U1:
				return 1;
			case ElementType.Char:
			case ElementType.I2:
			case ElementType.U2:
				return 2;
			case ElementType.I4:
			case ElementType.U4:
			case ElementType.R4:
				return 4;
			case ElementType.I8:
			case ElementType.U8:
			case ElementType.R8:
				return 8;
			case ElementType.Ptr:
			case ElementType.FnPtr:
				return IntPtr.Size;
			case ElementType.CModReqD:
			case ElementType.CModOpt:
				return MetadataReader.GetFieldTypeSize(((IModifierType)type).ElementType);
			}
			TypeDefinition typeDefinition = type.Resolve();
			if (typeDefinition != null && typeDefinition.HasLayoutInfo)
			{
				result = typeDefinition.ClassSize;
			}
			return result;
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000E428 File Offset: 0x0000C628
		private void InitializeFieldRVAs()
		{
			if (this.metadata.FieldRVAs != null)
			{
				return;
			}
			int num = this.MoveTo(Table.FieldRVA);
			Dictionary<uint, uint> dictionary = this.metadata.FieldRVAs = new Dictionary<uint, uint>(num);
			for (int i = 0; i < num; i++)
			{
				uint value = base.ReadUInt32();
				uint key = this.ReadTableIndex(Table.Field);
				dictionary.Add(key, value);
			}
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000E488 File Offset: 0x0000C688
		public int ReadFieldLayout(FieldDefinition field)
		{
			this.InitializeFieldLayouts();
			uint rid = field.token.RID;
			uint result;
			if (!this.metadata.FieldLayouts.TryGetValue(rid, out result))
			{
				return -1;
			}
			this.metadata.FieldLayouts.Remove(rid);
			return (int)result;
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000E4D4 File Offset: 0x0000C6D4
		private void InitializeFieldLayouts()
		{
			if (this.metadata.FieldLayouts != null)
			{
				return;
			}
			int num = this.MoveTo(Table.FieldLayout);
			Dictionary<uint, uint> dictionary = this.metadata.FieldLayouts = new Dictionary<uint, uint>(num);
			for (int i = 0; i < num; i++)
			{
				uint value = base.ReadUInt32();
				uint key = this.ReadTableIndex(Table.Field);
				dictionary.Add(key, value);
			}
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000E534 File Offset: 0x0000C734
		public bool HasEvents(TypeDefinition type)
		{
			this.InitializeEvents();
			Range range;
			return this.metadata.TryGetEventsRange(type, out range) && range.Length > 0U;
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0000E564 File Offset: 0x0000C764
		public Collection<EventDefinition> ReadEvents(TypeDefinition type)
		{
			this.InitializeEvents();
			Range range;
			if (!this.metadata.TryGetEventsRange(type, out range))
			{
				return new MemberDefinitionCollection<EventDefinition>(type);
			}
			MemberDefinitionCollection<EventDefinition> memberDefinitionCollection = new MemberDefinitionCollection<EventDefinition>(type, (int)range.Length);
			this.metadata.RemoveEventsRange(type);
			if (range.Length == 0U)
			{
				return memberDefinitionCollection;
			}
			this.context = type;
			if (!this.MoveTo(Table.EventPtr, range.Start))
			{
				if (!this.MoveTo(Table.Event, range.Start))
				{
					return memberDefinitionCollection;
				}
				for (uint num = 0U; num < range.Length; num += 1U)
				{
					this.ReadEvent(range.Start + num, memberDefinitionCollection);
				}
			}
			else
			{
				this.ReadPointers<EventDefinition>(Table.EventPtr, Table.Event, range, memberDefinitionCollection, new Action<uint, Collection<EventDefinition>>(this.ReadEvent));
			}
			return memberDefinitionCollection;
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000E61C File Offset: 0x0000C81C
		private void ReadEvent(uint event_rid, Collection<EventDefinition> events)
		{
			EventAttributes attributes = (EventAttributes)base.ReadUInt16();
			string name = this.ReadString();
			TypeReference typeDefOrRef = this.GetTypeDefOrRef(this.ReadMetadataToken(CodedIndex.TypeDefOrRef));
			EventDefinition eventDefinition = new EventDefinition(name, attributes, typeDefOrRef);
			eventDefinition.token = new MetadataToken(TokenType.Event, event_rid);
			if (MetadataReader.IsDeleted(eventDefinition))
			{
				return;
			}
			events.Add(eventDefinition);
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000E670 File Offset: 0x0000C870
		private void InitializeEvents()
		{
			if (this.metadata.Events != null)
			{
				return;
			}
			int num = this.MoveTo(Table.EventMap);
			this.metadata.Events = new Dictionary<uint, Range>(num);
			uint num2 = 1U;
			while ((ulong)num2 <= (ulong)((long)num))
			{
				uint type_rid = this.ReadTableIndex(Table.TypeDef);
				Range range = this.ReadEventsRange(num2);
				this.metadata.AddEventsRange(type_rid, range);
				num2 += 1U;
			}
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000E6D0 File Offset: 0x0000C8D0
		private Range ReadEventsRange(uint rid)
		{
			return this.ReadListRange(rid, Table.EventMap, Table.Event);
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000E6E0 File Offset: 0x0000C8E0
		public bool HasProperties(TypeDefinition type)
		{
			this.InitializeProperties();
			Range range;
			return this.metadata.TryGetPropertiesRange(type, out range) && range.Length > 0U;
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000E710 File Offset: 0x0000C910
		public Collection<PropertyDefinition> ReadProperties(TypeDefinition type)
		{
			this.InitializeProperties();
			Range range;
			if (!this.metadata.TryGetPropertiesRange(type, out range))
			{
				return new MemberDefinitionCollection<PropertyDefinition>(type);
			}
			this.metadata.RemovePropertiesRange(type);
			MemberDefinitionCollection<PropertyDefinition> memberDefinitionCollection = new MemberDefinitionCollection<PropertyDefinition>(type, (int)range.Length);
			if (range.Length == 0U)
			{
				return memberDefinitionCollection;
			}
			this.context = type;
			if (!this.MoveTo(Table.PropertyPtr, range.Start))
			{
				if (!this.MoveTo(Table.Property, range.Start))
				{
					return memberDefinitionCollection;
				}
				for (uint num = 0U; num < range.Length; num += 1U)
				{
					this.ReadProperty(range.Start + num, memberDefinitionCollection);
				}
			}
			else
			{
				this.ReadPointers<PropertyDefinition>(Table.PropertyPtr, Table.Property, range, memberDefinitionCollection, new Action<uint, Collection<PropertyDefinition>>(this.ReadProperty));
			}
			return memberDefinitionCollection;
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000E7C8 File Offset: 0x0000C9C8
		private void ReadProperty(uint property_rid, Collection<PropertyDefinition> properties)
		{
			PropertyAttributes attributes = (PropertyAttributes)base.ReadUInt16();
			string name = this.ReadString();
			uint signature = this.ReadBlobIndex();
			SignatureReader signatureReader = this.ReadSignature(signature);
			byte b = signatureReader.ReadByte();
			if ((b & 8) == 0)
			{
				throw new NotSupportedException();
			}
			bool hasThis = (b & 32) != 0;
			signatureReader.ReadCompressedUInt32();
			PropertyDefinition propertyDefinition = new PropertyDefinition(name, attributes, signatureReader.ReadTypeSignature());
			propertyDefinition.HasThis = hasThis;
			propertyDefinition.token = new MetadataToken(TokenType.Property, property_rid);
			if (MetadataReader.IsDeleted(propertyDefinition))
			{
				return;
			}
			properties.Add(propertyDefinition);
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000E858 File Offset: 0x0000CA58
		private void InitializeProperties()
		{
			if (this.metadata.Properties != null)
			{
				return;
			}
			int num = this.MoveTo(Table.PropertyMap);
			this.metadata.Properties = new Dictionary<uint, Range>(num);
			uint num2 = 1U;
			while ((ulong)num2 <= (ulong)((long)num))
			{
				uint type_rid = this.ReadTableIndex(Table.TypeDef);
				Range range = this.ReadPropertiesRange(num2);
				this.metadata.AddPropertiesRange(type_rid, range);
				num2 += 1U;
			}
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000E8B8 File Offset: 0x0000CAB8
		private Range ReadPropertiesRange(uint rid)
		{
			return this.ReadListRange(rid, Table.PropertyMap, Table.Property);
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000E8C8 File Offset: 0x0000CAC8
		private MethodSemanticsAttributes ReadMethodSemantics(MethodDefinition method)
		{
			this.InitializeMethodSemantics();
			Row<MethodSemanticsAttributes, MetadataToken> row;
			if (!this.metadata.Semantics.TryGetValue(method.token.RID, out row))
			{
				return MethodSemanticsAttributes.None;
			}
			TypeDefinition declaringType = method.DeclaringType;
			MethodSemanticsAttributes col = row.Col1;
			if (col <= MethodSemanticsAttributes.AddOn)
			{
				switch (col)
				{
				case MethodSemanticsAttributes.Setter:
					MetadataReader.GetProperty(declaringType, row.Col2).set_method = method;
					goto IL_174;
				case MethodSemanticsAttributes.Getter:
					MetadataReader.GetProperty(declaringType, row.Col2).get_method = method;
					goto IL_174;
				case MethodSemanticsAttributes.Setter | MethodSemanticsAttributes.Getter:
					break;
				case MethodSemanticsAttributes.Other:
				{
					TokenType tokenType = row.Col2.TokenType;
					if (tokenType == TokenType.Event)
					{
						EventDefinition @event = MetadataReader.GetEvent(declaringType, row.Col2);
						if (@event.other_methods == null)
						{
							@event.other_methods = new Collection<MethodDefinition>();
						}
						@event.other_methods.Add(method);
						goto IL_174;
					}
					if (tokenType != TokenType.Property)
					{
						throw new NotSupportedException();
					}
					PropertyDefinition property = MetadataReader.GetProperty(declaringType, row.Col2);
					if (property.other_methods == null)
					{
						property.other_methods = new Collection<MethodDefinition>();
					}
					property.other_methods.Add(method);
					goto IL_174;
				}
				default:
					if (col == MethodSemanticsAttributes.AddOn)
					{
						MetadataReader.GetEvent(declaringType, row.Col2).add_method = method;
						goto IL_174;
					}
					break;
				}
			}
			else
			{
				if (col == MethodSemanticsAttributes.RemoveOn)
				{
					MetadataReader.GetEvent(declaringType, row.Col2).remove_method = method;
					goto IL_174;
				}
				if (col == MethodSemanticsAttributes.Fire)
				{
					MetadataReader.GetEvent(declaringType, row.Col2).invoke_method = method;
					goto IL_174;
				}
			}
			throw new NotSupportedException();
			IL_174:
			this.metadata.Semantics.Remove(method.token.RID);
			return row.Col1;
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000EA6C File Offset: 0x0000CC6C
		private static EventDefinition GetEvent(TypeDefinition type, MetadataToken token)
		{
			if (token.TokenType != TokenType.Event)
			{
				throw new ArgumentException();
			}
			return MetadataReader.GetMember<EventDefinition>(type.Events, token);
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000EA8E File Offset: 0x0000CC8E
		private static PropertyDefinition GetProperty(TypeDefinition type, MetadataToken token)
		{
			if (token.TokenType != TokenType.Property)
			{
				throw new ArgumentException();
			}
			return MetadataReader.GetMember<PropertyDefinition>(type.Properties, token);
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000EAB0 File Offset: 0x0000CCB0
		private static TMember GetMember<TMember>(Collection<TMember> members, MetadataToken token) where TMember : IMemberDefinition
		{
			for (int i = 0; i < members.Count; i++)
			{
				TMember result = members[i];
				if (result.MetadataToken == token)
				{
					return result;
				}
			}
			throw new ArgumentException();
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000EAF4 File Offset: 0x0000CCF4
		private void InitializeMethodSemantics()
		{
			if (this.metadata.Semantics != null)
			{
				return;
			}
			int num = this.MoveTo(Table.MethodSemantics);
			Dictionary<uint, Row<MethodSemanticsAttributes, MetadataToken>> dictionary = this.metadata.Semantics = new Dictionary<uint, Row<MethodSemanticsAttributes, MetadataToken>>(0);
			uint num2 = 0U;
			while ((ulong)num2 < (ulong)((long)num))
			{
				MethodSemanticsAttributes col = (MethodSemanticsAttributes)base.ReadUInt16();
				uint key = this.ReadTableIndex(Table.Method);
				MetadataToken col2 = this.ReadMetadataToken(CodedIndex.HasSemantics);
				dictionary[key] = new Row<MethodSemanticsAttributes, MetadataToken>(col, col2);
				num2 += 1U;
			}
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000EB66 File Offset: 0x0000CD66
		public PropertyDefinition ReadMethods(PropertyDefinition property)
		{
			this.ReadAllSemantics(property.DeclaringType);
			return property;
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000EB75 File Offset: 0x0000CD75
		public EventDefinition ReadMethods(EventDefinition @event)
		{
			this.ReadAllSemantics(@event.DeclaringType);
			return @event;
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000EB84 File Offset: 0x0000CD84
		public MethodSemanticsAttributes ReadAllSemantics(MethodDefinition method)
		{
			this.ReadAllSemantics(method.DeclaringType);
			return method.SemanticsAttributes;
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000EB98 File Offset: 0x0000CD98
		private void ReadAllSemantics(TypeDefinition type)
		{
			Collection<MethodDefinition> methods = type.Methods;
			for (int i = 0; i < methods.Count; i++)
			{
				MethodDefinition methodDefinition = methods[i];
				if (!methodDefinition.sem_attrs_ready)
				{
					methodDefinition.sem_attrs = this.ReadMethodSemantics(methodDefinition);
					methodDefinition.sem_attrs_ready = true;
				}
			}
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000EBE5 File Offset: 0x0000CDE5
		private Range ReadParametersRange(uint method_rid)
		{
			return this.ReadListRange(method_rid, Table.Method, Table.Param);
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000EBF0 File Offset: 0x0000CDF0
		public Collection<MethodDefinition> ReadMethods(TypeDefinition type)
		{
			Range methods_range = type.methods_range;
			if (methods_range.Length == 0U)
			{
				return new MemberDefinitionCollection<MethodDefinition>(type);
			}
			MemberDefinitionCollection<MethodDefinition> memberDefinitionCollection = new MemberDefinitionCollection<MethodDefinition>(type, (int)methods_range.Length);
			if (!this.MoveTo(Table.MethodPtr, methods_range.Start))
			{
				if (!this.MoveTo(Table.Method, methods_range.Start))
				{
					return memberDefinitionCollection;
				}
				for (uint num = 0U; num < methods_range.Length; num += 1U)
				{
					this.ReadMethod(methods_range.Start + num, memberDefinitionCollection);
				}
			}
			else
			{
				this.ReadPointers<MethodDefinition>(Table.MethodPtr, Table.Method, methods_range, memberDefinitionCollection, new Action<uint, Collection<MethodDefinition>>(this.ReadMethod));
			}
			return memberDefinitionCollection;
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000EC80 File Offset: 0x0000CE80
		private void ReadPointers<TMember>(Table ptr, Table table, Range range, Collection<TMember> members, Action<uint, Collection<TMember>> reader) where TMember : IMemberDefinition
		{
			for (uint num = 0U; num < range.Length; num += 1U)
			{
				this.MoveTo(ptr, range.Start + num);
				uint num2 = this.ReadTableIndex(table);
				this.MoveTo(table, num2);
				reader(num2, members);
			}
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000ECCB File Offset: 0x0000CECB
		private static bool IsDeleted(IMemberDefinition member)
		{
			return member.IsSpecialName && member.Name == "_Deleted";
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000ECE7 File Offset: 0x0000CEE7
		private void InitializeMethods()
		{
			if (this.metadata.Methods != null)
			{
				return;
			}
			this.metadata.Methods = new MethodDefinition[this.image.GetTableLength(Table.Method)];
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000ED14 File Offset: 0x0000CF14
		private void ReadMethod(uint method_rid, Collection<MethodDefinition> methods)
		{
			MethodDefinition methodDefinition = new MethodDefinition();
			methodDefinition.rva = base.ReadUInt32();
			methodDefinition.ImplAttributes = (MethodImplAttributes)base.ReadUInt16();
			methodDefinition.Attributes = (MethodAttributes)base.ReadUInt16();
			methodDefinition.Name = this.ReadString();
			methodDefinition.token = new MetadataToken(TokenType.Method, method_rid);
			if (MetadataReader.IsDeleted(methodDefinition))
			{
				return;
			}
			methods.Add(methodDefinition);
			uint signature = this.ReadBlobIndex();
			Range param_range = this.ReadParametersRange(method_rid);
			this.context = methodDefinition;
			this.ReadMethodSignature(signature, methodDefinition);
			this.metadata.AddMethodDefinition(methodDefinition);
			if (param_range.Length == 0U)
			{
				return;
			}
			int position = this.position;
			this.ReadParameters(methodDefinition, param_range);
			this.position = position;
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000EDC4 File Offset: 0x0000CFC4
		private void ReadParameters(MethodDefinition method, Range param_range)
		{
			if (this.MoveTo(Table.ParamPtr, param_range.Start))
			{
				this.ReadParameterPointers(method, param_range);
				return;
			}
			if (!this.MoveTo(Table.Param, param_range.Start))
			{
				return;
			}
			for (uint num = 0U; num < param_range.Length; num += 1U)
			{
				this.ReadParameter(param_range.Start + num, method);
			}
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000EE20 File Offset: 0x0000D020
		private void ReadParameterPointers(MethodDefinition method, Range range)
		{
			for (uint num = 0U; num < range.Length; num += 1U)
			{
				this.MoveTo(Table.ParamPtr, range.Start + num);
				uint num2 = this.ReadTableIndex(Table.Param);
				this.MoveTo(Table.Param, num2);
				this.ReadParameter(num2, method);
			}
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000EE6C File Offset: 0x0000D06C
		private void ReadParameter(uint param_rid, MethodDefinition method)
		{
			ParameterAttributes attributes = (ParameterAttributes)base.ReadUInt16();
			ushort num = base.ReadUInt16();
			string name = this.ReadString();
			ParameterDefinition parameterDefinition = (num == 0) ? method.MethodReturnType.Parameter : method.Parameters[(int)(num - 1)];
			parameterDefinition.token = new MetadataToken(TokenType.Param, param_rid);
			parameterDefinition.Name = name;
			parameterDefinition.Attributes = attributes;
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000EECC File Offset: 0x0000D0CC
		private void ReadMethodSignature(uint signature, IMethodSignature method)
		{
			SignatureReader signatureReader = this.ReadSignature(signature);
			signatureReader.ReadMethodSignature(method);
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000EEE8 File Offset: 0x0000D0E8
		public PInvokeInfo ReadPInvokeInfo(MethodDefinition method)
		{
			this.InitializePInvokes();
			uint rid = method.token.RID;
			Row<PInvokeAttributes, uint, uint> row;
			if (!this.metadata.PInvokes.TryGetValue(rid, out row))
			{
				return null;
			}
			this.metadata.PInvokes.Remove(rid);
			return new PInvokeInfo(row.Col1, this.image.StringHeap.Read(row.Col2), this.module.ModuleReferences[(int)(row.Col3 - 1U)]);
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000EF6C File Offset: 0x0000D16C
		private void InitializePInvokes()
		{
			if (this.metadata.PInvokes != null)
			{
				return;
			}
			int num = this.MoveTo(Table.ImplMap);
			Dictionary<uint, Row<PInvokeAttributes, uint, uint>> dictionary = this.metadata.PInvokes = new Dictionary<uint, Row<PInvokeAttributes, uint, uint>>(num);
			for (int i = 1; i <= num; i++)
			{
				PInvokeAttributes col = (PInvokeAttributes)base.ReadUInt16();
				MetadataToken metadataToken = this.ReadMetadataToken(CodedIndex.MemberForwarded);
				uint col2 = this.ReadStringIndex();
				uint col3 = this.ReadTableIndex(Table.File);
				if (metadataToken.TokenType == TokenType.Method)
				{
					dictionary.Add(metadataToken.RID, new Row<PInvokeAttributes, uint, uint>(col, col2, col3));
				}
			}
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000EFFC File Offset: 0x0000D1FC
		public bool HasGenericParameters(IGenericParameterProvider provider)
		{
			this.InitializeGenericParameters();
			Range[] ranges;
			return this.metadata.TryGetGenericParameterRanges(provider, out ranges) && MetadataReader.RangesSize(ranges) > 0;
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000F02C File Offset: 0x0000D22C
		public Collection<GenericParameter> ReadGenericParameters(IGenericParameterProvider provider)
		{
			this.InitializeGenericParameters();
			Range[] array;
			if (!this.metadata.TryGetGenericParameterRanges(provider, out array))
			{
				return new GenericParameterCollection(provider);
			}
			this.metadata.RemoveGenericParameterRange(provider);
			GenericParameterCollection genericParameterCollection = new GenericParameterCollection(provider, MetadataReader.RangesSize(array));
			for (int i = 0; i < array.Length; i++)
			{
				this.ReadGenericParametersRange(array[i], provider, genericParameterCollection);
			}
			return genericParameterCollection;
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0000F094 File Offset: 0x0000D294
		private void ReadGenericParametersRange(Range range, IGenericParameterProvider provider, GenericParameterCollection generic_parameters)
		{
			if (!this.MoveTo(Table.GenericParam, range.Start))
			{
				return;
			}
			for (uint num = 0U; num < range.Length; num += 1U)
			{
				base.ReadUInt16();
				GenericParameterAttributes attributes = (GenericParameterAttributes)base.ReadUInt16();
				this.ReadMetadataToken(CodedIndex.TypeOrMethodDef);
				string name = this.ReadString();
				generic_parameters.Add(new GenericParameter(name, provider)
				{
					token = new MetadataToken(TokenType.GenericParam, range.Start + num),
					Attributes = attributes
				});
			}
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0000F139 File Offset: 0x0000D339
		private void InitializeGenericParameters()
		{
			if (this.metadata.GenericParameters != null)
			{
				return;
			}
			this.metadata.GenericParameters = this.InitializeRanges(Table.GenericParam, delegate
			{
				base.Advance(4);
				MetadataToken result = this.ReadMetadataToken(CodedIndex.TypeOrMethodDef);
				this.ReadStringIndex();
				return result;
			});
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0000F168 File Offset: 0x0000D368
		private Dictionary<MetadataToken, Range[]> InitializeRanges(Table table, Func<MetadataToken> get_next)
		{
			int num = this.MoveTo(table);
			Dictionary<MetadataToken, Range[]> dictionary = new Dictionary<MetadataToken, Range[]>(num);
			if (num == 0)
			{
				return dictionary;
			}
			MetadataToken metadataToken = MetadataToken.Zero;
			Range range = new Range(1U, 0U);
			uint num2 = 1U;
			while ((ulong)num2 <= (ulong)((long)num))
			{
				MetadataToken metadataToken2 = get_next();
				if (num2 == 1U)
				{
					metadataToken = metadataToken2;
					range.Length += 1U;
				}
				else if (metadataToken2 != metadataToken)
				{
					MetadataReader.AddRange(dictionary, metadataToken, range);
					range = new Range(num2, 1U);
					metadataToken = metadataToken2;
				}
				else
				{
					range.Length += 1U;
				}
				num2 += 1U;
			}
			MetadataReader.AddRange(dictionary, metadataToken, range);
			return dictionary;
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0000F204 File Offset: 0x0000D404
		private static void AddRange(Dictionary<MetadataToken, Range[]> ranges, MetadataToken owner, Range range)
		{
			if (owner.RID == 0U)
			{
				return;
			}
			Range[] array;
			if (!ranges.TryGetValue(owner, out array))
			{
				ranges.Add(owner, new Range[]
				{
					range
				});
				return;
			}
			array = array.Resize(array.Length + 1);
			array[array.Length - 1] = range;
			ranges[owner] = array;
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0000F268 File Offset: 0x0000D468
		public bool HasGenericConstraints(GenericParameter generic_parameter)
		{
			this.InitializeGenericConstraints();
			MetadataToken[] array;
			return this.metadata.TryGetGenericConstraintMapping(generic_parameter, out array) && array.Length > 0;
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000F294 File Offset: 0x0000D494
		public Collection<TypeReference> ReadGenericConstraints(GenericParameter generic_parameter)
		{
			this.InitializeGenericConstraints();
			MetadataToken[] array;
			if (!this.metadata.TryGetGenericConstraintMapping(generic_parameter, out array))
			{
				return new Collection<TypeReference>();
			}
			Collection<TypeReference> collection = new Collection<TypeReference>(array.Length);
			this.context = (IGenericContext)generic_parameter.Owner;
			for (int i = 0; i < array.Length; i++)
			{
				collection.Add(this.GetTypeDefOrRef(array[i]));
			}
			this.metadata.RemoveGenericConstraintMapping(generic_parameter);
			return collection;
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000F30C File Offset: 0x0000D50C
		private void InitializeGenericConstraints()
		{
			if (this.metadata.GenericConstraints != null)
			{
				return;
			}
			int num = this.MoveTo(Table.GenericParamConstraint);
			this.metadata.GenericConstraints = new Dictionary<uint, MetadataToken[]>(num);
			for (int i = 1; i <= num; i++)
			{
				this.AddGenericConstraintMapping(this.ReadTableIndex(Table.GenericParam), this.ReadMetadataToken(CodedIndex.TypeDefOrRef));
			}
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0000F362 File Offset: 0x0000D562
		private void AddGenericConstraintMapping(uint generic_parameter, MetadataToken constraint)
		{
			this.metadata.SetGenericConstraintMapping(generic_parameter, MetadataReader.AddMapping<uint, MetadataToken>(this.metadata.GenericConstraints, generic_parameter, constraint));
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0000F384 File Offset: 0x0000D584
		public bool HasOverrides(MethodDefinition method)
		{
			this.InitializeOverrides();
			MetadataToken[] array;
			return this.metadata.TryGetOverrideMapping(method, out array) && array.Length > 0;
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0000F3B0 File Offset: 0x0000D5B0
		public Collection<MethodReference> ReadOverrides(MethodDefinition method)
		{
			this.InitializeOverrides();
			MetadataToken[] array;
			if (!this.metadata.TryGetOverrideMapping(method, out array))
			{
				return new Collection<MethodReference>();
			}
			Collection<MethodReference> collection = new Collection<MethodReference>(array.Length);
			this.context = method;
			for (int i = 0; i < array.Length; i++)
			{
				collection.Add((MethodReference)this.LookupToken(array[i]));
			}
			this.metadata.RemoveOverrideMapping(method);
			return collection;
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0000F424 File Offset: 0x0000D624
		private void InitializeOverrides()
		{
			if (this.metadata.Overrides != null)
			{
				return;
			}
			int num = this.MoveTo(Table.MethodImpl);
			this.metadata.Overrides = new Dictionary<uint, MetadataToken[]>(num);
			for (int i = 1; i <= num; i++)
			{
				this.ReadTableIndex(Table.TypeDef);
				MetadataToken metadataToken = this.ReadMetadataToken(CodedIndex.MethodDefOrRef);
				if (metadataToken.TokenType != TokenType.Method)
				{
					throw new NotSupportedException();
				}
				MetadataToken @override = this.ReadMetadataToken(CodedIndex.MethodDefOrRef);
				this.AddOverrideMapping(metadataToken.RID, @override);
			}
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000F49F File Offset: 0x0000D69F
		private void AddOverrideMapping(uint method_rid, MetadataToken @override)
		{
			this.metadata.SetOverrideMapping(method_rid, MetadataReader.AddMapping<uint, MetadataToken>(this.metadata.Overrides, method_rid, @override));
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0000F4BF File Offset: 0x0000D6BF
		public MethodBody ReadMethodBody(MethodDefinition method)
		{
			return this.code.ReadMethodBody(method);
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0000F4D0 File Offset: 0x0000D6D0
		public CallSite ReadCallSite(MetadataToken token)
		{
			if (!this.MoveTo(Table.StandAloneSig, token.RID))
			{
				return null;
			}
			uint signature = this.ReadBlobIndex();
			CallSite callSite = new CallSite();
			this.ReadMethodSignature(signature, callSite);
			callSite.MetadataToken = token;
			return callSite;
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0000F510 File Offset: 0x0000D710
		public VariableDefinitionCollection ReadVariables(MetadataToken local_var_token)
		{
			if (!this.MoveTo(Table.StandAloneSig, local_var_token.RID))
			{
				return null;
			}
			SignatureReader signatureReader = this.ReadSignature(this.ReadBlobIndex());
			if (signatureReader.ReadByte() != 7)
			{
				throw new NotSupportedException();
			}
			uint num = signatureReader.ReadCompressedUInt32();
			if (num == 0U)
			{
				return null;
			}
			VariableDefinitionCollection variableDefinitionCollection = new VariableDefinitionCollection((int)num);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				variableDefinitionCollection.Add(new VariableDefinition(signatureReader.ReadTypeSignature()));
				num2++;
			}
			return variableDefinitionCollection;
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0000F580 File Offset: 0x0000D780
		public IMetadataTokenProvider LookupToken(MetadataToken token)
		{
			uint rid = token.RID;
			if (rid == 0U)
			{
				return null;
			}
			int position = this.position;
			IGenericContext genericContext = this.context;
			TokenType tokenType = token.TokenType;
			IMetadataTokenProvider result;
			if (tokenType <= TokenType.Field)
			{
				if (tokenType == TokenType.TypeRef)
				{
					result = this.GetTypeReference(rid);
					goto IL_C3;
				}
				if (tokenType == TokenType.TypeDef)
				{
					result = this.GetTypeDefinition(rid);
					goto IL_C3;
				}
				if (tokenType == TokenType.Field)
				{
					result = this.GetFieldDefinition(rid);
					goto IL_C3;
				}
			}
			else if (tokenType <= TokenType.MemberRef)
			{
				if (tokenType == TokenType.Method)
				{
					result = this.GetMethodDefinition(rid);
					goto IL_C3;
				}
				if (tokenType == TokenType.MemberRef)
				{
					result = this.GetMemberReference(rid);
					goto IL_C3;
				}
			}
			else
			{
				if (tokenType == TokenType.TypeSpec)
				{
					result = this.GetTypeSpecification(rid);
					goto IL_C3;
				}
				if (tokenType == TokenType.MethodSpec)
				{
					result = this.GetMethodSpecification(rid);
					goto IL_C3;
				}
			}
			return null;
			IL_C3:
			this.position = position;
			this.context = genericContext;
			return result;
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000F660 File Offset: 0x0000D860
		public FieldDefinition GetFieldDefinition(uint rid)
		{
			this.InitializeTypeDefinitions();
			FieldDefinition fieldDefinition = this.metadata.GetFieldDefinition(rid);
			if (fieldDefinition != null)
			{
				return fieldDefinition;
			}
			return this.LookupField(rid);
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0000F68C File Offset: 0x0000D88C
		private FieldDefinition LookupField(uint rid)
		{
			TypeDefinition fieldDeclaringType = this.metadata.GetFieldDeclaringType(rid);
			if (fieldDeclaringType == null)
			{
				return null;
			}
			MetadataReader.InitializeCollection(fieldDeclaringType.Fields);
			return this.metadata.GetFieldDefinition(rid);
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0000F6C4 File Offset: 0x0000D8C4
		public MethodDefinition GetMethodDefinition(uint rid)
		{
			this.InitializeTypeDefinitions();
			MethodDefinition methodDefinition = this.metadata.GetMethodDefinition(rid);
			if (methodDefinition != null)
			{
				return methodDefinition;
			}
			return this.LookupMethod(rid);
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0000F6F0 File Offset: 0x0000D8F0
		private MethodDefinition LookupMethod(uint rid)
		{
			TypeDefinition methodDeclaringType = this.metadata.GetMethodDeclaringType(rid);
			if (methodDeclaringType == null)
			{
				return null;
			}
			MetadataReader.InitializeCollection(methodDeclaringType.Methods);
			return this.metadata.GetMethodDefinition(rid);
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0000F728 File Offset: 0x0000D928
		private MethodSpecification GetMethodSpecification(uint rid)
		{
			if (!this.MoveTo(Table.MethodSpec, rid))
			{
				return null;
			}
			MethodReference method = (MethodReference)this.LookupToken(this.ReadMetadataToken(CodedIndex.MethodDefOrRef));
			uint signature = this.ReadBlobIndex();
			MethodSpecification methodSpecification = this.ReadMethodSpecSignature(signature, method);
			methodSpecification.token = new MetadataToken(TokenType.MethodSpec, rid);
			return methodSpecification;
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0000F778 File Offset: 0x0000D978
		private MethodSpecification ReadMethodSpecSignature(uint signature, MethodReference method)
		{
			SignatureReader signatureReader = this.ReadSignature(signature);
			byte b = signatureReader.ReadByte();
			if (b != 10)
			{
				throw new NotSupportedException();
			}
			GenericInstanceMethod genericInstanceMethod = new GenericInstanceMethod(method);
			signatureReader.ReadGenericInstanceSignature(method, genericInstanceMethod);
			return genericInstanceMethod;
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0000F7B0 File Offset: 0x0000D9B0
		private MemberReference GetMemberReference(uint rid)
		{
			this.InitializeMemberReferences();
			MemberReference memberReference = this.metadata.GetMemberReference(rid);
			if (memberReference != null)
			{
				return memberReference;
			}
			memberReference = this.ReadMemberReference(rid);
			if (memberReference != null && !memberReference.ContainsGenericParameter)
			{
				this.metadata.AddMemberReference(memberReference);
			}
			return memberReference;
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0000F7F8 File Offset: 0x0000D9F8
		private MemberReference ReadMemberReference(uint rid)
		{
			if (!this.MoveTo(Table.MemberRef, rid))
			{
				return null;
			}
			MetadataToken metadataToken = this.ReadMetadataToken(CodedIndex.MemberRefParent);
			string name = this.ReadString();
			uint signature = this.ReadBlobIndex();
			TokenType tokenType = metadataToken.TokenType;
			MemberReference memberReference;
			if (tokenType <= TokenType.TypeDef)
			{
				if (tokenType != TokenType.TypeRef && tokenType != TokenType.TypeDef)
				{
					goto IL_73;
				}
			}
			else
			{
				if (tokenType == TokenType.Method)
				{
					memberReference = this.ReadMethodMemberReference(metadataToken, name, signature);
					goto IL_79;
				}
				if (tokenType != TokenType.TypeSpec)
				{
					goto IL_73;
				}
			}
			memberReference = this.ReadTypeMemberReference(metadataToken, name, signature);
			goto IL_79;
			IL_73:
			throw new NotSupportedException();
			IL_79:
			memberReference.token = new MetadataToken(TokenType.MemberRef, rid);
			return memberReference;
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0000F890 File Offset: 0x0000DA90
		private MemberReference ReadTypeMemberReference(MetadataToken type, string name, uint signature)
		{
			TypeReference typeDefOrRef = this.GetTypeDefOrRef(type);
			if (!typeDefOrRef.IsArray)
			{
				this.context = typeDefOrRef;
			}
			MemberReference memberReference = this.ReadMemberReferenceSignature(signature, typeDefOrRef);
			memberReference.Name = name;
			return memberReference;
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0000F8C8 File Offset: 0x0000DAC8
		private MemberReference ReadMemberReferenceSignature(uint signature, TypeReference declaring_type)
		{
			SignatureReader signatureReader = this.ReadSignature(signature);
			if (signatureReader.buffer[signatureReader.position] == 6)
			{
				signatureReader.position++;
				return new FieldReference
				{
					DeclaringType = declaring_type,
					FieldType = signatureReader.ReadTypeSignature()
				};
			}
			MethodReference methodReference = new MethodReference();
			methodReference.DeclaringType = declaring_type;
			signatureReader.ReadMethodSignature(methodReference);
			return methodReference;
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0000F92C File Offset: 0x0000DB2C
		private MemberReference ReadMethodMemberReference(MetadataToken token, string name, uint signature)
		{
			MethodDefinition methodDefinition = this.GetMethodDefinition(token.RID);
			this.context = methodDefinition;
			MemberReference memberReference = this.ReadMemberReferenceSignature(signature, methodDefinition.DeclaringType);
			memberReference.Name = name;
			return memberReference;
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000F964 File Offset: 0x0000DB64
		private void InitializeMemberReferences()
		{
			if (this.metadata.MemberReferences != null)
			{
				return;
			}
			this.metadata.MemberReferences = new MemberReference[this.image.GetTableLength(Table.MemberRef)];
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0000F994 File Offset: 0x0000DB94
		public IEnumerable<MemberReference> GetMemberReferences()
		{
			this.InitializeMemberReferences();
			int tableLength = this.image.GetTableLength(Table.MemberRef);
			TypeSystem typeSystem = this.module.TypeSystem;
			MethodReference methodReference = new MethodReference(string.Empty, typeSystem.Void);
			methodReference.DeclaringType = new TypeReference(string.Empty, string.Empty, this.module, typeSystem.Corlib);
			MemberReference[] array = new MemberReference[tableLength];
			uint num = 1U;
			while ((ulong)num <= (ulong)((long)tableLength))
			{
				this.context = methodReference;
				array[(int)((UIntPtr)(num - 1U))] = this.GetMemberReference(num);
				num += 1U;
			}
			return array;
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0000FA24 File Offset: 0x0000DC24
		private void InitializeConstants()
		{
			if (this.metadata.Constants != null)
			{
				return;
			}
			int num = this.MoveTo(Table.Constant);
			Dictionary<MetadataToken, Row<ElementType, uint>> dictionary = this.metadata.Constants = new Dictionary<MetadataToken, Row<ElementType, uint>>(num);
			uint num2 = 1U;
			while ((ulong)num2 <= (ulong)((long)num))
			{
				ElementType col = (ElementType)base.ReadUInt16();
				MetadataToken key = this.ReadMetadataToken(CodedIndex.HasConstant);
				uint col2 = this.ReadBlobIndex();
				dictionary.Add(key, new Row<ElementType, uint>(col, col2));
				num2 += 1U;
			}
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000FA98 File Offset: 0x0000DC98
		public object ReadConstant(IConstantProvider owner)
		{
			this.InitializeConstants();
			Row<ElementType, uint> row;
			if (!this.metadata.Constants.TryGetValue(owner.MetadataToken, out row))
			{
				return Mixin.NoValue;
			}
			this.metadata.Constants.Remove(owner.MetadataToken);
			ElementType col = row.Col1;
			if (col == ElementType.String)
			{
				return MetadataReader.ReadConstantString(this.ReadBlob(row.Col2));
			}
			if (col == ElementType.Class || col == ElementType.Object)
			{
				return null;
			}
			return this.ReadConstantPrimitive(row.Col1, row.Col2);
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000FB24 File Offset: 0x0000DD24
		private static string ReadConstantString(byte[] blob)
		{
			int num = blob.Length;
			if ((num & 1) == 1)
			{
				num--;
			}
			return Encoding.Unicode.GetString(blob, 0, num);
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000FB4C File Offset: 0x0000DD4C
		private object ReadConstantPrimitive(ElementType type, uint signature)
		{
			SignatureReader signatureReader = this.ReadSignature(signature);
			return signatureReader.ReadConstantSignature(type);
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000FB8E File Offset: 0x0000DD8E
		private void InitializeCustomAttributes()
		{
			if (this.metadata.CustomAttributes != null)
			{
				return;
			}
			this.metadata.CustomAttributes = this.InitializeRanges(Table.CustomAttribute, delegate
			{
				MetadataToken result = this.ReadMetadataToken(CodedIndex.HasCustomAttribute);
				this.ReadMetadataToken(CodedIndex.CustomAttributeType);
				this.ReadBlobIndex();
				return result;
			});
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000FBC0 File Offset: 0x0000DDC0
		public bool HasCustomAttributes(ICustomAttributeProvider owner)
		{
			this.InitializeCustomAttributes();
			Range[] ranges;
			return this.metadata.TryGetCustomAttributeRanges(owner, out ranges) && MetadataReader.RangesSize(ranges) > 0;
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0000FBF0 File Offset: 0x0000DDF0
		public Collection<CustomAttribute> ReadCustomAttributes(ICustomAttributeProvider owner)
		{
			this.InitializeCustomAttributes();
			Range[] array;
			if (!this.metadata.TryGetCustomAttributeRanges(owner, out array))
			{
				return new Collection<CustomAttribute>();
			}
			Collection<CustomAttribute> collection = new Collection<CustomAttribute>(MetadataReader.RangesSize(array));
			for (int i = 0; i < array.Length; i++)
			{
				this.ReadCustomAttributeRange(array[i], collection);
			}
			this.metadata.RemoveCustomAttributeRange(owner);
			return collection;
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0000FC54 File Offset: 0x0000DE54
		private void ReadCustomAttributeRange(Range range, Collection<CustomAttribute> custom_attributes)
		{
			if (!this.MoveTo(Table.CustomAttribute, range.Start))
			{
				return;
			}
			int num = 0;
			while ((long)num < (long)((ulong)range.Length))
			{
				this.ReadMetadataToken(CodedIndex.HasCustomAttribute);
				MethodReference constructor = (MethodReference)this.LookupToken(this.ReadMetadataToken(CodedIndex.CustomAttributeType));
				uint signature = this.ReadBlobIndex();
				custom_attributes.Add(new CustomAttribute(signature, constructor));
				num++;
			}
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0000FCB8 File Offset: 0x0000DEB8
		private static int RangesSize(Range[] ranges)
		{
			uint num = 0U;
			for (int i = 0; i < ranges.Length; i++)
			{
				num += ranges[i].Length;
			}
			return (int)num;
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000FCE5 File Offset: 0x0000DEE5
		public byte[] ReadCustomAttributeBlob(uint signature)
		{
			return this.ReadBlob(signature);
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0000FCF0 File Offset: 0x0000DEF0
		public void ReadCustomAttributeSignature(CustomAttribute attribute)
		{
			SignatureReader signatureReader = this.ReadSignature(attribute.signature);
			if (!signatureReader.CanReadMore())
			{
				return;
			}
			if (signatureReader.ReadUInt16() != 1)
			{
				throw new InvalidOperationException();
			}
			MethodReference constructor = attribute.Constructor;
			if (constructor.HasParameters)
			{
				signatureReader.ReadCustomAttributeConstructorArguments(attribute, constructor.Parameters);
			}
			if (!signatureReader.CanReadMore())
			{
				return;
			}
			ushort num = signatureReader.ReadUInt16();
			if (num == 0)
			{
				return;
			}
			signatureReader.ReadCustomAttributeNamedArguments(num, ref attribute.fields, ref attribute.properties);
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0000FD68 File Offset: 0x0000DF68
		private void InitializeMarshalInfos()
		{
			if (this.metadata.FieldMarshals != null)
			{
				return;
			}
			int num = this.MoveTo(Table.FieldMarshal);
			Dictionary<MetadataToken, uint> dictionary = this.metadata.FieldMarshals = new Dictionary<MetadataToken, uint>(num);
			for (int i = 0; i < num; i++)
			{
				MetadataToken key = this.ReadMetadataToken(CodedIndex.HasFieldMarshal);
				uint value = this.ReadBlobIndex();
				if (key.RID != 0U)
				{
					dictionary.Add(key, value);
				}
			}
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0000FDD1 File Offset: 0x0000DFD1
		public bool HasMarshalInfo(IMarshalInfoProvider owner)
		{
			this.InitializeMarshalInfos();
			return this.metadata.FieldMarshals.ContainsKey(owner.MetadataToken);
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0000FDF0 File Offset: 0x0000DFF0
		public MarshalInfo ReadMarshalInfo(IMarshalInfoProvider owner)
		{
			this.InitializeMarshalInfos();
			uint signature;
			if (!this.metadata.FieldMarshals.TryGetValue(owner.MetadataToken, out signature))
			{
				return null;
			}
			SignatureReader signatureReader = this.ReadSignature(signature);
			this.metadata.FieldMarshals.Remove(owner.MetadataToken);
			return signatureReader.ReadMarshalInfo();
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0000FE68 File Offset: 0x0000E068
		private void InitializeSecurityDeclarations()
		{
			if (this.metadata.SecurityDeclarations != null)
			{
				return;
			}
			this.metadata.SecurityDeclarations = this.InitializeRanges(Table.DeclSecurity, delegate
			{
				base.ReadUInt16();
				MetadataToken result = this.ReadMetadataToken(CodedIndex.HasDeclSecurity);
				this.ReadBlobIndex();
				return result;
			});
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0000FE98 File Offset: 0x0000E098
		public bool HasSecurityDeclarations(ISecurityDeclarationProvider owner)
		{
			this.InitializeSecurityDeclarations();
			Range[] ranges;
			return this.metadata.TryGetSecurityDeclarationRanges(owner, out ranges) && MetadataReader.RangesSize(ranges) > 0;
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000FEC8 File Offset: 0x0000E0C8
		public Collection<SecurityDeclaration> ReadSecurityDeclarations(ISecurityDeclarationProvider owner)
		{
			this.InitializeSecurityDeclarations();
			Range[] array;
			if (!this.metadata.TryGetSecurityDeclarationRanges(owner, out array))
			{
				return new Collection<SecurityDeclaration>();
			}
			Collection<SecurityDeclaration> collection = new Collection<SecurityDeclaration>(MetadataReader.RangesSize(array));
			for (int i = 0; i < array.Length; i++)
			{
				this.ReadSecurityDeclarationRange(array[i], collection);
			}
			this.metadata.RemoveSecurityDeclarationRange(owner);
			return collection;
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0000FF2C File Offset: 0x0000E12C
		private void ReadSecurityDeclarationRange(Range range, Collection<SecurityDeclaration> security_declarations)
		{
			if (!this.MoveTo(Table.DeclSecurity, range.Start))
			{
				return;
			}
			int num = 0;
			while ((long)num < (long)((ulong)range.Length))
			{
				SecurityAction action = (SecurityAction)base.ReadUInt16();
				this.ReadMetadataToken(CodedIndex.HasDeclSecurity);
				uint signature = this.ReadBlobIndex();
				security_declarations.Add(new SecurityDeclaration(action, signature, this.module));
				num++;
			}
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000FF88 File Offset: 0x0000E188
		public byte[] ReadSecurityDeclarationBlob(uint signature)
		{
			return this.ReadBlob(signature);
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000FF94 File Offset: 0x0000E194
		public void ReadSecurityDeclarationSignature(SecurityDeclaration declaration)
		{
			uint signature = declaration.signature;
			SignatureReader signatureReader = this.ReadSignature(signature);
			if (signatureReader.buffer[signatureReader.position] != 46)
			{
				this.ReadXmlSecurityDeclaration(signature, declaration);
				return;
			}
			signatureReader.position++;
			uint num = signatureReader.ReadCompressedUInt32();
			Collection<SecurityAttribute> collection = new Collection<SecurityAttribute>((int)num);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				collection.Add(signatureReader.ReadSecurityAttribute());
				num2++;
			}
			declaration.security_attributes = collection;
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0001000C File Offset: 0x0000E20C
		private void ReadXmlSecurityDeclaration(uint signature, SecurityDeclaration declaration)
		{
			byte[] array = this.ReadBlob(signature);
			declaration.security_attributes = new Collection<SecurityAttribute>(1)
			{
				new SecurityAttribute(this.module.TypeSystem.LookupType("System.Security.Permissions", "PermissionSetAttribute"))
				{
					properties = new Collection<CustomAttributeNamedArgument>(1),
					properties = 
					{
						new CustomAttributeNamedArgument("XML", new CustomAttributeArgument(this.module.TypeSystem.String, Encoding.Unicode.GetString(array, 0, array.Length)))
					}
				}
			};
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0001009C File Offset: 0x0000E29C
		public Collection<ExportedType> ReadExportedTypes()
		{
			int num = this.MoveTo(Table.ExportedType);
			if (num == 0)
			{
				return new Collection<ExportedType>();
			}
			Collection<ExportedType> collection = new Collection<ExportedType>(num);
			for (int i = 1; i <= num; i++)
			{
				TypeAttributes attributes = (TypeAttributes)base.ReadUInt32();
				uint identifier = base.ReadUInt32();
				string name = this.ReadString();
				string @namespace = this.ReadString();
				MetadataToken token = this.ReadMetadataToken(CodedIndex.Implementation);
				ExportedType declaringType = null;
				IMetadataScope scope = null;
				TokenType tokenType = token.TokenType;
				if (tokenType != TokenType.AssemblyRef && tokenType != TokenType.File)
				{
					if (tokenType == TokenType.ExportedType)
					{
						declaringType = collection[(int)(token.RID - 1U)];
					}
				}
				else
				{
					scope = this.GetExportedTypeScope(token);
				}
				ExportedType exportedType = new ExportedType(@namespace, name, this.module, scope)
				{
					Attributes = attributes,
					Identifier = (int)identifier,
					DeclaringType = declaringType
				};
				exportedType.token = new MetadataToken(TokenType.ExportedType, i);
				collection.Add(exportedType);
			}
			return collection;
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00010194 File Offset: 0x0000E394
		private IMetadataScope GetExportedTypeScope(MetadataToken token)
		{
			int position = this.position;
			TokenType tokenType = token.TokenType;
			IMetadataScope result;
			if (tokenType != TokenType.AssemblyRef)
			{
				if (tokenType != TokenType.File)
				{
					throw new NotSupportedException();
				}
				this.InitializeModuleReferences();
				result = this.GetModuleReferenceFromFile(token);
			}
			else
			{
				this.InitializeAssemblyReferences();
				result = this.metadata.AssemblyReferences[(int)(token.RID - 1U)];
			}
			this.position = position;
			return result;
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00010200 File Offset: 0x0000E400
		private ModuleReference GetModuleReferenceFromFile(MetadataToken token)
		{
			if (!this.MoveTo(Table.File, token.RID))
			{
				return null;
			}
			base.ReadUInt32();
			string text = this.ReadString();
			Collection<ModuleReference> moduleReferences = this.module.ModuleReferences;
			ModuleReference moduleReference;
			for (int i = 0; i < moduleReferences.Count; i++)
			{
				moduleReference = moduleReferences[i];
				if (moduleReference.Name == text)
				{
					return moduleReference;
				}
			}
			moduleReference = new ModuleReference(text);
			moduleReferences.Add(moduleReference);
			return moduleReference;
		}

		// Token: 0x060003DC RID: 988 RVA: 0x00010272 File Offset: 0x0000E472
		private static void InitializeCollection(object o)
		{
		}

		// Token: 0x040003A4 RID: 932
		internal readonly Image image;

		// Token: 0x040003A5 RID: 933
		internal readonly ModuleDefinition module;

		// Token: 0x040003A6 RID: 934
		internal readonly MetadataSystem metadata;

		// Token: 0x040003A7 RID: 935
		internal IGenericContext context;

		// Token: 0x040003A8 RID: 936
		internal CodeReader code;
	}
}
