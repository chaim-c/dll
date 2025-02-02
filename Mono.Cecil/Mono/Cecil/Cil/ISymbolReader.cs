using System;

namespace Mono.Cecil.Cil
{
	// Token: 0x02000022 RID: 34
	public interface ISymbolReader : IDisposable
	{
		// Token: 0x06000160 RID: 352
		bool ProcessDebugHeader(ImageDebugDirectory directory, byte[] header);

		// Token: 0x06000161 RID: 353
		void Read(MethodBody body, InstructionMapper mapper);

		// Token: 0x06000162 RID: 354
		void Read(MethodSymbols symbols);
	}
}
