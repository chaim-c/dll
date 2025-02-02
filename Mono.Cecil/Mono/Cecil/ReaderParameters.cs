using System;
using System.IO;
using Mono.Cecil.Cil;

namespace Mono.Cecil
{
	// Token: 0x020000E0 RID: 224
	public sealed class ReaderParameters
	{
		// Token: 0x1700024F RID: 591
		// (get) Token: 0x0600087F RID: 2175 RVA: 0x0001BA10 File Offset: 0x00019C10
		// (set) Token: 0x06000880 RID: 2176 RVA: 0x0001BA18 File Offset: 0x00019C18
		public ReadingMode ReadingMode
		{
			get
			{
				return this.reading_mode;
			}
			set
			{
				this.reading_mode = value;
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000881 RID: 2177 RVA: 0x0001BA21 File Offset: 0x00019C21
		// (set) Token: 0x06000882 RID: 2178 RVA: 0x0001BA29 File Offset: 0x00019C29
		public IAssemblyResolver AssemblyResolver
		{
			get
			{
				return this.assembly_resolver;
			}
			set
			{
				this.assembly_resolver = value;
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000883 RID: 2179 RVA: 0x0001BA32 File Offset: 0x00019C32
		// (set) Token: 0x06000884 RID: 2180 RVA: 0x0001BA3A File Offset: 0x00019C3A
		public IMetadataResolver MetadataResolver
		{
			get
			{
				return this.metadata_resolver;
			}
			set
			{
				this.metadata_resolver = value;
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000885 RID: 2181 RVA: 0x0001BA43 File Offset: 0x00019C43
		// (set) Token: 0x06000886 RID: 2182 RVA: 0x0001BA4B File Offset: 0x00019C4B
		public Stream SymbolStream
		{
			get
			{
				return this.symbol_stream;
			}
			set
			{
				this.symbol_stream = value;
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000887 RID: 2183 RVA: 0x0001BA54 File Offset: 0x00019C54
		// (set) Token: 0x06000888 RID: 2184 RVA: 0x0001BA5C File Offset: 0x00019C5C
		public ISymbolReaderProvider SymbolReaderProvider
		{
			get
			{
				return this.symbol_reader_provider;
			}
			set
			{
				this.symbol_reader_provider = value;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000889 RID: 2185 RVA: 0x0001BA65 File Offset: 0x00019C65
		// (set) Token: 0x0600088A RID: 2186 RVA: 0x0001BA6D File Offset: 0x00019C6D
		public bool ReadSymbols
		{
			get
			{
				return this.read_symbols;
			}
			set
			{
				this.read_symbols = value;
			}
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x0001BA76 File Offset: 0x00019C76
		public ReaderParameters() : this(ReadingMode.Deferred)
		{
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x0001BA7F File Offset: 0x00019C7F
		public ReaderParameters(ReadingMode readingMode)
		{
			this.reading_mode = readingMode;
		}

		// Token: 0x04000559 RID: 1369
		private ReadingMode reading_mode;

		// Token: 0x0400055A RID: 1370
		private IAssemblyResolver assembly_resolver;

		// Token: 0x0400055B RID: 1371
		private IMetadataResolver metadata_resolver;

		// Token: 0x0400055C RID: 1372
		private Stream symbol_stream;

		// Token: 0x0400055D RID: 1373
		private ISymbolReaderProvider symbol_reader_provider;

		// Token: 0x0400055E RID: 1374
		private bool read_symbols;
	}
}
