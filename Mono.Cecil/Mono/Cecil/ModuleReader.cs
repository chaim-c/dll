using System;
using Mono.Cecil.Cil;
using Mono.Cecil.PE;

namespace Mono.Cecil
{
	// Token: 0x02000060 RID: 96
	internal abstract class ModuleReader
	{
		// Token: 0x06000327 RID: 807 RVA: 0x0000CA2B File Offset: 0x0000AC2B
		protected ModuleReader(Image image, ReadingMode mode)
		{
			this.image = image;
			this.module = new ModuleDefinition(image);
			this.module.ReadingMode = mode;
		}

		// Token: 0x06000328 RID: 808
		protected abstract void ReadModule();

		// Token: 0x06000329 RID: 809 RVA: 0x0000CA52 File Offset: 0x0000AC52
		protected void ReadModuleManifest(MetadataReader reader)
		{
			reader.Populate(this.module);
			this.ReadAssembly(reader);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000CA68 File Offset: 0x0000AC68
		private void ReadAssembly(MetadataReader reader)
		{
			AssemblyNameDefinition assemblyNameDefinition = reader.ReadAssemblyNameDefinition();
			if (assemblyNameDefinition == null)
			{
				this.module.kind = ModuleKind.NetModule;
				return;
			}
			AssemblyDefinition assemblyDefinition = new AssemblyDefinition();
			assemblyDefinition.Name = assemblyNameDefinition;
			this.module.assembly = assemblyDefinition;
			assemblyDefinition.main_module = this.module;
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000CAB4 File Offset: 0x0000ACB4
		public static ModuleDefinition CreateModuleFrom(Image image, ReaderParameters parameters)
		{
			ModuleReader moduleReader = ModuleReader.CreateModuleReader(image, parameters.ReadingMode);
			ModuleDefinition moduleDefinition = moduleReader.module;
			if (parameters.AssemblyResolver != null)
			{
				moduleDefinition.assembly_resolver = parameters.AssemblyResolver;
			}
			if (parameters.MetadataResolver != null)
			{
				moduleDefinition.metadata_resolver = parameters.MetadataResolver;
			}
			moduleReader.ReadModule();
			ModuleReader.ReadSymbols(moduleDefinition, parameters);
			return moduleDefinition;
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000CB0C File Offset: 0x0000AD0C
		private static void ReadSymbols(ModuleDefinition module, ReaderParameters parameters)
		{
			ISymbolReaderProvider symbolReaderProvider = parameters.SymbolReaderProvider;
			if (symbolReaderProvider == null && parameters.ReadSymbols)
			{
				symbolReaderProvider = SymbolProvider.GetPlatformReaderProvider();
			}
			if (symbolReaderProvider != null)
			{
				module.SymbolReaderProvider = symbolReaderProvider;
				ISymbolReader reader = (parameters.SymbolStream != null) ? symbolReaderProvider.GetSymbolReader(module, parameters.SymbolStream) : symbolReaderProvider.GetSymbolReader(module, module.FullyQualifiedName);
				module.ReadSymbols(reader);
			}
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000CB68 File Offset: 0x0000AD68
		private static ModuleReader CreateModuleReader(Image image, ReadingMode mode)
		{
			switch (mode)
			{
			case ReadingMode.Immediate:
				return new ImmediateModuleReader(image);
			case ReadingMode.Deferred:
				return new DeferredModuleReader(image);
			default:
				throw new ArgumentException();
			}
		}

		// Token: 0x040003A2 RID: 930
		protected readonly Image image;

		// Token: 0x040003A3 RID: 931
		protected readonly ModuleDefinition module;
	}
}
