using System;
using System.IO;
using System.Reflection;

namespace Mono.Cecil.Cil
{
	// Token: 0x02000024 RID: 36
	internal static class SymbolProvider
	{
		// Token: 0x06000165 RID: 357 RVA: 0x000074AC File Offset: 0x000056AC
		private static AssemblyName GetPlatformSymbolAssemblyName()
		{
			AssemblyName name = typeof(SymbolProvider).Assembly.GetName();
			AssemblyName assemblyName = new AssemblyName
			{
				Name = "Mono.Cecil." + SymbolProvider.symbol_kind,
				Version = name.Version
			};
			assemblyName.SetPublicKeyToken(name.GetPublicKeyToken());
			return assemblyName;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00007504 File Offset: 0x00005704
		private static Type GetPlatformType(string fullname)
		{
			Type type = Type.GetType(fullname);
			if (type != null)
			{
				return type;
			}
			AssemblyName platformSymbolAssemblyName = SymbolProvider.GetPlatformSymbolAssemblyName();
			type = Type.GetType(fullname + ", " + platformSymbolAssemblyName.FullName);
			if (type != null)
			{
				return type;
			}
			try
			{
				Assembly assembly = Assembly.Load(platformSymbolAssemblyName);
				if (assembly != null)
				{
					return assembly.GetType(fullname);
				}
			}
			catch (FileNotFoundException)
			{
			}
			catch (FileLoadException)
			{
			}
			return null;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000757C File Offset: 0x0000577C
		public static ISymbolReaderProvider GetPlatformReaderProvider()
		{
			if (SymbolProvider.reader_provider != null)
			{
				return SymbolProvider.reader_provider;
			}
			Type platformType = SymbolProvider.GetPlatformType(SymbolProvider.GetProviderTypeName("ReaderProvider"));
			if (platformType == null)
			{
				return null;
			}
			return SymbolProvider.reader_provider = (ISymbolReaderProvider)Activator.CreateInstance(platformType);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x000075BC File Offset: 0x000057BC
		private static string GetProviderTypeName(string name)
		{
			return string.Concat(new string[]
			{
				"Mono.Cecil.",
				SymbolProvider.symbol_kind,
				".",
				SymbolProvider.symbol_kind,
				name
			});
		}

		// Token: 0x06000169 RID: 361 RVA: 0x000075FC File Offset: 0x000057FC
		public static ISymbolWriterProvider GetPlatformWriterProvider()
		{
			if (SymbolProvider.writer_provider != null)
			{
				return SymbolProvider.writer_provider;
			}
			Type platformType = SymbolProvider.GetPlatformType(SymbolProvider.GetProviderTypeName("WriterProvider"));
			if (platformType == null)
			{
				return null;
			}
			return SymbolProvider.writer_provider = (ISymbolWriterProvider)Activator.CreateInstance(platformType);
		}

		// Token: 0x04000271 RID: 625
		private static readonly string symbol_kind = (Type.GetType("Mono.Runtime") != null) ? "Mdb" : "Pdb";

		// Token: 0x04000272 RID: 626
		private static ISymbolReaderProvider reader_provider;

		// Token: 0x04000273 RID: 627
		private static ISymbolWriterProvider writer_provider;
	}
}
