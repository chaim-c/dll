using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using Mono.Cecil.Cil;
using Mono.Cecil.Metadata;
using Mono.Cecil.PE;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x020000E3 RID: 227
	public sealed class ModuleDefinition : ModuleReference, ICustomAttributeProvider, IMetadataTokenProvider
	{
		// Token: 0x1700025E RID: 606
		// (get) Token: 0x060008A2 RID: 2210 RVA: 0x0001BB6B File Offset: 0x00019D6B
		public bool IsMain
		{
			get
			{
				return this.kind != ModuleKind.NetModule;
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x060008A3 RID: 2211 RVA: 0x0001BB79 File Offset: 0x00019D79
		// (set) Token: 0x060008A4 RID: 2212 RVA: 0x0001BB81 File Offset: 0x00019D81
		public ModuleKind Kind
		{
			get
			{
				return this.kind;
			}
			set
			{
				this.kind = value;
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x060008A5 RID: 2213 RVA: 0x0001BB8A File Offset: 0x00019D8A
		// (set) Token: 0x060008A6 RID: 2214 RVA: 0x0001BB92 File Offset: 0x00019D92
		public TargetRuntime Runtime
		{
			get
			{
				return this.runtime;
			}
			set
			{
				this.runtime = value;
				this.runtime_version = this.runtime.RuntimeVersionString();
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x060008A7 RID: 2215 RVA: 0x0001BBAC File Offset: 0x00019DAC
		// (set) Token: 0x060008A8 RID: 2216 RVA: 0x0001BBB4 File Offset: 0x00019DB4
		public string RuntimeVersion
		{
			get
			{
				return this.runtime_version;
			}
			set
			{
				this.runtime_version = value;
				this.runtime = this.runtime_version.ParseRuntime();
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x060008A9 RID: 2217 RVA: 0x0001BBCE File Offset: 0x00019DCE
		// (set) Token: 0x060008AA RID: 2218 RVA: 0x0001BBD6 File Offset: 0x00019DD6
		public TargetArchitecture Architecture
		{
			get
			{
				return this.architecture;
			}
			set
			{
				this.architecture = value;
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x060008AB RID: 2219 RVA: 0x0001BBDF File Offset: 0x00019DDF
		// (set) Token: 0x060008AC RID: 2220 RVA: 0x0001BBE7 File Offset: 0x00019DE7
		public ModuleAttributes Attributes
		{
			get
			{
				return this.attributes;
			}
			set
			{
				this.attributes = value;
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x060008AD RID: 2221 RVA: 0x0001BBF0 File Offset: 0x00019DF0
		// (set) Token: 0x060008AE RID: 2222 RVA: 0x0001BBF8 File Offset: 0x00019DF8
		public ModuleCharacteristics Characteristics
		{
			get
			{
				return this.characteristics;
			}
			set
			{
				this.characteristics = value;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x060008AF RID: 2223 RVA: 0x0001BC01 File Offset: 0x00019E01
		public string FullyQualifiedName
		{
			get
			{
				return this.fq_name;
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x060008B0 RID: 2224 RVA: 0x0001BC09 File Offset: 0x00019E09
		// (set) Token: 0x060008B1 RID: 2225 RVA: 0x0001BC11 File Offset: 0x00019E11
		public Guid Mvid
		{
			get
			{
				return this.mvid;
			}
			set
			{
				this.mvid = value;
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x060008B2 RID: 2226 RVA: 0x0001BC1A File Offset: 0x00019E1A
		internal bool HasImage
		{
			get
			{
				return this.Image != null;
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x060008B3 RID: 2227 RVA: 0x0001BC28 File Offset: 0x00019E28
		public bool HasSymbols
		{
			get
			{
				return this.symbol_reader != null;
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x060008B4 RID: 2228 RVA: 0x0001BC36 File Offset: 0x00019E36
		public ISymbolReader SymbolReader
		{
			get
			{
				return this.symbol_reader;
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x060008B5 RID: 2229 RVA: 0x0001BC3E File Offset: 0x00019E3E
		public override MetadataScopeType MetadataScopeType
		{
			get
			{
				return MetadataScopeType.ModuleDefinition;
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x060008B6 RID: 2230 RVA: 0x0001BC41 File Offset: 0x00019E41
		public AssemblyDefinition Assembly
		{
			get
			{
				return this.assembly;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x060008B7 RID: 2231 RVA: 0x0001BC49 File Offset: 0x00019E49
		internal MetadataImporter MetadataImporter
		{
			get
			{
				if (this.importer == null)
				{
					Interlocked.CompareExchange<MetadataImporter>(ref this.importer, new MetadataImporter(this), null);
				}
				return this.importer;
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x060008B8 RID: 2232 RVA: 0x0001BC6C File Offset: 0x00019E6C
		public IAssemblyResolver AssemblyResolver
		{
			get
			{
				if (this.assembly_resolver == null)
				{
					Interlocked.CompareExchange<IAssemblyResolver>(ref this.assembly_resolver, new DefaultAssemblyResolver(), null);
				}
				return this.assembly_resolver;
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x060008B9 RID: 2233 RVA: 0x0001BC8E File Offset: 0x00019E8E
		public IMetadataResolver MetadataResolver
		{
			get
			{
				if (this.metadata_resolver == null)
				{
					Interlocked.CompareExchange<IMetadataResolver>(ref this.metadata_resolver, new MetadataResolver(this.AssemblyResolver), null);
				}
				return this.metadata_resolver;
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x060008BA RID: 2234 RVA: 0x0001BCB6 File Offset: 0x00019EB6
		public TypeSystem TypeSystem
		{
			get
			{
				if (this.type_system == null)
				{
					Interlocked.CompareExchange<TypeSystem>(ref this.type_system, TypeSystem.CreateTypeSystem(this), null);
				}
				return this.type_system;
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x060008BB RID: 2235 RVA: 0x0001BCD9 File Offset: 0x00019ED9
		public bool HasAssemblyReferences
		{
			get
			{
				if (this.references != null)
				{
					return this.references.Count > 0;
				}
				return this.HasImage && this.Image.HasTable(Table.AssemblyRef);
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x060008BC RID: 2236 RVA: 0x0001BD14 File Offset: 0x00019F14
		public Collection<AssemblyNameReference> AssemblyReferences
		{
			get
			{
				if (this.references != null)
				{
					return this.references;
				}
				if (this.HasImage)
				{
					return this.Read<ModuleDefinition, Collection<AssemblyNameReference>>(ref this.references, this, (ModuleDefinition _, MetadataReader reader) => reader.ReadAssemblyReferences());
				}
				return this.references = new Collection<AssemblyNameReference>();
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x060008BD RID: 2237 RVA: 0x0001BD71 File Offset: 0x00019F71
		public bool HasModuleReferences
		{
			get
			{
				if (this.modules != null)
				{
					return this.modules.Count > 0;
				}
				return this.HasImage && this.Image.HasTable(Table.ModuleRef);
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x060008BE RID: 2238 RVA: 0x0001BDAC File Offset: 0x00019FAC
		public Collection<ModuleReference> ModuleReferences
		{
			get
			{
				if (this.modules != null)
				{
					return this.modules;
				}
				if (this.HasImage)
				{
					return this.Read<ModuleDefinition, Collection<ModuleReference>>(ref this.modules, this, (ModuleDefinition _, MetadataReader reader) => reader.ReadModuleReferences());
				}
				return this.modules = new Collection<ModuleReference>();
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x060008BF RID: 2239 RVA: 0x0001BE14 File Offset: 0x0001A014
		public bool HasResources
		{
			get
			{
				if (this.resources != null)
				{
					return this.resources.Count > 0;
				}
				if (!this.HasImage)
				{
					return false;
				}
				if (!this.Image.HasTable(Table.ManifestResource))
				{
					return this.Read<ModuleDefinition, bool>(this, (ModuleDefinition _, MetadataReader reader) => reader.HasFileResource());
				}
				return true;
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x060008C0 RID: 2240 RVA: 0x0001BE80 File Offset: 0x0001A080
		public Collection<Resource> Resources
		{
			get
			{
				if (this.resources != null)
				{
					return this.resources;
				}
				if (this.HasImage)
				{
					return this.Read<ModuleDefinition, Collection<Resource>>(ref this.resources, this, (ModuleDefinition _, MetadataReader reader) => reader.ReadResources());
				}
				return this.resources = new Collection<Resource>();
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x060008C1 RID: 2241 RVA: 0x0001BEDD File Offset: 0x0001A0DD
		public bool HasCustomAttributes
		{
			get
			{
				if (this.custom_attributes != null)
				{
					return this.custom_attributes.Count > 0;
				}
				return this.GetHasCustomAttributes(this);
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x060008C2 RID: 2242 RVA: 0x0001BEFD File Offset: 0x0001A0FD
		public Collection<CustomAttribute> CustomAttributes
		{
			get
			{
				return this.custom_attributes ?? this.GetCustomAttributes(ref this.custom_attributes, this);
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x060008C3 RID: 2243 RVA: 0x0001BF16 File Offset: 0x0001A116
		public bool HasTypes
		{
			get
			{
				if (this.types != null)
				{
					return this.types.Count > 0;
				}
				return this.HasImage && this.Image.HasTable(Table.TypeDef);
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x060008C4 RID: 2244 RVA: 0x0001BF50 File Offset: 0x0001A150
		public Collection<TypeDefinition> Types
		{
			get
			{
				if (this.types != null)
				{
					return this.types;
				}
				if (this.HasImage)
				{
					return this.Read<ModuleDefinition, TypeDefinitionCollection>(ref this.types, this, (ModuleDefinition _, MetadataReader reader) => reader.ReadTypes());
				}
				return this.types = new TypeDefinitionCollection(this);
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x060008C5 RID: 2245 RVA: 0x0001BFAE File Offset: 0x0001A1AE
		public bool HasExportedTypes
		{
			get
			{
				if (this.exported_types != null)
				{
					return this.exported_types.Count > 0;
				}
				return this.HasImage && this.Image.HasTable(Table.ExportedType);
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x060008C6 RID: 2246 RVA: 0x0001BFE8 File Offset: 0x0001A1E8
		public Collection<ExportedType> ExportedTypes
		{
			get
			{
				if (this.exported_types != null)
				{
					return this.exported_types;
				}
				if (this.HasImage)
				{
					return this.Read<ModuleDefinition, Collection<ExportedType>>(ref this.exported_types, this, (ModuleDefinition _, MetadataReader reader) => reader.ReadExportedTypes());
				}
				return this.exported_types = new Collection<ExportedType>();
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x060008C7 RID: 2247 RVA: 0x0001C050 File Offset: 0x0001A250
		// (set) Token: 0x060008C8 RID: 2248 RVA: 0x0001C0A9 File Offset: 0x0001A2A9
		public MethodDefinition EntryPoint
		{
			get
			{
				if (this.entry_point != null)
				{
					return this.entry_point;
				}
				if (this.HasImage)
				{
					return this.Read<ModuleDefinition, MethodDefinition>(ref this.entry_point, this, (ModuleDefinition _, MetadataReader reader) => reader.ReadEntryPoint());
				}
				return this.entry_point = null;
			}
			set
			{
				this.entry_point = value;
			}
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x0001C0B2 File Offset: 0x0001A2B2
		internal ModuleDefinition()
		{
			this.MetadataSystem = new MetadataSystem();
			this.token = new MetadataToken(TokenType.Module, 1);
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x0001C0E0 File Offset: 0x0001A2E0
		internal ModuleDefinition(Image image) : this()
		{
			this.Image = image;
			this.kind = image.Kind;
			this.RuntimeVersion = image.RuntimeVersion;
			this.architecture = image.Architecture;
			this.attributes = image.Attributes;
			this.characteristics = image.Characteristics;
			this.fq_name = image.FileName;
			this.reader = new MetadataReader(this);
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x0001C14E File Offset: 0x0001A34E
		public bool HasTypeReference(string fullName)
		{
			return this.HasTypeReference(string.Empty, fullName);
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x0001C15C File Offset: 0x0001A35C
		public bool HasTypeReference(string scope, string fullName)
		{
			ModuleDefinition.CheckFullName(fullName);
			return this.HasImage && this.GetTypeReference(scope, fullName) != null;
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x0001C17C File Offset: 0x0001A37C
		public bool TryGetTypeReference(string fullName, out TypeReference type)
		{
			return this.TryGetTypeReference(string.Empty, fullName, out type);
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x0001C18C File Offset: 0x0001A38C
		public bool TryGetTypeReference(string scope, string fullName, out TypeReference type)
		{
			ModuleDefinition.CheckFullName(fullName);
			if (!this.HasImage)
			{
				type = null;
				return false;
			}
			TypeReference typeReference;
			type = (typeReference = this.GetTypeReference(scope, fullName));
			return typeReference != null;
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x0001C1D5 File Offset: 0x0001A3D5
		private TypeReference GetTypeReference(string scope, string fullname)
		{
			return this.Read<Row<string, string>, TypeReference>(new Row<string, string>(scope, fullname), (Row<string, string> row, MetadataReader reader) => reader.GetTypeReference(row.Col1, row.Col2));
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x0001C209 File Offset: 0x0001A409
		public IEnumerable<TypeReference> GetTypeReferences()
		{
			if (!this.HasImage)
			{
				return Empty<TypeReference>.Array;
			}
			return this.Read<ModuleDefinition, IEnumerable<TypeReference>>(this, (ModuleDefinition _, MetadataReader reader) => reader.GetTypeReferences());
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x0001C245 File Offset: 0x0001A445
		public IEnumerable<MemberReference> GetMemberReferences()
		{
			if (!this.HasImage)
			{
				return Empty<MemberReference>.Array;
			}
			return this.Read<ModuleDefinition, IEnumerable<MemberReference>>(this, (ModuleDefinition _, MetadataReader reader) => reader.GetMemberReferences());
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x0001C279 File Offset: 0x0001A479
		public TypeReference GetType(string fullName, bool runtimeName)
		{
			if (!runtimeName)
			{
				return this.GetType(fullName);
			}
			return TypeParser.ParseType(this, fullName);
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x0001C290 File Offset: 0x0001A490
		public TypeDefinition GetType(string fullName)
		{
			ModuleDefinition.CheckFullName(fullName);
			int num = fullName.IndexOf('/');
			if (num > 0)
			{
				return this.GetNestedType(fullName);
			}
			return ((TypeDefinitionCollection)this.Types).GetType(fullName);
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x0001C2C9 File Offset: 0x0001A4C9
		public TypeDefinition GetType(string @namespace, string name)
		{
			Mixin.CheckName(name);
			return ((TypeDefinitionCollection)this.Types).GetType(@namespace ?? string.Empty, name);
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x0001C2EC File Offset: 0x0001A4EC
		public IEnumerable<TypeDefinition> GetTypes()
		{
			return ModuleDefinition.GetTypes(this.Types);
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x0001C51C File Offset: 0x0001A71C
		private static IEnumerable<TypeDefinition> GetTypes(Collection<TypeDefinition> types)
		{
			for (int i = 0; i < types.Count; i++)
			{
				TypeDefinition type = types[i];
				yield return type;
				if (type.HasNestedTypes)
				{
					foreach (TypeDefinition nested in ModuleDefinition.GetTypes(type.NestedTypes))
					{
						yield return nested;
					}
				}
			}
			yield break;
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x0001C539 File Offset: 0x0001A739
		private static void CheckFullName(string fullName)
		{
			if (fullName == null)
			{
				throw new ArgumentNullException("fullName");
			}
			if (fullName.Length == 0)
			{
				throw new ArgumentException();
			}
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x0001C558 File Offset: 0x0001A758
		private TypeDefinition GetNestedType(string fullname)
		{
			string[] array = fullname.Split(new char[]
			{
				'/'
			});
			TypeDefinition typeDefinition = this.GetType(array[0]);
			if (typeDefinition == null)
			{
				return null;
			}
			for (int i = 1; i < array.Length; i++)
			{
				TypeDefinition nestedType = typeDefinition.GetNestedType(array[i]);
				if (nestedType == null)
				{
					return null;
				}
				typeDefinition = nestedType;
			}
			return typeDefinition;
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x0001C5AB File Offset: 0x0001A7AB
		internal FieldDefinition Resolve(FieldReference field)
		{
			return this.MetadataResolver.Resolve(field);
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x0001C5B9 File Offset: 0x0001A7B9
		internal MethodDefinition Resolve(MethodReference method)
		{
			return this.MetadataResolver.Resolve(method);
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x0001C5C7 File Offset: 0x0001A7C7
		internal TypeDefinition Resolve(TypeReference type)
		{
			return this.MetadataResolver.Resolve(type);
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x0001C5D5 File Offset: 0x0001A7D5
		private static void CheckType(object type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x0001C5E5 File Offset: 0x0001A7E5
		private static void CheckField(object field)
		{
			if (field == null)
			{
				throw new ArgumentNullException("field");
			}
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x0001C5F5 File Offset: 0x0001A7F5
		private static void CheckMethod(object method)
		{
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x0001C605 File Offset: 0x0001A805
		private static void CheckContext(IGenericParameterProvider context, ModuleDefinition module)
		{
			if (context == null)
			{
				return;
			}
			if (context.Module != module)
			{
				throw new ArgumentException();
			}
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x0001C61C File Offset: 0x0001A81C
		private static ImportGenericContext GenericContextFor(IGenericParameterProvider context)
		{
			if (context == null)
			{
				return default(ImportGenericContext);
			}
			return new ImportGenericContext(context);
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x0001C63C File Offset: 0x0001A83C
		public TypeReference Import(Type type)
		{
			return this.Import(type, null);
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x0001C646 File Offset: 0x0001A846
		public TypeReference Import(Type type, IGenericParameterProvider context)
		{
			ModuleDefinition.CheckType(type);
			ModuleDefinition.CheckContext(context, this);
			return this.MetadataImporter.ImportType(type, ModuleDefinition.GenericContextFor(context), (context != null) ? ImportGenericKind.Open : ImportGenericKind.Definition);
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x0001C66E File Offset: 0x0001A86E
		public FieldReference Import(FieldInfo field)
		{
			return this.Import(field, null);
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x0001C678 File Offset: 0x0001A878
		public FieldReference Import(FieldInfo field, IGenericParameterProvider context)
		{
			ModuleDefinition.CheckField(field);
			ModuleDefinition.CheckContext(context, this);
			return this.MetadataImporter.ImportField(field, ModuleDefinition.GenericContextFor(context));
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x0001C69C File Offset: 0x0001A89C
		public MethodReference Import(MethodBase method)
		{
			ModuleDefinition.CheckMethod(method);
			return this.MetadataImporter.ImportMethod(method, default(ImportGenericContext), ImportGenericKind.Definition);
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x0001C6C5 File Offset: 0x0001A8C5
		public MethodReference Import(MethodBase method, IGenericParameterProvider context)
		{
			ModuleDefinition.CheckMethod(method);
			ModuleDefinition.CheckContext(context, this);
			return this.MetadataImporter.ImportMethod(method, ModuleDefinition.GenericContextFor(context), (context != null) ? ImportGenericKind.Open : ImportGenericKind.Definition);
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x0001C6F0 File Offset: 0x0001A8F0
		public TypeReference Import(TypeReference type)
		{
			ModuleDefinition.CheckType(type);
			if (type.Module == this)
			{
				return type;
			}
			return this.MetadataImporter.ImportType(type, default(ImportGenericContext));
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x0001C723 File Offset: 0x0001A923
		public TypeReference Import(TypeReference type, IGenericParameterProvider context)
		{
			ModuleDefinition.CheckType(type);
			if (type.Module == this)
			{
				return type;
			}
			ModuleDefinition.CheckContext(context, this);
			return this.MetadataImporter.ImportType(type, ModuleDefinition.GenericContextFor(context));
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x0001C750 File Offset: 0x0001A950
		public FieldReference Import(FieldReference field)
		{
			ModuleDefinition.CheckField(field);
			if (field.Module == this)
			{
				return field;
			}
			return this.MetadataImporter.ImportField(field, default(ImportGenericContext));
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x0001C783 File Offset: 0x0001A983
		public FieldReference Import(FieldReference field, IGenericParameterProvider context)
		{
			ModuleDefinition.CheckField(field);
			if (field.Module == this)
			{
				return field;
			}
			ModuleDefinition.CheckContext(context, this);
			return this.MetadataImporter.ImportField(field, ModuleDefinition.GenericContextFor(context));
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x0001C7AF File Offset: 0x0001A9AF
		public MethodReference Import(MethodReference method)
		{
			return this.Import(method, null);
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x0001C7B9 File Offset: 0x0001A9B9
		public MethodReference Import(MethodReference method, IGenericParameterProvider context)
		{
			ModuleDefinition.CheckMethod(method);
			if (method.Module == this)
			{
				return method;
			}
			ModuleDefinition.CheckContext(context, this);
			return this.MetadataImporter.ImportMethod(method, ModuleDefinition.GenericContextFor(context));
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x0001C7E5 File Offset: 0x0001A9E5
		public IMetadataTokenProvider LookupToken(int token)
		{
			return this.LookupToken(new MetadataToken((uint)token));
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x0001C7FC File Offset: 0x0001A9FC
		public IMetadataTokenProvider LookupToken(MetadataToken token)
		{
			return this.Read<MetadataToken, IMetadataTokenProvider>(token, (MetadataToken t, MetadataReader reader) => reader.LookupToken(t));
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x060008EF RID: 2287 RVA: 0x0001C822 File Offset: 0x0001AA22
		internal object SyncRoot
		{
			get
			{
				return this.module_lock;
			}
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x0001C82C File Offset: 0x0001AA2C
		internal TRet Read<TItem, TRet>(TItem item, Func<TItem, MetadataReader, TRet> read)
		{
			TRet result;
			lock (this.module_lock)
			{
				int position = this.reader.position;
				IGenericContext context = this.reader.context;
				TRet tret = read(item, this.reader);
				this.reader.position = position;
				this.reader.context = context;
				result = tret;
			}
			return result;
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x0001C8A4 File Offset: 0x0001AAA4
		internal TRet Read<TItem, TRet>(ref TRet variable, TItem item, Func<TItem, MetadataReader, TRet> read) where TRet : class
		{
			TRet result;
			lock (this.module_lock)
			{
				if (variable != null)
				{
					result = variable;
				}
				else
				{
					int position = this.reader.position;
					IGenericContext context = this.reader.context;
					TRet tret = read(item, this.reader);
					this.reader.position = position;
					this.reader.context = context;
					result = (variable = tret);
				}
			}
			return result;
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x060008F2 RID: 2290 RVA: 0x0001C93C File Offset: 0x0001AB3C
		public bool HasDebugHeader
		{
			get
			{
				return this.Image != null && !this.Image.Debug.IsZero;
			}
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x0001C95B File Offset: 0x0001AB5B
		public ImageDebugDirectory GetDebugHeader(out byte[] header)
		{
			if (!this.HasDebugHeader)
			{
				throw new InvalidOperationException();
			}
			return this.Image.GetDebugHeader(out header);
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x0001C978 File Offset: 0x0001AB78
		private void ProcessDebugHeader()
		{
			if (!this.HasDebugHeader)
			{
				return;
			}
			byte[] header;
			ImageDebugDirectory debugHeader = this.GetDebugHeader(out header);
			if (!this.symbol_reader.ProcessDebugHeader(debugHeader, header))
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x0001C9AC File Offset: 0x0001ABAC
		public static ModuleDefinition CreateModule(string name, ModuleKind kind)
		{
			return ModuleDefinition.CreateModule(name, new ModuleParameters
			{
				Kind = kind
			});
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x0001C9D0 File Offset: 0x0001ABD0
		public static ModuleDefinition CreateModule(string name, ModuleParameters parameters)
		{
			Mixin.CheckName(name);
			Mixin.CheckParameters(parameters);
			ModuleDefinition moduleDefinition = new ModuleDefinition
			{
				Name = name,
				kind = parameters.Kind,
				Runtime = parameters.Runtime,
				architecture = parameters.Architecture,
				mvid = Guid.NewGuid(),
				Attributes = ModuleAttributes.ILOnly,
				Characteristics = (ModuleCharacteristics.DynamicBase | ModuleCharacteristics.NoSEH | ModuleCharacteristics.NXCompat | ModuleCharacteristics.TerminalServerAware)
			};
			if (parameters.AssemblyResolver != null)
			{
				moduleDefinition.assembly_resolver = parameters.AssemblyResolver;
			}
			if (parameters.MetadataResolver != null)
			{
				moduleDefinition.metadata_resolver = parameters.MetadataResolver;
			}
			if (parameters.Kind != ModuleKind.NetModule)
			{
				AssemblyDefinition assemblyDefinition = new AssemblyDefinition();
				moduleDefinition.assembly = assemblyDefinition;
				moduleDefinition.assembly.Name = ModuleDefinition.CreateAssemblyName(name);
				assemblyDefinition.main_module = moduleDefinition;
			}
			moduleDefinition.Types.Add(new TypeDefinition(string.Empty, "<Module>", TypeAttributes.NotPublic));
			return moduleDefinition;
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x0001CAAB File Offset: 0x0001ACAB
		private static AssemblyNameDefinition CreateAssemblyName(string name)
		{
			if (name.EndsWith(".dll") || name.EndsWith(".exe"))
			{
				name = name.Substring(0, name.Length - 4);
			}
			return new AssemblyNameDefinition(name, new Version(0, 0, 0, 0));
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x0001CAE8 File Offset: 0x0001ACE8
		public void ReadSymbols()
		{
			if (string.IsNullOrEmpty(this.fq_name))
			{
				throw new InvalidOperationException();
			}
			ISymbolReaderProvider platformReaderProvider = SymbolProvider.GetPlatformReaderProvider();
			if (platformReaderProvider == null)
			{
				throw new InvalidOperationException();
			}
			this.ReadSymbols(platformReaderProvider.GetSymbolReader(this, this.fq_name));
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x0001CB2A File Offset: 0x0001AD2A
		public void ReadSymbols(ISymbolReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			this.symbol_reader = reader;
			this.ProcessDebugHeader();
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x0001CB47 File Offset: 0x0001AD47
		public static ModuleDefinition ReadModule(string fileName)
		{
			return ModuleDefinition.ReadModule(fileName, new ReaderParameters(ReadingMode.Deferred));
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x0001CB55 File Offset: 0x0001AD55
		public static ModuleDefinition ReadModule(Stream stream)
		{
			return ModuleDefinition.ReadModule(stream, new ReaderParameters(ReadingMode.Deferred));
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x0001CB64 File Offset: 0x0001AD64
		public static ModuleDefinition ReadModule(string fileName, ReaderParameters parameters)
		{
			ModuleDefinition result;
			using (Stream fileStream = ModuleDefinition.GetFileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				result = ModuleDefinition.ReadModule(fileStream, parameters);
			}
			return result;
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x0001CBA0 File Offset: 0x0001ADA0
		private static void CheckStream(object stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x0001CBB0 File Offset: 0x0001ADB0
		public static ModuleDefinition ReadModule(Stream stream, ReaderParameters parameters)
		{
			ModuleDefinition.CheckStream(stream);
			if (!stream.CanRead || !stream.CanSeek)
			{
				throw new ArgumentException();
			}
			Mixin.CheckParameters(parameters);
			return ModuleReader.CreateModuleFrom(ImageReader.ReadImageFrom(stream), parameters);
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x0001CBE0 File Offset: 0x0001ADE0
		private static Stream GetFileStream(string fileName, FileMode mode, FileAccess access, FileShare share)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			if (fileName.Length == 0)
			{
				throw new ArgumentException();
			}
			return new FileStream(fileName, mode, access, share);
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x0001CC07 File Offset: 0x0001AE07
		public void Write(string fileName)
		{
			this.Write(fileName, new WriterParameters());
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x0001CC15 File Offset: 0x0001AE15
		public void Write(Stream stream)
		{
			this.Write(stream, new WriterParameters());
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x0001CC24 File Offset: 0x0001AE24
		public void Write(string fileName, WriterParameters parameters)
		{
			using (Stream fileStream = ModuleDefinition.GetFileStream(fileName, FileMode.Create, FileAccess.ReadWrite, FileShare.None))
			{
				this.Write(fileStream, parameters);
			}
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x0001CC60 File Offset: 0x0001AE60
		public void Write(Stream stream, WriterParameters parameters)
		{
			ModuleDefinition.CheckStream(stream);
			if (!stream.CanWrite || !stream.CanSeek)
			{
				throw new ArgumentException();
			}
			Mixin.CheckParameters(parameters);
			ModuleWriter.WriteModuleTo(this, stream, parameters);
		}

		// Token: 0x04000568 RID: 1384
		internal Image Image;

		// Token: 0x04000569 RID: 1385
		internal MetadataSystem MetadataSystem;

		// Token: 0x0400056A RID: 1386
		internal ReadingMode ReadingMode;

		// Token: 0x0400056B RID: 1387
		internal ISymbolReaderProvider SymbolReaderProvider;

		// Token: 0x0400056C RID: 1388
		internal ISymbolReader symbol_reader;

		// Token: 0x0400056D RID: 1389
		internal IAssemblyResolver assembly_resolver;

		// Token: 0x0400056E RID: 1390
		internal IMetadataResolver metadata_resolver;

		// Token: 0x0400056F RID: 1391
		internal TypeSystem type_system;

		// Token: 0x04000570 RID: 1392
		private readonly MetadataReader reader;

		// Token: 0x04000571 RID: 1393
		private readonly string fq_name;

		// Token: 0x04000572 RID: 1394
		internal string runtime_version;

		// Token: 0x04000573 RID: 1395
		internal ModuleKind kind;

		// Token: 0x04000574 RID: 1396
		private TargetRuntime runtime;

		// Token: 0x04000575 RID: 1397
		private TargetArchitecture architecture;

		// Token: 0x04000576 RID: 1398
		private ModuleAttributes attributes;

		// Token: 0x04000577 RID: 1399
		private ModuleCharacteristics characteristics;

		// Token: 0x04000578 RID: 1400
		private Guid mvid;

		// Token: 0x04000579 RID: 1401
		internal AssemblyDefinition assembly;

		// Token: 0x0400057A RID: 1402
		private MethodDefinition entry_point;

		// Token: 0x0400057B RID: 1403
		private MetadataImporter importer;

		// Token: 0x0400057C RID: 1404
		private Collection<CustomAttribute> custom_attributes;

		// Token: 0x0400057D RID: 1405
		private Collection<AssemblyNameReference> references;

		// Token: 0x0400057E RID: 1406
		private Collection<ModuleReference> modules;

		// Token: 0x0400057F RID: 1407
		private Collection<Resource> resources;

		// Token: 0x04000580 RID: 1408
		private Collection<ExportedType> exported_types;

		// Token: 0x04000581 RID: 1409
		private TypeDefinitionCollection types;

		// Token: 0x04000582 RID: 1410
		private readonly object module_lock = new object();
	}
}
