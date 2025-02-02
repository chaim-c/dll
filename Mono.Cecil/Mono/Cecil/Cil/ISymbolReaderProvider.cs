using System;
using System.IO;

namespace Mono.Cecil.Cil
{
	// Token: 0x02000023 RID: 35
	public interface ISymbolReaderProvider
	{
		// Token: 0x06000163 RID: 355
		ISymbolReader GetSymbolReader(ModuleDefinition module, string fileName);

		// Token: 0x06000164 RID: 356
		ISymbolReader GetSymbolReader(ModuleDefinition module, Stream symbolStream);
	}
}
