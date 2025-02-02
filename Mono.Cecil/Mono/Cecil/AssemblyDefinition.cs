using System;
using System.IO;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x02000058 RID: 88
	public sealed class AssemblyDefinition : ICustomAttributeProvider, ISecurityDeclarationProvider, IMetadataTokenProvider
	{
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x0000C1CF File Offset: 0x0000A3CF
		// (set) Token: 0x060002D7 RID: 727 RVA: 0x0000C1D7 File Offset: 0x0000A3D7
		public AssemblyNameDefinition Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x0000C1E0 File Offset: 0x0000A3E0
		public string FullName
		{
			get
			{
				if (this.name == null)
				{
					return string.Empty;
				}
				return this.name.FullName;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x0000C1FB File Offset: 0x0000A3FB
		// (set) Token: 0x060002DA RID: 730 RVA: 0x0000C208 File Offset: 0x0000A408
		public MetadataToken MetadataToken
		{
			get
			{
				return new MetadataToken(TokenType.Assembly, 1);
			}
			set
			{
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060002DB RID: 731 RVA: 0x0000C214 File Offset: 0x0000A414
		public Collection<ModuleDefinition> Modules
		{
			get
			{
				if (this.modules != null)
				{
					return this.modules;
				}
				if (this.main_module.HasImage)
				{
					return this.main_module.Read<AssemblyDefinition, Collection<ModuleDefinition>>(ref this.modules, this, (AssemblyDefinition _, MetadataReader reader) => reader.ReadModules());
				}
				return this.modules = new Collection<ModuleDefinition>(1)
				{
					this.main_module
				};
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060002DC RID: 732 RVA: 0x0000C28A File Offset: 0x0000A48A
		public ModuleDefinition MainModule
		{
			get
			{
				return this.main_module;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060002DD RID: 733 RVA: 0x0000C292 File Offset: 0x0000A492
		// (set) Token: 0x060002DE RID: 734 RVA: 0x0000C29F File Offset: 0x0000A49F
		public MethodDefinition EntryPoint
		{
			get
			{
				return this.main_module.EntryPoint;
			}
			set
			{
				this.main_module.EntryPoint = value;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060002DF RID: 735 RVA: 0x0000C2AD File Offset: 0x0000A4AD
		public bool HasCustomAttributes
		{
			get
			{
				if (this.custom_attributes != null)
				{
					return this.custom_attributes.Count > 0;
				}
				return this.GetHasCustomAttributes(this.main_module);
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x0000C2D2 File Offset: 0x0000A4D2
		public Collection<CustomAttribute> CustomAttributes
		{
			get
			{
				return this.custom_attributes ?? this.GetCustomAttributes(ref this.custom_attributes, this.main_module);
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x0000C2F0 File Offset: 0x0000A4F0
		public bool HasSecurityDeclarations
		{
			get
			{
				if (this.security_declarations != null)
				{
					return this.security_declarations.Count > 0;
				}
				return this.GetHasSecurityDeclarations(this.main_module);
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x0000C315 File Offset: 0x0000A515
		public Collection<SecurityDeclaration> SecurityDeclarations
		{
			get
			{
				return this.security_declarations ?? this.GetSecurityDeclarations(ref this.security_declarations, this.main_module);
			}
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000C333 File Offset: 0x0000A533
		internal AssemblyDefinition()
		{
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000C33C File Offset: 0x0000A53C
		public static AssemblyDefinition CreateAssembly(AssemblyNameDefinition assemblyName, string moduleName, ModuleKind kind)
		{
			return AssemblyDefinition.CreateAssembly(assemblyName, moduleName, new ModuleParameters
			{
				Kind = kind
			});
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000C360 File Offset: 0x0000A560
		public static AssemblyDefinition CreateAssembly(AssemblyNameDefinition assemblyName, string moduleName, ModuleParameters parameters)
		{
			if (assemblyName == null)
			{
				throw new ArgumentNullException("assemblyName");
			}
			if (moduleName == null)
			{
				throw new ArgumentNullException("moduleName");
			}
			Mixin.CheckParameters(parameters);
			if (parameters.Kind == ModuleKind.NetModule)
			{
				throw new ArgumentException("kind");
			}
			AssemblyDefinition assembly = ModuleDefinition.CreateModule(moduleName, parameters).Assembly;
			assembly.Name = assemblyName;
			return assembly;
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000C3B8 File Offset: 0x0000A5B8
		public static AssemblyDefinition ReadAssembly(string fileName)
		{
			return AssemblyDefinition.ReadAssembly(ModuleDefinition.ReadModule(fileName));
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000C3C5 File Offset: 0x0000A5C5
		public static AssemblyDefinition ReadAssembly(string fileName, ReaderParameters parameters)
		{
			return AssemblyDefinition.ReadAssembly(ModuleDefinition.ReadModule(fileName, parameters));
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000C3D3 File Offset: 0x0000A5D3
		public static AssemblyDefinition ReadAssembly(Stream stream)
		{
			return AssemblyDefinition.ReadAssembly(ModuleDefinition.ReadModule(stream));
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000C3E0 File Offset: 0x0000A5E0
		public static AssemblyDefinition ReadAssembly(Stream stream, ReaderParameters parameters)
		{
			return AssemblyDefinition.ReadAssembly(ModuleDefinition.ReadModule(stream, parameters));
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000C3F0 File Offset: 0x0000A5F0
		private static AssemblyDefinition ReadAssembly(ModuleDefinition module)
		{
			AssemblyDefinition assembly = module.Assembly;
			if (assembly == null)
			{
				throw new ArgumentException();
			}
			return assembly;
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000C40E File Offset: 0x0000A60E
		public void Write(string fileName)
		{
			this.Write(fileName, new WriterParameters());
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000C41C File Offset: 0x0000A61C
		public void Write(Stream stream)
		{
			this.Write(stream, new WriterParameters());
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000C42A File Offset: 0x0000A62A
		public void Write(string fileName, WriterParameters parameters)
		{
			this.main_module.Write(fileName, parameters);
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000C439 File Offset: 0x0000A639
		public void Write(Stream stream, WriterParameters parameters)
		{
			this.main_module.Write(stream, parameters);
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000C448 File Offset: 0x0000A648
		public override string ToString()
		{
			return this.FullName;
		}

		// Token: 0x04000384 RID: 900
		private AssemblyNameDefinition name;

		// Token: 0x04000385 RID: 901
		internal ModuleDefinition main_module;

		// Token: 0x04000386 RID: 902
		private Collection<ModuleDefinition> modules;

		// Token: 0x04000387 RID: 903
		private Collection<CustomAttribute> custom_attributes;

		// Token: 0x04000388 RID: 904
		private Collection<SecurityDeclaration> security_declarations;
	}
}
