using System;
using System.IO;
using System.Reflection;
using Mono.Cecil.Cil;

namespace Mono.Cecil
{
	// Token: 0x020000E2 RID: 226
	public sealed class WriterParameters
	{
		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000899 RID: 2201 RVA: 0x0001BB1F File Offset: 0x00019D1F
		// (set) Token: 0x0600089A RID: 2202 RVA: 0x0001BB27 File Offset: 0x00019D27
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

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x0600089B RID: 2203 RVA: 0x0001BB30 File Offset: 0x00019D30
		// (set) Token: 0x0600089C RID: 2204 RVA: 0x0001BB38 File Offset: 0x00019D38
		public ISymbolWriterProvider SymbolWriterProvider
		{
			get
			{
				return this.symbol_writer_provider;
			}
			set
			{
				this.symbol_writer_provider = value;
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x0600089D RID: 2205 RVA: 0x0001BB41 File Offset: 0x00019D41
		// (set) Token: 0x0600089E RID: 2206 RVA: 0x0001BB49 File Offset: 0x00019D49
		public bool WriteSymbols
		{
			get
			{
				return this.write_symbols;
			}
			set
			{
				this.write_symbols = value;
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x0600089F RID: 2207 RVA: 0x0001BB52 File Offset: 0x00019D52
		// (set) Token: 0x060008A0 RID: 2208 RVA: 0x0001BB5A File Offset: 0x00019D5A
		public StrongNameKeyPair StrongNameKeyPair
		{
			get
			{
				return this.key_pair;
			}
			set
			{
				this.key_pair = value;
			}
		}

		// Token: 0x04000564 RID: 1380
		private Stream symbol_stream;

		// Token: 0x04000565 RID: 1381
		private ISymbolWriterProvider symbol_writer_provider;

		// Token: 0x04000566 RID: 1382
		private bool write_symbols;

		// Token: 0x04000567 RID: 1383
		private StrongNameKeyPair key_pair;
	}
}
