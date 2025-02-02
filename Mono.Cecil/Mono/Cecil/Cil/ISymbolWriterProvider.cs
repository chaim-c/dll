using System;
using System.IO;

namespace Mono.Cecil.Cil
{
	// Token: 0x02000026 RID: 38
	public interface ISymbolWriterProvider
	{
		// Token: 0x0600016E RID: 366
		ISymbolWriter GetSymbolWriter(ModuleDefinition module, string fileName);

		// Token: 0x0600016F RID: 367
		ISymbolWriter GetSymbolWriter(ModuleDefinition module, Stream symbolStream);
	}
}
