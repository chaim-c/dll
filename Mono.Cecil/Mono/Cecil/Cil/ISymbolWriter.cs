using System;

namespace Mono.Cecil.Cil
{
	// Token: 0x02000025 RID: 37
	public interface ISymbolWriter : IDisposable
	{
		// Token: 0x0600016B RID: 363
		bool GetDebugHeader(out ImageDebugDirectory directory, out byte[] header);

		// Token: 0x0600016C RID: 364
		void Write(MethodBody body);

		// Token: 0x0600016D RID: 365
		void Write(MethodSymbols symbols);
	}
}
