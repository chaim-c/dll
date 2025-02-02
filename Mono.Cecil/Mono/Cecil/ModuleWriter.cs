using System;
using System.IO;
using Mono.Cecil.Cil;
using Mono.Cecil.PE;

namespace Mono.Cecil
{
	// Token: 0x02000065 RID: 101
	internal static class ModuleWriter
	{
		// Token: 0x06000401 RID: 1025 RVA: 0x00010F44 File Offset: 0x0000F144
		public static void WriteModuleTo(ModuleDefinition module, Stream stream, WriterParameters parameters)
		{
			if ((module.Attributes & ModuleAttributes.ILOnly) == (ModuleAttributes)0)
			{
				throw new NotSupportedException("Writing mixed-mode assemblies is not supported");
			}
			if (module.HasImage && module.ReadingMode == ReadingMode.Deferred)
			{
				ImmediateModuleReader.ReadModule(module);
			}
			module.MetadataSystem.Clear();
			AssemblyNameDefinition assemblyNameDefinition = (module.assembly != null) ? module.assembly.Name : null;
			string fullyQualifiedName = stream.GetFullyQualifiedName();
			ISymbolWriterProvider symbolWriterProvider = parameters.SymbolWriterProvider;
			if (symbolWriterProvider == null && parameters.WriteSymbols)
			{
				symbolWriterProvider = SymbolProvider.GetPlatformWriterProvider();
			}
			ISymbolWriter symbolWriter = ModuleWriter.GetSymbolWriter(module, fullyQualifiedName, symbolWriterProvider);
			if (parameters.StrongNameKeyPair != null && assemblyNameDefinition != null)
			{
				assemblyNameDefinition.PublicKey = parameters.StrongNameKeyPair.PublicKey;
				module.Attributes |= ModuleAttributes.StrongNameSigned;
			}
			MetadataBuilder metadata = new MetadataBuilder(module, fullyQualifiedName, symbolWriterProvider, symbolWriter);
			ModuleWriter.BuildMetadata(module, metadata);
			if (module.symbol_reader != null)
			{
				module.symbol_reader.Dispose();
			}
			ImageWriter imageWriter = ImageWriter.CreateWriter(module, metadata, stream);
			imageWriter.WriteImage();
			if (parameters.StrongNameKeyPair != null)
			{
				CryptoService.StrongName(stream, imageWriter, parameters.StrongNameKeyPair);
			}
			if (symbolWriter != null)
			{
				symbolWriter.Dispose();
			}
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x00011051 File Offset: 0x0000F251
		private static void BuildMetadata(ModuleDefinition module, MetadataBuilder metadata)
		{
			if (!module.HasImage)
			{
				metadata.BuildMetadata();
				return;
			}
			module.Read<MetadataBuilder, MetadataBuilder>(metadata, delegate(MetadataBuilder builder, MetadataReader _)
			{
				builder.BuildMetadata();
				return builder;
			});
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00011087 File Offset: 0x0000F287
		private static ISymbolWriter GetSymbolWriter(ModuleDefinition module, string fq_name, ISymbolWriterProvider symbol_writer_provider)
		{
			if (symbol_writer_provider == null)
			{
				return null;
			}
			return symbol_writer_provider.GetSymbolWriter(module, fq_name);
		}
	}
}
